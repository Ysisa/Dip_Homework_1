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
    public partial class Show : Form
    {
        public Show()
        {
            System.Console.WriteLine("now begin");
            InitializeComponent();
        }

        public void ShowPicture(ref Bitmap bitmap)
        {
            showPicture.Image = Image.FromHbitmap(bitmap.GetHbitmap());
        }

    }
}
