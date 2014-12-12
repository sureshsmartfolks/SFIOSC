using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfhostSample.Messaging
{
    public class TranslationRequestDTO
    {
        public string Request { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public string[] MetaData { get; set; }
    }
}
