using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Movement
    {
        public bool UnlimitedStamina = true;
        public bool NoInertia = true;
        public int TimeTillRefill = 10;
        public int RefillAmount = 100;
        public int ChanceOfRefill = 100;

        public bool RunAndShoot = true;
    }
}
