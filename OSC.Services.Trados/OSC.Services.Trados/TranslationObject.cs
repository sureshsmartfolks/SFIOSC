using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfhostSample
{
    public class TranslationObject
    {
        public string SourceSegment { get; set; }
        public string TranslatedSegment { get; set; }
        public int score { get; set; }
    }

    public class TranslationFile
    {
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public string SourceFileName { get; set; }
        public List<TranslationObject> SegmentList { get; set; }
    }
}
