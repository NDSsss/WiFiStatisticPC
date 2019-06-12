using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseFile
{
    class Area
    {
        public List<ObserverPoint> observerPoints { get; set; }
        public List<PairedPointsForDraw> pointsForDraw { get; set; }
        public int minMeasures { get; set; }
    }
}
