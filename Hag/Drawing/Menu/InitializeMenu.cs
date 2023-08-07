using Hag.Helpers;
using Hag.Menu;
using Hag.Menu.EnumSliders;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hag.Drawing.Menu
{
    class InitializeMenu
    {
        /*
         Note to self, make sure everything gets asigned a value within a function so then we can recall the init and everything will reset to default.
        We dont want to reload a config and end up with a menu getting m
         
         */
        public static SubMenu CurrentMenu;
        public static List<SubMenu> MenuHistory = new List<SubMenu>();
        public static Entity Selected;

        string ConfigString = "Config";
        string WhitelistString = "";
        string BlacklistString = "";
        public InitializeMenu()
        {


            Init();
            System.Random rand = new System.Random();
            RenderMenu.Watermark = RenderMenu.Watermarks[rand.Next(0, RenderMenu.Watermarks.Count() - 1)];
        }
        void Init()
        {

            Main();
        }

        void Main()
        {


            SubMenu MainMenu = new SubMenu("Main", "");
            SubMenu AimbotMenu = new SubMenu("Aimbot", "Configure Aimbot");
            {

                SubMenu CrosshairMenu = new SubMenu("Prediction Crosshair", "Customize Prediction Crosshair");
                {
                    Toggle enable = new Toggle("Enable", "Draws Aimbot Prediction Point", ref Globals.Config.Aimbot.PredictionCrosshair.Enable);
                    CrosshairTypeSlider type = new CrosshairTypeSlider("Type", "Change Prediction Crosshair Type", ref Globals.Config.Aimbot.PredictionCrosshair.CrosshairType);
                    IntSlider spacing = new IntSlider("Crosshair Spacing", "Distance Between Parts Of The Crosshair", ref Globals.Config.Aimbot.PredictionCrosshair.CrosshairSpacing, 0, 100, 1);
                    IntSlider size = new IntSlider("Crosshair Size", "Size Of The Crosshair", ref Globals.Config.Aimbot.PredictionCrosshair.CrosshairSize, 0, 100, 1);
                    IntSlider thickness = new IntSlider("Crosshair Thickness", "Thickness Of Crosshair", ref Globals.Config.Aimbot.PredictionCrosshair.CrosshairThickness, 0, 100, 1);
                    FloatSlider animation = new FloatSlider("Crosshair Animation Smoothing", "Animation Speed Of Animated Crosshairs", ref Globals.Config.Aimbot.PredictionCrosshair.CrosshairAnimation, 0f, 40f, 0.25f);
                    CrosshairMenu.Items.Add(enable);
                    CrosshairMenu.Items.Add(type);
                    CrosshairMenu.Items.Add(spacing);
                    CrosshairMenu.Items.Add(size);
                    CrosshairMenu.Items.Add(thickness);
                    CrosshairMenu.Items.Add(animation);
                }

                AimbotMenu.Items.Add(CrosshairMenu);
                SubMenu AssaultRifle = new SubMenu("Assault Rifle", "Assault Rifle Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultAssaultRifle.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultAssaultRifle.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultAssaultRifle.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultAssaultRifle.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultAssaultRifle.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultAssaultRifle.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultAssaultRifle.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultRifle.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    AssaultRifle.Items.Add(Default);
                    foreach (var item in Globals.AssaultRifles)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);

                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        AssaultRifle.Items.Add(menu);

                    }
                }
                SubMenu AssaultCarbine = new SubMenu("Assault Carbine", "Assault Carbine Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultAssaultCarbine.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultAssaultCarbine.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultAssaultCarbine.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultAssaultCarbine.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultAssaultCarbine.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultAssaultCarbine.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultAssaultCarbine.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    AssaultCarbine.Items.Add(Default);
                    foreach (var item in Globals.AssaultCarbines)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);

                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        AssaultCarbine.Items.Add(menu);

                    }
                }
                SubMenu MachineGun = new SubMenu("Machine Gun", "Machine Gun Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultMachineGun.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultMachineGun.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultMachineGun.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultMachineGun.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultMachineGun.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultMachineGun.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultMachineGun.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);

                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMachineGun.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    MachineGun.Items.Add(Default);
                    foreach (var item in Globals.MachineGuns)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        MachineGun.Items.Add(menu);

                    }
                }
                SubMenu MarksmanRifle = new SubMenu("Marksman Rifle", "Marksman Rifle Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultMarksmanRifle.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultMarksmanRifle.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultMarksmanRifle.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultMarksmanRifle.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultMarksmanRifle.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultMarksmanRifle.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultMarksmanRifle.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    MarksmanRifle.Items.Add(Default);
                    foreach (var item in Globals.MarksmanRifles)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        MarksmanRifle.Items.Add(menu);

                    }
                }
                SubMenu Pistol = new SubMenu("Pistol", "Pistol Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultPistol.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultPistol.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultPistol.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultPistol.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultPistol.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultPistol.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultPistol.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultPistol.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    Pistol.Items.Add(Default);
                    foreach (var item in Globals.Pistols)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        Pistol.Items.Add(menu);

                    }
                }
                SubMenu Revolver = new SubMenu("Revolver", "Revolver Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultRevolver.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultRevolver.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultRevolver.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultRevolver.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultRevolver.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultRevolver.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultRevolver.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultRevolver.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    Revolver.Items.Add(Default);
                    foreach (var item in Globals.Revolvers)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        Revolver.Items.Add(menu);

                    }
                }
                SubMenu Shotgun = new SubMenu("Shotgun", "Shotgun Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultShotgun.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultShotgun.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultShotgun.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultShotgun.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultShotgun.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultShotgun.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultShotgun.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultShotgun.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    Shotgun.Items.Add(Default);
                    foreach (var item in Globals.Shotguns)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        Shotgun.Items.Add(menu);

                    }
                }
                SubMenu SMG = new SubMenu("SMG", "SMG Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultSMG.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultSMG.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultSMG.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultSMG.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultSMG.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultSMG.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultSMG.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSMG.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    SMG.Items.Add(Default);
                    foreach (var item in Globals.SMGs)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        SMG.Items.Add(menu);

                    }
                }
                SubMenu SniperRifle = new SubMenu("Sniper Rifle", "Sniper Rifle Weapon Configs");
                {
                    SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                    {
                        SubMenu general = new SubMenu("General", "General Aimbot Settings");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.DefaultSniperRifle.Enable);
                            Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.DefaultSniperRifle.DrawFov);
                            FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.DefaultSniperRifle.FovType);
                            IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.DefaultSniperRifle.Fov, 0, 2000, 20);
                            Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultSniperRifle.TargetLine);
                            Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.DefaultSniperRifle.AutoShoot);
                            Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.DefaultSniperRifle.Key);
                            general.Items.Add(enable);
                            general.Items.Add(drawfov);
                            general.Items.Add(fovtype);
                            general.Items.Add(fovamount);
                            general.Items.Add(targetline);
                            general.Items.Add(autoshoot);
                            general.Items.Add(key);
                        }
                        Default.Items.Add(general);
                        SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                        {
                            Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetting.StickToTarget);
                            IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                            targetting.Items.Add(sticktotarget);
                            targetting.Items.Add(targetswitchingtime);
                        }
                        Default.Items.Add(targetting);
                        SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetPlayer.Bone);
                            PlayerAimbot.Items.Add(enable);
                            PlayerAimbot.Items.Add(prediction);
                            PlayerAimbot.Items.Add(ignorefov);
                            PlayerAimbot.Items.Add(maxdistance);
                            PlayerAimbot.Items.Add(mindistance);
                            PlayerAimbot.Items.Add(hitchance);
                            PlayerAimbot.Items.Add(aimcone);
                            //PlayerAimbot.Items.Add(manipulation);
                            //PlayerAimbot.Items.Add(manipulationamount);
                            PlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(PlayerAimbot);
                        SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScav.Bone);
                            ScavAimbot.Items.Add(enable);
                            ScavAimbot.Items.Add(prediction);
                            ScavAimbot.Items.Add(ignorefov);
                            ScavAimbot.Items.Add(maxdistance);
                            ScavAimbot.Items.Add(mindistance);
                            ScavAimbot.Items.Add(hitchance);
                            ScavAimbot.Items.Add(aimcone);
                            //ScavAimbot.Items.Add(manipulation);
                            //ScavAimbot.Items.Add(manipulationamount);
                            ScavAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavAimbot);
                        SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetScavPlayer.Bone);
                            ScavPlayerAimbot.Items.Add(enable);
                            ScavPlayerAimbot.Items.Add(prediction);
                            ScavPlayerAimbot.Items.Add(ignorefov);
                            ScavPlayerAimbot.Items.Add(maxdistance);
                            ScavPlayerAimbot.Items.Add(mindistance);
                            ScavPlayerAimbot.Items.Add(hitchance);
                            ScavPlayerAimbot.Items.Add(aimcone);
                            //Scav//PlayerAimbot.Items.Add(manipulation);
                            //ScavPlayerAimbot.Items.Add(manipulationamount);
                            ScavPlayerAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(ScavPlayerAimbot);
                        SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                        {
                            Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.Enable);
                            Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.Prediction);
                            Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.IgnoreFov);
                            IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                            IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.MinDistance, 0, 2000, 25);
                            IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.Hitchance, 0, 100, 2);
                            FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                            Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.Manipulation);
                            FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                            BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.DefaultSniperRifle.AimbotTargetBoss.Bone);
                            BossAimbot.Items.Add(enable);
                            BossAimbot.Items.Add(prediction);
                            BossAimbot.Items.Add(ignorefov);
                            BossAimbot.Items.Add(maxdistance);
                            BossAimbot.Items.Add(mindistance);
                            BossAimbot.Items.Add(hitchance);
                            BossAimbot.Items.Add(aimcone);
                            //BossAimbot.Items.Add(manipulation);
                            //BossAimbot.Items.Add(manipulationamount);
                            BossAimbot.Items.Add(bone);
                        }
                        Default.Items.Add(BossAimbot);
                    }
                    SniperRifle.Items.Add(Default);
                    foreach (var item in Globals.SniperRifles)
                    {
                        string name = Helpers.ItemPriceHelper.list[item].name;
                        SubMenu menu = new SubMenu(name, string.Concat("Aimbot Weapon Config For The ", name, " Weapon"));
                        {
                            SubMenu general = new SubMenu("General", "General Aimbot Settings");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Enable);
                                Toggle drawfov = new Toggle("Draw Fov", "Draws Circle To Represent Aimbot FOV", ref Globals.Config.Aimbot.WeaponDict[item].DrawFov);
                                FovTypeSlider fovtype = new FovTypeSlider("Fov Type", "Changes How The Scope Interacts With Aimbot Fov", ref Globals.Config.Aimbot.WeaponDict[item].FovType);
                                IntSlider fovamount = new IntSlider("FOV", "Adjust Aimbot Fov Size", ref Globals.Config.Aimbot.WeaponDict[item].Fov, 0, 2000, 20);
                                Toggle targetline = new Toggle("Target Line", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].TargetLine);
                                Toggle autoshoot = new Toggle("Auto Shoot", "Draws Line To Target Player", ref Globals.Config.Aimbot.WeaponDict[item].AutoShoot);
                                Keybind key = new Keybind("Keybind", "Key To Use Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].Key);
                                general.Items.Add(enable);
                                general.Items.Add(drawfov);
                                general.Items.Add(fovtype);
                                general.Items.Add(fovamount);
                                general.Items.Add(targetline);
                                general.Items.Add(autoshoot);
                                general.Items.Add(key);
                            }
                            menu.Items.Add(general);
                            SubMenu targetting = new SubMenu("Targetting", "Targetting Aimbot Settings");
                            {
                                Toggle sticktotarget = new Toggle("Stick To Taget", "Prioritise Your Last Target", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.StickToTarget);
                                IntSlider targetswitchingtime = new IntSlider("Target Switch Time", "Time Between Searching For New Target In MS", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetting.TargetSwitchingTime, 0, 1000, 10);
                                targetting.Items.Add(sticktotarget);
                                targetting.Items.Add(targetswitchingtime);
                            }
                            menu.Items.Add(targetting);
                            SubMenu PlayerAimbot = new SubMenu("Player Aimbot", "Configure Aimbot For Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetPlayer.Bone);
                                PlayerAimbot.Items.Add(enable);
                                PlayerAimbot.Items.Add(prediction);
                                PlayerAimbot.Items.Add(ignorefov);
                                PlayerAimbot.Items.Add(maxdistance);
                                PlayerAimbot.Items.Add(mindistance);
                                PlayerAimbot.Items.Add(hitchance);
                                PlayerAimbot.Items.Add(aimcone);
                                //PlayerAimbot.Items.Add(manipulation);
                                //PlayerAimbot.Items.Add(manipulationamount);
                                PlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(PlayerAimbot);
                            SubMenu ScavAimbot = new SubMenu("Scav Aimbot", "Configure Aimbot For Scavs");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScav.Bone);
                                ScavAimbot.Items.Add(enable);
                                ScavAimbot.Items.Add(prediction);
                                ScavAimbot.Items.Add(ignorefov);
                                ScavAimbot.Items.Add(maxdistance);
                                ScavAimbot.Items.Add(mindistance);
                                ScavAimbot.Items.Add(hitchance);
                                ScavAimbot.Items.Add(aimcone);
                                //ScavAimbot.Items.Add(manipulation);
                                //ScavAimbot.Items.Add(manipulationamount);
                                ScavAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavAimbot);
                            SubMenu ScavPlayerAimbot = new SubMenu("Scav Player Aimbot", "Configure Aimbot For Scav Players");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetScavPlayer.Bone);
                                ScavPlayerAimbot.Items.Add(enable);
                                ScavPlayerAimbot.Items.Add(prediction);
                                ScavPlayerAimbot.Items.Add(ignorefov);
                                ScavPlayerAimbot.Items.Add(maxdistance);
                                ScavPlayerAimbot.Items.Add(mindistance);
                                ScavPlayerAimbot.Items.Add(hitchance);
                                ScavPlayerAimbot.Items.Add(aimcone);
                                //Scav//PlayerAimbot.Items.Add(manipulation);
                                //ScavPlayerAimbot.Items.Add(manipulationamount);
                                ScavPlayerAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(ScavPlayerAimbot);
                            SubMenu BossAimbot = new SubMenu("Boss Aimbot", "Configure Aimbot For Bosses");
                            {
                                Toggle enable = new Toggle("Enable", "Enables Scav Player Aimbot", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Enable);
                                Toggle prediction = new Toggle("Prediction", "Predicts BulletDrop And Movement", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Prediction);
                                Toggle ignorefov = new Toggle("Ignore Fov", "Ignore Fov Requirements", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.IgnoreFov);
                                IntSlider maxdistance = new IntSlider("Max Distance", "Maximum Distance That Aimbot Works At", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MaxDistance, 0, 2000, 25);
                                IntSlider mindistance = new IntSlider("Minimum Distance", "Minimum Distance That Aimbot Works Beyond", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.MinDistance, 0, 2000, 25);
                                IntSlider hitchance = new IntSlider("Hitchance", "Chance Of The Aimbot Working", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Hitchance, 0, 100, 2);
                                FloatSlider aimcone = new FloatSlider("Aimcone", "Amount Of Artificial Aimcone Created By Aimbot To Create Inaccuracy", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.AimConeAmount, 0, 2.5f, 0.1f);
                                Toggle manipulation = new Toggle("Manipulation", "Shoot Around Walls", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Manipulation);
                                FloatSlider manipulationamount = new FloatSlider("Manipulation Amount", "Distance Manipulation Will Leverage", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.ManipAmount, 0, 5f, 0.25f);
                                BoneTypeSlider bone = new BoneTypeSlider("Target Bone", "Bone The Aimbot Will Target(Hitscan Isn't Impelemented Yet)", ref Globals.Config.Aimbot.WeaponDict[item].AimbotTargetBoss.Bone);
                                BossAimbot.Items.Add(enable);
                                BossAimbot.Items.Add(prediction);
                                BossAimbot.Items.Add(ignorefov);
                                BossAimbot.Items.Add(maxdistance);
                                BossAimbot.Items.Add(mindistance);
                                BossAimbot.Items.Add(hitchance);
                                BossAimbot.Items.Add(aimcone);
                                //BossAimbot.Items.Add(manipulation);
                                //BossAimbot.Items.Add(manipulationamount);
                                BossAimbot.Items.Add(bone);
                            }
                            menu.Items.Add(BossAimbot);
                        }
                        SniperRifle.Items.Add(menu);

                    }
                }
                AimbotMenu.Items.Add(AssaultRifle);
                AimbotMenu.Items.Add(AssaultCarbine);
                AimbotMenu.Items.Add(MachineGun);
                AimbotMenu.Items.Add(MarksmanRifle);
                AimbotMenu.Items.Add(Pistol);
                AimbotMenu.Items.Add(Revolver);
                AimbotMenu.Items.Add(Shotgun);
                AimbotMenu.Items.Add(SMG);
                AimbotMenu.Items.Add(SniperRifle);
            }
            SubMenu EspMenu = new SubMenu("ESP", "Configure Visuals");
            {
                SubMenu ExfilEspMenu = new SubMenu("Exfil Esp", "Extraction/Exfiltration Esp");
                {
                    Toggle ExfilMenuEnable = new Toggle("Enable Exfil Esp", "Allows Exfil Esp To Be Drawn", ref Globals.Config.Exfil.Enable);
                    IntSlider ExfilMenuMaxDistance = new IntSlider("Exfil Esp Max Distance", "The Distance Exfil Esp Ceases To Draw", ref Globals.Config.Exfil.MaxDistance, 0, 2000, 25);
                    Toggle Battlemode = new Toggle("Enable In BattleMode", "", ref Globals.Config.Exfil.EnableEspInBattleMode);
                    ExfilEspMenu.Items.Add(ExfilMenuEnable);
                    
                    ExfilEspMenu.Items.Add(Battlemode);
                    ExfilEspMenu.Items.Add(ExfilMenuMaxDistance);
                    SubMenu ExfilTextMenu = new SubMenu("Exfil Name", "Configure Exfil Name Esp");
                    {
                        Toggle ExfilTextMenuEnable = new Toggle("Enable Name Esp", "Draws Exfil Point Name", ref Globals.Config.Exfil.Name.Enable);
                        TextAlignmentSlider ExfilTextMenuAlignment = new TextAlignmentSlider("Exfil Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Exfil.Name.Alignment);
                        IntSlider ExfilTextMenuLine = new IntSlider("Name Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Exfil.Name.Line, 1, 3, 1);
                        ExfilTextMenu.Items.Add(ExfilTextMenuEnable);
                        ExfilTextMenu.Items.Add(ExfilTextMenuAlignment);
                        ExfilTextMenu.Items.Add(ExfilTextMenuLine);
                    }
                    ExfilEspMenu.Items.Add(ExfilTextMenu);
                    SubMenu ExfilDistanceMenu = new SubMenu("Exfil Distance", "Configure Exfil Name Esp");
                    {
                        Toggle ExfilDistanceMenuEnable = new Toggle("Enable Distance Esp", "Draws Exfil Point Distance", ref Globals.Config.Exfil.Distance.Enable);
                        TextAlignmentSlider ExfilDistanceMenuAlignment = new TextAlignmentSlider("Exfil Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Exfil.Distance.Alignment);
                        IntSlider ExfilDistanceMenuLine = new IntSlider("Distance Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Exfil.Distance.Line, 1, 3, 1);
                        ExfilDistanceMenu.Items.Add(ExfilDistanceMenuEnable);
                        ExfilDistanceMenu.Items.Add(ExfilDistanceMenuAlignment);
                        ExfilDistanceMenu.Items.Add(ExfilDistanceMenuLine);
                    }
                    ExfilEspMenu.Items.Add(ExfilDistanceMenu);
                    SubMenu ExfilOpacityCullingMenu = new SubMenu("Opacity Culling Options", "Changes Text Opacity Depending On Selected Factor");
                    {
                        Toggle ExfilOpacityCullingByDistance = new Toggle("Cull Opacity By Distance", "Reduces Esp Opacity Depending On Distance From Target", ref Globals.Config.Exfil.LimitOpacityByDistance);
                        ExfilOpacityCullingMenu.Items.Add(ExfilOpacityCullingByDistance);
                    }
                    ExfilEspMenu.Items.Add(ExfilOpacityCullingMenu);


                }
                EspMenu.Items.Add(ExfilEspMenu);
                SubMenu PlayerEspMenu = new SubMenu("Player Esp", "Visuals For Players");
                {
                    Toggle PlayerEnable = new Toggle("Enable Player Esp", "Allows Player Esp To Draw", ref Globals.Config.Player.Enable);
                    IntSlider PlayerMaxDistance = new IntSlider("Player Esp Max Distance", "The Distance Player Esp Ceases To Draw", ref Globals.Config.Player.MaxDistance, 0, 2000, 25);
                    Toggle Battlemode = new Toggle("Enable In BattleMode", "", ref Globals.Config.Player.EnableEspInBattleMode);
                    Toggle PlayerVisCheck = new Toggle("Player Esp Text VisChecks", "Change Text Colour Based On Visibility", ref Globals.Config.Player.TextVisibilityCheck);
                    PlayerEspMenu.Items.Add(PlayerEnable);
                    PlayerEspMenu.Items.Add(PlayerMaxDistance);
                    PlayerEspMenu.Items.Add(PlayerVisCheck);
                    PlayerEspMenu.Items.Add(Battlemode);
                    SubMenu ChamsMenu = new SubMenu("Chams", "Configure Player Chams");
                    {
                        Toggle Enable = new Toggle("Enable", "Draws Player Chams", ref Globals.Config.Player.Chams);
                        Toggle ApplyToGear = new Toggle("Apply To Gear", "Applys Chams To Gear", ref Globals.Config.Player.ApplyChamsToGear);
                        ChamTypeSlider ChamType = new ChamTypeSlider("Cham Type", "Different Cham Varient", ref Globals.Config.Player.ChamsType);
                        ChamsMenu.Items.Add(Enable);
                        ChamsMenu.Items.Add(ApplyToGear);
                        ChamsMenu.Items.Add(ChamType);
                    }
                    PlayerEspMenu.Items.Add(ChamsMenu);
                    SubMenu PlayerTextMenu = new SubMenu("Player Name", "Configure Player Name Esp");
                    {
                        Toggle PlayerTextMenuEnable = new Toggle("Enable Name Esp", "Draws Player Tag", ref Globals.Config.Player.Name.Enable);
                        TextAlignmentSlider PlayerTextMenuAlignment = new TextAlignmentSlider("Player Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.Name.Alignment);
                        IntSlider PlayerTextMenuLine = new IntSlider("Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Player.Name.Line, 1, 3, 1);
                        PlayerTextMenu.Items.Add(PlayerTextMenuEnable);
                        PlayerTextMenu.Items.Add(PlayerTextMenuAlignment);
                        PlayerTextMenu.Items.Add(PlayerTextMenuLine);
                    }
                    PlayerEspMenu.Items.Add(PlayerTextMenu);
                    SubMenu PlayerDistanceMenu = new SubMenu("Player Distance Esp", "Configure Player Distance Esp");
                    {
                        Toggle PlayerDistanceMenuEnable = new Toggle("Enable Distance Esp", "Draws Player Distance", ref Globals.Config.Player.Distance.Enable);
                        TextAlignmentSlider PlayerDistanceMenuAlignment = new TextAlignmentSlider("Player Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.Distance.Alignment);
                        IntSlider PlayerDistanceMenuLine = new IntSlider("Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Player.Distance.Line, 1, 3, 1);
                        PlayerDistanceMenu.Items.Add(PlayerDistanceMenuEnable);
                        PlayerDistanceMenu.Items.Add(PlayerDistanceMenuAlignment);
                        PlayerDistanceMenu.Items.Add(PlayerDistanceMenuLine);
                    }
                    PlayerEspMenu.Items.Add(PlayerDistanceMenu);
                    SubMenu PlayerValueMenu = new SubMenu("Player Value Esp", "Configure Scav Player Value Esp");
                    {
                        Toggle PlayerValueMenuEnable = new Toggle("Enable Value Esp", "Draws Player Value", ref Globals.Config.Player.Value.Enable);
                        TextAlignmentSlider PlayerValueMenuAlignment = new TextAlignmentSlider("Player Esp Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.Value.Alignment);
                        IntSlider PlayerValueMenuLine = new IntSlider("Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Player.Value.Line, 1, 3, 1);
                        Toggle PlayerValueSkipAttachments = new Toggle("Skip Attachments", "Doesn't Add Attachments To Value", ref Globals.Config.Player.SkipAttachmentsInValue);
                        PlayerValueMenu.Items.Add(PlayerValueMenuEnable);
                        PlayerValueMenu.Items.Add(PlayerValueMenuAlignment);
                        PlayerValueMenu.Items.Add(PlayerValueMenuLine);
                        PlayerValueMenu.Items.Add(PlayerValueSkipAttachments);
                    }
                    PlayerEspMenu.Items.Add(PlayerValueMenu);
                    SubMenu PlayerSkeletonMenu = new SubMenu("Skeleton Esp", "Configure Player Skeleton");
                    {
                        Toggle PlayerSkeletonEnable = new Toggle("Enable Skeleton Esp", "Draws Player Skeleton", ref Globals.Config.Player.Skeleton.Enable);
                        SkeletonTypeSlider PlayerSkeletonType = new SkeletonTypeSlider("Skeleton Type", "Choose If The Skeleton Uses Visibility Or Health", ref Globals.Config.Player.Skeleton.Type);
                        IntSlider PlayerSkeletonMaxDistance = new IntSlider("Skeleton Max Distance", "Max Distance That Skeletons Draw At", ref Globals.Config.Player.Skeleton.MaxDistance, 0, 850, 25);
                        PlayerSkeletonMenu.Items.Add(PlayerSkeletonEnable);
                        PlayerSkeletonMenu.Items.Add(PlayerSkeletonType);
                        PlayerSkeletonMenu.Items.Add(PlayerSkeletonMaxDistance);
                    }
                    PlayerEspMenu.Items.Add(PlayerSkeletonMenu);
                    SubMenu PlayerWeaponMenu = new SubMenu("Player Weapon Esp", "Configure Player Weapon Esp");
                    {
                        Toggle PlayerWeaponMenuEnable = new Toggle("Enable Weapon Esp", "Draws Player Weapon", ref Globals.Config.Player.Weapon.Enable);
                        TextAlignmentSlider PlayerWeaponeMenuAlignment = new TextAlignmentSlider("Player Weapon Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.Weapon.Alignment);
                        IntSlider PlayerWeaponMenuLine = new IntSlider("Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Player.Weapon.Line, 1, 3, 1);
                        PlayerWeaponMenu.Items.Add(PlayerWeaponMenuEnable);
                        PlayerWeaponMenu.Items.Add(PlayerWeaponeMenuAlignment);
                        PlayerWeaponMenu.Items.Add(PlayerWeaponMenuLine);
                    }
                    PlayerEspMenu.Items.Add(PlayerWeaponMenu);

                    SubMenu PlayerAmmoMenu = new SubMenu("Player Ammo Esp", "Configure Player Weapon Esp");
                    {
                        Toggle PlayerAmmoMenuEnable = new Toggle("Enable Ammo Esp", "Draws Player Ammo", ref Globals.Config.Player.Ammo.Enable);
                        TextAlignmentSlider PlayerAmmoMenuAlignment = new TextAlignmentSlider("Player Ammo Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.Ammo.Alignment);
                        IntSlider PlayerAmmoMenuLine = new IntSlider("Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Player.Ammo.Line, 1, 3, 1);
                        PlayerAmmoMenu.Items.Add(PlayerAmmoMenuEnable);
                        PlayerAmmoMenu.Items.Add(PlayerAmmoMenuAlignment);
                        PlayerAmmoMenu.Items.Add(PlayerAmmoMenuLine);
                    }
                    PlayerEspMenu.Items.Add(PlayerAmmoMenu);
                    SubMenu PlayerBoxMenu = new SubMenu("Player Box Esp", "Configure Player Weapon Esp");
                    {
                        Toggle PlayerBoxMenuEnable = new Toggle("Enable Box Esp", "Draws Player Boxes", ref Globals.Config.Player.Box.Enable);
                        IntSlider PlayerBoxMaxDistance = new IntSlider("Box Max Distance", "Max Distance That Boxes Draw At", ref Globals.Config.Player.Box.MaxDistance, 0, 150, 25);
                        Toggle PlayerFillBoxMenuEnable = new Toggle("Enable Box Filled Esp", "Draws Filled Boxes", ref Globals.Config.Player.Box.Fill);
                        Toggle PlayerFillBoxVisibilityMenuEnable = new Toggle("Enable Filled Box Visibility Esp", "Draws Different Colours For Visible Players", ref Globals.Config.Player.Box.VisibilityCheck);
                        PlayerBoxMenu.Items.Add(PlayerBoxMenuEnable);
                        PlayerBoxMenu.Items.Add(PlayerBoxMaxDistance);
                        PlayerBoxMenu.Items.Add(PlayerFillBoxMenuEnable);
                        PlayerBoxMenu.Items.Add(PlayerFillBoxVisibilityMenuEnable);
                    }
                    PlayerEspMenu.Items.Add(PlayerBoxMenu);
                    SubMenu PlayerHealthbarMenu = new SubMenu("Player Healthbar Esp", "Configure Player Weapon Esp");
                    {
                        Toggle PlayerHealthbarMenuEnable = new Toggle("Enable Healthbar Esp", "Draws Player Healthbar", ref Globals.Config.Player.Healthbar.Enable);
                        TextAlignmentSlider PlayerHealthbarMenuAlignment = new TextAlignmentSlider("Player Healthbar Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.Healthbar.Alignment);
                        IntSlider PlayerHealthbarMaxDistance = new IntSlider("Healthbar Max Distance", "Max Distance That Healthbar Draw At", ref Globals.Config.Player.Healthbar.MaxDistance, 0, 850, 25);
                        PlayerHealthbarMenu.Items.Add(PlayerHealthbarMenuEnable);
                        PlayerHealthbarMenu.Items.Add(PlayerHealthbarMenuAlignment);
                        PlayerHealthbarMenu.Items.Add(PlayerHealthbarMaxDistance);
                    }
                    PlayerEspMenu.Items.Add(PlayerHealthbarMenu);
                    SubMenu PlayerItemsMenu = new SubMenu("Player Contents Esp", "Configure Player Contents Esp");
                    {
                        Toggle PlayerContentsEnable = new Toggle("Enable Contents Esp", "Draws Player Distance", ref Globals.Config.Player.ContentsList);
                        TextAlignmentSlider PlayerContentsAlignment = new TextAlignmentSlider("Player Contents Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Player.ContentListAlignment);
                        Keybind PlayerContentsLine = new Keybind("Player Contents Key", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Player.ContentsListKey);
                        PlayerItemsMenu.Items.Add(PlayerContentsEnable);
                        PlayerItemsMenu.Items.Add(PlayerContentsAlignment);
                        PlayerItemsMenu.Items.Add(PlayerContentsLine);

                        SubMenu WhitelistContainerMenu = new SubMenu("Whitelist Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Whitelist Esp", "Draw Whitelist On Contents List", ref Globals.Config.Player.WhitelistContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.WhitelistContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.WhitelistContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.WhitelistContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.WhitelistContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.WhitelistContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.WhitelistContainer.UseSlotPrice);
                            WhitelistContainerMenu.Items.Add(Enable);
                            WhitelistContainerMenu.Items.Add(Name);
                            WhitelistContainerMenu.Items.Add(Value);
                            WhitelistContainerMenu.Items.Add(Type);
                            WhitelistContainerMenu.Items.Add(SubType);
                            WhitelistContainerMenu.Items.Add(MinVal);
                            WhitelistContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(WhitelistContainerMenu);

                        SubMenu AmmoContainerMenu = new SubMenu("Ammo Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Ammo Esp", "Draw Ammo On Contents List", ref Globals.Config.Player.AmmoContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.AmmoContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.AmmoContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.AmmoContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.AmmoContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.AmmoContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.AmmoContainer.UseSlotPrice);
                            AmmoContainerMenu.Items.Add(Enable);
                            AmmoContainerMenu.Items.Add(Name);
                            AmmoContainerMenu.Items.Add(Value);
                            AmmoContainerMenu.Items.Add(Type);
                            AmmoContainerMenu.Items.Add(SubType);
                            AmmoContainerMenu.Items.Add(MinVal);
                            AmmoContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(AmmoContainerMenu);
                        SubMenu AttachmentsContainerMenu = new SubMenu("Attachments Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Attachments Esp", "Draw Attachments On Contents List", ref Globals.Config.Player.AttachmentsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.AttachmentsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.AttachmentsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.AttachmentsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.AttachmentsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.AttachmentsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.AttachmentsContainer.UseSlotPrice);
                            AttachmentsContainerMenu.Items.Add(Enable);
                            AttachmentsContainerMenu.Items.Add(Name);
                            AttachmentsContainerMenu.Items.Add(Value);
                            AttachmentsContainerMenu.Items.Add(Type);
                            AttachmentsContainerMenu.Items.Add(SubType);
                            AttachmentsContainerMenu.Items.Add(MinVal);
                            AttachmentsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(AttachmentsContainerMenu);
                        SubMenu BackpacksContainerMenu = new SubMenu("Backpacks Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Backpacks Esp", "Draw Backpacks On Contents List", ref Globals.Config.Player.BackpacksContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.BackpacksContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.BackpacksContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.BackpacksContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.BackpacksContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.BackpacksContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.BackpacksContainer.UseSlotPrice);
                            BackpacksContainerMenu.Items.Add(Enable);
                            BackpacksContainerMenu.Items.Add(Name);
                            BackpacksContainerMenu.Items.Add(Value);
                            BackpacksContainerMenu.Items.Add(Type);
                            BackpacksContainerMenu.Items.Add(SubType);
                            BackpacksContainerMenu.Items.Add(MinVal);
                            BackpacksContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(BackpacksContainerMenu);

                        SubMenu BarterItemsContainerMenu = new SubMenu("Barter Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable BarterItems Esp", "Draw BarterItems On Contents List", ref Globals.Config.Player.BarterItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.BarterItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.BarterItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.BarterItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.BarterItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.BarterItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.BarterItemsContainer.UseSlotPrice);
                            BarterItemsContainerMenu.Items.Add(Enable);
                            BarterItemsContainerMenu.Items.Add(Name);
                            BarterItemsContainerMenu.Items.Add(Value);
                            BarterItemsContainerMenu.Items.Add(Type);
                            BarterItemsContainerMenu.Items.Add(SubType);
                            BarterItemsContainerMenu.Items.Add(MinVal);
                            BarterItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(BarterItemsContainerMenu);
                        SubMenu CasesContainerMenu = new SubMenu("Case Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Cases Esp", "Draw Cases On Contents List", ref Globals.Config.Player.CasesContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.CasesContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.CasesContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.CasesContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.CasesContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.CasesContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.CasesContainer.UseSlotPrice);
                            CasesContainerMenu.Items.Add(Enable);
                            CasesContainerMenu.Items.Add(Name);
                            CasesContainerMenu.Items.Add(Value);
                            CasesContainerMenu.Items.Add(Type);
                            CasesContainerMenu.Items.Add(SubType);
                            CasesContainerMenu.Items.Add(MinVal);
                            CasesContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(CasesContainerMenu);
                        SubMenu ClothingContainerMenu = new SubMenu("Clothing Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Clothing Esp", "Draw Clothing On Contents List", ref Globals.Config.Player.ClothingContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.ClothingContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.ClothingContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.ClothingContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.ClothingContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.ClothingContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.ClothingContainer.UseSlotPrice);
                            ClothingContainerMenu.Items.Add(Enable);
                            ClothingContainerMenu.Items.Add(Name);
                            ClothingContainerMenu.Items.Add(Value);
                            ClothingContainerMenu.Items.Add(Type);
                            ClothingContainerMenu.Items.Add(SubType);
                            ClothingContainerMenu.Items.Add(MinVal);
                            ClothingContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(ClothingContainerMenu);

                        SubMenu FoodDrinkContainerMenu = new SubMenu("Food And Drink Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable FoodDrink Esp", "Draw Food And Drink On Contents List", ref Globals.Config.Player.FoodDrinkContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.FoodDrinkContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.FoodDrinkContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.FoodDrinkContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.FoodDrinkContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.FoodDrinkContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.FoodDrinkContainer.UseSlotPrice);
                            FoodDrinkContainerMenu.Items.Add(Enable);
                            FoodDrinkContainerMenu.Items.Add(Name);
                            FoodDrinkContainerMenu.Items.Add(Value);
                            FoodDrinkContainerMenu.Items.Add(Type);
                            FoodDrinkContainerMenu.Items.Add(SubType);
                            FoodDrinkContainerMenu.Items.Add(MinVal);
                            FoodDrinkContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(FoodDrinkContainerMenu);

                        SubMenu FuelContainerMenu = new SubMenu("Fuel Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Fuel Esp", "Draw Fuel On Contents List", ref Globals.Config.Player.FuelContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.FuelContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.FuelContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.FuelContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.FuelContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.FuelContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.FuelContainer.UseSlotPrice);
                            FuelContainerMenu.Items.Add(Enable);
                            FuelContainerMenu.Items.Add(Name);
                            FuelContainerMenu.Items.Add(Value);
                            FuelContainerMenu.Items.Add(Type);
                            FuelContainerMenu.Items.Add(SubType);
                            FuelContainerMenu.Items.Add(MinVal);
                            FuelContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(FuelContainerMenu);

                        SubMenu KeyContainerMenu = new SubMenu("Key Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Key Esp", "Draw Key On Contents List", ref Globals.Config.Player.KeyContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.KeyContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.KeyContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.KeyContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.KeyContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.KeyContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.KeyContainer.UseSlotPrice);
                            KeyContainerMenu.Items.Add(Enable);
                            KeyContainerMenu.Items.Add(Name);
                            KeyContainerMenu.Items.Add(Value);
                            KeyContainerMenu.Items.Add(Type);
                            KeyContainerMenu.Items.Add(SubType);
                            KeyContainerMenu.Items.Add(MinVal);
                            KeyContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(KeyContainerMenu);

                        SubMenu KeycardContainerMenu = new SubMenu("Keycard Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Keycard Esp", "Draw Keycard On Contents List", ref Globals.Config.Player.KeycardContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.KeycardContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.KeycardContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.KeycardContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.KeycardContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.KeycardContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.KeycardContainer.UseSlotPrice);
                            KeycardContainerMenu.Items.Add(Enable);
                            KeycardContainerMenu.Items.Add(Name);
                            KeycardContainerMenu.Items.Add(Value);
                            KeycardContainerMenu.Items.Add(Type);
                            KeycardContainerMenu.Items.Add(SubType);
                            KeycardContainerMenu.Items.Add(MinVal);
                            KeycardContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(KeycardContainerMenu);

                        SubMenu MedsContainerMenu = new SubMenu("Medical Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Meds Esp", "Draw Medical On Contents List", ref Globals.Config.Player.MedsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.MedsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.MedsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.MedsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.MedsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.MedsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.MedsContainer.UseSlotPrice);
                            MedsContainerMenu.Items.Add(Enable);
                            MedsContainerMenu.Items.Add(Name);
                            MedsContainerMenu.Items.Add(Value);
                            MedsContainerMenu.Items.Add(Type);
                            MedsContainerMenu.Items.Add(SubType);
                            MedsContainerMenu.Items.Add(MinVal);
                            MedsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(MedsContainerMenu);
                        SubMenu SpecialItemsContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Player.SpecialItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.SpecialItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.SpecialItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.SpecialItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.SpecialItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.SpecialItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.SpecialItemsContainer.UseSlotPrice);
                            SpecialItemsContainerMenu.Items.Add(Enable);
                            SpecialItemsContainerMenu.Items.Add(Name);
                            SpecialItemsContainerMenu.Items.Add(Value);
                            SpecialItemsContainerMenu.Items.Add(Type);
                            SpecialItemsContainerMenu.Items.Add(SubType);
                            SpecialItemsContainerMenu.Items.Add(MinVal);
                            SpecialItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(SpecialItemsContainerMenu);

                        SubMenu WeaponContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Player.WeaponContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Player.WeaponContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Player.WeaponContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Player.WeaponContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Player.WeaponContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Player.WeaponContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Player.WeaponContainer.UseSlotPrice);
                            WeaponContainerMenu.Items.Add(Enable);
                            WeaponContainerMenu.Items.Add(Name);
                            WeaponContainerMenu.Items.Add(Value);
                            WeaponContainerMenu.Items.Add(Type);
                            WeaponContainerMenu.Items.Add(SubType);
                            WeaponContainerMenu.Items.Add(MinVal);
                            WeaponContainerMenu.Items.Add(UseSlotPrice);
                        }
                        PlayerItemsMenu.Items.Add(WeaponContainerMenu);

                    }
                    PlayerEspMenu.Items.Add(PlayerItemsMenu);
                    SubMenu PlayerRadar = new SubMenu("Player Radar Options", "Edit Player Radar Settings");
                    {
                        Toggle enable = new Toggle("Enable", "Toggles On Player Radar", ref Globals.Config.PlayerRadar.Enable);
                        Toggle name = new Toggle("Name", "Display Name On Radar", ref Globals.Config.PlayerRadar.Name);
                        Toggle distance = new Toggle("Distance", "Display Distance On Radar", ref Globals.Config.PlayerRadar.Distance);
                        Toggle weapon = new Toggle("Weapon", "Display Weapon On Radar", ref Globals.Config.PlayerRadar.Weapon);
                        IntSlider fontsize = new IntSlider("Font Size", "Radar Font Size", ref Globals.Config.PlayerRadar.FontSize, 0, 14, 1);
                        IntSlider mindist = new IntSlider("Minimum Text Distance", "Ceases Drawing Radar Text Under This Distance", ref Globals.Config.PlayerRadar.MinimumTextDistance, 0, 1200, 25);
                        IntSlider maxdist = new IntSlider("Maxiumum Radar Draw Distance", "Ceases To Draw Over This Distance", ref Globals.Config.PlayerRadar.MaximumTextDistance, 0, 1200, 25);

                        PlayerRadar.Items.Add(enable);
                        PlayerRadar.Items.Add(name);
                        PlayerRadar.Items.Add(distance);
                        PlayerRadar.Items.Add(weapon);
                        PlayerRadar.Items.Add(fontsize);
                        PlayerRadar.Items.Add(mindist);
                        PlayerRadar.Items.Add(maxdist);

                    }
                    PlayerEspMenu.Items.Add(PlayerRadar);
                    OpacityTypeSlider ScavOpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Player.Opacity);
                    PlayerEspMenu.Items.Add(ScavOpacitySlider);
                    EspMenu.Items.Add(PlayerEspMenu);
                }
                SubMenu ScavEspMenu = new SubMenu("Scav Esp", "Visuals For Scavs");
                {
                    Toggle ScavEnable = new Toggle("Enable Scav Esp", "Allows Scav Esp To Draw", ref Globals.Config.Scav.Enable);
                    Toggle Battlemode = new Toggle("Enable In BattleMode", "", ref Globals.Config.Scav.EnableEspInBattleMode);
                    IntSlider ScavMaxDistance = new IntSlider("Scav Esp Max Distance", "The Distance Scav Esp Ceases To Draw", ref Globals.Config.Scav.MaxDistance, 0, 2000, 25);
                    Toggle ScavVisCheck = new Toggle("Scav Esp Text VisChecks", "Change Text Colour Based On Visibility", ref Globals.Config.Scav.TextVisibilityCheck);
                    ScavEspMenu.Items.Add(ScavEnable);
                    ScavEspMenu.Items.Add(ScavMaxDistance);
                    ScavEspMenu.Items.Add(ScavVisCheck);
                    ScavEspMenu.Items.Add(Battlemode);
                    SubMenu ChamsMenu = new SubMenu("Chams", "Configure Scav Chams");
                    {
                        Toggle Enable = new Toggle("Enable", "Draws Scav Chams", ref Globals.Config.Scav.Chams);
                        Toggle ApplyToGear = new Toggle("Apply To Gear", "Applys Chams To Gear", ref Globals.Config.Scav.ApplyChamsToGear);
                        ChamTypeSlider ChamType = new ChamTypeSlider("Cham Type", "Different Cham Varient", ref Globals.Config.Scav.ChamsType);
                        ChamsMenu.Items.Add(Enable);
                        ChamsMenu.Items.Add(ApplyToGear);
                        ChamsMenu.Items.Add(ChamType);
                    }
                    ScavEspMenu.Items.Add(ChamsMenu);
                    SubMenu ScavTextMenu = new SubMenu("Scav Name", "Configure Scav Name Esp");
                    {
                        Toggle ScavTextMenuEnable = new Toggle("Enable Name Esp", "Draws Scav Tag", ref Globals.Config.Scav.Name.Enable);
                        TextAlignmentSlider ScavTextMenuAlignment = new TextAlignmentSlider("Scav Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.Name.Alignment);
                        IntSlider ScavTextMenuLine = new IntSlider("Scav Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Scav.Name.Line, 1, 3, 1);
                        ScavTextMenu.Items.Add(ScavTextMenuEnable);
                        ScavTextMenu.Items.Add(ScavTextMenuAlignment);
                        ScavTextMenu.Items.Add(ScavTextMenuLine);
                    }
                    ScavEspMenu.Items.Add(ScavTextMenu);
                    SubMenu ScavDistanceMenu = new SubMenu("Scav Distance Esp", "Configure Scav Distance Esp");
                    {
                        Toggle ScavDistanceMenuEnable = new Toggle("Enable Distance Esp", "Draws Scav Distance", ref Globals.Config.Scav.Distance.Enable);
                        TextAlignmentSlider ScavDistanceMenuAlignment = new TextAlignmentSlider("Scav Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.Distance.Alignment);
                        IntSlider ScavDistanceMenuLine = new IntSlider("Scav Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Scav.Distance.Line, 1, 3, 1);
                        ScavDistanceMenu.Items.Add(ScavDistanceMenuEnable);
                        ScavDistanceMenu.Items.Add(ScavDistanceMenuAlignment);
                        ScavDistanceMenu.Items.Add(ScavDistanceMenuLine);
                    }
                    ScavEspMenu.Items.Add(ScavDistanceMenu);
                    SubMenu ScavValueMenu = new SubMenu("Scav Value Esp", "Configure Scav Value Esp");
                    {
                        Toggle ScavValueMenuEnable = new Toggle("Enable Value Esp", "Draws Scav Value", ref Globals.Config.Scav.Value.Enable);
                        TextAlignmentSlider ScavValueMenuAlignment = new TextAlignmentSlider("Scav Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.Value.Alignment);
                        IntSlider ScavValueMenuLine = new IntSlider("Scav Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Scav.Value.Line, 1, 3, 1);
                        Toggle ScavValueSkipAttachments = new Toggle("Skip Attachments", "Doesn't Add Attachments To Value", ref Globals.Config.Scav.SkipAttachmentsInValue);
                        ScavValueMenu.Items.Add(ScavValueMenuEnable);
                        ScavValueMenu.Items.Add(ScavValueMenuAlignment);
                        ScavValueMenu.Items.Add(ScavValueMenuLine);
                        ScavValueMenu.Items.Add(ScavValueSkipAttachments);
                    }
                    ScavEspMenu.Items.Add(ScavValueMenu);
                    SubMenu ScavSkeletonMenu = new SubMenu("Skeleton Esp", "Configure Scav Skeleton");
                    {
                        Toggle ScavSkeletonEnable = new Toggle("Enable Skeleton Esp", "Draws Scav Skeleton", ref Globals.Config.Scav.Skeleton.Enable);
                        SkeletonTypeSlider ScavSkeletonType = new SkeletonTypeSlider("Skeleton Type", "Choose If The Skeleton Uses Visibility Or Health", ref Globals.Config.Scav.Skeleton.Type);
                        IntSlider ScavSkeletonMaxDistance = new IntSlider("Skeleton Max Distance", "Max Distance That Skeletons Draw At", ref Globals.Config.Scav.Skeleton.MaxDistance, 0, 850, 25);
                        ScavSkeletonMenu.Items.Add(ScavSkeletonEnable);
                        ScavSkeletonMenu.Items.Add(ScavSkeletonType);
                        ScavSkeletonMenu.Items.Add(ScavSkeletonMaxDistance);
                    }
                    ScavEspMenu.Items.Add(ScavSkeletonMenu);

                    SubMenu ScavWeaponMenu = new SubMenu("Scav Weapon Esp", "Configure Scav Weapon Esp");
                    {
                        Toggle ScavWeaponMenuEnable = new Toggle("Enable Weapon Esp", "Draws Scav Weapon", ref Globals.Config.Scav.Weapon.Enable);
                        TextAlignmentSlider ScavWeaponeMenuAlignment = new TextAlignmentSlider("Scav Weapon Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.Weapon.Alignment);
                        IntSlider ScavWeaponMenuLine = new IntSlider("Scav Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Scav.Weapon.Line, 1, 3, 1);
                        ScavWeaponMenu.Items.Add(ScavWeaponMenuEnable);
                        ScavWeaponMenu.Items.Add(ScavWeaponeMenuAlignment);
                        ScavWeaponMenu.Items.Add(ScavWeaponMenuLine);
                    }
                    ScavEspMenu.Items.Add(ScavWeaponMenu);

                    SubMenu ScavAmmoMenu = new SubMenu("Scav Ammo Esp", "Configure Scav Weapon Esp");
                    {
                        Toggle ScavAmmoMenuEnable = new Toggle("Enable Ammo Esp", "Draws Scav Ammo", ref Globals.Config.Scav.Ammo.Enable);
                        TextAlignmentSlider ScavAmmoMenuAlignment = new TextAlignmentSlider("Scav Ammo Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.Ammo.Alignment);
                        IntSlider ScavAmmoMenuLine = new IntSlider("Scav Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Scav.Ammo.Line, 1, 3, 1);
                        ScavAmmoMenu.Items.Add(ScavAmmoMenuEnable);
                        ScavAmmoMenu.Items.Add(ScavAmmoMenuAlignment);
                        ScavAmmoMenu.Items.Add(ScavAmmoMenuLine);
                    }
                    ScavEspMenu.Items.Add(ScavAmmoMenu);

                    SubMenu ScavBoxMenu = new SubMenu("Scav Box Esp", "Configure Scav Weapon Esp");
                    {
                        Toggle ScavBoxMenuEnable = new Toggle("Enable Box Esp", "Draws Scav Boxes", ref Globals.Config.Scav.Box.Enable);
                        IntSlider ScavBoxMaxDistance = new IntSlider("Box Max Distance", "Max Distance That Boxes Draw At", ref Globals.Config.Scav.Box.MaxDistance, 0, 150, 25);
                        Toggle ScavFillBoxMenuEnable = new Toggle("Enable Box Filled Esp", "Draws Filled Boxes", ref Globals.Config.Scav.Box.Fill);
                        Toggle ScavFillBoxVisibilityMenuEnable = new Toggle("Enable Filled Box Visibility Esp", "Draws Different Colours For Visible Players", ref Globals.Config.Scav.Box.VisibilityCheck);
                        ScavBoxMenu.Items.Add(ScavBoxMenuEnable);
                        ScavBoxMenu.Items.Add(ScavBoxMaxDistance);
                        ScavBoxMenu.Items.Add(ScavFillBoxMenuEnable);
                        ScavBoxMenu.Items.Add(ScavFillBoxVisibilityMenuEnable);
                    }
                    ScavEspMenu.Items.Add(ScavBoxMenu);
                    SubMenu ScavHealthbarMenu = new SubMenu("Scav Healthbar Esp", "Configure Scav Weapon Esp");
                    {
                        Toggle ScavHealthbarMenuEnable = new Toggle("Enable Healthbar Esp", "Draws Scav Healthbar", ref Globals.Config.Scav.Healthbar.Enable);
                        TextAlignmentSlider ScavHealthbarMenuAlignment = new TextAlignmentSlider("Scav Healthbar Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.Healthbar.Alignment);
                        IntSlider ScavHealthbarMaxDistance = new IntSlider("Healthbar Max Distance", "Max Distance That Healthbar Draw At", ref Globals.Config.Scav.Healthbar.MaxDistance, 0, 850, 25);
                        ScavHealthbarMenu.Items.Add(ScavHealthbarMenuEnable);
                        ScavHealthbarMenu.Items.Add(ScavHealthbarMenuAlignment);
                        ScavHealthbarMenu.Items.Add(ScavHealthbarMaxDistance);
                    }
                    ScavEspMenu.Items.Add(ScavHealthbarMenu);
                    SubMenu ScavItemsMenu = new SubMenu("Scav Contents Esp", "Configure Scav Contents Esp");
                    {
                        Toggle ScavContentsEnable = new Toggle("Enable Contents Esp", "Draws Scav Distance", ref Globals.Config.Scav.ContentsList);
                        TextAlignmentSlider ScavContentsAlignment = new TextAlignmentSlider("Scav Contents Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Scav.ContentListAlignment);
                        Keybind ScavContentsLine = new Keybind("Scav Contents Key", "Key To Toggle On Or Off Contents", ref Globals.Config.Scav.ContentsListKey);
                        ScavItemsMenu.Items.Add(ScavContentsEnable);
                        ScavItemsMenu.Items.Add(ScavContentsAlignment);
                        ScavItemsMenu.Items.Add(ScavContentsLine);

                        SubMenu WhitelistContainerMenu = new SubMenu("Whitelist Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Whitelist Esp", "Draw Whitelist On Contents List", ref Globals.Config.Scav.WhitelistContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.WhitelistContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.WhitelistContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.WhitelistContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.WhitelistContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.WhitelistContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.WhitelistContainer.UseSlotPrice);
                            WhitelistContainerMenu.Items.Add(Enable);
                            WhitelistContainerMenu.Items.Add(Name);
                            WhitelistContainerMenu.Items.Add(Value);
                            WhitelistContainerMenu.Items.Add(Type);
                            WhitelistContainerMenu.Items.Add(SubType);
                            WhitelistContainerMenu.Items.Add(MinVal);
                            WhitelistContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(WhitelistContainerMenu);

                        SubMenu AmmoContainerMenu = new SubMenu("Ammo Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Ammo Esp", "Draw Ammo On Contents List", ref Globals.Config.Scav.AmmoContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.AmmoContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.AmmoContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.AmmoContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.AmmoContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.AmmoContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.AmmoContainer.UseSlotPrice);
                            AmmoContainerMenu.Items.Add(Enable);
                            AmmoContainerMenu.Items.Add(Name);
                            AmmoContainerMenu.Items.Add(Value);
                            AmmoContainerMenu.Items.Add(Type);
                            AmmoContainerMenu.Items.Add(SubType);
                            AmmoContainerMenu.Items.Add(MinVal);
                            AmmoContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(AmmoContainerMenu);
                        SubMenu AttachmentsContainerMenu = new SubMenu("Attachments Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Attachments Esp", "Draw Attachments On Contents List", ref Globals.Config.Scav.AttachmentsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.AttachmentsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.AttachmentsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.AttachmentsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.AttachmentsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.AttachmentsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.AttachmentsContainer.UseSlotPrice);
                            AttachmentsContainerMenu.Items.Add(Enable);
                            AttachmentsContainerMenu.Items.Add(Name);
                            AttachmentsContainerMenu.Items.Add(Value);
                            AttachmentsContainerMenu.Items.Add(Type);
                            AttachmentsContainerMenu.Items.Add(SubType);
                            AttachmentsContainerMenu.Items.Add(MinVal);
                            AttachmentsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(AttachmentsContainerMenu);
                        SubMenu BackpacksContainerMenu = new SubMenu("Backpacks Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Backpacks Esp", "Draw Backpacks On Contents List", ref Globals.Config.Scav.BackpacksContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.BackpacksContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.BackpacksContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.BackpacksContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.BackpacksContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.BackpacksContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.BackpacksContainer.UseSlotPrice);
                            BackpacksContainerMenu.Items.Add(Enable);
                            BackpacksContainerMenu.Items.Add(Name);
                            BackpacksContainerMenu.Items.Add(Value);
                            BackpacksContainerMenu.Items.Add(Type);
                            BackpacksContainerMenu.Items.Add(SubType);
                            BackpacksContainerMenu.Items.Add(MinVal);
                            BackpacksContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(BackpacksContainerMenu);

                        SubMenu BarterItemsContainerMenu = new SubMenu("Barter Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable BarterItems Esp", "Draw BarterItems On Contents List", ref Globals.Config.Scav.BarterItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.BarterItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.BarterItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.BarterItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.BarterItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.BarterItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.BarterItemsContainer.UseSlotPrice);
                            BarterItemsContainerMenu.Items.Add(Enable);
                            BarterItemsContainerMenu.Items.Add(Name);
                            BarterItemsContainerMenu.Items.Add(Value);
                            BarterItemsContainerMenu.Items.Add(Type);
                            BarterItemsContainerMenu.Items.Add(SubType);
                            BarterItemsContainerMenu.Items.Add(MinVal);
                            BarterItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(BarterItemsContainerMenu);
                        SubMenu CasesContainerMenu = new SubMenu("Case Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Cases Esp", "Draw Cases On Contents List", ref Globals.Config.Scav.CasesContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.CasesContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.CasesContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.CasesContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.CasesContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.CasesContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.CasesContainer.UseSlotPrice);
                            CasesContainerMenu.Items.Add(Enable);
                            CasesContainerMenu.Items.Add(Name);
                            CasesContainerMenu.Items.Add(Value);
                            CasesContainerMenu.Items.Add(Type);
                            CasesContainerMenu.Items.Add(SubType);
                            CasesContainerMenu.Items.Add(MinVal);
                            CasesContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(CasesContainerMenu);
                        SubMenu ClothingContainerMenu = new SubMenu("Clothing Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Clothing Esp", "Draw Clothing On Contents List", ref Globals.Config.Scav.ClothingContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.ClothingContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.ClothingContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.ClothingContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.ClothingContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.ClothingContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.ClothingContainer.UseSlotPrice);
                            ClothingContainerMenu.Items.Add(Enable);
                            ClothingContainerMenu.Items.Add(Name);
                            ClothingContainerMenu.Items.Add(Value);
                            ClothingContainerMenu.Items.Add(Type);
                            ClothingContainerMenu.Items.Add(SubType);
                            ClothingContainerMenu.Items.Add(MinVal);
                            ClothingContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(ClothingContainerMenu);

                        SubMenu FoodDrinkContainerMenu = new SubMenu("Food And Drink Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable FoodDrink Esp", "Draw Food And Drink On Contents List", ref Globals.Config.Scav.FoodDrinkContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.FoodDrinkContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.FoodDrinkContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.FoodDrinkContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.FoodDrinkContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.FoodDrinkContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.FoodDrinkContainer.UseSlotPrice);
                            FoodDrinkContainerMenu.Items.Add(Enable);
                            FoodDrinkContainerMenu.Items.Add(Name);
                            FoodDrinkContainerMenu.Items.Add(Value);
                            FoodDrinkContainerMenu.Items.Add(Type);
                            FoodDrinkContainerMenu.Items.Add(SubType);
                            FoodDrinkContainerMenu.Items.Add(MinVal);
                            FoodDrinkContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(FoodDrinkContainerMenu);

                        SubMenu FuelContainerMenu = new SubMenu("Fuel Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Fuel Esp", "Draw Fuel On Contents List", ref Globals.Config.Scav.FuelContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.FuelContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.FuelContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.FuelContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.FuelContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.FuelContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.FuelContainer.UseSlotPrice);
                            FuelContainerMenu.Items.Add(Enable);
                            FuelContainerMenu.Items.Add(Name);
                            FuelContainerMenu.Items.Add(Value);
                            FuelContainerMenu.Items.Add(Type);
                            FuelContainerMenu.Items.Add(SubType);
                            FuelContainerMenu.Items.Add(MinVal);
                            FuelContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(FuelContainerMenu);

                        SubMenu KeyContainerMenu = new SubMenu("Key Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Key Esp", "Draw Key On Contents List", ref Globals.Config.Scav.KeyContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.KeyContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.KeyContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.KeyContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.KeyContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.KeyContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.KeyContainer.UseSlotPrice);
                            KeyContainerMenu.Items.Add(Enable);
                            KeyContainerMenu.Items.Add(Name);
                            KeyContainerMenu.Items.Add(Value);
                            KeyContainerMenu.Items.Add(Type);
                            KeyContainerMenu.Items.Add(SubType);
                            KeyContainerMenu.Items.Add(MinVal);
                            KeyContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(KeyContainerMenu);

                        SubMenu KeycardContainerMenu = new SubMenu("Keycard Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Keycard Esp", "Draw Keycard On Contents List", ref Globals.Config.Scav.KeycardContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.KeycardContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.KeycardContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.KeycardContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.KeycardContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.KeycardContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.KeycardContainer.UseSlotPrice);
                            KeycardContainerMenu.Items.Add(Enable);
                            KeycardContainerMenu.Items.Add(Name);
                            KeycardContainerMenu.Items.Add(Value);
                            KeycardContainerMenu.Items.Add(Type);
                            KeycardContainerMenu.Items.Add(SubType);
                            KeycardContainerMenu.Items.Add(MinVal);
                            KeycardContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(KeycardContainerMenu);

                        SubMenu MedsContainerMenu = new SubMenu("Medical Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Meds Esp", "Draw Medical On Contents List", ref Globals.Config.Scav.MedsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.MedsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.MedsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.MedsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.MedsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.MedsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.MedsContainer.UseSlotPrice);
                            MedsContainerMenu.Items.Add(Enable);
                            MedsContainerMenu.Items.Add(Name);
                            MedsContainerMenu.Items.Add(Value);
                            MedsContainerMenu.Items.Add(Type);
                            MedsContainerMenu.Items.Add(SubType);
                            MedsContainerMenu.Items.Add(MinVal);
                            MedsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(MedsContainerMenu);
                        SubMenu SpecialItemsContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Scav.SpecialItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.SpecialItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.SpecialItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.SpecialItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.SpecialItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.SpecialItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.SpecialItemsContainer.UseSlotPrice);
                            SpecialItemsContainerMenu.Items.Add(Enable);
                            SpecialItemsContainerMenu.Items.Add(Name);
                            SpecialItemsContainerMenu.Items.Add(Value);
                            SpecialItemsContainerMenu.Items.Add(Type);
                            SpecialItemsContainerMenu.Items.Add(SubType);
                            SpecialItemsContainerMenu.Items.Add(MinVal);
                            SpecialItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(SpecialItemsContainerMenu);

                        SubMenu WeaponContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Scav.WeaponContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Scav.WeaponContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Scav.WeaponContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Scav.WeaponContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Scav.WeaponContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Scav.WeaponContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Scav.WeaponContainer.UseSlotPrice);
                            WeaponContainerMenu.Items.Add(Enable);
                            WeaponContainerMenu.Items.Add(Name);
                            WeaponContainerMenu.Items.Add(Value);
                            WeaponContainerMenu.Items.Add(Type);
                            WeaponContainerMenu.Items.Add(SubType);
                            WeaponContainerMenu.Items.Add(MinVal);
                            WeaponContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavItemsMenu.Items.Add(WeaponContainerMenu);

                    }
                    ScavEspMenu.Items.Add(ScavItemsMenu);
                    SubMenu ScavRadar = new SubMenu("Scav Radar Options", "Edit Scav Radar Settings");
                    {
                        Toggle enable = new Toggle("Enable", "Toggles On Scav Radar", ref Globals.Config.ScavRadar.Enable);
                        Toggle name = new Toggle("Name", "Display Name On Radar", ref Globals.Config.ScavRadar.Name);
                        Toggle distance = new Toggle("Distance", "Display Distance On Radar", ref Globals.Config.ScavRadar.Distance);
                        Toggle weapon = new Toggle("Weapon", "Display Weapon On Radar", ref Globals.Config.ScavRadar.Weapon);
                        IntSlider fontsize = new IntSlider("Font Size", "Radar Font Size", ref Globals.Config.ScavRadar.FontSize, 0, 14, 1);
                        IntSlider mindist = new IntSlider("Minimum Text Distance", "Ceases Drawing Radar Text Under This Distance", ref Globals.Config.ScavRadar.MinimumTextDistance, 0, 1200, 25);
                        IntSlider maxdist = new IntSlider("Maxiumum Radar Draw Distance", "Ceases To Draw Over This Distance", ref Globals.Config.ScavRadar.MaximumTextDistance, 0, 1200, 25);

                        ScavRadar.Items.Add(enable);
                        ScavRadar.Items.Add(name);
                        ScavRadar.Items.Add(distance);
                        ScavRadar.Items.Add(weapon);
                        ScavRadar.Items.Add(fontsize);
                        ScavRadar.Items.Add(mindist);
                        ScavRadar.Items.Add(maxdist);

                    }
                    ScavEspMenu.Items.Add(ScavRadar);
                    OpacityTypeSlider ScavOpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Scav.Opacity);
                    ScavEspMenu.Items.Add(ScavOpacitySlider);
                    EspMenu.Items.Add(ScavEspMenu);
                }
                SubMenu ScavPlayerEspMenu = new SubMenu("Scav Player Esp", "Visuals For Scav Players");
                {
                    Toggle ScavPlayerEnable = new Toggle("Enable Scav Player Esp", "Allows ScavPlayer Esp To Draw", ref Globals.Config.ScavPlayer.Enable);
                    IntSlider ScavPlayerMaxDistance = new IntSlider("Scav Player Esp Max Distance", "The Distance Scav Player Esp Ceases To Draw", ref Globals.Config.ScavPlayer.MaxDistance, 0, 2000, 25);
                    Toggle ScavPlayerVisCheck = new Toggle("Scav Player Esp Text VisChecks", "Change Text Colour Based On Visibility", ref Globals.Config.ScavPlayer.TextVisibilityCheck);
                    Toggle Battlemode = new Toggle("Enable In BattleMode", "", ref Globals.Config.ScavPlayer.EnableEspInBattleMode);
                    ScavPlayerEspMenu.Items.Add(ScavPlayerEnable);
                    ScavPlayerEspMenu.Items.Add(ScavPlayerMaxDistance);
                    ScavPlayerEspMenu.Items.Add(ScavPlayerVisCheck);
                    ScavPlayerEspMenu.Items.Add(Battlemode);
                    SubMenu ChamsMenu = new SubMenu("Chams", "Configure Scav Player Chams");
                    {
                        Toggle Enable = new Toggle("Enable", "Draws Scav Player Chams", ref Globals.Config.ScavPlayer.Chams);
                        Toggle ApplyToGear = new Toggle("Apply To Gear", "Applys Chams To Gear", ref Globals.Config.ScavPlayer.ApplyChamsToGear);
                        ChamTypeSlider ChamType = new ChamTypeSlider("Cham Type", "Different Cham Varient", ref Globals.Config.ScavPlayer.ChamsType);
                        ChamsMenu.Items.Add(Enable);
                        ChamsMenu.Items.Add(ApplyToGear);
                        ChamsMenu.Items.Add(ChamType);
                    }
                    ScavPlayerEspMenu.Items.Add(ChamsMenu);
                    SubMenu ScavPlayerTextMenu = new SubMenu("Scav Player Name", "Configure Scav Player Name Esp");
                    {
                        Toggle ScavPlayerTextMenuEnable = new Toggle("Enable Name Esp", "Draws Scav Player Tag", ref Globals.Config.ScavPlayer.Name.Enable);
                        TextAlignmentSlider ScavPlayerTextMenuAlignment = new TextAlignmentSlider("Scav Player Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.Name.Alignment);
                        IntSlider ScavPlayerTextMenuLine = new IntSlider("Scav Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.ScavPlayer.Name.Line, 1, 3, 1);
                        ScavPlayerTextMenu.Items.Add(ScavPlayerTextMenuEnable);
                        ScavPlayerTextMenu.Items.Add(ScavPlayerTextMenuAlignment);
                        ScavPlayerTextMenu.Items.Add(ScavPlayerTextMenuLine);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerTextMenu);
                    SubMenu ScavPlayerDistanceMenu = new SubMenu("Scav Player Distance Esp", "Configure Scav Player Distance Esp");
                    {
                        Toggle ScavPlayerDistanceMenuEnable = new Toggle("Enable Distance Esp", "Draws Scav Player Distance", ref Globals.Config.ScavPlayer.Distance.Enable);
                        TextAlignmentSlider ScavPlayerDistanceMenuAlignment = new TextAlignmentSlider("Scav Player Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.Distance.Alignment);
                        IntSlider ScavPlayerDistanceMenuLine = new IntSlider("ScavPlayer Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.ScavPlayer.Distance.Line, 1, 3, 1);
                        ScavPlayerDistanceMenu.Items.Add(ScavPlayerDistanceMenuEnable);
                        ScavPlayerDistanceMenu.Items.Add(ScavPlayerDistanceMenuAlignment);
                        ScavPlayerDistanceMenu.Items.Add(ScavPlayerDistanceMenuLine);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerDistanceMenu);
                    SubMenu ScavPlayerValueMenu = new SubMenu("Scav Player Value Esp", "Configure Scav Player Value Esp");
                    {
                        Toggle ScavPlayerValueMenuEnable = new Toggle("Enable Value Esp", "Draws Scav Player Value", ref Globals.Config.ScavPlayer.Value.Enable);
                        TextAlignmentSlider ScavPlayerValueMenuAlignment = new TextAlignmentSlider("Scav Player Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.Value.Alignment);
                        IntSlider ScavPlayerValueMenuLine = new IntSlider("Scav Player Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.ScavPlayer.Value.Line, 1, 3, 1);
                        Toggle ScavPlayerValueSkipAttachments = new Toggle("Skip Attachments", "Doesn't Add Attachments To Value", ref Globals.Config.ScavPlayer.SkipAttachmentsInValue);
                        ScavPlayerValueMenu.Items.Add(ScavPlayerValueMenuEnable);
                        ScavPlayerValueMenu.Items.Add(ScavPlayerValueMenuAlignment);
                        ScavPlayerValueMenu.Items.Add(ScavPlayerValueMenuLine);
                        ScavPlayerValueMenu.Items.Add(ScavPlayerValueSkipAttachments);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerValueMenu);

                    SubMenu ScavPlayerSkeletonMenu = new SubMenu("Skeleton Esp", "Configure Scav Player Skeleton");
                    {
                        Toggle ScavPlayerSkeletonEnable = new Toggle("Enable Skeleton Esp", "Draws Scav Player Skeleton", ref Globals.Config.ScavPlayer.Skeleton.Enable);
                        SkeletonTypeSlider ScavPlayerSkeletonType = new SkeletonTypeSlider("Skeleton Type", "Choose If The Skeleton Uses Visibility Or Health", ref Globals.Config.ScavPlayer.Skeleton.Type);
                        IntSlider ScavPlayerSkeletonMaxDistance = new IntSlider("Skeleton Max Distance", "Max Distance That Skeletons Draw At", ref Globals.Config.ScavPlayer.Skeleton.MaxDistance, 0, 850, 25);
                        ScavPlayerSkeletonMenu.Items.Add(ScavPlayerSkeletonEnable);
                        ScavPlayerSkeletonMenu.Items.Add(ScavPlayerSkeletonType);
                        ScavPlayerSkeletonMenu.Items.Add(ScavPlayerSkeletonMaxDistance);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerSkeletonMenu);
                    SubMenu ScavPlayerWeaponMenu = new SubMenu("Scav Player Weapon Esp", "Configure Scav Player Weapon Esp");
                    {
                        Toggle ScavPlayerWeaponMenuEnable = new Toggle("Enable Weapon Esp", "Draws Scav Player Weapon", ref Globals.Config.ScavPlayer.Weapon.Enable);
                        TextAlignmentSlider ScavPlayerWeaponeMenuAlignment = new TextAlignmentSlider("Scav Player Weapon Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.Weapon.Alignment);
                        IntSlider ScavPlayerWeaponMenuLine = new IntSlider("Scav Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.ScavPlayer.Weapon.Line, 1, 3, 1);
                        ScavPlayerWeaponMenu.Items.Add(ScavPlayerWeaponMenuEnable);
                        ScavPlayerWeaponMenu.Items.Add(ScavPlayerWeaponeMenuAlignment);
                        ScavPlayerWeaponMenu.Items.Add(ScavPlayerWeaponMenuLine);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerWeaponMenu);

                    SubMenu ScavPlayerAmmoMenu = new SubMenu("Scav Player Ammo Esp", "Configure Scav Player Weapon Esp");
                    {
                        Toggle ScavPlayerAmmoMenuEnable = new Toggle("Enable Ammo Esp", "Draws Scav Player Ammo", ref Globals.Config.ScavPlayer.Ammo.Enable);
                        TextAlignmentSlider ScavPlayerAmmoMenuAlignment = new TextAlignmentSlider("Scav Player Ammo Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.Ammo.Alignment);
                        IntSlider ScavPlayerAmmoMenuLine = new IntSlider("Scav Player Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.ScavPlayer.Ammo.Line, 1, 3, 1);
                        ScavPlayerAmmoMenu.Items.Add(ScavPlayerAmmoMenuEnable);
                        ScavPlayerAmmoMenu.Items.Add(ScavPlayerAmmoMenuAlignment);
                        ScavPlayerAmmoMenu.Items.Add(ScavPlayerAmmoMenuLine);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerAmmoMenu);
                    SubMenu ScavPlayerBoxMenu = new SubMenu("Scav Player Box Esp", "Configure Scav Player Weapon Esp");
                    {
                        Toggle ScavPlayerBoxMenuEnable = new Toggle("Enable Box Esp", "Draws Scav Player Boxes", ref Globals.Config.ScavPlayer.Box.Enable);
                        IntSlider ScavPlayerBoxMaxDistance = new IntSlider("Box Max Distance", "Max Distance That Boxes Draw At", ref Globals.Config.ScavPlayer.Box.MaxDistance, 0, 150, 25);
                        Toggle ScavPlayerFillBoxMenuEnable = new Toggle("Enable Box Filled Esp", "Draws Filled Boxes", ref Globals.Config.ScavPlayer.Box.Fill);
                        Toggle ScavPlayerFillBoxVisibilityMenuEnable = new Toggle("Enable Filled Box Visibility Esp", "Draws Different Colours For Visible Players", ref Globals.Config.ScavPlayer.Box.VisibilityCheck);
                        ScavPlayerBoxMenu.Items.Add(ScavPlayerBoxMenuEnable);
                        ScavPlayerBoxMenu.Items.Add(ScavPlayerBoxMaxDistance);
                        ScavPlayerBoxMenu.Items.Add(ScavPlayerFillBoxMenuEnable);
                        ScavPlayerBoxMenu.Items.Add(ScavPlayerFillBoxVisibilityMenuEnable);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerBoxMenu);
                    SubMenu ScavPlayerHealthbarMenu = new SubMenu("Scav Player Healthbar Esp", "Configure Scav Player Weapon Esp");
                    {
                        Toggle ScavPlayerHealthbarMenuEnable = new Toggle("Enable Healthbar Esp", "Draws Scav Player Healthbar", ref Globals.Config.ScavPlayer.Healthbar.Enable);
                        TextAlignmentSlider ScavPlayerHealthbarMenuAlignment = new TextAlignmentSlider("Scav Player Healthbar Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.Healthbar.Alignment);
                        IntSlider ScavPlayerHealthbarMaxDistance = new IntSlider("Healthbar Max Distance", "Max Distance That Healthbar Draw At", ref Globals.Config.ScavPlayer.Healthbar.MaxDistance, 0, 850, 25);
                        ScavPlayerHealthbarMenu.Items.Add(ScavPlayerHealthbarMenuEnable);
                        ScavPlayerHealthbarMenu.Items.Add(ScavPlayerHealthbarMenuAlignment);
                        ScavPlayerHealthbarMenu.Items.Add(ScavPlayerHealthbarMaxDistance);
                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerHealthbarMenu);
                    SubMenu ScavPlayerItemsMenu = new SubMenu("Scav Player Contents Esp", "Configure Scav Player Contents Esp");
                    {
                        Toggle ScavPlayerContentsEnable = new Toggle("Enable Contents Esp", "Draws ScavPlayer Distance", ref Globals.Config.ScavPlayer.ContentsList);
                        TextAlignmentSlider ScavPlayerContentsAlignment = new TextAlignmentSlider("ScavPlayer Contents Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.ScavPlayer.ContentListAlignment);
                        Keybind ScavPlayerContentsLine = new Keybind("ScavPlayer Contents Key", "Key To Toggle On And Off Contents", ref Globals.Config.ScavPlayer.ContentsListKey);
                        ScavPlayerItemsMenu.Items.Add(ScavPlayerContentsEnable);
                        ScavPlayerItemsMenu.Items.Add(ScavPlayerContentsAlignment);
                        ScavPlayerItemsMenu.Items.Add(ScavPlayerContentsLine);

                        SubMenu WhitelistContainerMenu = new SubMenu("Whitelist Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Whitelist Esp", "Draw Whitelist On Contents List", ref Globals.Config.ScavPlayer.WhitelistContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.WhitelistContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.WhitelistContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.WhitelistContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.WhitelistContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.WhitelistContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.WhitelistContainer.UseSlotPrice);
                            WhitelistContainerMenu.Items.Add(Enable);
                            WhitelistContainerMenu.Items.Add(Name);
                            WhitelistContainerMenu.Items.Add(Value);
                            WhitelistContainerMenu.Items.Add(Type);
                            WhitelistContainerMenu.Items.Add(SubType);
                            WhitelistContainerMenu.Items.Add(MinVal);
                            WhitelistContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(WhitelistContainerMenu);

                        SubMenu AmmoContainerMenu = new SubMenu("Ammo Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Ammo Esp", "Draw Ammo On Contents List", ref Globals.Config.ScavPlayer.AmmoContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.AmmoContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.AmmoContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.AmmoContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.AmmoContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.AmmoContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.AmmoContainer.UseSlotPrice);
                            AmmoContainerMenu.Items.Add(Enable);
                            AmmoContainerMenu.Items.Add(Name);
                            AmmoContainerMenu.Items.Add(Value);
                            AmmoContainerMenu.Items.Add(Type);
                            AmmoContainerMenu.Items.Add(SubType);
                            AmmoContainerMenu.Items.Add(MinVal);
                            AmmoContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(AmmoContainerMenu);
                        SubMenu AttachmentsContainerMenu = new SubMenu("Attachments Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Attachments Esp", "Draw Attachments On Contents List", ref Globals.Config.ScavPlayer.AttachmentsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.AttachmentsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.AttachmentsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.AttachmentsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.AttachmentsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.AttachmentsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.AttachmentsContainer.UseSlotPrice);
                            AttachmentsContainerMenu.Items.Add(Enable);
                            AttachmentsContainerMenu.Items.Add(Name);
                            AttachmentsContainerMenu.Items.Add(Value);
                            AttachmentsContainerMenu.Items.Add(Type);
                            AttachmentsContainerMenu.Items.Add(SubType);
                            AttachmentsContainerMenu.Items.Add(MinVal);
                            AttachmentsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(AttachmentsContainerMenu);
                        SubMenu BackpacksContainerMenu = new SubMenu("Backpacks Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Backpacks Esp", "Draw Backpacks On Contents List", ref Globals.Config.ScavPlayer.BackpacksContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.BackpacksContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.BackpacksContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.BackpacksContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.BackpacksContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.BackpacksContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.BackpacksContainer.UseSlotPrice);
                            BackpacksContainerMenu.Items.Add(Enable);
                            BackpacksContainerMenu.Items.Add(Name);
                            BackpacksContainerMenu.Items.Add(Value);
                            BackpacksContainerMenu.Items.Add(Type);
                            BackpacksContainerMenu.Items.Add(SubType);
                            BackpacksContainerMenu.Items.Add(MinVal);
                            BackpacksContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(BackpacksContainerMenu);

                        SubMenu BarterItemsContainerMenu = new SubMenu("Barter Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable BarterItems Esp", "Draw BarterItems On Contents List", ref Globals.Config.ScavPlayer.BarterItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.BarterItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.BarterItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.BarterItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.BarterItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.BarterItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.BarterItemsContainer.UseSlotPrice);
                            BarterItemsContainerMenu.Items.Add(Enable);
                            BarterItemsContainerMenu.Items.Add(Name);
                            BarterItemsContainerMenu.Items.Add(Value);
                            BarterItemsContainerMenu.Items.Add(Type);
                            BarterItemsContainerMenu.Items.Add(SubType);
                            BarterItemsContainerMenu.Items.Add(MinVal);
                            BarterItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(BarterItemsContainerMenu);
                        SubMenu CasesContainerMenu = new SubMenu("Case Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Cases Esp", "Draw Cases On Contents List", ref Globals.Config.ScavPlayer.CasesContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.CasesContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.CasesContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.CasesContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.CasesContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.CasesContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.CasesContainer.UseSlotPrice);
                            CasesContainerMenu.Items.Add(Enable);
                            CasesContainerMenu.Items.Add(Name);
                            CasesContainerMenu.Items.Add(Value);
                            CasesContainerMenu.Items.Add(Type);
                            CasesContainerMenu.Items.Add(SubType);
                            CasesContainerMenu.Items.Add(MinVal);
                            CasesContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(CasesContainerMenu);
                        SubMenu ClothingContainerMenu = new SubMenu("Clothing Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Clothing Esp", "Draw Clothing On Contents List", ref Globals.Config.ScavPlayer.ClothingContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.ClothingContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.ClothingContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.ClothingContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.ClothingContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.ClothingContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.ClothingContainer.UseSlotPrice);
                            ClothingContainerMenu.Items.Add(Enable);
                            ClothingContainerMenu.Items.Add(Name);
                            ClothingContainerMenu.Items.Add(Value);
                            ClothingContainerMenu.Items.Add(Type);
                            ClothingContainerMenu.Items.Add(SubType);
                            ClothingContainerMenu.Items.Add(MinVal);
                            ClothingContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(ClothingContainerMenu);

                        SubMenu FoodDrinkContainerMenu = new SubMenu("Food And Drink Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable FoodDrink Esp", "Draw Food And Drink On Contents List", ref Globals.Config.ScavPlayer.FoodDrinkContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.FoodDrinkContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.FoodDrinkContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.FoodDrinkContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.FoodDrinkContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.FoodDrinkContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.FoodDrinkContainer.UseSlotPrice);
                            FoodDrinkContainerMenu.Items.Add(Enable);
                            FoodDrinkContainerMenu.Items.Add(Name);
                            FoodDrinkContainerMenu.Items.Add(Value);
                            FoodDrinkContainerMenu.Items.Add(Type);
                            FoodDrinkContainerMenu.Items.Add(SubType);
                            FoodDrinkContainerMenu.Items.Add(MinVal);
                            FoodDrinkContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(FoodDrinkContainerMenu);

                        SubMenu FuelContainerMenu = new SubMenu("Fuel Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Fuel Esp", "Draw Fuel On Contents List", ref Globals.Config.ScavPlayer.FuelContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.FuelContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.FuelContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.FuelContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.FuelContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.FuelContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.FuelContainer.UseSlotPrice);
                            FuelContainerMenu.Items.Add(Enable);
                            FuelContainerMenu.Items.Add(Name);
                            FuelContainerMenu.Items.Add(Value);
                            FuelContainerMenu.Items.Add(Type);
                            FuelContainerMenu.Items.Add(SubType);
                            FuelContainerMenu.Items.Add(MinVal);
                            FuelContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(FuelContainerMenu);

                        SubMenu KeyContainerMenu = new SubMenu("Key Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Key Esp", "Draw Key On Contents List", ref Globals.Config.ScavPlayer.KeyContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.KeyContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.KeyContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.KeyContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.KeyContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.KeyContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.KeyContainer.UseSlotPrice);
                            KeyContainerMenu.Items.Add(Enable);
                            KeyContainerMenu.Items.Add(Name);
                            KeyContainerMenu.Items.Add(Value);
                            KeyContainerMenu.Items.Add(Type);
                            KeyContainerMenu.Items.Add(SubType);
                            KeyContainerMenu.Items.Add(MinVal);
                            KeyContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(KeyContainerMenu);

                        SubMenu KeycardContainerMenu = new SubMenu("Keycard Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Keycard Esp", "Draw Keycard On Contents List", ref Globals.Config.ScavPlayer.KeycardContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.KeycardContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.KeycardContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.KeycardContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.KeycardContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.KeycardContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.KeycardContainer.UseSlotPrice);
                            KeycardContainerMenu.Items.Add(Enable);
                            KeycardContainerMenu.Items.Add(Name);
                            KeycardContainerMenu.Items.Add(Value);
                            KeycardContainerMenu.Items.Add(Type);
                            KeycardContainerMenu.Items.Add(SubType);
                            KeycardContainerMenu.Items.Add(MinVal);
                            KeycardContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(KeycardContainerMenu);

                        SubMenu MedsContainerMenu = new SubMenu("Medical Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Meds Esp", "Draw Medical On Contents List", ref Globals.Config.ScavPlayer.MedsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.MedsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.MedsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.MedsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.MedsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.MedsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.MedsContainer.UseSlotPrice);
                            MedsContainerMenu.Items.Add(Enable);
                            MedsContainerMenu.Items.Add(Name);
                            MedsContainerMenu.Items.Add(Value);
                            MedsContainerMenu.Items.Add(Type);
                            MedsContainerMenu.Items.Add(SubType);
                            MedsContainerMenu.Items.Add(MinVal);
                            MedsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(MedsContainerMenu);
                        SubMenu SpecialItemsContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.ScavPlayer.SpecialItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.SpecialItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.SpecialItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.SpecialItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.SpecialItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.SpecialItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.SpecialItemsContainer.UseSlotPrice);
                            SpecialItemsContainerMenu.Items.Add(Enable);
                            SpecialItemsContainerMenu.Items.Add(Name);
                            SpecialItemsContainerMenu.Items.Add(Value);
                            SpecialItemsContainerMenu.Items.Add(Type);
                            SpecialItemsContainerMenu.Items.Add(SubType);
                            SpecialItemsContainerMenu.Items.Add(MinVal);
                            SpecialItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(SpecialItemsContainerMenu);

                        SubMenu WeaponContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.ScavPlayer.WeaponContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.ScavPlayer.WeaponContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.ScavPlayer.WeaponContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.ScavPlayer.WeaponContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.ScavPlayer.WeaponContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.ScavPlayer.WeaponContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.ScavPlayer.WeaponContainer.UseSlotPrice);
                            WeaponContainerMenu.Items.Add(Enable);
                            WeaponContainerMenu.Items.Add(Name);
                            WeaponContainerMenu.Items.Add(Value);
                            WeaponContainerMenu.Items.Add(Type);
                            WeaponContainerMenu.Items.Add(SubType);
                            WeaponContainerMenu.Items.Add(MinVal);
                            WeaponContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ScavPlayerItemsMenu.Items.Add(WeaponContainerMenu);

                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerItemsMenu);
                    SubMenu ScavPlayerRadar = new SubMenu("Scav Player Radar Options", "Edit Scav Player Radar Settings");
                    {
                        Toggle enable = new Toggle("Enable", "Toggles On Scav Player Radar", ref Globals.Config.ScavPlayerRadar.Enable);
                        Toggle name = new Toggle("Name", "Display Name On Radar", ref Globals.Config.ScavPlayerRadar.Name);
                        Toggle distance = new Toggle("Distance", "Display Distance On Radar", ref Globals.Config.ScavPlayerRadar.Distance);
                        Toggle weapon = new Toggle("Weapon", "Display Weapon On Radar", ref Globals.Config.ScavPlayerRadar.Weapon);
                        IntSlider fontsize = new IntSlider("Font Size", "Radar Font Size", ref Globals.Config.ScavPlayerRadar.FontSize, 0, 14, 1);
                        IntSlider mindist = new IntSlider("Minimum Text Distance", "Ceases Drawing Radar Text Under This Distance", ref Globals.Config.ScavPlayerRadar.MinimumTextDistance, 0, 1200, 25);
                        IntSlider maxdist = new IntSlider("Maxiumum Radar Draw Distance", "Ceases To Draw Over This Distance", ref Globals.Config.ScavPlayerRadar.MaximumTextDistance, 0, 1200, 25);

                        ScavPlayerRadar.Items.Add(enable);
                        ScavPlayerRadar.Items.Add(name);
                        ScavPlayerRadar.Items.Add(distance);
                        ScavPlayerRadar.Items.Add(weapon);
                        ScavPlayerRadar.Items.Add(fontsize);
                        ScavPlayerRadar.Items.Add(mindist);
                        ScavPlayerRadar.Items.Add(maxdist);

                    }
                    ScavPlayerEspMenu.Items.Add(ScavPlayerRadar);
                    OpacityTypeSlider ScavOpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.ScavPlayer.Opacity);
                    ScavPlayerEspMenu.Items.Add(ScavOpacitySlider);
                    EspMenu.Items.Add(ScavPlayerEspMenu);
                }
                SubMenu BossEspMenu = new SubMenu("Boss Esp", "Visuals For Bosss");
                {
                    Toggle BossEnable = new Toggle("Enable Boss Esp", "Allows Boss Esp To Draw", ref Globals.Config.Boss.Enable);
                    IntSlider BossMaxDistance = new IntSlider("Boss Esp Max Distance", "The Distance Boss Esp Ceases To Draw", ref Globals.Config.Boss.MaxDistance, 0, 2000, 25);
                    Toggle BossVisCheck = new Toggle("Boss Esp Text VisChecks", "Change Text Colour Based On Visibility", ref Globals.Config.Boss.TextVisibilityCheck);
                    Toggle Battlemode = new Toggle("Enable In BattleMode", "", ref Globals.Config.Boss.EnableEspInBattleMode);
                    BossEspMenu.Items.Add(BossEnable);
                    BossEspMenu.Items.Add(BossMaxDistance);
                    BossEspMenu.Items.Add(BossVisCheck);
                    BossEspMenu.Items.Add(Battlemode);
                    SubMenu ChamsMenu = new SubMenu("Chams", "Configure Boss Chams");
                    {
                        Toggle Enable = new Toggle("Enable", "Draws Boss Chams", ref Globals.Config.Boss.Chams);
                        Toggle ApplyToGear = new Toggle("Apply To Gear", "Applys Chams To Gear", ref Globals.Config.Boss.ApplyChamsToGear);
                        ChamTypeSlider ChamType = new ChamTypeSlider("Cham Type", "Different Cham Varient", ref Globals.Config.Boss.ChamsType);
                        ChamsMenu.Items.Add(Enable);
                        ChamsMenu.Items.Add(ApplyToGear);
                        ChamsMenu.Items.Add(ChamType);
                    }
                    BossEspMenu.Items.Add(ChamsMenu);
                    SubMenu BossTextMenu = new SubMenu("Boss Name", "Configure Boss Name Esp");
                    {
                        Toggle BossTextMenuEnable = new Toggle("Enable Name Esp", "Draws Boss Tag", ref Globals.Config.Boss.Name.Enable);
                        TextAlignmentSlider BossTextMenuAlignment = new TextAlignmentSlider("Boss Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.Name.Alignment);
                        IntSlider BossTextMenuLine = new IntSlider("Boss Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Boss.Name.Line, 1, 3, 1);
                        BossTextMenu.Items.Add(BossTextMenuEnable);
                        BossTextMenu.Items.Add(BossTextMenuAlignment);
                        BossTextMenu.Items.Add(BossTextMenuLine);
                    }
                    BossEspMenu.Items.Add(BossTextMenu);
                    SubMenu BossDistanceMenu = new SubMenu("Boss Distance Esp", "Configure Boss Distance Esp");
                    {
                        Toggle BossDistanceMenuEnable = new Toggle("Enable Distance Esp", "Draws Boss Distance", ref Globals.Config.Boss.Distance.Enable);
                        TextAlignmentSlider BossDistanceMenuAlignment = new TextAlignmentSlider("Boss Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.Distance.Alignment);
                        IntSlider BossDistanceMenuLine = new IntSlider("Boss Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Boss.Distance.Line, 1, 3, 1);
                        BossDistanceMenu.Items.Add(BossDistanceMenuEnable);
                        BossDistanceMenu.Items.Add(BossDistanceMenuAlignment);
                        BossDistanceMenu.Items.Add(BossDistanceMenuLine);
                    }
                    BossEspMenu.Items.Add(BossDistanceMenu);
                    SubMenu BossValueMenu = new SubMenu("Boss Value Esp", "Configure Boss Value Esp");
                    {
                        Toggle BossValueMenuEnable = new Toggle("Enable Value Esp", "Draws Boss Value", ref Globals.Config.Boss.Value.Enable);
                        TextAlignmentSlider BossValueMenuAlignment = new TextAlignmentSlider("Boss Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.Value.Alignment);
                        IntSlider BossValueMenuLine = new IntSlider("Boss Value Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Boss.Value.Line, 1, 3, 1);
                        Toggle BossValueSkipAttachments = new Toggle("Skip Attachments", "Doesn't Add Attachments To Value", ref Globals.Config.Boss.SkipAttachmentsInValue);
                        BossValueMenu.Items.Add(BossValueMenuEnable);
                        BossValueMenu.Items.Add(BossValueMenuAlignment);
                        BossValueMenu.Items.Add(BossValueMenuLine);
                        BossValueMenu.Items.Add(BossValueSkipAttachments);
                    }
                    BossEspMenu.Items.Add(BossValueMenu);
                    SubMenu BossSkeletonMenu = new SubMenu("Skeleton Esp", "Configure Boss Skeleton");
                    {
                        Toggle BossSkeletonEnable = new Toggle("Enable Skeleton Esp", "Draws Boss Skeleton", ref Globals.Config.Boss.Skeleton.Enable);
                        SkeletonTypeSlider BossSkeletonType = new SkeletonTypeSlider("Skeleton Type", "Choose If The Skeleton Uses Visibility Or Health", ref Globals.Config.Boss.Skeleton.Type);
                        IntSlider BossSkeletonMaxDistance = new IntSlider("Skeleton Max Distance", "Max Distance That Skeletons Draw At", ref Globals.Config.Boss.Skeleton.MaxDistance, 0, 850, 25);
                        BossSkeletonMenu.Items.Add(BossSkeletonEnable);
                        BossSkeletonMenu.Items.Add(BossSkeletonType);
                        BossSkeletonMenu.Items.Add(BossSkeletonMaxDistance);
                    }
                    BossEspMenu.Items.Add(BossSkeletonMenu);
                    SubMenu BossWeaponMenu = new SubMenu("Boss Weapon Esp", "Configure Boss Weapon Esp");
                    {
                        Toggle BossWeaponMenuEnable = new Toggle("Enable Weapon Esp", "Boss Player Weapon", ref Globals.Config.Boss.Weapon.Enable);
                        TextAlignmentSlider BossWeaponeMenuAlignment = new TextAlignmentSlider("Boss Weapon Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.Weapon.Alignment);
                        IntSlider BossWeaponMenuLine = new IntSlider("Boss Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Boss.Weapon.Line, 1, 3, 1);
                        BossWeaponMenu.Items.Add(BossWeaponMenuEnable);
                        BossWeaponMenu.Items.Add(BossWeaponeMenuAlignment);
                        BossWeaponMenu.Items.Add(BossWeaponMenuLine);
                    }
                    BossEspMenu.Items.Add(BossWeaponMenu);

                    SubMenu BossAmmoMenu = new SubMenu("Boss Ammo Esp", "Configure Boss Weapon Esp");
                    {
                        Toggle BossAmmoMenuEnable = new Toggle("Enable Ammo Esp", "Draws Boss Ammo", ref Globals.Config.Boss.Ammo.Enable);
                        TextAlignmentSlider BossAmmoMenuAlignment = new TextAlignmentSlider("Boss Ammo Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.Ammo.Alignment);
                        IntSlider BossAmmoMenuLine = new IntSlider("Boss Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Boss.Ammo.Line, 1, 3, 1);
                        BossAmmoMenu.Items.Add(BossAmmoMenuEnable);
                        BossAmmoMenu.Items.Add(BossAmmoMenuAlignment);
                        BossAmmoMenu.Items.Add(BossAmmoMenuLine);
                    }
                    BossEspMenu.Items.Add(BossAmmoMenu);
                    SubMenu BossBoxMenu = new SubMenu("Boss Box Esp", "Configure Boss Weapon Esp");
                    {
                        Toggle BossBoxMenuEnable = new Toggle("Enable Box Esp", "Draws Boss Boxes", ref Globals.Config.Boss.Box.Enable);
                        IntSlider BossBoxMaxDistance = new IntSlider("Box Max Distance", "Max Distance That Boxes Draw At", ref Globals.Config.Boss.Box.MaxDistance, 0, 150, 25);
                        Toggle BossFillBoxMenuEnable = new Toggle("Enable Box Filled Esp", "Draws Filled Boxes", ref Globals.Config.Boss.Box.Fill);
                        Toggle BossFillBoxVisibilityMenuEnable = new Toggle("Enable Filled Box Visibility Esp", "Draws Different Colours For Visible Players", ref Globals.Config.Boss.Box.VisibilityCheck);
                        BossBoxMenu.Items.Add(BossBoxMenuEnable);
                        BossBoxMenu.Items.Add(BossBoxMaxDistance);
                        BossBoxMenu.Items.Add(BossFillBoxMenuEnable);
                        BossBoxMenu.Items.Add(BossFillBoxVisibilityMenuEnable);
                    }
                    BossEspMenu.Items.Add(BossBoxMenu);
                    SubMenu BossHealthbarMenu = new SubMenu("Boss Healthbar Esp", "Configure Boss Weapon Esp");
                    {
                        Toggle BossHealthbarMenuEnable = new Toggle("Enable Healthbar Esp", "Draws Boss Healthbar", ref Globals.Config.Boss.Healthbar.Enable);
                        TextAlignmentSlider BossHealthbarMenuAlignment = new TextAlignmentSlider("Boss Healthbar Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.Healthbar.Alignment);
                        IntSlider BossHealthbarMaxDistance = new IntSlider("Healthbar Max Distance", "Max Distance That Healthbar Draw At", ref Globals.Config.Boss.Healthbar.MaxDistance, 0, 850, 25);
                        BossHealthbarMenu.Items.Add(BossHealthbarMenuEnable);
                        BossHealthbarMenu.Items.Add(BossHealthbarMenuAlignment);
                        BossHealthbarMenu.Items.Add(BossHealthbarMaxDistance);
                    }
                    BossEspMenu.Items.Add(BossHealthbarMenu);
                    SubMenu BossItemsMenu = new SubMenu("Boss Contents Esp", "Configure Boss Contents Esp");
                    {
                        Toggle BossContentsEnable = new Toggle("Enable Contents Esp", "Draws Boss Distance", ref Globals.Config.Boss.ContentsList);
                        TextAlignmentSlider BossContentsAlignment = new TextAlignmentSlider("Boss Contents Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Boss.ContentListAlignment);
                        Keybind BossContentsLine = new Keybind("Boss Contents Key", "Key To Toggle On And Off Contents", ref Globals.Config.Boss.ContentsListKey);
                        BossItemsMenu.Items.Add(BossContentsEnable);
                        BossItemsMenu.Items.Add(BossContentsAlignment);
                        BossItemsMenu.Items.Add(BossContentsLine);

                        SubMenu WhitelistContainerMenu = new SubMenu("Whitelist Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Whitelist Esp", "Draw Whitelist On Contents List", ref Globals.Config.Boss.WhitelistContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.WhitelistContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.WhitelistContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.WhitelistContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.WhitelistContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.WhitelistContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.WhitelistContainer.UseSlotPrice);
                            WhitelistContainerMenu.Items.Add(Enable);
                            WhitelistContainerMenu.Items.Add(Name);
                            WhitelistContainerMenu.Items.Add(Value);
                            WhitelistContainerMenu.Items.Add(Type);
                            WhitelistContainerMenu.Items.Add(SubType);
                            WhitelistContainerMenu.Items.Add(MinVal);
                            WhitelistContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(WhitelistContainerMenu);

                        SubMenu AmmoContainerMenu = new SubMenu("Ammo Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Ammo Esp", "Draw Ammo On Contents List", ref Globals.Config.Boss.AmmoContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.AmmoContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.AmmoContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.AmmoContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.AmmoContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.AmmoContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.AmmoContainer.UseSlotPrice);
                            AmmoContainerMenu.Items.Add(Enable);
                            AmmoContainerMenu.Items.Add(Name);
                            AmmoContainerMenu.Items.Add(Value);
                            AmmoContainerMenu.Items.Add(Type);
                            AmmoContainerMenu.Items.Add(SubType);
                            AmmoContainerMenu.Items.Add(MinVal);
                            AmmoContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(AmmoContainerMenu);
                        SubMenu AttachmentsContainerMenu = new SubMenu("Attachments Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Attachments Esp", "Draw Attachments On Contents List", ref Globals.Config.Boss.AttachmentsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.AttachmentsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.AttachmentsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.AttachmentsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.AttachmentsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.AttachmentsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.AttachmentsContainer.UseSlotPrice);
                            AttachmentsContainerMenu.Items.Add(Enable);
                            AttachmentsContainerMenu.Items.Add(Name);
                            AttachmentsContainerMenu.Items.Add(Value);
                            AttachmentsContainerMenu.Items.Add(Type);
                            AttachmentsContainerMenu.Items.Add(SubType);
                            AttachmentsContainerMenu.Items.Add(MinVal);
                            AttachmentsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(AttachmentsContainerMenu);
                        SubMenu BackpacksContainerMenu = new SubMenu("Backpacks Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Backpacks Esp", "Draw Backpacks On Contents List", ref Globals.Config.Boss.BackpacksContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.BackpacksContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.BackpacksContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.BackpacksContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.BackpacksContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.BackpacksContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.BackpacksContainer.UseSlotPrice);
                            BackpacksContainerMenu.Items.Add(Enable);
                            BackpacksContainerMenu.Items.Add(Name);
                            BackpacksContainerMenu.Items.Add(Value);
                            BackpacksContainerMenu.Items.Add(Type);
                            BackpacksContainerMenu.Items.Add(SubType);
                            BackpacksContainerMenu.Items.Add(MinVal);
                            BackpacksContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(BackpacksContainerMenu);

                        SubMenu BarterItemsContainerMenu = new SubMenu("Barter Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable BarterItems Esp", "Draw BarterItems On Contents List", ref Globals.Config.Boss.BarterItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.BarterItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.BarterItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.BarterItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.BarterItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.BarterItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.BarterItemsContainer.UseSlotPrice);
                            BarterItemsContainerMenu.Items.Add(Enable);
                            BarterItemsContainerMenu.Items.Add(Name);
                            BarterItemsContainerMenu.Items.Add(Value);
                            BarterItemsContainerMenu.Items.Add(Type);
                            BarterItemsContainerMenu.Items.Add(SubType);
                            BarterItemsContainerMenu.Items.Add(MinVal);
                            BarterItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(BarterItemsContainerMenu);
                        SubMenu CasesContainerMenu = new SubMenu("Case Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Cases Esp", "Draw Cases On Contents List", ref Globals.Config.Boss.CasesContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.CasesContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.CasesContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.CasesContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.CasesContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.CasesContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.CasesContainer.UseSlotPrice);
                            CasesContainerMenu.Items.Add(Enable);
                            CasesContainerMenu.Items.Add(Name);
                            CasesContainerMenu.Items.Add(Value);
                            CasesContainerMenu.Items.Add(Type);
                            CasesContainerMenu.Items.Add(SubType);
                            CasesContainerMenu.Items.Add(MinVal);
                            CasesContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(CasesContainerMenu);
                        SubMenu ClothingContainerMenu = new SubMenu("Clothing Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Clothing Esp", "Draw Clothing On Contents List", ref Globals.Config.Boss.ClothingContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.ClothingContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.ClothingContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.ClothingContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.ClothingContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.ClothingContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.ClothingContainer.UseSlotPrice);
                            ClothingContainerMenu.Items.Add(Enable);
                            ClothingContainerMenu.Items.Add(Name);
                            ClothingContainerMenu.Items.Add(Value);
                            ClothingContainerMenu.Items.Add(Type);
                            ClothingContainerMenu.Items.Add(SubType);
                            ClothingContainerMenu.Items.Add(MinVal);
                            ClothingContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(ClothingContainerMenu);

                        SubMenu FoodDrinkContainerMenu = new SubMenu("Food And Drink Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable FoodDrink Esp", "Draw Food And Drink On Contents List", ref Globals.Config.Boss.FoodDrinkContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.FoodDrinkContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.FoodDrinkContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.FoodDrinkContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.FoodDrinkContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.FoodDrinkContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.FoodDrinkContainer.UseSlotPrice);
                            FoodDrinkContainerMenu.Items.Add(Enable);
                            FoodDrinkContainerMenu.Items.Add(Name);
                            FoodDrinkContainerMenu.Items.Add(Value);
                            FoodDrinkContainerMenu.Items.Add(Type);
                            FoodDrinkContainerMenu.Items.Add(SubType);
                            FoodDrinkContainerMenu.Items.Add(MinVal);
                            FoodDrinkContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(FoodDrinkContainerMenu);

                        SubMenu FuelContainerMenu = new SubMenu("Fuel Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Fuel Esp", "Draw Fuel On Contents List", ref Globals.Config.Boss.FuelContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.FuelContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.FuelContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.FuelContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.FuelContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.FuelContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.FuelContainer.UseSlotPrice);
                            FuelContainerMenu.Items.Add(Enable);
                            FuelContainerMenu.Items.Add(Name);
                            FuelContainerMenu.Items.Add(Value);
                            FuelContainerMenu.Items.Add(Type);
                            FuelContainerMenu.Items.Add(SubType);
                            FuelContainerMenu.Items.Add(MinVal);
                            FuelContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(FuelContainerMenu);

                        SubMenu KeyContainerMenu = new SubMenu("Key Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Key Esp", "Draw Key On Contents List", ref Globals.Config.Boss.KeyContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.KeyContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.KeyContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.KeyContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.KeyContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.KeyContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.KeyContainer.UseSlotPrice);
                            KeyContainerMenu.Items.Add(Enable);
                            KeyContainerMenu.Items.Add(Name);
                            KeyContainerMenu.Items.Add(Value);
                            KeyContainerMenu.Items.Add(Type);
                            KeyContainerMenu.Items.Add(SubType);
                            KeyContainerMenu.Items.Add(MinVal);
                            KeyContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(KeyContainerMenu);

                        SubMenu KeycardContainerMenu = new SubMenu("Keycard Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Keycard Esp", "Draw Keycard On Contents List", ref Globals.Config.Boss.KeycardContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.KeycardContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.KeycardContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.KeycardContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.KeycardContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.KeycardContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.KeycardContainer.UseSlotPrice);
                            KeycardContainerMenu.Items.Add(Enable);
                            KeycardContainerMenu.Items.Add(Name);
                            KeycardContainerMenu.Items.Add(Value);
                            KeycardContainerMenu.Items.Add(Type);
                            KeycardContainerMenu.Items.Add(SubType);
                            KeycardContainerMenu.Items.Add(MinVal);
                            KeycardContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(KeycardContainerMenu);

                        SubMenu MedsContainerMenu = new SubMenu("Medical Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Meds Esp", "Draw Medical On Contents List", ref Globals.Config.Boss.MedsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.MedsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.MedsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.MedsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.MedsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.MedsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.MedsContainer.UseSlotPrice);
                            MedsContainerMenu.Items.Add(Enable);
                            MedsContainerMenu.Items.Add(Name);
                            MedsContainerMenu.Items.Add(Value);
                            MedsContainerMenu.Items.Add(Type);
                            MedsContainerMenu.Items.Add(SubType);
                            MedsContainerMenu.Items.Add(MinVal);
                            MedsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(MedsContainerMenu);
                        SubMenu SpecialItemsContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Boss.SpecialItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.SpecialItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.SpecialItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.SpecialItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.SpecialItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.SpecialItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.SpecialItemsContainer.UseSlotPrice);
                            SpecialItemsContainerMenu.Items.Add(Enable);
                            SpecialItemsContainerMenu.Items.Add(Name);
                            SpecialItemsContainerMenu.Items.Add(Value);
                            SpecialItemsContainerMenu.Items.Add(Type);
                            SpecialItemsContainerMenu.Items.Add(SubType);
                            SpecialItemsContainerMenu.Items.Add(MinVal);
                            SpecialItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(SpecialItemsContainerMenu);

                        SubMenu WeaponContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Boss.WeaponContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Boss.WeaponContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Boss.WeaponContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Boss.WeaponContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Boss.WeaponContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Boss.WeaponContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Boss.WeaponContainer.UseSlotPrice);
                            WeaponContainerMenu.Items.Add(Enable);
                            WeaponContainerMenu.Items.Add(Name);
                            WeaponContainerMenu.Items.Add(Value);
                            WeaponContainerMenu.Items.Add(Type);
                            WeaponContainerMenu.Items.Add(SubType);
                            WeaponContainerMenu.Items.Add(MinVal);
                            WeaponContainerMenu.Items.Add(UseSlotPrice);
                        }
                        BossItemsMenu.Items.Add(WeaponContainerMenu);

                    }
                    BossEspMenu.Items.Add(BossItemsMenu);
                    SubMenu BossRadar = new SubMenu("Boss Radar Options", "Edit Boss Radar Settings");
                    {
                        Toggle enable = new Toggle("Enable", "Toggles On Boss Radar", ref Globals.Config.BossRadar.Enable);
                        Toggle name = new Toggle("Name", "Display Name On Radar", ref Globals.Config.BossRadar.Name);
                        Toggle distance = new Toggle("Distance", "Display Distance On Radar", ref Globals.Config.BossRadar.Distance);
                        Toggle weapon = new Toggle("Weapon", "Display Weapon On Radar", ref Globals.Config.BossRadar.Weapon);
                        IntSlider fontsize = new IntSlider("Font Size", "Radar Font Size", ref Globals.Config.BossRadar.FontSize, 0, 14, 1);
                        IntSlider mindist = new IntSlider("Minimum Text Distance", "Ceases Drawing Radar Text Under This Distance", ref Globals.Config.BossRadar.MinimumTextDistance, 0, 1200, 25);
                        IntSlider maxdist = new IntSlider("Maxiumum Radar Draw Distance", "Ceases To Draw Over This Distance", ref Globals.Config.BossRadar.MaximumTextDistance, 0, 1200, 25);

                        BossRadar.Items.Add(enable);
                        BossRadar.Items.Add(name);
                        BossRadar.Items.Add(distance);
                        BossRadar.Items.Add(weapon);
                        BossRadar.Items.Add(fontsize);
                        BossRadar.Items.Add(mindist);
                        BossRadar.Items.Add(maxdist);

                    }
                    BossEspMenu.Items.Add(BossRadar);
                    OpacityTypeSlider ScavOpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.ScavPlayer.Opacity);
                    BossEspMenu.Items.Add(ScavOpacitySlider);
                    EspMenu.Items.Add(BossEspMenu);
                }
                SubMenu GrenadeEspMenu = new SubMenu("Grenade Esp", "Visuals For Grenades");
                {
                    Toggle GrenadeEnable = new Toggle("Enable Grenade Esp", "Allows Grenade Esp To Draw", ref Globals.Config.Grenade.Enable);
                    IntSlider GrenadeMaxDistance = new IntSlider("Grenade Esp Max Distance", "The Distance Grenade Esp Ceases To Draw", ref Globals.Config.Grenade.MaxDistance, 0, 2000, 25);
                    Toggle Battlemode = new Toggle("Enable Grenade Esp In Battlemode", "Allows Grenade Esp To Draw In Battlemode", ref Globals.Config.Grenade.EnableEspInBattleMode);
                    GrenadeEspMenu.Items.Add(GrenadeEnable);
                    GrenadeEspMenu.Items.Add(GrenadeMaxDistance);
                    GrenadeEspMenu.Items.Add(Battlemode);
                    SubMenu GrenadeNameMenu = new SubMenu("Grenade Name", "Configure Grenade Name Esp");
                    {
                        Toggle NameEnable = new Toggle("Enable Name Esp", "Draws Grenade Name", ref Globals.Config.Grenade.Name.Enable);
                        TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Grenade Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Grenade.Name.Alignment);
                        IntSlider NameLine = new IntSlider("Grenade Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Grenade.Name.Line, 1, 3, 1);
                        GrenadeNameMenu.Items.Add(NameEnable);
                        GrenadeNameMenu.Items.Add(NameAlignment);
                        GrenadeNameMenu.Items.Add(NameLine);
                    }
                    GrenadeEspMenu.Items.Add(GrenadeNameMenu);

                    SubMenu GrenadeDistanceMenu = new SubMenu("Grenade Distance", "Configure Grenade Distance Esp");
                    {
                        Toggle DistanceEnable = new Toggle("Enable Distance Esp", "Draws Grenade Distance", ref Globals.Config.Grenade.Distance.Enable);
                        TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Grenade Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Grenade.Distance.Alignment);
                        IntSlider DistanceLine = new IntSlider("Grenade Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Grenade.Distance.Line, 1, 3, 1);
                        GrenadeDistanceMenu.Items.Add(DistanceEnable);
                        GrenadeDistanceMenu.Items.Add(DistanceAlignment);
                        GrenadeDistanceMenu.Items.Add(DistanceLine);
                    }
                    GrenadeEspMenu.Items.Add(GrenadeDistanceMenu);

                    SubMenu GrenadeThrowerNameMenu = new SubMenu("Grenade Thrower Name", "Configure Grenade Thrower Name Esp");
                    {
                        Toggle ThrowerNameEnable = new Toggle("Enable Thrower Name Esp", "Draws Grenade Thrower Name", ref Globals.Config.Grenade.ThrowerName.Enable);
                        TextAlignmentSlider ThrowerNameAlignment = new TextAlignmentSlider("Grenade Thrower Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Grenade.ThrowerName.Alignment);
                        IntSlider ThrowerNameLine = new IntSlider("Grenade Esp Line", "The Line Offset The Esp Is Drawn At", ref Globals.Config.Grenade.ThrowerName.Line, 1, 3, 1);
                        GrenadeThrowerNameMenu.Items.Add(ThrowerNameEnable);
                        GrenadeThrowerNameMenu.Items.Add(ThrowerNameAlignment);
                        GrenadeThrowerNameMenu.Items.Add(ThrowerNameLine);
                    }
                    GrenadeEspMenu.Items.Add(GrenadeThrowerNameMenu);

                    SubMenu GrenadeDurationMenu = new SubMenu("Grenade Duration Bar", "Configure Grenade Duration Bar Esp");
                    {
                        Toggle GrenadeDurationEnable = new Toggle("Enable Duration Bar Esp", "Draws Grenade Duration", ref Globals.Config.Grenade.Durationbar.Enable);
                        TextAlignmentSlider GrenadeDurationAlignment = new TextAlignmentSlider("Grenade Duration Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Grenade.Durationbar.Alignment);
                        IntSlider GrenadeDurationMaxDistance = new IntSlider("Grenade Duration Bar Max Distance", "The Distance The Duration Bar Ceases To Draw", ref Globals.Config.Grenade.Durationbar.MaxDistance, 0, 1000, 25);
                        GrenadeDurationMenu.Items.Add(GrenadeDurationEnable);
                        GrenadeDurationMenu.Items.Add(GrenadeDurationAlignment);
                        GrenadeDurationMenu.Items.Add(GrenadeDurationMaxDistance);
                    }
                    GrenadeEspMenu.Items.Add(GrenadeDurationMenu);
                    Toggle GrenadeCullOpacity = new Toggle("Cull Opacity With Distance", "Lowers Opacity The Further You Are From The Grenade", ref Globals.Config.Grenade.CullOpacityWithDistance);
                    GrenadeEspMenu.Items.Add(GrenadeCullOpacity);
                    EspMenu.Items.Add(GrenadeEspMenu);
                }
                SubMenu ItemEspMenu = new SubMenu("Item Esp", "Visuals For Items");
                {
                    SubMenu AddWhitelistMenu = new SubMenu("Add Items To Whitelist", "Menu For Adding And Removing Whitelisted Items");
                    {
                        SubMenu Add = new SubMenu("Add", "Add Items To Whitelist");
                        {
                            TextField WhitelistTextbox = new TextField("Item Template ID", "Enter Item Template ID", ref WhitelistString);
                            Button CopyWhitelistIDFromClipboard = new Button("Copy Template ID From Clipboard", "Copies The Template From Your Clipboard", () => { WhitelistString = GUIUtility.systemCopyBuffer; WhitelistTextbox.Value = WhitelistString; });
                            Button AddToWhitelist = new Button("Add Item", "Adds Item To Whitelist, Reload Config To See It.", () => { WhitelistString = WhitelistTextbox.Value; if (Helpers.ItemPriceHelper.list.ContainsKey(WhitelistString)) { Globals.AlteringWhitelist = true; Globals.Config.Items.WhitelistedItems.Add(WhitelistString); } Globals.AlteringWhitelist = false; });
                            Add.Items.Add(WhitelistTextbox);
                            Add.Items.Add(CopyWhitelistIDFromClipboard);
                            Add.Items.Add(AddToWhitelist);
                            AddWhitelistMenu.Items.Add(Add);
                        }
                        SubMenu Remove = new SubMenu("Remove", "Remove Items From Whitelist");
                        {
                            foreach (string item in Globals.Config.Items.WhitelistedItems)
                            {
                                SubMenu menu = new SubMenu(Helpers.ItemPriceHelper.list[item].name, "");
                                {
                                    Button RemoveFromWhitelist = new Button("Remove Item", "Removes Item From Whitelist, Reload Config To See Changes.", () => { if (Helpers.ItemPriceHelper.list.ContainsKey(item)) { Globals.AlteringWhitelist = true; Globals.Config.Items.WhitelistedItems.Remove(item); } Globals.AlteringWhitelist = false; });
                                    menu.Items.Add(RemoveFromWhitelist);
                                }
                                Remove.Items.Add(menu);
                            }
                            AddWhitelistMenu.Items.Add(Remove);
                        }
                        ItemEspMenu.Items.Add(AddWhitelistMenu);
                    }

                    SubMenu AddBlacklistMenu = new SubMenu("Add Items To Blacklist", "Menu For Adding And Removing Blacklisted Items");
                    {
                        SubMenu Add = new SubMenu("Add", "Add Items To Blacklist");
                        {
                            TextField BlacklistTextbox = new TextField("Item Template ID", "Enter Item Template ID", ref BlacklistString);
                            Button CopyBlacklistIDFromClipboard = new Button("Copy Template ID From Clipboard", "Copies The Template From Your Clipboard", () => { BlacklistString = GUIUtility.systemCopyBuffer; BlacklistTextbox.Value = BlacklistString; });
                            Button AddToBlacklist = new Button("Add Item", "Adds Item To Blacklist, Reload Config To See It.", () => { BlacklistString = BlacklistTextbox.Value; if (Helpers.ItemPriceHelper.list.ContainsKey(BlacklistString)) { Globals.AlteringWhitelist = true; Globals.Config.Items.BlacklistedItems.Add(BlacklistString); } Globals.AlteringWhitelist = false; });
                            Add.Items.Add(BlacklistTextbox);
                            Add.Items.Add(CopyBlacklistIDFromClipboard);
                            Add.Items.Add(AddToBlacklist);
                            AddBlacklistMenu.Items.Add(Add);
                        }
                        SubMenu Remove = new SubMenu("Remove", "Remove Items From Blacklist");
                        {
                            foreach (string item in Globals.Config.Items.WhitelistedItems)
                            {
                                SubMenu menu = new SubMenu(Helpers.ItemPriceHelper.list[item].name, "");
                                {
                                    Button RemoveFromBlacklist = new Button("Remove Item", "Removes Item From Blacklist, Reload Config To See Changes.", () => { if (Helpers.ItemPriceHelper.list.ContainsKey(item)) { Globals.AlteringWhitelist = true; Globals.Config.Items.BlacklistedItems.Remove(item); } Globals.AlteringWhitelist = false; });
                                    menu.Items.Add(RemoveFromBlacklist);
                                }
                                Remove.Items.Add(menu);
                            }
                            AddBlacklistMenu.Items.Add(Remove);
                        }
                        ItemEspMenu.Items.Add(AddBlacklistMenu);
                    }
                    SubMenu WhitelistMenu = new SubMenu("Whitelisted Items", "Settings For Whitelisted Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Whitelist.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Whitelist.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Whitelist.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Whitelist.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Whitelist.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Whitelist.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Whitelist.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Whitelist.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Whitelist.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Whitelist.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Whitelist.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Whitelist.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Whitelist.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Whitelist.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Whitelist.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Whitelist.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Whitelist.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Whitelist.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Whitelist.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Whitelist.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Whitelist.CullOpacity);
                        WhitelistMenu.Items.Add(Enable);
                        WhitelistMenu.Items.Add(EnableBattlemode);
                        WhitelistMenu.Items.Add(Name);
                        WhitelistMenu.Items.Add(Distance);
                        WhitelistMenu.Items.Add(MaxDistance);
                        WhitelistMenu.Items.Add(ValueSwitch);
                        WhitelistMenu.Items.Add(MinimumValue);
                        WhitelistMenu.Items.Add(Value);
                        WhitelistMenu.Items.Add(ItemType);
                        WhitelistMenu.Items.Add(ItemSubType);
                        WhitelistMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(WhitelistMenu);
                    }
                    SubMenu AmmoMenu = new SubMenu("Ammo Items", "Settings For Ammo Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Ammo.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Ammo.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Ammo.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Ammo.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Ammo.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Ammo.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Ammo.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Ammo.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Ammo.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Ammo.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Ammo.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Ammo.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Ammo.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Ammo.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Ammo.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Ammo.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Ammo.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Ammo.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Ammo.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Ammo.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Ammo.CullOpacity);
                        AmmoMenu.Items.Add(Enable);
                        AmmoMenu.Items.Add(EnableBattlemode);
                        AmmoMenu.Items.Add(Name);
                        AmmoMenu.Items.Add(Distance);
                        AmmoMenu.Items.Add(MaxDistance);
                        AmmoMenu.Items.Add(ValueSwitch);
                        AmmoMenu.Items.Add(MinimumValue);
                        AmmoMenu.Items.Add(Value);
                        AmmoMenu.Items.Add(ItemType);
                        AmmoMenu.Items.Add(ItemSubType);
                        AmmoMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(AmmoMenu);
                    }
                    SubMenu ArmourMenu = new SubMenu("Armour Items", "Settings For Armour Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Armour.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Armour.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Armour.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Armour.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Armour.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Armour.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Armour.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Armour.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Armour.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Armour.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Armour.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Armour.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Armour.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Armour.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Armour.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Armour.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Armour.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Armour.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Armour.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Armour.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Armour.CullOpacity);
                        ArmourMenu.Items.Add(Enable);
                        ArmourMenu.Items.Add(EnableBattlemode);
                        ArmourMenu.Items.Add(Name);
                        ArmourMenu.Items.Add(Distance);
                        ArmourMenu.Items.Add(MaxDistance);
                        ArmourMenu.Items.Add(ValueSwitch);
                        ArmourMenu.Items.Add(MinimumValue);
                        ArmourMenu.Items.Add(Value);
                        ArmourMenu.Items.Add(ItemType);
                        ArmourMenu.Items.Add(ItemSubType);
                        ArmourMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(ArmourMenu);
                    }
                    SubMenu AttachmenuMenu = new SubMenu("Attachment Items", "Settings For Attachment Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Attachments.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Attachments.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Attachments.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Attachments.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Attachments.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Attachments.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Attachments.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Attachments.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Attachments.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Attachments.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Attachments.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Attachments.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Attachments.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Attachments.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Attachments.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Attachments.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Attachments.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Attachments.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Attachments.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Attachments.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Attachments.CullOpacity);
                        AttachmenuMenu.Items.Add(Enable);
                        AttachmenuMenu.Items.Add(EnableBattlemode);
                        AttachmenuMenu.Items.Add(Name);
                        AttachmenuMenu.Items.Add(Distance);
                        AttachmenuMenu.Items.Add(MaxDistance);
                        AttachmenuMenu.Items.Add(ValueSwitch);
                        AttachmenuMenu.Items.Add(MinimumValue);
                        AttachmenuMenu.Items.Add(Value);
                        AttachmenuMenu.Items.Add(ItemType);
                        AttachmenuMenu.Items.Add(ItemSubType);
                        AttachmenuMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(AttachmenuMenu);
                    }
                    SubMenu BackpacksMenu = new SubMenu("Backpack Items", "Settings For Backpack Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Backpacks.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Backpacks.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Backpacks.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Backpacks.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Backpacks.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Backpacks.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Backpacks.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Backpacks.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Backpacks.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Backpacks.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Backpacks.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Backpacks.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Backpacks.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Backpacks.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Backpacks.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Backpacks.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Backpacks.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Backpacks.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Backpacks.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Backpacks.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Backpacks.CullOpacity);
                        BackpacksMenu.Items.Add(Enable);
                        BackpacksMenu.Items.Add(EnableBattlemode);
                        BackpacksMenu.Items.Add(Name);
                        BackpacksMenu.Items.Add(Distance);
                        BackpacksMenu.Items.Add(MaxDistance);
                        BackpacksMenu.Items.Add(ValueSwitch);
                        BackpacksMenu.Items.Add(MinimumValue);
                        BackpacksMenu.Items.Add(Value);
                        BackpacksMenu.Items.Add(ItemType);
                        BackpacksMenu.Items.Add(ItemSubType);
                        BackpacksMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(BackpacksMenu);
                    }
                    SubMenu BarterMenu = new SubMenu("Barter Items", "Settings For Barter Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.BarterItems.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.BarterItems.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.BarterItems.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.BarterItems.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.BarterItems.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.BarterItems.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.BarterItems.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.BarterItems.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.BarterItems.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.BarterItems.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.BarterItems.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.BarterItems.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.BarterItems.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.BarterItems.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.BarterItems.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.BarterItems.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.BarterItems.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.BarterItems.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.BarterItems.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.BarterItems.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.BarterItems.CullOpacity);
                        BarterMenu.Items.Add(Enable);
                        BarterMenu.Items.Add(EnableBattlemode);
                        BarterMenu.Items.Add(Name);
                        BarterMenu.Items.Add(Distance);
                        BarterMenu.Items.Add(MaxDistance);
                        BarterMenu.Items.Add(ValueSwitch);
                        BarterMenu.Items.Add(MinimumValue);
                        BarterMenu.Items.Add(Value);
                        BarterMenu.Items.Add(ItemType);
                        BarterMenu.Items.Add(ItemSubType);
                        BarterMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(BarterMenu);
                    }
                    SubMenu CaseMenu = new SubMenu("Case Items", "Settings For Case Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Cases.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Cases.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Cases.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Cases.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Cases.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Cases.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Cases.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Cases.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Cases.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Cases.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Cases.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Cases.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Cases.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Cases.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Cases.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Cases.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Cases.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Cases.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Cases.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Cases.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Cases.CullOpacity);
                        CaseMenu.Items.Add(Enable);
                        CaseMenu.Items.Add(EnableBattlemode);
                        CaseMenu.Items.Add(Name);
                        CaseMenu.Items.Add(Distance);
                        CaseMenu.Items.Add(MaxDistance);
                        CaseMenu.Items.Add(ValueSwitch);
                        CaseMenu.Items.Add(MinimumValue);
                        CaseMenu.Items.Add(Value);
                        CaseMenu.Items.Add(ItemType);
                        CaseMenu.Items.Add(ItemSubType);
                        CaseMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(CaseMenu);
                    }
                    SubMenu ClothingMenu = new SubMenu("Clothing Items", "Settings For Clothing Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Clothing.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Clothing.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Clothing.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Clothing.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Clothing.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Clothing.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Clothing.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Clothing.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Clothing.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Clothing.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Clothing.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Clothing.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Clothing.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Clothing.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Clothing.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Clothing.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Clothing.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Clothing.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Clothing.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Clothing.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Clothing.CullOpacity);
                        ClothingMenu.Items.Add(Enable);
                        ClothingMenu.Items.Add(EnableBattlemode);
                        ClothingMenu.Items.Add(Name);
                        ClothingMenu.Items.Add(Distance);
                        ClothingMenu.Items.Add(MaxDistance);
                        ClothingMenu.Items.Add(ValueSwitch);
                        ClothingMenu.Items.Add(MinimumValue);
                        ClothingMenu.Items.Add(Value);
                        ClothingMenu.Items.Add(ItemType);
                        ClothingMenu.Items.Add(ItemSubType);
                        ClothingMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(ClothingMenu);
                    }
                    SubMenu FoodDrinkMenu = new SubMenu("Food/Drink Items", "Settings For Food/Drink Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.FoodDrink.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.FoodDrink.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.FoodDrink.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.FoodDrink.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.FoodDrink.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.FoodDrink.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.FoodDrink.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.FoodDrink.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.FoodDrink.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.FoodDrink.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.FoodDrink.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.FoodDrink.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.FoodDrink.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.FoodDrink.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.FoodDrink.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.FoodDrink.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.FoodDrink.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.FoodDrink.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.FoodDrink.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.FoodDrink.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.FoodDrink.CullOpacity);
                        FoodDrinkMenu.Items.Add(Enable);
                        FoodDrinkMenu.Items.Add(EnableBattlemode);
                        FoodDrinkMenu.Items.Add(Name);
                        FoodDrinkMenu.Items.Add(Distance);
                        FoodDrinkMenu.Items.Add(MaxDistance);
                        FoodDrinkMenu.Items.Add(ValueSwitch);
                        FoodDrinkMenu.Items.Add(MinimumValue);
                        FoodDrinkMenu.Items.Add(Value);
                        FoodDrinkMenu.Items.Add(ItemType);
                        FoodDrinkMenu.Items.Add(ItemSubType);
                        FoodDrinkMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(FoodDrinkMenu);
                    }
                    SubMenu FuelMenu = new SubMenu("Fuel Items", "Settings For Fuel Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Fuel.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Fuel.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Fuel.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Fuel.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Fuel.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Fuel.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Fuel.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Fuel.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Fuel.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Fuel.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Fuel.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Fuel.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Fuel.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Fuel.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Fuel.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Fuel.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Fuel.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Fuel.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Fuel.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Fuel.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Fuel.CullOpacity);
                        FuelMenu.Items.Add(Enable);
                        FuelMenu.Items.Add(EnableBattlemode);
                        FuelMenu.Items.Add(Name);
                        FuelMenu.Items.Add(Distance);
                        FuelMenu.Items.Add(MaxDistance);
                        FuelMenu.Items.Add(ValueSwitch);
                        FuelMenu.Items.Add(MinimumValue);
                        FuelMenu.Items.Add(Value);
                        FuelMenu.Items.Add(ItemType);
                        FuelMenu.Items.Add(ItemSubType);
                        FuelMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(FuelMenu);
                    }
                    SubMenu KeyMenu = new SubMenu("Key Items", "Settings For Key Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Key.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Key.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Key.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Key.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Key.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Key.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Key.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Key.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Key.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Key.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Key.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Key.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Key.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Key.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Key.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Key.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Key.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Key.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Key.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Key.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Key.CullOpacity);
                        KeyMenu.Items.Add(Enable);
                        KeyMenu.Items.Add(EnableBattlemode);
                        KeyMenu.Items.Add(Name);
                        KeyMenu.Items.Add(Distance);
                        KeyMenu.Items.Add(MaxDistance);
                        KeyMenu.Items.Add(ValueSwitch);
                        KeyMenu.Items.Add(MinimumValue);
                        KeyMenu.Items.Add(Value);
                        KeyMenu.Items.Add(ItemType);
                        KeyMenu.Items.Add(ItemSubType);
                        KeyMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(KeyMenu);
                    }
                    SubMenu KeycardMenu = new SubMenu("Keycard Items", "Settings For Keycard Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Keycard.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Keycard.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Keycard.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Keycard.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Keycard.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Keycard.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Keycard.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Keycard.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Keycard.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Keycard.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Keycard.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Keycard.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Keycard.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Keycard.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Keycard.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Keycard.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Keycard.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Keycard.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Keycard.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Keycard.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Keycard.CullOpacity);
                        KeycardMenu.Items.Add(Enable);
                        KeycardMenu.Items.Add(EnableBattlemode);
                        KeycardMenu.Items.Add(Name);
                        KeycardMenu.Items.Add(Distance);
                        KeycardMenu.Items.Add(MaxDistance);
                        KeycardMenu.Items.Add(ValueSwitch);
                        KeycardMenu.Items.Add(MinimumValue);
                        KeycardMenu.Items.Add(Value);
                        KeycardMenu.Items.Add(ItemType);
                        KeycardMenu.Items.Add(ItemSubType);
                        KeycardMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(KeycardMenu);
                    }
                    SubMenu MedMenu = new SubMenu("Medical Items", "Settings For Medical Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Meds.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Meds.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Meds.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Meds.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Meds.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Meds.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Meds.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Meds.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Meds.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Meds.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Meds.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Meds.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Meds.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Meds.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Meds.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Meds.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Meds.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Meds.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Meds.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Meds.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Meds.CullOpacity);
                        MedMenu.Items.Add(Enable);
                        MedMenu.Items.Add(EnableBattlemode);
                        MedMenu.Items.Add(Name);
                        MedMenu.Items.Add(Distance);
                        MedMenu.Items.Add(MaxDistance);
                        MedMenu.Items.Add(ValueSwitch);
                        MedMenu.Items.Add(MinimumValue);
                        MedMenu.Items.Add(Value);
                        MedMenu.Items.Add(ItemType);
                        MedMenu.Items.Add(ItemSubType);
                        MedMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(MedMenu);
                    }
                    SubMenu QuestMenu = new SubMenu("Quest Items", "Settings For Quest Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.QuestItems.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.QuestItems.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.QuestItems.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.QuestItems.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.QuestItems.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.QuestItems.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.QuestItems.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.QuestItems.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }

                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.QuestItems.MaxDistance, 0, 2000, 25);
                        Toggle ShowInactive = new Toggle("Show None Required Quest Items", "Shows Quest Items You Don't Currently Need", ref Globals.Config.Items.QuestItems.ShowInactiveQuestItems);
                        Toggle Opacity = new Toggle("Cull Opacity With Distance", "Removes Opacity Of Colour With Distance From Item", ref Globals.Config.Items.QuestItems.CullOpacityWithDistance);
                        QuestMenu.Items.Add(Enable);
                        QuestMenu.Items.Add(EnableBattlemode);
                        QuestMenu.Items.Add(Name);
                        QuestMenu.Items.Add(Distance);
                        QuestMenu.Items.Add(MaxDistance);
                        QuestMenu.Items.Add(ShowInactive);
                        QuestMenu.Items.Add(Opacity);
                        ItemEspMenu.Items.Add(QuestMenu);
                    }
                    SubMenu SpecialMenu = new SubMenu("Special Items", "Settings For Special Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.SpecialItems.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.SpecialItems.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.SpecialItems.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.SpecialItems.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.SpecialItems.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.SpecialItems.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.SpecialItems.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.SpecialItems.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.SpecialItems.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.SpecialItems.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.SpecialItems.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.SpecialItems.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.SpecialItems.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.SpecialItems.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.SpecialItems.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.SpecialItems.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.SpecialItems.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.SpecialItems.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.SpecialItems.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.SpecialItems.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.SpecialItems.CullOpacity);
                        SpecialMenu.Items.Add(Enable);
                        SpecialMenu.Items.Add(EnableBattlemode);
                        SpecialMenu.Items.Add(Name);
                        SpecialMenu.Items.Add(Distance);
                        SpecialMenu.Items.Add(MaxDistance);
                        SpecialMenu.Items.Add(ValueSwitch);
                        SpecialMenu.Items.Add(MinimumValue);
                        SpecialMenu.Items.Add(Value);
                        SpecialMenu.Items.Add(ItemType);
                        SpecialMenu.Items.Add(ItemSubType);
                        SpecialMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(SpecialMenu);

                    }
                    SubMenu WeaponMenu = new SubMenu("Weapon Items", "Settings For Weapon Items");
                    {
                        Toggle Enable = new Toggle("Enable", "Turns On Whitelisted Item Esp", ref Globals.Config.Items.Weapon.Enable);
                        Toggle EnableBattlemode = new Toggle("Enable In BattleMode", "Keeps This Item Esp Enabled While BattleMode Is Active", ref Globals.Config.Items.Weapon.EnableEspInBattleMode);
                        SubMenu Name = new SubMenu("Name", "Item Name Esp");
                        {
                            Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Items.Weapon.Name.Enable);
                            TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Weapon.Name.Alignment);
                            IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Items.Weapon.Name.Line, 1, 3, 1);
                            Name.Items.Add(EnableName);
                            Name.Items.Add(NameAlignment);
                            Name.Items.Add(NamePosition);
                        }
                        SubMenu Distance = new SubMenu("Distance", "Item Distance Esp");
                        {
                            Toggle EnableDistance = new Toggle("Enable Distance", "Turns On Distance Esp", ref Globals.Config.Items.Weapon.Distance.Enable);
                            TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Distance Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Weapon.Distance.Alignment);
                            IntSlider DistancePosition = new IntSlider("Distance Line", "Name Line", ref Globals.Config.Items.Weapon.Distance.Line, 1, 3, 1);
                            Distance.Items.Add(EnableDistance);
                            Distance.Items.Add(DistanceAlignment);
                            Distance.Items.Add(DistancePosition);
                        }
                        IntSlider MaxDistance = new IntSlider("Maximum Distance", "Max Distance Items Cease To Be Drawn At", ref Globals.Config.Items.Weapon.MaxDistance, 0, 2000, 25);
                        Toggle ValueSwitch = new Toggle("Use Slot Price", "Uses Slot Price As Value Instead Of Actual Value", ref Globals.Config.Items.Weapon.UseItemSlotPrice);
                        IntSlider MinimumValue = new IntSlider("Minimum Value", "Minimum Value For Item To Be Shown", ref Globals.Config.Items.Weapon.MinPrice, 0, 500000, 1000);
                        SubMenu Value = new SubMenu("Value", "Item Value Esp");
                        {
                            Toggle EnableValue = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Weapon.Value.Enable);
                            TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Weapon.Value.Alignment);
                            IntSlider ValuePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Weapon.Value.Line, 1, 3, 1);
                            Value.Items.Add(EnableValue);
                            Value.Items.Add(ValueAlignment);
                            Value.Items.Add(ValuePosition);
                        }
                        SubMenu ItemType = new SubMenu("Item Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Weapon.ItemType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Weapon.ItemType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Weapon.ItemType.Line, 1, 3, 1);
                            ItemType.Items.Add(EnableType);
                            ItemType.Items.Add(TypeAlignment);
                            ItemType.Items.Add(TypePosition);
                        }
                        SubMenu ItemSubType = new SubMenu("Item Sub Type", "Item Type Esp");
                        {
                            Toggle EnableType = new Toggle("Enable Value", "Turns On Value Esp", ref Globals.Config.Items.Weapon.ItemSubType.Enable);
                            TextAlignmentSlider TypeAlignment = new TextAlignmentSlider("Value Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Items.Weapon.ItemSubType.Alignment);
                            IntSlider TypePosition = new IntSlider("Value Line", "Name Line", ref Globals.Config.Items.Weapon.ItemSubType.Line, 1, 3, 1);
                            ItemSubType.Items.Add(EnableType);
                            ItemSubType.Items.Add(TypeAlignment);
                            ItemSubType.Items.Add(TypePosition);
                        }
                        OpacityTypeSlider OpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Items.Weapon.CullOpacity);
                        WeaponMenu.Items.Add(Enable);
                        WeaponMenu.Items.Add(EnableBattlemode);
                        WeaponMenu.Items.Add(Name);
                        WeaponMenu.Items.Add(Distance);
                        WeaponMenu.Items.Add(MaxDistance);
                        WeaponMenu.Items.Add(ValueSwitch);
                        WeaponMenu.Items.Add(MinimumValue);
                        WeaponMenu.Items.Add(Value);
                        WeaponMenu.Items.Add(ItemType);
                        WeaponMenu.Items.Add(ItemSubType);
                        WeaponMenu.Items.Add(OpacitySlider);
                        ItemEspMenu.Items.Add(WeaponMenu);

                    }
                    EspMenu.Items.Add(ItemEspMenu);
                }
                SubMenu ContainerEspMenu = new SubMenu("Container Esp", "Visuals For Containers");
                {
                    Toggle ContainerEnable = new Toggle("Enable", "Enables Container Esp", ref Globals.Config.Container.Enable);
                    IntSlider ContainerMaxDistance = new IntSlider("Max Distance", "Distance Containers Cease To Draw At", ref Globals.Config.Container.MaxDistance, 0, 2000, 25);
                    IntSlider ContainerMinValue = new IntSlider("Minimum Value", "Prevents Containers Being Drawn Below This Value", ref Globals.Config.Container.MinValue, 0, 1000000, 1000);
                    Toggle Battlemode = new Toggle("Enable Container Esp In Battlemode", "Allows Container Esp To Draw In Battlemode", ref Globals.Config.Container.EnableEspInBattleMode);
                    SubMenu ContainerName = new SubMenu("Name Esp", "Configure Container Name Esp");
                    {
                        Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Container.Name.Enable);
                        TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Container.Name.Alignment);
                        IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Container.Name.Line, 1, 3, 1);
                        ContainerName.Items.Add(EnableName);
                        ContainerName.Items.Add(NameAlignment);
                        ContainerName.Items.Add(NamePosition);

                    }
                    SubMenu ContainerDistance = new SubMenu("Distance Esp", "Configure Container Distance Esp");
                    {
                        Toggle EnableDistance = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Container.Distance.Enable);
                        TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Container.Distance.Alignment);
                        IntSlider DistancePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Container.Distance.Line, 1, 3, 1);
                        ContainerDistance.Items.Add(EnableDistance);
                        ContainerDistance.Items.Add(DistanceAlignment);
                        ContainerDistance.Items.Add(DistancePosition);

                    }
                    SubMenu ContainerValue = new SubMenu("Value Esp", "Configure Container Value Esp");
                    {
                        Toggle EnablValue = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Container.Value.Enable);
                        TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Container.Value.Alignment);
                        IntSlider ValuePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Container.Value.Line, 1, 3, 1);
                        ContainerValue.Items.Add(EnablValue);
                        ContainerValue.Items.Add(ValueAlignment);
                        ContainerValue.Items.Add(ValuePosition);

                    }
                    SubMenu ContainerList = new SubMenu("Container List", "Configure The Container Item List");
                    {
                        Toggle EnableList = new Toggle("Enable Contents List", "Enables Container Item List Esp", ref Globals.Config.Container.ContentsList);
                        TextAlignmentSlider ListAlignment = new TextAlignmentSlider("Contents List Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Container.ContainerAlignment);
                        Keybind ListKey = new Keybind("Container Contents Key", "Key To Toggle On And Off Contents", ref Globals.Config.Container.ContainerKey);

                        ContainerList.Items.Add(EnableList);
                        ContainerList.Items.Add(ListAlignment);
                        ContainerList.Items.Add(ListKey);

                        SubMenu WhitelistContainerMenu = new SubMenu("Whitelist Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Whitelist Esp", "Draw Whitelist On Contents List", ref Globals.Config.Container.WhitelistContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.WhitelistContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.WhitelistContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.WhitelistContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.WhitelistContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.WhitelistContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.WhitelistContainer.UseSlotPrice);
                            Toggle Override = new Toggle("Override Container", "Overrides Original Container Max Distance And Min Value If It Contains A Whitelisted Item", ref Globals.Config.Container.OverrideWithWhitelist);
                            WhitelistContainerMenu.Items.Add(Enable);
                            WhitelistContainerMenu.Items.Add(Name);
                            WhitelistContainerMenu.Items.Add(Value);
                            WhitelistContainerMenu.Items.Add(Type);
                            WhitelistContainerMenu.Items.Add(SubType);
                            WhitelistContainerMenu.Items.Add(MinVal);
                            WhitelistContainerMenu.Items.Add(UseSlotPrice);
                            WhitelistContainerMenu.Items.Add(Override);
                        }
                        ContainerList.Items.Add(WhitelistContainerMenu);

                        SubMenu AmmoContainerMenu = new SubMenu("Ammo Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Ammo Esp", "Draw Ammo On Contents List", ref Globals.Config.Container.AmmoContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.AmmoContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.AmmoContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.AmmoContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.AmmoContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.AmmoContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.AmmoContainer.UseSlotPrice);
                            AmmoContainerMenu.Items.Add(Enable);
                            AmmoContainerMenu.Items.Add(Name);
                            AmmoContainerMenu.Items.Add(Value);
                            AmmoContainerMenu.Items.Add(Type);
                            AmmoContainerMenu.Items.Add(SubType);
                            AmmoContainerMenu.Items.Add(MinVal);
                            AmmoContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(AmmoContainerMenu);
                        SubMenu AttachmentsContainerMenu = new SubMenu("Attachments Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Attachments Esp", "Draw Attachments On Contents List", ref Globals.Config.Container.AttachmentsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.AttachmentsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.AttachmentsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.AttachmentsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.AttachmentsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.AttachmentsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.AttachmentsContainer.UseSlotPrice);
                            AttachmentsContainerMenu.Items.Add(Enable);
                            AttachmentsContainerMenu.Items.Add(Name);
                            AttachmentsContainerMenu.Items.Add(Value);
                            AttachmentsContainerMenu.Items.Add(Type);
                            AttachmentsContainerMenu.Items.Add(SubType);
                            AttachmentsContainerMenu.Items.Add(MinVal);
                            AttachmentsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(AttachmentsContainerMenu);
                        SubMenu BackpacksContainerMenu = new SubMenu("Backpacks Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Backpacks Esp", "Draw Backpacks On Contents List", ref Globals.Config.Container.BackpacksContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.BackpacksContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.BackpacksContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.BackpacksContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.BackpacksContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.BackpacksContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.BackpacksContainer.UseSlotPrice);
                            BackpacksContainerMenu.Items.Add(Enable);
                            BackpacksContainerMenu.Items.Add(Name);
                            BackpacksContainerMenu.Items.Add(Value);
                            BackpacksContainerMenu.Items.Add(Type);
                            BackpacksContainerMenu.Items.Add(SubType);
                            BackpacksContainerMenu.Items.Add(MinVal);
                            BackpacksContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(BackpacksContainerMenu);

                        SubMenu BarterItemsContainerMenu = new SubMenu("Barter Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable BarterItems Esp", "Draw BarterItems On Contents List", ref Globals.Config.Container.BarterItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.BarterItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.BarterItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.BarterItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.BarterItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.BarterItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.BarterItemsContainer.UseSlotPrice);
                            BarterItemsContainerMenu.Items.Add(Enable);
                            BarterItemsContainerMenu.Items.Add(Name);
                            BarterItemsContainerMenu.Items.Add(Value);
                            BarterItemsContainerMenu.Items.Add(Type);
                            BarterItemsContainerMenu.Items.Add(SubType);
                            BarterItemsContainerMenu.Items.Add(MinVal);
                            BarterItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(BarterItemsContainerMenu);
                        SubMenu CasesContainerMenu = new SubMenu("Case Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Cases Esp", "Draw Cases On Contents List", ref Globals.Config.Container.CasesContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.CasesContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.CasesContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.CasesContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.CasesContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.CasesContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.CasesContainer.UseSlotPrice);
                            CasesContainerMenu.Items.Add(Enable);
                            CasesContainerMenu.Items.Add(Name);
                            CasesContainerMenu.Items.Add(Value);
                            CasesContainerMenu.Items.Add(Type);
                            CasesContainerMenu.Items.Add(SubType);
                            CasesContainerMenu.Items.Add(MinVal);
                            CasesContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(CasesContainerMenu);
                        SubMenu ClothingContainerMenu = new SubMenu("Clothing Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Clothing Esp", "Draw Clothing On Contents List", ref Globals.Config.Container.ClothingContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.ClothingContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.ClothingContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.ClothingContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.ClothingContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.ClothingContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.ClothingContainer.UseSlotPrice);
                            ClothingContainerMenu.Items.Add(Enable);
                            ClothingContainerMenu.Items.Add(Name);
                            ClothingContainerMenu.Items.Add(Value);
                            ClothingContainerMenu.Items.Add(Type);
                            ClothingContainerMenu.Items.Add(SubType);
                            ClothingContainerMenu.Items.Add(MinVal);
                            ClothingContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(ClothingContainerMenu);

                        SubMenu FoodDrinkContainerMenu = new SubMenu("Food And Drink Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable FoodDrink Esp", "Draw Food And Drink On Contents List", ref Globals.Config.Container.FoodDrinkContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.FoodDrinkContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.FoodDrinkContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.FoodDrinkContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.FoodDrinkContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.FoodDrinkContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.FoodDrinkContainer.UseSlotPrice);
                            FoodDrinkContainerMenu.Items.Add(Enable);
                            FoodDrinkContainerMenu.Items.Add(Name);
                            FoodDrinkContainerMenu.Items.Add(Value);
                            FoodDrinkContainerMenu.Items.Add(Type);
                            FoodDrinkContainerMenu.Items.Add(SubType);
                            FoodDrinkContainerMenu.Items.Add(MinVal);
                            FoodDrinkContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(FoodDrinkContainerMenu);

                        SubMenu FuelContainerMenu = new SubMenu("Fuel Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Fuel Esp", "Draw Fuel On Contents List", ref Globals.Config.Container.FuelContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.FuelContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.FuelContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.FuelContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.FuelContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.FuelContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.FuelContainer.UseSlotPrice);
                            FuelContainerMenu.Items.Add(Enable);
                            FuelContainerMenu.Items.Add(Name);
                            FuelContainerMenu.Items.Add(Value);
                            FuelContainerMenu.Items.Add(Type);
                            FuelContainerMenu.Items.Add(SubType);
                            FuelContainerMenu.Items.Add(MinVal);
                            FuelContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(FuelContainerMenu);

                        SubMenu KeyContainerMenu = new SubMenu("Key Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Key Esp", "Draw Key On Contents List", ref Globals.Config.Container.KeyContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.KeyContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.KeyContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.KeyContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.KeyContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.KeyContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.KeyContainer.UseSlotPrice);
                            KeyContainerMenu.Items.Add(Enable);
                            KeyContainerMenu.Items.Add(Name);
                            KeyContainerMenu.Items.Add(Value);
                            KeyContainerMenu.Items.Add(Type);
                            KeyContainerMenu.Items.Add(SubType);
                            KeyContainerMenu.Items.Add(MinVal);
                            KeyContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(KeyContainerMenu);

                        SubMenu KeycardContainerMenu = new SubMenu("Keycard Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Keycard Esp", "Draw Keycard On Contents List", ref Globals.Config.Container.KeycardContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.KeycardContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.KeycardContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.KeycardContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.KeycardContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.KeycardContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.KeycardContainer.UseSlotPrice);
                            KeycardContainerMenu.Items.Add(Enable);
                            KeycardContainerMenu.Items.Add(Name);
                            KeycardContainerMenu.Items.Add(Value);
                            KeycardContainerMenu.Items.Add(Type);
                            KeycardContainerMenu.Items.Add(SubType);
                            KeycardContainerMenu.Items.Add(MinVal);
                            KeycardContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(KeycardContainerMenu);

                        SubMenu MedsContainerMenu = new SubMenu("Medical Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Meds Esp", "Draw Medical On Contents List", ref Globals.Config.Container.MedsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.MedsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.MedsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.MedsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.MedsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.MedsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.MedsContainer.UseSlotPrice);
                            MedsContainerMenu.Items.Add(Enable);
                            MedsContainerMenu.Items.Add(Name);
                            MedsContainerMenu.Items.Add(Value);
                            MedsContainerMenu.Items.Add(Type);
                            MedsContainerMenu.Items.Add(SubType);
                            MedsContainerMenu.Items.Add(MinVal);
                            MedsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(MedsContainerMenu);
                        SubMenu QuestContainerMenu = new SubMenu("Quest Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Quest Item Esp", "Draw Quest Items On Contents List", ref Globals.Config.Container.QuestItemContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.QuestItemContainer.Name);
                            Toggle Override = new Toggle("Override Container", "Overrides The Original Container Max Distance And Minimum Value For Quest Items", ref Globals.Config.Container.OverrideWithQuestItems);
                            QuestContainerMenu.Items.Add(Enable);
                            QuestContainerMenu.Items.Add(Name);
                            QuestContainerMenu.Items.Add(Override);
                        }
                        ContainerList.Items.Add(QuestContainerMenu);
                        SubMenu SpecialItemsContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Container.SpecialItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.SpecialItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.SpecialItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.SpecialItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.SpecialItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.SpecialItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.SpecialItemsContainer.UseSlotPrice);
                            SpecialItemsContainerMenu.Items.Add(Enable);
                            SpecialItemsContainerMenu.Items.Add(Name);
                            SpecialItemsContainerMenu.Items.Add(Value);
                            SpecialItemsContainerMenu.Items.Add(Type);
                            SpecialItemsContainerMenu.Items.Add(SubType);
                            SpecialItemsContainerMenu.Items.Add(MinVal);
                            SpecialItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(SpecialItemsContainerMenu);

                        SubMenu WeaponContainerMenu = new SubMenu("Weapon Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Weapon Esp", "Draw Weapon On Contents List", ref Globals.Config.Container.WeaponContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Container.WeaponContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Container.WeaponContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Container.WeaponContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Container.WeaponContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Container.WeaponContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Container.WeaponContainer.UseSlotPrice);
                            WeaponContainerMenu.Items.Add(Enable);
                            WeaponContainerMenu.Items.Add(Name);
                            WeaponContainerMenu.Items.Add(Value);
                            WeaponContainerMenu.Items.Add(Type);
                            WeaponContainerMenu.Items.Add(SubType);
                            WeaponContainerMenu.Items.Add(MinVal);
                            WeaponContainerMenu.Items.Add(UseSlotPrice);
                        }
                        ContainerList.Items.Add(WeaponContainerMenu);
                    }
                    OpacityTypeSlider ContainerOpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Container.Opacity);
                    ContainerEspMenu.Items.Add(ContainerEnable);
                    ContainerEspMenu.Items.Add(ContainerMaxDistance);
                    ContainerEspMenu.Items.Add(ContainerMinValue);
                    ContainerEspMenu.Items.Add(Battlemode);
                    ContainerEspMenu.Items.Add(ContainerName);
                    ContainerEspMenu.Items.Add(ContainerDistance);
                    ContainerEspMenu.Items.Add(ContainerValue);
                    ContainerEspMenu.Items.Add(ContainerList);
                    ContainerEspMenu.Items.Add(ContainerOpacitySlider);
                    EspMenu.Items.Add(ContainerEspMenu);
                }
                SubMenu CorpseEspMenu = new SubMenu("Corpse Esp", "Visuals For Corpse");
                {
                    Toggle CorpseEnable = new Toggle("Enable", "Enables Container Esp", ref Globals.Config.Corpse.Enable);
                    IntSlider CorpseMaxDistance = new IntSlider("Max Distance", "Distance Containers Cease To Draw At", ref Globals.Config.Corpse.MaxDistance, 0, 2000, 25);
                    IntSlider CorpseMinValue = new IntSlider("Minimum Value", "Prevents Containers Being Drawn Below This Value", ref Globals.Config.Corpse.MinValue, 0, 1000000, 1000);
                    Toggle Battlemode = new Toggle("Enable Container Esp In Battlemode", "Allows Container Esp To Draw In Battlemode", ref Globals.Config.Corpse.EnableEspInBattleMode);
                    SubMenu CorpseName = new SubMenu("Name Esp", "Configure Corpse Name Esp");
                    {
                        Toggle EnableName = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Corpse.Name.Enable);
                        TextAlignmentSlider NameAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Corpse.Name.Alignment);
                        IntSlider NamePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Corpse.Name.Line, 1, 3, 1);
                        CorpseName.Items.Add(EnableName);
                        CorpseName.Items.Add(NameAlignment);
                        CorpseName.Items.Add(NamePosition);

                    }
                    SubMenu CorpseDistance = new SubMenu("Distance Esp", "Configure Corpse Distance");
                    {
                        Toggle EnableDistance = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Corpse.Distance.Enable);
                        TextAlignmentSlider DistanceAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Corpse.Distance.Alignment);
                        IntSlider DistancePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Corpse.Distance.Line, 1, 3, 1);
                        CorpseDistance.Items.Add(EnableDistance);
                        CorpseDistance.Items.Add(DistanceAlignment);
                        CorpseDistance.Items.Add(DistancePosition);

                    }
                    SubMenu CorpseValue = new SubMenu("Value Esp", "Configure Corpse Value");
                    {
                        Toggle EnablValue = new Toggle("Enable Name", "Turns On Name Esp", ref Globals.Config.Corpse.Value.Enable);
                        TextAlignmentSlider ValueAlignment = new TextAlignmentSlider("Name Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Corpse.Value.Alignment);
                        IntSlider ValuePosition = new IntSlider("Name Line", "Name Line", ref Globals.Config.Corpse.Value.Line, 1, 3, 1);
                        CorpseValue.Items.Add(EnablValue);
                        CorpseValue.Items.Add(ValueAlignment);
                        CorpseValue.Items.Add(ValuePosition);

                    }
                    SubMenu CorpseList = new SubMenu("Container List", "Configure The Container Item List");
                    {
                        Toggle EnableList = new Toggle("Enable Contents List", "Enables Container Item List Esp", ref Globals.Config.Corpse.ContentsList);
                        TextAlignmentSlider ListAlignment = new TextAlignmentSlider("Contents List Alignment", "Aligns Text Either: Top, Bottom, Right Or Left", ref Globals.Config.Corpse.ContainerAlignment);
                        Keybind ListKey = new Keybind("Corpse Contents Key", "Key To Toggle On And Off Contents", ref Globals.Config.Corpse.ContainerKey);

                        CorpseList.Items.Add(EnableList);
                        CorpseList.Items.Add(ListAlignment);
                        CorpseList.Items.Add(ListKey);

                        SubMenu WhitelistContainerMenu = new SubMenu("Whitelist Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Whitelist Esp", "Draw Whitelist On Contents List", ref Globals.Config.Corpse.WhitelistContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.WhitelistContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.WhitelistContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.WhitelistContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.WhitelistContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.WhitelistContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.WhitelistContainer.UseSlotPrice);
                            Toggle Override = new Toggle("Override Corpse", "Overrides Original Corpse Max Distance And Min Value If It Contains A Whitelisted Item", ref Globals.Config.Corpse.OverrideWithWhitelist);
                            WhitelistContainerMenu.Items.Add(Enable);
                            WhitelistContainerMenu.Items.Add(Name);
                            WhitelistContainerMenu.Items.Add(Value);
                            WhitelistContainerMenu.Items.Add(Type);
                            WhitelistContainerMenu.Items.Add(SubType);
                            WhitelistContainerMenu.Items.Add(MinVal);
                            WhitelistContainerMenu.Items.Add(UseSlotPrice);
                            WhitelistContainerMenu.Items.Add(Override);
                        }
                        CorpseList.Items.Add(WhitelistContainerMenu);

                        SubMenu AmmoContainerMenu = new SubMenu("Ammo Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Ammo Esp", "Draw Ammo On Contents List", ref Globals.Config.Corpse.AmmoContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.AmmoContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.AmmoContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.AmmoContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.AmmoContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.AmmoContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.AmmoContainer.UseSlotPrice);
                            AmmoContainerMenu.Items.Add(Enable);
                            AmmoContainerMenu.Items.Add(Name);
                            AmmoContainerMenu.Items.Add(Value);
                            AmmoContainerMenu.Items.Add(Type);
                            AmmoContainerMenu.Items.Add(SubType);
                            AmmoContainerMenu.Items.Add(MinVal);
                            AmmoContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(AmmoContainerMenu);
                        SubMenu AttachmentsContainerMenu = new SubMenu("Attachments Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Attachments Esp", "Draw Attachments On Contents List", ref Globals.Config.Corpse.AttachmentsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.AttachmentsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.AttachmentsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.AttachmentsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.AttachmentsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.AttachmentsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.AttachmentsContainer.UseSlotPrice);
                            AttachmentsContainerMenu.Items.Add(Enable);
                            AttachmentsContainerMenu.Items.Add(Name);
                            AttachmentsContainerMenu.Items.Add(Value);
                            AttachmentsContainerMenu.Items.Add(Type);
                            AttachmentsContainerMenu.Items.Add(SubType);
                            AttachmentsContainerMenu.Items.Add(MinVal);
                            AttachmentsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(AttachmentsContainerMenu);
                        SubMenu BackpacksContainerMenu = new SubMenu("Backpacks Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Backpacks Esp", "Draw Backpacks On Contents List", ref Globals.Config.Corpse.BackpacksContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.BackpacksContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.BackpacksContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.BackpacksContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.BackpacksContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.BackpacksContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.BackpacksContainer.UseSlotPrice);
                            BackpacksContainerMenu.Items.Add(Enable);
                            BackpacksContainerMenu.Items.Add(Name);
                            BackpacksContainerMenu.Items.Add(Value);
                            BackpacksContainerMenu.Items.Add(Type);
                            BackpacksContainerMenu.Items.Add(SubType);
                            BackpacksContainerMenu.Items.Add(MinVal);
                            BackpacksContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(BackpacksContainerMenu);

                        SubMenu BarterItemsContainerMenu = new SubMenu("Barter Items Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable BarterItems Esp", "Draw BarterItems On Contents List", ref Globals.Config.Corpse.BarterItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.BarterItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.BarterItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.BarterItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.BarterItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.BarterItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.BarterItemsContainer.UseSlotPrice);
                            BarterItemsContainerMenu.Items.Add(Enable);
                            BarterItemsContainerMenu.Items.Add(Name);
                            BarterItemsContainerMenu.Items.Add(Value);
                            BarterItemsContainerMenu.Items.Add(Type);
                            BarterItemsContainerMenu.Items.Add(SubType);
                            BarterItemsContainerMenu.Items.Add(MinVal);
                            BarterItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(BarterItemsContainerMenu);
                        SubMenu CasesContainerMenu = new SubMenu("Case Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Cases Esp", "Draw Cases On Contents List", ref Globals.Config.Corpse.CasesContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.CasesContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.CasesContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.CasesContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.CasesContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.CasesContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.CasesContainer.UseSlotPrice);
                            CasesContainerMenu.Items.Add(Enable);
                            CasesContainerMenu.Items.Add(Name);
                            CasesContainerMenu.Items.Add(Value);
                            CasesContainerMenu.Items.Add(Type);
                            CasesContainerMenu.Items.Add(SubType);
                            CasesContainerMenu.Items.Add(MinVal);
                            CasesContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(CasesContainerMenu);
                        SubMenu ClothingContainerMenu = new SubMenu("Clothing Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Clothing Esp", "Draw Clothing On Contents List", ref Globals.Config.Corpse.ClothingContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.ClothingContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.ClothingContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.ClothingContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.ClothingContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.ClothingContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.ClothingContainer.UseSlotPrice);
                            ClothingContainerMenu.Items.Add(Enable);
                            ClothingContainerMenu.Items.Add(Name);
                            ClothingContainerMenu.Items.Add(Value);
                            ClothingContainerMenu.Items.Add(Type);
                            ClothingContainerMenu.Items.Add(SubType);
                            ClothingContainerMenu.Items.Add(MinVal);
                            ClothingContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(ClothingContainerMenu);

                        SubMenu FoodDrinkContainerMenu = new SubMenu("Food And Drink Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable FoodDrink Esp", "Draw Food And Drink On Contents List", ref Globals.Config.Corpse.FoodDrinkContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.FoodDrinkContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.FoodDrinkContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.FoodDrinkContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.FoodDrinkContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.FoodDrinkContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.FoodDrinkContainer.UseSlotPrice);
                            FoodDrinkContainerMenu.Items.Add(Enable);
                            FoodDrinkContainerMenu.Items.Add(Name);
                            FoodDrinkContainerMenu.Items.Add(Value);
                            FoodDrinkContainerMenu.Items.Add(Type);
                            FoodDrinkContainerMenu.Items.Add(SubType);
                            FoodDrinkContainerMenu.Items.Add(MinVal);
                            FoodDrinkContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(FoodDrinkContainerMenu);

                        SubMenu FuelContainerMenu = new SubMenu("Fuel Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Fuel Esp", "Draw Fuel On Contents List", ref Globals.Config.Corpse.FuelContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.FuelContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.FuelContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.FuelContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.FuelContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.FuelContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.FuelContainer.UseSlotPrice);
                            FuelContainerMenu.Items.Add(Enable);
                            FuelContainerMenu.Items.Add(Name);
                            FuelContainerMenu.Items.Add(Value);
                            FuelContainerMenu.Items.Add(Type);
                            FuelContainerMenu.Items.Add(SubType);
                            FuelContainerMenu.Items.Add(MinVal);
                            FuelContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(FuelContainerMenu);

                        SubMenu KeyContainerMenu = new SubMenu("Key Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Key Esp", "Draw Key On Contents List", ref Globals.Config.Corpse.KeyContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.KeyContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.KeyContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.KeyContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.KeyContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.KeyContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.KeyContainer.UseSlotPrice);
                            KeyContainerMenu.Items.Add(Enable);
                            KeyContainerMenu.Items.Add(Name);
                            KeyContainerMenu.Items.Add(Value);
                            KeyContainerMenu.Items.Add(Type);
                            KeyContainerMenu.Items.Add(SubType);
                            KeyContainerMenu.Items.Add(MinVal);
                            KeyContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(KeyContainerMenu);

                        SubMenu KeycardContainerMenu = new SubMenu("Keycard Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Keycard Esp", "Draw Keycard On Contents List", ref Globals.Config.Corpse.KeycardContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.KeycardContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.KeycardContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.KeycardContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.KeycardContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.KeycardContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.KeycardContainer.UseSlotPrice);
                            KeycardContainerMenu.Items.Add(Enable);
                            KeycardContainerMenu.Items.Add(Name);
                            KeycardContainerMenu.Items.Add(Value);
                            KeycardContainerMenu.Items.Add(Type);
                            KeycardContainerMenu.Items.Add(SubType);
                            KeycardContainerMenu.Items.Add(MinVal);
                            KeycardContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(KeycardContainerMenu);

                        SubMenu MedsContainerMenu = new SubMenu("Medical Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Meds Esp", "Draw Medical On Contents List", ref Globals.Config.Corpse.MedsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.MedsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.MedsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.MedsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.MedsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.MedsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.MedsContainer.UseSlotPrice);
                            MedsContainerMenu.Items.Add(Enable);
                            MedsContainerMenu.Items.Add(Name);
                            MedsContainerMenu.Items.Add(Value);
                            MedsContainerMenu.Items.Add(Type);
                            MedsContainerMenu.Items.Add(SubType);
                            MedsContainerMenu.Items.Add(MinVal);
                            MedsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(MedsContainerMenu);
                        SubMenu SpecialItemsContainerMenu = new SubMenu("SpecialItems Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable SpecialItems Esp", "Draw SpecialItems On Contents List", ref Globals.Config.Corpse.SpecialItemsContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.SpecialItemsContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.SpecialItemsContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.SpecialItemsContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.SpecialItemsContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.SpecialItemsContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.SpecialItemsContainer.UseSlotPrice);
                            SpecialItemsContainerMenu.Items.Add(Enable);
                            SpecialItemsContainerMenu.Items.Add(Name);
                            SpecialItemsContainerMenu.Items.Add(Value);
                            SpecialItemsContainerMenu.Items.Add(Type);
                            SpecialItemsContainerMenu.Items.Add(SubType);
                            SpecialItemsContainerMenu.Items.Add(MinVal);
                            SpecialItemsContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(SpecialItemsContainerMenu);

                        SubMenu WeaponContainerMenu = new SubMenu("Weapon Contents Esp", "");
                        {
                            Toggle Enable = new Toggle("Enable Weapon Esp", "Draw Weapon On Contents List", ref Globals.Config.Corpse.WeaponContainer.Enable);
                            Toggle Name = new Toggle("Draw Name", "Draws Item Name", ref Globals.Config.Corpse.WeaponContainer.Name);
                            Toggle Value = new Toggle("Draw Value", "Draws Item Name", ref Globals.Config.Corpse.WeaponContainer.Value);
                            Toggle Type = new Toggle("Draw Type", "Draws Item Name", ref Globals.Config.Corpse.WeaponContainer.Type);
                            Toggle SubType = new Toggle("Draw Sub-Type", "Draws Item Name", ref Globals.Config.Corpse.WeaponContainer.SubType);
                            IntSlider MinVal = new IntSlider("Minimum Value", "Minimum Value For Items To Be Drawn", ref Globals.Config.Corpse.WeaponContainer.MinValue, 0, 1000000, 1000);
                            Toggle UseSlotPrice = new Toggle("Use Price Per Slot", "Makes Entire Value And Contents Value For This Item Category Use Slot Price", ref Globals.Config.Corpse.WeaponContainer.UseSlotPrice);
                            WeaponContainerMenu.Items.Add(Enable);
                            WeaponContainerMenu.Items.Add(Name);
                            WeaponContainerMenu.Items.Add(Value);
                            WeaponContainerMenu.Items.Add(Type);
                            WeaponContainerMenu.Items.Add(SubType);
                            WeaponContainerMenu.Items.Add(MinVal);
                            WeaponContainerMenu.Items.Add(UseSlotPrice);
                        }
                        CorpseList.Items.Add(WeaponContainerMenu);
                    }
                    OpacityTypeSlider ContainerOpacitySlider = new OpacityTypeSlider("Opacity Modes", "Allows You To Change Opacity By Value, By Distance Or Not At All", ref Globals.Config.Corpse.Opacity);
                    CorpseEspMenu.Items.Add(CorpseEnable);
                    CorpseEspMenu.Items.Add(CorpseMaxDistance);
                    CorpseEspMenu.Items.Add(CorpseMinValue);
                    CorpseEspMenu.Items.Add(Battlemode);
                    CorpseEspMenu.Items.Add(CorpseName);
                    CorpseEspMenu.Items.Add(CorpseDistance);
                    CorpseEspMenu.Items.Add(CorpseValue);
                    CorpseEspMenu.Items.Add(CorpseList);
                    CorpseEspMenu.Items.Add(ContainerOpacitySlider);
                    EspMenu.Items.Add(CorpseEspMenu);
                }
                SubMenu LocalPlayerMenu = new SubMenu("Local Player", "Change Visuals For Local Player");
                {
                    SubMenu ChamsMenu = new SubMenu("Chams", "Configure Local Player Chams");
                    {
                        Toggle Enable = new Toggle("Enable", "Draws Hand Chams", ref Globals.Config.LocalPlayer.Chams);
                        Toggle ApplyToGun = new Toggle("Apply To Gun", "Applys Chams To Gun", ref Globals.Config.LocalPlayer.ChamsOnGun);
                        ChamTypeSlider ChamType = new ChamTypeSlider("Cham Type", "Different Cham Varient", ref Globals.Config.LocalPlayer.ChamsType);
                        ChamsMenu.Items.Add(Enable);
                        ChamsMenu.Items.Add(ApplyToGun);
                        ChamsMenu.Items.Add(ChamType);
                    }
                    LocalPlayerMenu.Items.Add(ChamsMenu);
                }
                EspMenu.Items.Add(LocalPlayerMenu);
                Toggle W2SManagement = new Toggle("Calculate Scope And Unscoped At The Same Time", "Turned Off It Wont Draw Esp Outside Of A Scope Zoom, Turned On It Draws Esp That It Currently Outside The Scope But Invisible Because Of The Scope Borders", ref Globals.Config.QualityOfLife.CalculateScopeAndScene);
                EspMenu.Items.Add(W2SManagement);
            }
            SubMenu MiscMenu = new SubMenu("Misc", "Configure Misc");
            {
                SubMenu Weapons = new SubMenu("Weapons", "Change Weapon Stats");
                {
                    SubMenu AssaultRifle = new SubMenu("Assault Rifle", "Assault Rifle Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultAssaultRifle.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider(" Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultAssaultRifle.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider(" Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultAssaultRifle.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultAssaultRifle.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultAssaultRifle.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultAssaultRifle.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultAssaultRifle.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultAssaultRifle.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultAssaultRifle.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultAssaultRifle.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultAssaultRifle.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultAssaultRifle.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(HitSpeed);
                            Default.Items.Add(HitSpeedAmount);
                            AssaultRifle.Items.Add(Default);
                        }
                        foreach (var item in Globals.AssaultRifles)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider(" Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider(" Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            AssaultRifle.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(AssaultRifle);

                    SubMenu AssaultCarbine = new SubMenu("Assault Carbine", "Assault Carbine Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultAssaultCarbine.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultAssaultCarbine.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultAssaultCarbine.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultAssaultCarbine.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultAssaultCarbine.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultAssaultCarbine.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultAssaultCarbine.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultAssaultCarbine.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultAssaultCarbine.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultAssaultCarbine.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultAssaultCarbine.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultAssaultCarbine.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(HitSpeed);
                            Default.Items.Add(HitSpeedAmount);
                            AssaultCarbine.Items.Add(Default);
                        }
                        foreach (var item in Globals.AssaultCarbines)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            AssaultCarbine.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(AssaultCarbine);

                    SubMenu MachineGun = new SubMenu("Machine Gun", "Machine Gun Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultMachineGun.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultMachineGun.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultMachineGun.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultMachineGun.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultMachineGun.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultMachineGun.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultMachineGun.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultMachineGun.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultMachineGun.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultMachineGun.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultMachineGun.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultMachineGun.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            MachineGun.Items.Add(Default);
                        }
                        foreach (var item in Globals.MachineGuns)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            MachineGun.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(MachineGun);

                    SubMenu MarksmanRifle = new SubMenu("Marksman Rifles", "Marksman Rifle Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultMarksmanRifle.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultMarksmanRifle.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultMarksmanRifle.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultMarksmanRifle.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultMarksmanRifle.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultMarksmanRifle.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultMarksmanRifle.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultMarksmanRifle.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultMarksmanRifle.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultMarksmanRifle.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultMarksmanRifle.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultMarksmanRifle.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            MarksmanRifle.Items.Add(Default);
                        }
                        foreach (var item in Globals.MarksmanRifles)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            MarksmanRifle.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(MarksmanRifle);

                    SubMenu Pistols = new SubMenu("Pistols", "Pistol Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultPistol.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultPistol.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultPistol.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultPistol.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultPistol.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultPistol.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultPistol.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultPistol.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultPistol.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultPistol.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultPistol.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultPistol.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Pistols.Items.Add(Default);
                        }
                        foreach (var item in Globals.Pistols)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            Pistols.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(Pistols);

                    SubMenu Revolvers = new SubMenu("Revolvers", "Revolver Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultRevolver.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultRevolver.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultRevolver.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultRevolver.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultRevolver.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultRevolver.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultRevolver.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultRevolver.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultRevolver.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultRevolver.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultRevolver.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultRevolver.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Revolvers.Items.Add(Default);
                        }
                        foreach (var item in Globals.Revolvers)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            Revolvers.Items.Add(menu);

                        }
                    }

                    Weapons.Items.Add(Revolvers);
                    SubMenu Shotgun = new SubMenu("Shotguns", "Shotgun Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultShotgun.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultShotgun.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultShotgun.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultShotgun.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultShotgun.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultShotgun.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultShotgun.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultShotgun.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultShotgun.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultShotgun.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultShotgun.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultShotgun.HitSpeedAmount, 0, 5, 0.25f);

                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Shotgun.Items.Add(Default);
                        }
                        foreach (var item in Globals.Shotguns)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            Shotgun.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(Shotgun);

                    SubMenu SMG = new SubMenu("SMG", "SMG Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultSMG.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultSMG.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultSMG.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultSMG.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultSMG.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultSMG.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultSMG.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultSMG.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultSMG.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultSMG.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultSMG.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultSMG.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            SMG.Items.Add(Default);
                        }
                        foreach (var item in Globals.SMGs)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            SMG.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(SMG);

                    SubMenu SniperRifle = new SubMenu("Sniper Rifles", "Snipers Rifle Weapon Configs");
                    {
                        SubMenu Default = new SubMenu("Default", "Configure Weapon Configs For Entirely If Weapon Is Category That Is Unselected");
                        {
                            Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.DefaultSniperRifle.NoRecoil);
                            IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.DefaultSniperRifle.Recoilx, 0, 100, 5);
                            IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.DefaultSniperRifle.Recoily, 0, 100, 5);
                            Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.DefaultSniperRifle.NoSway);
                            IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.DefaultSniperRifle.SwayAmount, 0, 100, 5);
                            Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.DefaultSniperRifle.UnlockFireModes);
                            Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.DefaultSniperRifle.NoMalfunction);
                            IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.DefaultSniperRifle.MalfunctionChance, 0, 100, 5);
                            Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultSniperRifle.FastFire);
                            IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultSniperRifle.FastFireAmount, 0, 600, 5);
                            Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.DefaultSniperRifle.HitSpeedMultiplier);
                            FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.DefaultSniperRifle.HitSpeedAmount, 0, 5, 0.25f);
                            Default.Items.Add(UnlockFiremodes);
                            Default.Items.Add(NoRecoil);
                            Default.Items.Add(NoRecoilx);
                            Default.Items.Add(NoRecoily);
                            Default.Items.Add(NoSway);
                            Default.Items.Add(NoSwayAmount);
                            Default.Items.Add(NoMalfunction);
                            Default.Items.Add(NoMalfunctionAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            Default.Items.Add(FastFire);
                            Default.Items.Add(FastFireAmount);
                            SniperRifle.Items.Add(Default);
                        }
                        foreach (var item in Globals.SniperRifles)
                        {
                            string name = Helpers.ItemPriceHelper.list[item].name;
                            SubMenu menu = new SubMenu(name, string.Concat("Weapon Config For The ", name, " Weapon"));
                            {
                                Toggle NoRecoil = new Toggle("No Recoil", "Allows Recoil Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoRecoil);
                                IntSlider NoRecoilx = new IntSlider("Recoil X Axis Percentage", "Percentage Of X Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoilx, 0, 100, 5);
                                IntSlider NoRecoily = new IntSlider("Recoil Y Axis Percentage", "Percentage Of Y Axis Recoil", ref Globals.Config.Weapon.WeaponDict[item].Recoily, 0, 100, 5);
                                Toggle NoSway = new Toggle("No Sway", "Allows Sway Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoSway);
                                IntSlider NoSwayAmount = new IntSlider("Sway Percentage", "Percentage Sway", ref Globals.Config.Weapon.WeaponDict[item].SwayAmount, 0, 100, 5);
                                Toggle UnlockFiremodes = new Toggle("Unlock FireModes", "Allows You To Use All FireModes", ref Globals.Config.Weapon.WeaponDict[item].UnlockFireModes);
                                Toggle NoMalfunction = new Toggle("No Malfunction", "Enables Malfunction Modifications", ref Globals.Config.Weapon.WeaponDict[item].NoMalfunction);
                                IntSlider NoMalfunctionAmount = new IntSlider("Malfunction Chance Percentage", "Percentage Decrase Of The Current Malfunction Chance", ref Globals.Config.Weapon.WeaponDict[item].MalfunctionChance, 0, 100, 5);
                                Toggle FastFire = new Toggle("Fast Fire", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFire);
                                IntSlider FastFireAmount = new IntSlider("Fast Fire Percentage Increase", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].FastFireAmount, 0, 600, 5);
                                Toggle HitSpeed = new Toggle("Increased Bullet Speed", "Increases Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedMultiplier);
                                FloatSlider HitSpeedAmount = new FloatSlider("Increased Bullet Speed Multiplier", "Percentage Increase Of The Current Fire Rate", ref Globals.Config.Weapon.WeaponDict[item].HitSpeedAmount, 0, 5, 0.25f);
                                menu.Items.Add(UnlockFiremodes);
                                menu.Items.Add(NoRecoil);
                                menu.Items.Add(NoRecoilx);
                                menu.Items.Add(NoRecoily);
                                menu.Items.Add(NoSway);
                                menu.Items.Add(NoSwayAmount);
                                menu.Items.Add(NoMalfunction);
                                menu.Items.Add(NoMalfunctionAmount);
                                menu.Items.Add(FastFire);
                                menu.Items.Add(FastFireAmount);
                                menu.Items.Add(HitSpeed);
                                menu.Items.Add(HitSpeedAmount);
                            }
                            SniperRifle.Items.Add(menu);

                        }
                    }
                    Weapons.Items.Add(SniperRifle);
                }
                MiscMenu.Items.Add(Weapons);
                SubMenu Movement = new SubMenu("Movement", "Alter Player Movement");
                {
                    SubMenu stamina = new SubMenu("Stamina Menu", "Mess With Stamina Properties");
                    {
                        Toggle unlimstamina = new Toggle("Unlimited Stamina", "Sprint Forever", ref Globals.Config.Movement.UnlimitedStamina);
                        IntSlider time = new IntSlider("Time Till Stamina Refill", "Time Till Stamina Is Refilled", ref Globals.Config.Movement.TimeTillRefill, 0, 120, 1);
                        IntSlider chance = new IntSlider("Chance Of Stamina Refill", "Chance That Stamina Is Filled", ref Globals.Config.Movement.ChanceOfRefill, 0, 100, 2);
                        IntSlider refillamount = new IntSlider("Amount Of Stamina", "The Amount Of Stamina Is Filled", ref Globals.Config.Movement.RefillAmount, 0, 100, 2);

                        stamina.Items.Add(unlimstamina);
                        stamina.Items.Add(time);
                        stamina.Items.Add(chance);
                        stamina.Items.Add(refillamount);
                    }
                    Movement.Items.Add(stamina);
                    Toggle runandshoot = new Toggle("Run And Shoot", "Removes Sprint Block", ref Globals.Config.Movement.RunAndShoot);
                    Toggle noinertia = new Toggle("No Inertia", "Removes Inertia", ref Globals.Config.Movement.NoInertia);
                    Movement.Items.Add(runandshoot);
                    Movement.Items.Add(noinertia);
                }
                MiscMenu.Items.Add(Movement);
                SubMenu Items = new SubMenu("Items", "Misc Item Stuff");
                {
                    Toggle interact = new Toggle("Interact With Inactive Quest Items", "Allows You To Pickup Quest Items That Aren't Active", ref Globals.Config.ItemMisc.ShowInactiveQuestItems);
                    Items.Items.Add(interact);
                }
                MiscMenu.Items.Add(Items);
                SubMenu Atmosphere = new SubMenu("Atmosphere", "Change Your Game World");
                {
                    Toggle FullBright = new Toggle("Full Bright", "Spanws A Light To Help You See", ref Globals.Config.Atmosphere.FullBright);
                    Toggle SetTime = new Toggle("Set Time", "Sets The Time To Selected Time", ref Globals.Config.Atmosphere.SetTime);
                    IntSlider Time = new IntSlider("Time", "Set Time's Time", ref Globals.Config.Atmosphere.Time, 0, 23, 1);
                    Toggle Weather = new Toggle("Clear Weather", "Stops Rain, Wind, Thunder", ref Globals.Config.Atmosphere.ClearWeather);
                    Toggle SetFog = new Toggle("Set Fog", "Sets Amount Of Fog To Selected Amount", ref Globals.Config.Atmosphere.FogModifier);
                    FloatSlider Fog = new FloatSlider("Fog", "Set Fog's Fog", ref Globals.Config.Atmosphere.Fog, 0f, 0.1f, 0.005f);
                    Atmosphere.Items.Add(FullBright);
                    Atmosphere.Items.Add(SetTime);
                    Atmosphere.Items.Add(Time);
                    Atmosphere.Items.Add(Weather);
                    Atmosphere.Items.Add(SetFog);
                    Atmosphere.Items.Add(Fog);
                }
                MiscMenu.Items.Add(Atmosphere);
                SubMenu LocalPlayer = new SubMenu("Local Player", "Manage Your Player");
                {
                    Toggle Night = new Toggle("Night Vision", "", ref Globals.Config.LocalPlayer.NightVision);
                    Toggle Thermal = new Toggle("Thermal Vision", "", ref Globals.Config.LocalPlayer.ThermalVision);
                    Toggle NoVisor = new Toggle("No Visor", "Removes Visor", ref Globals.Config.LocalPlayer.NoVisor);
                    Toggle UnlockAngles = new Toggle("Unlock View Angles", "Allows You To Look Up And Down", ref Globals.Config.LocalPlayer.UnlockViewAngles);
                    LocalPlayer.Items.Add(Night);
                    LocalPlayer.Items.Add(Thermal);
                    LocalPlayer.Items.Add(NoVisor);
                    LocalPlayer.Items.Add(UnlockAngles);
  
                }
                MiscMenu.Items.Add(LocalPlayer);
            }
            SubMenu MenuMenu = new SubMenu("Menu", "Configure Menu Layout");
            {
                SubMenu MenuPositioning = new SubMenu("Menu Positioning", "Certain Options For The Menu");
                {
                    FloatSlider menuposx = new FloatSlider("Menu Pos X", "Change X Axis Of Menu", ref Globals.Config.Menu.MenuPos.x, 0, Screen.width, 10);
                    FloatSlider menuposy = new FloatSlider("Menu Pos Y", "Change Y Axis Of Menu", ref Globals.Config.Menu.MenuPos.y, 0, Screen.height, 10);
                    MenuPositioning.Items.Add(menuposx);
                    MenuPositioning.Items.Add(menuposy);
                }
                MenuMenu.Items.Add(MenuPositioning);
                SubMenu MenuOptions = new SubMenu("Menu Options", "Certain Options For The Menu");
                {
                    Toggle menubackground = new Toggle("Menu Background", "Draws A Background Around Menu Options", ref Globals.Config.Menu.DrawBackground);
                    Keybind battlemode = new Keybind("BattleMode Key", "Ceases Drawing Selected Esp Options", ref Globals.Config.BattleMode.BattleModeKey);
                    MenuOptions.Items.Add(menubackground);
                    MenuOptions.Items.Add(battlemode);
                }
                MenuMenu.Items.Add(MenuOptions);
                SubMenu MenuDebugOptions = new SubMenu("Menu Debug Options", "Debug Stuff");
                {
                    Toggle waitforw2s = new Toggle("Wait For W2S", "Helps With Performance And Crashing But Can Cause ESP Flickering", ref Globals.Config.Menu.WaitForW2S);
                    MenuDebugOptions.Items.Add(waitforw2s);
                    Toggle garbagecollection = new Toggle("Collect Garbage", "Prevents Memory Usage At The Trade Off Of FPS", ref Globals.Config.Menu.GarbageCollection);
                    MenuDebugOptions.Items.Add(garbagecollection);
                }
                MenuMenu.Items.Add(MenuDebugOptions);
                SubMenu Hud = new SubMenu("HUD", "Change Options To The UI Hud");
                {
                    SubMenu Crosshair = new SubMenu("Crosshair", "Change And Customie Your Crosshair");
                    {
                        Toggle enable = new Toggle("Enable", "Toggles The Crosshair", ref Globals.Config.Crosshair.Enable);
                        CrosshairTypeSlider type = new CrosshairTypeSlider("Type", "Change Crosshair Type", ref Globals.Config.Crosshair.CrosshairType);
                        IntSlider spacing = new IntSlider("Crosshair Spacing", "Distance Between Parts Of The Crosshair", ref Globals.Config.Crosshair.CrosshairSpacing, 0, 100, 1);
                        IntSlider size = new IntSlider("Crosshair Size", "Size Of The Crosshair", ref Globals.Config.Crosshair.CrosshairSize, 0, 100, 1);
                        IntSlider thickness = new IntSlider("Crosshair Thickness", "Thickness Of Crosshair", ref Globals.Config.Crosshair.CrosshairThickness, 0, 100, 1);
                        FloatSlider animation = new FloatSlider("Crosshair Animation Smoothing", "Animation Speed Of Animated Crosshairs", ref Globals.Config.Crosshair.CrosshairAnimation, 0f, 40f, 0.25f);
                        Toggle dynamiccrosshair = new Toggle("Dynamic Crosshair", "Displays The Crosshair From The Fireport", ref Globals.Config.Crosshair.DynamicCrosshair);
                        Crosshair.Items.Add(enable);
                        Crosshair.Items.Add(type);
                        Crosshair.Items.Add(spacing);
                        Crosshair.Items.Add(size);
                        Crosshair.Items.Add(thickness);
                        Crosshair.Items.Add(animation);
                        Crosshair.Items.Add(dynamiccrosshair);
                    }
                    Hud.Items.Add(Crosshair);
                    SubMenu Radar = new SubMenu("Radar", "Change The Properties Of The Radar");
                    {
                        Toggle enable = new Toggle("Enable", "Toggles Radar", ref Globals.Config.Radar.Enable);
                        Toggle background = new Toggle("Background", "Draws A Background To The Radar", ref Globals.Config.Radar.Background);
                        IntSlider size = new IntSlider("Size", "Size Of The Radar", ref Globals.Config.Radar.Size, 0, 1000, 10);
                        IntSlider posx = new IntSlider("Position X Axis", "Position Of The Radar", ref Globals.Config.Radar.Radarx, 0, Screen.width, 25);
                        IntSlider posy = new IntSlider("Position Y Axis", "Position Of The Radar", ref Globals.Config.Radar.Radary, 0, Screen.height, 25);

                        Radar.Items.Add(enable);
                        Radar.Items.Add(background);
                        Radar.Items.Add(size);
                        Radar.Items.Add(posx);
                        Radar.Items.Add(posy);
                    }
                    Hud.Items.Add(Radar);
                    SubMenu Weapon = new SubMenu("Weapon", "Change And Customie Your Crosshair");
                    {
                        Toggle enable = new Toggle("Draw Weapon Ammo Hud", "Displays Weapon Information", ref Globals.Config.Hud.AmmoHud);
                        IntSlider x = new IntSlider("Position X Axis", "X Axis Position Of Hud", ref Globals.Config.Hud.Ammox, 0, Screen.width, 10);
                        IntSlider y = new IntSlider("Position Y Axis", "Y Axis Position Of Hud", ref Globals.Config.Hud.Ammoy, 0, Screen.height, 10);
                        Toggle allweapon = new Toggle("Draw All Weapons", "Displays Information For Inactive Weapons", ref Globals.Config.Hud.AmmoHudDisplayAllWeapons);
                        Weapon.Items.Add(enable);
                        Weapon.Items.Add(x);
                        Weapon.Items.Add(y);
                        Weapon.Items.Add(allweapon);
                    }
                    Hud.Items.Add(Weapon);
                }
                MenuMenu.Items.Add(Hud);
            }
            SubMenu ConfigMenu = new SubMenu("Config", "Load/Save Configs");

            CurrentMenu = MainMenu;
            Selected = AimbotMenu;
            MenuHistory = new List<SubMenu>() { MainMenu };

            MainMenu.Items.Add(AimbotMenu);
            MainMenu.Items.Add(EspMenu);
            MainMenu.Items.Add(MiscMenu);
            MainMenu.Items.Add(MenuMenu);
            MainMenu.Items.Add(ConfigMenu);



            SubMenu ColourMenu = new SubMenu("Colour", "Configure Colours");
            {
                Dictionary<string, SubMenu> CreatedMenuList = new Dictionary<string, SubMenu>();
                foreach (KeyValuePair<string, Color32> value in Globals.Config.Colours.ColourDict)
                {

                    string HostColourMenuString = value.Key.Substring(0, value.Key.IndexOf(" ")); // find the first space and end there
                    SubMenu HostMenu = new SubMenu(HostColourMenuString, "Colour Categories");
                    if (CreatedMenuList.ContainsKey(HostColourMenuString))
                        HostMenu = CreatedMenuList[HostColourMenuString];
                    else
                    {
                        CreatedMenuList[HostColourMenuString] = HostMenu;
                    }
                    SubMenu colourmenu = new SubMenu(value.Key.Substring(value.Key.IndexOf(" ") + 1, value.Key.Length - value.Key.IndexOf(" ") - 1), "Customize Colours");
                    int alpha = Helpers.ColourHelper.GetColour(value.Key).a;
                    IntSlider slidera = new IntSlider("Alpha", "Change The Colour Opacity", ref alpha, 0, 255, 10);
                    int red = Helpers.ColourHelper.GetColour(value.Key).r;
                    IntSlider sliderr = new IntSlider("Red", "Change Amount Of Red In Colour", ref red, 0, 255, 10);
                    int green = Helpers.ColourHelper.GetColour(value.Key).g;
                    IntSlider sliderg = new IntSlider("Green", "Change Amount Of Green In Colour", ref green, 0, 255, 10);
                    int blue = Helpers.ColourHelper.GetColour(value.Key).b;
                    IntSlider sliderb = new IntSlider("Blue", "Change Amount Of Blue In Colour", ref blue, 0, 255, 10);
                    colourmenu.Items.Add(slidera);
                    colourmenu.Items.Add(sliderr);
                    colourmenu.Items.Add(sliderg);
                    colourmenu.Items.Add(sliderb);
                    colourmenu.Items.Add(new Button("Save Colour", "Right Arrow To Save The Colour", () => Helpers.ColourHelper.SetColour(value.Key, new Color32((byte)red, (byte)green, (byte)blue, (byte)alpha))));
                    HostMenu.Items.Add(colourmenu);


                    // got to add the menus after we have initialized all the values

                }
                foreach (SubMenu menu in CreatedMenuList.Values)
                    ColourMenu.Items.Add(menu);
                MainMenu.Items.Add(ColourMenu);
            }
            SubMenu SaveConfigMenu = new SubMenu("Save Configs", "");
            {
                foreach (string str in ConfigHelper.GetConfigs())
                {
                    Button btn = new Button(string.Concat("Save ", str), "", () => ConfigHelper.SaveConfig(str));
                    SaveConfigMenu.Items.Add(btn);
                }
                ConfigMenu.Items.Add(SaveConfigMenu);

            }


            SubMenu LoadConfigMenu = new SubMenu("Load Configs", "");
            {

                foreach (string str in ConfigHelper.GetConfigs())
                {
                    Button btn = new Button(string.Concat("Load ", str), "", () => { ConfigHelper.LoadConfig(str); Init(); });
                    LoadConfigMenu.Items.Add(btn);
                }
                ConfigMenu.Items.Add(LoadConfigMenu);
            }

            SubMenu CreateConfigMenu = new SubMenu("Create Config", "");
            {
                TextField ConfigTextbox = new TextField("Config Name", "Enter A Config Name To Create", ref ConfigString);
                Button CopyConfigNameFromClipboard = new Button($"Copy Config Name From Clipboard {ConfigString}", "Copies The Config Name From Your Clipboard", () => { ConfigString = GUIUtility.systemCopyBuffer; ConfigTextbox.Value = ConfigString; });
                Button CreateConfig = new Button($"Create Config", "Creates Your Config", () => { ConfigString = ConfigTextbox.Value; ConfigHelper.SaveConfig(ConfigString); Init(); }); // set the value from the text field. I WISH I HAD ACCESS TO C POINTERS HERE!!!
                CreateConfigMenu.Items.Add(ConfigTextbox);
                CreateConfigMenu.Items.Add(CopyConfigNameFromClipboard);
                CreateConfigMenu.Items.Add(CreateConfig);
                ConfigMenu.Items.Add(CreateConfigMenu);
            }









        }

    }
}
