using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Attribute
{
    /// <summary>
    /// 描述的是特性在属性下的输出和存储行为
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AttributeBind<T>: IAttributeBind
    {
        /// <summary>
        /// 某个子类设置对应的特性如何去存储
        /// </summary>
        protected abstract Action<PropertyInfo, object> OptionInvork { get; } 

        /// <summary>
        /// 某个子类设置对应的特性如何去显示
        /// </summary>
        public abstract PropertyInfo BindDisplay(PropertyInfo property);
        public  void SetValue(PropertyInfo propertyInfo,object value)
        {
            if (propertyInfo.GetCustomAttribute(typeof(T)) == null) return;
            OptionInvork(propertyInfo, value);
        }
    }
    interface IAttributeBind
    {
        void SetValue(PropertyInfo propertyInfo, object value);
        PropertyInfo BindDisplay(PropertyInfo property);
    }
    

}
