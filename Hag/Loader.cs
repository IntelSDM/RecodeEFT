using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Hag;

using System.Threading;
using System.IO;
using Hag.Helpers;
using System.Reflection;

namespace Hag
{
    [ObfuscationAttribute(Exclude = true)]
    public static class Loader
    {
        [ObfuscationAttribute(Exclude = true)]
        public static void Load()
        {
            ObfuscatedLoad.Init();
        }

    }
    class ObfuscatedLoad
    {
            public static void Init()
            {
                DumbHook FileAvaliability = new DumbHook();

                FileAvaliability.Init(
                       typeof(FilesChecker.CheckResultExtension).GetMethod(
                           "Succeed", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static),
                       typeof(Globals).GetMethod(
                           "Succeed", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                       );
                FileAvaliability.Hook();
                DumbHook FileFailed = new DumbHook();

                FileFailed.Init(
                       typeof(FilesChecker.CheckResultExtension).GetMethod(
                           "Failed", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static),
                       typeof(Globals).GetMethod(
                           "Failed", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                       );
                FileFailed.Hook();



                GameObject.DontDestroyOnLoad(HackObject);


            HackObject.AddComponent<Globals>();
            HackObject.AddComponent<Drawing.Menu.RenderMenu>();
            HackObject.AddComponent<Esp.Caching>();
            HackObject.AddComponent<Esp.ExfilEsp>();
            HackObject.AddComponent<Esp.GrenadeEsp>();
            HackObject.AddComponent<Esp.PlayerEsp>();
            HackObject.AddComponent<Esp.ItemEsp>();
            HackObject.AddComponent<Esp.ContainerEsp>();
            HackObject.AddComponent<Esp.CorpseEsp>();
            HackObject.AddComponent<Esp.Radar>();


            HackObject.AddComponent<Hooks.SilentAim>();
            HackObject.AddComponent<Hooks.GarbageCollector>();
            HackObject.AddComponent<Hooks.LootThroughWalls>();

            HackObject.AddComponent<Aimbot.Aimbot>();

            HackObject.AddComponent<Misc.WeaponMods>();
            HackObject.AddComponent<Misc.QualityOfLife>();
            HackObject.AddComponent<Misc.Binds>();
            HackObject.AddComponent<Misc.Movement>();
            HackObject.AddComponent<Misc.Hud>();
            HackObject.AddComponent<Misc.LocalPlayer>();

           GameObject.DontDestroyOnLoad(HackObject);
              
            }
            private static GameObject HackObject = new GameObject();
        }
    
    
}
