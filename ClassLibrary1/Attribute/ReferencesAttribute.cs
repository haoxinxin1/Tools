using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Attribute
{
    public class ReferencesAttribute :System.Attribute
    {
        public Type References { get; set; }
        public ReferencesAttribute(Type className)
        {
            this.References = className;
        }
    }
}
