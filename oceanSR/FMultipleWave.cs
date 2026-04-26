using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oceanSR
{
    public partial class FMultipleWave : Form
    {
        public FMultipleWave()
        {
            InitializeComponent();
        }

        public List<int> MultipleWaves;

        private void button1_Click(object sender, EventArgs e)
        {
            MultipleWaves = new List<int>();
            int count = dataGridView1.RowCount;
            for (int i = 0; i < count-1; i++)
            {
                try { MultipleWaves.Add(Convert.ToInt16(dataGridView1[0, i].Value)); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
