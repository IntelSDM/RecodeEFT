using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using EFT.Interactive;
using System.Collections;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using Hag.Helpers;
using System.Reflection;
namespace Hag.Misc
{
    class Movement : MonoBehaviour
    {
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(Move());
        }
        
        IEnumerator Move()
        {
            for (; ; )
            {
                if (Globals.LocalPlayer == null || Globals.GameWorld == null)
                    goto END;
                if (Globals.Config.Movement.RunAndShoot)
                {

                    EFTHardSettings.Instance.UnsuitableStates = new EPlayerState[0];
                }
                    System.Random rand = new System.Random();
                    if (rand.Next(0, 100) > Globals.Config.Movement.ChanceOfRefill)
                    {
                        if (Globals.Config.Movement.TimeTillRefill > 0)
                            yield return new WaitForSeconds(Globals.Config.Movement.TimeTillRefill);
                        else
                            yield return new WaitForEndOfFrame();
                    }
                    if (Globals.Config.Movement.UnlimitedStamina)
                    {
                        Globals.LocalPlayer.Physical.HandsStamina.Current = Globals.Config.Movement.RefillAmount;
                        Globals.LocalPlayer.Physical.Stamina.Current = Globals.Config.Movement.RefillAmount;
                    }
                if (Globals.Config.Movement.NoInertia)
                {
                    Globals.LocalPlayer.Physical.Inertia = 0;
                    //        Globals.LocalPlayer.MovementContext.InertiaSettings.MoveTime = 0;
                    //EFTHardSettings.Instance.MOVEMENT_MASK
                    //      Globals.LocalPlayer.MovementContext.InertiaSettings.
                    Globals.LocalPlayer.MovementContext.InertiaSettings.MinDirectionBlendTime = 0;
                    Globals.LocalPlayer.MovementContext.InertiaSettings.PenaltyPower = 0;
                    Globals.LocalPlayer.MovementContext.InertiaSettings.BaseJumpPenalty = 0;
                    Globals.LocalPlayer.MovementContext.InertiaSettings.DurationPower = 0;
                    Globals.LocalPlayer.MovementContext.InertiaSettings.BaseJumpPenaltyDuration = 0;
                    Globals.LocalPlayer.MovementContext.InertiaSettings.FallThreshold = float.MaxValue;

                }
                if (Globals.Config.Movement.TimeTillRefill > 0)
                    yield return new WaitForSeconds(Globals.Config.Movement.TimeTillRefill);
                else
                    yield return new WaitForEndOfFrame();
                END:
                    yield return new WaitForEndOfFrame();
            }
        }
    }
}
