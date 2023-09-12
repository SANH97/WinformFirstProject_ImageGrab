using OpenCvSharp;
using OpenCvSharp.Extensions;
using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace WinformFirstProject_ImageGrab
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        private Bitmap bitmapImage; 
        private void button3_Click(object sender, EventArgs e)
        {
            //openfiledialog 활용
            string image_file = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName;
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;

            }
            pictureBox2.Image = Bitmap.FromFile(image_file);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            bitmapImage = new Bitmap(image_file);
        }   

        private void button4_Click(object sender, EventArgs e)
        {
            
            Mat matImage = BitmapConverter.ToMat(bitmapImage);
            // 이미지를 그레이스케일로 변환
            Mat grayImage = new Mat();
            Cv2.CvtColor(matImage, grayImage, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(grayImage, grayImage, 128, 255, ThresholdTypes.Binary);

            // 외곽선 검출
            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(grayImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // 외곽선을 그리는 데 사용할 빈 이미지 생성
            Mat resultImage = new Mat(matImage.Size(), MatType.CV_8UC3, Scalar.All(0));

            // 검출된 외곽선을 빨간색으로 그리기
            Cv2.DrawContours(resultImage, contours, -1, Scalar.Red, 3);

            Bitmap binaryBitmap = BitmapConverter.ToBitmap(resultImage);
            pictureBox3.Image = binaryBitmap;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;


        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
