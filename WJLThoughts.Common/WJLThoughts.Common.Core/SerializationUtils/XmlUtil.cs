using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace WJLThoughts.Common.Core.SerializationUtils
{
    /// <summary>
    /// xml序列化
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">序列化对象</param>
        /// <returns></returns>
        public static string Serialize<T>(T model) where T : class
        {
            try
            {
                string xml = string.Empty;
                using (var ms = new MemoryStream())
                {
                    XmlSerializer xmlSer = new XmlSerializer(typeof(T));
                    xmlSer.Serialize(ms, model);
                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    xml = sr.ReadToEnd();
                }
                return xml;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strXml">xml数据</param>
        /// <returns></returns>
        public static T Deserialize<T>(string strXml) where T : class
        {
            try
            {
                object obj;
                using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(strXml)))
                {
                    using (XmlReader xmlReader = XmlReader.Create(memoryStream))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        obj = xmlSerializer.Deserialize(xmlReader);
                    }
                }
                return obj as T;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
