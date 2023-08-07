using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using System.Reflection;
using System.Collections;
using Hag.Renderer;
using Hag.Helpers;
using System.Runtime.InteropServices;

namespace Hag.Aimbot
{
    class Aimbot : MonoBehaviour
    {
        public static SDK.Players TargetPlayers;
        public static Vector3 Fireport;
        public static int FOV;
        public static bool KeyPressed = false;

        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(UpdateTargetPlayer());
            StartCoroutine(AutoShoot());
            StartCoroutine(GetKey());
            //  StartCoroutine(UpdateFov());
        }


        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
           
                if (Globals.GameWorld == null)
                    return;
                if (Globals.LocalPlayer == null)
                    return;
                if (Globals.LocalPlayer.Fireport == null)
                    return;
                if (Globals.LocalPlayerWeapon == null)
                    return;

                Configs.Aimbot instance = GetInstance(Globals.LocalPlayerWeapon.TemplateId);
                if (instance == null)
                    return;
                if (instance.DrawFov)
                {
                    Color32 col = ColourHelper.GetColour("Aimbot Fov Colour");

                    if (instance.FovType == 1)
                      {
                       

                        var currentAimingMod = Globals.LocalPlayer.ProceduralWeaponAnimation.CurrentAimingMod;
                        if (currentAimingMod == null)
                            return;

                        var zoom = currentAimingMod.GetCurrentOpticZoom();
                        if (Globals.ScopeCamera != null && zoom > 1 && Globals.LocalPlayer.HandsController.IsAiming)
                              FOV = (int)(instance.Fov / Globals.LocalPlayer.ProceduralWeaponAnimation.CurrentAimingMod.GetCurrentOpticZoom());
                          else
                              FOV = instance.Fov;
                      }
                      if (instance.FovType == 2)
                      {
                  

                        var currentAimingMod = Globals.LocalPlayer.ProceduralWeaponAnimation.CurrentAimingMod;
                        if (currentAimingMod == null)
                            return;

                        var zoom = currentAimingMod.GetCurrentOpticZoom();
                        if (Globals.ScopeCamera != null && zoom > 1 && Globals.LocalPlayer.HandsController.IsAiming)
                              FOV = (int)(instance.Fov * Globals.LocalPlayer.ProceduralWeaponAnimation.CurrentAimingMod.GetCurrentOpticZoom());
                          else
                              FOV = instance.Fov;
                      }
                      if (instance.FovType == 3)
                    FOV = (int)instance.Fov;
                    renderer.DrawCircle(Screen.width / 2, Screen.height / 2, FOV, 1, new Direct2DColor(col.r, col.g, col.b, col.a));

                }
                if (TargetPlayers != null && Esp.EspConstants.IsScreenPointVisible(TargetPlayers.TargetPosScreen) && TargetPlayer.IsTargetSuitable && instance.TargetLine)
                {
                    Color32 col = ColourHelper.GetColour("Aimbot Target Line Colour");
                    renderer.DrawLine(Screen.width / 2, Screen.height / 2, TargetPlayers.TargetPosScreen.x, TargetPlayers.TargetPosScreen.y, 1, new Direct2DColor(col.r, col.g, col.b, col.a));
                }
                //    if(TargetPlayer.RequiresManipulation && Esp.EspConstants.IsScreenPointVisible(TargetPlayer.ManipulationAngleScreen))
                //  renderer.DrawCircle(TargetPlayer.ManipulationAngleScreen.x, TargetPlayer.ManipulationAngleScreen.y, 5, 1, new Direct2DColor(255, 255, 255, 255));

