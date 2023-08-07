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
using System.Reflection;
using System.IO;
using Hag.Helpers;

namespace Hag.Esp
{
    class CorpseEsp : MonoBehaviour
    {
        static int HighestValue = 0;
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(UpdateEsp());
        }

        IEnumerator UpdateEsp()
        {
            for (; ; )
            {
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    HighestValue = 0;
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    goto END;
                if (!(Globals.Config.Corpse.Enable))
                    goto END;
                foreach (SDK.Corpses corpses in Globals.CorpseDict.Values)
                {
                    try
                    {
                        if (corpses.Corpse == null)
                            continue;

                        corpses.W2SPos = EspConstants.WorldToScreenPoint(corpses.Corpse.transform.position);
                        corpses.Distance = (int)Vector3.Distance(Globals.LocalPlayer.Transform.position, corpses.Corpse.transform.position);
                  //      corpses.RemoveChams();
                        if(EspConstants.IsScreenPointVisible(corpses.W2SPos) && corpses.Distance < Globals.Config.Corpse.MaxDistance)
                        corpses.SetContainerValue();

                        if (corpses.Value > HighestValue)
                            HighestValue = corpses.Value;
                    }
                    catch(Exception ex) { System.IO.File.WriteAllText("corpse cache 1", $"{ex.Message}\n{ex.ToString()}"); }
                }
                END:
                yield return new WaitForEndOfFrame();
            }
        }
        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
            if ((Globals.MainCamera == null || Globals.GameWorld == null))
                return;
            if (!(Globals.Config.Corpse.Enable))
                return;
            if (Globals.BattleMode && !Globals.Config.Corpse.EnableEspInBattleMode)
                return;
            foreach (SDK.Corpses corpses in Globals.CorpseDict.Values)
            {
                try
                {
                    if (corpses.Corpse == null)
                        continue;
                 
                    if (corpses.Value < Globals.Config.Corpse.MinValue && !(Globals.Config.Corpse.OverrideWithWhitelist && corpses.ContainsWhitelistedItem))
                        continue;
                    if (corpses.Distance > Globals.Config.Corpse.MaxDistance && !(Globals.Config.Corpse.OverrideWithWhitelist && corpses.ContainsWhitelistedItem))
                        continue;
                    if (!EspConstants.IsScreenPointVisible(corpses.W2SPos))
                        continue;
                    string[] EspTextBottom = new string[4];
                    string[] EspTextTop = new string[4];
                    string[] EspTextRight = new string[4];
                    string[] EspTextLeft = new string[4];

                    Color32 unitytextcolour = ColourHelper.GetColour("Corpse Text Colour");
                    Direct2DColor textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)corpses.Value / (float)(HighestValue + 1));
                    int distancealpha = (int)Mathf.Lerp(255, 38, (float)corpses.Distance / (float)(Globals.Config.Corpse.MaxDistance + 1));
                    if (Globals.Config.Corpse.Opacity == 1 && !(Globals.Config.Corpse.OverrideWithWhitelist && corpses.ContainsWhitelistedItem))
                        textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);
                    if (Globals.Config.Corpse.Opacity == 2 && !(Globals.Config.Corpse.OverrideWithWhitelist && corpses.ContainsWhitelistedItem))
                        textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, valuealpha);
                    if (Globals.Config.Corpse.Name.Enable)
                    {
                        switch (Globals.Config.Corpse.Name.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Corpse.Name.Line] += "Corpse";
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Corpse.Name.Line] += "Corpse";
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Corpse.Name.Line] += "Corpse";
                                break;
                            case 4:

                                EspTextTop[Globals.Config.Corpse.Name.Line] += "Corpse";
                                break;

                        }
                    }

                    if (Globals.Config.Corpse.Distance.Enable)
                    {
                        switch (Globals.Config.Corpse.Distance.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Corpse.Distance.Line] += string.Concat("[", corpses.Distance, "m]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Corpse.Distance.Line] += string.Concat("[", corpses.Distance, "m]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Corpse.Distance.Line] += string.Concat("[", corpses.Distance, "m]");
                                break;
                            case 4:

                                EspTextTop[Globals.Config.Corpse.Distance.Line] += string.Concat("[", corpses.Distance, "m]");
                                break;

                        }
                    }
                    if (Globals.Config.Corpse.Value.Enable)
                    {
                        switch (Globals.Config.Corpse.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Corpse.Value.Line] += string.Concat("[", corpses.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Corpse.Value.Line] += string.Concat("[", corpses.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Corpse.Value.Line] += string.Concat("[", corpses.Value / 1000, "k]");
                                break;
                            case 4:

                                EspTextTop[Globals.Config.Corpse.Value.Line] += string.Concat("[", corpses.Value / 1000, "k]");
                                break;

                        }
                    }
                    if (Globals.Config.Corpse.ContentsList && Globals.ContainerContents)
                    {
                        int line = 0;
                        switch (Globals.Config.Corpse.ContainerAlignment)
                        {
                            case 1:
                                for (int i = 1; i <= 3; i++)
                                {

                                    string str = EspTextBottom[i];
                                    if (str == null || str == "")
                                    {
                                        line = i;
                                        break;
                                    }

                                }
                                if (line == 0)
                                    line = 4;

                                foreach (SDK.ContainerItems containeritems in corpses.ItemList)
                                {

                                    containeritems.Colour.Alpha = textcolour.Alpha;


                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";
                                    renderer.DrawTextCentered(string.Concat(name, value, type, subtype), corpses.W2SPos.x, corpses.W2SPos.y + (17 * ((line - 1))) + (12 * corpses.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                }
                                break;
                            case 2:


                                for (int i = 1; i >= 3; i++)
                                {

                                    string str = EspTextBottom[i];
                                    if (str == null || str == "")
                                    {
                                        line = i;
                                        break;
                                    }

                                }

                                if (line == 0)
                                    line = 4;


                                foreach (SDK.ContainerItems containeritems in corpses.ItemList)
                                {
                                    containeritems.Colour.Alpha = textcolour.Alpha;
                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                    renderer.DrawTextLeft(string.Concat(name, value, type, subtype), (corpses.W2SPos.x - 48), corpses.W2SPos.y + (17 * (line - 1)) + (16 * corpses.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                }
                                break;
                            case 3:

                                for (int i = 1; i >= 3; i++)
                                {

                                    string str = EspTextBottom[i];
                                    if (str == null || str == "")
                                    {
                                        line = i;
                                        break;
                                    }

                                }

                                if (line == 0)
                                    line = 4;


                                foreach (SDK.ContainerItems containeritems in corpses.ItemList)
                                {
                                    containeritems.Colour.Alpha = textcolour.Alpha;

                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                    renderer.DrawText(string.Concat(name, value, type, subtype), (corpses.W2SPos.x + 48), corpses.W2SPos.y + (17 * (line - 1)) + (16 * corpses.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
                                }
                                break;
                            case 4:
                                for (int i = 3; i >= 1; i--)
                                {

                                    string str = EspTextTop[i];
                                    if (str == null || str == "")
                                    {
                                        line = i;
                                        break;
                                    }

                                }
                                if (line == 0)
                                    line = 4;

                                foreach (SDK.ContainerItems containeritems in corpses.ItemList)
                                {
                                    containeritems.Colour.Alpha = textcolour.Alpha;
                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                    renderer.DrawTextCentered(string.Concat(name, value, type, subtype, (corpses.ItemList.Count)), corpses.W2SPos.x, (((corpses.W2SPos.y - 60) - (17 * (line - 1)) - (12 * corpses.ItemList.IndexOf(containeritems)))), 10, font, containeritems.Colour);
                                }
                                break;

                        }

                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        if (EspTextBottom[i] != null)
                            renderer.DrawTextCentered(EspTextBottom[i], corpses.W2SPos.x, corpses.W2SPos.y + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextRight[i] != null)
                            renderer.DrawText(EspTextRight[i], corpses.W2SPos.x + 48, (corpses.W2SPos.y - 48) + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextLeft[i] != null)
                            renderer.DrawTextLeft(EspTextLeft[i], corpses.W2SPos.x - 48, (corpses.W2SPos.y - 48) + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextTop[i] != null)
                            renderer.DrawTextCentered(EspTextTop[i], corpses.W2SPos.x, (corpses.W2SPos.y - 60) - (16 * (i - 1)), 11, font, textcolour);
                    }
                }
                catch(Exception ex) { System.IO.File.WriteAllText("corpse esp 1", $"{ex.Message}\n{ex.ToString()}"); }
                }
        }

    }

}
