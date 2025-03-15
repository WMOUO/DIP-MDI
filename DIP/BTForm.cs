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
    public partial class BTForm: Form
    {
        public Bitmap bitmap;
        public ToolStripStatusLabel tssl_1,tssl_2;
        public DIPSample dip;
        int n;

        public BTForm()
        {
            InitializeComponent();
        }

        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void byte_cut(int* f0, int w, int h, int* g0, int n);

        private void byte_cut(int n)
        {
            int u = Convert.ToInt32(Math.Pow(2, n));
            int[] f = dip.bmp2array(bitmap);
            int[] g = new int[bitmap.Width * bitmap.Height];
            unsafe
            {
                fixed (int* f0 = f) fixed (int* g0 = g)
                {
                    byte_cut(f0, bitmap.Width, bitmap.Height, g0, u);
                }
            }
            pictureBox1.Image = dip.array2bmp(g);
        }
        private void BTForm_Load(object sender, EventArgs e)
        {
            n = 0;
            label3.Text = n.ToString();
            byte_cut(n);
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            n = hScrollBar2.Value / 100;
            label3.Text = n.ToString();
            byte_cut(n);
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
