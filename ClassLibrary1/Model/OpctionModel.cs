using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model
{
    public class OpctionModel
    {
        /// <summary>
        /// 操作码 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 执行某些操作
        /// </summary>
        public Action Action { get;set; }
        /// <summary>
        /// 是否goto至起点处
        /// </summary>
        public bool IsGotoStart { get; set; }
    }
}
