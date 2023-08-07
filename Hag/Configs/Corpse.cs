using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Hag.Configs
{
    class Corpse
    {
        public bool Enable = true;
        public EspText Name = new EspText(true, 1, 1);
        public EspText Distance = new EspText(true, 1, 1);
        public EspText Value = new EspText(true, 1, 1);
        public int MinValue = 10000;
        public int MaxDistance = 500;
        public UnityEngine.KeyCode ContainerKey = UnityEngine.KeyCode.V;
        public int Opacity = 1;
        public bool ContentsList = true;
        public int ContainerAlignment = 1;
        public bool EnableEspInBattleMode = false;
        public bool OverrideWithWhitelist = true;
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
