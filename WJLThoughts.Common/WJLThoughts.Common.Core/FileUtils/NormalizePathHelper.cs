using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WJLThoughts.Common.Core.FileUtils
{
    /// <summary>
    /// 统一化路径
    /// </summary>
   public class NormalizePathHelper
    {
        /// <summary>
        /// 根据平台统一化路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string NormalizePath(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return path?.Replace('/', '\\').Trim();
            else
                return path?.Replace('\\', '/').Trim();
        }
        /// <summary>
        /// 统一化文件夹路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string filePath)
        {
            return NormalizePath(System.IO.Path.GetDirectoryName(filePath));
        }
    }
}
