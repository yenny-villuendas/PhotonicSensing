using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Random;

using NetOceanDirect;
using System;
using System.Collections.Generic;

using System.Data;
using System.Diagnostics;

using System.IO;
using System.Linq;
using System.Numerics;

using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace oceanSR
{
    public partial class Form1 : Form
    {
        public class MyContainer
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
        private FMultipleWave fWave = new FMultipleWave();

        private MyContainer signal_data = new MyContainer();
        private MyContainer fft_data = new MyContainer();
        private MyContainer phase_data = new MyContainer();
        private MyContainer spectrum_data = new MyContainer();

        private Random rdm = new Random();
        private Stopwatch stopWatch = new Stopwatch();
        private string save_path = "";
        private string save_name;
        private int maxValuesToShow = 50;
        int tick = 0;

        private List<int> MultipleWaves = new List<int>();
        private Dictionary<string, MyContainer> dic_signals = new Dictionary<string, MyContainer>();

        private MyContainer FillArraysRandomly(int n, int minX, int maxX, int minY, int maxY)
        {
            MyContainer new_data = new MyContainer();
            new_data.xAxis = new List<double>();
            new_data.yAxis = new List<double>();

            // Generating unique x-values in range
            HashSet<double> uniqueX = new HashSet<double>();
            while (uniqueX.Count < n)
                uniqueX.Add(Math.Round(minX + rdm.NextDouble() * (maxX - minX), 2));
            new_data.xAxis.AddRange(uniqueX.ToList());
            new_data.xAxis.Sort(); // sorting to plot later
            //Generating y-values
            for (int i = 0; i < n; i++)
                new_data.yAxis.Add(rdm.Next(minY, maxY));
            
            return new_data;
        }

        private MyContainer FillArraysNormally(int n, double mean, double stdDev)
        {
            MyContainer new_data = new MyContainer();
            new_data.xAxis = new List<double>();
            new_data.yAxis = new List<double>();

            Normal normalDist = new Normal(mean, stdDev, SystemRandomSource.Default);
            for (int i = 0; i < n; i++)
            {
                new_data.xAxis.Add(i +1);
                new_data.yAxis.Add(normalDist.Sample());
            }
            //new_data.yAxis.Sort();
            return new_data;
        }

        private MyContainer FillArraysNormallyPDF(int n, double mean, double stdDev)
        {
            MyContainer new_data = new MyContainer();
            new_data.xAxis = new List<double>();
            new_data.yAxis = new List<double>();

            Normal normalDist = new Normal(mean, stdDev, SystemRandomSource.Default);
            for (int i = 0; i < n; i++)
                new_data.xAxis.Add(i + 1);
            for (int i = 0; i < n; i++)
                new_data.yAxis.Add(normalDist.Density(new_data.xAxis[i]));
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
                //button2.Enabled = true;
                //integrationTime = 100000;
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
            button2.Enabled = false;
            btnRestart.Enabled = true;

            timer1.Interval = (int)(integrationTime / 1000); //interval es en ms, inegrationTime es en microseg

            fGraphics = new FGraphics();
            fGraphics.stop_measure = false;
            fGraphics.path = save_path;
            fGraphics.customName = save_name;

            //the signal chart and its series
            List<string> keys = dic_signals.Keys.ToList();
            string first = keys[0];
            fGraphics.chart2.Series[0].Name = "Signal at " + first;
            for (int i = 1; i < MultipleWaves.Count; i++)
            {
                string name = keys[i];
                System.Windows.Forms.DataVisualization.Charting.Series current_series = new System.Windows.Forms.DataVisualization.Charting.Series();
                fGraphics.chart2.Series.Add(current_series);
                fGraphics.chart2.Series[i].Name = "Signal at " + name;
                fGraphics.chart2.Series[i].ChartType = SeriesChartType.Line;
            }

            fGraphics.Show();

            /*
            #region Visual testing - static
            // esto es temporal para yo probar las gráficas
            spectrum_data = FillArraysRandomly(300, 630, 950, 0, 1000);
            FillSpectrumDataChart(fGraphics);
            fGraphics.chart1.Series[0].Points.DataBindXY(fGraphics.xSpectra, fGraphics.ySpectra);

            // las señales
            fGraphics.chart2.ChartAreas[0].AxisY.Title = "Intensity"; //"O\u2082"
            signal_data = FillArraysNormally(300, 200, 5); // mean=150, stdDev=5 for 300 samples
            FillIntensityDataChart(fGraphics);
            fGraphics.chart2.Series[0].Name = "NeuroChem 1";
            fGraphics.chart2.Series[0].Points.DataBindXY(fGraphics.xSignal, fGraphics.ySignal);
            // for the remaining series
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series("NeuroChem 2");
            fGraphics.chart2.Series.Add(series2);
            fGraphics.chart2.Series[1].ChartType = SeriesChartType.Line;
            MyContainer signal2_data = FillArraysNormally(300, 150, 5);
            fGraphics.chart2.Series[1].Points.DataBindXY(signal2_data.xAxis, signal2_data.yAxis);

            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series("NeuroChem 3");
            fGraphics.chart2.Series.Add(series3);
            fGraphics.chart2.Series[2].ChartType = SeriesChartType.Line;
            MyContainer signal3_data = FillArraysNormally(300, 100, 5);
            fGraphics.chart2.Series[2].Points.DataBindXY(signal3_data.xAxis, signal3_data.yAxis);

            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series("O₂");
            fGraphics.chart2.Series.Add(series4);
            fGraphics.chart2.Series[3].ChartType = SeriesChartType.Line;
            fGraphics.chart2.Series[3].Color = Color.Olive;
            MyContainer signal4_data = FillArraysNormally(300, 50, 5);
            fGraphics.chart2.Series[3].Points.DataBindXY(signal4_data.xAxis, signal4_data.yAxis);

            // las señales como pdf
            MyContainer phase1_sim = FillArraysNormallyPDF(300, 200, 5);
            fGraphics.chart3.Series[0].Name = "Sensor 1";
            fGraphics.chart3.Series[0].Color = Color.Red;
            fGraphics.chart3.Series[0].Points.DataBindXY(phase1_sim.xAxis, phase1_sim.yAxis);
            // for the remaining series
            System.Windows.Forms.DataVisualization.Charting.Series pha_series2 = new System.Windows.Forms.DataVisualization.Charting.Series("Sensor 2");
            fGraphics.chart3.Series.Add(pha_series2);
            fGraphics.chart3.Series[1].ChartType = SeriesChartType.Line;
            MyContainer phase2_sim = FillArraysNormallyPDF(300, 150, 5);
            fGraphics.chart3.Series[1].Points.DataBindXY(phase2_sim.xAxis, phase2_sim.yAxis);

            System.Windows.Forms.DataVisualization.Charting.Series pha_series3 = new System.Windows.Forms.DataVisualization.Charting.Series("Sensor 3");
            fGraphics.chart3.Series.Add(pha_series3);
            fGraphics.chart3.Series[2].ChartType = SeriesChartType.Line;
            MyContainer phase3_sim = FillArraysNormallyPDF(300, 100, 5);
            fGraphics.chart3.Series[2].Points.DataBindXY(phase3_sim.xAxis, phase3_sim.yAxis);

            System.Windows.Forms.DataVisualization.Charting.Series pha_series4 = new System.Windows.Forms.DataVisualization.Charting.Series("Sensor 4");
            fGraphics.chart3.Series.Add(pha_series4);
            fGraphics.chart3.Series[3].ChartType = SeriesChartType.Line;
            fGraphics.chart3.Series[3].Color = Color.Olive;
            MyContainer phase4_sim = FillArraysNormallyPDF(300, 50, 5);
            fGraphics.chart3.Series[3].Points.DataBindXY(phase4_sim.xAxis, phase4_sim.yAxis);

            fGraphics.Refresh();
            #endregion
            */

            timer1.Start();
            stopWatch.Restart();
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

        private void FillFourierDataChart(FGraphics fGraphics, List<double> data)
        {
            complexData = GetComplexData(data);
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
                phase_data.xAxis.Add(last_id + i + 1);

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
            List<string> keys = dic_signals.Keys.ToList();
            string first = keys[0];

            if ((fGraphics.stop_measure) || (stopWatch.ElapsedMilliseconds >= val))
            {
                timer1.Stop();
                stopWatch.Stop();
                // en caso de que termine y todavía no haya calculado la fase
                if (fGraphics.yPhase_hist.Count == 0)
                {
                    complexData = GetComplexData(fGraphics.chartDic_signals_hist[first].yAxis);
                    fGraphics.yPhase_hist = CalculatePhase(complexData);
                    fGraphics.xPhase_hist.Clear();
                    for (int i = 0; i < fGraphics.yPhase_hist.Count; i++)
                        fGraphics.xPhase_hist.Add(i + 1);
                    fGraphics.chart3.Series[0].Points.DataBindXY(fGraphics.xPhase_hist, fGraphics.yPhase_hist);
                    fGraphics.chart3.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.xPhase_hist[0];
                    fGraphics.chart3.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.xPhase_hist[fGraphics.xPhase_hist.Count - 1];
                }
                fGraphics.EnableSave();
            }
            else
            {
                try
                {
                    //signal_data = FillArraysRandomly(300, 0, 100, 0, 100);
                  
                    double[] spectrum = ocean.getSpectrum(deviceID, ref errorCode);
                    double[] allWaveLengths = ocean.getWavelengths(deviceID, ref errorCode);
                    spectrum_data.xAxis = allWaveLengths.Select(x => Math.Round(x, 1)).ToList();
                    spectrum_data.yAxis = spectrum.ToList();
                    //The spectrum chart
                    FillSpectrumDataChart(fGraphics);
                    fGraphics.chart1.Series[0].Points.DataBindXY(fGraphics.xSpectra, fGraphics.ySpectra);

                    // the intensity chart
                    double current_value;
                    if (rbRange.Checked)
                    {
                        string name = nudMinWave.Value.ToString() + "-" + nudMaxWave.Value.ToString();
                        double lo = (int)nudMinWave.Value;
                        double hi = (int)nudMaxWave.Value;
                        int[] inxR = ocean.getIndicesAtWavelengthRange(deviceID, ref errorCode, ref allWaveLengths, lo, hi);
                        current_value = Math.Round(inxR.Select(i => spectrum_data.yAxis[i]).ToList().Average(), 3);
                        FillSingleSignalData(name, current_value, stopWatch.ElapsedMilliseconds);
                    }
                    else if (rbValue.Checked)
                        {
                            double tmp = 0;
                            double approximateWavlength = (double)nudSingleWave.Value;
                            int idx = ocean.getIndexAtWavelength(deviceID, ref errorCode, ref tmp, approximateWavlength);
                            current_value = Math.Round(spectrum[idx], 3);
                            FillSingleSignalData(nudSingleWave.Value.ToString(), current_value, stopWatch.ElapsedMilliseconds);
                        }
                        else
                        {
                            // defino cuántas señales diferentes quiere (una por longitud de onda)
                            int signal_count = MultipleWaves.Count;
                            if (signal_count == 0) { throw new Exception("There are no selected wavelenghs"); }
                            double[] current_values = new double[signal_count];
                            //el ejemplo usa 4 índices: es como si midiera hasta 4 canales, dice:
                            // "up to 4 pixel (wavelength) positions to focus on" pero yo vi que funciona con los que uno quiera
                            for (int i = 0; i < signal_count; i++)
                            {
                                double tmp = 0;
                                int idx = ocean.getIndexAtWavelength(deviceID, ref errorCode, ref tmp, MultipleWaves[i]);
                                current_values[i] = Math.Round(spectrum[idx], 3);
                                //if (i == 0) current_values[i] = 200;
                            }
                            FillMultipleSignalData(current_values, stopWatch.ElapsedMilliseconds);
                        }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

                //the signal chart
                fGraphics.chart2.Series[0].Points.DataBindXY(fGraphics.chartDic_signals[first].xAxis, fGraphics.chartDic_signals[first].yAxis);
                for (int i = 1; i < MultipleWaves.Count; i++)
                {
                    string name = keys[i];
                    fGraphics.chart2.Series[i].Points.DataBindXY(fGraphics.chartDic_signals[name].xAxis, fGraphics.chartDic_signals[name].yAxis);
                }

                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.chartDic_signals[first].xAxis[0];
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.chartDic_signals[first].xAxis[fGraphics.chartDic_signals[first].xAxis.Count - 1];
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisY.Minimum = Double.NaN; // autoscale y-axis
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisY.Maximum = Double.NaN;
                /*
                FillFourierDataChart(fGraphics);
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.xFourier[0];
                fGraphics.chart2.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.xFourier[fGraphics.xFourier.Count - 1];
                fGraphics.chart2.Series[0].Points.DataBindXY(fGraphics.xFourier, fGraphics.yFourier);
                */

                //the phase
                tick++;
                if (tick%10 == 0)
                {
                    complexData = GetComplexData(fGraphics.chartDic_signals_hist[first].yAxis);
                    fGraphics.yPhase_hist = CalculatePhase(complexData);
                    fGraphics.xPhase_hist.Clear();
                    for (int i = 0; i < fGraphics.yPhase_hist.Count; i++)
                        fGraphics.xPhase_hist.Add(i + 1 );
                    fGraphics.xPhase.Clear();
                    fGraphics.yPhase.Clear();
                    // asumiendo que solo quiero mostrar maxNumber
                    if (fGraphics.yPhase_hist.Count > maxValuesToShow) // hay de más
                    {
                        int start = fGraphics.xPhase_hist.Count - maxValuesToShow;
                        fGraphics.xPhase.AddRange(fGraphics.xPhase_hist.GetRange(start, maxValuesToShow));
                        fGraphics.yPhase.AddRange(fGraphics.yPhase_hist.GetRange(start, maxValuesToShow));
                    }
                    else
                    {
                        fGraphics.xPhase.AddRange(fGraphics.xPhase_hist);
                        fGraphics.yPhase.AddRange(fGraphics.yPhase_hist);
                    }
                    fGraphics.chart3.Series[0].Points.DataBindXY(fGraphics.xPhase, fGraphics.yPhase);
                    fGraphics.chart3.ChartAreas["ChartArea1"].AxisX.Minimum = fGraphics.xPhase[0];
                    fGraphics.chart3.ChartAreas["ChartArea1"].AxisX.Maximum = fGraphics.xPhase[fGraphics.xPhase.Count - 1];
                }
            }
        }

        private void FillMultipleSignalData(double[] current_values, long elapsedMilliseconds)
        {
            for (int i = 0; i < current_values.Length; i++)
            {
                string name = MultipleWaves[i].ToString(); // los valores se corresponden con las señales
                if (dic_signals[name].xAxis != null)
                {
                    dic_signals[name].xAxis.Add(elapsedMilliseconds);
                    dic_signals[name].yAxis.Add(current_values[i]);
                }
                else
                {
                    dic_signals[name].xAxis = new List<double>();
                    dic_signals[name].xAxis.Add(elapsedMilliseconds);
                    dic_signals[name].yAxis = new List<double>();
                    dic_signals[name].yAxis.Add(current_values[i]);
                }
            }
            List<string> keys = dic_signals.Keys.ToList();
            for (int i = 0; i < current_values.Length; i++)
            {
                if (fGraphics.chartDic_signals_hist.Keys.Contains(keys[i])) //tiene datos de antes
                {
                    fGraphics.chartDic_signals_hist[keys[i]].xAxis.Add(elapsedMilliseconds);
                    fGraphics.chartDic_signals_hist[keys[i]].yAxis.Add(current_values[i]);
                    fGraphics.chartDic_signals[keys[i]].xAxis.Add(elapsedMilliseconds);
                    fGraphics.chartDic_signals[keys[i]].yAxis.Add(current_values[i]);
                    int count = fGraphics.chartDic_signals[keys[i]].xAxis.Count - maxValuesToShow;
                    if (count > 0) //tengo más de los que puedo mostrar
                    {
                        fGraphics.chartDic_signals[keys[i]].xAxis.RemoveRange(0, count);
                        fGraphics.chartDic_signals[keys[i]].yAxis.RemoveRange(0, count);
                    }
                }
                else
                {
                    fGraphics.chartDic_signals_hist.Add(keys[i], new MyContainer());
                    fGraphics.chartDic_signals_hist[keys[i]].xAxis = new List<double>();
                    fGraphics.chartDic_signals_hist[keys[i]].xAxis.Add(elapsedMilliseconds);
                    fGraphics.chartDic_signals_hist[keys[i]].yAxis = new List<double>();
                    fGraphics.chartDic_signals_hist[keys[i]].yAxis.Add(current_values[i]);

                    fGraphics.chartDic_signals.Add(keys[i], new MyContainer());
                    fGraphics.chartDic_signals[keys[i]].xAxis = new List<double>();
                    fGraphics.chartDic_signals[keys[i]].xAxis.Add(elapsedMilliseconds);
                    fGraphics.chartDic_signals[keys[i]].yAxis = new List<double>();
                    fGraphics.chartDic_signals[keys[i]].yAxis.Add(current_values[i]);
                }
            }

        }

        private void FillSingleSignalData(string name, double current_value, long elapsedMilliseconds)
        {
            if (dic_signals[name].xAxis != null)
            {
                dic_signals[name].xAxis.Add(elapsedMilliseconds);
                dic_signals[name].yAxis.Add(current_value);
            }
            else
            {
                dic_signals[name].xAxis = new List<double>();
                dic_signals[name].xAxis.Add(elapsedMilliseconds);
                dic_signals[name].yAxis = new List<double>();
                dic_signals[name].yAxis.Add(current_value);
            }
            if (fGraphics.chartDic_signals_hist.Keys.Contains(name)) //tiene datos de antes
            {
                fGraphics.chartDic_signals_hist[name].xAxis.Add(elapsedMilliseconds);
                fGraphics.chartDic_signals_hist[name].yAxis.Add(current_value);
                fGraphics.chartDic_signals[name].xAxis.Add(elapsedMilliseconds);
                fGraphics.chartDic_signals[name].yAxis.Add(current_value);
                int count = fGraphics.chartDic_signals[name].xAxis.Count - maxValuesToShow;
                if (count > 0) //tengo más de los que puedo mostrar
                {
                    fGraphics.chartDic_signals[name].xAxis.RemoveRange(0, count);
                    fGraphics.chartDic_signals[name].yAxis.RemoveRange(0, count);
                }
            }
            else
            {
                fGraphics.chartDic_signals_hist.Add(name, new MyContainer());
                fGraphics.chartDic_signals_hist[name].xAxis = new List<double>();
                fGraphics.chartDic_signals_hist[name].xAxis.Add(elapsedMilliseconds);
                fGraphics.chartDic_signals_hist[name].yAxis = new List<double>();
                fGraphics.chartDic_signals_hist[name].yAxis.Add(current_value);

                fGraphics.chartDic_signals.Add(name, new MyContainer());
                fGraphics.chartDic_signals[name].xAxis = new List<double>();
                fGraphics.chartDic_signals[name].xAxis.Add(elapsedMilliseconds);
                fGraphics.chartDic_signals[name].yAxis = new List<double>();
                fGraphics.chartDic_signals[name].yAxis.Add(current_value);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRange.Checked)
            {
                rbValue.Checked = false;
                rbMultiple.Checked = false;
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                save_path = folderBrowserDialog1.SelectedPath;
                save_name = tbxFileName.Text;
                btnSelectedVals.Enabled = true;
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

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Purpose, Developers, Lincences");
            /*
             The Photonic Sensing software is designed to run on Windows operating system as a desktop application. Its purpose is to provide a user-friendly interface for monitoring signals with the Ocean Electronics’ Ocean SR spectrometer. The software is developed with Microsoft Visual Studio 2026 Community Edition and, therefore, is intended for academic research purposes. 
             */
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] pdfData = Properties.Resources.user_manual;
            string tempPath = Path.Combine(Path.GetTempPath(), "software overview.pdf");
            File.WriteAllBytes(tempPath, pdfData);
            System.Diagnostics.Process.Start(new ProcessStartInfo(tempPath) { UseShellExecute = true });
        }

        private void rbValue_CheckedChanged(object sender, EventArgs e)
        {
            if (rbValue.Checked)
            {
                rbMultiple.Checked = false;
                rbRange.Checked = false;
            }
        }

        private void rbMultiple_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMultiple.Checked)
            {  
                rbRange.Checked = false;
                rbValue.Checked = false;
            }
        }

        private void btnSelectedVals_Click(object sender, EventArgs e)
        {
            btnSelectedVals.Enabled = false;
            if (rbMultiple.Checked)
            {
                fWave.ShowDialog();
                if (fWave.DialogResult == DialogResult.OK)
                {
                    MultipleWaves = fWave.MultipleWaves;
                    for (int i = 0; i < MultipleWaves.Count; i++)
                        dic_signals.Add(MultipleWaves[i].ToString(), new MyContainer());
                }
                else
                {
                    btnSelectedVals.Enabled = true;
                    throw new Exception("Please fill the data correctly");
                }
            }   
            if (rbRange.Checked)
            {
                int min = (int)nudMinWave.Value;
                int max = (int)nudMaxWave.Value;
                if (min > max) throw new Exception("Max wavelenght must be greater than Min wavelenght");
                string name = nudMinWave.Value.ToString() + "-" + nudMaxWave.Value.ToString();
                dic_signals.Add(name, new MyContainer());
            }
            if (rbValue.Checked)
            {
                dic_signals.Add(nudSingleWave.Value.ToString(), new MyContainer());
                MultipleWaves.Add((int)nudSingleWave.Value);
            }
            button2.Enabled = true;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            dic_signals = new Dictionary<string, MyContainer>();
            MultipleWaves.Clear();

            if (rbMultiple.Checked)
            {
                FMultipleWave fWave = new FMultipleWave();
                fWave.Visible = false;
                fWave.ShowDialog();
                if (fWave.DialogResult == DialogResult.OK)
                {
                    MultipleWaves = fWave.MultipleWaves;
                    for (int i = 0; i < MultipleWaves.Count; i++)
                        dic_signals.Add(MultipleWaves[i].ToString(), new MyContainer());
                }
                else
                {
                    btnSelectedVals.Enabled = true;
                    throw new Exception("Please fill the data correctly");
                }
            }
            if (rbRange.Checked)
            {
                int min = (int)nudMinWave.Value;
                int max = (int)nudMaxWave.Value;
                if (min > max) throw new Exception("Max wavelenght must be greater than Min wavelenght");
                string name = nudMinWave.Value.ToString() + "-" + nudMaxWave.Value.ToString();
                dic_signals.Add(name, new MyContainer());
            }
            if (rbValue.Checked)
            {
                dic_signals.Add(nudSingleWave.Value.ToString(), new MyContainer());
                MultipleWaves.Add((int)nudSingleWave.Value);
            }

            tick = 0;
            spectrum_data = new MyContainer();
            fGraphics.xPhase.Clear();
            fGraphics.yPhase.Clear();
            fGraphics.xPhase_hist.Clear();
            fGraphics.yPhase_hist.Clear();
            button2.Enabled = true;
        }
    }
}
