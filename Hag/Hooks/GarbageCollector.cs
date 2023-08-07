using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using Hag.Helpers;
using UnityEngine.Scripting;
using System.Reflection;

namespace Hag.Hooks
{
    class GarbageCollector : MonoBehaviour
    {
        // the names used in here are all vague since some cant be obfuscated which means we call another obfuscated function so to hide the functionality of the hook we make the function names vague

        // this is needed or your pc will freeze closing the game
        DumbHook Hook = new DumbHook();
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            Hook.Init(typeof(Player).Assembly.GetType("").GetMethod("GCEnabled", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static), typeof(GarbageCollector).GetMethod("Func", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Hook.Hook();
        }

        [ObfuscationAttribute(Exclude = true)]
        public static bool Func()
        {
            return GCEnabledObfuscated();
        }
        public static bool GCEnabledObfuscated()
        {
            //   UnityEngine.Scripting.GarbageCollector.GCMode = UnityEngine.Scripting.GarbageCollector.Mode.Enabled;
            if (Globals.Config.Menu.GarbageCollection)
                return true;
            else
                return false;
        }
    }
}
