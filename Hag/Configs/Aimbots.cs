using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Aimbots
    {
        public Aimbot DefaultSniperRifle = new Aimbot();
        public Aimbot DefaultMarksmanRifle = new Aimbot();
        public Aimbot DefaultAssaultRifle = new Aimbot();
        public Aimbot DefaultAssaultCarbine = new Aimbot();
        public Aimbot DefaultMachineGun = new Aimbot();
        public Aimbot DefaultShotgun = new Aimbot();
        public Aimbot DefaultPistol = new Aimbot();
        public Aimbot DefaultRevolver = new Aimbot();
        public Aimbot DefaultSMG = new Aimbot();
        public PredictionCrosshair PredictionCrosshair = new PredictionCrosshair();
        public Dictionary<string, Aimbot> WeaponDict = new Dictionary<string, Aimbot>();
    }
}
