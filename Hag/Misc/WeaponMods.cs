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
    /*
    TODO
    When i have some free time i need to recode this, change the shitloads of methods to just getting a config instance as in the aimbot class
    */
    class WeaponMods : MonoBehaviour
    {

        public static Configs.Weapon GetSpeedFactorInstance(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.HitSpeedMultiplier)
                        return instance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.HitSpeedMultiplier)
                    return categoryinstance;
            }
            return null;
        }
        Dictionary<string, Vector2> OriginalRecoilxy = new Dictionary<string, Vector2>();
        Dictionary<string, Vector2> OriginalRecoilz = new Dictionary<string, Vector2>();

        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(UpdateWeapons());
        }
        #region Recoil Stats
        bool NoRecoilEnabled(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoRecoil)
                    return true;
            }
            return false;
        }

        int RecoilX(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
               
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoilx;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoilx;
            }
            return 1;
        }
        int RecoilY(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;

            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoRecoil)
                        return instance.Recoily;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoRecoil)
                    return categoryinstance.Recoily;
            }
            return 1;
        }
        bool NoSwayEnabled(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoSway)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoSway)
                    return true;
            }
            return false;
        }
        bool IsFiremodeEnabled(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.UnlockFireModes)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.UnlockFireModes)
                    return true;
            }
            return false;
        }
        bool NoMalfunctionEnabled(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoMalfunction)
                    return true;
            }
            return false;
        }
        bool FastFireEnabled(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.FastFire)
                    return true;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return true;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.FastFire)
                    return true;
            }
            return false;
        }
        int MalfunctionChance(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;

            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoMalfunction)
                        return instance.MalfunctionChance;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoMalfunction)
                    return categoryinstance.MalfunctionChance;
            }
            return 1;
        }
        int SwayAmount(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;

            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.NoSway)
                        return instance.SwayAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.NoSway)
                    return categoryinstance.SwayAmount;
            }
            return 1;
        }

        int FastFireAmount(string template)
        {
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultCarbine)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultCarbine;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;

            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.AssaultRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultAssaultRifle;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.MachineGun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultMachineGun;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.SniperRifle)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSniperRifle;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Smg)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultSMG;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Shotgun)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultShotgun;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Revolver)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultRevolver;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            if (ItemPriceHelper.list[template].subtype == item_subtype.Pistol)
            {
                if (Globals.Config.Weapon.WeaponDict.ContainsKey(template))
                {
                    Configs.Weapon instance = Globals.Config.Weapon.WeaponDict[template];
                    if (instance.FastFire)
                        return instance.FastFireAmount;
                }
                Configs.Weapon categoryinstance = Globals.Config.Weapon.DefaultPistol;
                if (categoryinstance.FastFire)
                    return categoryinstance.FastFireAmount;
            }
            return 1;
        }
        #endregion
        void NoRecoil()
        {
            if (Globals.LocalPlayerWeapon == null)
                return;
            if (Globals.LocalPlayer == null)
                return;
            if (!(Globals.LocalPlayer?.HandsController?.Item is Weapon))
                return;
            try
            {

                if (!OriginalRecoilxy.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalRecoilxy.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy);
                if (!OriginalRecoilz.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalRecoilz.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ);
                if (!NoRecoilEnabled(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                {// resset the recoil values as no recoil is disabled
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = new Vector2(OriginalRecoilxy[Globals.LocalPlayer?.HandsController?.Item.TemplateId].x, OriginalRecoilxy[Globals.LocalPlayer?.HandsController?.Item.TemplateId].y);
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = new Vector2(OriginalRecoilz[Globals.LocalPlayer?.HandsController?.Item.TemplateId].x, OriginalRecoilz[Globals.LocalPlayer?.HandsController?.Item.TemplateId].y);
                    return;
                }


                Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = new Vector2(OriginalRecoilxy[Globals.LocalPlayer?.HandsController?.Item.TemplateId].x * ((float)RecoilX(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f), OriginalRecoilxy[Globals.LocalPlayer?.HandsController?.Item.TemplateId].y * ((float)RecoilY(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f));
                Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = new Vector2(OriginalRecoilz[Globals.LocalPlayer?.HandsController?.Item.TemplateId].x * ((float)RecoilX(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f), OriginalRecoilz[Globals.LocalPlayer?.HandsController?.Item.TemplateId].y * ((float)RecoilY(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f));
            }
            catch { }
            }
         Dictionary<string, float> OriginalBreathIntensity = new Dictionary<string, float>();
        Dictionary<string, float> OriginalMotionReact = new Dictionary<string, float>();
        Dictionary<string, float> OriginalAimingDisplacement = new Dictionary<string, float>();
        Dictionary<string, float> OriginalBreathOverweight = new Dictionary<string, float>();
        Dictionary<string, float> OirignalHipPenalty = new Dictionary<string, float>();
        Dictionary<string, bool> OriginalWalkEffector = new Dictionary<string, bool>();
        void NoSway()
        {
            if (Globals.LocalPlayerWeapon == null)
                return;
            if (Globals.LocalPlayer == null)
                return;
            if (!(Globals.LocalPlayer?.HandsController?.Item is Weapon))
                return;
            try
            {
                if (!OriginalBreathIntensity.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalBreathIntensity.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity);
                if (!OriginalMotionReact.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalMotionReact.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity);
                if (!OriginalAimingDisplacement.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalAimingDisplacement.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.AimingDisplacementStr);
                if (!OriginalBreathOverweight.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalBreathOverweight.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Overweight);
                if (!OirignalHipPenalty.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OirignalHipPenalty.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.HipPenalty);
                //  if (!OriginalWalkEffector.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                //    OriginalWalkEffector.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, value: Globals.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled);

                if (!NoSwayEnabled(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                {
                    //  Globals.LocalPlayer.ProceduralWeaponAnimation.AimingSpeed
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = OriginalBreathIntensity[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = OriginalMotionReact[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    Globals.LocalPlayer.ProceduralWeaponAnimation.AimingDisplacementStr = OriginalAimingDisplacement[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Overweight = OriginalBreathOverweight[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.HipPenalty = OirignalHipPenalty[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    //     if (Globals.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled == false)
                    //     Globals.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = true;
                    return;
                }
                Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = OriginalBreathIntensity[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)SwayAmount(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);
                Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = OriginalMotionReact[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)SwayAmount(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);
                Globals.LocalPlayer.ProceduralWeaponAnimation.AimingDisplacementStr = OriginalAimingDisplacement[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)SwayAmount(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);
                Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Overweight = OriginalBreathOverweight[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)SwayAmount(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);
                Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.HipPenalty = OirignalHipPenalty[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)SwayAmount(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);

                if (SwayAmount(Globals.LocalPlayer?.HandsController?.Item.TemplateId) == 0)
                    Globals.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
            }
            catch { }

        }
        Dictionary<string, EFT.InventoryLogic.Weapon.EFireMode[]> OriginalFireModes = new Dictionary<string, EFT.InventoryLogic.Weapon.EFireMode[]>();
        void UnlockFireModes()
        {
            if (Globals.LocalPlayerWeapon == null)
                return;
            if (Globals.LocalPlayer == null)
                return;
            if (!(Globals.LocalPlayer?.HandsController?.Item is Weapon))
                return;
            try
            {
                if (!OriginalFireModes.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalFireModes.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayerWeapon.Template.weapFireType);
                if (!IsFiremodeEnabled(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                {
                    Globals.LocalPlayerWeapon.Template.weapFireType = OriginalFireModes[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    return;
                }
                EFT.InventoryLogic.Weapon.EFireMode[] firemodes = new EFT.InventoryLogic.Weapon.EFireMode[4]
                {
              EFT.InventoryLogic.Weapon.EFireMode.single,
              EFT.InventoryLogic.Weapon.EFireMode.burst,
              EFT.InventoryLogic.Weapon.EFireMode.semiauto,
             EFT.InventoryLogic.Weapon.EFireMode.fullauto
                };
                Globals.LocalPlayerWeapon.Template.weapFireType = firemodes;
            }
            catch { }
        }
        Dictionary<string, float> OriginalFeedChance = new Dictionary<string, float>();
        Dictionary<string, float> OriginalMisFireChance = new Dictionary<string, float>();
        Dictionary<string, float> OriginalBaseMalfunctionChance = new Dictionary<string, float>();
        void NoMalfunction()
        {
            if (Globals.LocalPlayerWeapon == null)
                return;
            if (Globals.LocalPlayer == null)
                return;
            if (!(Globals.LocalPlayer?.HandsController?.Item is Weapon))
                return;
            try
            {
                if (!OriginalFeedChance.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalFeedChance.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfFeedChance);
                if (!OriginalMisFireChance.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalMisFireChance.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfMisfireChance);
                if (!OriginalBaseMalfunctionChance.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalBaseMalfunctionChance.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayerWeapon.Template.BaseMalfunctionChance);

                if (!NoMalfunctionEnabled(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                {
                    Globals.LocalPlayerWeapon.Template.BaseMalfunctionChance = OriginalBaseMalfunctionChance[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfMisfireChance = OriginalMisFireChance[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfFeedChance = OriginalFeedChance[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    return;
                }
                Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfFeedChance = OriginalFeedChance[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)MalfunctionChance(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);
                Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfMisfireChance = OriginalMisFireChance[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)MalfunctionChance(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);
                Globals.LocalPlayerWeapon.Template.BaseMalfunctionChance = OriginalBaseMalfunctionChance[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)MalfunctionChance(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f);

            }
            catch { }
            
            }
        Dictionary<string, int> OriginalFastFire = new Dictionary<string, int>();
        void FastFire()
        {
            if (Globals.LocalPlayerWeapon == null)
                return;
            if (Globals.LocalPlayer == null)
                return;
            if (!(Globals.LocalPlayer?.HandsController?.Item is Weapon))
                return;
            try
            {
                if (!OriginalFastFire.ContainsKey(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                    OriginalFastFire.Add(Globals.LocalPlayer?.HandsController?.Item.TemplateId, Globals.LocalPlayerWeapon.Template.bFirerate);
                if (!FastFireEnabled(Globals.LocalPlayer?.HandsController?.Item.TemplateId))
                {
                    Globals.LocalPlayerWeapon.Template.bFirerate = OriginalFastFire[Globals.LocalPlayer?.HandsController?.Item.TemplateId];
                    return;
                }
                Globals.LocalPlayerWeapon.Template.bFirerate = (int)(OriginalFastFire[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * (OriginalFastFire[Globals.LocalPlayer?.HandsController?.Item.TemplateId] * ((float)MalfunctionChance(Globals.LocalPlayer?.HandsController?.Item.TemplateId) / 100f)));
            }
            catch { }
            }
        IEnumerator UpdateWeapons()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null)
                    goto END;
                NoRecoil();
                NoSway();
                UnlockFireModes();
                NoMalfunction();
                FastFire();
                END:
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
