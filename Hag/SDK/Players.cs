using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using EFT.InventoryLogic;
using Hag.Helpers;

namespace Hag.SDK
{
    enum PlayerType
    {
       Player, 
       Scav,
       ScavPlayer,
       Boss
    }

    class Players
    {
        public Players(Player player)
        {
            Player = player;
            bool IsScav = player.Profile.Info.RegistrationDate <= 0 && !player.Profile.Info.Settings.IsBoss();
            bool IsScavPlayer = player.Profile.Side == EPlayerSide.Savage && !IsScav && !player.IsAI;
            bool IsPlayer = (player.Profile.Side == EPlayerSide.Usec || player.Profile.Side == EPlayerSide.Bear) && !IsScavPlayer && !IsScav;
            bool IsBoss = player.Profile.Info.RegistrationDate <= 0 && player.Profile.Info.Settings.IsBoss();
            if (IsScav)
            {
                this.PlayerType = PlayerType.Scav;
                this.ScavInstance = Globals.Config.Scav;
            }
            if (IsScavPlayer)
            {
                this.PlayerType = PlayerType.ScavPlayer;
                this.ScavInstance = Globals.Config.ScavPlayer;
            }
            if (IsPlayer)
            {
                this.PlayerType = PlayerType.Player;
                this.PlayerInstance = Globals.Config.Player;
            }
            if (IsBoss)
            {
                this.PlayerType = PlayerType.Boss;
                this.ScavInstance = Globals.Config.Boss;
            }
            Friendly = Globals.LocalPlayer.Profile.Info.GroupId == player.Profile.Info.GroupId
                     && player.Profile.Info.GroupId != "0"
                     && player.Profile.Info.GroupId != ""
                     && player.Profile.Info.GroupId != null;
            if (player != null && player.HealthController.IsAlive)
                SetPlayerValue();
        }
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
                         //   System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
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
                          //  System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
                        }

                    }
                }
            }
            return false;
        }
       
        public bool ContainsWhitelistedItem;
        private bool DoAddToList(Item item,Helpers.OurItem ouritem,ref bool SlotPrice, ref bool name, ref bool value, ref bool type, ref bool subtype)
        {
            bool isitemquestrequirement = IsItemAQuestRequirement(item);

            // this code started very nice but turned into this mess as i didn't want to spam too many if statements in like 6 different functions for each of the outputs, saved myself like 100 if statements by making this mess.
            if (this.PlayerType == PlayerType.Player)
            {
                if (Globals.Config.Player.WhitelistContainer.Enable && (Globals.Config.Items.WhitelistedItems.Contains(item.Template._id)|| (isitemquestrequirement && !item.QuestItem)) && (!Globals.Config.Player.WhitelistContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.WhitelistContainer.MinValue || Globals.Config.Player.WhitelistContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.WhitelistContainer.MinValue || isitemquestrequirement))
                { ContainsWhitelistedItem = true; SlotPrice = Globals.Config.Player.WhitelistContainer.UseSlotPrice; name = Globals.Config.Player.WhitelistContainer.Name; value = Globals.Config.Player.WhitelistContainer.Value; type = Globals.Config.Player.WhitelistContainer.Type;subtype = Globals.Config.Player.WhitelistContainer.SubType; return true; }
                if (ouritem.subtype == Helpers.item_subtype.Fuel && Globals.Config.Player.FuelContainer.Enable && (!Globals.Config.Player.FuelContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.FuelContainer.MinValue || Globals.Config.Player.FuelContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.FuelContainer.MinValue))
                { SlotPrice = Globals.Config.Player.FuelContainer.UseSlotPrice; name = Globals.Config.Player.FuelContainer.Name; value = Globals.Config.Player.FuelContainer.Value; type = Globals.Config.Player.FuelContainer.Type; subtype = Globals.Config.Player.FuelContainer.SubType; return true; }
                if ((ouritem.subtype == Helpers.item_subtype.Ammo || ouritem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.Player.AmmoContainer.Enable && (!Globals.Config.Player.AmmoContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.AmmoContainer.MinValue || Globals.Config.Player.AmmoContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.AmmoContainer.MinValue))
                { SlotPrice = Globals.Config.Player.AmmoContainer.UseSlotPrice; name = Globals.Config.Player.AmmoContainer.Name; value = Globals.Config.Player.AmmoContainer.Value; type = Globals.Config.Player.AmmoContainer.Type; subtype = Globals.Config.Player.AmmoContainer.SubType; return true; }
                if ((ouritem.subtype == Helpers.item_subtype.Food || ouritem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.Player.FoodDrinkContainer.Enable && (!Globals.Config.Player.FoodDrinkContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.FoodDrinkContainer.MinValue || Globals.Config.Player.FoodDrinkContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.FoodDrinkContainer.MinValue))
                { SlotPrice = Globals.Config.Player.FoodDrinkContainer.UseSlotPrice; name = Globals.Config.Player.FoodDrinkContainer.Name; value = Globals.Config.Player.FoodDrinkContainer.Value; type = Globals.Config.Player.FoodDrinkContainer.Type; subtype = Globals.Config.Player.FoodDrinkContainer.SubType; return true; }
                if ((ouritem.type == Helpers.item_type.Key && ouritem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.Player.KeyContainer.Enable && (!Globals.Config.Player.KeyContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.KeyContainer.MinValue || Globals.Config.Player.KeyContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.KeyContainer.MinValue))
                { SlotPrice = Globals.Config.Player.KeyContainer.UseSlotPrice; name = Globals.Config.Player.KeyContainer.Name; value = Globals.Config.Player.KeyContainer.Value; type = Globals.Config.Player.KeyContainer.Type; subtype = Globals.Config.Player.KeyContainer.SubType; return true; }
                if (ouritem.subtype == Helpers.item_subtype.Keycard && Globals.Config.Player.KeycardContainer.Enable && (!Globals.Config.Player.KeycardContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.KeycardContainer.MinValue || Globals.Config.Player.KeycardContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.KeycardContainer.MinValue))
                { SlotPrice = Globals.Config.Player.KeycardContainer.UseSlotPrice; name = Globals.Config.Player.KeycardContainer.Name; value = Globals.Config.Player.KeycardContainer.Value; type = Globals.Config.Player.KeycardContainer.Type; subtype = Globals.Config.Player.KeycardContainer.SubType; return true; }
                if (ouritem.subtype == item_subtype.Backpack && Globals.Config.Player.BackpacksContainer.Enable && (!Globals.Config.Player.BackpacksContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.BackpacksContainer.MinValue || Globals.Config.Player.BackpacksContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.BackpacksContainer.MinValue))
                { SlotPrice = Globals.Config.Player.BackpacksContainer.UseSlotPrice; name = Globals.Config.Player.BackpacksContainer.Name; value = Globals.Config.Player.BackpacksContainer.Value; type = Globals.Config.Player.BackpacksContainer.Type; subtype = Globals.Config.Player.BackpacksContainer.SubType; return true; }
                if ((ouritem.type == item_type.Mod || ouritem.type == item_type.Muzzle || ouritem.type == item_type.Sights || ouritem.type == item_type.SpecialScope || ouritem.type == item_type.MasterMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.FunctionalMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.MasterMod) && Globals.Config.Player.AttachmentsContainer.Enable && (!Globals.Config.Player.AttachmentsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.AttachmentsContainer.MinValue || Globals.Config.Player.AttachmentsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.AttachmentsContainer.MinValue))
                { SlotPrice = Globals.Config.Player.AttachmentsContainer.UseSlotPrice; name = Globals.Config.Player.AttachmentsContainer.Name; value = Globals.Config.Player.AttachmentsContainer.Value; type = Globals.Config.Player.AttachmentsContainer.Type; subtype = Globals.Config.Player.AttachmentsContainer.SubType; return true; }
                if (ouritem.subtype == item_subtype.ArmoredEquipment && Globals.Config.Player.ArmourContainer.Enable && (!Globals.Config.Player.ArmourContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.ArmourContainer.MinValue || Globals.Config.Player.ArmourContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.ArmourContainer.MinValue))
                { SlotPrice = Globals.Config.Player.ArmourContainer.UseSlotPrice; name = Globals.Config.Player.ArmourContainer.Name; value = Globals.Config.Player.ArmourContainer.Value; type = Globals.Config.Player.ArmourContainer.Type; subtype = Globals.Config.Player.ArmourContainer.SubType; return true; }
                if (ouritem.subtype == item_subtype.Equipment && Globals.Config.Player.ClothingContainer.Enable && (!Globals.Config.Player.ClothingContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.ClothingContainer.MinValue || Globals.Config.Player.ClothingContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.ClothingContainer.MinValue))
                { SlotPrice = Globals.Config.Player.ClothingContainer.UseSlotPrice; name = Globals.Config.Player.ClothingContainer.Name; value = Globals.Config.Player.ClothingContainer.Value; type = Globals.Config.Player.ClothingContainer.Type; subtype = Globals.Config.Player.ClothingContainer.SubType; return true; }
                if (ouritem.type == item_type.RepairKits && Globals.Config.Player.RepairKitsContainer.Enable && (!Globals.Config.Player.RepairKitsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.RepairKitsContainer.MinValue || Globals.Config.Player.RepairKitsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.RepairKitsContainer.MinValue))
                { SlotPrice = Globals.Config.Player.RepairKitsContainer.UseSlotPrice; name = Globals.Config.Player.RepairKitsContainer.Name; value = Globals.Config.Player.RepairKitsContainer.Value; type = Globals.Config.Player.RepairKitsContainer.Type; subtype = Globals.Config.Player.RepairKitsContainer.SubType; return true; }
                if (ouritem.type == item_type.Meds && Globals.Config.Player.MedsContainer.Enable && (!Globals.Config.Player.MedsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.MedsContainer.MinValue || Globals.Config.Player.MedsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.MedsContainer.MinValue))
                { SlotPrice = Globals.Config.Player.MedsContainer.UseSlotPrice; name = Globals.Config.Player.MedsContainer.Name; value = Globals.Config.Player.MedsContainer.Value; type = Globals.Config.Player.MedsContainer.Type; subtype = Globals.Config.Player.MedsContainer.SubType; return true; }
                if (ouritem.type == item_type.Weapon && Globals.Config.Player.WeaponContainer.Enable && (!Globals.Config.Player.WeaponContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.WeaponContainer.MinValue || Globals.Config.Player.WeaponContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.WeaponContainer.MinValue))
                { SlotPrice = Globals.Config.Player.WeaponContainer.UseSlotPrice; name = Globals.Config.Player.WeaponContainer.Name; value = Globals.Config.Player.WeaponContainer.Value; type = Globals.Config.Player.WeaponContainer.Type; subtype = Globals.Config.Player.WeaponContainer.SubType; return true; }
                if (ouritem.type == item_type.Item && Globals.Config.Player.SpecialItemsContainer.Enable && (!Globals.Config.Player.SpecialItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.SpecialItemsContainer.MinValue || Globals.Config.Player.SpecialItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.SpecialItemsContainer.MinValue))
                { SlotPrice = Globals.Config.Player.SpecialItemsContainer.UseSlotPrice; name = Globals.Config.Player.SpecialItemsContainer.Name; value = Globals.Config.Player.SpecialItemsContainer.Value; type = Globals.Config.Player.SpecialItemsContainer.Type; subtype = Globals.Config.Player.SpecialItemsContainer.SubType; return true; }
                if (ouritem.type == item_type.BarterItem && Globals.Config.Player.BarterItemsContainer.Enable && (!Globals.Config.Player.BarterItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.BarterItemsContainer.MinValue || Globals.Config.Player.BarterItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.BarterItemsContainer.MinValue))
                { SlotPrice = Globals.Config.Player.BarterItemsContainer.UseSlotPrice; name = Globals.Config.Player.BarterItemsContainer.Name; value = Globals.Config.Player.BarterItemsContainer.Value; type = Globals.Config.Player.BarterItemsContainer.Type; subtype = Globals.Config.Player.BarterItemsContainer.SubType; return true; }
                if (ouritem.type == item_type.CompoundItem && Globals.Config.Player.CasesContainer.Enable && (!Globals.Config.Player.CasesContainer.UseSlotPrice && ouritem.price >= Globals.Config.Player.CasesContainer.MinValue || Globals.Config.Player.CasesContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Player.CasesContainer.MinValue))
                { SlotPrice = Globals.Config.Player.CasesContainer.UseSlotPrice; name = Globals.Config.Player.CasesContainer.Name; value = Globals.Config.Player.CasesContainer.Value; type = Globals.Config.Player.CasesContainer.Type; subtype = Globals.Config.Player.CasesContainer.SubType; return true; }
            }
            if (this.PlayerType == PlayerType.ScavPlayer)
            {

                if (Globals.Config.ScavPlayer.WhitelistContainer.Enable && (Globals.Config.Items.WhitelistedItems.Contains(item.Template._id) || (isitemquestrequirement && !item.QuestItem)) && (!Globals.Config.ScavPlayer.WhitelistContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.WhitelistContainer.MinValue || Globals.Config.ScavPlayer.WhitelistContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.WhitelistContainer.MinValue || isitemquestrequirement))
                { ContainsWhitelistedItem = true; SlotPrice = Globals.Config.ScavPlayer.WhitelistContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.WhitelistContainer.Name; value = Globals.Config.ScavPlayer.WhitelistContainer.Value; type = Globals.Config.ScavPlayer.WhitelistContainer.Type; subtype = Globals.Config.ScavPlayer.WhitelistContainer.SubType; return true; }
                if (ouritem.subtype == Helpers.item_subtype.Fuel && Globals.Config.ScavPlayer.FuelContainer.Enable && (!Globals.Config.ScavPlayer.FuelContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.FuelContainer.MinValue || Globals.Config.ScavPlayer.FuelContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.FuelContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.FuelContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.FuelContainer.Name; value = Globals.Config.ScavPlayer.FuelContainer.Value; type = Globals.Config.ScavPlayer.FuelContainer.Type; subtype = Globals.Config.ScavPlayer.FuelContainer.SubType; return true; }
                if ((ouritem.subtype == Helpers.item_subtype.Ammo || ouritem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.ScavPlayer.AmmoContainer.Enable && (!Globals.Config.ScavPlayer.AmmoContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.AmmoContainer.MinValue || Globals.Config.ScavPlayer.AmmoContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.AmmoContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.AmmoContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.AmmoContainer.Name; value = Globals.Config.ScavPlayer.AmmoContainer.Value; type = Globals.Config.ScavPlayer.AmmoContainer.Type; subtype = Globals.Config.ScavPlayer.AmmoContainer.SubType; return true; }
                if ((ouritem.subtype == Helpers.item_subtype.Food || ouritem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.ScavPlayer.FoodDrinkContainer.Enable && (!Globals.Config.ScavPlayer.FoodDrinkContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.FoodDrinkContainer.MinValue || Globals.Config.ScavPlayer.FoodDrinkContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.FoodDrinkContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.FoodDrinkContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.FoodDrinkContainer.Name; value = Globals.Config.ScavPlayer.FoodDrinkContainer.Value; type = Globals.Config.ScavPlayer.FoodDrinkContainer.Type; subtype = Globals.Config.ScavPlayer.FoodDrinkContainer.SubType; return true; }
                if ((ouritem.type == Helpers.item_type.Key && ouritem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.ScavPlayer.KeyContainer.Enable && (!Globals.Config.ScavPlayer.KeyContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.KeyContainer.MinValue || Globals.Config.ScavPlayer.KeyContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.KeyContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.KeyContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.KeyContainer.Name; value = Globals.Config.ScavPlayer.KeyContainer.Value; type = Globals.Config.ScavPlayer.KeyContainer.Type; subtype = Globals.Config.ScavPlayer.KeyContainer.SubType; return true; }
                if (ouritem.subtype == Helpers.item_subtype.Keycard && Globals.Config.ScavPlayer.KeycardContainer.Enable && (!Globals.Config.ScavPlayer.KeycardContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.KeycardContainer.MinValue || Globals.Config.ScavPlayer.KeycardContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.KeycardContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.KeycardContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.KeycardContainer.Name; value = Globals.Config.ScavPlayer.KeycardContainer.Value; type = Globals.Config.ScavPlayer.KeycardContainer.Type; subtype = Globals.Config.ScavPlayer.KeycardContainer.SubType; return true; }
                if (ouritem.subtype == item_subtype.Backpack && Globals.Config.ScavPlayer.BackpacksContainer.Enable && (!Globals.Config.ScavPlayer.BackpacksContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.BackpacksContainer.MinValue || Globals.Config.ScavPlayer.BackpacksContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.BackpacksContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.BackpacksContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.BackpacksContainer.Name; value = Globals.Config.ScavPlayer.BackpacksContainer.Value; type = Globals.Config.ScavPlayer.BackpacksContainer.Type; subtype = Globals.Config.ScavPlayer.BackpacksContainer.SubType; return true; }
                if ((ouritem.type == item_type.Mod || ouritem.type == item_type.Muzzle || ouritem.type == item_type.Sights || ouritem.type == item_type.SpecialScope || ouritem.type == item_type.MasterMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.FunctionalMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.MasterMod) && Globals.Config.ScavPlayer.AttachmentsContainer.Enable && (!Globals.Config.ScavPlayer.AttachmentsContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.AttachmentsContainer.MinValue || Globals.Config.ScavPlayer.AttachmentsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.AttachmentsContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.AttachmentsContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.AttachmentsContainer.Name; value = Globals.Config.ScavPlayer.AttachmentsContainer.Value; type = Globals.Config.ScavPlayer.AttachmentsContainer.Type; subtype = Globals.Config.ScavPlayer.AttachmentsContainer.SubType; return true; }
                if (ouritem.subtype == item_subtype.ArmoredEquipment && Globals.Config.ScavPlayer.ArmourContainer.Enable && (!Globals.Config.ScavPlayer.ArmourContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.ArmourContainer.MinValue || Globals.Config.ScavPlayer.ArmourContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.ArmourContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.ArmourContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.ArmourContainer.Name; value = Globals.Config.ScavPlayer.ArmourContainer.Value; type = Globals.Config.ScavPlayer.ArmourContainer.Type; subtype = Globals.Config.ScavPlayer.ArmourContainer.SubType; return true; }
                if (ouritem.subtype == item_subtype.Equipment && Globals.Config.ScavPlayer.ClothingContainer.Enable && (!Globals.Config.ScavPlayer.ClothingContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.ClothingContainer.MinValue || Globals.Config.ScavPlayer.ClothingContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.ClothingContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.ClothingContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.ClothingContainer.Name; value = Globals.Config.ScavPlayer.ClothingContainer.Value; type = Globals.Config.ScavPlayer.ClothingContainer.Type; subtype = Globals.Config.ScavPlayer.ClothingContainer.SubType; return true; }
                if (ouritem.type == item_type.RepairKits && Globals.Config.ScavPlayer.RepairKitsContainer.Enable && (!Globals.Config.ScavPlayer.RepairKitsContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.RepairKitsContainer.MinValue || Globals.Config.ScavPlayer.RepairKitsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.RepairKitsContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.RepairKitsContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.RepairKitsContainer.Name; value = Globals.Config.ScavPlayer.RepairKitsContainer.Value; type = Globals.Config.ScavPlayer.RepairKitsContainer.Type; subtype = Globals.Config.ScavPlayer.RepairKitsContainer.SubType; return true; }
                if (ouritem.type == item_type.Meds && Globals.Config.ScavPlayer.MedsContainer.Enable && (!Globals.Config.ScavPlayer.MedsContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.MedsContainer.MinValue || Globals.Config.ScavPlayer.MedsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.MedsContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.MedsContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.MedsContainer.Name; value = Globals.Config.ScavPlayer.MedsContainer.Value; type = Globals.Config.ScavPlayer.MedsContainer.Type; subtype = Globals.Config.ScavPlayer.MedsContainer.SubType; return true; }
                if (ouritem.type == item_type.Weapon && Globals.Config.ScavPlayer.WeaponContainer.Enable && (!Globals.Config.ScavPlayer.WeaponContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.WeaponContainer.MinValue || Globals.Config.ScavPlayer.WeaponContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.WeaponContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.WeaponContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.WeaponContainer.Name; value = Globals.Config.ScavPlayer.WeaponContainer.Value; type = Globals.Config.ScavPlayer.WeaponContainer.Type; subtype = Globals.Config.ScavPlayer.WeaponContainer.SubType; return true; }
                if (ouritem.type == item_type.Item && Globals.Config.ScavPlayer.SpecialItemsContainer.Enable && (!Globals.Config.ScavPlayer.SpecialItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.SpecialItemsContainer.MinValue || Globals.Config.ScavPlayer.SpecialItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.SpecialItemsContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.SpecialItemsContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.SpecialItemsContainer.Name; value = Globals.Config.ScavPlayer.SpecialItemsContainer.Value; type = Globals.Config.ScavPlayer.SpecialItemsContainer.Type; subtype = Globals.Config.ScavPlayer.SpecialItemsContainer.SubType; return true; }
                if (ouritem.type == item_type.BarterItem && Globals.Config.ScavPlayer.BarterItemsContainer.Enable && (!Globals.Config.ScavPlayer.BarterItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.BarterItemsContainer.MinValue || Globals.Config.ScavPlayer.BarterItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.BarterItemsContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.BarterItemsContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.BarterItemsContainer.Name; value = Globals.Config.ScavPlayer.BarterItemsContainer.Value; type = Globals.Config.ScavPlayer.BarterItemsContainer.Type; subtype = Globals.Config.ScavPlayer.BarterItemsContainer.SubType; return true; }
                if (ouritem.type == item_type.CompoundItem && Globals.Config.ScavPlayer.CasesContainer.Enable && (!Globals.Config.ScavPlayer.CasesContainer.UseSlotPrice && ouritem.price >= Globals.Config.ScavPlayer.CasesContainer.MinValue || Globals.Config.ScavPlayer.CasesContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.ScavPlayer.CasesContainer.MinValue))
                { SlotPrice = Globals.Config.ScavPlayer.CasesContainer.UseSlotPrice; name = Globals.Config.ScavPlayer.CasesContainer.Name; value = Globals.Config.ScavPlayer.CasesContainer.Value; type = Globals.Config.ScavPlayer.CasesContainer.Type; subtype = Globals.Config.ScavPlayer.CasesContainer.SubType; return true; }
            }
                if (this.PlayerType == PlayerType.Scav)
                {

                    if (Globals.Config.Scav.WhitelistContainer.Enable && (Globals.Config.Items.WhitelistedItems.Contains(item.Template._id) || (isitemquestrequirement && !item.QuestItem)) && (!Globals.Config.Scav.WhitelistContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.WhitelistContainer.MinValue || Globals.Config.Scav.WhitelistContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.WhitelistContainer.MinValue || isitemquestrequirement))
                { ContainsWhitelistedItem = true; SlotPrice = Globals.Config.Scav.WhitelistContainer.UseSlotPrice; name = Globals.Config.Scav.WhitelistContainer.Name; value = Globals.Config.Scav.WhitelistContainer.Value; type = Globals.Config.Scav.WhitelistContainer.Type; subtype = Globals.Config.Scav.WhitelistContainer.SubType; return true; }
                    if (ouritem.subtype == Helpers.item_subtype.Fuel && Globals.Config.Scav.FuelContainer.Enable && (!Globals.Config.Scav.FuelContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.FuelContainer.MinValue || Globals.Config.Scav.FuelContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.FuelContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.FuelContainer.UseSlotPrice; name = Globals.Config.Scav.FuelContainer.Name; value = Globals.Config.Scav.FuelContainer.Value; type = Globals.Config.Scav.FuelContainer.Type; subtype = Globals.Config.Scav.FuelContainer.SubType; return true; }
                    if ((ouritem.subtype == Helpers.item_subtype.Ammo || ouritem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.Scav.AmmoContainer.Enable && (!Globals.Config.Scav.AmmoContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.AmmoContainer.MinValue || Globals.Config.Scav.AmmoContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.AmmoContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.AmmoContainer.UseSlotPrice; name = Globals.Config.Scav.AmmoContainer.Name; value = Globals.Config.Scav.AmmoContainer.Value; type = Globals.Config.Scav.AmmoContainer.Type; subtype = Globals.Config.Scav.AmmoContainer.SubType; return true; }
                    if ((ouritem.subtype == Helpers.item_subtype.Food || ouritem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.Scav.FoodDrinkContainer.Enable && (!Globals.Config.Scav.FoodDrinkContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.FoodDrinkContainer.MinValue || Globals.Config.Scav.FoodDrinkContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.FoodDrinkContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.FoodDrinkContainer.UseSlotPrice; name = Globals.Config.Scav.FoodDrinkContainer.Name; value = Globals.Config.Scav.FoodDrinkContainer.Value; type = Globals.Config.Scav.FoodDrinkContainer.Type; subtype = Globals.Config.Scav.FoodDrinkContainer.SubType; return true; }
                    if ((ouritem.type == Helpers.item_type.Key && ouritem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.Scav.KeyContainer.Enable && (!Globals.Config.Scav.KeyContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.KeyContainer.MinValue || Globals.Config.Scav.KeyContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.KeyContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.KeyContainer.UseSlotPrice; name = Globals.Config.Scav.KeyContainer.Name; value = Globals.Config.Scav.KeyContainer.Value; type = Globals.Config.Scav.KeyContainer.Type; subtype = Globals.Config.Scav.KeyContainer.SubType; return true; }
                    if (ouritem.subtype == Helpers.item_subtype.Keycard && Globals.Config.Scav.KeycardContainer.Enable && (!Globals.Config.Scav.KeycardContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.KeycardContainer.MinValue || Globals.Config.Scav.KeycardContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.KeycardContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.KeycardContainer.UseSlotPrice; name = Globals.Config.Scav.KeycardContainer.Name; value = Globals.Config.Scav.KeycardContainer.Value; type = Globals.Config.Scav.KeycardContainer.Type; subtype = Globals.Config.Scav.KeycardContainer.SubType; return true; }
                    if (ouritem.subtype == item_subtype.Backpack && Globals.Config.Scav.BackpacksContainer.Enable && (!Globals.Config.Scav.BackpacksContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.BackpacksContainer.MinValue || Globals.Config.Scav.BackpacksContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.BackpacksContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.BackpacksContainer.UseSlotPrice; name = Globals.Config.Scav.BackpacksContainer.Name; value = Globals.Config.Scav.BackpacksContainer.Value; type = Globals.Config.Scav.BackpacksContainer.Type; subtype = Globals.Config.Scav.BackpacksContainer.SubType; return true; }
                    if ((ouritem.type == item_type.Mod || ouritem.type == item_type.Muzzle || ouritem.type == item_type.Sights || ouritem.type == item_type.SpecialScope || ouritem.type == item_type.MasterMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.FunctionalMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.MasterMod) && Globals.Config.Scav.AttachmentsContainer.Enable && (!Globals.Config.Scav.AttachmentsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.AttachmentsContainer.MinValue || Globals.Config.Scav.AttachmentsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.AttachmentsContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.AttachmentsContainer.UseSlotPrice; name = Globals.Config.Scav.AttachmentsContainer.Name; value = Globals.Config.Scav.AttachmentsContainer.Value; type = Globals.Config.Scav.AttachmentsContainer.Type; subtype = Globals.Config.Scav.AttachmentsContainer.SubType; return true; }
                    if (ouritem.subtype == item_subtype.ArmoredEquipment && Globals.Config.Scav.ArmourContainer.Enable && (!Globals.Config.Scav.ArmourContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.ArmourContainer.MinValue || Globals.Config.Scav.ArmourContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.ArmourContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.ArmourContainer.UseSlotPrice; name = Globals.Config.Scav.ArmourContainer.Name; value = Globals.Config.Scav.ArmourContainer.Value; type = Globals.Config.Scav.ArmourContainer.Type; subtype = Globals.Config.Scav.ArmourContainer.SubType; return true; }
                    if (ouritem.subtype == item_subtype.Equipment && Globals.Config.Scav.ClothingContainer.Enable && (!Globals.Config.Scav.ClothingContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.ClothingContainer.MinValue || Globals.Config.Scav.ClothingContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.ClothingContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.ClothingContainer.UseSlotPrice; name = Globals.Config.Scav.ClothingContainer.Name; value = Globals.Config.Scav.ClothingContainer.Value; type = Globals.Config.Scav.ClothingContainer.Type; subtype = Globals.Config.Scav.ClothingContainer.SubType; return true; }
                    if (ouritem.type == item_type.RepairKits && Globals.Config.Scav.RepairKitsContainer.Enable && (!Globals.Config.Scav.RepairKitsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.RepairKitsContainer.MinValue || Globals.Config.Scav.RepairKitsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.RepairKitsContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.RepairKitsContainer.UseSlotPrice; name = Globals.Config.Scav.RepairKitsContainer.Name; value = Globals.Config.Scav.RepairKitsContainer.Value; type = Globals.Config.Scav.RepairKitsContainer.Type; subtype = Globals.Config.Scav.RepairKitsContainer.SubType; return true; }
                    if (ouritem.type == item_type.Meds && Globals.Config.Scav.MedsContainer.Enable && (!Globals.Config.Scav.MedsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.MedsContainer.MinValue || Globals.Config.Scav.MedsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.MedsContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.MedsContainer.UseSlotPrice; name = Globals.Config.Scav.MedsContainer.Name; value = Globals.Config.Scav.MedsContainer.Value; type = Globals.Config.Scav.MedsContainer.Type; subtype = Globals.Config.Scav.MedsContainer.SubType; return true; }
                    if (ouritem.type == item_type.Weapon && Globals.Config.Scav.WeaponContainer.Enable && (!Globals.Config.Scav.WeaponContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.WeaponContainer.MinValue || Globals.Config.Scav.WeaponContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.WeaponContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.WeaponContainer.UseSlotPrice; name = Globals.Config.Scav.WeaponContainer.Name; value = Globals.Config.Scav.WeaponContainer.Value; type = Globals.Config.Scav.WeaponContainer.Type; subtype = Globals.Config.Scav.WeaponContainer.SubType; return true; }
                    if (ouritem.type == item_type.Item && Globals.Config.Scav.SpecialItemsContainer.Enable && (!Globals.Config.Scav.SpecialItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.SpecialItemsContainer.MinValue || Globals.Config.Scav.SpecialItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.SpecialItemsContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.SpecialItemsContainer.UseSlotPrice; name = Globals.Config.Scav.SpecialItemsContainer.Name; value = Globals.Config.Scav.SpecialItemsContainer.Value; type = Globals.Config.Scav.SpecialItemsContainer.Type; subtype = Globals.Config.Scav.SpecialItemsContainer.SubType; return true; }
                    if (ouritem.type == item_type.BarterItem && Globals.Config.Scav.BarterItemsContainer.Enable && (!Globals.Config.Scav.BarterItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.BarterItemsContainer.MinValue || Globals.Config.Scav.BarterItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.BarterItemsContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.BarterItemsContainer.UseSlotPrice; name = Globals.Config.Scav.BarterItemsContainer.Name; value = Globals.Config.Scav.BarterItemsContainer.Value; type = Globals.Config.Scav.BarterItemsContainer.Type; subtype = Globals.Config.Scav.BarterItemsContainer.SubType; return true; }
                    if (ouritem.type == item_type.CompoundItem && Globals.Config.Scav.CasesContainer.Enable && (!Globals.Config.Scav.CasesContainer.UseSlotPrice && ouritem.price >= Globals.Config.Scav.CasesContainer.MinValue || Globals.Config.Scav.CasesContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Scav.CasesContainer.MinValue))
                    { SlotPrice = Globals.Config.Scav.CasesContainer.UseSlotPrice; name = Globals.Config.Scav.CasesContainer.Name; value = Globals.Config.Scav.CasesContainer.Value; type = Globals.Config.Scav.CasesContainer.Type; subtype = Globals.Config.Scav.CasesContainer.SubType; return true; }
                }
                if (this.PlayerType == PlayerType.Boss)
                {

                    if (Globals.Config.Boss.WhitelistContainer.Enable && (Globals.Config.Items.WhitelistedItems.Contains(item.Template._id) || (isitemquestrequirement && !item.QuestItem)) && (!Globals.Config.Boss.WhitelistContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.WhitelistContainer.MinValue || Globals.Config.Boss.WhitelistContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.WhitelistContainer.MinValue || isitemquestrequirement))
                { ContainsWhitelistedItem = true; SlotPrice = Globals.Config.Boss.WhitelistContainer.UseSlotPrice; name = Globals.Config.Boss.WhitelistContainer.Name; value = Globals.Config.Boss.WhitelistContainer.Value; type = Globals.Config.Boss.WhitelistContainer.Type; subtype = Globals.Config.Boss.WhitelistContainer.SubType; return true; }
                    if (ouritem.subtype == Helpers.item_subtype.Fuel && Globals.Config.Boss.FuelContainer.Enable && (!Globals.Config.Boss.FuelContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.FuelContainer.MinValue || Globals.Config.Boss.FuelContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.FuelContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.FuelContainer.UseSlotPrice; name = Globals.Config.Boss.FuelContainer.Name; value = Globals.Config.Boss.FuelContainer.Value; type = Globals.Config.Boss.FuelContainer.Type; subtype = Globals.Config.Boss.FuelContainer.SubType; return true; }
                    if ((ouritem.subtype == Helpers.item_subtype.Ammo || ouritem.subtype == Helpers.item_subtype.AmmoBox) && Globals.Config.Boss.AmmoContainer.Enable && (!Globals.Config.Boss.AmmoContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.AmmoContainer.MinValue || Globals.Config.Boss.AmmoContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.AmmoContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.AmmoContainer.UseSlotPrice; name = Globals.Config.Boss.AmmoContainer.Name; value = Globals.Config.Boss.AmmoContainer.Value; type = Globals.Config.Boss.AmmoContainer.Type; subtype = Globals.Config.Boss.AmmoContainer.SubType; return true; }
                    if ((ouritem.subtype == Helpers.item_subtype.Food || ouritem.subtype == Helpers.item_subtype.FoodDrink) && Globals.Config.Boss.FoodDrinkContainer.Enable && (!Globals.Config.Boss.FoodDrinkContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.FoodDrinkContainer.MinValue || Globals.Config.Boss.FoodDrinkContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.FoodDrinkContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.FoodDrinkContainer.UseSlotPrice; name = Globals.Config.Boss.FoodDrinkContainer.Name; value = Globals.Config.Boss.FoodDrinkContainer.Value; type = Globals.Config.Boss.FoodDrinkContainer.Type; subtype = Globals.Config.Boss.FoodDrinkContainer.SubType; return true; }
                    if ((ouritem.type == Helpers.item_type.Key && ouritem.subtype != Helpers.item_subtype.Keycard) && Globals.Config.Boss.KeyContainer.Enable && (!Globals.Config.Boss.KeyContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.KeyContainer.MinValue || Globals.Config.Boss.KeyContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.KeyContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.KeyContainer.UseSlotPrice; name = Globals.Config.Boss.KeyContainer.Name; value = Globals.Config.Boss.KeyContainer.Value; type = Globals.Config.Boss.KeyContainer.Type; subtype = Globals.Config.Boss.KeyContainer.SubType; return true; }
                    if (ouritem.subtype == Helpers.item_subtype.Keycard && Globals.Config.Boss.KeycardContainer.Enable && (!Globals.Config.Boss.KeycardContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.KeycardContainer.MinValue || Globals.Config.Boss.KeycardContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.KeycardContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.KeycardContainer.UseSlotPrice; name = Globals.Config.Boss.KeycardContainer.Name; value = Globals.Config.Boss.KeycardContainer.Value; type = Globals.Config.Boss.KeycardContainer.Type; subtype = Globals.Config.Boss.KeycardContainer.SubType; return true; }
                    if (ouritem.subtype == item_subtype.Backpack && Globals.Config.Boss.BackpacksContainer.Enable && (!Globals.Config.Boss.BackpacksContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.BackpacksContainer.MinValue || Globals.Config.Boss.BackpacksContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.BackpacksContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.BackpacksContainer.UseSlotPrice; name = Globals.Config.Boss.BackpacksContainer.Name; value = Globals.Config.Boss.BackpacksContainer.Value; type = Globals.Config.Boss.BackpacksContainer.Type; subtype = Globals.Config.Boss.BackpacksContainer.SubType; return true; }
                    if ((ouritem.type == item_type.Mod || ouritem.type == item_type.Muzzle || ouritem.type == item_type.Sights || ouritem.type == item_type.SpecialScope || ouritem.type == item_type.MasterMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.FunctionalMod || ouritem.type == item_type.GearMod || ouritem.type == item_type.MasterMod) && Globals.Config.Boss.AttachmentsContainer.Enable && (!Globals.Config.Boss.AttachmentsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.AttachmentsContainer.MinValue || Globals.Config.Boss.AttachmentsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.AttachmentsContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.AttachmentsContainer.UseSlotPrice; name = Globals.Config.Boss.AttachmentsContainer.Name; value = Globals.Config.Boss.AttachmentsContainer.Value; type = Globals.Config.Boss.AttachmentsContainer.Type; subtype = Globals.Config.Boss.AttachmentsContainer.SubType; return true; }
                    if (ouritem.subtype == item_subtype.ArmoredEquipment && Globals.Config.Boss.ArmourContainer.Enable && (!Globals.Config.Boss.ArmourContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.ArmourContainer.MinValue || Globals.Config.Boss.ArmourContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.ArmourContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.ArmourContainer.UseSlotPrice; name = Globals.Config.Boss.ArmourContainer.Name; value = Globals.Config.Boss.ArmourContainer.Value; type = Globals.Config.Boss.ArmourContainer.Type; subtype = Globals.Config.Boss.ArmourContainer.SubType; return true; }
                    if (ouritem.subtype == item_subtype.Equipment && Globals.Config.Boss.ClothingContainer.Enable && (!Globals.Config.Boss.ClothingContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.ClothingContainer.MinValue || Globals.Config.Boss.ClothingContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.ClothingContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.ClothingContainer.UseSlotPrice; name = Globals.Config.Boss.ClothingContainer.Name; value = Globals.Config.Boss.ClothingContainer.Value; type = Globals.Config.Boss.ClothingContainer.Type; subtype = Globals.Config.Boss.ClothingContainer.SubType; return true; }
                    if (ouritem.type == item_type.RepairKits && Globals.Config.Boss.RepairKitsContainer.Enable && (!Globals.Config.Boss.RepairKitsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.RepairKitsContainer.MinValue || Globals.Config.Boss.RepairKitsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.RepairKitsContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.RepairKitsContainer.UseSlotPrice; name = Globals.Config.Boss.RepairKitsContainer.Name; value = Globals.Config.Boss.RepairKitsContainer.Value; type = Globals.Config.Boss.RepairKitsContainer.Type; subtype = Globals.Config.Boss.RepairKitsContainer.SubType; return true; }
                    if (ouritem.type == item_type.Meds && Globals.Config.Boss.MedsContainer.Enable && (!Globals.Config.Boss.MedsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.MedsContainer.MinValue || Globals.Config.Boss.MedsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.MedsContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.MedsContainer.UseSlotPrice; name = Globals.Config.Boss.MedsContainer.Name; value = Globals.Config.Boss.MedsContainer.Value; type = Globals.Config.Boss.MedsContainer.Type; subtype = Globals.Config.Boss.MedsContainer.SubType; return true; }
                    if (ouritem.type == item_type.Weapon && Globals.Config.Boss.WeaponContainer.Enable && (!Globals.Config.Boss.WeaponContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.WeaponContainer.MinValue || Globals.Config.Boss.WeaponContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.WeaponContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.WeaponContainer.UseSlotPrice; name = Globals.Config.Boss.WeaponContainer.Name; value = Globals.Config.Boss.WeaponContainer.Value; type = Globals.Config.Boss.WeaponContainer.Type; subtype = Globals.Config.Boss.WeaponContainer.SubType; return true; }
                    if (ouritem.type == item_type.Item && Globals.Config.Boss.SpecialItemsContainer.Enable && (!Globals.Config.Boss.SpecialItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.SpecialItemsContainer.MinValue || Globals.Config.Boss.SpecialItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.SpecialItemsContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.SpecialItemsContainer.UseSlotPrice; name = Globals.Config.Boss.SpecialItemsContainer.Name; value = Globals.Config.Boss.SpecialItemsContainer.Value; type = Globals.Config.Boss.SpecialItemsContainer.Type; subtype = Globals.Config.Boss.SpecialItemsContainer.SubType; return true; }
                    if (ouritem.type == item_type.BarterItem && Globals.Config.Boss.BarterItemsContainer.Enable && (!Globals.Config.Boss.BarterItemsContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.BarterItemsContainer.MinValue || Globals.Config.Boss.BarterItemsContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.BarterItemsContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.BarterItemsContainer.UseSlotPrice; name = Globals.Config.Boss.BarterItemsContainer.Name; value = Globals.Config.Boss.BarterItemsContainer.Value; type = Globals.Config.Boss.BarterItemsContainer.Type; subtype = Globals.Config.Boss.BarterItemsContainer.SubType; return true; }
                    if (ouritem.type == item_type.CompoundItem && Globals.Config.Boss.CasesContainer.Enable && (!Globals.Config.Boss.CasesContainer.UseSlotPrice && ouritem.price >= Globals.Config.Boss.CasesContainer.MinValue || Globals.Config.Boss.CasesContainer.UseSlotPrice && ouritem.price_per_slot >= Globals.Config.Boss.CasesContainer.MinValue))
                    { SlotPrice = Globals.Config.Boss.CasesContainer.UseSlotPrice; name = Globals.Config.Boss.CasesContainer.Name; value = Globals.Config.Boss.CasesContainer.Value; type = Globals.Config.Boss.CasesContainer.Type; subtype = Globals.Config.Boss.CasesContainer.SubType; return true; }
                }
            
                SlotPrice = true;
            name = true;
            value = true;
            subtype = true;
            type = true;
            return false;
        }
     
        public void ApplyChams() // stolen from a 4 year old version of chases source that i dumped
        {
            try
            {
                bool enabled = false;
                if (ScavInstance != null)
                    enabled = ScavInstance.Chams;
                if (PlayerInstance != null)
                    enabled = PlayerInstance.Chams;
                int Shader = 0;
                if (ScavInstance != null)
                    Shader = ScavInstance.ChamsType;
                if (PlayerInstance != null)
                    Shader = PlayerInstance.ChamsType;
                UnityEngine.Shader shader = null;
                if (Shader == 0)
                    shader = Helpers.ShaderHelper.Flat;
                if (Shader == 1)
                    shader = Helpers.ShaderHelper.Textured;
                if (Shader == 2)
                    shader = Helpers.ShaderHelper.Pulse;
                if (Shader == 3)
                    shader = Helpers.ShaderHelper.Rainbow;
                if (Shader == 4)
                    shader = Helpers.ShaderHelper.Wireframe;
                if (Shader == 5)
                    shader = Helpers.ShaderHelper.TransparentShader;
                Color32 primarycolour = new Color32();
                Color32 secondarycolour = new Color32();
                if (this.PlayerType == PlayerType.Scav)
                {
                    primarycolour = ColourHelper.GetColour("Scav Chams Primary Colour");
                    secondarycolour = ColourHelper.GetColour("Scav Chams Secondary Colour");
                }
                if (this.PlayerType == PlayerType.Player)
                {
                    primarycolour = ColourHelper.GetColour("Player Chams Primary Colour");
                    secondarycolour = ColourHelper.GetColour("Player Chams Secondary Colour");
                }
                if (this.PlayerType == PlayerType.ScavPlayer)
                {
                    primarycolour = ColourHelper.GetColour("ScavPlayer Chams Primary Colour");
                    secondarycolour = ColourHelper.GetColour("ScavPlayer Chams Secondary Colour");
                }
                if (this.PlayerType == PlayerType.Boss)
                {
                    primarycolour = ColourHelper.GetColour("Boss Chams Primary Colour");
                    secondarycolour = ColourHelper.GetColour("Boss Chams Secondary Colour");
                }
                if (this.Friendly)
                {
                    primarycolour = ColourHelper.GetColour("Player Friend Chams Primary Colour");
                    secondarycolour = ColourHelper.GetColour("Player Friend Chams Secondary Colour");
                }
                var skins = Player.PlayerBody.BodySkins.Values;
                foreach (var r in Player.gameObject.GetComponentsInChildren<UnityEngine.Renderer>())
                {

                    string name = r.material.name;
                    if (ScavInstance != null && !(ScavInstance.ApplyChamsToGear) && (name.Contains("weapon") || name.Contains("ammo") || name.Contains("sight") || name.Contains("grip") || name.Contains("mount") || name.Contains("item") || name.Contains("mag") || name.Contains("tactical") || name.Contains("scope") || name.Contains("barrel") || name.Contains("patron") || name.Contains("muzzle")))
                        continue;
                    if (PlayerInstance != null && !(PlayerInstance.ApplyChamsToGear) && (name.Contains("weapon") || name.Contains("ammo") || name.Contains("sight") || name.Contains("grip") || name.Contains("mount") || name.Contains("item") || name.Contains("mag") || name.Contains("tactical") || name.Contains("scope") || name.Contains("barrel") || name.Contains("patron") || name.Contains("muzzle")))
                        continue;
                    if (Shader == 6)
                        r.material = ShaderHelper.GalaxyMat;
                    foreach (Material m in r.materials)
                    {
                        if (enabled)
                        {
                            if (Shader == 0 || Shader == 1)
                            {
                                m.SetColor("_ColorVisible", secondarycolour);
                                m.SetColor("_ColorBehind", primarycolour);
                            }
                            if (Shader == 2)
                            {
                                m.SetColor("_Emissioncolour", primarycolour);
                            }
                            if (Shader == 4)
                            {
                                m.SetColor("_WireColor", primarycolour);
                            }
                            if (Shader == 5)
                            {
                                m.SetColor("_Color", primarycolour);
                            }
                            if (!Globals.Shaders.ContainsKey(m.name))
                            {
                                Globals.Shaders.Add(m.name, m.shader);
                            }

                            if (m.shader != shader && Shader != 6)
                            {
                                m.shader = shader;
                            }
                        }
                        else
                        {

                            if (Globals.Shaders.ContainsKey(m.name))
                            {

                                if (m.shader == shader)
                                {
                                    m.shader = Globals.Shaders[m.name];
                                }
                            }
                        }

                    }
                }
            }
            catch { }
        }
        private int NextTime = 0;

        public void SetPlayerValue()
        {
            if (Time.time > NextTime)
            {
                int value = 0;
                List<SDK.ContainerItems> itemlist = new List<SDK.ContainerItems>();
                foreach (var slot in this.Player.Profile.Inventory.Equipment.AllSlots)
                {

                    // in recode add bool to skip attachments in value
                    if (slot.Name == "SecuredContainer")
                        continue;
                    if (this.PlayerType == PlayerType.Player && slot.Name == "ArmBand")
                        continue;
                    if (this.PlayerType == PlayerType.Player && slot.Name == "Dogtag")
                        continue;
                    if (this.PlayerType == PlayerType.Player && slot.Name == "Scabbard") // knife
                        continue;

                    try
                    {
                        foreach (Item item in slot.Items)
                        {
                            try
                            {
                                if (item.Template._id == "5f4f9eb969cdc30ff33f09db") // compass
                                    continue;
                                if (item.Template._id == "557ffd194bdc2d28148b457f") // actual pocket id
                                    continue;
                                if (item.Template._id == "55d7217a4bdc2d86028b456d") // default inventory
                                    continue;
                                Helpers.OurItem ouritem = Helpers.ItemPriceHelper.list[item.Template._id];
                                if (this.PlayerType == PlayerType.Player && Globals.Config.Player.SkipAttachmentsInValue && (ouritem.type == Helpers.item_type.Muzzle || ouritem.type == Helpers.item_type.Mod || ouritem.type == Helpers.item_type.FunctionalMod || ouritem.type == Helpers.item_type.MasterMod || ouritem.type == Helpers.item_type.Sights || ouritem.type == Helpers.item_type.Magazine))
                                    continue;
                                if (this.PlayerType == PlayerType.Scav && Globals.Config.Scav.SkipAttachmentsInValue && (ouritem.type == Helpers.item_type.Muzzle || ouritem.type == Helpers.item_type.Mod || ouritem.type == Helpers.item_type.FunctionalMod || ouritem.type == Helpers.item_type.MasterMod || ouritem.type == Helpers.item_type.Sights || ouritem.type == Helpers.item_type.Magazine))
                                    continue;
                                if (this.PlayerType == PlayerType.ScavPlayer && Globals.Config.ScavPlayer.SkipAttachmentsInValue && (ouritem.type == Helpers.item_type.Muzzle || ouritem.type == Helpers.item_type.Mod || ouritem.type == Helpers.item_type.FunctionalMod || ouritem.type == Helpers.item_type.MasterMod || ouritem.type == Helpers.item_type.Sights || ouritem.type == Helpers.item_type.Magazine))
                                    continue;
                                if (this.PlayerType == PlayerType.Boss && Globals.Config.Boss.SkipAttachmentsInValue && (ouritem.type == Helpers.item_type.Muzzle || ouritem.type == Helpers.item_type.Mod || ouritem.type == Helpers.item_type.FunctionalMod || ouritem.type == Helpers.item_type.MasterMod || ouritem.type == Helpers.item_type.Sights || ouritem.type == Helpers.item_type.Magazine))
                                    continue;

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
                            catch (Exception ex) { System.IO.File.WriteAllText("player sdk 1", $"{ex.Message}\n{ex.ToString()}"); }
                        }
                    }
                    catch (Exception ex) { System.IO.File.WriteAllText("player sdk 2", $"{ex.Message}\n{ex.ToString()}"); }

                }
                System.Random rand = new System.Random();
                NextTime = (int)(Time.time + 4 + rand.Next(0, 3)); // this will help distribute the heft 
                this.Value = value;
                this.ItemList = OrganizeByValue(itemlist);
                this.ItemList.Reverse();
            }
        }
        public bool Friendly;
        public Weapon Weapon;
        public Player Player;
        public int Distance;
        public Vector3 W2SPos;
        public Vector3[] BoneW2S = new Vector3[15];
        public Vector3[] BoneWorld = new Vector3[15];
        public bool[] BoneVisible = new bool[15];
        public PlayerType PlayerType;
        public bool W2SCalculated = false;
        public int Value = 0;
        public List<ContainerItems> ItemList;
        public Vector3 TargetPos;
        public Vector3 TargetPosScreenPred;
        public Vector3 TargetPosScreen;
        public Configs.Scav ScavInstance = null;
        public Configs.Player PlayerInstance = null;
    }
}
