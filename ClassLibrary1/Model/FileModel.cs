using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Model
{
    public class FileModel
    {
        public string FilePath { get; set; }
        public List<string> Links { get; set; }
        public List<string> Scripts { get; set; }
        public FileModel(string filePath)
        {
            this.FilePath = filePath;
            Links = new List<string>();
            Scripts= new List<string>();
        }

    }
}
