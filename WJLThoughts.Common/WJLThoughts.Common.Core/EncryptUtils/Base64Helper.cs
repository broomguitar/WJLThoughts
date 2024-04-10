using System;
using System.Collections.Generic;
using System.Text;

namespace WJLThoughts.Common.Core.EncryptUtils
{
    /// <summary>
    /// Base64帮助类,编码格式默认 Encoding.UTF8
    /// </summary>
    public static class Base64Helper
    {
        public static string ToBase64String(string value, Encoding encoding = null)
        {
            try
            {
                if (value == null || value == "")
                {
                    return "";
                }
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] bytes = encoding.GetBytes(value);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string FromBase64String(string value, Encoding encoding = null)
        {
            try
            {
                if (value == null || value == "")
                {
                    return "";
                }
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] bytes = Convert.FromBase64String(value);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
