using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;
using Image = System.Web.UI.WebControls.Image;

namespace CirclePackingASPdotNET
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnDraw_Click(object sender, EventArgs e)
        {
            //dll檔路徑
            string appPath = Request.PhysicalApplicationPath;
            string dllPath = AppDomain.CurrentDomain.BaseDirectory;//跟上面的appPath是一樣的
            //儲存用資料夾
            string saveDir = @"pics";
            string saveDirPath = appPath + "/" + saveDir;
            if (!Directory.Exists(saveDirPath))
            {
                Directory.CreateDirectory(saveDirPath);
            }
            //儲存用路徑
            string savePath = Path.Combine(appPath + saveDir, "pic.jpg");
            // 嘗試解析輸入的數字
            if (float.TryParse(Dia_text.Text, out float Diameter_val) && float.TryParse(slice_text.Text, out float Slice_dd) && float.TryParse(inward_text.Text, out float Inward_val))
            {
                Bitmap bitmap = DrawCircles(Diameter_val, Slice_dd, Inward_val);
                // 保存圖像到檔案
                bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //drawingImage.Source = ConvertBitmapToImageSource(bitmap);
            }
            else
            {
                //MessageBox.Show("請輸入一個有效的數字");
            }
            // 如果表格沒有任何列或是最後一列的欄數等於所設定的最大顯示數量時，新增一列
            if (imgTable.Rows.Count == 0)
            {
                imgTable.Rows.Add(new TableRow());
            }
            System.Web.UI.WebControls.Image img = new Image(); //宣告圖片物件
            img.Width = 200;//設定圖片寬度，讓高度自動縮放
                            //取得圖片簡短路徑
            string url = savePath.Substring(savePath.IndexOf(@"pics\"));
            //將反斜線取代成斜線，避免錯誤發生，並設定成圖片物件的來源
            img.ImageUrl = url.Replace(@"\", @"/");
            TableCell cell = new TableCell();//新增儲存格
                                             //加入目前列
            imgTable.Rows[imgTable.Rows.Count - 1].Cells.Add(cell);
            cell.Controls.Add(img);//圖片控制項加入儲存格
        }

        private Bitmap DrawCircles(float _Dia, float _Slice_DD, float _Inward)
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
                    g.DrawLine(pen, new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)), new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((75 - new_inward) * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)), new System.Drawing.Point((int)((75 - new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)((5 + new_inward) * x_ratio), (int)((75 - new_inward) * y_ratio)), new System.Drawing.Point((int)((75 - new_inward) * x_ratio), (int)((75 - new_inward) * y_ratio)));
                    g.DrawLine(pen, new System.Drawing.Point((int)((75 - new_inward) * x_ratio), (int)((5 + new_inward) * y_ratio)), new System.Drawing.Point((int)((75 - new_inward) * x_ratio), (int)((75 - new_inward) * y_ratio)));

                }
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 1))
                {
                    for (int i = 0; i < counts; i++)
                    {
                        for (int j = 0; j < counts; j++)
                        {
                            float Corner_x = i * (dia + slice_dd) + new_inward + 5;
                            float Corner_y = j * (dia + slice_dd) + new_inward + 5;
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