using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class EspPlayerBox
    {
        public EspPlayerBox(bool enable, int maxdist, bool fill, bool vischeck)
        {
            Enable = enable;
            MaxDistance = maxdist;
            Fill = fill;
            VisibilityCheck = vischeck;
        }
        public bool Enable;
        public int MaxDistance;
        public bool Fill;
        public bool VisibilityCheck;
    }
}
