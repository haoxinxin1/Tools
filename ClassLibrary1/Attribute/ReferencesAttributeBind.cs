using ClassLibrary1.Model;
using ClassLibrary1.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Attribute
{
    public class ReferencesAttributeBind : AttributeBind<ReferencesAttribute>
    {
        protected override Action<PropertyInfo, object> OptionInvork { get; } = (p, o) =>
        {
            p.SetValue(ConfigUtil.configModel, o);
        };

        public override PropertyInfo BindDisplay(PropertyInfo property)
        {
        Start:
            var sb = new StringBuilder();
            sb.Append("选择排序依据的属性：\n");
            var disAttr = property.GetCustomAttribute(typeof(ReferencesAttribute)) as ReferencesAttribute;
            var props = disAttr.References.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var display = props[i].GetCustomAttribute(typeof(DisplayNameAttribute), false) as DisplayNameAttribute;
                sb.Append((i + 1).ToString());
                sb.Append("、");
                if (display != null)
                {
                    sb.Append(display.DisplayName);
                }
                sb.Append("   ");
                
            }
            Console.WriteLine(sb);
            if (!int.TryParse(ConsoleUtil.Read(ReadMode.SaveEdit), out var selectIndex))
            {
                goto Start;
            }
            return props[selectIndex - 1];
        }
    }
}
