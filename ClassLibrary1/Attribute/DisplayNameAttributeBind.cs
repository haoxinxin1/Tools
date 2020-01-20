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
    public class DisplayNameAttributeBind : AttributeBind<DisplayNameAttribute>
    {
        protected override Action<PropertyInfo, object> OptionInvork { get; } = (p, v) =>
        {
            if (p.PropertyType == typeof(bool))
            {
                if (bool.TryParse(v.ToString(), out var boolresult))
                {
                    p.SetValue(ConfigUtil.configModel, (bool)v);
                }
                else if (v.ToString() == "1")
                {
                    p.SetValue(ConfigUtil.configModel, true);
                }
            }
            else
            {
                p.SetValue(ConfigUtil.configModel, v);
            }
        };

        public override PropertyInfo BindDisplay(PropertyInfo property)
        {
        contineConfNum:
            var sb = new StringBuilder();
            sb.Append("选择修改的配置序号\n");
            var props = typeof(ConfigModel).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var dis1 = (DisplayNameAttribute)(props[i].GetCustomAttributes(typeof(DisplayNameAttribute), false)[0]);
                sb.Append((i + 1).ToString(), "、", dis1.DisplayName, "\t");
                var value = props[i].GetValue(ConfigUtil.configModel);
                if (props[i].PropertyType == typeof(bool))
                {
                    sb.Append((bool)value ? "是" : "否");
                }
                else
                {
                    sb.Append(value);
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb);
            if (!int.TryParse(ConsoleUtil.Read(ReadMode.Select), out var confNum))
            {
                goto contineConfNum;
            }
            var selectedProperty = props[confNum - 1];
            var sb2 = new StringBuilder();
            sb2.Append("修改项：【", confNum.ToString() + ".");

            var isbool = selectedProperty.PropertyType == typeof(bool);
            var isint = selectedProperty.PropertyType == typeof(int);
            var dispalyArr = (selectedProperty.GetCustomAttributes(typeof(DisplayNameAttribute), false));
            if (dispalyArr.Length > 0)
            {
                var dis = (DisplayNameAttribute)dispalyArr[0];
                sb2.Append(dis.DisplayName);
                sb2.Append(":");
                sb2.Append("】");
            }
            Console.WriteLine(sb2);
            return selectedProperty;
        }
    }
}
