using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using System.Reflection;
using System.Collections;
using Hag.Renderer;
using Hag.Helpers;

namespace Hag.Esp
{
    class ContainerEsp : MonoBehaviour
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
                if (!(Globals.Config.Container.Enable))
                    goto END;
                foreach (SDK.Containers containers in Globals.ContainerDict.Values)
                {
                    try
                    {
                        if (containers.Container == null)
                            continue;

                        containers.W2SPos = EspConstants.WorldToScreenPoint(containers.Container.transform.position);
                        containers.Distance = (int)Vector3.Distance(Globals.LocalPlayer.Transform.position, containers.Container.transform.position);
                        if(Esp.EspConstants.IsScreenPointVisible(containers.W2SPos) && containers.Distance < Globals.Config.Container.MaxDistance)
                        containers.SetContainerValue();
                     
                        if (containers.Value > HighestValue)
                            HighestValue = containers.Value;
                    }
                    catch(Exception ex) { System.IO.File.WriteAllText("container cache 1", $"{ex.Message}\n{ex.ToString()}"); }
                }
                END:
                yield return new WaitForEndOfFrame();
            }
        }
        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
            if ((Globals.MainCamera == null || Globals.GameWorld == null))
                return;
            if (!(Globals.Config.Container.Enable))
                return;
            if (Globals.BattleMode && !Globals.Config.Container.EnableEspInBattleMode)
                return;
            foreach (SDK.Containers containers in Globals.ContainerDict.Values)
            {
                try
                {
                    if (containers.Container == null)
                        continue;
                  
                    if (containers.Value < Globals.Config.Container.MinValue && !(containers.ContainsWhitelistedItem && Globals.Config.Container.OverrideWithWhitelist))
                        continue;
                    if (containers.Distance > Globals.Config.Container.MaxDistance && !(containers.ContainsWhitelistedItem && Globals.Config.Container.OverrideWithWhitelist))
                        continue;
                    if (!EspConstants.IsScreenPointVisible(containers.W2SPos))
                        continue;

                    string[] EspTextBottom = new string[4];
                    string[] EspTextTop = new string[4];
                    string[] EspTextRight = new string[4];
                    string[] EspTextLeft = new string[4];

                    Color32 unitytextcolour = ColourHelper.GetColour("Container Text Colour");
                    Direct2DColor textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                    int valuealpha = (int)Mathf.Lerp(50, 255, (float)containers.Value / (float)(HighestValue + 1));
                    int distancealpha = (int)Mathf.Lerp(255, 38, (float)containers.Distance / (float)(Globals.Config.Container.MaxDistance + 1));
                    if (Globals.Config.Container.Opacity == 1 && !(Globals.Config.Container.OverrideWithWhitelist && containers.ContainsWhitelistedItem) && !(Globals.Config.Container.OverrideWithQuestItems && containers.ContainsQuestItem))
                        textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);
                    if (Globals.Config.Container.Opacity == 2 && !(Globals.Config.Container.OverrideWithWhitelist && containers.ContainsWhitelistedItem) && !(Globals.Config.Container.OverrideWithQuestItems && containers.ContainsQuestItem))
                        textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, valuealpha);

                    if (Globals.Config.Container.Name.Enable)
                    {
                        switch (Globals.Config.Container.Name.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Container.Name.Line] += containers.Container.Template.LocalizedShortName();
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Container.Name.Line] += containers.Container.Template.LocalizedShortName();
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Container.Name.Line] += containers.Container.Template.LocalizedShortName();
                                break;
                            case 4:

                                EspTextTop[Globals.Config.Container.Name.Line] += containers.Container.Template.LocalizedShortName();
                                break;

                        }
                    }

                    if (Globals.Config.Container.Distance.Enable)
                    {
                        switch (Globals.Config.Container.Distance.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Container.Distance.Line] += string.Concat("[", containers.Distance, "m]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Container.Distance.Line] += string.Concat("[", containers.Distance, "m]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Container.Distance.Line] += string.Concat("[", containers.Distance, "m]");
                                break;
                            case 4:

                                EspTextTop[Globals.Config.Container.Distance.Line] += string.Concat("[", containers.Distance, "m]");
                                break;

                        }
                    }
                    if (Globals.Config.Container.Value.Enable)
                    {
                        switch (Globals.Config.Container.Value.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Container.Value.Line] += string.Concat("[", containers.Value / 1000, "k]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Container.Value.Line] += string.Concat("[", containers.Value / 1000, "k]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Container.Value.Line] += string.Concat("[", containers.Value / 1000, "k]");
                                break;
                            case 4:

                                EspTextTop[Globals.Config.Container.Value.Line] += string.Concat("[", containers.Value / 1000, "k]");
                                break;

                        }
                    }
                    if (Globals.Config.Container.ContentsList && Globals.ContainerContents)
                    {
                        int line = 0;
                        switch (Globals.Config.Container.ContainerAlignment)
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

                                foreach (SDK.ContainerItems containeritems in containers.ItemList)
                                {

                                    containeritems.Colour.Alpha = textcolour.Alpha;


                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";
                                    renderer.DrawTextCentered(string.Concat(name, value, type, subtype), containers.W2SPos.x, containers.W2SPos.y + (17 * ((line - 1))) + (12 * containers.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
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


                                foreach (SDK.ContainerItems containeritems in containers.ItemList)
                                {
                                    containeritems.Colour.Alpha = textcolour.Alpha;
                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                    renderer.DrawTextLeft(string.Concat(name, value, type, subtype), (containers.W2SPos.x - 48), containers.W2SPos.y + (17 * (line - 1)) + (16 * containers.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
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


                                foreach (SDK.ContainerItems containeritems in containers.ItemList)
                                {
                                    containeritems.Colour.Alpha = textcolour.Alpha;

                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                    renderer.DrawText(string.Concat(name, value, type, subtype), (containers.W2SPos.x + 48), containers.W2SPos.y + (17 * (line - 1)) + (16 * containers.ItemList.IndexOf(containeritems)), 10, font, containeritems.Colour);
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

                                foreach (SDK.ContainerItems containeritems in containers.ItemList)
                                {
                                    containeritems.Colour.Alpha = textcolour.Alpha;
                                    string name = containeritems.ShowName ? containeritems.Item.LocalizedShortName() : "";
                                    string value = containeritems.ShowValue ? string.Concat("[", (containeritems.Value / 1000).ToString(), "k]") : "";
                                    string type = containeritems.ShowType ? string.Concat("[", containeritems.OurItem.type.ToString(), "]") : "";
                                    string subtype = containeritems.ShowSubType ? string.Concat("[", containeritems.OurItem.subtype.ToString(), "]") : "";

                                    renderer.DrawTextCentered(string.Concat(name, value, type, subtype, (containers.ItemList.Count)), containers.W2SPos.x, (((containers.W2SPos.y - 60) - (17 * (line - 1)) - (12 * containers.ItemList.IndexOf(containeritems)))), 10, font, containeritems.Colour);
                                }
                                break;

                        }
                    }




                    for (int i = 1; i <= 3; i++)
                    {
                        if (EspTextBottom[i] != null)
                            renderer.DrawTextCentered(EspTextBottom[i], containers.W2SPos.x, containers.W2SPos.y + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextRight[i] != null)
                            renderer.DrawText(EspTextRight[i], containers.W2SPos.x + 48, (containers.W2SPos.y - 48) + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextLeft[i] != null)
                            renderer.DrawTextLeft(EspTextLeft[i], containers.W2SPos.x - 48, (containers.W2SPos.y - 48) + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextTop[i] != null)
                            renderer.DrawTextCentered(EspTextTop[i], containers.W2SPos.x, (containers.W2SPos.y - 60) - (16 * (i - 1)), 11, font, textcolour);
                    }
                }
                catch(Exception ex) { System.IO.File.WriteAllText("container esp 1", $"{ex.Message}\n{ex.ToString()}"); }
                }
        }
        }
    }
