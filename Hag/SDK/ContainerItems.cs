using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using UnityEngine;
namespace Hag.SDK
{
    class ContainerItems
    {
        public ContainerItems(Item item,Helpers.OurItem ouritem,int value, bool showname,bool showvalue, bool showtype, bool showsubtype, bool questrequirement)
        {
            Item = item;
            Value = value;
            OurItem = ouritem;
           // Colour = Helpers.ColourHelper.GetItemColour(this);
            ShowName = showname;
            ShowValue = showvalue;
            ShowType = showtype;
            ShowSubType = showsubtype;
            QuestRequirement = questrequirement;
        }
        public int Value;
        public Renderer.Direct2DColor Colour;
        public Item Item;
        public Helpers.OurItem OurItem;
        public bool ShowName;
        public bool ShowValue;
        public bool ShowType;
        public bool ShowSubType;
        public bool QuestRequirement;

    }
}
