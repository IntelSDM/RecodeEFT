using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using UnityEngine;
using Hag.Helpers;
namespace Hag.SDK
{
    class Corpses
    {
        private List<ContainerItems> OrganizeByValue(List<ContainerItems> items)
        {
            return items.OfType<ContainerItems>()
              .OrderBy(items => items.Value)
              .ToList();
        }
        bool IsItemAQuestRequirement(Item item)
        {

            foreach (var quests in Globals.LocalPlayer.Profile.QuestsData)
            {

                foreach (var conditionlist in quests.Template.Conditions)
                {
                    if (conditionlist.Key != EFT.Quests.EQuestStatus.AvailableForFinish)
                        continue;
                    foreach (var condition in conditionlist.Value)
                    {

                        if (condition is EFT.Quests.ConditionFindItem)
                        {
                            var finditem = condition as EFT.Quests.ConditionFindItem;
                            if (finditem.onlyFoundInRaid && !item.SpawnedInSession)
                                continue;
                            foreach (string str in finditem.target)
                                if (item.TemplateId == str)
                                    return true;
                            //  File.WriteAllText("Condition " + condition.FormattedDescription, str);
                           //System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
                        }
                        if (condition is EFT.Quests.ConditionHandoverItem)
                        {
                            var handoveritem = condition as EFT.Quests.ConditionHandoverItem;
                            if (handoveritem.onlyFoundInRaid && !item.SpawnedInSession)
                                continue;
                            foreach (string str in handoveritem.target)
                                if (item.TemplateId == str)
                                    return true;
                            // File.WriteAllText("Condition " + condition.FormattedDescription, str);
                            //System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
                        }

                    }
                }
            }
            return false;
        }
        public Corpses(LootItem corpse)
        {
            Corpse = corpse;
         //   this.SetContainerValue();
        }
        public int Distance;
        public int Value;
        public Vector3 W2SPos;
        public LootItem Corpse;
        public List<ContainerItems> ItemList;
        public bool ContainsWhitelistedItem;
        private bool DoAddToList(Item item, Helpers.OurItem ouritem, ref bool SlotPrice, ref bool name, ref bool value, ref bool type, ref bool subtype)
        {
            // this code started very nice but turned into this mess as i didn't want to spam too many if statements in like 6 different functions for each of the outputs, saved myself like 100 if statements by making this mess.
            bool isitemquestrequirement = IsItemAQuestRequirement(item);
            if (Globals.Config.Corpse.WhitelistContainer.Enable && (Globals.Config.Items.WhitelistedItems.Contains(item.Template._id) || (isitemquestrequirement && !item.QuestItem)) && (!Globals.Config.Corpse.WhitelistContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.WhitelistContainer.MinValue || Globals.Config.Corpse.WhitelistContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.WhitelistContainer.MinValue || isitemquestrequirement))
            { ContainsWhitelistedItem = true; SlotPrice = Globals.Config.Corpse.WhitelistContainer.UseSlotPrice; name = Globals.Config.Corpse.WhitelistContainer.Name; value = Globals.Config.Corpse.WhitelistContainer.Value; type = Globals.Config.Corpse.WhitelistContainer.Type; subtype = Globals.Config.Corpse.WhitelistContainer.SubType; return true; }
            if (ouritem.subtype == Helpers.item_subtype.Fuel && Globals.Config.Corpse.FuelContainer.Enable && (!Globals.Config.Corpse.FuelContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.FuelContainer.MinValue || Globals.Config.Corpse.FuelContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.FuelContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.FuelContainer.UseSlotPrice; name = Globals.Config.Corpse.FuelContainer.Name; value = Globals.Config.Corpse.FuelContainer.Value; type = Globals.Config.Corpse.FuelContainer.Type; subtype = Globals.Config.Corpse.FuelContainer.SubType; return true; }
            if ((ouritem.subtype == Helpers.item_subtype.Ammo || ouritem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.Corpse.AmmoContainer.Enable && (!Globals.Config.Corpse.AmmoContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.AmmoContainer.MinValue || Globals.Config.Corpse.AmmoContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.AmmoContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.AmmoContainer.UseSlotPrice; name = Globals.Config.Corpse.AmmoContainer.Name; value = Globals.Config.Corpse.AmmoContainer.Value; type = Globals.Config.Corpse.AmmoContainer.Type; subtype = Globals.Config.Corpse.AmmoContainer.SubType; return true; }
            if ((ouritem.subtype == Helpers.item_subtype.Food || ouritem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.Corpse.FoodDrinkContainer.Enable && (!Globals.Config.Corpse.FoodDrinkContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.FoodDrinkContainer.MinValue || Globals.Config.Corpse.FoodDrinkContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.FoodDrinkContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.FoodDrinkContainer.UseSlotPrice; name = Globals.Config.Corpse.FoodDrinkContainer.Name; value = Globals.Config.Corpse.FoodDrinkContainer.Value; type = Globals.Config.Corpse.FoodDrinkContainer.Type; subtype = Globals.Config.Corpse.FoodDrinkContainer.SubType; return true; }
            if ((ouritem.type == Helpers.item_type.Key && ouritem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.Corpse.KeyContainer.Enable && (!Globals.Config.Corpse.KeyContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.KeyContainer.MinValue || Globals.Config.Corpse.KeyContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.KeyContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.KeyContainer.UseSlotPrice; name = Globals.Config.Corpse.KeyContainer.Name; value = Globals.Config.Corpse.KeyContainer.Value; type = Globals.Config.Corpse.KeyContainer.Type; subtype = Globals.Config.Corpse.KeyContainer.SubType; return true; }
            if (ouritem.subtype == Helpers.item_subtype.Keycard && Globals.Config.Corpse.KeycardContainer.Enable && (!Globals.Config.Corpse.KeycardContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.KeycardContainer.MinValue || Globals.Config.Corpse.KeycardContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.KeycardContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.KeycardContainer.UseSlotPrice; name = Globals.Config.Corpse.KeycardContainer.Name; value = Globals.Config.Corpse.KeycardContainer.Value; type = Globals.Config.Corpse.KeycardContainer.Type; subtype = Globals.Config.Corpse.KeycardContainer.SubType; return true; }
            if (ouritem.subtype == item_subtype.Backpack && Globals.Config.Corpse.BackpacksContainer.Enable && (!Globals.Config.Corpse.BackpacksContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.BackpacksContainer.MinValue || Globals.Config.Corpse.BackpacksContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.BackpacksContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.BackpacksContainer.UseSlotPrice; name = Globals.Config.Corpse.BackpacksContainer.Name; value = Globals.Config.Corpse.BackpacksContainer.Value; type = Globals.Config.Corpse.BackpacksContainer.Type; subtype = Globals.Config.Corpse.BackpacksContainer.SubType; return true; }
            if ((ouritem.type == item_type.Mod || ouritem.type == item_type.Muzzle || ouritem.type == item_type.Sights || ouritem.type == item_type.SpecialScope || ouritem.type == item_type.MasterMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.FunctionalMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.MasterMod) && Globals.Config.Corpse.AttachmentsContainer.Enable && (!Globals.Config.Corpse.AttachmentsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.AttachmentsContainer.MinValue || Globals.Config.Corpse.AttachmentsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.AttachmentsContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.AttachmentsContainer.UseSlotPrice; name = Globals.Config.Corpse.AttachmentsContainer.Name; value = Globals.Config.Corpse.AttachmentsContainer.Value; type = Globals.Config.Corpse.AttachmentsContainer.Type; subtype = Globals.Config.Corpse.AttachmentsContainer.SubType; return true; }
            if (ouritem.subtype == item_subtype.ArmoredEquipment && Globals.Config.Corpse.ArmourContainer.Enable && (!Globals.Config.Corpse.ArmourContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.ArmourContainer.MinValue || Globals.Config.Corpse.ArmourContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.ArmourContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.ArmourContainer.UseSlotPrice; name = Globals.Config.Corpse.ArmourContainer.Name; value = Globals.Config.Corpse.ArmourContainer.Value; type = Globals.Config.Corpse.ArmourContainer.Type; subtype = Globals.Config.Corpse.ArmourContainer.SubType; return true; }
            if (ouritem.subtype == item_subtype.Equipment && Globals.Config.Corpse.ClothingContainer.Enable && (!Globals.Config.Corpse.ClothingContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.ClothingContainer.MinValue || Globals.Config.Corpse.ClothingContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.ClothingContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.ClothingContainer.UseSlotPrice; name = Globals.Config.Corpse.ClothingContainer.Name; value = Globals.Config.Corpse.ClothingContainer.Value; type = Globals.Config.Corpse.ClothingContainer.Type; subtype = Globals.Config.Corpse.ClothingContainer.SubType; return true; }
            if (ouritem.type == item_type.RepairKits && Globals.Config.Corpse.RepairKitsContainer.Enable && (!Globals.Config.Corpse.RepairKitsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.RepairKitsContainer.MinValue || Globals.Config.Corpse.RepairKitsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.RepairKitsContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.RepairKitsContainer.UseSlotPrice; name = Globals.Config.Corpse.RepairKitsContainer.Name; value = Globals.Config.Corpse.RepairKitsContainer.Value; type = Globals.Config.Corpse.RepairKitsContainer.Type; subtype = Globals.Config.Corpse.RepairKitsContainer.SubType; return true; }
            if (ouritem.type == item_type.Meds && Globals.Config.Corpse.MedsContainer.Enable && (!Globals.Config.Corpse.MedsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.MedsContainer.MinValue || Globals.Config.Corpse.MedsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.MedsContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.MedsContainer.UseSlotPrice; name = Globals.Config.Corpse.MedsContainer.Name; value = Globals.Config.Corpse.MedsContainer.Value; type = Globals.Config.Corpse.MedsContainer.Type; subtype = Globals.Config.Corpse.MedsContainer.SubType; return true; }
            if (ouritem.type == item_type.Weapon && Globals.Config.Corpse.WeaponContainer.Enable && (!Globals.Config.Corpse.WeaponContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.WeaponContainer.MinValue || Globals.Config.Corpse.WeaponContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.WeaponContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.WeaponContainer.UseSlotPrice; name = Globals.Config.Corpse.WeaponContainer.Name; value = Globals.Config.Corpse.WeaponContainer.Value; type = Globals.Config.Corpse.WeaponContainer.Type; subtype = Globals.Config.Corpse.WeaponContainer.SubType; return true; }
            if (ouritem.type == item_type.Item && Globals.Config.Corpse.SpecialItemsContainer.Enable && (!Globals.Config.Corpse.SpecialItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.SpecialItemsContainer.MinValue || Globals.Config.Corpse.SpecialItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.SpecialItemsContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.SpecialItemsContainer.UseSlotPrice; name = Globals.Config.Corpse.SpecialItemsContainer.Name; value = Globals.Config.Corpse.SpecialItemsContainer.Value; type = Globals.Config.Corpse.SpecialItemsContainer.Type; subtype = Globals.Config.Corpse.SpecialItemsContainer.SubType; return true; }
            if (ouritem.type == item_type.BarterItem && Globals.Config.Corpse.BarterItemsContainer.Enable && (!Globals.Config.Corpse.BarterItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.BarterItemsContainer.MinValue || Globals.Config.Corpse.BarterItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.BarterItemsContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.BarterItemsContainer.UseSlotPrice; name = Globals.Config.Corpse.BarterItemsContainer.Name; value = Globals.Config.Corpse.BarterItemsContainer.Value; type = Globals.Config.Corpse.BarterItemsContainer.Type; subtype = Globals.Config.Corpse.BarterItemsContainer.SubType; return true; }
            if (ouritem.type == item_type.CompoundItem && Globals.Config.Corpse.CasesContainer.Enable && (!Globals.Config.Corpse.CasesContainer.UseSlotPrice && ouritem.price >= Globals.Config.Corpse.CasesContainer.MinValue || Globals.Config.Corpse.CasesContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Corpse.CasesContainer.MinValue))
            { SlotPrice = Globals.Config.Corpse.CasesContainer.UseSlotPrice; name = Globals.Config.Corpse.CasesContainer.Name; value = Globals.Config.Corpse.CasesContainer.Value; type = Globals.Config.Corpse.CasesContainer.Type; subtype = Globals.Config.Corpse.CasesContainer.SubType; return true; }



            SlotPrice = true;
            name = true;
            value = true;
            subtype = true;
            type = true;
            return false;
        }
        public void RemoveChams()
        {
            if (!(Globals.Config.Scav.Chams || Globals.Config.Player.Chams || Globals.Config.ScavPlayer.Chams || Globals.Config.Boss.Chams))
                return;
            foreach (var r in Corpse.gameObject.GetComponentsInChildren<UnityEngine.Renderer>())
            {

                string name = r.material.name;
                //            if (name.Contains("weapon") || name.Contains("ammo") || name.Contains("sight") || name.Contains("grip") || name.Contains("mount") || name.Contains("item") || name.Contains("mag") || name.Contains("tactical") || name.Contains("scope") || name.Contains("barrel") || name.Contains("patron") || name.Contains("muzzle"))
                //              continue;
                foreach (Material m in r.materials)
                {
                 

                        if (Globals.Shaders.ContainsKey(m.name))
                        {

                          
                                m.shader = Globals.Shaders[m.name];
                            
                        }
                    

                }
            }
        }
        private int NextTime = 0;
        public void SetContainerValue()
        {
            if (Time.time > NextTime)
            {
                int value = 0;
                List<SDK.ContainerItems> itemlist = new List<SDK.ContainerItems>();
                foreach (var slot in this.Corpse.ItemOwner.Root.AllSlots)
                {
                    // in recode add bool to skip attachments in value
                    if (slot.Name == "SecuredContainer")
                        continue;
                    if (slot.Name == "ArmBand" && slot.BlockerSlots.Count > 0)
                        continue;
                    if (slot.Name == "Dogtag")
                        continue;
                    if (slot.Name == "Scabbard" && slot.BlockerSlots.Count > 0) // knife
                        continue;


                    foreach (Item item in slot.Items)
                    {
                        try
                        {
                            Helpers.OurItem ouritem = Helpers.ItemPriceHelper.list[item.Template._id];
                            bool UseSlot = false;
                            bool showname = false;
                            bool showvalue = false;
                            bool showtype = false;
                            bool showsubtype = false;
                            bool addtolist = DoAddToList(item, ouritem, ref UseSlot, ref showname, ref showvalue, ref showtype, ref showsubtype);
                            int itemvalue = UseSlot ? ouritem.price_per_slot : ouritem.price;
                            if (UseSlot)
                                value += ouritem.price_per_slot;
                            else
                                value += ouritem.price;
                            if (!addtolist)
                                continue;
                            SDK.ContainerItems containeritem = new ContainerItems(item, ouritem, itemvalue, showname, showvalue, showtype, showsubtype, IsItemAQuestRequirement(item));
                            containeritem.Colour = ColourHelper.GetItemColour(containeritem);
                            itemlist.Add(containeritem);
                        }
                        catch (Exception ex) { System.IO.File.WriteAllText("corpse sdk 1", $"{ex.Message}\n{ex.ToString()}"); }
                    }


                }
                System.Random rand = new System.Random();
                NextTime = (int)(Time.time + 4 + rand.Next(0, 3)); // this will help distribute the heft 
                this.Value = value;
                this.ItemList = OrganizeByValue(itemlist);
                this.ItemList.Reverse();
            }
            }
        }
}
