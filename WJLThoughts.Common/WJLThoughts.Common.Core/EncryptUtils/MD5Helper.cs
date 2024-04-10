using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WJLThoughts.Common.Core.EncryptUtils
{
    /// <summary>
    /// MD5辅助类
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string encryptStr)
        {
            try
            {
                var md5 = new MD5CryptoServiceProvider();
                string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(encryptStr)), 4, 8);
                t2 = t2.Replace("-", "");
                return t2;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string encryptStr)
        {
            try
            {
                string cl = encryptStr;
                string ret = "";
                MD5 md5 = MD5.Create();
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
                // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                for (int i = 0; i < s.Length; i++)
                {
                    // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                    ret = ret + s[i].ToString("X");
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string encryptStr)
        {
            try
            {
                string cl = encryptStr;
                MD5 md5 = MD5.Create();
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
                return Convert.ToBase64String(s);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 文件MD5值
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open))
                {
                    System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(file);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
    }
}
