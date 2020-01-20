using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary1.Util
{
    public static class FileUtil
    {
        /// <summary>
        /// 转换为绝对路径  D:
        /// </summary>
        /// <param name="relaivePath"></param>
        /// <returns></returns>
        public static string AbsolutePath(string rootPath, string relaivePath)
        {
            Directory.SetCurrentDirectory(rootPath);
            return Path.GetFullPath(relaivePath);
        }
        /// <summary>
        /// 转换为相对路径  ../
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string RelativePath(string rootPath, string fullPath)
        {
            //from - www.cnphp6.com

            string[] absoluteDirectories = rootPath.Split('\\');
            string[] relativeDirectories = fullPath.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                throw new ArgumentException("Paths do not have a common base");

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                if (absoluteDirectories[index].Length > 0)
                    relativePath.Append("..\\");

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                relativePath.Append(relativeDirectories[index] + "\\");
            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }
        /// <summary>
        /// 获取指定目录下所有的指定文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extension">后缀</param>
        /// <returns></returns>
        public static List<string> GetAllFile(string path, string extension)
        {
            try
            {
                var result = new List<string>();
                ReadFile(path, result, extension);

                return result;
            }
            catch (DirectoryNotFoundException)
            {
                throw new ArgumentException("文件路径不存在");
            }
            catch
            {

                throw;
            }
        }
        private static List<Task> ReadFile(string path, List<string> result, string extension)
        {
            List<Task> taskList = new List<Task>();
            string[] dirs = null;
            try
            {
                dirs = Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException)
            {

            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            if (dirs != null)
                foreach (var dir in dirs)
                {
                    var task = Task.Run(() =>
                    {
                        taskList.AddRange(ReadFile(dir, result, extension));
                    });
                    //task.Wait();
                    if (task != null)
                    {
                        taskList.Add(task);

                    }
                }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException)
            {

            }
            catch (Exception)
            {

                throw;
            }
            if (files != null)
                foreach (var file in files)
                {
                    if (extension == null || Regex.IsMatch(file, (extension.Contains(".") ? extension : "." + extension) + "$"))
                    {
                        result.Add(file);
                    }
                }
            Task.WaitAll(taskList.ToArray());
            return taskList;
        }

        //根据文件路径得到文件所引用的js css文件列表
        public static async Task<Model.FileModel> ReadFile(string filePath)
        {
            var file = new Model.FileModel(filePath);
            var stream = new StreamReader(filePath);
            var line = await stream.ReadLineAsync();
            do
            {
                if (Regex.IsMatch(line, "< *?link.*?href=.*?>"))
                {
                    file.Links.Add(line);
                }
                else if (Regex.IsMatch(line, "< *?script.*?src.*?>"))
                {
                    file.Scripts.Add(line);
                }
                line = await stream.ReadLineAsync();
            } while (line!=null);
            return file;
        }
    }
}
