#define IsTask 
using ClassLibrary1.Model;
using ClassLibrary1.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Run
    {
        static async Task Main()
        {
            //加载配置文件
            ConfigUtil.Load();

            #region 测试
            

            #endregion
            while (true)
            {
                var s = ConsoleUtil.Read(ReadMode.FolderPath);//读取用户输入的路径
                var files=FileUtil.GetAllFile(s, ".html");
                var fileModels = new List<FileModel>();
                foreach (var item in files)
                {
                    var r = await FileUtil.ReadFile(item);
                    fileModels.Add(r);
                }


                //var s= ConsoleUtil.Read(i=>i.Append(".html"));
                //  Console.WriteLine(s);
                //  var key2 = Console.ReadKey();
            }
        }
        
    }
}
