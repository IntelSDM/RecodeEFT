using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Item
    {
        public bool Enable = true;
        public int MinPrice = 50000;
        public int MaxDistance = 500;
        public int CullOpacity = 1;
       public EspText Name = new EspText(true, 1, 1);
        public EspText ItemType = new EspText(true, 1, 2);
        public EspText ItemSubType = new EspText(true, 1, 2);
        public EspText Distance = new EspText(true, 1, 1);
        public EspText Value = new EspText(true, 1, 1);
        public bool EnableEspInBattleMode = false;
        public bool UseItemSlotPrice = false;
    }
}
