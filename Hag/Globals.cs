using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using EFT;
using EFT.Interactive;
using Hag.Helpers;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.UI;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;
using EFT.InventoryLogic;
using Hag.SDK;
namespace Hag
{
    class Globals : MonoBehaviour
    {

    
        public static Camera MainCamera;
        public static Camera ScopeCamera;
        public static EFT.InventoryLogic.Weapon LocalPlayerWeapon;
        public static EFT.Player LocalPlayer;
        public static GameWorld GameWorld;
        public static int LocalPlayerValue;

        public static string IPAddress;
        public static string Port;
        public static bool Offline = false;

        public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

        public static Dictionary<LootableContainer,Containers> ContainerDict = new Dictionary<LootableContainer,Containers>();
        public static Dictionary<Player, Players> PlayerDict = new Dictionary<Player, Players>();
        public static Dictionary<ExfiltrationPoint,ExfilPoints> ExfilDict = new Dictionary<ExfiltrationPoint, ExfilPoints>();
        public static Dictionary<LootItem,Corpses> CorpseDict = new Dictionary<LootItem,Corpses>();
        public static Dictionary<LootItem,Items> ItemDict = new Dictionary<LootItem,Items>();
        public static Dictionary<Throwable,Grenades> GrenadeDict = new Dictionary<Throwable,Grenades>();

        public static List<string> MachineGuns = new List<string>();
        public static List<string> SniperRifles = new List<string>();
        public static List<string> SMGs = new List<string>();
        public static List<string> Shotguns = new List<string>();
        public static List<string> Revolvers = new List<string>();
        public static List<string> Pistols = new List<string>();
        public static List<string> MarksmanRifles = new List<string>();
        public static List<string> AssaultRifles = new List<string>();
        public static List<string> AssaultCarbines = new List<string>();

        public static Hag.Configs.Config Config = new Configs.Config();

        public static bool EndedFrame = true;
        public static bool AlteringWhitelist = false;
        public static float FlyHackValue = 0;
        public static Drawing.Menu.RenderMenu MenuInstance;


        public static bool BattleMode = false;
        public static bool PlayerContents = true;
        public static bool ScavContents = true;
        public static bool ScavPlayerContents = true;
        public static bool BossContents = true;
        public static bool ContainerContents = true;
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();

        }

        #region Weapon config
        public static void AddWeaponConfig() // call this one time if it isn't in the config already
        {
            foreach (var item in Helpers.ItemPriceHelper.list)
            {
                if (item.Value.subtype == item_subtype.AssaultCarbine)
                    AssaultCarbines.Add(item.Key);
                if (item.Value.subtype == item_subtype.MachineGun)
                    if (!item.Value.name.Contains("weapon_"))
                        MachineGuns.Add(item.Key);
                if (item.Value.subtype == item_subtype.Smg)
                    if (!item.Value.name.Contains("weapon_"))
                        SMGs.Add(item.Key);
                if (item.Value.subtype == item_subtype.AssaultRifle)
                    if (!item.Value.name.Contains("weapon_"))
                        AssaultRifles.Add(item.Key);
                if (item.Value.subtype == item_subtype.SniperRifle)
                    if (!item.Value.name.Contains("weapon_"))
                        SniperRifles.Add(item.Key);
                if (item.Value.subtype == item_subtype.Shotgun)
                    if (!item.Value.name.Contains("weapon_"))
                        Shotguns.Add(item.Key);
                if (item.Value.subtype == item_subtype.Revolver)
                    if (!item.Value.name.Contains("weapon_"))
                        Revolvers.Add(item.Key);
                if (item.Value.subtype == item_subtype.Pistol)
                    if (!item.Value.name.Contains("weapon_"))
                        Pistols.Add(item.Key);
                if (item.Value.subtype == item_subtype.MarksmanRifle)
                    if (!item.Value.name.Contains("weapon_"))
                        MarksmanRifles.Add(item.Key);
            }
            MachineGuns.Sort();
            AssaultCarbines.Sort();
            AssaultRifles.Sort();
            SMGs.Sort();
            SniperRifles.Sort();
            Shotguns.Sort();
            Revolvers.Sort();
            Pistols.Sort();
            MarksmanRifles.Sort();
            foreach (string item in MachineGuns)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false,false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in SniperRifles)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in SMGs)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in Shotguns)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in Revolvers)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in Pistols)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in MarksmanRifles)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in AssaultRifles)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
            foreach (string item in AssaultCarbines)
            {
                if (!Globals.Config.Weapon.WeaponDict.ContainsKey(item))
                    Globals.Config.Weapon.WeaponDict.Add(item, new Configs.Weapon(false, false));
                if (!Globals.Config.Aimbot.WeaponDict.ContainsKey(item))
                    Globals.Config.Aimbot.WeaponDict.Add(item, new Configs.Aimbot());
            }
        }
        #endregion

        void StartObfuscated()
        {
           
          
            Helpers.ConfigHelper.CreateEnvironment();
            Helpers.ColourHelper.AddColours();
            AddWeaponConfig();
            MenuInstance = new Drawing.Menu.RenderMenu();
            Drawing.Draw drawing = new Drawing.Draw();
            drawing.Start();

            ShaderHelper.GetShader();
        }
        [ObfuscationAttribute(Exclude = true)]
        public static bool Failed(FilesChecker.ICheckResult result)
        {
            return false;
        }
        [ObfuscationAttribute(Exclude = true)]
        public static bool Succeed(FilesChecker.ICheckResult result)
        {
            return true;
        }
    }
}
