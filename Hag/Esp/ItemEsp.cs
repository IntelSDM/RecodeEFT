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
using System.IO;
namespace Hag.Esp
{
    class ItemEsp : MonoBehaviour
    {
        static int HighestValue = 0;
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(UpdateEsp());
        }
   
        IEnumerator UpdateEsp()
        {
            for (; ; )
            {
                try
                {
                    if ((Globals.MainCamera == null || Globals.GameWorld == null))
                        HighestValue = 0;

                        if ((Globals.MainCamera == null || Globals.GameWorld == null))
                        goto END;
              
                    foreach (SDK.Items items in Globals.ItemDict.Values)
                    {
                        if (items.Item == null)
                            continue;
                        try
                        {
                            items.W2SPos = EspConstants.WorldToScreenPoint(items.Item.transform.position);
                            items.Distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, items.Item.transform.position);
                            if (items.Value > HighestValue)
                                HighestValue = items.Value;
                            if (items.Item.Item.QuestItem && Globals.Config.ItemMisc.ShowInactiveQuestItems)
                            {
                                   items.Item.gameObject.SetActive(true);
                         //      items.Item.gameObject.activeSelf
                                //    string[] ids = new string[] { Globals.GameWorld.CurrentProfileId };
                           //         items.Item.ValidProfiles = ids;
                                
                            }
                             
                        }
                        catch(Exception ex) { System.IO.File.WriteAllText("item cache 1", $"{ex.Message}\n{ex.ToString()}"); }
                    }
                }
                catch(Exception ex) { System.IO.File.WriteAllText("item cache 2", $"{ex.Message}\n{ex.ToString()}"); }
                END:
                    yield return new WaitForEndOfFrame();
            }
        }
        public bool IsItemEnabled(SDK.Items items)
        {
            if ((Globals.Config.Items.WhitelistedItems.Contains(items.Item.TemplateId) || (items.RequiredForQuest && !items.Item.Item.Template.QuestItem)) && Globals.Config.Items.Whitelist.Enable )
                return true;
            if (Globals.Config.Items.BlacklistedItems.Contains(items.Item.TemplateId))
                return false;
            if (items.OurItem.subtype == Helpers.item_subtype.Fuel && Globals.Config.Items.Fuel.Enable)
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Ammo || items.OurItem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.Items.Ammo.Enable)
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Food || items.OurItem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.Items.FoodDrink.Enable)
                return true;
            if ((items.Item.Item.QuestItem && Globals.Config.Items.QuestItems.Enable) && (items.RequiredForQuest || Globals.Config.Items.QuestItems.ShowInactiveQuestItems))
                return true;
            if ((items.OurItem.type == Helpers.item_type.Key && items.OurItem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.Items.Key.Enable)
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Keycard && Globals.Config.Items.Keycard.Enable)
                return true;
            if (items.OurItem.subtype == item_subtype.Backpack && Globals.Config.Items.Backpacks.Enable)
                return true;
            if ((items.OurItem.type == item_type.Mod || items.OurItem.type == item_type.Muzzle || items.OurItem.type == item_type.Sights || items.OurItem.type == item_type.SpecialScope || items.OurItem.type == item_type.MasterMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.FunctionalMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.MasterMod) && Globals.Config.Items.Attachments.Enable)
                return true;
            if (items.OurItem.subtype == item_subtype.ArmoredEquipment && Globals.Config.Items.Armour.Enable)
                return true;
            if (items.OurItem.subtype == item_subtype.Equipment && Globals.Config.Items.Clothing.Enable)
                return true;
            if (items.OurItem.type == item_type.RepairKits && Globals.Config.Items.RepairKits.Enable)
                return true;
            if (items.OurItem.type == item_type.Meds && Globals.Config.Items.Meds.Enable)
                return true;
            if (items.OurItem.type == item_type.Weapon && Globals.Config.Items.Weapon.Enable)
                return true;
            if (items.OurItem.type == item_type.Item && Globals.Config.Items.SpecialItems.Enable)
                return true;
            if (items.OurItem.type == item_type.BarterItem && Globals.Config.Items.BarterItems.Enable)
                return true;
            if (items.OurItem.type == item_type.CompoundItem && Globals.Config.Items.Cases.Enable)
                return true;
            
            return false;
        }
        public bool IsItemInDistance(SDK.Items items)
        {
            if ((Globals.Config.Items.WhitelistedItems.Contains(items.Item.TemplateId) || (items.RequiredForQuest && !items.Item.Item.Template.QuestItem)) && (items.Distance <= Globals.Config.Items.Whitelist.MaxDistance))
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Fuel && (items.Distance <= Globals.Config.Items.Fuel.MaxDistance))
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Ammo || items.OurItem.subtype == Helpers.item_subtype.AmmoBox) && (items.Distance <= Globals.Config.Items.Ammo.MaxDistance))
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Food || items.OurItem.subtype == Helpers.item_subtype.FoodDrink) && (items.Distance <= Globals.Config.Items.FoodDrink.MaxDistance))
                return true;
            if ((items.Item.Item.QuestItem && (items.RequiredForQuest || Globals.Config.Items.QuestItems.ShowInactiveQuestItems) && items.Distance <= Globals.Config.Items.QuestItems.MaxDistance))
                return true;
            if ((items.OurItem.type == Helpers.item_type.Key && items.OurItem.subtype != Helpers.item_subtype.Keycard) && (items.Distance <= Globals.Config.Items.Key.MaxDistance))
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Keycard && (items.Distance <= Globals.Config.Items.Keycard.MaxDistance))
                return true;
            if (items.OurItem.subtype == item_subtype.Backpack && (items.Distance <= Globals.Config.Items.Backpacks.MaxDistance))
                return true;
            if ((items.OurItem.type == item_type.Mod || items.OurItem.type == item_type.Muzzle || items.OurItem.type == item_type.Sights || items.OurItem.type == item_type.SpecialScope || items.OurItem.type == item_type.MasterMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.FunctionalMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.MasterMod) && (items.Distance <= Globals.Config.Items.Attachments.MaxDistance))
                return true;
            if (items.OurItem.subtype == item_subtype.ArmoredEquipment &&  (items.Distance <= Globals.Config.Items.Armour.MaxDistance))
                return true;
            if (items.OurItem.subtype == item_subtype.Equipment &&  (items.Distance <= Globals.Config.Items.Clothing.MaxDistance))
                return true;
            if (items.OurItem.type == item_type.RepairKits && (items.Distance <= Globals.Config.Items.RepairKits.MaxDistance))
                return true;
            if (items.OurItem.type == item_type.Meds && (items.Distance <= Globals.Config.Items.Meds.MaxDistance))
                return true;
            if (items.OurItem.type == item_type.Weapon && (items.Distance <= Globals.Config.Items.Weapon.MaxDistance))
                return true;
            if (items.OurItem.type == item_type.Item && (items.Distance <= Globals.Config.Items.SpecialItems.MaxDistance))
                return true;
            if (items.OurItem.type == item_type.BarterItem && (items.Distance <= Globals.Config.Items.BarterItems.MaxDistance))
                return true;
            if (items.OurItem.type == item_type.CompoundItem && (items.Distance <= Globals.Config.Items.Cases.MaxDistance))
                return true;

            return false;
        }
        public bool IsItemInValue(SDK.Items items)
        {
            if (items.Value < 1000 && !items.Item.Item.QuestItem)
                return false;
            if ((Globals.Config.Items.WhitelistedItems.Contains(items.Item.TemplateId) || (items.RequiredForQuest && !items.Item.Item.Template.QuestItem)) && (!Globals.Config.Items.Whitelist.UseItemSlotPrice && items.Value >= Globals.Config.Items.Whitelist.MinPrice || Globals.Config.Items.Whitelist.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Whitelist.MinPrice ||  (items.RequiredForQuest && !items.Item.Item.Template.QuestItem)))
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Fuel && (!Globals.Config.Items.Fuel.UseItemSlotPrice && items.Value >= Globals.Config.Items.Fuel.MinPrice || Globals.Config.Items.Fuel.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Fuel.MinPrice))
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Ammo || items.OurItem.subtype == Helpers.item_subtype.AmmoBox) && (!Globals.Config.Items.Ammo.UseItemSlotPrice && items.Value >= Globals.Config.Items.Ammo.MinPrice || Globals.Config.Items.Ammo.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Ammo.MinPrice))
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Food || items.OurItem.subtype == Helpers.item_subtype.FoodDrink) && (!Globals.Config.Items.FoodDrink.UseItemSlotPrice && items.Value >= Globals.Config.Items.FoodDrink.MinPrice || Globals.Config.Items.FoodDrink.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.FoodDrink.MinPrice))
                return true;
            if ((items.Item.Item.QuestItem && Globals.Config.Items.QuestItems.Enable) && (items.RequiredForQuest || Globals.Config.Items.QuestItems.ShowInactiveQuestItems))
                return true; // just to allow quest items to show
            if ((items.OurItem.type == Helpers.item_type.Key && items.OurItem.subtype != Helpers.item_subtype.Keycard) && (!Globals.Config.Items.Key.UseItemSlotPrice && items.Value >= Globals.Config.Items.Key.MinPrice || Globals.Config.Items.Key.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Key.MinPrice))
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Keycard && (!Globals.Config.Items.Keycard.UseItemSlotPrice && items.Value >= Globals.Config.Items.Keycard.MinPrice || Globals.Config.Items.Keycard.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Keycard.MinPrice))
                return true;
            if (items.OurItem.subtype == item_subtype.Backpack && (!Globals.Config.Items.Backpacks.UseItemSlotPrice && items.Value >= Globals.Config.Items.Backpacks.MinPrice || Globals.Config.Items.Backpacks.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Backpacks.MinPrice))
                return true;
            if ((items.OurItem.type == item_type.Mod || items.OurItem.type == item_type.Muzzle || items.OurItem.type == item_type.Sights || items.OurItem.type == item_type.SpecialScope || items.OurItem.type == item_type.MasterMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.FunctionalMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.MasterMod) && (!Globals.Config.Items.Attachments.UseItemSlotPrice && items.Value >= Globals.Config.Items.Attachments.MinPrice || Globals.Config.Items.Attachments.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Attachments.MinPrice))
                return true;
            if (items.OurItem.subtype == item_subtype.ArmoredEquipment && (!Globals.Config.Items.Armour.UseItemSlotPrice && items.Value >= Globals.Config.Items.Armour.MinPrice || Globals.Config.Items.Armour.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Armour.MinPrice))
                return true;
            if (items.OurItem.subtype == item_subtype.Equipment && (!Globals.Config.Items.Clothing.UseItemSlotPrice && items.Value >= Globals.Config.Items.Clothing.MinPrice || Globals.Config.Items.Clothing.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Clothing.MinPrice))
                return true;
            if (items.OurItem.type == item_type.RepairKits && (!Globals.Config.Items.RepairKits.UseItemSlotPrice && items.Value >= Globals.Config.Items.RepairKits.MinPrice ||Globals.Config.Items.RepairKits.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.RepairKits.MinPrice))
                return true;
            if (items.OurItem.type == item_type.Meds && (!Globals.Config.Items.Meds.UseItemSlotPrice && items.Value >= Globals.Config.Items.Meds.MinPrice || Globals.Config.Items.Meds.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Meds.MinPrice))
                return true;
            if (items.OurItem.type == item_type.Weapon && (!Globals.Config.Items.Weapon.UseItemSlotPrice && items.Value >= Globals.Config.Items.Weapon.MinPrice || Globals.Config.Items.Weapon.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Weapon.MinPrice))
                return true;
             if (items.OurItem.type == item_type.Item && (!Globals.Config.Items.SpecialItems.UseItemSlotPrice && items.Value >= Globals.Config.Items.SpecialItems.MinPrice || Globals.Config.Items.SpecialItems.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.SpecialItems.MinPrice))
                return true;
            if (items.OurItem.type == item_type.BarterItem && (!Globals.Config.Items.BarterItems.UseItemSlotPrice && items.Value >= Globals.Config.Items.BarterItems.MinPrice || Globals.Config.Items.BarterItems.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.BarterItems.MinPrice))
                return true;
            if (items.OurItem.type == item_type.CompoundItem && (!Globals.Config.Items.Cases.UseItemSlotPrice && items.Value >= Globals.Config.Items.Cases.MinPrice || Globals.Config.Items.Cases.UseItemSlotPrice && items.OurItem.price_per_slot >= Globals.Config.Items.Cases.MinPrice))
                return true;
            return false;
        }
        void DrawItem(Direct2DRenderer renderer, Direct2DFont font, SDK.Items items)
        {
            string[] EspTextBottom = new string[4];
            string[] EspTextTop = new string[4];
            string[] EspTextRight = new string[4];
            string[] EspTextLeft = new string[4];
            Color32 unitycolour = new Color32();
            Direct2DColor dxcolour = new Direct2DColor();
            if (Globals.Config.Items.WhitelistedItems.Contains(items.Item.TemplateId) || items.RequiredForQuest)
            {
                unitycolour = ColourHelper.GetColour("Item Whitelisted Item Text Colour");

                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Whitelist.MaxDistance + 1));
                int valuealpha = 255;
               
                    valuealpha = (int)Mathf.Lerp(255, 50, (float)items.Value / (float)(HighestValue + 1));
                if (Globals.Config.Items.Whitelist.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Whitelist.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Whitelist.Name.Enable)
                {
                    switch (Globals.Config.Items.Whitelist.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Whitelist.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Whitelist.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Whitelist.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Whitelist.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Whitelist.Distance.Enable)
                {
                    switch (Globals.Config.Items.Whitelist.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Whitelist.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Whitelist.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Whitelist.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Whitelist.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Whitelist.Value.Enable)
                {
                    if (Globals.Config.Items.Whitelist.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Whitelist.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Whitelist.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Whitelist.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Whitelist.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Whitelist.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Whitelist.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Whitelist.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Whitelist.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Whitelist.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Whitelist.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Whitelist.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Whitelist.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Whitelist.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Whitelist.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Whitelist.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END; // incase the whitelisted item is elsewhere in here, we wont draw twice
            }
            if (items.OurItem.subtype == Helpers.item_subtype.Fuel)
            {
                unitycolour = ColourHelper.GetColour("Item Fuel Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Fuel.MaxDistance + 1));

               
                   int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Fuel.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Fuel.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Fuel.Name.Enable)
                {
                    switch (Globals.Config.Items.Fuel.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Fuel.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Fuel.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Fuel.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Fuel.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Fuel.Distance.Enable)
                {
                    switch (Globals.Config.Items.Fuel.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Fuel.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Fuel.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Fuel.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Fuel.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Fuel.Value.Enable)
                {
                    if (Globals.Config.Items.Fuel.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Fuel.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Fuel.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Fuel.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Fuel.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Fuel.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Fuel.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Fuel.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Fuel.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Fuel.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Fuel.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Fuel.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Fuel.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Fuel.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Fuel.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Fuel.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if ((items.OurItem.subtype == Helpers.item_subtype.Ammo || items.OurItem.subtype == Helpers.item_subtype.AmmoBox))
            {
                
                unitycolour = ColourHelper.GetColour("Item Ammo Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Ammo.MaxDistance + 1));
             
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Ammo.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Ammo.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Ammo.Name.Enable)
                {
                    switch (Globals.Config.Items.Ammo.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Ammo.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Ammo.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Ammo.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Ammo.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Ammo.Distance.Enable)
                {
                    switch (Globals.Config.Items.Ammo.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Ammo.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Ammo.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Ammo.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Ammo.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Ammo.Value.Enable)
                {
                    if (Globals.Config.Items.Ammo.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Ammo.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Ammo.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Ammo.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Ammo.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Ammo.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Ammo.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Ammo.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Ammo.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Ammo.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Ammo.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Ammo.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Ammo.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Ammo.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Ammo.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Ammo.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if ((items.OurItem.subtype == Helpers.item_subtype.Food || items.OurItem.subtype == Helpers.item_subtype.FoodDrink))
            {
                unitycolour = ColourHelper.GetColour("Item Food/Drink Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.FoodDrink.MaxDistance + 1));
              
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.FoodDrink.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.FoodDrink.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.FoodDrink.Name.Enable)
                {
                    switch (Globals.Config.Items.FoodDrink.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.FoodDrink.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.FoodDrink.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.FoodDrink.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.FoodDrink.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.FoodDrink.Distance.Enable)
                {
                    switch (Globals.Config.Items.FoodDrink.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.FoodDrink.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.FoodDrink.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.FoodDrink.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.FoodDrink.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.FoodDrink.Value.Enable)
                {
                    if (Globals.Config.Items.FoodDrink.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.FoodDrink.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.FoodDrink.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.FoodDrink.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.FoodDrink.ItemType.Enable)
                {
                    switch (Globals.Config.Items.FoodDrink.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.FoodDrink.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.FoodDrink.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.FoodDrink.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.FoodDrink.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.FoodDrink.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.FoodDrink.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.FoodDrink.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.FoodDrink.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.FoodDrink.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.FoodDrink.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if ((items.Item.Item.QuestItem))
            {
                unitycolour = ColourHelper.GetColour("Item Quest Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.QuestItems.MaxDistance + 1));
                if (Globals.Config.Items.QuestItems.CullOpacityWithDistance)
                    unitycolour.a = (byte)distancealpha;

                if (Globals.Config.Items.QuestItems.Name.Enable)
                {
                    switch (Globals.Config.Items.QuestItems.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.QuestItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.QuestItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.QuestItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.QuestItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.QuestItems.Distance.Enable)
                {
                    switch (Globals.Config.Items.QuestItems.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.QuestItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.QuestItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.QuestItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.QuestItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                goto END;
            }
            if ((items.OurItem.type == Helpers.item_type.Key && items.OurItem.subtype != Helpers.item_subtype.Keycard))
            {
                unitycolour = ColourHelper.GetColour("Item Key Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Key.MaxDistance + 1));
            
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Key.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Key.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Key.Name.Enable)
                {
                    switch (Globals.Config.Items.Key.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Key.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Key.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Key.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Key.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Key.Distance.Enable)
                {
                    switch (Globals.Config.Items.Key.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Key.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Key.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Key.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Key.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Key.Value.Enable)
                {
                    if (Globals.Config.Items.Key.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Key.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Key.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Key.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Key.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Key.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Key.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Key.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Key.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Key.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Key.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Key.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Key.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Key.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Key.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Key.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.subtype == Helpers.item_subtype.Keycard)
            {
                unitycolour = ColourHelper.GetColour("Item Keycard Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Keycard.MaxDistance + 1));

               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Keycard.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Keycard.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Keycard.Name.Enable)
                {
                    switch (Globals.Config.Items.Keycard.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Keycard.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Keycard.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Keycard.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Keycard.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Keycard.Distance.Enable)
                {
                    switch (Globals.Config.Items.Keycard.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Keycard.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Keycard.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Keycard.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Keycard.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Keycard.Value.Enable)
                {
                    if (Globals.Config.Items.Keycard.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Keycard.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Keycard.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Keycard.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Keycard.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Keycard.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Keycard.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Keycard.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Keycard.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Keycard.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Keycard.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Keycard.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Keycard.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Keycard.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Keycard.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Keycard.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.subtype == item_subtype.Backpack)
            {
                unitycolour = ColourHelper.GetColour("Item Backpack Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Backpacks.MaxDistance + 1));
             
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Backpacks.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Backpacks.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Backpacks.Name.Enable)
                {
                    switch (Globals.Config.Items.Backpacks.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Backpacks.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Backpacks.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Backpacks.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Backpacks.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Backpacks.Distance.Enable)
                {
                    switch (Globals.Config.Items.Backpacks.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Backpacks.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Backpacks.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Backpacks.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Backpacks.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Backpacks.Value.Enable)
                {
                    if (Globals.Config.Items.Backpacks.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Backpacks.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Backpacks.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Backpacks.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Backpacks.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Backpacks.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Backpacks.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Backpacks.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Backpacks.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Backpacks.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Backpacks.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Backpacks.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Backpacks.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Backpacks.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Backpacks.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Backpacks.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if ((items.OurItem.type == item_type.Mod || items.OurItem.type == item_type.Muzzle || items.OurItem.type == item_type.Sights || items.OurItem.type == item_type.SpecialScope || items.OurItem.type == item_type.MasterMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.FunctionalMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.MasterMod))
            {
                unitycolour = ColourHelper.GetColour("Item Attachment Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Attachments.MaxDistance + 1));
             
               
                   int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Attachments.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Attachments.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Attachments.Name.Enable)
                {
                    switch (Globals.Config.Items.Attachments.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Attachments.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Attachments.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Attachments.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Attachments.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Attachments.Distance.Enable)
                {
                    switch (Globals.Config.Items.Attachments.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Attachments.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Attachments.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Attachments.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Attachments.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Attachments.Value.Enable)
                {
                    if (Globals.Config.Items.Attachments.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Attachments.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Attachments.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Attachments.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Attachments.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Attachments.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Attachments.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Attachments.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Attachments.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Attachments.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Attachments.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Attachments.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Attachments.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Attachments.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Attachments.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Attachments.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.subtype == item_subtype.ArmoredEquipment)
            {
                unitycolour = ColourHelper.GetColour("Item Armour Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Armour.MaxDistance + 1));
              
               
                   int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Armour.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Armour.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Armour.Name.Enable)
                {
                    switch (Globals.Config.Items.Armour.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Armour.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Armour.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Armour.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Armour.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Armour.Distance.Enable)
                {
                    switch (Globals.Config.Items.Armour.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Armour.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Armour.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Armour.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Armour.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Armour.Value.Enable)
                {
                    if (Globals.Config.Items.Armour.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Armour.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Armour.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Armour.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Armour.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Armour.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Armour.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Armour.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Armour.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Armour.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Armour.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Armour.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Armour.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Armour.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Armour.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Armour.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.subtype == item_subtype.Equipment)
            {
                unitycolour = ColourHelper.GetColour("Item Clothing Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Clothing.MaxDistance + 1));

               
                  int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Clothing.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Clothing.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Clothing.Name.Enable)
                {
                    switch (Globals.Config.Items.Clothing.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Clothing.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Clothing.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Clothing.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Clothing.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Clothing.Distance.Enable)
                {
                    switch (Globals.Config.Items.Clothing.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Clothing.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Clothing.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Clothing.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Clothing.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Clothing.Value.Enable)
                {
                    if (Globals.Config.Items.Clothing.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Clothing.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Clothing.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Clothing.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Clothing.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Clothing.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Clothing.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Clothing.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Clothing.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Clothing.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Clothing.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Clothing.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Clothing.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Clothing.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Clothing.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Clothing.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.type == item_type.RepairKits)
            {
                unitycolour = ColourHelper.GetColour("Item RepairKit Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.RepairKits.MaxDistance + 1));
              
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.RepairKits.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.RepairKits.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.RepairKits.Name.Enable)
                {
                    switch (Globals.Config.Items.RepairKits.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.RepairKits.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.RepairKits.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.RepairKits.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.RepairKits.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.RepairKits.Distance.Enable)
                {
                    switch (Globals.Config.Items.RepairKits.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.RepairKits.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.RepairKits.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.RepairKits.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.RepairKits.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.RepairKits.Value.Enable)
                {
                    if (Globals.Config.Items.RepairKits.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.RepairKits.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.RepairKits.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.RepairKits.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.RepairKits.ItemType.Enable)
                {
                    switch (Globals.Config.Items.RepairKits.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.RepairKits.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.RepairKits.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.RepairKits.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.RepairKits.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.RepairKits.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.RepairKits.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.RepairKits.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.RepairKits.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.RepairKits.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.RepairKits.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.type == item_type.Meds)
            {
                unitycolour = ColourHelper.GetColour("Item Medical Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Meds.MaxDistance + 1));
               
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Meds.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Meds.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Meds.Name.Enable)
                {
                    switch (Globals.Config.Items.Meds.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Meds.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Meds.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Meds.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Meds.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Meds.Distance.Enable)
                {
                    switch (Globals.Config.Items.Meds.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Meds.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Meds.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Meds.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Meds.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Meds.Value.Enable)
                {
                    if (Globals.Config.Items.Meds.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Meds.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Meds.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Meds.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Meds.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Meds.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Meds.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Meds.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Meds.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Meds.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Meds.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Meds.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Meds.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Meds.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Meds.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Meds.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.type == item_type.Weapon)
            {
                unitycolour = ColourHelper.GetColour("Item Weapon Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Weapon.MaxDistance + 1));
             
               
                   int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Weapon.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Weapon.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Weapon.Name.Enable)
                {
                    switch (Globals.Config.Items.Weapon.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Weapon.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Weapon.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Weapon.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Weapon.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Weapon.Distance.Enable)
                {
                    switch (Globals.Config.Items.Weapon.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Weapon.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Weapon.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Weapon.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Weapon.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Weapon.Value.Enable)
                {
                    if (Globals.Config.Items.Weapon.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Weapon.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Weapon.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Weapon.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Weapon.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Weapon.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Weapon.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Weapon.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Weapon.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Weapon.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Weapon.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Weapon.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Weapon.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Weapon.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Weapon.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Weapon.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.type == item_type.Item)
            {
                unitycolour = ColourHelper.GetColour("Item Special Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.SpecialItems.MaxDistance + 1));

               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.SpecialItems.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.SpecialItems.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.SpecialItems.Name.Enable)
                {
                    switch (Globals.Config.Items.SpecialItems.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.SpecialItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.SpecialItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.SpecialItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.SpecialItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.SpecialItems.Distance.Enable)
                {
                    switch (Globals.Config.Items.SpecialItems.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.SpecialItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.SpecialItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.SpecialItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.SpecialItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.SpecialItems.Value.Enable)
                {
                    if (Globals.Config.Items.SpecialItems.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.SpecialItems.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.SpecialItems.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.SpecialItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.SpecialItems.ItemType.Enable)
                {
                    switch (Globals.Config.Items.SpecialItems.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.SpecialItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.SpecialItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.SpecialItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.SpecialItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.SpecialItems.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.SpecialItems.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.SpecialItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.SpecialItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.SpecialItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.SpecialItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.type == item_type.BarterItem)
            {
                unitycolour = ColourHelper.GetColour("Item Barter Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.BarterItems.MaxDistance + 1));
              
               
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.BarterItems.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.BarterItems.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.BarterItems.Name.Enable)
                {
                    switch (Globals.Config.Items.BarterItems.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.BarterItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.BarterItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.BarterItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.BarterItems.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.BarterItems.Distance.Enable)
                {
                    switch (Globals.Config.Items.BarterItems.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.BarterItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.BarterItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.BarterItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.BarterItems.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.BarterItems.Value.Enable)
                {
                    if (Globals.Config.Items.BarterItems.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.BarterItems.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.BarterItems.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.BarterItems.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.BarterItems.ItemType.Enable)
                {
                    switch (Globals.Config.Items.BarterItems.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.BarterItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.BarterItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.BarterItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.BarterItems.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.BarterItems.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.BarterItems.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.BarterItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.BarterItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.BarterItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.BarterItems.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            }
            if (items.OurItem.type == item_type.CompoundItem)
            {
                unitycolour = ColourHelper.GetColour("Item Case Item Text Colour");
                int distancealpha = (int)Mathf.Lerp(255, 38, (float)items.Distance / (float)(Globals.Config.Items.Cases.MaxDistance + 1));
                
               
                   int valuealpha = (int)Mathf.Lerp(50, 255, (float)items.Value / (float)(HighestValue+ 1));
                if (Globals.Config.Items.Cases.CullOpacity == 1)
                    unitycolour.a = (byte)distancealpha;
                if (Globals.Config.Items.Cases.CullOpacity == 2)
                    unitycolour.a = (byte)valuealpha;
                if (Globals.Config.Items.Cases.Name.Enable)
                {
                    switch (Globals.Config.Items.Cases.Name.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Cases.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Cases.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Cases.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Cases.Name.Line] += items.Item.Item.ShortName.Localized();
                            break;

                    }
                }
                if (Globals.Config.Items.Cases.Distance.Enable)
                {
                    switch (Globals.Config.Items.Cases.Distance.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Cases.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Cases.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Cases.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Cases.Distance.Line] += string.Concat("[", items.Distance, "m]");
                            break;

                    }
                }
                if (Globals.Config.Items.Cases.Value.Enable)
                {
                    if (Globals.Config.Items.Cases.UseItemSlotPrice)
                    {
                        switch (Globals.Config.Items.Cases.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.OurItem.price_per_slot / 1000, "k]");
                                break;

                        }
                    }
                    else
                    {
                        switch (Globals.Config.Items.Cases.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.Value / 1000, "k]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Items.Cases.Value.Line] += string.Concat("[", items.Value/1000, "k]");
                                break;

                        }
                    }
                }
                if (Globals.Config.Items.Cases.ItemType.Enable)
                {
                    switch (Globals.Config.Items.Cases.ItemType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Cases.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Cases.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Cases.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Cases.ItemType.Line] += string.Concat("[", items.OurItem.type, "]");
                            break;

                    }
                }
                if (Globals.Config.Items.Cases.ItemSubType.Enable)
                {
                    switch (Globals.Config.Items.Cases.ItemSubType.Alignment)
                    {
                        case 1:
                            EspTextBottom[Globals.Config.Items.Cases.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 2:
                            EspTextLeft[Globals.Config.Items.Cases.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 3:
                            EspTextRight[Globals.Config.Items.Cases.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;
                        case 4:
                            EspTextTop[Globals.Config.Items.Cases.ItemSubType.Line] += string.Concat("[", items.OurItem.subtype, "]");
                            break;

                    }
                }
                goto END;
            } // lerp shit here
            END:
            dxcolour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
            for (int i = 1; i <= 3; i++)
            {
                if (EspTextBottom[i] != null)
                    renderer.DrawTextCentered(EspTextBottom[i], items.W2SPos.x, items.W2SPos.y + (16 * (i - 1)), 11, font, dxcolour);
                if (EspTextRight[i] != null)
                    renderer.DrawText(EspTextRight[i], items.W2SPos.x + 36, (items.W2SPos.y - 36) + (16 * (i - 1)), 11, font, dxcolour);
                if (EspTextLeft[i] != null)
                    renderer.DrawTextLeft(EspTextLeft[i], items.W2SPos.x - 36, (items.W2SPos.y - 36) + (16 * (i - 1)), 11, font, dxcolour);
                if (EspTextTop[i] != null)
                    renderer.DrawTextCentered(EspTextTop[i], items.W2SPos.x, (items.W2SPos.y - 48) - (16 * (i - 1)), 11, font, dxcolour);
            }
        }
        bool CheckBattleMode(SDK.Items items)
        {
            if (!Globals.BattleMode)
                return true;
            if (Globals.Config.Items.WhitelistedItems.Contains(items.Item.TemplateId) && (Globals.BattleMode && Globals.Config.Items.Whitelist.EnableEspInBattleMode ))
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Fuel   && ( Globals.BattleMode && Globals.Config.Items.Fuel.EnableEspInBattleMode ))
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Ammo || items.OurItem.subtype == Helpers.item_subtype.AmmoBox) && (Globals.BattleMode && Globals.Config.Items.Ammo.EnableEspInBattleMode))
                return true;
            if ((items.OurItem.subtype == Helpers.item_subtype.Food || items.OurItem.subtype == Helpers.item_subtype.FoodDrink) && (Globals.BattleMode && Globals.Config.Items.FoodDrink.EnableEspInBattleMode ))
                return true;
            if ((items.Item.Item.QuestItem && (Globals.BattleMode && Globals.Config.Items.QuestItems.EnableEspInBattleMode )))
                return true;
            if ((items.OurItem.type == Helpers.item_type.Key && items.OurItem.subtype != Helpers.item_subtype.Keycard) && (Globals.BattleMode && Globals.Config.Items.Key.EnableEspInBattleMode))
                return true;
            if (items.OurItem.subtype == Helpers.item_subtype.Keycard && (Globals.BattleMode && Globals.Config.Items.Keycard.EnableEspInBattleMode))
                return true;
            if (items.OurItem.subtype == item_subtype.Backpack && (Globals.BattleMode && Globals.Config.Items.Backpacks.EnableEspInBattleMode))
                return true;
            if ((items.OurItem.type == item_type.Mod || items.OurItem.type == item_type.Muzzle || items.OurItem.type == item_type.Sights || items.OurItem.type == item_type.SpecialScope || items.OurItem.type == item_type.MasterMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.FunctionalMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.MasterMod) && (Globals.BattleMode &&Globals.Config.Items.Attachments.EnableEspInBattleMode))
                return true;
            if (items.OurItem.subtype == item_subtype.ArmoredEquipment && (Globals.BattleMode && Globals.Config.Items.Armour.EnableEspInBattleMode))
                return true;
            if (items.OurItem.subtype == item_subtype.Equipment && (Globals.BattleMode && Globals.Config.Items.Clothing.EnableEspInBattleMode ))
                return true;
            if (items.OurItem.type == item_type.RepairKits && (Globals.BattleMode && Globals.Config.Items.RepairKits.EnableEspInBattleMode ))
                return true;
            if (items.OurItem.type == item_type.Meds && (Globals.BattleMode && Globals.Config.Items.Meds.EnableEspInBattleMode))
                return true;
            if (items.OurItem.type == item_type.Weapon && (Globals.BattleMode && Globals.Config.Items.Weapon.EnableEspInBattleMode))
                return true;
            if (items.OurItem.type == item_type.Item && (Globals.BattleMode && Globals.Config.Items.SpecialItems.EnableEspInBattleMode))
                return true;
            if (items.OurItem.type == item_type.BarterItem && (Globals.BattleMode && Globals.Config.Items.BarterItems.EnableEspInBattleMode))
                return true;
            if (items.OurItem.type == item_type.CompoundItem && (Globals.BattleMode && Globals.Config.Items.Cases.EnableEspInBattleMode))
                return true;
            return false;
        }
        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
            if ((Globals.MainCamera == null || Globals.GameWorld == null))
                return;
           if (Globals.AlteringWhitelist)
                return;
            for (int i = 0; i < Globals.GameWorld.LootItems.Count; i++)
            {
                LootItem item = Globals.GameWorld.LootItems.GetByIndex(i);
                System.Random random = new System.Random();
            }
                foreach (SDK.Items items in Globals.ItemDict.Values)
            {
                try
                {
                    if (items.Item == null)
                        continue;
                    if (!EspConstants.IsScreenPointVisible(items.W2SPos))
                        continue;
                    if (Globals.AlteringWhitelist)
                        return; // prevent exeptions when we could be reading the whitelist
                    if (!IsItemEnabled(items))
                        continue;
                    if (!IsItemInDistance(items))
                        continue;
                    if (!CheckBattleMode(items))
                        continue;
                    if (!IsItemInValue(items))
                        continue;
                    DrawItem(renderer, font, items);



                    //  renderer.DrawText("test", rand.Next(0, 1000), rand.Next(0, 1000), font, new Direct2DColor(255, 0, 0, 255));

                }
                catch(Exception ex) { System.IO.File.WriteAllText("item esp 1", $"{ex.Message}\n{ex.ToString()}"); }
            }
        }
    }
}
