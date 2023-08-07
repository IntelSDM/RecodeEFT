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

namespace Hag.Aimbot
{
    class TargetPlayer
    {
        public static bool IsTargetSuitable = false; // basically we can use this so we can keep our target player but not target it if it isn't a suitable target
        public static bool RequiresManipulation = false;
        public static Vector3 ManipulationAngle = Vector3.zero;
        public static Vector3 ManipulationAngleScreen = Vector3.zero;
        private static float LastTargetTime = 0;
        static List<SDK.Players> OrganizedDict(Dictionary<Player,SDK.Players>.ValueCollection p,Configs.Aimbot aimbot)
        {
            return p.OfType<SDK.Players>()
             
              .OrderBy(p => p.Distance)
               .ThenBy(p => Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), TranslateTargetLimbW2S(p, aimbot)))
              .ToList();
        }
        static List<Vector3> OrganizeBone(List<Vector3> p)
        {
            return (from val in p
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(val.x,val.y))
                    select val).ToList<Vector3>();
        }
        static List<Vector3> OrganizeBone(List<Vector3> p, Vector3[] boneworld)
        {
            return (from val in p
                    let index = Array.IndexOf(boneworld, val)
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(val.x, val.y))
                    select boneworld[index]).ToList<Vector3>();
        }
        public static string TranslateTargetLimb(int limb)
        {
            switch (limb)
            {
                case 0:
                    return "Nearest";
                case 1:
                    return "Head";
                case 2:
                    return "Neck";
                case 3:
                    return "Chest";
                case 4:
                    return "Stomach";
                case 5:
                    return "Arms";
                case 6:
                    return "Legs";
                case 7:
                    return "Hands";
                case 8:
                    return "Feet";
            }
            return "";
        }
        private static Vector3 TranslateTargetLimbW2S(SDK.Players players, Configs.Aimbot instance)
        {
            Configs.AimbotTarget targetinstance = instance.AimbotTargetPlayer;
            if (players.PlayerType == SDK.PlayerType.Boss)
                targetinstance = instance.AimbotTargetBoss;
            if (players.PlayerType == SDK.PlayerType.Scav)
                targetinstance = instance.AimbotTargetScav;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
                targetinstance = instance.AimbotTargetScavPlayer;
            if (players.PlayerType == SDK.PlayerType.Player)
                targetinstance = instance.AimbotTargetPlayer;

            switch (targetinstance.Bone)
            {
                case 0:
                    Vector3 Closestbone = Vector3.zero;
                    /*  for (int i = 0; i < players.BoneW2S.Count(); i++)
                      {
                          if (players.BoneW2S[i] == Vector3.zero)
                              continue;
                          if (Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(players.BoneW2S[i].x, players.BoneW2S[i].y)) < Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Closestbone.x, Closestbone.y)))
                              Closestbone = players.BoneWorld[i];
                      }*/
                    //    return OrganizeBone(p: players.BoneW2S.ToList())[0]; // return the 1st closest bone
                    foreach (Vector3 bone in players.BoneW2S)
                    {
                        if (bone == Vector3.zero)
                            continue;
                        if (Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(bone.x, bone.y)) < Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Closestbone.x, Closestbone.y)))
                            Closestbone = bone;
                    }
                    return Closestbone;
                case 1:
                    return players.BoneW2S[0];
                case 2:
                    return players.BoneW2S[1];
                case 3:
                    return players.BoneW2S[14];
                case 4:
                    return players.BoneW2S[4];
                case 5:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[5]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[6]) ? players.BoneW2S[6] : players.BoneW2S[5];
                case 6:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[11]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[13]) ? players.BoneW2S[13] : players.BoneW2S[11];
                case 7:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[7]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[8]) ? players.BoneW2S[8] : players.BoneW2S[7];
                case 8:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[9]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[10]) ? players.BoneW2S[10] : players.BoneW2S[9];
                case 9:
                    if(players.BoneVisible[0] || CanManipulate(players,players.BoneWorld[14],targetinstance))
                        return players.BoneW2S[0];
                    if (players.BoneVisible[1] || CanManipulate(players, players.BoneWorld[14], targetinstance))
                        return players.BoneW2S[1];
                    if (players.BoneVisible[14] || CanManipulate(players, players.BoneWorld[14], targetinstance))
                        return players.BoneW2S[14];
                    if (players.BoneVisible[4] || CanManipulate(players, players.BoneWorld[4], targetinstance))
                        return players.BoneW2S[4];
                    if (players.BoneVisible[5] || CanManipulate(players, players.BoneWorld[5], targetinstance))
                        return players.BoneW2S[5];
                    if (players.BoneVisible[6] || CanManipulate(players, players.BoneWorld[6], targetinstance))
                        return players.BoneW2S[6];
                    if (players.BoneVisible[11] || CanManipulate(players, players.BoneWorld[11], targetinstance))
                        return players.BoneW2S[11];
                    if (players.BoneVisible[13] || CanManipulate(players, players.BoneWorld[13], targetinstance))
                        return players.BoneW2S[13];
                    if (players.BoneVisible[7] || CanManipulate(players, players.BoneWorld[7], targetinstance))
                        return players.BoneW2S[7];
                    if (players.BoneVisible[8] || CanManipulate(players, players.BoneWorld[8], targetinstance))
                        return players.BoneW2S[8];
                    if (players.BoneVisible[9] || CanManipulate(players, players.BoneWorld[9], targetinstance))
                        return players.BoneW2S[9];
                    if (players.BoneVisible[10] || CanManipulate(players, players.BoneWorld[10], targetinstance))
                        return players.BoneW2S[10];
                    return players.BoneW2S[0];
                    
            }
            return Vector2.zero;
        }
        public static int Aimbone(int limb,SDK.Players players)
        {
            switch (limb)
            {
                case 0:
                    int Closestbone = 0;
                    Vector3 ClosestboneVec = Vector3.zero;
                    foreach (Vector3 bone in players.BoneW2S)
                    {
                        if (bone == Vector3.zero)
                            continue;
                        if (Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(bone.x, bone.y)) < Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(ClosestboneVec.x, ClosestboneVec.y)))
                        {
                            Closestbone = Array.IndexOf(players.BoneW2S, bone);
                            ClosestboneVec = players.BoneWorld[Array.IndexOf(players.BoneW2S, bone)];
                        }
                        }
                        return Closestbone;
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 3:
                    return 14 ;
                case 4:
                    return 4;
                case 5:
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[5]))
                        return 5;
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[6]))
                        return 6;
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[5]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[6]) ? 6 : 5;
                case 6:
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[13]))
                        return 13;
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[11]))
                        return 11;
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[11]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[13]) ?13 : 11;
                case 7:
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[7]))
                        return 7;
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[8]))
                        return 8;
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[7]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[8]) ? 8 : 7;
                case 8:
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[9]))
                        return 9;
                    if (RaycastHelper.IsPointVisible(players.Player, players.BoneWorld[10]))
                        return 10;
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[10]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[9]) ? 9 : 10;
            }
            return 0;
        }
        private static Vector3 TranslateTargetLimbWorld(SDK.Players players, Configs.Aimbot instance)
        {
            Configs.AimbotTarget targetinstance = instance.AimbotTargetPlayer;
            if (players.PlayerType == SDK.PlayerType.Boss)
                targetinstance = instance.AimbotTargetBoss;
            if (players.PlayerType == SDK.PlayerType.Scav)
                targetinstance = instance.AimbotTargetScav;
            if (players.PlayerType == SDK.PlayerType.ScavPlayer)
                targetinstance = instance.AimbotTargetScavPlayer;
            if (players.PlayerType == SDK.PlayerType.Player)
                targetinstance = instance.AimbotTargetPlayer;
            switch (targetinstance.Bone)
            {
                case 0:
                    Vector3 Closestbone = Vector3.zero;
                    foreach (Vector3 bone in players.BoneW2S)
                    {
                        if (bone == Vector3.zero)
                            continue;
                        if (Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(bone.x, bone.y)) < Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Closestbone.x, Closestbone.y)))
                            Closestbone = players.BoneWorld[Array.IndexOf(players.BoneW2S, bone)];
                    }
                    return Closestbone;
                   // return OrganizeBone(p: players.BoneW2S.ToList(),players.BoneWorld)[0]; // return the 1st closest bone
                case 1:
                    return players.BoneWorld[0];
                case 2:
                    return players.BoneWorld[1];
                case 3:
                    return players.BoneWorld[14];
                case 4:
                    return players.BoneWorld[4];
                case 5:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[5]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[6]) ? players.BoneWorld[6] : players.BoneWorld[5];
                case 6:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[11]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[13]) ? players.BoneWorld[13] : players.BoneWorld[11];
                case 7:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[7]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[8]) ? players.BoneWorld[8] : players.BoneWorld[7];
                case 8:
                    return Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[9]) > Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), players.BoneW2S[10]) ? players.BoneWorld[10] : players.BoneWorld[9];
                case 9:
                    if (players.BoneVisible[0] || CanManipulate(players, players.BoneWorld[14], targetinstance))
                        return players.BoneWorld[0];
                    if (players.BoneVisible[1] || CanManipulate(players, players.BoneWorld[14], targetinstance))
                        return players.BoneWorld[1];
                    if (players.BoneVisible[14] || CanManipulate(players, players.BoneWorld[14], targetinstance))
                        return players.BoneWorld[14];
                    if (players.BoneVisible[4] || CanManipulate(players, players.BoneWorld[4], targetinstance))
                        return players.BoneWorld[4];
                    if (players.BoneVisible[5] || CanManipulate(players, players.BoneWorld[5], targetinstance))
                        return players.BoneWorld[5];
                    if (players.BoneVisible[6] || CanManipulate(players, players.BoneWorld[6], targetinstance))
                        return players.BoneWorld[6];
                    if (players.BoneVisible[11] || CanManipulate(players, players.BoneWorld[11], targetinstance))
                        return players.BoneWorld[11];
                    if (players.BoneVisible[13] || CanManipulate(players, players.BoneWorld[13], targetinstance))
                        return players.BoneWorld[13];
                    if (players.BoneVisible[7] || CanManipulate(players, players.BoneWorld[7], targetinstance))
                        return players.BoneWorld[7];
                    if (players.BoneVisible[8] || CanManipulate(players, players.BoneWorld[8], targetinstance))
                        return players.BoneWorld[8];
                    if (players.BoneVisible[9] || CanManipulate(players, players.BoneWorld[9], targetinstance))
                        return players.BoneWorld[9];
                    if (players.BoneVisible[10] || CanManipulate(players, players.BoneWorld[10], targetinstance))
                        return players.BoneWorld[10];
                    return players.BoneWorld[0];
            }
            return Vector2.zero;
        }

      
        private static bool CanManipulate(SDK.Players players,Vector3 bone, Configs.AimbotTarget instance)
        {
            List<Vector3> Angles = new List<Vector3>();
            Angles.Add(Globals.LocalPlayer.Fireport.position + Globals.MainCamera.transform.forward  * instance.ManipAmount);
     //       Angles.Add(Globals.LocalPlayer.Fireport.position + Globals.MainCamera.transform.forward * -2.0f);
            Angles.Add(Globals.LocalPlayer.Fireport.position + Globals.MainCamera.transform.up * instance.ManipAmount);
            Angles.Add(Globals.LocalPlayer.Fireport.position + Globals.MainCamera.transform.right * instance.ManipAmount);
            Angles.Add(Globals.LocalPlayer.Fireport.position + Globals.MainCamera.transform.right * -instance.ManipAmount);
            Angles.Add(Globals.LocalPlayer.Fireport.position + Globals.MainCamera.transform.up * -instance.ManipAmount);
            Angles.Add(Globals.MainCamera.transform.position);

            foreach (Vector3 pos in Angles)
            {
                if (RaycastHelper.IsPointVisible(pos, players.Player, bone))
                {
                    RequiresManipulation = true;
                    ManipulationAngle = pos;
                    ManipulationAngleScreen = Esp.EspConstants.WorldToScreenPoint(ManipulationAngle);
                    return true;
                }
            }
            RequiresManipulation = false;
            return false;
        }
        public static void GetTargetPlayer()
        {
            if (Globals.LocalPlayer == null || Globals.LocalPlayerWeapon == null || Globals.GameWorld == null)
            {
                Aimbot.TargetPlayers = null;
             
                return;
            }
                Configs.Aimbot Instance = Aimbot.GetInstance(Globals.LocalPlayerWeapon.TemplateId);
                 if (Instance == null)
                 {
                     Aimbot.TargetPlayers = null;
                    
                     return;
                 }

            // foreach(SDK.Players players in Globals.PlayerDict.Values)//   foreach (SDK.Players players in OrganizedDict(Globals.PlayerDict, Instance.Bone).Values)
            Configs.AimbotTarget targetinstance = Instance.AimbotTargetPlayer;
            foreach (SDK.Players players in OrganizedDict(Globals.PlayerDict.Values, Instance))
            {
                if (players == null)
                    continue;
                if (players.Player == null)
                    continue;
                if (!players.Player.HealthController.IsAlive)
                    continue;
                players.TargetPos = targetinstance.Prediction ? Prediction.PredictedPos(players, TranslateTargetLimbWorld(players,Instance)) : TranslateTargetLimbWorld(players, Instance);
                players.TargetPosScreen = TranslateTargetLimbW2S(players, Instance);
                players.TargetPosScreenPred = Esp.EspConstants.WorldToScreenPoint(Prediction.PredictedPos(players, TranslateTargetLimbWorld(players, Instance)));


                if (players.PlayerType == SDK.PlayerType.Boss)
                    targetinstance = Instance.AimbotTargetBoss;
                if (players.PlayerType == SDK.PlayerType.Scav)
                    targetinstance = Instance.AimbotTargetScav;
                if (players.PlayerType == SDK.PlayerType.ScavPlayer)
                    targetinstance = Instance.AimbotTargetScavPlayer;
                if (players.PlayerType == SDK.PlayerType.Player)
                    targetinstance = Instance.AimbotTargetPlayer;

            }
            if (Instance.AimbotTargetting.StickToTarget)
            {
                if (Aimbot.TargetPlayers == null)
                    goto LOOP;
                if (Aimbot.TargetPlayers.Player == null)
                    goto LOOP;
                if (!(Aimbot.TargetPlayers.Player.HealthController.IsAlive))
                    goto LOOP;
                if (Aimbot.TargetPlayers.Distance > targetinstance.MaxDistance)
                    goto LOOP;
                if (Aimbot.TargetPlayers.Distance < targetinstance.MinDistance)
                    goto LOOP;
                if (!targetinstance.Enable)
                    goto LOOP;
                if (Aimbot.TargetPlayers.Friendly)
                    goto LOOP;
                Vector3 translatedtargetlimb = Aimbot.TargetPlayers.TargetPosScreen;
                if (!targetinstance.IgnoreFov && Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(translatedtargetlimb.x, translatedtargetlimb.y)) > Aimbot.FOV)
                    goto LOOP;
                /*     if (targetinstance.Manipulation)
                      {
                          if (!CanManipulate(Aimbot.TargetPlayers, TranslateTargetLimbWorld(Aimbot.TargetPlayers, Instance), targetinstance))
                              goto LOOP;
                      }
                      else
                      {
                          if (!RaycastHelper.IsPointVisible(Aimbot.TargetPlayers.Player, TranslateTargetLimbWorld(Aimbot.TargetPlayers, Instance)))
                              goto LOOP;
                      }*/
                if (!RaycastHelper.IsPointVisible(Aimbot.TargetPlayers.Player, TranslateTargetLimbWorld(Aimbot.TargetPlayers, Instance)))
                    goto LOOP;
                IsTargetSuitable = true;
                return;
            }
            LOOP:
            if (Time.time >= LastTargetTime)
            {
                
                foreach (SDK.Players players in OrganizedDict(Globals.PlayerDict.Values, Instance))
                {
                    if (players == null)
                        continue;
                    if (players.Player == null)
                        continue;
                    if (!players.Player.HealthController.IsAlive)
                        continue;
                    if (players.Distance > targetinstance.MaxDistance)
                        continue;
                    if (players.Distance < targetinstance.MinDistance)
                        continue;
                    if (!targetinstance.Enable)
                        continue;
                    if (players.Friendly)
                        continue;
                    Vector3 translatedtargetlimb = players.TargetPosScreen;
                    if (!targetinstance.IgnoreFov && Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(translatedtargetlimb.x, translatedtargetlimb.y)) > Aimbot.FOV)
                        continue;
                    /*      if (targetinstance.Manipulation)
                          {
                              if (!CanManipulate(players, TranslateTargetLimbWorld(players, Instance), targetinstance))
                                  continue;
                          }
                          else
                          {
                              if (!RaycastHelper.IsPointVisible(players.Player, TranslateTargetLimbWorld(players, Instance)))
                                  continue;
                          }*/
                    if (!RaycastHelper.IsPointVisible(players.Player, TranslateTargetLimbWorld(players, Instance)))
                        continue;
                    Aimbot.TargetPlayers = players;
                    IsTargetSuitable = true;
                    LastTargetTime = Time.time + (Instance.AimbotTargetting.TargetSwitchingTime / 1000f);
                    return;

                }
            }
           IsTargetSuitable = false; // instead of returning null lets just make a double check on the target 
        }
    }
}
