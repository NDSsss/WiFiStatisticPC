using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseFile
{
    class ObserverPair
    {
        public string observer1 { get; set; }
        public string observer2 { get; set; }
        public int from1to2 { get; set; }
        public List<PairPoint> pairPoints { get; set; }

    }
}
