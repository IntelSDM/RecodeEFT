using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class EspSkeleton
    {
        public EspSkeleton(bool enable, int type, int maxdistance)
        {
            Enable = enable;
            Type = type;
            MaxDistance = maxdistance;
        }
        public bool Enable;
        public int Type;
        public int MaxDistance;
    }
}
