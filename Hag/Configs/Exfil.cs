using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Exfil
    {
        public bool Enable = true;
        public EspText Name = new EspText(true,1,1);
        public EspText Distance = new EspText(true, 1, 1);
        public int MaxDistance = 1250;
        public bool LimitOpacityByDistance = true;
        public bool EnableEspInBattleMode = false;
    }
}
