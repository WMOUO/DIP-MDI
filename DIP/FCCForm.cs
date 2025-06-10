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
    public partial class FCCForm: Form
    {
        internal Bitmap pBitmap, bitmap;
        internal ToolStripStatusLabel pf1;
        public DIPSample dIP;

        public FCCForm()
        {
            InitializeComponent();
        }

        private void FCCForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pBitmap;
            pictureBox1.Width = pBitmap.Width;
            pictureBox1.Height = pBitmap.Height;
            hScrollBar1.Maximum = int.Parse(this.Text);
            hScrollBar1.Width = pBitmap.Width - textBox1.Width;
            hScrollBar1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y+ pBitmap.Height);
            textBox1.Location = new Point(hScrollBar1.Location.X + hScrollBar1.Width, hScrollBar1.Location.Y);
            this.Width = pBitmap.Width + 50;
            this.Height = pBitmap.Height + hScrollBar1.Height+50;
        }

        private void pShow() 
        {
            int[] f, g,k;
            int w, h,label=int.Parse(textBox1.Text);
            f=dIP.bmp2array(pBitmap);
            g = dIP.bmp2array(bitmap);
            w = pBitmap.Width;
            h = pBitmap.Height;
            k= new int[w * h];
            if (label > 0)
            {
                for (int i = 0; i < w * h; i++)
                {
                    if (g[i] == label) k[i] = f[i];
                    else k[i] = 0;
                }
            }else {
                for (int i = 0; i < w * h; i++)
                    k[i] = f[i];
            }
            pictureBox1.Image = dIP.array2bmp(k);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n <= hScrollBar1.Maximum && n > hScrollBar1.Minimum)
                    hScrollBar1.Value = n;
                else
                    hScrollBar1.Value = hScrollBar1.Minimum;
                pShow();
            }
            catch (FormatException)
            {
                MessageBox.Show("請輸入有效的數字", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Color pixel = pBitmap.GetPixel(e.X, e.Y);
            int bright = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
            pf1.Text = "座標  (X, Y)=(" + e.X + "," + e.Y + ")    (R, G, B)=(" + pixel.R.ToString() + "," + pixel.G.ToString() + "," + pixel.B.ToString() + ")    亮度: " + bright.ToString();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            textBox1.Text = hScrollBar1.Value.ToString();
            pShow();
        }
    }
}
