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
    class Items
    {
        bool IsItemAQuestRequirement()
        {

            foreach (var quests in Globals.LocalPlayer.Profile.QuestsData)
            {

                foreach (var conditionlist in quests.Template.Conditions)
                {
                    if (conditionlist.Key != EFT.Quests.EQuestStatus.AvailableForFinish)
                        continue;
                    foreach (EFT.Quests.Condition condition in conditionlist.Value)
                    {
                       
                        if (condition is EFT.Quests.ConditionFindItem)
                        {
                            EFT.Quests.ConditionFindItem finditem = condition as EFT.Quests.ConditionFindItem;
                            if (finditem.onlyFoundInRaid && !this.Item.Item.SpawnedInSession)
                                continue;
                            foreach (string str in finditem.target)
                                if (this.Item.Item.TemplateId == str)
                                    return true;
                            //  File.WriteAllText("Condition " + condition.FormattedDescription, str);
                            //System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
                        }
                        if (condition is EFT.Quests.ConditionHandoverItem)
                        {
                            EFT.Quests.ConditionHandoverItem handoveritem = condition as EFT.Quests.ConditionHandoverItem;
                            if (handoveritem.onlyFoundInRaid && !this.Item.Item.SpawnedInSession)
                                continue;
                            foreach (string str in handoveritem.target)
                                if (this.Item.Item.TemplateId == str)
                                    return true;
                            // File.WriteAllText("Condition " + condition.FormattedDescription, str);
                            //System.IO.File.WriteAllText("Condition " + conditionlist.ToString(), conditionlist.ToString());
                        }

                    }
                }
            }
            return false;
        }
        public Items(LootItem item)
        {
            Item = item;
            OurItem = Helpers.ItemPriceHelper.list[item.TemplateId];
            Value = OurItem.price;
            RequiredForQuest = IsItemAQuestRequirement();
        }
        public int Distance;
        public int Value;
        public Vector3 W2SPos;
        public OurItem OurItem;
        public LootItem Item;
        public bool RequiredForQuest;
    }
}
