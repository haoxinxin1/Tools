using ClassLibrary1.Attribute;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model
{
    /// <summary>
    /// 一个对象 表述的是某一个前台框架会被哪些页面引用
    /// </summary>
   public class Repetition
    {
        [DisplayName("框架名称")]
        public string FrameName { get; set; }
        [DisplayName("框架引用数量")]
        public int Count { get; set; }
        [DisplayName("文件名称")]
        public List<string> FileName { get; set; }
    }
}
