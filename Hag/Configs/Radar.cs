using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Radar
    {
        public bool Enable = true;
        public bool Background = true;
        public bool Map = false;
        public int Size = 100;
        public int Radarx = UnityEngine.Screen.width - 250;
        public int Radary = UnityEngine.Screen.height - 250;
        public int RadarMaxDistance = 400;
    }
}
