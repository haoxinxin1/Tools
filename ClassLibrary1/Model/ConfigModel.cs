using ClassLibrary1.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model
{
    public class ConfigModel
    {
        [DisplayName("保存结果")]
        public bool IsSaveResult { get; set; }//保存结果
        [DisplayName("保存路径")]
        public string SavePath { get; set; }//保存路径 
        [DisplayName("文件后缀")]
        public string Suffix { get; set; }//文件后缀
        [DisplayName("单元间距")]
        public int SpaceLeng { get; set; }
        [DisplayName("排序属性")]
        [References(typeof(Repetition))]
        public string OrderPropName { get; set; }
    }
}
