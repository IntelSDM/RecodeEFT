using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class EspHealthbar
    {
        public EspHealthbar(bool enabled,int alignment,int maxdistance)
        {
            Enable = enabled;
            Alignment = alignment;
            MaxDistance = maxdistance;
        }
        public bool Enable;
       public int Alignment;
        public int MaxDistance;
    }
}
