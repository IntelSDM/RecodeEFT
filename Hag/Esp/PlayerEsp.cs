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
using Hag.Helpers;
using System.Reflection;

namespace Hag.Esp
{
    class PlayerEsp : MonoBehaviour
    {
        static int HighestValue = 0;
        public PlayerEsp()
        {
            Init();
        }
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(UpdateEsp());
        }
        #region Checks
        public static string GetBossName(SDK.Players players)
        {
            string bossname = "";
            switch (players.Player.Profile.Info.Settings.Role)
            {
                case WildSpawnType.gifter:
                    bossname = "Santa";
                    break;
                case WildSpawnType.bossZryachiy:
                    bossname = "Zryachiy";
                    break;
                case WildSpawnType.bossKilla:
                    bossname = "Killa";
                    break;
                case WildSpawnType.bossSanitar:
                    bossname = "Sanitar";
                    break;
                case WildSpawnType.bossGluhar:
                    bossname = "Glukhar";
                    break;
                case WildSpawnType.bossKojaniy:
                    bossname = "Shturman";
                    break;
                case WildSpawnType.bossBully:
                    bossname = "Reshala";
                    break;
                case WildSpawnType.bossTagilla:
                    bossname = "Tagilla";
                    break;
                case WildSpawnType.bossKnight:
                    bossname = "Knight";
                    break;
                case WildSpawnType.followerBigPipe:
                    bossname = "Big Pipe ;)";
                    break;
                case WildSpawnType.followerBirdEye:
                    bossname = "Bird Eye";
                    break;
                case WildSpawnType.sectantPriest:
                case WildSpawnType.sectantWarrior:
                    bossname = "Cultist";
                    break;
                case WildSpawnType.assaultGroup:
                case WildSpawnType.pmcBot:
                case WildSpawnType.cursedAssault:
                case WildSpawnType.exUsec:
                    bossname = "Raider";
                    break;
                case WildSpawnType.followerBully:
                case WildSpawnType.followerGluharAssault:
                case WildSpawnType.followerGluharScout:
                case WildSpawnType.followerGluharSnipe:
                case WildSpawnType.followerSanitar:
                case WildSpawnType.followerTest:
                case WildSpawnType.followerKojaniy:
                case WildSpawnType.followerTagilla:
                case WildSpawnType.followerZryachiy:
                    bossname = "Boss Follower";
                    break;
                default:
                    bossname = "Raider";
                    break;
            }
            return bossname;
        }
        int GetPlayerSkeletonType(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav)
                return Globals.Config.Scav.Skeleton.Type;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
                return Globals.Config.ScavPlayer.Skeleton.Type;
            if (players.PlayerType == SDK.PlayerType.Boss)
                return Globals.Config.Boss.Skeleton.Type;
            if (players.PlayerType == SDK.PlayerType.Player)
                return Globals.Config.Player.Skeleton.Type;

