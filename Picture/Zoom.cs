using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture
{
    public partial class Zoom : Form
    {
        private int xPixel, yPixel;

        public Zoom()
        {
            InitializeComponent();
        }

        public void setPixel(int x, int y)
        {
            xPixel = x;
            yPixel = y;
        }

        private void Zoom_Load(object sender, EventArgs e)
        {
            xZoom.Text = xPixel.ToString();
            yZoom.Text = yPixel.ToString();
        }

        private void Text2_Click(object sender, EventArgs e)
        {

        }

        private void yZoom_TextChanged(object sender, EventArgs e)
        {

        }

        private void startZoom_Click(object sender, EventArgs e)
        {
            if (xZoom.Text == "0" || yZoom.Text == "0")
            {
                MessageBox.Show("缩放量不能为0", "警告",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool GetNearOrBil
        {
            get
            {
                return nearestNeigh.Checked;
            }
        }

        public string GetXZoom
        {
            get
            {
                return xZoom.Text;
            }
        }

        public string GetYZoom
        {
            get
            {
                return yZoom.Text;
            }
        }

        private void Text_Click(object sender, EventArgs e)
        {

        }


    }
}
