using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Attribute
{
    public class DisplayNameAttribute:System.Attribute
    {
        public string DisplayName { get;  set; }
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
