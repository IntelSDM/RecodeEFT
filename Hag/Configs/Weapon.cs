using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Weapon
    {
        public Weapon(bool recoil,bool sway)
        {
            NoRecoil = recoil;
            NoSway = sway;
        }
        public bool NoRecoil;
        public int Recoilx = 1;
        public int Recoily = 1;

        public bool NoSway = true;
        public int SwayAmount;

        public bool UnlockFireModes = true;

        public bool NoMalfunction = true;
        public int MalfunctionChance = 100;

        public bool FastFire;
        public int FastFireAmount;

        public bool HitSpeedMultiplier = false;
        public float HitSpeedAmount = 2.2f;
    }
}
