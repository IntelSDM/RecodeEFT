using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hag.Configs
{
    class AimbotTarget
    {
        public bool Enable = false;
        public int Bone = 1;
        public bool IgnoreFov = false;
        public bool Manipulation = true;
        public int MaxDistance = 450;
        public int MinDistance = 0;
        public float ManipAmount = 2.5f;
        public float AimConeAmount = 0;
        public int Hitchance = 100;
        public bool Prediction = false;
    }
}
