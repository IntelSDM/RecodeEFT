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
    class Grenades
    {
        public Grenades(Grenade nade)
        {
            Grenade = nade;
            StartTime = Time.time;
        }
        public int Distance;
        public Vector3 W2SPos;
        public Grenade Grenade;
        public float StartTime;
        public List<SDK.Players> VulnerablePlayerList;
    }
}
