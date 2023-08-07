using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class QuestItem
    {
        public bool Enable = true;
        public int MaxDistance = 500;
        public bool CullOpacityWithDistance = true;
        public EspText Name = new EspText(true, 1, 1);
        public EspText Distance = new EspText(true, 1, 1);
        public bool EnableEspInBattleMode = false;
        public bool ShowInactiveQuestItems = true;
        
    }
}
