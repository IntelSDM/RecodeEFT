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
    class Containers
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
                          //  System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
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
                           // System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
                        }

                    }
                }
            }
            return false;
        }
        public Containers(LootableContainer container)
        {
            Container = container;
        //    if(container != null)
         //   this.SetContainerValue();
        }
        public int Distance;
        public int Value;
        public Vector3 W2SPos;
        public LootableContainer Container;
        public List<ContainerItems> ItemList;
        public bool ContainsWhitelistedItem;
        public bool ContainsQuestItem;
        private bool DoAddToList(Item item, Helpers.OurItem ouritem, ref bool SlotPrice, ref bool name, ref bool value, ref bool type, ref bool subtype)
        {
            // this code started very nice but turned into this mess as i didn't want to spam too many if statements in like 6 different functions for each of the outputs, saved myself like 100 if statements by making this mess.
            bool isitemquestrequirement = IsItemAQuestRequirement(item);
            if (Globals.Config.Container.WhitelistContainer.Enable && (Globals.Config.Items.WhitelistedItems.Contains(item.Template._id) || (isitemquestrequirement && !item.QuestItem)) && (!Globals.Config.Container.WhitelistContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.WhitelistContainer.MinValue || Globals.Config.Container.WhitelistContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.WhitelistContainer.MinValue || isitemquestrequirement))
            { this.ContainsWhitelistedItem = true; SlotPrice = Globals.Config.Container.WhitelistContainer.UseSlotPrice; name = Globals.Config.Container.WhitelistContainer.Name; value = Globals.Config.Container.WhitelistContainer.Value; type = Globals.Config.Container.WhitelistContainer.Type; subtype = Globals.Config.Container.WhitelistContainer.SubType; return true; }
            if (Globals.Config.Container.QuestItemContainer.Enable && item.QuestItem && isitemquestrequirement)
            { this.ContainsQuestItem = true;  SlotPrice = false; name = Globals.Config.Container.QuestItemContainer.Name; value = false; type =false; subtype =false; return true; }
            if (ouritem.subtype == Helpers.item_subtype.Fuel && Globals.Config.Container.FuelContainer.Enable && (!Globals.Config.Container.FuelContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.FuelContainer.MinValue || Globals.Config.Container.FuelContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.FuelContainer.MinValue))
            { SlotPrice = Globals.Config.Container.FuelContainer.UseSlotPrice; name = Globals.Config.Container.FuelContainer.Name; value = Globals.Config.Container.FuelContainer.Value; type = Globals.Config.Container.FuelContainer.Type; subtype = Globals.Config.Container.FuelContainer.SubType; return true; }
            if ((ouritem.subtype == Helpers.item_subtype.Ammo || ouritem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.Container.AmmoContainer.Enable && (!Globals.Config.Container.AmmoContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.AmmoContainer.MinValue || Globals.Config.Container.AmmoContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.AmmoContainer.MinValue))
            { SlotPrice = Globals.Config.Container.AmmoContainer.UseSlotPrice; name = Globals.Config.Container.AmmoContainer.Name; value = Globals.Config.Container.AmmoContainer.Value; type = Globals.Config.Container.AmmoContainer.Type; subtype = Globals.Config.Container.AmmoContainer.SubType; return true; }
            if ((ouritem.subtype == Helpers.item_subtype.Food || ouritem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.Container.FoodDrinkContainer.Enable && (!Globals.Config.Container.FoodDrinkContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.FoodDrinkContainer.MinValue || Globals.Config.Container.FoodDrinkContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.FoodDrinkContainer.MinValue))
            { SlotPrice = Globals.Config.Container.FoodDrinkContainer.UseSlotPrice; name = Globals.Config.Container.FoodDrinkContainer.Name; value = Globals.Config.Container.FoodDrinkContainer.Value; type = Globals.Config.Container.FoodDrinkContainer.Type; subtype = Globals.Config.Container.FoodDrinkContainer.SubType; return true; }
            if ((ouritem.type == Helpers.item_type.Key && ouritem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.Container.KeyContainer.Enable && (!Globals.Config.Container.KeyContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.KeyContainer.MinValue || Globals.Config.Container.KeyContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.KeyContainer.MinValue))
            { SlotPrice = Globals.Config.Container.KeyContainer.UseSlotPrice; name = Globals.Config.Container.KeyContainer.Name; value = Globals.Config.Container.KeyContainer.Value; type = Globals.Config.Container.KeyContainer.Type; subtype = Globals.Config.Container.KeyContainer.SubType; return true; }
            if (ouritem.subtype == Helpers.item_subtype.Keycard && Globals.Config.Container.KeycardContainer.Enable && (!Globals.Config.Container.KeycardContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.KeycardContainer.MinValue || Globals.Config.Container.KeycardContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.KeycardContainer.MinValue))
            { SlotPrice = Globals.Config.Container.KeycardContainer.UseSlotPrice; name = Globals.Config.Container.KeycardContainer.Name; value = Globals.Config.Container.KeycardContainer.Value; type = Globals.Config.Container.KeycardContainer.Type; subtype = Globals.Config.Container.KeycardContainer.SubType; return true; }
            if (ouritem.subtype == item_subtype.Backpack && Globals.Config.Container.BackpacksContainer.Enable && (!Globals.Config.Container.BackpacksContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.BackpacksContainer.MinValue || Globals.Config.Container.BackpacksContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.BackpacksContainer.MinValue))
            { SlotPrice = Globals.Config.Container.BackpacksContainer.UseSlotPrice; name = Globals.Config.Container.BackpacksContainer.Name; value = Globals.Config.Container.BackpacksContainer.Value; type = Globals.Config.Container.BackpacksContainer.Type; subtype = Globals.Config.Container.BackpacksContainer.SubType; return true; }
            if ((ouritem.type == item_type.Mod || ouritem.type == item_type.Muzzle || ouritem.type == item_type.Sights || ouritem.type == item_type.SpecialScope || ouritem.type == item_type.MasterMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.FunctionalMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.MasterMod) && Globals.Config.Container.AttachmentsContainer.Enable && (!Globals.Config.Container.AttachmentsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.AttachmentsContainer.MinValue || Globals.Config.Container.AttachmentsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.AttachmentsContainer.MinValue))
            { SlotPrice = Globals.Config.Container.AttachmentsContainer.UseSlotPrice; name = Globals.Config.Container.AttachmentsContainer.Name; value = Globals.Config.Container.AttachmentsContainer.Value; type = Globals.Config.Container.AttachmentsContainer.Type; subtype = Globals.Config.Container.AttachmentsContainer.SubType; return true; }
            if (ouritem.subtype == item_subtype.ArmoredEquipment && Globals.Config.Container.ArmourContainer.Enable && (!Globals.Config.Container.ArmourContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.ArmourContainer.MinValue || Globals.Config.Container.ArmourContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.ArmourContainer.MinValue))
            { SlotPrice = Globals.Config.Container.ArmourContainer.UseSlotPrice; name = Globals.Config.Container.ArmourContainer.Name; value = Globals.Config.Container.ArmourContainer.Value; type = Globals.Config.Container.ArmourContainer.Type; subtype = Globals.Config.Container.ArmourContainer.SubType; return true; }
            if (ouritem.subtype == item_subtype.Equipment && Globals.Config.Container.ClothingContainer.Enable && (!Globals.Config.Container.ClothingContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.ClothingContainer.MinValue || Globals.Config.Container.ClothingContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.ClothingContainer.MinValue))
            { SlotPrice = Globals.Config.Container.ClothingContainer.UseSlotPrice; name = Globals.Config.Container.ClothingContainer.Name; value = Globals.Config.Container.ClothingContainer.Value; type = Globals.Config.Container.ClothingContainer.Type; subtype = Globals.Config.Container.ClothingContainer.SubType; return true; }
            if (ouritem.type == item_type.RepairKits && Globals.Config.Container.RepairKitsContainer.Enable && (!Globals.Config.Container.RepairKitsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.RepairKitsContainer.MinValue || Globals.Config.Container.RepairKitsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.RepairKitsContainer.MinValue))
            { SlotPrice = Globals.Config.Container.RepairKitsContainer.UseSlotPrice; name = Globals.Config.Container.RepairKitsContainer.Name; value = Globals.Config.Container.RepairKitsContainer.Value; type = Globals.Config.Container.RepairKitsContainer.Type; subtype = Globals.Config.Container.RepairKitsContainer.SubType; return true; }
            if (ouritem.type == item_type.Meds && Globals.Config.Container.MedsContainer.Enable && (!Globals.Config.Container.MedsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.MedsContainer.MinValue || Globals.Config.Container.MedsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.MedsContainer.MinValue))
            { SlotPrice = Globals.Config.Container.MedsContainer.UseSlotPrice; name = Globals.Config.Container.MedsContainer.Name; value = Globals.Config.Container.MedsContainer.Value; type = Globals.Config.Container.MedsContainer.Type; subtype = Globals.Config.Container.MedsContainer.SubType; return true; }
            if (ouritem.type == item_type.Weapon && Globals.Config.Container.WeaponContainer.Enable && (!Globals.Config.Container.WeaponContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.WeaponContainer.MinValue || Globals.Config.Container.WeaponContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.WeaponContainer.MinValue))
            { SlotPrice = Globals.Config.Container.WeaponContainer.UseSlotPrice; name = Globals.Config.Container.WeaponContainer.Name; value = Globals.Config.Container.WeaponContainer.Value; type = Globals.Config.Container.WeaponContainer.Type; subtype = Globals.Config.Container.WeaponContainer.SubType; return true; }
            if (ouritem.type == item_type.Item && Globals.Config.Container.SpecialItemsContainer.Enable && (!Globals.Config.Container.SpecialItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.SpecialItemsContainer.MinValue || Globals.Config.Container.SpecialItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.SpecialItemsContainer.MinValue))
            { SlotPrice = Globals.Config.Container.SpecialItemsContainer.UseSlotPrice; name = Globals.Config.Container.SpecialItemsContainer.Name; value = Globals.Config.Container.SpecialItemsContainer.Value; type = Globals.Config.Container.SpecialItemsContainer.Type; subtype = Globals.Config.Container.SpecialItemsContainer.SubType; return true; }
            if (ouritem.type == item_type.BarterItem && Globals.Config.Container.BarterItemsContainer.Enable && (!Globals.Config.Container.BarterItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.BarterItemsContainer.MinValue || Globals.Config.Container.BarterItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.BarterItemsContainer.MinValue))
            { SlotPrice = Globals.Config.Container.BarterItemsContainer.UseSlotPrice; name = Globals.Config.Container.BarterItemsContainer.Name; value = Globals.Config.Container.BarterItemsContainer.Value; type = Globals.Config.Container.BarterItemsContainer.Type; subtype = Globals.Config.Container.BarterItemsContainer.SubType; return true; }
            if (ouritem.type == item_type.CompoundItem && Globals.Config.Container.CasesContainer.Enable && (!Globals.Config.Container.CasesContainer.UseSlotPrice && ouritem.price >= Globals.Config.Container.CasesContainer.MinValue || Globals.Config.Container.CasesContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Container.CasesContainer.MinValue))
            { SlotPrice = Globals.Config.Container.CasesContainer.UseSlotPrice; name = Globals.Config.Container.CasesContainer.Name; value = Globals.Config.Container.CasesContainer.Value; type = Globals.Config.Container.CasesContainer.Type; subtype = Globals.Config.Container.CasesContainer.SubType; return true; }



            SlotPrice = true;
            name = true;
            value = true;
            subtype = true;
            type = true;
            return false;
        }
        private int NextTime = 0;
        public void SetContainerValue()
        {
            if (Time.time > NextTime)
            {
                int value = 0;
                List<SDK.ContainerItems> itemlist = new List<SDK.ContainerItems>();
                foreach (var item in this.Container.ItemOwner.RootItem.GetAllItems())
                {
                    // in recode add bool to skip attachments in value

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
                    catch (Exception ex) { System.IO.File.WriteAllText("container sdk 1", $"{ex.Message}\n{ex.ToString()}"); }

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
