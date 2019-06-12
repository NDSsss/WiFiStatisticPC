using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseFile
{
    class PairedPointsForDraw
    {
        public int pointOnePosX { get; set; }
        public int pointOnePosY { get; set; }
        public int pointTwoPosX { get; set; }
        public int pointTwoPosY { get; set; }
        public List<List<CommonPoint>> measuresOfCommonPoints { get; set; }
    }
}
