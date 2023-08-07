using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Weapons
    {
        public Weapon DefaultSniperRifle = new Weapon(true,true);
        public Weapon DefaultMarksmanRifle = new Weapon(true, true);
        public Weapon DefaultAssaultRifle = new Weapon(true, true);
        public Weapon DefaultAssaultCarbine = new Weapon(true, true);
        public Weapon DefaultMachineGun = new Weapon(true, true);
        public Weapon DefaultShotgun = new Weapon(true, true);
        public Weapon DefaultPistol = new Weapon(true, true);
        public Weapon DefaultRevolver = new Weapon(true, true);
        public Weapon DefaultSMG = new Weapon(true, true);

        public Dictionary<string, Weapon> WeaponDict = new Dictionary<string, Weapon>();
    }
}
