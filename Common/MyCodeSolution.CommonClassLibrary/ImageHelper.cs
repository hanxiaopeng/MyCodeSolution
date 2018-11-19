using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MyCodeSolution.CommonClassLibrary
{
    /// <summary>
    /// 图片处理类
    /// </summary>
    public class ImageHelper
    {
        internal static readonly string AllowExt = ".jpe|.jpeg|.jpg|.png|.tif|.tiff|.bmp|.gif";

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="ImgPath"></param>
        public ImageHelper()
        {
            #region init htmimes
            #endregion
        }

        private static Hashtable GetHTMimes()
        {
            Hashtable htmimes = new Hashtable();
            htmimes[".jpe"] = "image/jpeg";
            htmimes[".jpeg"] = "image/jpeg";
            htmimes[".jpg"] = "image/jpeg";
            htmimes[".png"] = "image/png";
            htmimes[".tif"] = "image/tiff";
            htmimes[".tiff"] = "image/tiff";
            htmimes[".bmp"] = "image/bmp";
            htmimes[".gif"] = "image/gif";
            return htmimes;
        }

        #region 文字水印

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="SavePath">文字</param>
        /// <param name="SavePath">保存图片路径</param>
        public static void LetterWaterMark(string sourceImagePath, string addText, string SavePath, int[] WaterPos, Font font, Color color)
        {
            Image image = Image.FromFile(sourceImagePath);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            //Font f = new Font("宋体", 32);
            Brush b = new SolidBrush(color);
            g.DrawString(addText, font, b, WaterPos[0], WaterPos[1]);
            g.Dispose();
            GC.SuppressFinalize(g);

            //保存加水印过后的图片
            image.Save(SavePath);
            image.Dispose();
            GC.SuppressFinalize(image);
        }
        #endregion

        #region 加图片水印

        /// <summary>
        /// 水印加到源图片上
        /// </summary>
        /// <param name="sourceImagePath"></param>
        /// <param name="waterMarkFile"></param>
        /// <param name="SavePath"></param>
        /// <param name="WaterPos"></param>
        public static void PicWaterMark(string sourceImagePath, string waterMarkFile, string SavePath, int[] WaterPos)
        {
            try
            {
                Image image = Image.FromFile(sourceImagePath);
                Image copyImage = Image.FromFile(waterMarkFile);
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(copyImage, new Rectangle(WaterPos[0], WaterPos[1], copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                GC.SuppressFinalize(g);

                //保存加水印过后的图片
                image.Save(SavePath);
                image.Dispose();
                GC.SuppressFinalize(image);
            }
            catch
            { }
        }

        /// <summary>
        /// 水印加到空白图片上
        /// </summary>
        /// <param name="waterMarkFile"></param>
        /// <param name="SavePath"></param>
        /// <param name="WaterPos"></param>
        public static void PicWaterMark(string waterMarkFile, string SavePath, int imgWidth, int imgHeight)
        {
            try
            {
                Bitmap bitmap = new Bitmap(imgWidth, imgHeight, PixelFormat.Format32bppArgb);
                Graphics gWhite = Graphics.FromImage(bitmap);
                gWhite.Clear(System.Drawing.Color.White);
                gWhite.DrawImage(bitmap, 0, 0);
                gWhite.Dispose();
                GC.SuppressFinalize(gWhite);

                Image copyImage = Image.FromFile(waterMarkFile);
                int posX = 0;
                int posY = 0;
                if (imgWidth > copyImage.Width)
                {
                    posX = (imgWidth - copyImage.Width) / 2;
                }
                if (imgHeight > copyImage.Height)
                {
                    posY = (imgHeight - copyImage.Height) / 2;
                }
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(copyImage, new Rectangle(posX, posY, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                GC.SuppressFinalize(g);

                //保存加水印过后的图片
                bitmap.Save(SavePath);
                bitmap.Dispose();
                GC.SuppressFinalize(bitmap);
            }
            catch
            { }
        }
        #endregion

        #region 图像明暗调整
        /// <summary>
        /// 图像明暗调整
        /// </summary>
        /// <param name="b">原始图</param>
        /// <param name="degree">亮度[-255, 255]</param>
        /// <returns></returns>
        public static Bitmap Lighten(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -255) degree = -255;
            if (degree > 255) degree = 255;

            try
            {
                int width = b.Width;
                int height = b.Height;

                int pix = 0;

                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // 处理指定位置像素的亮度
                            for (int i = 0; i < 3; i++)
                            {
                                pix = p[i] + degree;

                                if (degree < 0) p[i] = (byte)Math.Max(0, pix);
                                if (degree > 0) p[i] = (byte)Math.Min(255, pix);

                            } // i
                            p += 3;
                        } // x
                        p += offset;
                    } // y
                }

                b.UnlockBits(data);

                return b;
            }
            catch
            {
                return null;
            }

        } // end of Lighten
        #endregion

        #region 图像对比度调整
        /// <summary>
        /// 图像对比度调整
        /// </summary>
        /// <param name="b">原始图</param>
        /// <param name="degree">对比度[-100, 100]</param>
        /// <returns></returns>
        public static Bitmap Contrast(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -100) degree = -100;
            if (degree > 100) degree = 100;

            try
            {
                double pixel = 0;
                double contrast = (100.0 + degree) / 100.0;
                contrast *= contrast;
                int width = b.Width;
                int height = b.Height;
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // 处理指定位置像素的对比度
                            for (int i = 0; i < 3; i++)
                            {
                                pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
                                if (pixel < 0) pixel = 0;
                                if (pixel > 255) pixel = 255;
                                p[i] = (byte)pixel;
                            } // i
                            p += 3;
                        } // x
                        p += offset;
                    } // y
                }
                b.UnlockBits(data);
                return b;
            }
            catch
            {
                return null;
            }
        } // end of Contrast
        #endregion

        #region 生成缩略图

        /// <summary>
        /// 将图片变为缩略图，如果宽度大于高度，按照宽度缩略，否则按照高度缩略
        /// </summary>
        /// <param name="SourceImagePath">源图片</param>
        /// <param name="SavePath">目标路径</param>
        /// <param name="ImageWidth">缩略图宽度</param>
        /// <param name="ImageWidth">缩略图高度</param>
        public static void Breviary(string SourceImagePath, string SavePath, int ImageWidth, int ImageHeight)
        {
            //int BreviaryImageWidth = ImageWidth;
            string sExt = SourceImagePath.Substring(SourceImagePath.LastIndexOf(".")).ToLower();
            if (SourceImagePath.ToString() == System.String.Empty) throw new NullReferenceException("SourceImagePath is null!");
            if (!CheckValidExt(sExt))
            {
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]", "SourceImagePath");
            }
            //从 原图片 创建 Image 对象 
            Image image = Image.FromFile(SourceImagePath);
            //int num = ((BreviaryImageWidth / 4) * 3);
            int width = image.Width;
            int height = image.Height;
            //计算图片的比例 
            int iImageWidth = 0;
            int iImageHeight = 0;
            if (width >= height)
            {
                iImageWidth = ImageWidth;
                iImageHeight = (int)((1.0 * height / width) * ImageWidth);
            }
            else
            {
                iImageWidth = (int)((1.0 * width / height) * ImageHeight);
                iImageHeight = ImageHeight;
            }
            //用指定的大小和格式初始化 Bitmap 类的新实例 
            Bitmap bitmap = new Bitmap(iImageWidth, iImageHeight, PixelFormat.Format32bppArgb);
            //从指定的 Image 对象创建新 Graphics 对象 
            Graphics graphics = Graphics.FromImage(bitmap);
            //清除整个绘图面并以透明背景色填充 
            graphics.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制 原图片 对象 
            graphics.DrawImage(image, new Rectangle(0, 0, iImageWidth, iImageHeight));
            image.Dispose();
            try
            {
                //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件 
                string savepath = (SavePath == null ? SourceImagePath : SavePath);
                SaveImage(bitmap, savepath, GetCodecInfo((string)GetHTMimes()[sExt]));
            }
            catch (Exception e)
            {
            }
            finally
            {
                bitmap.Dispose();
                graphics.Dispose();
            }
        }

        /// <summary> 
        /// 检测扩展名的有效性 
        /// </summary> 
        /// <param name="sExt">文件名扩展名</param> 
        /// <returns>如果扩展名有效,返回true,否则返回false.</returns> 
        private static bool CheckValidExt(string sExt)
        {
            bool flag = false;
            string[] aExt = AllowExt.Split('|');
            foreach (string filetype in aExt)
            {
                if (filetype.ToLower() == sExt)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        /// <summary> 
        /// 获取图像编码解码器的所有相关信息 
        /// </summary> 
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param> 
        /// <returns>返回图像编码解码器的所有相关信息</returns> 
        static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        #endregion

        #region 按规定的尺寸缩裁图

        /// <summary>
        /// 按规定的尺寸缩裁图
        /// </summary>
        /// <param name="thumbnailImagePath">目标路径</param>
        public static void BreviaryDimension(string SourceImagePath, string SavePath, int ImageWidth, int ImageHeight)
        {
            string sExt = SourceImagePath.Substring(SourceImagePath.LastIndexOf(".")).ToLower();
            if (SourceImagePath.ToString() == System.String.Empty) throw new NullReferenceException("SourceImagePath is null!");
            if (!CheckValidExt(sExt))
            {
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]", "SourceImagePath");
            }
            //从 原图片 创建 Image 对象 
            Image image = Image.FromFile(SourceImagePath);

            //用指定的大小和格式初始化 Bitmap 类的新实例 
            Bitmap bitmap = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format32bppArgb);
            //从指定的 Image 对象创建新 Graphics 对象 
            Graphics graphics = Graphics.FromImage(bitmap);
            //清除整个绘图面并以透明背景色填充 
            graphics.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制 原图片 对象 
            graphics.DrawImage(image, new Rectangle(0, 0, ImageWidth, ImageHeight));
            image.Dispose();
            try
            {
                //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件 
                string savepath = (SavePath == null ? SourceImagePath : SavePath);
                SaveImage(bitmap, savepath, GetCodecInfo((string)GetHTMimes()[sExt]));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
                graphics.Dispose();
            }
        }

        #endregion

        #region 剪裁图片
        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="SourceImagePath">源图片路径</param>
        /// <param name="SavePath">保存剪裁后图片路径</param>
        /// <param name="CutWidth">剪裁宽度(必须大于0)</param>
        /// <param name="CutHeight">剪裁高度(必须大于0)</param>
        /// <param name="PointX">距离源图片左侧</param>
        /// <param name="PointY">距离源图片上侧</param>
        public static void CutImage(string SourceImagePath, string SavePath, int CutWidth, int CutHeight, int PointX, int PointY)
        {
            string sExt = SourceImagePath.Substring(SourceImagePath.LastIndexOf(".")).ToLower();
            Image imgSource = Image.FromFile(SourceImagePath);
            Bitmap bmImg = new Bitmap(CutWidth, CutHeight, PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bmImg);
            graphics.DrawImage(imgSource, new Rectangle(0, 0, CutWidth, CutHeight), PointX, PointY, CutWidth, CutHeight, GraphicsUnit.Pixel);
            imgSource.Dispose();
            try
            {
                //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件 
                string savepath = (SavePath == null ? SourceImagePath : SavePath);
                SaveImage(bmImg, savepath, GetCodecInfo((string)GetHTMimes()[sExt]));
            }
            catch (Exception e)
            {
            }
            finally
            {
                imgSource.Dispose();
                bmImg.Dispose();
                graphics.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private static void SaveImage(System.Drawing.Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象 
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)90));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }
    }
}
