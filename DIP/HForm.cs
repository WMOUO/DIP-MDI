using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public partial class HForm: Form
    {
        public double[] data;
        public string title;
        public Bitmap image;
        public HForm()
        {
            InitializeComponent();
        }

        private void HForm_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add(title);
            chart1.Series.Add("Gray");
            chart1.ChartAreas.Add("Gray");
            pictureBox1.Image = image;
            for (int i = 0; i < 256; i++)
            {
                chart1.Series[0].Points.AddXY(i + 1, data[i]);
            }
        }
    }
}
