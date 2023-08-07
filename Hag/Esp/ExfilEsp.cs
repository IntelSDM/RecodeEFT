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
    class ExfilEsp : MonoBehaviour
    {

        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(UpdateEsp());
        }
        // main thread
        IEnumerator UpdateEsp()
        {
            for (; ; )
            {
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    goto END;
              
                    foreach (var exfil in Globals.ExfilDict)
                    {
                    if (exfil.Key == null)
                        continue;
                    SDK.ExfilPoints exfils = exfil.Value;
                        if (exfils == null || exfils.ExfilPoint == null)
                            continue;
                       
                            exfils.W2Sed = false;
                            exfils.Distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, exfils.ExfilPoint.transform.position);

                            exfils.W2SPos = EspConstants.WorldToScreenPoint(exfils.ExfilPoint.transform.position);
                            exfils.W2Sed = true;
                      
                            // exfils.ExfilPoint.Settings.
                        }
              
            //    catch(Exception ex) { System.IO.File.WriteAllText("exfil cache 1", $"{ex.Message}\n{ex.ToString()}"); }
                END:
                yield return new WaitForEndOfFrame();
            }
        }
       
        // done on different thread
        public void Draw(Direct2DRenderer renderer, Direct2DFont font)
        {
           
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    return;
                if (Globals.Config.Exfil.Enable == false)
                    return;
                foreach (var exfil in Globals.ExfilDict)
                {
                if (exfil.Key == null)
                    continue;
                SDK.ExfilPoints exfils = exfil.Value;
                        if (exfils == null || exfils.ExfilPoint == null)
                            continue;
                        if (exfils.ExfilPoint.Status == EExfiltrationStatus.Pending || exfils.ExfilPoint.Status == EExfiltrationStatus.NotPresent)
                            continue;
                        if (exfils.Distance > Globals.Config.Exfil.MaxDistance)
                            continue;
                        if (!EspConstants.IsScreenPointVisible(exfils.W2SPos))
                            continue;
                        if (!exfils.W2Sed)
                            continue;
                        //     if (!exfils.ExfilPoint.HasMetRequirements(Globals.LocalPlayer.ProfileId))
                        //        continue;
                        string[] EspTextBottom = new string[4];
                        string[] EspTextTop = new string[4];
                        string[] EspTextRight = new string[4];
                        string[] EspTextLeft = new string[4];

                        int distancealpha = (int)Mathf.Lerp(255, 38, (float)exfils.Distance / (float)(Globals.Config.Exfil.MaxDistance + 1));
                        Color32 unitytextcolour = ColourHelper.GetColour("Exfil Text Colour");
                        Direct2DColor textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, unitytextcolour.a);
                        if (Globals.Config.Exfil.LimitOpacityByDistance)
                            textcolour = new Direct2DColor(unitytextcolour.r, unitytextcolour.g, unitytextcolour.b, distancealpha);

                        if (Globals.Config.Exfil.Name.Enable)
                        {
                            switch (Globals.Config.Exfil.Name.Alignment)
                            {
                                case 1:
                                    EspTextBottom[Globals.Config.Exfil.Name.Line] += exfils.ExfilPoint.Settings.Name;
                                    break;
                                case 2:
                                    EspTextLeft[Globals.Config.Exfil.Name.Line] += exfils.ExfilPoint.Settings.Name;
                                    break;
                                case 3:
                                    EspTextRight[Globals.Config.Exfil.Name.Line] += exfils.ExfilPoint.Settings.Name;
                                    break;
                                case 4:
                                    EspTextTop[Globals.Config.Exfil.Name.Line] += exfils.ExfilPoint.Settings.Name;
                                    break;

                            }


                        }
                        if (Globals.Config.Exfil.Distance.Enable)
                        {
                            switch (Globals.Config.Exfil.Distance.Alignment)
                            {
                                case 1:
                                    EspTextBottom[Globals.Config.Exfil.Distance.Line] += string.Concat("[", exfils.Distance, "m]");
                                    break;
                                case 2:
                                    EspTextLeft[Globals.Config.Exfil.Distance.Line] += string.Concat("[", exfils.Distance, "m]");
                                    break;
                                case 3:
                                    EspTextRight[Globals.Config.Exfil.Distance.Line] += string.Concat("[", exfils.Distance, "m]");
                                    break;
                                case 4:

                                    EspTextTop[Globals.Config.Exfil.Distance.Line] += string.Concat("[", exfils.Distance, "m]");
                                    break;

                            }




                        }
                        for (int i = 1; i <= 3; i++)
                        {
                            if (EspTextBottom[i] != null)
                                renderer.DrawTextCentered(EspTextBottom[i], exfils.W2SPos.x, exfils.W2SPos.y + (16 * (i - 1)), 11, font, textcolour);
                            if (EspTextRight[i] != null)
                                renderer.DrawText(EspTextRight[i], exfils.W2SPos.x + 48, (exfils.W2SPos.y - 48) + (16 * (i - 1)), 11, font, textcolour);
                            if (EspTextLeft[i] != null)
                                renderer.DrawTextLeft(EspTextLeft[i], exfils.W2SPos.x - 48, (exfils.W2SPos.y - 48) + (16 * (i - 1)), 11, font, textcolour);
                            if (EspTextTop[i] != null)
                                renderer.DrawTextCentered(EspTextTop[i], exfils.W2SPos.x, (exfils.W2SPos.y - 60) - (16 * (i - 1)), 11, font, textcolour);
                        }
                    }
           
        }
    }
}
