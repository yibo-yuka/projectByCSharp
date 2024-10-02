using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePicture
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入數字：");
            string num = Console.ReadLine();
            Console.WriteLine($"已收到您輸入的數字，{num}");

            // 定義圖像的寬度和高度
            int width = 400;
            int height = 400;
            int n = Convert.ToInt32(num);
            // 創建一個新的位圖圖像
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                // 創建一個 Graphics 對象來繪製圖形
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // 設定背景顏色
                    g.Clear(Color.White);
                    int w = 80;
                    int h = 80;

                    float x_ratio = width / w;
                    float y_ratio = height / h;
                    //float x_ratio = 1;
                    //float y_ratio = 1;

                    float dia = 10;
                    // 定義畫筆顏色和寬度
                    using (Pen pen = new Pen(Color.Blue, 3))
                    {
                        // 畫邊框
                        g.DrawLine(pen, new Point((int)(5 * x_ratio), (int)(5 * y_ratio)), new Point((int)(5 * x_ratio), (int)(75 * y_ratio)));
                        g.DrawLine(pen, new Point((int)(5 * x_ratio), (int)(5 * y_ratio)), new Point((int)(75 * x_ratio), (int)(5 * y_ratio)));
                        g.DrawLine(pen, new Point((int)(5 * x_ratio), (int)(75 * y_ratio)), new Point((int)(75 * x_ratio), (int)(75 * y_ratio)));
                        g.DrawLine(pen, new Point((int)(75 * x_ratio), (int)(5 * y_ratio)), new Point((int)(75 * x_ratio), (int)(75 * y_ratio)));
                        
                    }
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            for (int j = 1; j <= 7; j++)
                            {
                                g.DrawEllipse(pen, (10 * i - n / 2) * x_ratio, (10 * j - n / 2) * y_ratio, n * x_ratio, n * y_ratio);
                            }
                        }
                    }
                }

                // 保存圖像到檔案
                bitmap.Save("line.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            Console.WriteLine("圖像已保存為 line.png，請查看該文件。");

            Console.WriteLine("點擊任意鍵關閉...");
            Console.ReadKey();
        }
    }
}
