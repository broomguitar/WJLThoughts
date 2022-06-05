using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WJLThoughts.Common.EncryptUtils
{
    /// <summary>
    /// DES加密解密辅助类
    /// </summary>
    public static class DESHelper
    {
        /// <summary>  
        /// DES加密方法  
        /// </summary>  
        /// <param name="encryptedValue">要加密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>加密后的字符串</returns>  
        public static string DESEncrypt(string originalValue, string key, string iv)
        {
            try
            {
                string encryptKeyall = Convert.ToString(key);    //定义密钥
                if (encryptKeyall.Length < 9)
                {
                    for (; ; )
                    {
                        if (encryptKeyall.Length < 9)
                            encryptKeyall += encryptKeyall;
                        else
                            break;
                    }
                }
                string encryptKey = encryptKeyall.Substring(0, 8);
                using (DESCryptoServiceProvider sa
                    = new DESCryptoServiceProvider())
                {
                    sa.Key = Encoding.UTF8.GetBytes(encryptKey);
                    sa.IV = Encoding.UTF8.GetBytes(iv);
                    using (ICryptoTransform ct = sa.CreateEncryptor())
                    {
                        byte[] by = Encoding.UTF8.GetBytes(originalValue);
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, ct,
                                                             CryptoStreamMode.Write))
                            {
                                cs.Write(by, 0, by.Length);
                                cs.FlushFinalBlock();
                            }
                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// DES解密方法  
        /// </summary>  
        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DESDecrypt(string encryptedValue, string key, string iv)
        {
            try
            {
                string encryptKeyall = Convert.ToString(key);    //定义密钥
                if (encryptKeyall.Length < 9)
                {
                    for (; ; )
                    {
                        if (encryptKeyall.Length < 9)
                            encryptKeyall += encryptKeyall;
                        else
                            break;
                    }
                }
                string encryptKey = encryptKeyall.Substring(0, 8);
                using (DESCryptoServiceProvider sa =
                    new DESCryptoServiceProvider
                    { Key = Encoding.UTF8.GetBytes(encryptKey), IV = Encoding.UTF8.GetBytes(iv) })
                {
                    using (ICryptoTransform ct = sa.CreateDecryptor())
                    {
                        byte[] byt = Convert.FromBase64String(encryptedValue);

                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                            {
                                cs.Write(byt, 0, byt.Length);
                                cs.FlushFinalBlock();
                            }
                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
