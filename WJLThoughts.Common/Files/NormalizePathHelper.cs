using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WJLThoughts.Common.Files
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
    }
}
