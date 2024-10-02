using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CirclePackingWPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            // 嘗試解析輸入的數字
            if (float.TryParse(diameter.Text, out float Diameter) && float.TryParse(slice.Text, out float Slice_dd) && float.TryParse(inward.Text,out float Inward))
            {
                Bitmap bitmap = DrawCircles(Diameter,Slice_dd,Inward);
                drawingImage.Source = ConvertBitmapToImageSource(bitmap);
            }
            else
            {
                MessageBox.Show("請輸入一個有效的數字");
            }
        }

        private Bitmap DrawCircles(float _Dia,float _Slice_DD,float _Inward)
        {
            // 定義畫布大小
            int width = 400;
            int height = 400;
            float dia = _Dia;
            float slice_dd = _Slice_DD;
            float inward = _Inward;

            float CircleArea = 70 - inward * 2;
            int n = 1;
            float Occupy = 0;
            while (true)
            {
                Occupy = dia * n + slice_dd * (n - 1);
                if (Occupy > CircleArea)
                {
                    break;
                }
                n++;
            }
            int counts = n - 1;
            float new_Occupy = dia * counts + slice_dd * (counts - 1);
            float new_inward = inward + (CircleArea - new_Occupy) / 2;


            // 創建 Bitmap 對象
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(System.Drawing.Color.White); // 清除背景為白色

                int w = 80;
                int h = 80;

                float x_ratio = width / w;
                float y_ratio = height / h;
                //float x_ratio = 1;
                //float y_ratio = 1;

                
                // 定義畫筆顏色和寬度
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Blue, 2))
                {
                    // 畫大邊框
                    g.DrawLine(pen, new System.Drawing.Point((int)(0 * x_ratio), (int)(0 * y_ratio)), new System.Drawing.Point((int)(0 * x_ratio), (int)(80 * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)(0 * x_ratio), (int)(0 * y_ratio)), new System.Drawing.Point((int)(80 * x_ratio), (int)(0 * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)(0 * x_ratio), (int)(80 * y_ratio)), new System.Drawing.Point((int)(80 * x_ratio), (int)(80 * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)(80 * x_ratio), (int)(0 * y_ratio)), new System.Drawing.Point((int)(80 * x_ratio), (int)(80 * y_ratio)));
                    
                    // 畫邊框
                    g.DrawLine(pen, new System.Drawing.Point((int)(5 * x_ratio), (int)(5 * y_ratio)), new System.Drawing.Point((int)(5 * x_ratio), (int)(75 * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)(5 * x_ratio), (int)(5 * y_ratio)), new System.Drawing.Point((int)(75 * x_ratio), (int)(5 * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)(5 * x_ratio), (int)(75 * y_ratio)), new System.Drawing.Point((int)(75 * x_ratio), (int)(75 * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)(75 * x_ratio), (int)(5 * y_ratio)), new System.Drawing.Point((int)(75 * x_ratio), (int)(75 * y_ratio)));

                }
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Green, 1))
                {
                    // 畫邊框
                    g.DrawLine(pen, new System.Drawing.Point((int)((5+new_inward) * x_ratio), (int)((5+new_inward) * y_ratio)), new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((75-new_inward) * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)), new System.Drawing.Point((int)((75-new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((75-new_inward) * y_ratio)), new System.Drawing.Point((int)((75-new_inward) * x_ratio), (int)((75-new_inward) * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)((75-new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)), new System.Drawing.Point((int)((75-new_inward) * x_ratio), (int)((75-new_inward) * y_ratio)));

                }
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 1))
                {
                    for (int i = 0; i < counts; i++)
                    {
                        for (int j = 0; j < counts; j++)
                        {
                            float Corner_x = i * (dia + slice_dd) + new_inward+5;
                            float Corner_y = j * (dia + slice_dd) + new_inward+5;
                            g.DrawEllipse(pen, Corner_x * x_ratio, Corner_y * y_ratio, dia * x_ratio, dia * y_ratio);
                        }
                    }
                }
            }
            return bitmap;
        }
        private BitmapSource ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0; // 重置流位置

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // 立即加載
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // 冻结对象以便在多线程环境中使用

                return bitmapImage;
            }
        }

    }

}
