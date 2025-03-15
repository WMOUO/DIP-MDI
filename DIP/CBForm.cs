using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public partial class CBForm: Form
    {
        public Bitmap bitmap;
        public ToolStripStatusLabel tssl_1,tssl_2;
        public DIPSample dip;
        public CBForm()
        {
            InitializeComponent();
        }
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void color_change(int* f0, int w, int h, int* g0, int a, int b);
        private void CBForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = bitmap;
        }

        private void change_color()
        {
            int a = hScrollBar1.Value;
            int b = hScrollBar2.Value;
            int[] f = dip.bmp2array(bitmap);
            int[] g = new int[bitmap.Width * bitmap.Height];
            unsafe
            {
                fixed (int* f0 = f) fixed (int* g0 = g)
                {
                    color_change(f0, bitmap.Width, bitmap.Height, g0, a, b);
                }
            }
            pictureBox1.Image = dip.array2bmp(g);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            float x = hScrollBar1.Value;
            textBox2.Text = (x / 1000).ToString();
            change_color();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            int y = hScrollBar2.Value;
            textBox1.Text = y.ToString();
            change_color();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int x;
            if (textBox1.Text == "")
                x = 0;
            else
                x = Int32.Parse(textBox1.Text);

            hScrollBar2.Value = x;
            change_color();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float x;
            if (textBox2.Text == "")
                x = 0;
            else
                x = float.Parse(textBox2.Text);

            hScrollBar1.Value = Convert.ToInt32(x * 1000);
            change_color();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    break;
                }
            }

            MSForm childForm = new MSForm();
            childForm.MdiParent = dip;
            childForm.pf1 = tssl_1;
            childForm.pf2 = tssl_2;
            childForm.pBitmap = new Bitmap(pictureBox1.Image);
            childForm.Show();
            this.Close();
        }
    }
}
