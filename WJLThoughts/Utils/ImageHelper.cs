using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WJLThoughts.Utils
{
    public class ImageHelper
    {
        /// <summary>
        /// Bitmap-->BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            try
            {
                if (bitmap == null)
                {
                    return null;
                }
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
                return bitmapImage;
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// Bitmap-->BitmapFrame
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapFrame BitmapToBitmapFrame(Bitmap bitmap)
        {
            try
            {
                if (bitmap == null)
                {
                    return null;
                }
                BitmapFrame bf = null;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    bf = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
                return bf;
            }
            catch (Exception)
            {

                return null;
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        public static BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();

            try
            {
                BitmapSource source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                return source;
            }
            finally
            {
                DeleteObject(hBitmap);
            }
        }
        /// <summary>
        /// Bitmap---->byte[]
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                bitmap.Save(ms, bitmap.RawFormat);
                byte[] byteImage = new byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
        /// <summary>
        /// BitmapImage---->Bitmap
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage, bool isPng = false)
        {
            try
            {
                Bitmap bitmap;
                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    if (isPng)
                    {
                        enc = new PngBitmapEncoder();
                    }
                    enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                    enc.Save(outStream);
                    bitmap = new Bitmap(outStream);
                    return bitmap;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// BitmapImage---->byte[]
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToBytes(BitmapImage bitmapImage)
        {
            byte[] ByteArray = null;

            try
            {
                Stream stream = bitmapImage.StreamSource;
                if (stream != null && stream.Length > 0)
                {
                    stream.Position = 0;
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        ByteArray = br.ReadBytes((int)stream.Length);
                    }
                }
            }
            catch
            {
                return null;
            }

            return ByteArray;
        }
        /// <summary>
        /// ImageSource---->Bitmap
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        public static Bitmap ImageSourceToBitmap(ImageSource imageSource)
        {
            try
            {
                BitmapSource m = (BitmapSource)imageSource;
                Bitmap bmp = new Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                System.Drawing.Imaging.BitmapData data = bmp.LockBits(
                new Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride); bmp.UnlockBits(data);
                bmp.UnlockBits(data);
                return bmp;
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// byte[]---->Bitmap
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap(stream);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
        }
        /// <summary>
        /// 裁剪图像
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <param name="cut"></param>
        /// <returns></returns>
        public static BitmapSource CutImage(BitmapSource bitmapSource, Int32Rect cut)
        {
            //计算Stride
            var stride = bitmapSource.Format.BitsPerPixel * cut.Width / 8;
            //声明字节数组
            byte[] data = new byte[cut.Height * stride];
            //调用CopyPixels
            bitmapSource.CopyPixels(cut, data, stride, 0);
            return BitmapSource.Create(cut.Width, cut.Height, 0, 0, PixelFormats.Bgra32, null, data, stride);
        }
        /// <summary>
        /// filepath---->bitmapImage
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(string imagePath, bool isZip = false)
        {
            if (File.Exists(imagePath))
            {
                try
                {
                    BitmapImage bim;
                    byte[] buf = null;
                    using (BinaryReader loader = new BinaryReader(File.Open(imagePath, FileMode.Open)))
                    {
                        FileInfo fd = new FileInfo(imagePath);
                        int Length = (int)fd.Length;
                        buf = new byte[Length];
                        buf = loader.ReadBytes((int)fd.Length);
                        loader.Close();
                        //开始加载图像
                        bim = new BitmapImage();
                        bim.BeginInit();
                        bim.CacheOption = BitmapCacheOption.OnLoad;
                        if (isZip)
                        {
                            bim.DecodePixelHeight = 100;
                        }
                        bim.StreamSource = new MemoryStream(buf);
                        bim.EndInit();
                        bim.Freeze();
                        loader.Dispose();
                    }
                    return bim;

                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// filpath--->bitmapsource
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                try
                {
                    byte[] buffer = File.ReadAllBytes(imagePath);
                    var ms = new System.IO.MemoryStream(buffer);
                    var bmp = new System.Drawing.Bitmap(ms);
                    var source = BitmapToBitmapSource(bmp);
                    ms.Close();
                    ms.Dispose();
                    bmp.Dispose();
                    return source;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="inputBitmapSource"></param>
        /// <param name="filePath"></param>
        public static void SaveImage(BitmapSource inputBitmapSource, string filePath)
        {
            try
            {
                BitmapEncoder encoder = null;
                var extName = System.IO.Path.GetExtension(filePath).ToLower();
                switch (extName)
                {
                    case ".png": encoder = new PngBitmapEncoder(); break;
                    case ".jpg": encoder = new JpegBitmapEncoder(); break;
                    case ".bmp": encoder = new BmpBitmapEncoder(); break;
                    default:
                        break;
                }
                encoder.Frames.Add(BitmapFrame.Create(inputBitmapSource));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SaveImage(Bitmap bitmap, string filePath)
        {
            SaveImage(BitmapToBitmapSource(bitmap), filePath);
        }
        public static void SaveJpgImage(Bitmap bitmap, string filePath)
        {
            System.Drawing.Imaging.ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            System.Drawing.Imaging.EncoderParameter myEncoderParameter;
            System.Drawing.Imaging.EncoderParameters myEncoderParameters;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.

            // An EncoderParameters object has an array of EncoderParameter

            // objects. In this case, there is only one

            // EncoderParameter object in the array.
            myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality level 25.
            myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save("Shapes025.jpg", myImageCodecInfo, myEncoderParameters);

            // Save the bitmap as a JPEG file with quality level 50.
            myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save("Shapes050.jpg", myImageCodecInfo, myEncoderParameters);

            // Save the bitmap as a JPEG file with quality level 75.
            myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 75L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save(filePath, myImageCodecInfo, myEncoderParameters);
        }
        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            System.Drawing.Imaging.ImageCodecInfo[] encoders;
            encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="filePath"></param>
        public static void SaveImageB(Bitmap bitmap, string filePath)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            PixelFormat pixelFormats = ConvertBmpPixelFormat(bitmap.PixelFormat);

            //Create Bitmap Source
            BitmapSource source = BitmapSource.Create(bitmap.Width,
                                                      bitmap.Height,
                                                      bitmap.HorizontalResolution,
                                                      bitmap.VerticalResolution,
                                                      pixelFormats,
                                                      null,
                                                      bitmapData.Scan0,
                                                      bitmapData.Stride * bitmap.Height,
                                                      bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            //new File Stream
            using (FileStream stream = new FileStream(filePath, FileMode.Create))

            {   //save Tiff Bitmap 
                BitmapEncoder encoder = null;
                var extName = System.IO.Path.GetExtension(filePath).ToLower();
                switch (extName)
                {
                    case ".png": encoder = new PngBitmapEncoder(); break;
                    case ".jpg": encoder = new JpegBitmapEncoder(); break;
                    case ".bmp": encoder = new BmpBitmapEncoder(); break;
                    default:
                        break;
                }
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(stream);
                stream.Close();
            }
        }
        //Convert Bmp Pixel Format to Media Pixel Format
        public static PixelFormat ConvertBmpPixelFormat(System.Drawing.Imaging.PixelFormat pixelformat)
        {
            PixelFormat pixelFormats = PixelFormats.Default;

            switch (pixelformat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    pixelFormats = PixelFormats.Bgra32;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                    pixelFormats = PixelFormats.Rgba64;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    pixelFormats = PixelFormats.Rgb24;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                    pixelFormats = PixelFormats.Rgb48;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    pixelFormats = PixelFormats.Gray8;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    pixelFormats = PixelFormats.Gray16;
                    break;
            }
            return pixelFormats;
        }
        public static TransformedBitmap GetTransformedBitmap(BitmapSource bitmapSource, double angle)
        {
            if (bitmapSource is null)
            {
                return null;
            }

            TransformedBitmap tb = new TransformedBitmap();
            tb.BeginInit();
            tb.Source = bitmapSource;
            RotateTransform transform = new RotateTransform(angle);
            tb.Transform = transform;
            tb.EndInit();
            return tb;
        }
        /// <summary>
        /// 图片文件base64序列化
        /// </summary>
        /// <param name="imgpath">图片路径</param>
        /// <returns></returns>
        public static string ImgToBase64StringFromFile(string imgpath)
        {
            return ImgToBase64String(new Bitmap(imgpath));
        }
        public static string ImgToBase64String(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            string pic = Convert.ToBase64String(arr);
            //string urlcode = System.Web.HttpUtility.UrlEncode(pic);

            return pic;
        }
    }
}
