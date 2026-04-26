using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Microsoft;
using Microsoft.Office.Interop.Excel;
using NetOceanDirect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace oceanSR
{
    public partial class Form1 : Form
    {
        private class MyContainer
        {
            public List<double> xAxis;
            public List<double> yAxis;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private uint integrationTime;
        private OceanDirect ocean;
        private int deviceID = 0;
        private int errorCode = 0;
        private Complex[] complexData;
        private FGraphics fGraphics;

        private MyContainer signal_data = new MyContainer();
        private MyContainer fft_data = new MyContainer();
        private MyContainer phase_data = new MyContainer();
        private MyContainer spectrum_data = new MyContainer();

        private Random rdm = new Random();
        private Stopwatch stopWatch = new Stopwatch();
        private string save_path = "";
        private string save_name;
        private int maxValuesToShow = 500;

        private MyContainer FillArraysRandomly(int n)
        {
            MyContainer new_data = new MyContainer();
            new_data.xAxis = new List<double>();
            new_data.yAxis = new List<double>();
            for (int i = 0; i < n; i++)
            {
                new_data.xAxis.Add(i*5);
                new_data.yAxis.Add(rdm.Next(1000));
            }
            return new_data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false; //deshabilito para que no me den 2 veces por error
            ocean = OceanDirect.getInstance();
            Devices[] devs = ocean.findDevices();
            if (0 == devs.Length)
            {
                MessageBox.Show("Spectrometer not found");
                button1.Enabled = true;
                //habilito botón para medir temporalmente para yo ver las gráficas
                //integrationTime = 100000;
                //button2.Enabled = true;
            }
            else
            {
                deviceID = devs[0].Id; // el id del tarequito
                MessageBox.Show("Spectrometer found with ID: " + deviceID);
                ocean.openDevice(deviceID, ref errorCode);
                //var itime = ocean.getIntegrationTimeMicros(deviceID, ref errorCode);
                //MessageBox.Show("Default integration time: " + itime.ToString());
                integrationTime = (uint)numericUpDown1.Value;
                ocean.setIntegrationTimeMicros(deviceID, ref errorCode, integrationTime);
                // primera llamada para que se sincronicen
                double[] spectrum = ocean.getSpectrum(deviceID, ref errorCode);
                spectrum_data.yAxis = spectrum.ToList();
                double[] allWaveLengths = ocean.getWavelengths(deviceID, ref errorCode);
                spectrum_data.xAxis = allWaveLengths.ToList();
                //habilito botón para seleccionar carpeta
                btnFolder.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int min = (int)nudMinWave.Value;
            int max = (int)nudMaxWave.Value;
            if (min > max) MessageBox.Show("Max wavelenght must be greater than Min wavelenght",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                signal_data = new MyContainer();
                spectrum_data = new MyContainer();
                phase_data = new MyContainer();

                timer1.Interval = (int)(integrationTime / 1000); //interval es en ms, inegrationTime es en microseg

                fGraphics = new FGraphics();
                fGraphics.stop_measure = false;
                fGraphics.path = save_path;
                fGraphics.customName = save_name;

                // esto es temporal para yo probar las gráficas
                //signal_data = FillArraysRandomly(300);

                fGraphics.Show();

                timer1.Start();
                stopWatch.Restart();
            }
            
        }

        private Complex[] GetComplexData(List<double> signal)
        {
            Complex[] result = signal.Select(r => new Complex(r, 0)).ToArray();
            FFT(result);
            return result;
        }

        private List<double> FFT(Complex[] Data)
        {
            Fourier.Forward(Data, FourierOptions.Default); // FFT (in-place)
            return Data.Select(c => c.Magnitude).ToList();
        }

        private void FillFourierDataChart(FGraphics fGraphics)
        {
            complexData = GetComplexData(signal_data.yAxis);
            fft_data.yAxis = FFT(complexData);
            fft_data.yAxis.RemoveAt(0); // FFT tiene un pico raro en 0
            fft_data.xAxis = new List<double>();
            for (int i = 0; i < fft_data.yAxis.Count; i++)
                fft_data.xAxis.Add(i);

            if (fGraphics.yFourier_hist.Count > 0) //tiene datos de antes
            {
                fGraphics.yFourier_hist.AddRange(fft_data.yAxis);
                int idx = (int)fGraphics.xFourier_hist[fGraphics.xFourier_hist.Count - 1];
                for (int j = 0; j < fft_data.xAxis.Count; j++)
                    fGraphics.xFourier_hist.Add(idx + j + 1);
            }
            else
            {
                fGraphics.xFourier_hist = new List<double>(fft_data.xAxis);
                fGraphics.yFourier_hist = new List<double>(fft_data.yAxis);
                fGraphics.xFourier = new List<double>(fft_data.xAxis);
                fGraphics.yFourier = new List<double>(fft_data.yAxis);
            }
        }

        private void FillPhaseDataChart(FGraphics fGraphics, Complex[] complexData)
        {
            int last_id = 0; ;
            if (fGraphics.yPhase_hist.Count > 0)
                last_id = fGraphics.yPhase_hist.Count;

            phase_data.yAxis = CalculatePhase(complexData);
            phase_data.xAxis = new List<double>();
            for (int i = 0; i < phase_data.yAxis.Count; i++)
                phase_data.xAxis.Add(last_id + i);

            if (fGraphics.yPhase_hist.Count > 0) //tiene datos de antes
            {
                fGraphics.xPhase_hist.AddRange(phase_data.xAxis);
                fGraphics.yPhase_hist.AddRange(phase_data.yAxis);

                fGraphics.xPhase.AddRange(phase_data.xAxis);
                fGraphics.yPhase.AddRange(phase_data.yAxis);
            }
            else
            {
                fGraphics.xPhase_hist = new List<double>(phase_data.xAxis);
                fGraphics.yPhase_hist = new List<double>(phase_data.yAxis);

                fGraphics.xPhase = new List<double>(phase_data.xAxis);
                fGraphics.yPhase = new List<double>(phase_data.yAxis);
            }
            int count = fGraphics.xPhase.Count - maxValuesToShow;
            if (count > 0) //tengo más de los que puedo mostrar
            {
                fGraphics.xPhase.RemoveRange(0, count);
                fGraphics.yPhase.RemoveRange(0, count);
            }
        }

        private void FillIntensityDataChart(FGraphics fGraphics)
        {
            if (fGraphics.ySignal_hist.Count > 0) //tiene datos de antes
            {
                fGraphics.xSignal_hist.AddRange(signal_data.xAxis);
                fGraphics.ySignal_hist.AddRange(signal_data.yAxis);
                fGraphics.xSignal = signal_data.xAxis;
                fGraphics.ySignal = signal_data.yAxis;
                int count = signal_data.xAxis.Count - maxValuesToShow;
                if (count > 0) //tengo más de los que puedo mostrar
                {
                    fGraphics.xSignal.RemoveRange(0, count);
                    fGraphics.ySignal.RemoveRange(0, count);
                }
            }
            else
            {
                fGraphics.xSignal_hist = new List<double>(signal_data.xAxis);
                fGraphics.ySignal_hist = new List<double>(signal_data.yAxis);
                fGraphics.xSignal = new List<double>(signal_data.xAxis);
                fGraphics.ySignal = new List<double>(signal_data.yAxis);
            }
        }

        private void FillSpectrumDataChart(FGraphics fGraphics)
        {
            if (fGraphics.ySpectra.Count > 0) //tiene datos de antes
            {
                fGraphics.xSpectra.Clear();
                fGraphics.xSpectra.AddRange(spectrum_data.xAxis);
                fGraphics.ySpectra.Clear();
                fGraphics.ySpectra.AddRange(spectrum_data.yAxis);
            }
            else
            {
                fGraphics.xSpectra = new List<double>(spectrum_data.xAxis);
                fGraphics.xSpectra = new List<double>(spectrum_data.yAxis);
                fGraphics.ySpectra = new List<double>(spectrum_data.xAxis);
                fGraphics.ySpectra = new List<double>(spectrum_data.yAxis);
            }
        }

        private List<double> CalculatePhase(Complex[] fftResult)
        {
            List<double> phase = new List<double>();
            for (int i = 0; i < fftResult.Length; i++)
            {
                // Math.Atan2(y, x) -> Math.Atan2(imaginary, real)
                phase.Add(Math.Atan2(fftResult[i].Imaginary, fftResult[i].Real));
            }
            return phase; // Phase in radians
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ocean.closeDevice(deviceID, ref errorCode);
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            int val = (int)numericUpDown2.Value * 1000;
            if ((fGraphics.stop_measure) || (stopWatch.ElapsedMilliseconds >= val))
            {
                timer1.Stop();
                stopWatch.Stop();
                fGraphics.EnableSave();
            }
            else
            {
                try
                {
                    //signal_data = FillArraysRandomly(300);
                    double[] spectrum = ocean.getSpectrum(deviceID, ref errorCode);
                    double[] allWaveLengths = ocean.getWavelengths(deviceID, ref errorCode);
                    spectrum_data.xAxis = allWaveLengths.Select(x => Math.Round(x, 1)).ToList();
                    spectrum_data.yAxis = spectrum.ToList();
                    //The spectrum chart
                    FillSpectrumDataChart(fGraphics);
                    fGraphics.chart1.Series[0].Points.DataBindXY(fGraphics.xSpectra, fGraphics.ySpectra);

                    double current_value;
                    if (rbRange.Checked)
                    {
                        double lo = (int)nudMinWave.Value;
                        double hi = (int)nudMaxWave.Value;
                        int[] inxR = ocean.getIndicesAtWavelengthRange(deviceID, ref errorCode, ref allWaveLengths, lo, hi);
                        current_value = inxR.Select(i => spectrum_data.yAxis[i]).ToList().Average();
                    }
                    else
                    {
                        double wl = 0;
                        double approximateWavlength = (double)nudSingleWave.Value;
                        int p_index = ocean.getIndexAtWavelength(deviceID, ref errorCode, ref wl, approximateWavlength);
                        current_value = spectrum[p_index];
                    }
                    if (signal_data.xAxis != null)
                    {
                        signal_data.xAxis.Add(stopWatch.ElapsedMilliseconds);
                        signal_data.yAxis.Add(current_value);
                    }
                    else
                    {
                        signal_data.xAxis = new List<double>();
                        signal_data.xAxis.Add(stopWatch.ElapsedMilliseconds);
                        signal_data.yAxis = new List<double>();
                        signal_data.yAxis.Add(current_value);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                //the signal chart
                FillIntensityDataChart(fGraphics);
                fGraphics.chart2.Series[0].Points.DataBindXY(fGraphics.xSignal, fGraphics.ySignal);
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.xSignal[0];
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.xSignal[fGraphics.xSignal.Count - 1];

                /*
                FillFourierDataChart(fGraphics);
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.xFourier[0];
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.xFourier[fGraphics.xFourier.Count - 1];
                fGraphics.chart2.Series[0].Points.DataBindXY(fGraphics.xFourier, fGraphics.yFourier);
                */

                //the phase
                if (fGraphics.xSignal_hist.Count > 1)
                {
                    complexData = GetComplexData(fGraphics.ySignal_hist);
                    FillPhaseDataChart(fGraphics, complexData);
                    fGraphics.chart3.Series[0].Points.DataBindXY(fGraphics.xPhase, fGraphics.yPhase);
                    fGraphics.chart3.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.xPhase[0];
                    fGraphics.chart3.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.xPhase[fGraphics.xPhase.Count - 1];
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                save_path = folderBrowserDialog1.SelectedPath;
                save_name = tbxFileName.Text;
                button2.Enabled = true;
            }
            else MessageBox.Show("Please select a folder to save");
        }

        private void tbxFileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Get list of invalid characters for filenames
            char[] invalidChars = Path.GetInvalidFileNameChars();
            if (invalidChars.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
