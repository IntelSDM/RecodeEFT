using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Hag.Configs
{
    class Player
    {
        public bool Enable = true;
        public int MaxDistance = 850;
        public int Opacity = 0;
        public bool TextVisibilityCheck = false;
        public EspText Name = new EspText(true, 1, 1);
        public EspText Distance = new EspText(true, 1, 1);
        public EspSkeleton Skeleton = new EspSkeleton(true, 2, 250);
        public EspText Weapon = new EspText(true, 1, 2);
        public EspText Ammo = new EspText(true, 1, 2);
        public EspHealthbar Healthbar = new EspHealthbar(true,1,250);
        public EspText Value = new EspText(true, 1, 1);
        public EspPlayerBox Box = new EspPlayerBox(true, 250, true, true);
        public bool EnableEspInBattleMode = false;
        public bool SkipAttachmentsInValue = true;
        public bool ContentsList = false;
        public int ContentListAlignment = 2;
        public KeyCode ContentsListKey = KeyCode.K;
        public bool Chams = false;
        public int ChamsType = 0;
        public bool ApplyChamsToGear = true;

        public ContainerItem WhitelistContainer = new ContainerItem();
        public ContainerItem FuelContainer = new ContainerItem();
        public ContainerItem BarterItemsContainer = new ContainerItem();
        public ContainerItem SpecialItemsContainer = new ContainerItem();
        public ContainerItem CasesContainer = new ContainerItem();
        public ContainerItem BackpacksContainer = new ContainerItem();
        public ContainerItem WeaponContainer = new ContainerItem();
        public ContainerItem ClothingContainer = new ContainerItem();
        public ContainerItem ArmourContainer = new ContainerItem();
        public ContainerItem FoodDrinkContainer = new ContainerItem();
        public ContainerItem KeyContainer = new ContainerItem();
        public ContainerItem KeycardContainer = new ContainerItem();
        public ContainerItem MedsContainer = new ContainerItem();
        public ContainerItem AmmoContainer = new ContainerItem();
        public ContainerItem RepairKitsContainer = new ContainerItem();
        public ContainerItem AttachmentsContainer = new ContainerItem();
    }
}
