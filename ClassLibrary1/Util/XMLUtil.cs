using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ClassLibrary1.Util
{
    internal static class XMLUtil
    {
        /// <summary>
        /// 读取xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static object Read(string filePath,Type targetType)
        {
            XmlSerializer serializer = new XmlSerializer(targetType);
            using (StreamReader reader = new StreamReader(filePath))
            {
                return serializer.Deserialize(reader);
            }
        }
        /// <summary>
        /// 将指定对象写入XML文件
        /// </summary>
        /// <typeparam name="T">写入对象的类型</typeparam>
        /// <param name="filePath">文件位置</param>
        /// <param name="writeInstan">要写入的对象</param>
        public static void Write<T>(string filePath,T writeInstan)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string content = string.Empty;
            //序列化成字符串
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, writeInstan);
                content = writer.ToString();
            }
            //保存到文件
            using (StreamWriter stream_writer = new StreamWriter(filePath))
            {
                try
                {
                    stream_writer.Write(content);
                }
                catch (ObjectDisposedException)
                {
                    throw new IOException("写入失败");
                }
                catch (NotSupportedException)
                {
                    throw new IOException("写入失败");
                }
                catch (IOException)
                {
                    throw new IOException("写入失败");
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
