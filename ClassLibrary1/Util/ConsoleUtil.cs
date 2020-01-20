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
    public static class ConsoleUtil
    {


        private readonly static List<Model.OpctionModel> opctionModels;
        /// <summary>
        /// 操作对象的初始化
        /// </summary>
        static ConsoleUtil()
        {
            opctionModels = new List<OpctionModel>();
            opctionModels.Add(new OpctionModel
            {
                Code = "HELP",
                Description = "帮助",
                IsGotoStart = true,
                Action = () =>
                {
                    var sb = new StringBuilder();
                    foreach (var item in opctionModels.OrderBy(nameof(OpctionModel.Code)))
                    {
                        sb.Append((item.Code + ":").LeftAligning(20));
                        sb.Append(item.Description.LeftAligning(20));
                        sb.Append("\n");
                    }
                    Console.Write(sb);
                },

            });
            opctionModels.Add(new OpctionModel
            {
                Code = "CLS",
                Description = "清理屏幕",
                IsGotoStart = true,
                Action = () =>
                {
                    Console.Clear();
                },
            });

            opctionModels.Add(new OpctionModel
            {
                Code = "EDIT",
                Description = "修改配置",
                IsGotoStart = true,
                Action = () =>
                {
                    ConfigUtil.configModel = ConfigUtil.UpdateConfig();
                },
            });
            opctionModels.Add(new OpctionModel
            {
                Code = "DEL",
                Description = "删除生成的记录文件",
                IsGotoStart = true,
                Action = () =>
                {
                    if (File.Exists(ConfigUtil.configModel.SavePath))
                    {
                        File.Delete(ConfigUtil.configModel.SavePath);
                        Console.WriteLine("文件已删除");
                    }
                    else
                        Console.WriteLine("文件不存在");
                },
            });
        }
        /// <summary>
        /// 读取用户输入的码 并作出某些操作
        /// </summary>
        /// <returns></returns>
        public static string Read(ReadMode readMode = 0)
        {
        Start:
            if (ReadMode.SaveEdit == readMode)
            {
                Console.WriteLine("输入要保存的值：");
            }
            else if (ReadMode.FolderPath == readMode)
            {
                Console.WriteLine("输入相应的操作：");
            }
            else if (ReadMode.Select == readMode)
            {
                Console.WriteLine("输入编辑项：");
            }

            var str = string.Empty;
            while (true)
            {
                var s = Console.ReadLine();
                // //D:\QMDownload\SoftMgr   或者../../QMDownload\SoftMgr   
                // -------------------空白  ( C-Z 盘或者../开头)------ '/' 路径分隔符 --有效字符和空白字符        
                if (s == null) continue;
                if (!Regex.IsMatch(s, "^\\s*(([C-Z]):|\\.\\.(\\\\|/))((\\\\{1,3}|/{1,3})[\\w \\s]*?)+"))//如果匹配到路径格式的话就直接越过以下循环
                    foreach (var item in opctionModels.OrderBy(i => i.Code))
                    {
                        if (item.Code.Equals(s, StringComparison.OrdinalIgnoreCase) && item.IsGotoStart)
                        {
                            item.Action.Invoke();
                            goto Start;
                        }
                    }
                str += s;
                if (readMode == ReadMode.SaveEdit || readMode == ReadMode.Select) break;
                if (ReadMode.FolderPath == readMode && (s == string.Empty)) break;
            }
            return str;
        }

        
        public static string Read(Func<StringBuilder, StringBuilder> func)
        {
            Action<StringBuilder> resetLine = (s) =>
            {
                Console.WriteLine(" ");
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.Write(s);
            };
            var sb = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey();
                
                if(key.Key == ConsoleKey.Tab)
                {
                    var str = func(sb);
                    resetLine(sb);
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        sb = sb.Remove(sb.Length - 1, 1);
                        resetLine(sb);
                    }
                  
                }else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }else if(key.Key.ToString().Length==1)
                {
                    sb.Append(key.KeyChar);
                }
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
            return sb.ToString();

        }
    }
    public enum ReadMode
    {
        FolderPath, Select, SaveEdit
    }
}
