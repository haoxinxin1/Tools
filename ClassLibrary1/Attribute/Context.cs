using ClassLibrary1.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Attribute
{
    public class Context
    {
        private static Dictionary<Type, IAttributeBind> dirs;
        //当需要增加新功能时这里只需要往集合里添加一下就行
        static Context()
        {
            if (dirs == null)
                dirs = new Dictionary<Type, IAttributeBind>();
            dirs.Add(typeof(DisplayNameAttribute),new DisplayNameAttributeBind());
            dirs.Add(typeof(ReferencesAttribute),new ReferencesAttributeBind());
        }
        public void SetValue(PropertyInfo propertyInfo, object value)
        {
            foreach (var item in dirs)
            {
                item.Value.SetValue(propertyInfo, value);
            }
        }
        public void DisPaly()
        {
            if (dirs.Count <= 0) return;
            var display = dirs.FirstOrDefault().Value;
            var selectProp1= display.BindDisplay(null);
            var attrs = selectProp1.GetCustomAttributes().Where(i=>i.GetType()!=typeof(DisplayNameAttribute));
            if (attrs.Count() == 0)
            {
                display.SetValue(selectProp1, ConsoleUtil.Read(ReadMode.SaveEdit));
            }
            else
            {
                for (int i = 1; i < dirs.Count; i++)
                {
                    if (attrs.ElementAt(0).GetType() == dirs.ElementAt(i).Key)
                    {
                        var selectPropw2 = dirs.ElementAt(i).Value.BindDisplay(selectProp1);
                        //把选择的Repetition的属性名称 赋值给配置对象 然后配置对象决定使用哪个属性去排序
                        dirs.ElementAt(i).Value.SetValue(selectProp1,selectPropw2.Name);
                    }
                }
              
            }
            
          

        }
    }
}
