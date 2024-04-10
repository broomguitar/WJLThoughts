using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace WJLThoughts.Common.Core.Auxs
{
    /// <summary>
    /// 枚举描述信息特性
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        private string m_strDescription;
        public EnumDescriptionAttribute(string strPrinterName)
        {
            m_strDescription = strPrinterName;
        }

        public string Description
        {
            get { return m_strDescription; }
        }
    }
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetDescriptionAttribute(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return value.ToString();
            }
            object[] attributes = fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((EnumDescriptionAttribute)attributes[0]).Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
