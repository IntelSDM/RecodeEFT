using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using EFT.Ballistics;

namespace Hag.Helpers
{
    class RaycastHelper
    {
        private static RaycastHit RaycastHit;
        private static readonly LayerMask Mask = 1 << 12 | 1 << 16 | 1 << 18 | 1 << 31 | 1 << 22;

        public static bool Penetrate(BallisticCollider collider)
        {
            // File.WriteAllText("test.txt", collider.PenetrationLevel.ToString());
            collider.PenetrationChance = 100000;
            Globals.LocalPlayerWeapon.CurrentAmmoTemplate.PenetrationChance = 100000;
            if (collider.PenetrationChance >= 1E-45f && Globals.LocalPlayerWeapon.CurrentAmmoTemplate.PenetrationPower > collider.PenetrationLevel)
            {

                return true;
            }
            return false;
        }
        public static bool IsPointVisible(Player player, Vector3 BonePos)
        {

                return (Physics.Linecast(
                        Globals.LocalPlayer.PlayerBones.Head.position,
                        BonePos,
                        out RaycastHit,
                        Mask) && RaycastHit.collider && RaycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject);
            
        }
        public static bool IsPointVisible(Vector3 Startpos ,Player player, Vector3 BonePos)
        {

            if (Physics.Linecast(
                    Startpos,
                    BonePos,
                    out RaycastHit,
                    Mask) && RaycastHit.collider && RaycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject)
                return true;

         
            return false;

        }
        public static bool IsPointVisible(Player player, Vector3 BonePos,Grenade grenade)
        {

            return (Physics.Linecast(
                    grenade.transform.position,
                    BonePos,
                    out RaycastHit,
                    Mask) && RaycastHit.collider && RaycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject);

        }
        public static Vector3 BarrelRaycast()
        {
            try
            {
                if (Globals.LocalPlayer == null)
                    return Vector3.zero;
                if (Globals.LocalPlayer.Fireport == null)
                    return Vector3.zero;

                Physics.Linecast(
                    Globals.LocalPlayer.Fireport.position,
                    Globals.LocalPlayer.Fireport.position - Globals.LocalPlayer.Fireport.up * 1000f,
                    out RaycastHit,
                    Mask);

                return RaycastHit.point;
            }
            catch
            {
                return Vector3.zero;
            }
        }
    }
}
