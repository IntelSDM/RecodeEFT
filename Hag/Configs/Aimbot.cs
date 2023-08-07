using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class Aimbot
    {
        public bool Enable = false;
        public bool DrawFov = true;
        public int Fov = 200;
        public bool TargetLine = true;
        public int FovType = 3;
        public bool AutoShoot = false;
        public UnityEngine.KeyCode Key = UnityEngine.KeyCode.Mouse3;

       public AimbotTargetting AimbotTargetting = new AimbotTargetting();
        public AimbotTarget AimbotTargetPlayer = new AimbotTarget();
        public AimbotTarget AimbotTargetScav = new AimbotTarget();
        public AimbotTarget AimbotTargetScavPlayer = new AimbotTarget();
        public AimbotTarget AimbotTargetBoss = new AimbotTarget();
    }
}
