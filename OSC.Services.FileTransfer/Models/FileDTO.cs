using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSC.Services.FileTransfer.Models
{
    public class FileDTO
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