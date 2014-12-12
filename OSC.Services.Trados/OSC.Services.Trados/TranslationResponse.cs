using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfhostSample
{
    public class TranslationResponse
    {
        public String Translation { get; set; }
        public int Score { get; set; }
        public string Culture { get; set; }
    }
}