            return 0;
        }
        bool CheckPlayerEnabled(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && Globals.Config.Scav.Enable == false)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && Globals.Config.ScavPlayer.Enable == false)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && Globals.Config.Boss.Enable == false)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && Globals.Config.Player.Enable == false)
                return false;
            return true;
        }
        bool CheckPlayerSkeletonEnabled(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && Globals.Config.Scav.Skeleton.Enable == false)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && Globals.Config.ScavPlayer.Skeleton.Enable == false)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && Globals.Config.Boss.Skeleton.Enable == false)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && Globals.Config.Player.Skeleton.Enable == false)
                return false;
            return true;
        }
        bool CheckPlayerMaxDistance(SDK.Players players)
        {


            if (players.PlayerType == SDK.PlayerType.Scav && players.Distance> Globals.Config.Scav.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && players.Distance  > Globals.Config.ScavPlayer.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && players.Distance> Globals.Config.Boss.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && players.Distance > Globals.Config.Player.MaxDistance)
                return false;
            return true;
        }
        bool CheckPlayerSkeletonMaxDistance(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && players.Distance > Globals.Config.Scav.Skeleton.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && players.Distance > Globals.Config.ScavPlayer.Skeleton.MaxDistance )
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && players.Distance > Globals.Config.Boss.Skeleton.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && players.Distance > Globals.Config.Player.Skeleton.MaxDistance )
                return false;

            return true;
        }
        Direct2DColor GetColourByHealth(float health, float healthmax)
        {
            int green = (int)Mathf.Lerp(0, 255, (float)health / (float)(healthmax));
            return new Direct2DColor(green, 0,0,255);
        }
        Direct2DColor GetSkeletonColour(SDK.Players players,int bone)
        {
            Direct2DColor colour = new Direct2DColor(0,0,0,0);
            if (players.PlayerType == SDK.PlayerType.Player)
            {
                if (players.BoneVisible[bone])
                {
                    Color32 unitycolour = ColourHelper.GetColour("Player Skeleton Visible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
                else 
                {
                    Color32 unitycolour = ColourHelper.GetColour("Player Skeleton Invisible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
            }
            if (players.PlayerType == SDK.PlayerType.Scav)
            {
                if (players.BoneVisible[bone])
                {
                    Color32 unitycolour = ColourHelper.GetColour("Scav Skeleton Visible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
                else
                {
                    Color32 unitycolour = ColourHelper.GetColour("Scav Skeleton Invisible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
            }
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
            {
                if (players.BoneVisible[bone])
                {
                    Color32 unitycolour = ColourHelper.GetColour("ScavPlayer Skeleton Visible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
                else
                {
                    Color32 unitycolour = ColourHelper.GetColour("ScavPlayer Skeleton Invisible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
            }
            if (players.PlayerType == SDK.PlayerType.Boss)
            {
                if (players.BoneVisible[bone])
                {
                    Color32 unitycolour = ColourHelper.GetColour("Boss Skeleton Visible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
                else
                {
                    Color32 unitycolour = ColourHelper.GetColour("Boss Skeleton Invisible Colour");
                    colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                }
            }
            return colour;
        }
      
        void DrawSkeleton(Direct2DRenderer renderer,SDK.Players players)
        {
            if (!CheckPlayerSkeletonEnabled(players))
                return;
            if (!CheckPlayerSkeletonMaxDistance(players))
                return;
         
               Direct2DColor Colour;

            Vector3 headpos = players.BoneW2S[0];
            Vector3 neckpos = players.BoneW2S[1];
            Vector3 shoulderleft = players.BoneW2S[2];
            Vector3 armleft = players.BoneW2S[5];
            Vector3 palmleft = players.BoneW2S[7];
            Vector3 shoulderright = players.BoneW2S[3];
            Vector3 armright = players.BoneW2S[6];
            Vector3 palmright = players.BoneW2S[8];
            Vector3 pelvis = players.BoneW2S[4];
            Vector3 legright = players.BoneW2S[11];
            Vector3 footright = players.BoneW2S[10];
            Vector3 legleft = players.BoneW2S[13];
            Vector3 footleft = players.BoneW2S[9];
            if (GetPlayerSkeletonType(players) == 1) // health
            {
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.Head).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.Head).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(headpos) && (Esp.EspConstants.IsScreenPointVisible(neckpos)))
                    renderer.DrawCircle(headpos.x, headpos.y, (neckpos.y - headpos.y), 1,Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.Chest).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.Chest).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(shoulderright) && (Esp.EspConstants.IsScreenPointVisible(shoulderleft)))
                    renderer.DrawLine(shoulderright.x, shoulderright.y, shoulderleft.x, shoulderleft.y, 1,Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftArm).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftArm).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(armleft) && (Esp.EspConstants.IsScreenPointVisible(shoulderleft)))
                    renderer.DrawLine(shoulderleft.x, shoulderleft.y, armleft.x, armleft.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftArm).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftArm).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(armleft) && (Esp.EspConstants.IsScreenPointVisible(palmleft)))
                    renderer.DrawLine(armleft.x, armleft.y, palmleft.x, palmleft.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightArm).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightArm).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(armright) && (Esp.EspConstants.IsScreenPointVisible(shoulderright)))
                    renderer.DrawLine(shoulderright.x, shoulderright.y, armright.x, armright.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightArm).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightArm).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(armright) && (Esp.EspConstants.IsScreenPointVisible(palmright)))
                    renderer.DrawLine(armright.x, armright.y, palmright.x, palmright.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.Stomach).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.Stomach).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(neckpos) && (Esp.EspConstants.IsScreenPointVisible(pelvis)))
                    renderer.DrawLine(neckpos.x, neckpos.y, pelvis.x, pelvis.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightLeg).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightLeg).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(legright) && (Esp.EspConstants.IsScreenPointVisible(pelvis)))
                    renderer.DrawLine(legright.x, legright.y, pelvis.x, pelvis.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftLeg).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftLeg).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(legleft) && (Esp.EspConstants.IsScreenPointVisible(pelvis)))
                    renderer.DrawLine(legleft.x, legleft.y, pelvis.x, pelvis.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftLeg).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.LeftLeg).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(legleft) && (Esp.EspConstants.IsScreenPointVisible(footleft)))
                    renderer.DrawLine(legleft.x, legleft.y, footleft.x, footleft.y, 1, Colour);
                Colour = GetColourByHealth(players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightLeg).Current, players.Player.HealthController.GetBodyPartHealth(EBodyPart.RightLeg).Maximum);
                if (Esp.EspConstants.IsScreenPointVisible(legright) && (Esp.EspConstants.IsScreenPointVisible(footright)))
                    renderer.DrawLine(legright.x, legright.y, footright.x, footright.y, 1, Colour);
            }
            if (GetPlayerSkeletonType(players) == 2) // visible
            {
               
                Colour = GetSkeletonColour(players, 3);
                if (Esp.EspConstants.IsScreenPointVisible(shoulderright) && (Esp.EspConstants.IsScreenPointVisible(players.BoneW2S[14])))
                    renderer.DrawLine(shoulderright.x, shoulderright.y, (neckpos.x), (shoulderright.y + shoulderleft.y) /2, 1, Colour);

                Colour = GetSkeletonColour(players, 2);
                if (Esp.EspConstants.IsScreenPointVisible(shoulderleft) && (Esp.EspConstants.IsScreenPointVisible(players.BoneW2S[14])))
                    renderer.DrawLine(shoulderleft.x, shoulderleft.y, ( neckpos.x), (shoulderleft.y + shoulderright.y) /2, 1, Colour);

                Colour = GetSkeletonColour(players, 5);
               if (Esp.EspConstants.IsScreenPointVisible(armleft) && (Esp.EspConstants.IsScreenPointVisible(shoulderleft)))
                    renderer.DrawLine(shoulderleft.x, shoulderleft.y, armleft.x, armleft.y, 1, Colour);
                Colour = GetSkeletonColour(players, 7);
                if (Esp.EspConstants.IsScreenPointVisible(armleft) && (Esp.EspConstants.IsScreenPointVisible(palmleft)))
                    renderer.DrawLine(armleft.x, armleft.y, palmleft.x, palmleft.y, 1, Colour);
                Colour = GetSkeletonColour(players, 6);
                if (Esp.EspConstants.IsScreenPointVisible(armright) && (Esp.EspConstants.IsScreenPointVisible(shoulderright)))
                    renderer.DrawLine(shoulderright.x, shoulderright.y, armright.x, armright.y, 1, Colour);
                Colour = GetSkeletonColour(players, 8);
                      if (Esp.EspConstants.IsScreenPointVisible(armright) && (Esp.EspConstants.IsScreenPointVisible(palmright)))
                          renderer.DrawLine(armright.x, armright.y, palmright.x, palmright.y, 1, Colour);

                Colour = GetSkeletonColour(players, 0);
                if (Esp.EspConstants.IsScreenPointVisible(headpos) && (Esp.EspConstants.IsScreenPointVisible(neckpos)))
                    renderer.DrawCircle(headpos.x, headpos.y, (neckpos.y - headpos.y), 1, Colour);
                Colour = GetSkeletonColour(players, 4);
                if (Esp.EspConstants.IsScreenPointVisible(pelvis) && (Esp.EspConstants.IsScreenPointVisible(players.BoneW2S[14])))
                    renderer.DrawLine(pelvis.x, pelvis.y, players.BoneW2S[14].x, players.BoneW2S[14].y, 1, Colour);
                Colour = GetSkeletonColour(players, 14);
                if (Esp.EspConstants.IsScreenPointVisible(neckpos) && (Esp.EspConstants.IsScreenPointVisible(players.BoneW2S[14])))
                    renderer.DrawLine(neckpos.x, neckpos.y, players.BoneW2S[14].x, players.BoneW2S[14].y, 1, Colour);

                Colour = GetSkeletonColour(players, 11);
                if (Esp.EspConstants.IsScreenPointVisible(legright) && (Esp.EspConstants.IsScreenPointVisible(pelvis)))
                    renderer.DrawLine(legright.x, legright.y, pelvis.x, pelvis.y, 1, Colour);
                Colour = GetSkeletonColour(players, 13);
                if (Esp.EspConstants.IsScreenPointVisible(legleft) && (Esp.EspConstants.IsScreenPointVisible(pelvis)))
                    renderer.DrawLine(legleft.x, legleft.y, pelvis.x, pelvis.y, 1, Colour);
                Colour = GetSkeletonColour(players, 9);
                if (Esp.EspConstants.IsScreenPointVisible(legleft) && (Esp.EspConstants.IsScreenPointVisible(footleft)))
                    renderer.DrawLine(legleft.x, legleft.y, footleft.x, footleft.y, 1, Colour);
                Colour = GetSkeletonColour(players, 10);
                if (Esp.EspConstants.IsScreenPointVisible(legright) && (Esp.EspConstants.IsScreenPointVisible(footright)))
                    renderer.DrawLine(legright.x, legright.y, footright.x, footright.y, 1, Colour);
            }
        }
        bool CheckBoxEnabled(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && !Globals.Config.Scav.Box.Enable)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer &&!Globals.Config.ScavPlayer.Box.Enable)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && !Globals.Config.Boss.Box.Enable)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player &&  !Globals.Config.Player.Box.Enable)
                return false;

            return true;
        }
        bool CheckBoxDistance(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && players.Distance > Globals.Config.Scav.Box.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && players.Distance > Globals.Config.ScavPlayer.Box.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && players.Distance > Globals.Config.Boss.Box.MaxDistance)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && players.Distance > Globals.Config.Player.Box.MaxDistance)
                return false;

            return true;
        }
        bool CheckBoxFill(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && !Globals.Config.Scav.Box.Fill)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && !Globals.Config.ScavPlayer.Box.Fill)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && !Globals.Config.Boss.Box.Fill)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && !Globals.Config.Player.Box.Fill)
                return false;

            return true;
        }
        bool CheckBoxVisibilityCheck(SDK.Players players)
        {
            if (players.PlayerType == SDK.PlayerType.Scav && !Globals.Config.Scav.Box.VisibilityCheck)
                return false;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer && !Globals.Config.ScavPlayer.Box.VisibilityCheck)
                return false;
            if (players.PlayerType == SDK.PlayerType.Boss && !Globals.Config.Boss.Box.VisibilityCheck)
                return false;
            if (players.PlayerType == SDK.PlayerType.Player && !Globals.Config.Player.Box.VisibilityCheck)
                return false;

            return true;
        }
        Direct2DColor BoxColour(SDK.Players players)
        {
            Color32 unitycolour = new Color32();
            Direct2DColor colour = new Direct2DColor();
            if (players.PlayerType == SDK.PlayerType.Scav)
            {
                unitycolour = ColourHelper.GetColour("Scav Box Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
            {
                unitycolour = ColourHelper.GetColour("ScavPlayer Box Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Boss)
            {
                unitycolour = ColourHelper.GetColour("Boss Box Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Player)
            {
                unitycolour = ColourHelper.GetColour("Player Box Colour");
                if (players.Friendly == true)
                    unitycolour = ColourHelper.GetColour("Player Friend Box Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }

            return colour;
        }
        Direct2DColor BoxFillColour(SDK.Players players)
        {
            Color32 unitycolour = new Color32();
            Direct2DColor colour = new Direct2DColor();
            if (players.PlayerType == SDK.PlayerType.Scav)
            {
                unitycolour = ColourHelper.GetColour("Scav Box Filled Normal Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
            {
                unitycolour = ColourHelper.GetColour("ScavPlayer Filled Normal Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Boss)
            {
                unitycolour = ColourHelper.GetColour("Boss Box Filled Normal Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Player)
            {
                unitycolour = ColourHelper.GetColour("Player Box Filled Normal Colour");
                if (players.Friendly == true)
                    unitycolour = ColourHelper.GetColour("Player Friend Box Filled Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }

            return colour;
        }

        Direct2DColor BoxFillVisibleColour(SDK.Players players)
        {
            Color32 unitycolour = new Color32();
            Direct2DColor colour = new Direct2DColor();
            if (players.PlayerType == SDK.PlayerType.Scav)
            {
                unitycolour = ColourHelper.GetColour("Scav Box Filled Visible Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
            {
                unitycolour = ColourHelper.GetColour("ScavPlayer Filled Visible Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Boss)
            {
                unitycolour = ColourHelper.GetColour("Boss Box Filled Visible Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Player)
            {
                unitycolour = ColourHelper.GetColour("Player Box Filled Visible Colour");
                if (players.Friendly == true)
                    unitycolour = ColourHelper.GetColour("Player Friend Box Filled Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }

            return colour;
        }

        Direct2DColor BoxFillInvisibleColour(SDK.Players players)
        {
            Color32 unitycolour = new Color32();
            Direct2DColor colour = new Direct2DColor();
            if (players.PlayerType == SDK.PlayerType.Scav)
            {
                unitycolour = ColourHelper.GetColour("Scav Box Filled Invisible Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
            {
                unitycolour = ColourHelper.GetColour("ScavPlayer Filled Invisible Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Boss)
            {
                unitycolour = ColourHelper.GetColour("Boss Box Filled Invisible Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }
            if (players.PlayerType == SDK.PlayerType.Player)
            {
                unitycolour = ColourHelper.GetColour("Player Box Filled Invisible Colour");
                if (players.Friendly == true)
                    unitycolour = ColourHelper.GetColour("Player Friend Box Filled Colour");
                colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            }

            return colour;
        }
        void DrawPredictionSpot(Direct2DRenderer renderer, SDK.Players players)
        {
            if (players.TargetPosScreenPred == null)
                return;
            if (Globals.LocalPlayerWeapon == null)
                return;
            if (!Globals.Config.Aimbot.PredictionCrosshair.Enable)
                return;
            Configs.Aimbot instance = Aimbot.Aimbot.GetInstance(Globals.LocalPlayerWeapon.TemplateId);
            if (instance == null)
                return;
            Color32 pcolour = ColourHelper.GetColour("Aimbot Prediction Crosshair Primary Colour");
            Color32 scolour = ColourHelper.GetColour("Aimbot Prediction Crosshair Secondary Colour");
            Direct2DColor primarycolour = new Direct2DColor(pcolour.r, pcolour.g, pcolour.g, pcolour.a);
            Direct2DColor secondarycolour = new Direct2DColor(scolour.r, scolour.g, scolour.g, scolour.a);

            Vector2 pos = new Vector2(players.TargetPosScreenPred.x,players.TargetPosScreenPred.y);

            switch (Globals.Config.Aimbot.PredictionCrosshair.CrosshairType)
            {

                case 0: // circle

                    //   if(Globals.Config.Aimbot.PredictionCrosshair.Background)
                    renderer.BorderedCircle(pos.x, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour, secondarycolour);
                    // else
                    renderer.DrawCircle(pos.x, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    break;
                case 1: // cross
                    renderer.DrawLine(pos.x - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, pos.x - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, pos.x + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);

                    renderer.DrawLine(pos.x, pos.y - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.x, pos.y - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x, pos.y + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.x, pos.y + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);

                    break;


                case 2: // cross dot

                    renderer.FillRectangle((pos.x) - (Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness / 2f), (pos.y) - (Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness / 2f), Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, secondarycolour);

                    renderer.DrawLine(pos.x - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, pos.x - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, pos.x + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);

                    renderer.DrawLine(pos.x, pos.y - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.x, pos.y - Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x, pos.y + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, pos.x, pos.y + Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);


                    break;

                case 3: // dot
                        //    if (Globals.Config.Aimbot.PredictionCrosshair.Background)
                    renderer.FillRectangle((pos.x) - (Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness / 2f) - 1, (pos.y) - (Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness / 2f) - 1, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness + 2, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness + 1, secondarycolour);
                    renderer.FillRectangle((pos.x) - (Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness / 2f), (pos.y) - (Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness / 2f), Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    break;
                case 4: // filled circle
                        // if (Globals.Config.Aimbot.PredictionCrosshair.Background)
                    renderer.DrawCrosshair(CrosshairStyle.Dot, pos.x, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness + 1, Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, secondarycolour);
                    renderer.DrawCrosshair(CrosshairStyle.Dot, pos.x, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, primarycolour);
                    break;
                case 5: // swastika
                    renderer.RotateSwastika(pos.x, pos.y, Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize, Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, primarycolour);
                    break;
            }
        }
        void DrawBox(Direct2DRenderer renderer,SDK.Players players,float width,float height)
        {
            if (!CheckBoxEnabled(players))
                return;
            if (!CheckBoxDistance(players))
                return;

            
            if (CheckBoxFill(players))
            {
                if (CheckBoxVisibilityCheck(players))
                {
                    bool visible = false;
                    for (int i = 0; i < players.BoneWorld.Count(); i++)
                    {
                        if (players.BoneWorld[i] != null && players.BoneWorld[i] != Vector3.zero && players.BoneVisible[i])
                        {
                            visible =  true;
                        }
                    }
                        if (visible)
                    renderer.FillRectangle((players.W2SPos.x - (width / 2) ), players.BoneW2S[0].y - 5, width, height, BoxFillVisibleColour(players));
                    else
                        renderer.FillRectangle((players.W2SPos.x - (width / 2)), players.BoneW2S[0].y - 5, width, height, BoxFillInvisibleColour(players));
                }
                else
                {
                    renderer.FillRectangle((players.W2SPos.x - (width / 2)), players.BoneW2S[0].y - 5, width, height, BoxFillColour(players));
                }
            }
            renderer.DrawRectangle(players.W2SPos.x - (width / 2), players.BoneW2S[0].y - 5, width, height, 3f, new Direct2DColor(0, 0, 0, 255)); // background
            renderer.DrawRectangle(players.W2SPos.x - (width / 2), players.BoneW2S[0].y - 5, width, height, 1f, BoxColour(players));
        }
        public bool CheckBattleMode(SDK.Players players)
        {
            if (!Globals.BattleMode)
                return true;
            if (Globals.BattleMode && Globals.Config.Player.EnableEspInBattleMode && players.PlayerType == SDK.PlayerType.Player)
                return true;
            if (Globals.BattleMode && Globals.Config.Scav.EnableEspInBattleMode && players.PlayerType == SDK.PlayerType.Scav)
                return true;
            if (Globals.BattleMode && Globals.Config.ScavPlayer.EnableEspInBattleMode && players.PlayerType == SDK.PlayerType.ScavPlayer)
                return true;
            if (Globals.BattleMode && Globals.Config.Boss.EnableEspInBattleMode && players.PlayerType == SDK.PlayerType.Boss)
                return true;
            return false;
        }
            #endregion

        // main thread
        IEnumerator UpdateEsp()
        {
            for (; ; )
            {
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    HighestValue = 0;
                    if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    goto END;
                if (!(Globals.Config.Player.Enable) && !(Globals.Config.ScavPlayer.Enable) && !(Globals.Config.Scav.Enable) && !(Globals.Config.Boss.Enable))
                    goto END;
                try
                {
                    foreach (SDK.Players players in Globals.PlayerDict.Values)
                    {
                        try
                        {
                            if (players == null || players.Player == null || players.Player.HealthController.IsAlive == false)
                                continue;
                            // save up some resources
                            if (!CheckPlayerEnabled(players))
                                continue;

                            players.Distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, players.Player.Transform.position);
                            if (!CheckPlayerMaxDistance(players))
                                continue;

                            if (players.Value > HighestValue)
                                HighestValue = players.Value;
                            players.ApplyChams();
                            players.W2SCalculated = false;
                            players.TargetPosScreen = EspConstants.WorldToScreenPoint(players.TargetPos);
                            players.W2SPos = EspConstants.WorldToScreenPoint(players.Player.Transform.position);
                            if(EspConstants.IsScreenPointVisible(players.W2SPos))
                                   players.SetPlayerValue();
                            //         if(players.Value == 0 && players.ItemList.Count == 0)
                            //        players.SetPlayerValue(); // just a check so then the playerlist can have an initial value 
                           


                                if (players.Player.PlayerBones.Head != null)
                                    players.BoneWorld[0] = players.Player.PlayerBones.Head.position;
                                if (players.Player.PlayerBones.Neck != null)
                                    players.BoneWorld[1] = players.Player.PlayerBones.Neck.position;
                                if (players.Player.PlayerBones.Shoulders[0] != null)
                                    players.BoneWorld[2] = players.Player.PlayerBones.Shoulders[0].position;
                                if (players.Player.PlayerBones.Shoulders[1] != null)
                                    players.BoneWorld[3] = players.Player.PlayerBones.Shoulders[1].position;
                                if (players.Player.PlayerBones.Pelvis != null)
                                    players.BoneWorld[4] = players.Player.PlayerBones.Pelvis.position;
                                if (players.Player.PlayerBones.Forearms[0] != null)
                                    players.BoneWorld[5] = players.Player.PlayerBones.Forearms[0].position;
                                if (players.Player.PlayerBones.Forearms[1] != null)
                                    players.BoneWorld[6] = players.Player.PlayerBones.Forearms[1].position;
                                if (players.Player.PlayerBones.LeftPalm != null)
                                    players.BoneWorld[7] = players.Player.PlayerBones.LeftPalm.position;
                                if (players.Player.PlayerBones.RightPalm != null)
                                    players.BoneWorld[8] = players.Player.PlayerBones.RightPalm.position;
                                if (players.Player.PlayerBones.KickingFoot != null)
                                    players.BoneWorld[9] = players.Player.PlayerBones.KickingFoot.position; // left foot

                                if (players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(18).Value != null)
                                    players.BoneWorld[10] = players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(18).Value.position; // right foot
                                if (players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(17).Value != null)
                                    players.BoneWorld[11] = players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(17).Value.position; // right knee
                                if (players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(112).Value != null)
                                    players.BoneWorld[12] = players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(112).Value.position;
                                if (players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(22).Value != null)
                                    players.BoneWorld[13] = players.Player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones.ElementAt(22).Value.position; // Left Knee foot
                                if (players.Player.PlayerBones.Neck != null && players.Player.PlayerBones.Pelvis != null)
                                    players.BoneWorld[14] = (players.Player.PlayerBones.Neck.transform.position + players.Player.PlayerBody.PlayerBones.Pelvis.position) / 2; // centre point in chest
                                for (int i = 0; i < players.BoneWorld.Count(); i++)
                                {
                                    if (players.BoneWorld[i] != null && players.BoneWorld[i] != Vector3.zero)
                                    {
                                        players.BoneW2S[i] = Esp.EspConstants.WorldToScreenPoint(players.BoneWorld[i]);
                                    // commented out, might be useful in the future for performance but might be useful to just get all visible bones for auto shoot
                                    //      if(i == 0) // always get head even if its off screen so we can do is spotted shit
                                    //     players.BoneVisible[i] = Helpers.RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[i]);
                                    //      else if(i !=0 && EspConstants.IsScreenPointVisible(players.BoneW2S[i]))// else check if the bone is on the screen, otherwise it can frick off
                                      //  if (CheckPlayerSkeletonEnabled(players) && GetPlayerSkeletonType(players) == 2)
                                      //  {
                                            players.BoneVisible[i] = Helpers.RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[i]);
                                        //}

                                    }
                              

                                }
                        /*    if (!(CheckPlayerSkeletonEnabled(players) && GetPlayerSkeletonType(players) == 2))
                            {
                               

                                if (Globals.LocalPlayerWeapon != null)
                                {
                                    Configs.Aimbot instance =  Aimbot.Aimbot.GetInstance(Globals.LocalPlayerWeapon.TemplateId);
                                    Configs.AimbotTarget targetinstance = new Configs.AimbotTarget();
                                    if (players.PlayerType == SDK.PlayerType.Boss)
                                        targetinstance = instance.AimbotTargetBoss;
                                    if (players.PlayerType == SDK.PlayerType.Player)
                                        targetinstance = instance.AimbotTargetPlayer;
                                    if (players.PlayerType == SDK.PlayerType.Scav)
                                        targetinstance = instance.AimbotTargetScav;
                                    if (players.PlayerType == SDK.PlayerType.ScavPlayer)
                                        targetinstance = instance.AimbotTargetScavPlayer;

                                    if(targetinstance == null)
                                        players.BoneVisible[0] = Helpers.RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[0]);

                                    Helpers.RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[Aimbot.TargetPlayer.Aimbone(targetinstance.Bone, players)]);
                                }
                                else
                                {
                                     players.BoneVisible[0] = Helpers.RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[0]);
                                }

                            }*/
                            players.W2SCalculated = true;
                        }
                        catch(Exception ex) { System.IO.File.WriteAllText("playeresp 1", $"{ex.Message}\n{ex.ToString()}"); }
                    }
                }
                catch(Exception ex) { System.IO.File.WriteAllText("playeresp 2", $"{ex.Message}\n{ex.ToString()}"); }
                END:
                yield return new WaitForEndOfFrame();
            }
        }
        void Init()
        { 
        
        }
        
        // done on different thread
        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
         
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    return;
                if (!(Globals.Config.Player.Enable) && !(Globals.Config.ScavPlayer.Enable) && !(Globals.Config.Scav.Enable) && !(Globals.Config.Boss.Enable))
                    return;
            foreach (SDK.Players players in Globals.PlayerDict.Values)
            {
                try
                {
                    if (players == null || players.Player == null || players.Player.HealthController.IsAlive == false)
                        continue;
                    if (!CheckPlayerEnabled(players))
                        continue;
                    if (!CheckPlayerMaxDistance(players))
                        continue;

                    bool anythingvisible = false;
                    foreach (Vector3 bone in players.BoneW2S)
                    {
                        if (EspConstants.IsScreenPointVisible(bone))
                            anythingvisible = true;
                    }
                    if (anythingvisible == false)
                        continue;
                    if (!CheckBattleMode(players))
                        continue;
                 
                       Player player = players.Player;
                    if (players.Player?.HandsController?.Item is Weapon)
                    {
                        players.Weapon = (Weapon)players.Player?.HandsController?.Item;
                    }
                    else
                        players.Weapon = null;
                        int AmmoCurrent = 0;
                        int AmmoMax = 0;
                        if (players.Weapon != null && players.Player?.HandsController?.Item is Weapon)
                        {
                            AmmoCurrent = players.Weapon.GetCurrentMagazine().Count;
                            AmmoMax = players.Weapon.GetCurrentMagazine().MaxCount;
                        }
                        // set the height to left foot, if right foot has more distance then use that.
                        float height = Vector3.Distance(new Vector3(players.BoneW2S[0].x, players.BoneW2S[0].y - 4, players.BoneW2S[0].z), new Vector3(players.BoneW2S[0].x, players.BoneW2S[9].y, players.BoneW2S[0].z));
                        if(height < Vector3.Distance(new Vector3(players.BoneW2S[0].x, players.BoneW2S[0].y - 4, players.BoneW2S[0].z), new Vector3(players.BoneW2S[0].x, players.BoneW2S[10].y, players.BoneW2S[0].z)))
                            height = Vector3.Distance(new Vector3(players.BoneW2S[0].x, players.BoneW2S[0].y - 4, players.BoneW2S[0].z), new Vector3(players.BoneW2S[0].x, players.BoneW2S[10].y, players.BoneW2S[0].z));
                        float width = height / 2;
                        float Health = (player.HealthController.GetBodyPartHealth(EBodyPart.Common, false).Current / player.HealthController.GetBodyPartHealth(EBodyPart.Common).Maximum) * 100f;


                        string[] EspTextBottom = new string[4];
                        string[] EspTextTop = new string[4];
                        string[] EspTextRight = new string[4];
                        string[] EspTextLeft = new string[4];

                        Direct2DColor textcolour = new Direct2DColor();
                         if (players.PlayerType == SDK.PlayerType.Player)
                         {

                             if (!players.W2SCalculated && Globals.Config.Menu.WaitForW2S)
                                 continue;
                             int distancealpha = (int)Mathf.Lerp(255, 38, (float)players.Distance / (float)(Globals.Config.Player.MaxDistance + 1));
                             Color32 unitytextcolour = ColourHelper.GetColour("Player Text Colour");
                             if (Globals.Config.Player.TextVisibilityCheck)
                             {
                                 bool visible = false;
                                 for (int i = 0; i < players.BoneWorld.Count(); i++)
                                 {
                                     if (players.BoneWorld[i] != null && players.BoneWorld[i] != Vector3.zero && players.BoneVisible[i])
                                     {
                                         visible = true;
                                     }
                                 }
                                 if (visible)
                                     unitytextcolour = ColourHelper.GetColour("Player Text Visible Colour");
                                 else
                                     unitytextcolour = ColourHelper.GetColour("Player Text Invisible Colour");
                             }
                             if (players.Friendly)
                                 unitytextcolour = ColourHelper.GetColour("Player Friend Colour");
                             int valuealpha = (int)Mathf.Lerp(50, 255, (float)players.Value / (float)(HighestValue + 1));

                             textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                             if (Globals.Config.Player.Opacity == 1)
                                 textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);
                             if (Globals.Config.Player.Opacity == 2)
                                 textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, valuealpha);
                             if (Globals.Config.Player.Name.Enable)
                             {
                                 switch (Globals.Config.Player.Name.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Player.Name.Line] += player.Profile.Nickname.Localized();
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Player.Name.Line] += player.Profile.Nickname.Localized();
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Player.Name.Line] += player.Profile.Nickname.Localized();
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Player.Name.Line] += player.Profile.Nickname.Localized();
                                         break;

                                 }
                             }
                             if (Globals.Config.Player.Distance.Enable)
                             {
                                 switch (Globals.Config.Player.Distance.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Player.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Player.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Player.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Player.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;

                                 }

                             }
                             if (Globals.Config.Player.Value.Enable)
                             {
                                 switch (Globals.Config.Player.Value.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Player.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Player.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Player.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Player.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;

                                 }

                             }

                             if (Globals.Config.Player.Weapon.Enable && players.Weapon != null)
                             {
                                 switch (Globals.Config.Player.Weapon.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Player.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Player.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Player.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Player.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;

                                 }
                             }

                             if (Globals.Config.Player.Ammo.Enable && players.Weapon != null)
                             {
                                 switch (Globals.Config.Player.Ammo.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Player.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Player.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Player.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Player.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;

                                 }
                             }

                             int healthline = 0;
                             if (Globals.Config.Player.Healthbar.Enable)
                             {
                                 if (players.Distance < Globals.Config.Player.Healthbar.MaxDistance)
                                 {
                                     int line = 0;
                                     switch (Globals.Config.Player.Healthbar.Alignment)
                                     {
                                         case 1:
                                             for (int i = 1; i <= 3; i++)
                                             {

                                                 string str = EspTextBottom[i];
                                                 if (str == null || str == "")
                                                 {
                                                     line = i;
                                                     break;
                                                 }

                                             }
                                             if (line == 0)
                                                 line = 4;
                                             healthline = line;
                                             renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, (players.W2SPos.y + (16 * (line - 1))) + 3, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 2:
                                             renderer.DrawHorizontalBar(Health, (players.W2SPos.x + width / 2) + 3, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 3:

                                             renderer.DrawHorizontalBar(Health, (players.W2SPos.x - width / 2) - 5, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 4:
                                             for (int i = 1; i <= 3; i++)
                                             {

                                                 string str = EspTextTop[i];
                                                 if (str == null || str == "")
                                                 {
                                                     line = i;
                                                     break;
                                                 }

                                             }
                                             if (line == 0)
                                                 line = 4;
                                             healthline = line;
                                             renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, ((players.BoneW2S[0].y) - (17 * (line - 1))) - 14, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                     }

                                 }
                             }
                             if (Globals.Config.Player.ContentsList && Globals.PlayerContents)
                             {
                                 int bottomline = 0;
                                 int line = 0;


                                 switch (Globals.Config.Player.ContentListAlignment)
                                 {

                                     case 1:

                                         for (int i = 1; i <= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (line == 0)
                                             line = 4;
                                         if (Globals.Config.Player.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";
                                             renderer.DrawTextCentered(string.Concat(name, value, type, subtype), players.W2SPos.x, players.W2SPos.y + (17 * ((line - 1))) + (12 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         //     EspTextLeft[1] += players.ItemList.Count();
                                         break;
                                     case 2:


                                         for (int i = 1; i >= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (Globals.Config.Player.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         if (line == 0)
                                             line = 4;
                                         if (bottomline == 0)
                                             bottomline = 4;

                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawTextLeft(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) - width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         break;
                                     case 3:

                                         for (int i = 1; i >= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (Globals.Config.Player.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         if (line == 0)
                                             line = 4;
                                         if (bottomline == 0)
                                             bottomline = 4;

                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawText(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) + width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         break;
                                     case 4:
                                         for (int i = 3; i >= 1; i--)
                                         {

                                             string str = EspTextTop[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (line == 0)
                                             line = 4;
                                         if (Globals.Config.Player.Healthbar.Alignment == 4)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawTextCentered(string.Concat(name, value, type, subtype, (players.ItemList.Count)), players.BoneW2S[0].x, (((players.BoneW2S[0].y - 18) - (17 * (line - 1)) - (12 * players.ItemList.IndexOf(containeritems)))), 10, font, containeritems.Colour);
                                         }
                                         break;

                                 }
                             }

                         }
                        if (players.PlayerType == SDK.PlayerType.Scav)
                       {
                      
                            if (!players.W2SCalculated && Globals.Config.Menu.WaitForW2S)
                                continue;
                            int distancealpha = (int)Mathf.Lerp(255, 38, (float)players.Distance / (float)(Globals.Config.Scav.MaxDistance + 1));
                            Color32 unitytextcolour = ColourHelper.GetColour("Scav Text Colour");
                            if (Globals.Config.Scav.TextVisibilityCheck)
                            {
                                bool visible = false;
                                for (int i = 0; i < players.BoneWorld.Count(); i++)
                                {
                                    if (players.BoneWorld[i] != null && players.BoneWorld[i] != Vector3.zero && players.BoneVisible[i])
                                    {
                                        visible = true;
                                    }
                                }
                                if (visible)
                                    unitytextcolour = ColourHelper.GetColour("Scav Text Visible Colour");
                                else
                                    unitytextcolour = ColourHelper.GetColour("Scav Text Invisible Colour");
                            }
                            int valuealpha = (int)Mathf.Lerp(50, 255, (float)players.Value / (float)(HighestValue + 1));
                            textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                            if (Globals.Config.Player.Opacity == 1)
                                textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);
                            if (Globals.Config.Player.Opacity == 2)
                                textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, valuealpha);


                            if (Globals.Config.Scav.Name.Enable)
                            {
                                switch (Globals.Config.Scav.Name.Alignment)
                                {
                                    case 1:
                                        EspTextBottom[Globals.Config.Scav.Name.Line] += "Scav";
                                        break;
                                    case 2:
                                        EspTextLeft[Globals.Config.Scav.Name.Line] += "Scav";
                                        break;
                                    case 3:
                                        EspTextRight[Globals.Config.Scav.Name.Line] += "Scav";
                                        break;
                                    case 4:
                                        EspTextTop[Globals.Config.Scav.Name.Line] += "Scav";
                                        break;

                                }
                            }
                            if (Globals.Config.Scav.Distance.Enable)
                            {
                                switch (Globals.Config.Scav.Distance.Alignment)
                                {
                                    case 1:
                                        EspTextBottom[Globals.Config.Scav.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                        break;
                                    case 2:
                                        EspTextLeft[Globals.Config.Scav.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                        break;
                                    case 3:
                                        EspTextRight[Globals.Config.Scav.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                        break;
                                    case 4:
                                        EspTextTop[Globals.Config.Scav.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                        break;

                                }
                            }
                            if (Globals.Config.Scav.Value.Enable)
                            {
                                switch (Globals.Config.Scav.Value.Alignment)
                                {
                                    case 1:
                                        EspTextBottom[Globals.Config.Scav.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                        break;
                                    case 2:
                                        EspTextLeft[Globals.Config.Scav.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                        break;
                                    case 3:
                                        EspTextRight[Globals.Config.Scav.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                        break;
                                    case 4:
                                        EspTextTop[Globals.Config.Scav.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                        break;

                                }
                            }
                            if (Globals.Config.Scav.Weapon.Enable && players.Weapon != null)
                            {
                                switch (Globals.Config.Scav.Weapon.Alignment)
                                {
                                    case 1:
                                        EspTextBottom[Globals.Config.Scav.Weapon.Line] += players.Weapon.ShortName.Localized();
                                        break;
                                    case 2:
                                        EspTextLeft[Globals.Config.Scav.Weapon.Line] += players.Weapon.ShortName.Localized();
                                        break;
                                    case 3:
                                        EspTextRight[Globals.Config.Scav.Weapon.Line] += players.Weapon.ShortName.Localized();
                                        break;
                                    case 4:
                                        EspTextTop[Globals.Config.Scav.Weapon.Line] += players.Weapon.ShortName.Localized();
                                        break;

                                }
                            }

                            if (Globals.Config.Scav.Ammo.Enable && players.Weapon != null)
                            {
                                switch (Globals.Config.Scav.Ammo.Alignment)
                                {
                                    case 1:
                                        EspTextBottom[Globals.Config.Scav.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                        break;
                                    case 2:
                                        EspTextLeft[Globals.Config.Scav.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                        break;
                                    case 3:
                                        EspTextRight[Globals.Config.Scav.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                        break;
                                    case 4:
                                        EspTextTop[Globals.Config.Scav.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                        break;

                                }
                            }
                            int healthline = 0;
                            if (Globals.Config.Scav.Healthbar.Enable)
                            {
                                if (players.Distance < Globals.Config.Scav.Healthbar.MaxDistance)
                                {
                                    int line = 0;
                                    switch (Globals.Config.Scav.Healthbar.Alignment)
                                    {
                                        case 1:
                                            for (int i = 1; i <= 3; i++)
                                            {

                                                string str = EspTextBottom[i];
                                                if (str == null || str == "")
                                                {
                                                    line = i;
                                                    break;
                                                }

                                            }
                                            if (line == 0)
                                                line = 4;
                                            healthline = line;
                                            renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, (players.W2SPos.y + (16 * (line - 1))) + 3, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                            break;
                                        case 2:
                                            renderer.DrawHorizontalBar(Health, (players.W2SPos.x + width / 2) + 3, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                            break;
                                        case 3:

                                            renderer.DrawHorizontalBar(Health, (players.W2SPos.x - width / 2) - 5, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                            break;
                                        case 4:
                                            for (int i = 1; i <= 3; i++)
                                            {

                                                string str = EspTextTop[i];
                                                if (str == null || str == "")
                                                {
                                                    line = i;
                                                    break;
                                                }

                                            }
                                            if (line == 0)
                                                line = 4;
                                            healthline = line;
                                            renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, ((players.BoneW2S[0].y) - (17 * (line - 1))) - 14, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                            break;
                                    }

                                }
                            }

                            if (Globals.Config.Scav.ContentsList && Globals.ScavContents)
                            {
                                int bottomline = 0;
                                int line = 0;


                                switch (Globals.Config.Scav.ContentListAlignment)
                                {

                                    case 1:

                                        for (int i = 1; i <= 3; i++)
                                        {

                                            string str = EspTextBottom[i];
                                            if (str == null || str == "")
                                            {
                                                line = i;
                                                break;
                                            }

                                        }
                                        if (line == 0)
                                            line = 4;
                                        if (Globals.Config.Scav.Healthbar.Alignment == 1)
                                            line = healthline + 1; // if the health bar is on line the 4th line or not
                                        foreach (SDK.ContainerItems containeritems in players.ItemList)
                                        {
                                            containeritems.Colour.Alpha = textcolour.Alpha;
                                            string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                            string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                            string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                            string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";
                                            renderer.DrawTextCentered(string.Concat(name, value, type, subtype), players.W2SPos.x, players.W2SPos.y + (17 * ((line - 1))) + (12 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                        }
                                        //     EspTextLeft[1] += players.ItemList.Count();
                                        break;
                                    case 2:


                                        for (int i = 1; i >= 3; i++)
                                        {

                                            string str = EspTextBottom[i];
                                            if (str == null || str == "")
                                            {
                                                line = i;
                                                break;
                                            }

                                        }
                                        if (Globals.Config.Scav.Healthbar.Alignment == 1)
                                            line = healthline + 1; // if the health bar is on line the 4th line or not
                                        if (line == 0)
                                            line = 4;
                                        if (bottomline == 0)
                                            bottomline = 4;

                                        foreach (SDK.ContainerItems containeritems in players.ItemList)
                                        {
                                            containeritems.Colour.Alpha = textcolour.Alpha;
                                            string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                            string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                            string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                            string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                            renderer.DrawTextLeft(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) - width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                        }
                                        break;
                                    case 3:

                                        for (int i = 1; i >= 3; i++)
                                        {

                                            string str = EspTextBottom[i];
                                            if (str == null || str == "")
                                            {
                                                line = i;
                                                break;
                                            }

                                        }
                                        if (Globals.Config.Scav.Healthbar.Alignment == 1)
                                            line = healthline + 1; // if the health bar is on line the 4th line or not
                                        if (line == 0)
                                            line = 4;
                                        if (bottomline == 0)
                                            bottomline = 4;

                                        foreach (SDK.ContainerItems containeritems in players.ItemList)
                                        {
                                            containeritems.Colour.Alpha = textcolour.Alpha;
                                            string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                            string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                            string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                            string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                            renderer.DrawText(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) + width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                        }
                                        break;
                                    case 4:
                                        for (int i = 3; i >= 1; i--)
                                        {

                                            string str = EspTextTop[i];
                                            if (str == null || str == "")
                                            {
                                                line = i;
                                                break;
                                            }

                                        }
                                        if (line == 0)
                                            line = 4;
                                        if (Globals.Config.Scav.Healthbar.Alignment == 4)
                                            line = healthline + 1; // if the health bar is on line the 4th line or not
                                        foreach (SDK.ContainerItems containeritems in players.ItemList)
                                        {
                                            containeritems.Colour.Alpha = textcolour.Alpha;
                                            string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                            string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                            string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                            string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                            renderer.DrawTextCentered(string.Concat(name, value, type, subtype, (players.ItemList.Count)), players.BoneW2S[0].x, (((players.BoneW2S[0].y - 18) - (17 * (line - 1)) - (12 * players.ItemList.IndexOf(containeritems)))), 10, font, containeritems.Colour);
                                        }
                                        break;

                                }
                            }
                      
                    }

                        if (players.PlayerType == SDK.PlayerType.ScavPlayer)
                         {
                             if (!players.W2SCalculated && Globals.Config.Menu.WaitForW2S)
                                 continue;
                             int distancealpha = (int)Mathf.Lerp(255, 38, (float)players.Distance / (float)(Globals.Config.ScavPlayer.MaxDistance + 1));
                             Color32 unitytextcolour = ColourHelper.GetColour("ScavPlayer Text Colour");
                             if (Globals.Config.ScavPlayer.TextVisibilityCheck)
                             {
                                 bool visible = false;
                                 for (int i = 0; i < players.BoneWorld.Count(); i++)
                                 {
                                     if (players.BoneWorld[i] != null && players.BoneWorld[i] != Vector3.zero && players.BoneVisible[i])
                                     {
                                         visible = true;
                                     }
                                 }
                                 if (visible)
                                     unitytextcolour = ColourHelper.GetColour("ScavPlayer Text Visible Colour");
                                 else
                                     unitytextcolour = ColourHelper.GetColour("ScavPlayer Text Invisible Colour");
                             }
                             int valuealpha = (int)Mathf.Lerp(50, 255, (float)players.Value / (float)(HighestValue + 1));
                             textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                             if (Globals.Config.Player.Opacity == 1)
                                 textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);
                             if (Globals.Config.Player.Opacity == 2)
                                 textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, valuealpha);

                             if (Globals.Config.ScavPlayer.Enable)
                             {
                                 switch (Globals.Config.ScavPlayer.Name.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.ScavPlayer.Name.Line] += "Scav Player";
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.ScavPlayer.Name.Line] += "Scav Player";
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.ScavPlayer.Name.Line] += "Scav Player";
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.ScavPlayer.Name.Line] += "Scav Player";
                                         break;

                                 }
                             }
                             if (Globals.Config.ScavPlayer.Distance.Enable)
                             {
                                 switch (Globals.Config.ScavPlayer.Distance.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.ScavPlayer.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.ScavPlayer.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.ScavPlayer.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.ScavPlayer.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;

                                 }
                             }
                             if (Globals.Config.ScavPlayer.Value.Enable)
                             {
                                 switch (Globals.Config.ScavPlayer.Value.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.ScavPlayer.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.ScavPlayer.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.ScavPlayer.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.ScavPlayer.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;

                                 }
                             }
                             if (Globals.Config.ScavPlayer.Weapon.Enable && players.Weapon != null)
                             {
                                 switch (Globals.Config.ScavPlayer.Weapon.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.ScavPlayer.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.ScavPlayer.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.ScavPlayer.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.ScavPlayer.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;

                                 }
                             }

                             if (Globals.Config.ScavPlayer.Ammo.Enable && players.Weapon != null)
                             {
                                 switch (Globals.Config.ScavPlayer.Ammo.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.ScavPlayer.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.ScavPlayer.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.ScavPlayer.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.ScavPlayer.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;

                                 }
                             }
                             int healthline = 0;
                             if (Globals.Config.ScavPlayer.Healthbar.Enable && Globals.ScavPlayerContents)
                             {
                                 if (players.Distance < Globals.Config.ScavPlayer.Healthbar.MaxDistance)
                                 {
                                     int line = 0;
                                     switch (Globals.Config.ScavPlayer.Healthbar.Alignment)
                                     {
                                         case 1:
                                             for (int i = 1; i <= 3; i++)
                                             {

                                                 string str = EspTextBottom[i];
                                                 if (str == null || str == "")
                                                 {
                                                     line = i;
                                                     break;
                                                 }

                                             }
                                             if (line == 0)
                                                 line = 4;
                                             healthline = line;
                                             renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, (players.W2SPos.y + (16 * (line - 1))) + 3, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 2:
                                             renderer.DrawHorizontalBar(Health, (players.W2SPos.x + width / 2) + 3, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 3:

                                             renderer.DrawHorizontalBar(Health, (players.W2SPos.x - width / 2) - 5, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 4:
                                             for (int i = 1; i <= 3; i++)
                                             {

                                                 string str = EspTextTop[i];
                                                 if (str == null || str == "")
                                                 {
                                                     line = i;
                                                     break;
                                                 }

                                             }
                                             if (line == 0)
                                                 line = 4;
                                             healthline = line;
                                             renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, ((players.BoneW2S[0].y) - (17 * (line - 1))) - 14, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                     }

                                 }
                             }
                             if (Globals.Config.ScavPlayer.ContentsList)
                             {
                                 int bottomline = 0;
                                 int line = 0;


                                 switch (Globals.Config.ScavPlayer.ContentListAlignment)
                                 {

                                     case 1:

                                         for (int i = 1; i <= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (line == 0)
                                             line = 4;
                                         if (Globals.Config.ScavPlayer.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";
                                             renderer.DrawTextCentered(string.Concat(name, value, type, subtype), players.W2SPos.x, players.W2SPos.y + (17 * ((line - 1))) + (12 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         //     EspTextLeft[1] += players.ItemList.Count();
                                         break;
                                     case 2:


                                         for (int i = 1; i >= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (Globals.Config.ScavPlayer.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         if (line == 0)
                                             line = 4;
                                         if (bottomline == 0)
                                             bottomline = 4;

                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawTextLeft(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) - width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         break;
                                     case 3:

                                         for (int i = 1; i >= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (Globals.Config.ScavPlayer.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         if (line == 0)
                                             line = 4;
                                         if (bottomline == 0)
                                             bottomline = 4;

                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawText(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) + width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         break;
                                     case 4:
                                         for (int i = 3; i >= 1; i--)
                                         {

                                             string str = EspTextTop[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (line == 0)
                                             line = 4;
                                         if (Globals.Config.ScavPlayer.Healthbar.Alignment == 4)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawTextCentered(string.Concat(name, value, type, subtype, (players.ItemList.Count)), players.BoneW2S[0].x, (((players.BoneW2S[0].y - 18) - (17 * (line - 1)) - (12 * players.ItemList.IndexOf(containeritems)))), 10, font, containeritems.Colour);
                                         }
                                         break;

                                 }
                             }
                         }
                         if (players.PlayerType == SDK.PlayerType.Boss)
                         {
                             if (!players.W2SCalculated && Globals.Config.Menu.WaitForW2S)
                                 continue;
                             int distancealpha = (int)Mathf.Lerp(255, 38, (float)players.Distance / (float)(Globals.Config.Boss.MaxDistance + 1));
                             Color32 unitytextcolour = ColourHelper.GetColour("Boss Text Colour");
                             if (Globals.Config.Boss.TextVisibilityCheck)
                             {
                                 bool visible = false;
                                 for (int i = 0; i < players.BoneWorld.Count(); i++)
                                 {
                                     if (players.BoneWorld[i] != null && players.BoneWorld[i] != Vector3.zero && players.BoneVisible[i])
                                     {
                                         visible = true;
                                     }
                                 }
                                 if (visible)
                                     unitytextcolour = ColourHelper.GetColour("Boss Text Visible Colour");
                                 else
                                     unitytextcolour = ColourHelper.GetColour("Boss Text Invisible Colour");
                             }
                             int valuealpha = (int)Mathf.Lerp(50, 255, (float)players.Value / (float)(HighestValue + 1));
                             textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                             if (Globals.Config.Player.Opacity == 1)
                                 textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);
                             if (Globals.Config.Player.Opacity == 2)
                                 textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, valuealpha);

                             string bossname = GetBossName(players);

                             if (Globals.Config.Boss.Enable)
                             {
                                 switch (Globals.Config.Boss.Name.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Boss.Name.Line] += bossname;
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Boss.Name.Line] += bossname;
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Boss.Name.Line] += bossname;
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Boss.Name.Line] += bossname;
                                         break;

                                 }
                             }
                             if (Globals.Config.Boss.Distance.Enable)
                             {
                                 switch (Globals.Config.Boss.Distance.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Boss.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Boss.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Boss.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Boss.Distance.Line] += string.Concat("[", players.Distance, "m]");
                                         break;

                                 }

                             }
                             if (Globals.Config.Boss.Value.Enable)
                             {
                                 switch (Globals.Config.Boss.Value.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Boss.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Boss.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Boss.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Boss.Value.Line] += string.Concat("[", players.Value / 1000, "k]");
                                         break;

                                 }

                             }

                             if (Globals.Config.Boss.Weapon.Enable && players.Weapon != null)
                             {
                                 switch (Globals.Config.Boss.Weapon.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Boss.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Boss.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Boss.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Boss.Weapon.Line] += players.Weapon.ShortName.Localized();
                                         break;

                                 }
                             }

                             if (Globals.Config.Boss.Ammo.Enable && players.Weapon != null)
                             {
                                 switch (Globals.Config.Boss.Ammo.Alignment)
                                 {
                                     case 1:
                                         EspTextBottom[Globals.Config.Boss.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 2:
                                         EspTextLeft[Globals.Config.Boss.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 3:
                                         EspTextRight[Globals.Config.Boss.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;
                                     case 4:
                                         EspTextTop[Globals.Config.Boss.Ammo.Line] += string.Concat("[", AmmoCurrent, "/", AmmoMax, "]");
                                         break;

                                 }
                             }
                             int healthline = 0;
                             if (Globals.Config.Boss.Healthbar.Enable)
                             {
                                 if (players.Distance < Globals.Config.Boss.Healthbar.MaxDistance)
                                 {
                                     int line = 0;
                                     switch (Globals.Config.Boss.Healthbar.Alignment)
                                     {
                                         case 1:
                                             for (int i = 1; i <= 3; i++)
                                             {

                                                 string str = EspTextBottom[i];
                                                 if (str == null || str == "")
                                                 {
                                                     line = i;
                                                     break;
                                                 }

                                             }
                                             if (line == 0)
                                                 line = 4;
                                             healthline = line;
                                             renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, (players.W2SPos.y + (16 * (line - 1))) + 3, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 2:
                                             renderer.DrawHorizontalBar(Health, (players.W2SPos.x + width / 2) + 3, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 3:

                                             renderer.DrawHorizontalBar(Health, (players.W2SPos.x - width / 2) - 5, players.BoneW2S[0].y - 10, 2, height, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                         case 4:
                                             for (int i = 1; i <= 3; i++)
                                             {

                                                 string str = EspTextTop[i];
                                                 if (str == null || str == "")
                                                 {
                                                     line = i;
                                                     break;
                                                 }

                                             }
                                             if (line == 0)
                                                 line = 4;
                                             healthline = line;
                                             renderer.DrawVerticalBar(Health, players.W2SPos.x - (width / 2) - 2, ((players.BoneW2S[0].y) - (17 * (line - 1))) - 14, width + 2, 2, 1, new Direct2DColor(0, 255, 0, 255), new Direct2DColor(15, 15, 15, 185));
                                             break;
                                     }

                                 }
                             }
                             if (Globals.Config.Boss.ContentsList && Globals.BossContents)
                             {
                                 int bottomline = 0;
                                 int line = 0;


                                 switch (Globals.Config.Boss.ContentListAlignment)
                                 {

                                     case 1:

                                         for (int i = 1; i <= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (line == 0)
                                             line = 4;
                                         if (Globals.Config.Boss.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;

                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";
                                             renderer.DrawTextCentered(string.Concat(name, value, type, subtype), players.W2SPos.x, players.W2SPos.y + (17 * ((line - 1))) + (12 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         //     EspTextLeft[1] += players.ItemList.Count();
                                         break;
                                     case 2:


                                         for (int i = 1; i >= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (Globals.Config.Boss.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         if (line == 0)
                                             line = 4;
                                         if (bottomline == 0)
                                             bottomline = 4;

                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawTextLeft(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) - width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         break;
                                     case 3:

                                         for (int i = 1; i >= 3; i++)
                                         {

                                             string str = EspTextBottom[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (Globals.Config.Boss.Healthbar.Alignment == 1)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         if (line == 0)
                                             line = 4;
                                         if (bottomline == 0)
                                             bottomline = 4;

                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawText(string.Concat(name, value, type, subtype), (players.W2SPos.x + width / 2) + width * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * players.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                         }
                                         break;
                                     case 4:
                                         for (int i = 3; i >= 1; i--)
                                         {

                                             string str = EspTextTop[i];
                                             if (str == null || str == "")
                                             {
                                                 line = i;
                                                 break;
                                             }

                                         }
                                         if (line == 0)
                                             line = 4;
                                         if (Globals.Config.Boss.Healthbar.Alignment == 4)
                                             line = healthline + 1; // if the health bar is on line the 4th line or not
                                         foreach (SDK.ContainerItems containeritems in players.ItemList)
                                         {
                                             containeritems.Colour.Alpha = textcolour.Alpha;
                                             string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                             string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                             string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                             string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                             renderer.DrawTextCentered(string.Concat(name, value, type, subtype), players.BoneW2S[0].x, (((players.BoneW2S[0].y - 18) - (17 * (line - 1)) - (12 * players.ItemList.IndexOf(containeritems)))), 10, font, containeritems.Colour);
                                         }
                                         break;

                                 }
                             }
                         }

                          for (int i = 1; i <= 3; i++)
                        {
                            if (EspTextBottom[i] != null)
                                if (EspConstants.IsScreenPointVisible(players.W2SPos))
                                    renderer.DrawTextCentered(EspTextBottom[i], players.W2SPos.x, players.W2SPos.y + (16 * (i - 1)), 11, font, textcolour);
                            if (EspTextLeft[i] != null)
                                if (EspConstants.IsScreenPointVisible(players.W2SPos) && EspConstants.IsScreenPointVisible(players.BoneW2S[0]))
                                    renderer.DrawTextLeft(EspTextLeft[i], (players.W2SPos.x + width / 2) - (width * 1.5f), (players.W2SPos.y - ((height / 3) * (i - 1))) - height / 3, 11, font, textcolour);
                            if (EspTextRight[i] != null)
                                if (EspConstants.IsScreenPointVisible(players.W2SPos) && EspConstants.IsScreenPointVisible(players.BoneW2S[0]))
                                    renderer.DrawText(EspTextRight[i], (players.W2SPos.x + width / 2) + (width), (players.W2SPos.y - ((height / 3) * (i - 1))) - height / 3, 11, font, textcolour);
                            if (EspTextTop[i] != null)
                                if (EspConstants.IsScreenPointVisible(players.BoneW2S[0]))
                                    renderer.DrawTextCentered(EspTextTop[i], players.BoneW2S[0].x, (players.BoneW2S[0].y - 22) - (17 * (i - 1)), 11, font, textcolour);
                        }
                        DrawSkeleton(renderer, players);
                        DrawBox(renderer, players, width, height);
                    DrawPredictionSpot(renderer, players);
                    
                }
                catch(Exception ex) { System.IO.File.WriteAllText("player draw", $"{ex.Message}\n {ex.ToString()}"); }
                }
                
                

        }
    }
}
