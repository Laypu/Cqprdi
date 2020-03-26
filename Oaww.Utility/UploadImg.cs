using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oaww.Utility
{
    public static class UploadImg
    {
        #region uploadImgThumb
        public static string uploadImgThumb(string filename, string thumpfilename, int maxWidth, adjustImageEnum adjustImage = adjustImageEnum.none)
        {

            try
            {

                System.Drawing.Image img = default(System.Drawing.Image);
                Bitmap bmp = default(Bitmap);
                Graphics g = default(Graphics);
                int newWidth = 0;
                int newHeight = 0;
                using (FileStream imgfile = new FileStream(filename, System.IO.FileMode.Open))
                {
                    img = System.Drawing.Image.FromStream(imgfile);
                    switch (adjustImage)
                    {
                        case adjustImageEnum.none:
                            // // zoom out
                            if ((img.Width > maxWidth))
                            {
                                newWidth = maxWidth;
                                newHeight = img.Height * newWidth / img.Width;
                            }
                            else
                            {
                                newWidth = img.Width;
                                newHeight = img.Height;
                            }

                            break;
                        case adjustImageEnum.limitMaxImageSize:

                            if ((img.Width > img.Height && img.Width > maxWidth))
                            {
                                // // resize image based on its width
                                newWidth = maxWidth;
                                newHeight = img.Height * newWidth / img.Width;
                            }
                            else if ((img.Height > img.Width && img.Height > maxWidth))
                            {
                                // // resize image based on its height
                                newHeight = maxWidth;
                                newWidth = img.Width * newHeight / img.Height;
                            }
                            else
                            {
                                // // zoom out
                                if ((img.Width > maxWidth))
                                {
                                    newWidth = maxWidth;
                                    newHeight = img.Height * newWidth / img.Width;
                                }
                                else
                                {
                                    newWidth = img.Width;
                                    newHeight = img.Height;
                                }
                            }

                            break;
                        case adjustImageEnum.limitMinImageSize:

                            // // at least one side must has specified size
                            if ((img.Width > img.Height))
                            {
                                // // resize image based on its width
                                newHeight = maxWidth;
                                newWidth = img.Width * newHeight / img.Height;
                            }
                            else if ((img.Height > img.Width))
                            {
                                // // resize image based on its height
                                newWidth = maxWidth;
                                newHeight = img.Height * newWidth / img.Width;
                            }
                            else
                            {
                                // // zoom out
                                if ((img.Width > maxWidth))
                                {
                                    newWidth = maxWidth;
                                    newHeight = img.Height * newWidth / img.Width;
                                }
                                else
                                {
                                    newWidth = img.Width;
                                    newHeight = img.Height;
                                }
                            }

                            break;
                    }

                    bmp = new Bitmap(newWidth, newHeight);
                    g = Graphics.FromImage(bmp);
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(img, 0, 0, newWidth, newHeight);
                    img.Dispose();

                    bmp.Save(thumpfilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    g.Dispose();
                }

                return thumpfilename.Split('\\').Last();
            }
            catch (Exception ex)
            {
                //NLogManagement.SystemLogInfo("uploadImgThumb Error=" + ex.Message);
                return "";
            }

        }

        public static string uploadImgThumb(HttpPostedFileBase UploadFile, string uploadPath, int maxWidth, adjustImageEnum adjustImage = adjustImageEnum.none)
        {

            // // uploadPath ends with /

            string fullFileName, fileMainName;
            string fileNameExt = "";
            var r = new Random();
            Image img;
            Bitmap bmp;
            Graphics g;
            int newWidth = default(int), newHeight = default(int);


            if (UploadFile != null && UploadFile.ContentLength > 0)
            {
                fileMainName = UploadFile.FileName;
                fileMainName = fileMainName.Substring(fileMainName.LastIndexOf(@"\") + 1);

                if (fileMainName.LastIndexOf(".") != -1)
                {
                    fileNameExt = fileMainName.Substring(fileMainName.LastIndexOf("."));
                    fileMainName = fileMainName.Substring(0, fileMainName.LastIndexOf("."));
                }

                while (File.Exists(uploadPath + fileMainName + fileNameExt))
                    fileMainName += "_" + r.Next().ToString() + "_thumb";
            }
            else
                return null;


            fullFileName = uploadPath + fileMainName + fileNameExt;
            UploadFile.SaveAs(fullFileName);               // // temp original image

            img = System.Drawing.Image.FromFile(fullFileName);

            switch (adjustImage)
            {
                case adjustImageEnum.none:
                    {
                        if (img.Width > maxWidth)
                        {
                            newWidth = maxWidth;
                            newHeight = img.Height * newWidth / img.Width;
                        }
                        else
                        {
                            newWidth = img.Width;
                            newHeight = img.Height;
                        }

                        break;
                    }

                case adjustImageEnum.limitMaxImageSize:
                    {
                        if (img.Width > img.Height && img.Width > maxWidth)
                        {
                            // // resize image based on its width
                            newWidth = maxWidth;
                            newHeight = img.Height * newWidth / img.Width;
                        }
                        else if (img.Height > img.Width && img.Height > maxWidth)
                        {
                            // // resize image based on its height
                            newHeight = maxWidth;
                            newWidth = img.Width * newHeight / img.Height;
                        }
                        else if (img.Width > maxWidth)
                        {
                            newWidth = maxWidth;
                            newHeight = img.Height * newWidth / img.Width;
                        }
                        else
                        {
                            newWidth = img.Width;
                            newHeight = img.Height;
                        }

                        break;
                    }

                case adjustImageEnum.limitMinImageSize:
                    {

                        // // at least one side must has specified size
                        if (img.Width > img.Height)
                        {
                            // // resize image based on its width
                            newHeight = maxWidth;
                            newWidth = img.Width * newHeight / img.Height;
                        }
                        else if (img.Height > img.Width)
                        {
                            // // resize image based on its height
                            newWidth = maxWidth;
                            newHeight = img.Height * newWidth / img.Width;
                        }
                        else if (img.Width > maxWidth)
                        {
                            newWidth = maxWidth;
                            newHeight = img.Height * newWidth / img.Width;
                        }
                        else
                        {
                            newWidth = img.Width;
                            newHeight = img.Height;
                        }

                        break;
                    }
            }

            bmp = new Bitmap(newWidth, newHeight); // , img.PixelFormat)
            g = Graphics.FromImage(bmp);
            g.CompositingQuality =  CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, newWidth, newHeight);
            // newImg = img.GetThumbnailImage(newWidth, newHeight, Nothing, Nothing)
            img.Dispose();


            bmp.Save(fullFileName, System.Drawing.Imaging.ImageFormat.Jpeg);


            g.Dispose();


            return fileMainName + fileNameExt;
        }
        #endregion

        #region uploadImgThumbMaxHeight
        public static string uploadImgThumbMaxHeight(string filename, string thumpfilename, int maxHeight, string fileformat, adjustImageEnum adjustImage = adjustImageEnum.none)
        {
            try
            {
                System.Drawing.Image img = default(System.Drawing.Image);
                Bitmap bmp = default(Bitmap);
                Graphics g = default(Graphics);
                float newWidth = 0;
                float newHeight = 0;
                using (FileStream imgfile = new FileStream(filename, System.IO.FileMode.Open))
                {
                    img = System.Drawing.Image.FromStream(imgfile);
                    var haschange = false;
                    switch (adjustImage)
                    {
                        case adjustImageEnum.none:
                            // // zoom out
                            if ((img.Height > maxHeight))
                            {
                                newHeight = maxHeight;
                                newWidth = img.Width * newHeight / img.Height;
                                haschange = true;
                            }
                            else
                            {
                                newWidth = img.Width;
                                newHeight = img.Height;
                            }

                            break;
                        case adjustImageEnum.limitMaxImageHeight:
                            // // zoom out
                            if ((img.Height != maxHeight))
                            {
                                newHeight = maxHeight;
                                newWidth = img.Width * newHeight / img.Height;
                                haschange = true;
                            }
                            else
                            {
                                newWidth = img.Width;
                                newHeight = img.Height;
                            }

                            break;
                        case adjustImageEnum.limitMaxImageSize:

                            if ((img.Width > img.Height && img.Height > maxHeight))
                            {
                                // // resize image based on its width
                                newHeight = maxHeight;
                                newWidth = img.Width * newHeight / img.Height;
                                haschange = true;
                            }
                            else if ((img.Height > img.Width && img.Height > maxHeight))
                            {
                                // // resize image based on its height
                                newWidth = maxHeight;
                                newHeight = img.Height * newWidth / img.Width;
                                haschange = true;
                            }
                            else
                            {
                                if ((img.Height > maxHeight))
                                {
                                    newHeight = maxHeight;
                                    newWidth = img.Width * newHeight / img.Height;
                                    haschange = true;
                                }
                                else
                                {
                                    newWidth = img.Width;
                                    newHeight = img.Height;
                                }
                            }
                            break;
                        case adjustImageEnum.limitMinImageSize:

                            // // at least one side must has specified size
                            if ((img.Width > img.Height))
                            {
                                // // resize image based on its width
                                newWidth = maxHeight;
                                newHeight = img.Height * newWidth / img.Width;
                                haschange = true;
                            }
                            else if ((img.Height > img.Width))
                            {
                                // // resize image based on its height
                                newHeight = maxHeight;
                                newWidth = img.Width * newHeight / img.Height;
                                haschange = true;
                            }
                            else
                            {
                                // // zoom out
                                if ((img.Height > maxHeight))
                                {
                                    newHeight = maxHeight;
                                    newWidth = img.Width * newHeight / img.Height;
                                    haschange = true;
                                }
                                else
                                {
                                    newWidth = img.Width;
                                    newHeight = img.Height;
                                }
                            }

                            break;
                    }
                    if (maxHeight >= newHeight && haschange == false)   //直接複製就好
                    {
                        File.Copy(filename, thumpfilename, true);
                    }
                    else
                    {
                        bmp = new Bitmap((int)newWidth, (int)newHeight);
                        g = Graphics.FromImage(bmp);
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(img, 0, 0, newWidth, newHeight);
                        img.Dispose();

                        if (fileformat.ToLower().IndexOf("png", 0, StringComparison.Ordinal) >= 0)
                        {
                            bmp.Save(thumpfilename, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        else
                        {
                            bmp.Save(thumpfilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        g.Dispose();
                    }
                }
                return thumpfilename;
            }
            catch (Exception ex)
            {
                //NLogManagement.SystemLogInfo("uploadImgThumb Error=" + ex.Message);
                return "";
            }

        }

        #endregion

        #region uploadImgThumbCustomer
        public static string uploadImgThumbCustomer(string filename, string thumpfilename, int maxHeight, int maxWidth)
        {

            try
            {
                System.Drawing.Image img = default(System.Drawing.Image);
                Bitmap bmp = default(Bitmap);
                Graphics g = default(Graphics);
                float newWidth = maxWidth;
                float newHeight = maxHeight;
                using (FileStream imgfile = new FileStream(filename, System.IO.FileMode.Open))
                {
                    img = System.Drawing.Image.FromStream(imgfile);
                    bmp = new Bitmap((int)newWidth, (int)newHeight);
                    g = Graphics.FromImage(bmp);
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(img, 0, 0, newWidth, newHeight);
                    img.Dispose();

                    bmp.Save(thumpfilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    g.Dispose();
                }

                return thumpfilename;
            }
            catch (Exception ex)
            {
                //NLogManagement.SystemLogInfo("uploadImgThumb Error=" + ex.Message);
                return "";
            }

        }

        #endregion

        #region uploadImgThumbCustomer
        public static string uploadImgThumbCustomer(string filename, string thumpfilename, int maxHeight, int maxWidth, string fileformat)
        {
            try
            {
                System.Drawing.Image img = default(System.Drawing.Image);
                Bitmap bmp = default(Bitmap);
                Graphics g = default(Graphics);
                float newWidth = maxWidth;
                float newHeight = maxHeight;
                using (FileStream imgfile = new FileStream(filename, System.IO.FileMode.Open))
                {
                    img = System.Drawing.Image.FromStream(imgfile);
                    if (img.Height != maxHeight || img.Width != maxWidth)
                    {
                        bmp = new Bitmap((int)newWidth, (int)newHeight);
                        g = Graphics.FromImage(bmp);
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(img, 0, 0, newWidth, newHeight);
                        img.Dispose();

                        if (fileformat.ToLower().IndexOf("png", 0, StringComparison.Ordinal) >= 0)
                        {
                            bmp.Save(thumpfilename, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        else
                        {
                            bmp.Save(thumpfilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        g.Dispose();
                    }
                    else
                    {
                        File.Copy(filename, thumpfilename, true);
                    }
                }

                return thumpfilename;
            }
            catch (Exception ex)
            {
                //NLogManagement.SystemLogInfo("uploadImgThumb Error=" + ex.Message);
                return "";
            }

        }
        #endregion
    }
}
