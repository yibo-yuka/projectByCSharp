using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CirclePackingCalculation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadImages();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //隱藏所有訊息標籤控制項
            lblChoose.Visible = false;
            lblFailed.Visible = false;
            lblSuccess.Visible = false;

            //如果使用者有選擇檔案的話
            if (FileUpload1.HasFile)
            {
                //取得使用者所上傳檔案的檔名
                string fileName = FileUpload1.FileName;
                //取得使用者所上傳檔案的大小
                int fileSize = FileUpload1.PostedFile.ContentLength;
                //取得使用者所上傳檔案的副檔名
                string fileExtension = Path.GetExtension(fileName).ToLower();
                //設定允許的副檔名類型
                string[] allowedExtension = new string[] { ".jpg", ".png", ".jpeg", ".gif", ".bmp" };
                //設定最大上傳為 8MB(8,388,608 bytes)
                int allowFileSize = 8388608;
                bool fileOK = false;

                //判斷副檔名是否為圖片類型
                for (int i = 0; i < allowedExtension.Length; i++)
                {
                    if (fileExtension.Equals(allowedExtension[i]))
                    {
                        fileOK = true;
                    }
                }

                //如果副檔名是圖片類型就上傳，否則顯示錯誤訊息
                if (fileOK)
                {
                    //判斷檔案大小是否符合
                    if (fileSize <= allowFileSize)
                    {
                        string saveDir = @"Uploads"; //設定儲存資料夾
                        //取得實體網站路徑
                        string appPath = Request.PhysicalApplicationPath;
                        //設定完整儲存路徑
                        string savePath = Path.Combine(appPath + saveDir, fileName);

                        //儲存檔案
                        try
                        {
                            FileUpload1.SaveAs(savePath);
                            //上傳成功
                            lblSuccess.Visible = true;
                            //重新載入圖片
                            LoadImages();
                        }
                        catch (Exception)
                        {
                            //上傳失敗
                            lblFailed.Visible = true;
                        }
                    }
                    else
                    {
                        //顯示錯誤訊息
                        lblChoose.Text = "所選擇的檔案大小超出" + (allowFileSize / 1024 / 1024) + "MB";
                        lblFailed.Visible = true;
                    }
                }
                else
                {
                    //顯示錯誤訊息
                    lblChoose.Text = "所選擇的檔案並非為圖片類型";
                    lblChoose.Visible = true;
                }
            }
            else
            {
                //未選擇檔案
                lblChoose.Text = "未選擇圖片";
                lblChoose.Visible= true;
            }
        }
        protected void LoadImages()
        {
            //如果目前表格中有圖片，全部刪掉
            while (imgTable.Rows.Count > 0)
            {
                imgTable.Rows.RemoveAt(0);
            }

            //載入Uploads下所有圖片

            //設定Uploads資料夾路徑
            string uploadDir = @"Uploads";
            //取得實體網站路徑
            string appPath = Request.PhysicalApplicationPath;
            //設定完整儲存路徑
            string dirPath = Path.Combine(appPath , uploadDir);
            //取得該資料夾下所有圖片
            IEnumerable<string> images = Directory.GetFiles(dirPath);
            int maxCol = 5; //設定每列中最多顯示幾張圖片

            //開始把圖片顯示在頁面上
            foreach (string fileName in images)
            {
                //如果表格沒有任何列或是最後一列的欄數等於所設定的最大顯示數量時，新增一列
                if(imgTable.Rows.Count == 0 || imgTable.Rows[imgTable.Rows.Count-1].Cells.Count == maxCol)
                {
                    imgTable.Rows.Add(new TableRow());
                }

                Image img = new Image(); //宣告圖片物件
                img.Width = 200;//設定圖片寬度，讓高度自動縮放
                //取得圖片簡短路徑
                string url = fileName.Substring(fileName.IndexOf(@"Uploads\"));
                //將反斜線取代成斜線，避免錯誤發生，並設定成圖片物件的來源
                img.ImageUrl = url.Replace(@"\", @"/");
                TableCell cell = new TableCell();//新增儲存格
                //加入目前列
                imgTable.Rows[imgTable.Rows.Count - 1].Cells.Add(cell);
                cell.Controls.Add(img);//圖片控制項加入儲存格
            }
        }
    }
}