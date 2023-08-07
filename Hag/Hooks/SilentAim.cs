using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Collections;
using Hag.Renderer;
using EFT.InventoryLogic;
using EFT.Interactive;
using System.Threading;
using EFT.UI;
using System.Reflection;
using System.IO;
using Hag.Helpers;

namespace Hag.Hooks
{
    class SilentAim : MonoBehaviour
    {
        private static DumbHook Hook;
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            Hook = new DumbHook();
            Hook.Init(typeof(EFT.Ballistics.BallisticsCalculator).GetMethod("CreateShot", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public), typeof(SilentAim).GetMethod("Func", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public));
            Hook.Hook();
        }
        [ObfuscationAttribute(Exclude = true)] 
        public object Func(object ammo, Vector3 origin, Vector3 direction, int fireIndex, Player player, Item weapon, float speedFactor = 1f, int fragmentIndex = 0)
        {
            return CreateShotHook(ammo, origin, direction, fireIndex, player, weapon, speedFactor, fragmentIndex);
        }
        public object CreateShotHook(object ammo, Vector3 origin, Vector3 direction, int fireIndex, Player player, Item weapon, float speedFactor = 1f, int fragmentIndex = 0)
        {
            if (Globals.LocalPlayer == null || Globals.LocalPlayerWeapon == null)
                goto EndHook;
            if (Aimbot.Aimbot.TargetPlayers == null)
                goto EndHook;
            if (!Aimbot.TargetPlayer.IsTargetSuitable)
                goto EndHook;

            Configs.Weapon weaponinstance = Misc.WeaponMods.GetSpeedFactorInstance(Globals.LocalPlayerWeapon.TemplateId);
            if (weaponinstance != null) // if no speed multipliers are active then it will return null
            {
                speedFactor = weaponinstance.HitSpeedAmount;
            }

            Configs.Aimbot aimbotinstance = Aimbot.Aimbot.GetInstance(Globals.LocalPlayerWeapon.TemplateId);
            if (aimbotinstance == null)
                goto EndHook;
             
            

            Configs.AimbotTarget targetinstance = aimbotinstance.AimbotTargetPlayer;
            if (!aimbotinstance.AutoShoot && !Aimbot.Aimbot.KeyPressed && aimbotinstance.Key != KeyCode.Mouse0)
                goto EndHook;
            if (aimbotinstance.Key == KeyCode.Mouse0 && !(Input.GetKey(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse0) && !aimbotinstance.AutoShoot))
                goto EndHook;
            if (Aimbot.Aimbot.TargetPlayers.PlayerType == SDK.PlayerType.Boss)
                targetinstance = aimbotinstance.AimbotTargetBoss;
            if (Aimbot.Aimbot.TargetPlayers.PlayerType == SDK.PlayerType.Scav)
                targetinstance = aimbotinstance.AimbotTargetScav;
            if (Aimbot.Aimbot.TargetPlayers.PlayerType == SDK.PlayerType.ScavPlayer)
                targetinstance = aimbotinstance.AimbotTargetScavPlayer;
            if (Aimbot.Aimbot.TargetPlayers.PlayerType == SDK.PlayerType.Player)
                targetinstance = aimbotinstance.AimbotTargetPlayer;

            System.Random hitchancerand = new System.Random();
            if (hitchancerand.Next(0, 100) > targetinstance.Hitchance)
                goto EndHook;
   //         if (Aimbot.TargetPlayer.RequiresManipulation)
         //       origin = Aimbot.TargetPlayer.ManipulationAngle;
            direction = (Aimbot.Aimbot.TargetPlayers.TargetPos - origin).normalized;

            if (targetinstance.AimConeAmount > 0)
            {
                Vector2 randomPoint = UnityEngine.Random.insideUnitCircle.normalized;
                float angle = UnityEngine.Random.Range(0f, targetinstance.AimConeAmount);

                direction += Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles;
            }
            EndHook:
            Hook.Unhook();
            object[] parameters = new object[]
               {
                    ammo,
                    origin,
                    direction,
                    fireIndex,
                    player,
                    weapon,
                    speedFactor,
                    fragmentIndex
               };
            object result = Hook.OriginalMethod.Invoke(this, parameters);

            Hook.Hook();
            return result;
        }
    }
}
