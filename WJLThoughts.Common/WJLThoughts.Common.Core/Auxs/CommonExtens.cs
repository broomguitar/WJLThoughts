using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WJLThoughts.Common.Core.Auxs
{
    public static class CommonExtens
    {
        #region String
        /// <summary>
        /// 指示该字符串是否是Null或空字符串。
        /// </summary>
        /// <summary xml:lang="en">
        /// 
        /// </summary>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }
        public static string Cut(this string text, int length, string filler = null)
        {
            if (text.Length <= length)
                return text;
            else
            {
                return text.Remove(length) + filler;
            }
        }
        /// <summary>
        /// 去除字符串中中文
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string RemoveChineseCharacter(this string src)
        {
            if (string.IsNullOrEmpty(src))
            {
                return string.Empty;
            }
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]"
            );
            return regex.Replace(src, string.Empty);
        }
        #endregion

        #region Timestamp <-> Datetime
        public static long ToTimeStamp(this DateTime date, bool withMilliseconds = true)
        {
            TimeSpan ts = date - new DateTime(1970, 1, 1);
            if (withMilliseconds)
                return Convert.ToInt64(ts.TotalMilliseconds);
            else
                return Convert.ToInt64(ts.TotalSeconds);
        }

        public static DateTime ToDateTime(this long timeStamp, bool withMilliseconds = true)
        {
            if (withMilliseconds)
                return TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1)).AddMilliseconds(timeStamp);
            else
                return TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1)).AddSeconds(timeStamp);
        }
        #endregion

        #region DigitalConverter
        public static int? ToInt(this string text)
        {
            var result = 0;
            if (int.TryParse(text, out result))
                return result;
            else
                return null;
        }

        public static double? ToDouble(this string text)
        {
            var result = 0.0;
            if (double.TryParse(text, out result))
                return result;
            else
                return null;
        }
        #endregion
    }
}
