using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Util
{
    public static class StringUtil
    {
        /// <summary>
        /// 将字符串用空格填充至charWidth个长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charWidth"></param>
        /// <returns></returns>
        public static string LeftAligning(this string str, int charWidth)
        {
            var format = "{0,-" + charWidth + "}";
            return String.Format(format, str);
        }
        /// <summary>
        /// stringBuillder的追加方法 传入的对象会调用ToString()
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string Append(this StringBuilder sb,params object[] arr)
        {
            foreach (var item in arr)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }
    }
}