                // if(TargetPosition)

           
            }
        float NextShot = 0;
        IEnumerator AutoShoot()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null)
                    goto END;   
                if (Globals.LocalPlayer == null)
                    goto END;
                if (Globals.LocalPlayerWeapon == null)
                    goto END;
                if (TargetPlayers == null || !TargetPlayer.IsTargetSuitable)
                    goto END;
                Configs.Aimbot instance = GetInstance(Globals.LocalPlayerWeapon.TemplateId);
                if (instance == null)
                    goto END;
                if (!instance.AutoShoot)
                    goto END;
                Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                NextShot = Time.time + 0.064f;
                Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                
                END:
                yield return new WaitForEndOfFrame();
            }
        }
        IEnumerator GetKey()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null)
                    goto END;
                if (Globals.LocalPlayer == null)
                    goto END;
                if (Globals.LocalPlayerWeapon == null)
                    goto END;
                if (TargetPlayers == null || !TargetPlayer.IsTargetSuitable)
                    goto END;
                Configs.Aimbot instance = GetInstance(Globals.LocalPlayerWeapon.TemplateId);
                if (instance == null)
                    goto END;
                KeyPressed = UnityEngine.Input.GetKey(instance.Key) || UnityEngine.Input.GetKeyDown(instance.Key);


                END:
                yield return new WaitForEndOfFrame();
            }
        }
        public static Configs.Aimbot GetInstance(string template)
        {
          
                item_subtype type = ItemPriceHelper.list[template].subtype;
                if (type == item_subtype.AssaultCarbine)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultAssaultCarbine;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.AssaultRifle)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultAssaultRifle;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.MachineGun)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultMachineGun;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.SniperRifle)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultSniperRifle;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.Smg)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultSMG;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.Shotgun)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultShotgun;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.Revolver)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultRevolver;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
                if (type == item_subtype.Pistol)
                {
                    if (Globals.Config.Aimbot.WeaponDict.ContainsKey(template))
                    {
                        Configs.Aimbot instance = Globals.Config.Aimbot.WeaponDict[template];
                        if (instance.Enable)
                            return instance;
                    }
                    Configs.Aimbot categoryinstance = Globals.Config.Aimbot.DefaultPistol;
                    if (categoryinstance.Enable)
                        return categoryinstance;
                }
          
                return null;
        }
        IEnumerator UpdateTargetPlayer()
        {
            for (; ; )
            {
                if (Globals.LocalPlayer == null || Globals.GameWorld == null || Globals.LocalPlayerWeapon == null)
                    goto END;
                TargetPlayer.GetTargetPlayer();
                
                /*   foreach (SDK.Players players in Globals.PlayerDict.Values)
                   {
                       if (players.Player == null || !players.Player.HealthController.IsAlive)
                           continue;
                       Vector3 velocity = Globals.Offline ? players.Player.Velocity : players.Player._characterController.velocity; // offline and online player velocities are different

                   }*/
                /*   if (Input.GetKeyDown(KeyCode.V))
                   {
                       TrajectoryCalculator traj = new TrajectoryCalculator(Vector3.zero, new Vector3(Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.InitialSpeed * Globals.LocalPlayerWeapon.SpeedFactor, 0f, 0f), Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.BulletMassGram, Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.BulletDiameterMilimeters, Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.BallisticCoeficient);
                       float num = 0f;
                       int i = 0;
                       int num2 = traj.MaxAllowedLength - 1;
                       while (i <= num2)
                       {
                           int num3 = (i + num2) / 2;
                           System.IO.File.WriteAllText(traj[num3].position.ToString(), $"{traj[num3].position} | {traj[num3].time} | {traj[num3].velocity} | {traj[num3].index}");
                       }
                   }*/
               /* foreach (SDK.Players players in Globals.PlayerDict.Values)
                {
                    players.TargetPos = Prediction.PredictedPos(players, players.BoneWorld[0]);
                    if (!players.BoneVisible[0])
                        continue;
                    if (Input.GetKey(KeyCode.V))
                    {
                        float ScreenCenterX = (Screen.width / 2);
                        float ScreenCenterY = (Screen.height / 2);
                        float TargetX = 0;
                        float TargetY = 0;
                        float x = players.TargetPosScreen.x;
                        float y = players.TargetPosScreen.y;
                        if (x != 0)
                        {
                            if (x > ScreenCenterX)
                            {
                                TargetX = -(ScreenCenterX - x);

                                if (TargetX + ScreenCenterX > ScreenCenterX * 2) TargetX = 0;
                            }
                            if (x < ScreenCenterX)
                            {
                                TargetX = x - ScreenCenterX;

                                if (TargetX + ScreenCenterX < 0) TargetX = 0;
                            }
                        }
                        if (y != 0)
                        {
                            if (y > ScreenCenterY)
                            {
                                TargetY = -(ScreenCenterY - y);

                                if (TargetY + ScreenCenterY > ScreenCenterY * 2) TargetY = 0;
                            }
                            if (y < ScreenCenterY)
                            {
                                TargetY = y - ScreenCenterY;

                                if (TargetY + ScreenCenterY < 0) TargetY = 0;
                            }
                        }

                        mouse_event(0x0001, (uint)(TargetX), (uint)(TargetY), 0, UIntPtr.Zero);
                    }
                }*/
                    END:
                yield return new WaitForEndOfFrame();

                }


            }
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
        }
    }
