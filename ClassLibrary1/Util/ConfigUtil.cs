using ClassLibrary1.Attribute;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary1.Util
{
    public static class ConfigUtil
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static readonly string CONFIGURL = "./conf.xml";
        public static ConfigModel configModel;
        /// <summary>
        /// 修改配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ConfigModel UpdateConfig()
        {
            var contxt = new Context();

            contxt.DisPaly();
            return configModel;
        }
        static ConfigUtil()
        {
            //首先先判断配置文件是相对路径还是绝对路径  相对路径的话修改为绝对路径
            if (Regex.IsMatch(CONFIGURL, "^\\."))
            {
                CONFIGURL = FileUtil.AbsolutePath(AppDomain.CurrentDomain.BaseDirectory, CONFIGURL);
            }
        }
        public static void  Load()
        {
            //不存在则创建xml配置文件
            if (!File.Exists(CONFIGURL))
            {
                var stream = File.Create(CONFIGURL);
                stream.Close(); stream.Dispose();
                configModel= new ConfigModel
                {
                    IsSaveResult = true,
                    SavePath = @"C:\Users\haoxin\Desktop\layout.log",
                    SpaceLeng = 20,
                    Suffix = ".html",
                    OrderPropName = nameof(Repetition.Count)
                };
                XMLUtil.Write(CONFIGURL, configModel);
            }
            //存在则读取xml配置文件
            else
            {
               configModel=(ConfigModel) XMLUtil.Read(CONFIGURL, typeof(ConfigModel));
            }
        }
    }
}
