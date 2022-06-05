using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WJLThoughts.Common.EncryptUtils
{
    /// <summary>
    /// AES算法加密解密类
    /// </summary>
    public static class AESHelper
    {
        /// <summary>
        /// AES 算法加密(默认ECB模式) 将明文加密，返回密文
        /// </summary>
        /// <param name="EncryptStr">明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="mode">模式</param>
        /// <returns>加密的密文</returns>
        public static string AESEncrypt(string EncryptStr, string Key, string Iv, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] ivArray = Encoding.UTF8.GetBytes(Iv);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(EncryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = mode;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.IV = ivArray;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// AES 算法解密(默认ECB模式)，返回明文
        /// </summary>
        /// <param name="DecryptStr">密文</param>
        /// <param name="Key">密钥</param>
        /// <param name="mode">模式</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string DecryptStr, string Key, string Iv, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] ivArray = Encoding.UTF8.GetBytes(Iv);
                byte[] toEncryptArray = Convert.FromBase64String((DecryptStr));

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = mode;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.IV = ivArray;
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// AES 算法加密(默认ECB模式) 将明文加密，加密后进行base64编码，返回密文
        /// </summary>
        /// <param name="EncryptStr">明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="mode">模式</param>
        /// <returns>加密后base64编码的密文</returns>
        public static string AESEncryptor_Base64(string EncryptStr, string Key, string Iv, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] ivArray = Encoding.UTF8.GetBytes(Iv);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(EncryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = mode;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.IV = ivArray;
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// AES 算法解密(默认ECB模式) 将密文base64解码进行解密，返回明文
        /// </summary>
        /// <param name="DecryptStr">密文</param>
        /// <param name="Key">密钥</param>
        /// <param name="mode">模式</param>
        /// <returns>明文</returns>
        public static string AESDecryptor_Base64(string DecryptStr, string Key, string Iv, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] ivArray = Encoding.UTF8.GetBytes(Iv);
                byte[] toEncryptArray = Convert.FromBase64String(DecryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = mode;
                rDel.Padding = PaddingMode.PKCS7;
                rDel.IV = ivArray;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///AES 算法加密(默认ECB模式) 将明文加密，加密后进行Hex编码，返回密文
        /// </summary>
        /// <param name="str">明文</param>
        /// <param name="key">密钥</param>
        /// <param name="mode">模式</param>
        /// <returns>加密后Hex编码的密文</returns>
        public static string AESEncryptor_Hex(string str, string key, string iv, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return null;
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                {
                    Key = StrToHexByte(key),
                    IV = StrToHexByte(iv),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };

                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return ToHexString(resultArray);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///AES 算法解密(默认ECB模式) 将密文Hex解码后进行解密，返回明文
        /// </summary>
        /// <param name="str">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="mode">模式</param>
        /// <returns>明文</returns>
        public static string AESDecryptor_Hex(string str, string key, string iv, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return null;
                Byte[] toEncryptArray = StrToHexByte(str);

                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                {
                    Key = StrToHexByte(key),
                    IV = StrToHexByte(iv),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };

                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// byte数组Hex编码
        /// </summary>
        /// <param name="bytes">需要进行编码的byte[]</param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            try
            {
                string hexString = string.Empty;
                if (bytes != null)
                {
                    StringBuilder strB = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        strB.Append(bytes[i].ToString("X2"));
                    }
                    hexString = strB.ToString();
                }
                return hexString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary> 
        /// 字符串进行Hex解码(Hex.decodeHex())
        /// </summary> 
        /// <param name="hexString">需要进行解码的字符串</param> 
        /// <returns></returns> 
        public static byte[] StrToHexByte(string hexString)
        {
            try
            {
                hexString = hexString.Replace(" ", "");
                if ((hexString.Length % 2) != 0)
                    hexString += " ";
                byte[] returnBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                return returnBytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
