using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC.BusinessObjects.FileTransfer
{
    public class FileObject
    {
        public string fileid { get; set; }
        public DateTime uploaddate { get; set; }
        public string filepath { get; set; }
        public string filename { get; set; }
        public string uploadby { get; set; }
        public long filesize { get; set; }
        public int dbid { get; set; }
        public string _type { get; set; }
    }
}
