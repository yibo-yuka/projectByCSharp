using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace CirclePackingCalculation
{
    public class DrawImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // 設定回應內容類型為圖像
            context.Response.ContentType = "image/png";

            // 建立一個 Bitmap 物件 (圖像大小：200x200)
            using (Bitmap bmp = new Bitmap(200, 200))
            {
                // 建立 Graphics 物件
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // 填充背景色
                    g.Clear(Color.White);

                    // 繪製矩形
                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawRectangle(pen, 10, 10, 100, 50);

                    // 繪製圓形
                    g.DrawEllipse(pen, 120, 10, 60, 60);

                    // 填充藍色圓形
                    Brush brush = new SolidBrush(Color.Blue);
                    g.FillEllipse(brush, 120, 80, 60, 60);

                    // 畫一條線
                    g.DrawLine(pen, 10, 150, 180, 150);

                    // 釋放資源
                    pen.Dispose();
                    brush.Dispose();
                }

                // 將圖像輸出到回應流
                bmp.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
