using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace WJLThoughts.Common.Core.FileUtils
{
    /// <summary>
    /// 将实体对象读写到本地文集
    /// </summary>
    public static class ObjectToFileHelper
    {
        /// <summary>
        /// 读取Json文件数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullFileName">json文件路径</param>
        /// <returns></returns>
        public static T ReadDataFromJsonFile<T>(string fullFileName)
        {
            try
            {
                using (StreamReader reader = File.OpenText(fullFileName))
                {
                    JsonReader jsonReader = new JsonTextReader(reader);
                    return new JsonSerializer().Deserialize<T>(jsonReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 写入Json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据</param>
        /// <param name="fullFileName">json文件路径</param>
        /// <returns></returns>
        public static bool WriteDataToJsonFile<T>(T data, string fullFileName)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(fullFileName))
                {
                    JsonWriter jsonWriter = new JsonTextWriter(writer);
                    new JsonSerializer().Serialize(jsonWriter, data);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 将数据序保存到本地
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static bool SaveObject<T>(string path, T obj) where T : ISerializable
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
#pragma warning disable SYSLIB0011
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
#pragma warning restore SYSLIB0011

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 从本地读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T ReadObject<T>(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return default(T);
                }
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
#pragma warning disable SYSLIB0011
                    IFormatter formatter = new BinaryFormatter();
                    T myObj = (T)formatter.Deserialize(stream);
                    return myObj;
#pragma warning disable SYSLIB0011
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 将一个字符串内容写入到指定的目录文件中
        /// </summary>
        /// <param name="path">要写入的目录包括文件名</param>
        /// <param name="content">要写入的内容</param>
        /// <param name="append">是否要追加还是覆盖</param>
        public static void SaveText(string path, string content, bool append = false)
        {
            if (append)
            {
                //追加到原来内容的后面
                File.AppendAllText(path, content);
            }
            else
            {
                //覆盖原来的内容
                File.WriteAllText(path, content);
            }
        }
        /// <summary>
        /// 从指定的目录中读取文件内容
        /// </summary>
        /// <param name="path">要读取的目录</param>
        /// <returns></returns>
        public static string ReadText(string path)
        {
            string str = File.ReadAllText(path);
            return str;
        }
    }
}
