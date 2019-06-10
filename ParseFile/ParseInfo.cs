using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseFile
{
    public class ParseInfo
    {
        public string timeStamp { get; set; }
        public string SSId { get; set; }
        public string BSSID { get; set; }
        public string Strength { get; set; }
        public string primaryChannel { get; set; }
        public string PrimaryFrequency { get; set; }
        public string centerChannel { get; set; }
        public string centerFrequency { get; set; }
        public string Range { get; set; }
        public string Distance { get; set; }
        public string Security { get; set; }
    }
}
