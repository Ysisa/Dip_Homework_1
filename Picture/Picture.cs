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
    public partial class Picture : Form
    {

        private string curFileName;
        private System.Drawing.Bitmap curBitmap;

        public Picture()
        {
            InitializeComponent();
        }

        private void Picture_Load(object sender, EventArgs e)
        {

        }

        //打开图像
        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "位图(PNG, JPEG)|*.png;*.jpg";
            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;

            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                curFileName = opnDlg.FileName;
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }

            ReloadPicture();
        }

        //保存图像
        private void save_Click(object sender, EventArgs e)
        {
            if (curBitmap == null)
            {
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter = "PNG|*.png|JPG|*.jpg";
            saveDlg.ShowHelp = true;

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                string strFileExtn = fileName.Remove(0, fileName.Length - 3);

                switch (strFileExtn)
                {
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    default:
                        break;
                }
            }
        }

        //关闭图像
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //像素法黑白化
        private void pixel_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Color curColor;
                int ret;

                for (int i = 0; i < curBitmap.Width; i++)
                {
                    for (int j = 0; j < curBitmap.Height; j++)
                    {
                        curColor = curBitmap.GetPixel(i, j);
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        curBitmap.SetPixel(i, j, Color.FromArgb(curColor.A, ret, ret, ret));
                    }
                }

                ReloadPicture();
            }
        }

        //内存法黑白化
        private void memory_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                PixelProcess processor = new PixelProcess();
                processor.SetBitmapGray(ref curBitmap, 0.299, 0.587, 0.114);

                ReloadPicture();
            }
        }

        //缩放
        private void zoom_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Zoom zoomForm = new Zoom();
                zoomForm.setPixel(curBitmap.Width, curBitmap.Height);

                if (zoomForm.ShowDialog() == DialogResult.OK)
                {

                    //得到处理后图像的横向&纵向像素, 并建立相应的数组
                    int widthAfter = Convert.ToInt32(zoomForm.GetXZoom);
                    int heightAfter = Convert.ToInt32(zoomForm.GetYZoom);

                    if (zoomForm.GetNearOrBil == true)
                    {
                        ScaleProcess processor = new ScaleProcess();
                        processor.NearestInter(ref curBitmap, widthAfter, heightAfter);
                    }
                    else
                    {
                        ScaleProcess processor = new ScaleProcess();
                        processor.BilinearInter(ref curBitmap, widthAfter, heightAfter);
                    } 
                }

                ReloadPicture();
            }
        }

        //颜色降级
        private void To128Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                PixelProcess processor = new PixelProcess();
                processor.SetBitmapColorDegree(ref curBitmap, 7);

                ReloadPicture();
            }
        }

        private void To32Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                PixelProcess processor = new PixelProcess();
                processor.SetBitmapColorDegree(ref curBitmap, 5);

                ReloadPicture();
            }
        }

        private void To8Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                PixelProcess processor = new PixelProcess();
                processor.SetBitmapColorDegree(ref curBitmap, 3);

                ReloadPicture();
            }
        }

        private void To4Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                PixelProcess processor = new PixelProcess();
                processor.SetBitmapColorDegree(ref curBitmap, 2);

                ReloadPicture();
            }
        }

        private void To2Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                PixelProcess processor = new PixelProcess();
                processor.SetBitmapColorDegree(ref curBitmap, 1);

                ReloadPicture();
            }
        }

        //重新加载图像
        private void ReloadPicture()
        {
            if (curBitmap != null)
            {
                pictureBox1.Width = curBitmap.Width;
                pictureBox1.Height = curBitmap.Height;
                pictureBox1.Image = Image.FromHbitmap(curBitmap.GetHbitmap());
            }
        }
    }
}
