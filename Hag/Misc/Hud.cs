using Hag.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Collections;
using Hag.Renderer;
using EFT.InventoryLogic;
using EFT.Interactive;
using System.Threading;
using EFT.UI;
using Hag.Helpers;
using System.Reflection;

namespace Hag.Misc
{
    class Hud : MonoBehaviour
    {
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            //   Application.lowMemory += OnLowMemory;
           StartCoroutine(SetCrosshair());
        //    StartCoroutine(DisableGC());
        }
        private static Vector3 BarrelPos;
        IEnumerator SetCrosshair()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null || Globals.LocalPlayer == null || Globals.LocalPlayerWeapon == null || !Globals.Config.Crosshair.Enable)
                {
                    BarrelPos = Vector3.zero;
                    goto END;
                }
                  

                BarrelPos = Esp.EspConstants.WorldToScreenPoint(RaycastHelper.BarrelRaycast());
                END:
                yield return new WaitForEndOfFrame();
            }
        }
        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
            DrawAmmoHud(renderer, font);
            DrawCrosshair(renderer);
        }
        private int AmmoWidth = 0;
        private int AmmoHeight = 0;
        public void DrawAmmoHud(Direct2DRenderer renderer,Direct2DFont font)
        {
   //         if (Globals.GameWorld == null || Globals.LocalPlayer == null)
          //      return;
            if (!Globals.Config.Hud.AmmoHud)
                return;
            Color32 primary = ColourHelper.GetColour("Hud Ammo Primary Colour");
            Direct2DColor primarycolour = new Direct2DColor(primary.r, primary.g, primary.b, primary.a);
            Color32 secondary = ColourHelper.GetColour("Hud Ammo Secondary Colour");
            Direct2DColor secondarycolour = new Direct2DColor(secondary.r, secondary.g, secondary.b, secondary.a);
            try
            {

                List<int> tempwidth = new List<int>();
                int tempheight = 0;

                renderer.FillRectangle(Globals.Config.Hud.Ammox - AmmoWidth / 2, Globals.Config.Hud.Ammoy, AmmoWidth +40, 20, secondarycolour);
                renderer.DrawRectangle(Globals.Config.Hud.Ammox - AmmoWidth /2, Globals.Config.Hud.Ammoy, AmmoWidth + 40, 20, 1, primarycolour);
                tempwidth.Add(renderer.DrawTextCentered("Ammo:", Globals.Config.Hud.Ammox +20 , Globals.Config.Hud.Ammoy, 14, font, primarycolour));
                renderer.FillRectangle(Globals.Config.Hud.Ammox - AmmoWidth /2, Globals.Config.Hud.Ammoy + 20, AmmoWidth+ 40, AmmoHeight, secondarycolour);
                renderer.DrawRectangle(Globals.Config.Hud.Ammox - AmmoWidth /2, Globals.Config.Hud.Ammoy + 20, AmmoWidth + 40, AmmoHeight, 1, primarycolour);
                  if (Globals.LocalPlayerWeapon != null)
                   {
                tempheight += 25;
                tempwidth.Add(renderer.DrawText("In Hands:", (Globals.Config.Hud.Ammox - (AmmoWidth/2) + 10) , Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                //  renderer.DrawLine(Globals.Config.Hud.Ammox, Globals.Config.Hud.Ammoy + 45, Globals.Config.Hud.Ammox + AmmoWidth + 40, Globals.Config.Hud.Ammoy + 45, 1, primarycolour);
                tempheight += 15;
                    string finalvalue = "";
                    string name = "";
                    try { name = Globals.LocalPlayerWeapon.ShortName.Localized(); finalvalue += name; } catch { }
                    int chamber = 0;
                    try { chamber = Globals.LocalPlayerWeapon.ChamberAmmoCount; } catch { }
                    int current = 0;
                    try { current = Globals.LocalPlayerWeapon.GetCurrentMagazineCount(); current += chamber; } catch { }
                    int max = 0;
                    try { max = Globals.LocalPlayerWeapon.GetMaxMagazineCount(); finalvalue += string.Concat(" [", current, "/", max, "]"); } catch { }
                    string ammo = "";
                    try { ammo = Globals.LocalPlayerWeapon.Chambers[0].ContainedItem.ShortName.Localized(); finalvalue += string.Concat(" | ", ammo); } catch { }
                    string firemode = "";
                    try { firemode = Globals.LocalPlayerWeapon.SelectedFireMode.ToString(); finalvalue += string.Concat(" | ", firemode); } catch { }

                    tempwidth.Add(renderer.DrawTextCentered(finalvalue, Globals.Config.Hud.Ammox+20, Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
               
                }
                if (Globals.Config.Hud.AmmoHudDisplayAllWeapons)
                {
                    if (Globals.LocalPlayer.Profile.Inventory.Equipment.GetSlot(EFT.InventoryLogic.EquipmentSlot.FirstPrimaryWeapon).ContainedItem != null)
                    {
                        Weapon weapon = Globals.LocalPlayer.Profile.Inventory.Equipment.GetSlot(EFT.InventoryLogic.EquipmentSlot.FirstPrimaryWeapon).ContainedItem as Weapon;
                        if (weapon != Globals.LocalPlayerWeapon)
                        {
                            tempheight += 20;
                            tempwidth.Add(renderer.DrawText("Primary:", (Globals.Config.Hud.Ammox - (AmmoWidth / 2) + 10), Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                            tempheight += 15;
                            string finalvalue = "";
                            string name = "";
                            try { name = weapon.ShortName.Localized(); finalvalue += name; } catch { }
                            int chamber = 0;
                            try { chamber = weapon.ChamberAmmoCount; } catch { }
                            int current = 0;
                            try { current = weapon.GetCurrentMagazineCount(); current += chamber; } catch { }
                            int max = 0;
                            try { max = weapon.GetMaxMagazineCount(); finalvalue += string.Concat(" [", current, "/", max, "]"); } catch { }
                            string ammo = "";
                            try { ammo = weapon.Chambers[0].ContainedItem.ShortName.Localized(); finalvalue += string.Concat(" | ", ammo); } catch { }
                            string firemode = "";
                            try { firemode = weapon.SelectedFireMode.ToString(); finalvalue += string.Concat(" | ", firemode); } catch { }

                            tempwidth.Add(renderer.DrawTextCentered(finalvalue, Globals.Config.Hud.Ammox + 20, Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                        }
                        }
                    if (Globals.LocalPlayer.Profile.Inventory.Equipment.GetSlot(EFT.InventoryLogic.EquipmentSlot.SecondPrimaryWeapon).ContainedItem != null)
                    {
                        Weapon weapon = Globals.LocalPlayer.Profile.Inventory.Equipment.GetSlot(EFT.InventoryLogic.EquipmentSlot.SecondPrimaryWeapon).ContainedItem as Weapon;
                        if (weapon != Globals.LocalPlayerWeapon)
                        {
                            tempheight += 20;
                            tempwidth.Add(renderer.DrawText("Secondary:", (Globals.Config.Hud.Ammox - (AmmoWidth / 2) + 10), Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                            tempheight += 15;
                            string finalvalue = "";
                            string name = "";
                            try { name = weapon.ShortName.Localized(); finalvalue += name; } catch { }
                            int chamber = 0;
                            try { chamber = weapon.ChamberAmmoCount; } catch { }
                            int current = 0;
                            try { current = weapon.GetCurrentMagazineCount(); current += chamber; } catch { }
                            int max = 0;
                            try { max = weapon.GetMaxMagazineCount(); finalvalue += string.Concat(" [", current, "/", max, "]"); } catch { }
                            string ammo = "";
                            try { ammo = weapon.Chambers[0].ContainedItem.ShortName.Localized(); finalvalue += string.Concat(" | ", ammo); } catch { }
                            string firemode = "";
                            try { firemode = weapon.SelectedFireMode.ToString(); finalvalue += string.Concat(" | ", firemode); } catch { }

                            tempwidth.Add(renderer.DrawTextCentered(finalvalue, Globals.Config.Hud.Ammox + 20, Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                        }
                    }
                    if (Globals.LocalPlayer.Profile.Inventory.Equipment.GetSlot(EFT.InventoryLogic.EquipmentSlot.Holster).ContainedItem != null)
                    {
                        Weapon weapon = Globals.LocalPlayer.Profile.Inventory.Equipment.GetSlot(EFT.InventoryLogic.EquipmentSlot.Holster).ContainedItem as Weapon;
                        if (weapon != Globals.LocalPlayerWeapon)
                        {
                            tempheight += 20;
                            tempwidth.Add(renderer.DrawText("Holster:", (Globals.Config.Hud.Ammox - (AmmoWidth / 2) + 10), Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                            tempheight += 15;
                            string finalvalue = "";
                            string name = "";
                            try { name = weapon.ShortName.Localized(); finalvalue += name; } catch { }
                            int chamber = 0;
                            try { chamber = weapon.ChamberAmmoCount; } catch { }
                            int current = 0;
                            try { current = weapon.GetCurrentMagazineCount(); current += chamber; } catch { }
                            int max = 0;
                            try { max = weapon.GetMaxMagazineCount(); finalvalue += string.Concat(" [", current, "/", max, "]"); } catch { }
                            string ammo = "";
                            try { ammo = weapon.Chambers[0].ContainedItem.ShortName.Localized(); finalvalue += string.Concat(" | ", ammo); } catch { }
                            string firemode = "";
                            try { firemode = weapon.SelectedFireMode.ToString(); finalvalue += string.Concat(" | ", firemode); } catch { }

                            tempwidth.Add(renderer.DrawTextCentered(finalvalue, Globals.Config.Hud.Ammox + 20, Globals.Config.Hud.Ammoy + tempheight, 12, font, primarycolour));
                        }
                    }
                }
                tempwidth.Sort(); // smallest to largest
                tempheight += 10; // padding
                AmmoWidth = tempwidth[tempwidth.Count - 1];
                AmmoHeight = tempheight;
                
                // remove garbage.
                tempwidth.Clear();
            }
            catch { }
        }
        public void DrawCrosshair(Direct2DRenderer renderer)
        {
            Vector2 pos = Globals.Config.Crosshair.DynamicCrosshair ? new Vector2(BarrelPos.x, BarrelPos.y) : new Vector2(Screen.width / 2f, Screen.height / 2f);
            if (Globals.GameWorld == null || Globals.LocalPlayer == null || !Globals.Config.Crosshair.Enable)
            {
                if (BarrelPos == null)
                    pos = new Vector2(Screen.width / 2f, Screen.height / 2f);
                return;
            }
                
            Color32 pcolour = ColourHelper.GetColour("Crosshair Primary Colour");
            Color32 scolour = ColourHelper.GetColour("Crosshair Secondary Colour");
            Direct2DColor primarycolour = new Direct2DColor(pcolour.r, pcolour.g, pcolour.g, pcolour.a);
            Direct2DColor secondarycolour = new Direct2DColor(scolour.r, scolour.g, scolour.g, scolour.a);
          
            if (BarrelPos == null)
                pos = new Vector2(Screen.width / 2f, Screen.height / 2f);

            switch (Globals.Config.Crosshair.CrosshairType)
            {

                case 0: // circle
                 
                 //   if(Globals.Config.Crosshair.Background)
                    renderer.BorderedCircle(pos.x, pos.y, Globals.Config.Crosshair.CrosshairSpacing, Globals.Config.Crosshair.CrosshairThickness, primarycolour, secondarycolour);
                   // else
                        renderer.DrawCircle(pos.x, pos.y, Globals.Config.Crosshair.CrosshairSpacing, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    break;
                case 1: // cross
                    renderer.DrawLine(pos.x - Globals.Config.Crosshair.CrosshairSize - Globals.Config.Crosshair.CrosshairSpacing, pos.y, pos.x - Globals.Config.Crosshair.CrosshairSpacing, pos.y, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x + Globals.Config.Crosshair.CrosshairSize + Globals.Config.Crosshair.CrosshairSpacing, pos.y, pos.x + Globals.Config.Crosshair.CrosshairSpacing, pos.y, Globals.Config.Crosshair.CrosshairThickness, primarycolour);

                    renderer.DrawLine(pos.x, pos.y - Globals.Config.Crosshair.CrosshairSize - Globals.Config.Crosshair.CrosshairSpacing, pos.x, pos.y - Globals.Config.Crosshair.CrosshairSpacing, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x, pos.y + Globals.Config.Crosshair.CrosshairSize + Globals.Config.Crosshair.CrosshairSpacing, pos.x, pos.y + Globals.Config.Crosshair.CrosshairSpacing, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    
                    break;

            
                case 2: // cross dot

                    renderer.FillRectangle((pos.x) - (Globals.Config.Crosshair.CrosshairThickness / 2f), (pos.y) - (Globals.Config.Crosshair.CrosshairThickness / 2f), Globals.Config.Crosshair.CrosshairThickness, Globals.Config.Crosshair.CrosshairThickness, secondarycolour);

                    renderer.DrawLine(pos.x - Globals.Config.Crosshair.CrosshairSize - Globals.Config.Crosshair.CrosshairSpacing, pos.y, pos.x - Globals.Config.Crosshair.CrosshairSpacing, pos.y, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x + Globals.Config.Crosshair.CrosshairSize + Globals.Config.Crosshair.CrosshairSpacing, pos.y, pos.x + Globals.Config.Crosshair.CrosshairSpacing, pos.y, Globals.Config.Crosshair.CrosshairThickness, primarycolour);

                    renderer.DrawLine(pos.x, pos.y - Globals.Config.Crosshair.CrosshairSize - Globals.Config.Crosshair.CrosshairSpacing, pos.x, pos.y - Globals.Config.Crosshair.CrosshairSpacing, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    renderer.DrawLine(pos.x, pos.y + Globals.Config.Crosshair.CrosshairSize + Globals.Config.Crosshair.CrosshairSpacing, pos.x, pos.y + Globals.Config.Crosshair.CrosshairSpacing, Globals.Config.Crosshair.CrosshairThickness, primarycolour);


                    break;

                case 3: // dot
                //    if (Globals.Config.Crosshair.Background)
                        renderer.FillRectangle((pos.x) - (Globals.Config.Crosshair.CrosshairThickness / 2f) - 1, (pos.y) - (Globals.Config.Crosshair.CrosshairThickness / 2f) - 1 , Globals.Config.Crosshair.CrosshairThickness + 2, Globals.Config.Crosshair.CrosshairThickness + 1,secondarycolour);
                    renderer.FillRectangle((pos.x) - (Globals.Config.Crosshair.CrosshairThickness/2f), (pos.y) - (Globals.Config.Crosshair.CrosshairThickness/2f), Globals.Config.Crosshair.CrosshairThickness, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    break;
                case 4: // filled circle
                   // if (Globals.Config.Crosshair.Background)
                        renderer.DrawCrosshair(CrosshairStyle.Dot, pos.x, pos.y, Globals.Config.Crosshair.CrosshairThickness +1, Globals.Config.Crosshair.CrosshairSpacing, secondarycolour);
                    renderer.DrawCrosshair(CrosshairStyle.Dot, pos.x, pos.y, Globals.Config.Crosshair.CrosshairThickness, Globals.Config.Crosshair.CrosshairSpacing, primarycolour);
                    break;
                case 5: // swastika
                    renderer.RotateSwastika(pos.x, pos.y, Globals.Config.Crosshair.CrosshairSize, Globals.Config.Crosshair.CrosshairThickness, primarycolour);
                    break;
            }
        }
    }
}
