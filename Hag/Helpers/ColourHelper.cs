using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Hag.Helpers
{
    class ColourHelper
    {
        #region RGBColours
        public static int Cases = 0;
        public static float R = 1.00f;
        public static float G = 0.00f;
        public static float B = 1.00f;
        #endregion

        public static Hag.Renderer.Direct2DColor GetItemColour(SDK.ContainerItems items)
        {
            Hag.Renderer.Direct2DColor colour = new Renderer.Direct2DColor();

            if (Globals.Config.Items.WhitelistedItems.Contains(items.Item.TemplateId) || (items.QuestRequirement && !items.Item.QuestItem))
            {
                Color unitycolour = GetColour("Item Whitelisted Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.subtype == Helpers.item_subtype.Fuel)
            {
                Color unitycolour = GetColour("Item Fuel Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if ((items.OurItem.subtype == Helpers.item_subtype.Ammo || items.OurItem.subtype == Helpers.item_subtype.AmmoBox))
            {
                Color unitycolour = GetColour("Item Ammo Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if ((items.OurItem.subtype == Helpers.item_subtype.Food || items.OurItem.subtype == Helpers.item_subtype.FoodDrink))
            {
                Color unitycolour = GetColour("Item Food/Drink Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if ((items.OurItem.type == Helpers.item_type.Key && items.OurItem.subtype != Helpers.item_subtype.Keycard))
            {
                Color unitycolour = GetColour("Item Key Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.subtype == Helpers.item_subtype.Keycard )
            {
                Color unitycolour = GetColour("Item Keycard Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.subtype == item_subtype.Backpack)
            {
                Color unitycolour = GetColour("Item Backpack Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if ((items.OurItem.type == item_type.Mod || items.OurItem.type == item_type.Muzzle || items.OurItem.type == item_type.Sights || items.OurItem.type == item_type.SpecialScope || items.OurItem.type == item_type.MasterMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.FunctionalMod || items.OurItem.type == item_type.GearMod || items.OurItem.type == item_type.MasterMod))
            {
                Color unitycolour = GetColour("Item Attachment Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.subtype == item_subtype.ArmoredEquipment )
            {
                Color unitycolour = GetColour("Item Armour Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.subtype == item_subtype.Equipment)
            {
                Color unitycolour = GetColour("Item Clothing Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.type == item_type.RepairKits )
            {
                Color unitycolour = GetColour("Item RepairKit Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.type == item_type.Meds )
            {
                Color unitycolour = GetColour("Item Medical Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.type == item_type.Weapon)
            {
                Color unitycolour = GetColour("Item Weapon Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.type == item_type.Item)
            {
                Color unitycolour = GetColour("Item Special Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.type == item_type.BarterItem)
            {
                Color unitycolour = GetColour("Item Barter Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.OurItem.type == item_type.CompoundItem)
            {
                Color unitycolour = GetColour("Item Case Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            if (items.Item.QuestItem)
            {
                Color unitycolour = GetColour("Item Quest Item Text Colour");
                colour = new Renderer.Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                return colour;
            }
            return new Renderer.Direct2DColor(255,255,255,255);
        }
        public static void AddColours()
        {
            AddColour("Menu Primary Colour", Color.red);
            AddColour("Menu Secondary Colour", Color.white);
            AddColour("Menu Background Colour", new Color32(30,30,30,150));

            AddColour("Aimbot Fov Colour", new Color32(255, 255, 255, 255));
            AddColour("Aimbot Target Line Colour", new Color32(0,150, 255, 255));
            AddColour("Aimbot Prediction Crosshair Primary Colour", new Color32(255, 255, 255, 255));
            AddColour("Aimbot Prediction Crosshair Secondary Colour", new Color32(0, 150, 255, 255));

            AddColour("Player Text Colour", Color.red);
            AddColour("Player Text Visible Colour", Color.red);
            AddColour("Player Text Invisible Colour", Color.white);
            AddColour("Player Bone Visible Colour", Color.red);
            AddColour("Player Bone Invisible Colour", Color.white);
            AddColour("Player Healthbar Colour", Color.green);
            AddColour("Player Healthbar Background Colour", Color.black);
            AddColour("Player Friend Colour",Color.green);
            AddColour("Player Friend Box Colour", Color.green);
            AddColour("Player Friend Box Filled Colour", new Color32(30, 30, 30, 135));
            AddColour("Player Box Colour", Color.red);
            AddColour("Player Box Filled Invisible Colour", new Color32(255, 255, 255, 135));
            AddColour("Player Box Filled Visible Colour", new Color32(140, 30, 30, 135));
            AddColour("Player Box Filled Normal Colour", new Color32(30, 30, 30, 135));
            AddColour("Player Chams Primary Colour", new Color32(255, 0, 0, 255));
            AddColour("Player Chams Secondary Colour", new Color32(0, 255, 0, 255));
            AddColour("Player Friend Chams Primary Colour", new Color32(0, 255, 150, 255));
            AddColour("Player Friend Chams Secondary Colour", new Color32(0, 255, 0, 255));

            AddColour("Scav Text Colour", Color.blue);
            AddColour("Scav Text Visible Colour", Color.blue);
            AddColour("Scav Text Invisible Colour", Color.white);
            AddColour("Scav Skeleton Visible Colour", Color.red);
            AddColour("Scav Skeleton Invisible Colour", Color.white);
            AddColour("Scav Healthbar Colour", Color.green);
            AddColour("Scav Healthbar Background Colour", Color.black);
            AddColour("Scav Box Colour", Color.red);
            AddColour("Scav Box Filled Invisible Colour", new Color32(30, 30, 30, 135));
            AddColour("Scav Box Filled Visible Colour", new Color32(140, 40, 40, 135));
            AddColour("Scav Box Filled Normal Colour", new Color32(30, 30, 30, 135));
            AddColour("Scav Chams Primary Colour", new Color32(255, 0, 0, 255));
            AddColour("Scav Chams Secondary Colour", new Color32(0, 255, 0, 255));

            AddColour("ScavPlayer Text Colour", Color.cyan);
            AddColour("ScavPlayer Text Visible Colour", Color.cyan);
            AddColour("ScavPlayer Text Invisible Colour", Color.white);
            AddColour("ScavPlayer Skeleton Visible Colour", Color.red);
            AddColour("ScavPlayer Skeleton Invisible Colour", Color.white);
            AddColour("ScavPlayer Healthbar Colour", Color.green);
            AddColour("ScavPlayer Healthbar Background Colour", Color.black);
            AddColour("ScavPlayer Box Colour", Color.red);
            AddColour("ScavPlayer Box Filled Invisible Colour", new Color32(30,30,30, 135));
            AddColour("ScavPlayer Box Filled Visible Colour", new Color32(140, 40, 40, 135));
            AddColour("ScavPlayer Box Filled Normal Colour", new Color32(30, 30, 30, 135));
            AddColour("ScavPlayer Chams Primary Colour", new Color32(255, 0, 0, 255));
            AddColour("ScavPlayer Chams Secondary Colour", new Color32(0, 255, 0, 255));

            AddColour("Boss Text Colour", Color.magenta);
            AddColour("Boss Text Visible Colour", Color.magenta);
            AddColour("Boss Text Invisible Colour", Color.white);
            AddColour("Boss Skeleton Visible Colour", Color.red);
            AddColour("Boss Skeleton Invisible Colour", Color.white);
            AddColour("Boss Box Colour", Color.red);
            AddColour("Boss Healthbar Colour", Color.green);
            AddColour("Boss Healthbar Background Colour", Color.black);
            AddColour("Boss Box Filled Invisible Colour", new Color32(30, 30, 30, 135));
            AddColour("Boss Box Filled Visible Colour", new Color32(140, 40, 40, 135));
            AddColour("Boss Box Filled Normal Colour", new Color32(30, 30, 30, 135));
            AddColour("Boss Chams Primary Colour", new Color32(255, 0, 0, 255));
            AddColour("Boss Chams Secondary Colour", new Color32(0, 255, 0, 255));

            AddColour("Grenade Text Colour", Color.red);
            AddColour("Grenade Duration Bar Colour", Color.cyan);
            AddColour("Grenade Duration Bar Background Colour", Color.black);
            AddColour("Grenade Friendly Colour", Color.green);

            AddColour("Item Whitelisted Item Text Colour", Color.red);
            AddColour("Item Barter Item Text Colour", Color.white);
            AddColour("Item Quest Item Text Colour", Color.white);
            AddColour("Item Fuel Item Text Colour", Color.white);
            AddColour("Item Special Item Text Colour", Color.white);
            AddColour("Item Case Item Text Colour", Color.white);
            AddColour("Item Backpack Item Text Colour", Color.white);
            AddColour("Item Weapon Item Text Colour", Color.white);
            AddColour("Item Ammo Item Text Colour", Color.white);
            AddColour("Item Armour Item Text Colour", Color.white);
            AddColour("Item Clothing Item Text Colour", Color.white);
            AddColour("Item Key Item Text Colour", Color.white);
            AddColour("Item Keycard Item Text Colour", Color.magenta);
            AddColour("Item Medical Item Text Colour", Color.white);
            AddColour("Item RepairKit Item Text Colour", Color.white);
            AddColour("Item Attachment Item Text Colour", Color.white);
            AddColour("Item Food/Drink Item Text Colour", Color.white);

            AddColour("Exfil Text Colour", Color.green);

            AddColour("Container Text Colour", Color.yellow);

            AddColour("Corpse Text Colour", Color.magenta);

            AddColour("Crosshair Secondary Colour", new Color32(30, 30, 30, 180));
            AddColour("Crosshair Primary Colour", new Color32(255, 0, 0, 255));

            AddColour("Radar Primary Colour", new Color32(255, 255, 255, 255));
            AddColour("Radar Secondary Colour", new Color32(10, 10, 10, 180));
            AddColour("Radar Player Invisible Colour", new Color32(0, 150, 255, 255));
            AddColour("Radar Player Visible Colour", new Color32(255, 0, 0, 255));
            AddColour("Radar Boss Invisible Colour", new Color32(0, 150, 255, 255));
            AddColour("Radar Boss Visible Colour", new Color32(255, 0, 0, 255));
            AddColour("Radar Scav Player Invisible Colour", new Color32(0, 150, 255, 255));
            AddColour("Radar Scav Player Visible Colour", new Color32(255, 0, 0, 255));
            AddColour("Radar Scav Invisible Colour", new Color32(0, 150, 255, 255));
            AddColour("Radar Scav Visible Colour", new Color32(255, 0, 0, 255));

            AddColour("LocalPlayer Chams Primary Colour", new Color32(255, 0, 0, 255));
            AddColour("LocalPlayer Chams Secondary Colour", new Color32(0, 255, 0, 255));

            AddColour("Atmosphere FullBright Colour", new Color(1f, 0.839f, 0.66f, 1f));

            AddColour("Hud Ammo Primary Colour", new Color32(255, 255, 255, 255));
            AddColour("Hud Ammo Secondary Colour", new Color32(30, 30, 30, 150));
            AddColour("Hud Player List Primary Colour", new Color32(255, 255, 255, 255));
            AddColour("Hud Player List Secondary Colour", new Color32(30, 30, 30, 150));
        }
        public static Color32 GetColour(string id)
        {
            Color32 colour = Color.red;
            Globals.Config.Colours.ColourDict.TryGetValue(id, out colour);
            return colour;
        }

        public static void AddColour(string id, Color32 colour)
        {
            if (!Globals.Config.Colours.ColourDict.ContainsKey(id))
                Globals.Config.Colours.ColourDict.Add(id, colour);
        }

        public static void SetColour(string id, Color32 colour)
        { 
            Globals.Config.Colours.ColourDict[id] = colour; 
        }

      
    }
}
