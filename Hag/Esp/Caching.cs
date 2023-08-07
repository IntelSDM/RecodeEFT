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
using Comfort.Common;
using EFT.UI;
using UnityEngine.Scripting;
using Hag.Helpers;

namespace Hag.Esp
{
    class Caching : MonoBehaviour
    {
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(CacheConstants());
            StartCoroutine(CachePlayers());
            StartCoroutine(CacheItems());
            StartCoroutine(CacheExfil());
            StartCoroutine(CacheContainers());
            StartCoroutine(CacheGrenades());
        }//Globals.GameWorld.MineManager
        IEnumerator CachePlayers()
        {

            for (; ; )
            {
              
                if (Globals.GameWorld == null)
                    goto END;
                if (!Globals.EndedFrame)
                    continue;
              
                if ((Globals.MainCamera == null || Globals.GameWorld == null )  && Globals.PlayerDict.Count > 0)
                     Globals.PlayerDict.Clear();
                foreach ( Player player in Globals.GameWorld.RegisteredPlayers)    
                {
                    if (player.IsYourPlayer)
                    {
                        Globals.LocalPlayer = player;
                        continue;
                    }
                    if (!Globals.PlayerDict.ContainsKey(player))
                        Globals.PlayerDict.Add(player, new SDK.Players(player));
                  //  Globals.PlayerList.Add(new SDK.Players(player));
                 //   player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint.Bones
                }
                END:
                yield return new WaitForSeconds(1f);
            }
        }
        IEnumerator CacheGrenades()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null)
                    goto END;
                if (!Globals.EndedFrame)
                    continue;
                if ((Globals.MainCamera == null || Globals.GameWorld == null) && Globals.GrenadeDict.Count > 0)
                    Globals.GrenadeDict.Clear();
                for (int i = 0; i < Globals.GameWorld.Grenades.Count; i++)
                {
                    Throwable throwable = Globals.GameWorld.Grenades.GetByIndex(i);
                    Grenade grenade = throwable as Grenade;
                    if (!Globals.GrenadeDict.ContainsKey(throwable))
                        Globals.GrenadeDict.Add(throwable, new SDK.Grenades(grenade));

                }
                END:
                yield return new WaitForSeconds(0.05f);
            }
        }
        IEnumerator CacheItems()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null)
                    goto END;
                if (!Globals.EndedFrame)
                    continue;
                if ((Globals.MainCamera == null || Globals.GameWorld == null) && Globals.CorpseDict.Count > 0)
                    Globals.CorpseDict.Clear();
                if ((Globals.MainCamera == null || Globals.GameWorld == null) && Globals.ItemDict.Count > 0)
                    Globals.ItemDict.Clear();
                for (int i = 0; i < Globals.GameWorld.LootItems.Count; i++)
                {
                    LootItem item = Globals.GameWorld.LootItems.GetByIndex(i);
                    if (item == null)
                        continue;

                    if (!(item.Item.TemplateId == "55d7217a4bdc2d86028b456d".Localized()))
                    {
                        // none corpse
                        if (!Globals.ItemDict.ContainsKey(item))
                            Globals.ItemDict.Add(item, new SDK.Items(item));
                    }

                    if (item.Item.TemplateId == "55d7217a4bdc2d86028b456d".Localized())
                    {
                        if (!Globals.CorpseDict.ContainsKey(item))
                            Globals.CorpseDict.Add(item, new SDK.Corpses(item));
                        // corpse
                    }

                }
                END:
                yield return new WaitForSeconds(1f);
            }
        }
        IEnumerator CacheExfil()
        {
            for (; ; )
            {

                if (Globals.GameWorld == null)
                    goto END;
                if (!Globals.EndedFrame)
                    continue;
                if ((Globals.MainCamera == null || Globals.GameWorld == null) && Globals.ExfilDict.Count > 0)
                    Globals.ExfilDict.Clear();
                foreach (ExfiltrationPoint exfilpoint
                in LocationScene.GetAllObjects<ExfiltrationPoint>(false))
                {
                    if (exfilpoint == null)
                        continue;

                    if (!Globals.ExfilDict.ContainsKey(exfilpoint))
                        Globals.ExfilDict.Add(exfilpoint, new SDK.ExfilPoints(exfilpoint));

                }
                END:
                yield return new WaitForSeconds(10f);
            }
        }
        IEnumerator CacheContainers()
        {
            for (; ; )
            {

                if (Globals.GameWorld == null)
                    goto END;
                if (!Globals.EndedFrame)
                    continue;
                if ((Globals.MainCamera == null || Globals.GameWorld == null) && Globals.ContainerDict.Count > 0)
                    Globals.ContainerDict.Clear();

                foreach (LootableContainer container
                in LocationScene.GetAllObjects<LootableContainer>(false))
                {
                    if (container == null)
                        continue;

                    if (!Globals.ContainerDict.ContainsKey(container))
                        Globals.ContainerDict.Add(container, new SDK.Containers(container));
                }
                END:
                yield return new WaitForSeconds(5f);
            }
        }

        IEnumerator CacheConstants()
        {
            for (; ; )
            {
             if(Camera.main !=null)
                    Globals.MainCamera = Camera.main;
             if(Singleton<GameWorld>.Instance != null)
                    Globals.GameWorld = Singleton<GameWorld>.Instance;
             
                    if (Globals.LocalPlayer != null)
                    if (Globals.LocalPlayer?.HandsController?.Item is Weapon)
                        Globals.LocalPlayerWeapon = (Weapon)Globals.LocalPlayer?.HandsController?.Item;
                if (MonoBehaviourSingleton<PreloaderUI>.Instance != null)
                {
                    if (MonoBehaviourSingleton<PreloaderUI>.Instance.GetPrivateField<string>(ResolvedNames.SessionID) == null)
                        Globals.Offline = true;
                    else
                        Globals.Offline = false;
                }
            /*    try
                {
                  //  Globals.IPAddress = UnityEngine.Networking.NetworkClient.allClients.Last().serverIp;
                  //  Globals.Port = UnityEngine.Networking.NetworkClient.allClients.Last().serverPort.ToString();
                }
                catch { }
                    try
                    {
                    if (MonoBehaviourSingleton<PreloaderUI>.Instance.GetPrivateField<string>(ResolvedNames.SessionID) == null)
                        Globals.Offline = true;
                    else
                        Globals.Offline = false;
                }
                catch { }
            
                try
                {
         //           Globals.LocalPlayer.PlayerBody.Dispose();
                 //   Globals.GameWorld.RemovePlayerObject(Globals.LocalPlayer);
              //      Globals.GameWorld.SpeedLimitsEnabled = false;
              //      Time.timeScale = 2;
                }
                catch { }*/
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
