using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using UnityEngine;
namespace Hag.SDK
{
    class ExfilPoints
    {
        public ExfilPoints(ExfiltrationPoint exfil)
        {
            ExfilPoint = exfil;
        }
        public int Distance;
        public Vector3 W2SPos;
        public bool W2Sed;
        public ExfiltrationPoint ExfilPoint;
    }
}
