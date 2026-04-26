using MathNet.Numerics.LinearAlgebra.Factorization;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace oceanSR
{
    public partial class FGraphics : Form
    {
        public FGraphics()
        {
            InitializeComponent();
            InitializeSpectraGraphic();
            InitializeSignalGraphic();
            InitializePhaseGraphic();

            checkedListBox1.SetItemChecked(2, true);
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeSpectraGraphic()
        {
            chart1.Titles.Add("Raw Spectra");
            var chartArea = chart1.ChartAreas[0];

            chartArea.AxisX.Title = "Wavelenght (nm)";
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisX.TitleAlignment = StringAlignment.Center;
            
            chartArea.AxisY.Title = "Counts (a.u.)";
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY.TextOrientation = TextOrientation.Rotated270;

            // Enable zooming and selection on the X-axis
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.AutoScroll = true;

            // Optional: Enable zooming on the Y-axis as well
            chartArea.AxisY.ScaleView.Zoomable = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;

            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[0].Color = Color.Black;
        }

        private void InitializeSignalGraphic()
        {
            chart2.Titles.Add("Pixels (a.u)");
            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].IsVisibleInLegend = false;
            chart2.Series[0].Color = Color.Red;

            var chartArea = chart2.ChartAreas[0];

            chartArea.AxisX.Title = "Time (ms)";
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisX.TitleAlignment = StringAlignment.Center;

            chartArea.AxisY.Title = "Amplitude";
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY.TextOrientation = TextOrientation.Rotated270;
        }

        private void InitializePhaseGraphic()
        {
            chart3.Titles.Add("Monitoring phase");
            chart3.Series[0].ChartType = SeriesChartType.Line;
            chart3.Series[0].IsVisibleInLegend = false;
            chart3.Series[0].Color = Color.Blue;

            var chartArea = chart3.ChartAreas[0];

            chartArea.AxisX.Title = "Samples";
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisX.TitleAlignment = StringAlignment.Center; 

            chartArea.AxisY.Title = "Phase (radians)";
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY.TextOrientation = TextOrientation.Rotated270;
        }

        public bool stop_measure = true;

        public List<double> xFourier = new List<double>();
        public List<double> yFourier = new List<double>();
        public List<double> xSignal = new List<double>();
        public List<double> ySignal = new List<double>();
        public List<double> xPhase = new List<double>();
        public List<double> yPhase = new List<double>();
        public List<double> xSpectra = new List<double>();
        public List<double> ySpectra = new List<double>();
        public List<double> xSpectraSelected = new List<double>();
        public List<double> ySpectraSelected = new List<double>();


        public List<double> xFourier_hist = new List<double>();
        public List<double> yFourier_hist = new List<double>();
        public List<double> xSignal_hist = new List<double>();
        public List<double> ySignal_hist = new List<double>();
        public List<double> xPhase_hist = new List<double>();
        public List<double> yPhase_hist = new List<double>();
        public string path;
        public string customName;

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0)
            {
                try
                {
                    chart1.SaveImage(path + @"/Spectrum " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".png", ChartImageFormat.Png);
                    chart2.SaveImage(path + @"/Signal " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".png", ChartImageFormat.Png);
                    chart3.SaveImage(path + @"/Phase " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".png", ChartImageFormat.Png);
                    MessageBox.Show("Images saved succesfully!");
                }
                catch { }
            }
            else if (checkedListBox1.SelectedIndex == 1)
            {
                try
                {
                    SaveCSV(path + @"/Spectrum " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv", "Wavelenght, Count (a.u.)", xSpectra, ySpectra);
                    SaveCSV(path + @"/Signal " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv", "Time (ms), Sensor Signal", xSignal_hist, ySignal_hist);
                    SaveCSV(path + @"/Phase " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv", "Time (ms), Phase", xPhase_hist, yPhase_hist);
                    MessageBox.Show("Data saved succesfully");
                }
                catch { }
            }
            else
            {
                try
                {
                    chart1.SaveImage(path + @"/Spectrum " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".png", ChartImageFormat.Png);
                    chart2.SaveImage(path + @"/Signal " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".png", ChartImageFormat.Png);
                    chart3.SaveImage(path + @"/Phase " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".png", ChartImageFormat.Png);
                    SaveCSV(path + @"/Spectrum " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv", "Wavelenth, Count (a.u.)", xSpectra, ySpectra);
                    SaveCSV(path + @"/Signal " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv", "Time (ms), Sensor Signal", xSignal_hist, ySignal_hist);
                    SaveCSV(path + @"/Phase " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv", "Time (ms), Phase", xPhase_hist, yPhase_hist);

                    MessageBox.Show("Images and data saved succesfully!");
                }
                catch { }
            }


        }
        private void SaveCSV(string fileName, string heading, List<double> xAxis, List<double> yAxis) 
        {
            try
            {
                int rowCount = Math.Min(xAxis.Count, yAxis.Count);

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    // Write Column Headers
                    writer.WriteLine(heading);

                    for (int i = 0; i < rowCount; i++)
                    {
                        // Write each pair as a row: Value1,Value2
                        writer.WriteLine($"{xAxis[i]},{yAxis[i]}");
                    }
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop_measure = true; 
            button1.Enabled = true;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Reset axis zoom (0 resets to original view)
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            // Reset axis zoom (0 resets to original view)
            chart2.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart2.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            // Reset axis zoom (0 resets to original view)
            chart3.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart3.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        public void EnableSave()
        {
            button1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                xSpectraSelected = new List<double>(xSpectra);
                ySpectraSelected = new List<double>(ySpectra);
                checkBox1.Enabled = false;
                chart1.SaveImage(path + @"/SelectedSpectrum " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", ChartImageFormat.Png);
                SaveCSV(path + @"/SelectedSpectrum " + customName + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv", "Wavelenth, Count (a.u.)", xSpectraSelected, ySpectraSelected);
                DialogResult result = MessageBox.Show("Spectra selected and saved. Do you want the option to later select another spectra?", "Selection", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    checkBox1.Checked = false;
                    checkBox1.Enabled = true;
                }
            }
        }
    }

    
}
