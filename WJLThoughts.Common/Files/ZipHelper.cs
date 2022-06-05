using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace WJLThoughts.Common.Files
{
    /// <summary>
    /// 解压缩类
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePath">原文件</param>
        /// <param name="zipFilePath">压缩后文件</param>
        /// <returns></returns>
        public static bool Zip(string filePath, string zipFilePath)
        {
            try
            {
                ZipFile.CreateFromDirectory(filePath, zipFilePath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePath">原文件</param>
        /// <param name="zipFilePath">压缩后文件</param>
        /// <param name="compressionLevel">压缩级别</param>
        /// <param name="includeBaseDirectory">是否包括基本文件夹</param>
        /// <returns></returns>
        public static bool Zip(string filePath, string zipFilePath, CompressionLevel compressionLevel, bool includeBaseDirectory = false)
        {
            try
            {
                ZipFile.CreateFromDirectory(filePath, zipFilePath, compressionLevel, includeBaseDirectory);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool Zip(string filePath, string zipFilePath, CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
        {
            try
            {
                ZipFile.CreateFromDirectory(filePath, zipFilePath, compressionLevel, includeBaseDirectory, entryNameEncoding);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UnZip(string zipFilePath, string filePath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFilePath, filePath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UnZip(string zipFilePath, string filePath, Encoding entryNameEncoding)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFilePath, filePath, entryNameEncoding);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
