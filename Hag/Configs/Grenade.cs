using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Grenade
    {
        public bool Enable = true;
        public int MaxDistance = 500;
        public EspText Name = new EspText(true, 1, 1);
        public EspText Distance = new EspText(true, 1, 1);
        public EspText ThrowerName = new EspText(true, 1, 2);
        public EspHealthbar Durationbar = new EspHealthbar(true, 4, 150);
        public bool CullOpacityWithDistance = true;
        public bool EnableEspInBattleMode = false;
        public bool GrenadeList = true;
        public int GrenadeAlignment = 1;
        public bool GrenadeHitBoxLine = true;
    }
}
