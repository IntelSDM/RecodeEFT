using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Collections;
using Hag.Renderer;
using System.Reflection;

namespace Hag.Esp
{
    class GrenadeEsp : MonoBehaviour
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
        IEnumerator UpdateEsp()
        {
            for (; ; )
            {
                if ((Globals.MainCamera == null || Globals.GameWorld == null))
                    goto END;
                if (!Globals.Config.Grenade.Enable)
                    goto END;
                try
                {
                    foreach (SDK.Grenades grenades in Globals.GrenadeDict.Values)
                    {
                        if (grenades.Grenade == null)
                            continue;
                        try
                        {
                            if (Globals.GrenadeDict.Count > 40) // basic check incase someone does some unlimited grenade exploit on our users, they wont crash
                                continue;
                            grenades.W2SPos = EspConstants.WorldToScreenPoint(grenades.Grenade.transform.position);
                            grenades.Distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, grenades.Grenade.transform.position);


                            if (grenades.VulnerablePlayerList.Count > 0)
                                continue;
                        /*    foreach (SDK.Players players in Globals.PlayerDict.Values)
                            {
                                float distance = Vector3.Distance(players.Player.Transform.position, grenades.Grenade.transform.position);
                                if (distance > 8)
                                    continue;
                                bool[] isbonevisible = new bool[15];
                                foreach (Vector3 bonepos in players.BoneWorld)
                                {
                                    isbonevisible[players.BoneWorld.IndexOf(bonepos)] = Helpers.RaycastHelper.IsPointVisible(players.Player, bonepos, grenades.Grenade);
                                }
                                foreach (bool isvisible in isbonevisible)
                                {
                                    if (isvisible == true)
                                        grenades.VulnerablePlayerList.Add(players);
                                }
                            }*/
                        }
                        catch(Exception ex) { System.IO.File.WriteAllText("grenade cache 1", $"{ex.Message}\n{ex.ToString()}"); }
                    }
                }
                catch(Exception ex)
                { System.IO.File.WriteAllText("grenade cache 2", $"{ex.Message}\n{ex.ToString()}"); }
                END:
                yield return new WaitForEndOfFrame();
            }
        }
        void DrawPlayerList(SDK.Grenades grenades,string[] EspTextBottom, string[] EspTextTop, Direct2DColor textcolour,Direct2DRenderer renderer, Direct2DFont font)
        {
            if (!Globals.Config.Grenade.GrenadeList)
                return;
            if (grenades.VulnerablePlayerList.Count < 0)
                return;
          
                int line = 0;
              
                switch (Globals.Config.Grenade.GrenadeAlignment)
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

                        foreach (SDK.Players players in grenades.VulnerablePlayerList)
                        {
                        //containeritems.Colour.Alpha = textcolour.Alpha;
                        string name = players.Player.Profile.Nickname;
                        Color32 unitycolour = Helpers.ColourHelper.GetColour("Player Text Colour");
                        if(players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("Boss Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Scav)
                            unitycolour = Helpers.ColourHelper.GetColour("Scav Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("ScavPlayer Text Colour");
                        if (players.Friendly)
                            unitycolour = Helpers.ColourHelper.GetColour("Player Friend Colour");
                        Direct2DColor colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, textcolour.Alpha);
                        
                        renderer.DrawTextCentered(string.Concat(name), players.W2SPos.x, players.W2SPos.y + (17 * ((line - 1))) + (12 * grenades.VulnerablePlayerList.IndexOf(players)), 10, font, colour);
                        }
                        //     EspTextLeft[1] += players.ItemList.Count();
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
                        if (Globals.Config.ScavPlayer.Healthbar.Alignment == 1)
                            line = line + 1; // if the health bar is on line the 4th line or not
                        if (line == 0)
                            line = 4;


                    foreach (SDK.Players players in grenades.VulnerablePlayerList)
                    {
                        string name = players.Player.Profile.Nickname;
                        Color32 unitycolour = Helpers.ColourHelper.GetColour("Player Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("Boss Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Scav)
                            unitycolour = Helpers.ColourHelper.GetColour("Scav Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("ScavPlayer Text Colour");
                        if (players.Friendly)
                            unitycolour = Helpers.ColourHelper.GetColour("Player Friend Colour");
                        Direct2DColor colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, textcolour.Alpha);

                        renderer.DrawTextLeft(string.Concat(name), (players.W2SPos.x + 20 / 2) - 20 * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * grenades.VulnerablePlayerList.IndexOf(players)), 10, font, colour);
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
                        if (Globals.Config.ScavPlayer.Healthbar.Alignment == 1)
                            line = line + 1; // if the health bar is on line the 4th line or not
                        if (line == 0)
                            line = 4;


                    foreach (SDK.Players players in grenades.VulnerablePlayerList)
                    {
                        string name = players.Player.Profile.Nickname;
                        Color32 unitycolour = Helpers.ColourHelper.GetColour("Player Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("Boss Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Scav)
                            unitycolour = Helpers.ColourHelper.GetColour("Scav Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("ScavPlayer Text Colour");
                        if (players.Friendly)
                            unitycolour = Helpers.ColourHelper.GetColour("Player Friend Colour");
                        Direct2DColor colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, textcolour.Alpha);

                        renderer.DrawText(string.Concat(name), (players.W2SPos.x + 20 / 2) + 20 * 2.1f, players.W2SPos.y + (17 * (line - 1)) + (16 * grenades.VulnerablePlayerList.IndexOf(players)), 10, font, colour);
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

                    foreach (SDK.Players players in grenades.VulnerablePlayerList)
                    {
                        string name = players.Player.Profile.Nickname;
                        Color32 unitycolour = Helpers.ColourHelper.GetColour("Player Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("Boss Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Scav)
                            unitycolour = Helpers.ColourHelper.GetColour("Scav Text Colour");
                        if (players.PlayerType == SDK.PlayerType.Boss)
                            unitycolour = Helpers.ColourHelper.GetColour("ScavPlayer Text Colour");
                        if (players.Friendly)
                            unitycolour = Helpers.ColourHelper.GetColour("Player Friend Colour");
                        Direct2DColor colour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, textcolour.Alpha);

                        renderer.DrawTextCentered(string.Concat(name), players.BoneW2S[0].x, (((players.BoneW2S[0].y - 18) - (17 * (line - 1)) - (12 * grenades.VulnerablePlayerList.IndexOf(players)))), 10, font, colour);
                        }
                        break;

                }
            grenades.VulnerablePlayerList.Clear();
        }
        public void Draw(Direct2DRenderer renderer,Direct2DFont font)
        {
            if (!Globals.Config.Grenade.Enable)
                return;
            if (Globals.MainCamera == null)
                return;
            if (Globals.BattleMode && !Globals.Config.Grenade.EnableEspInBattleMode)
                return;
            foreach (SDK.Grenades grenades in Globals.GrenadeDict.Values)
            {
                try
                {
                    if (grenades.Grenade == null)
                        continue;
                    if (!Esp.EspConstants.IsScreenPointVisible(grenades.W2SPos))
                        continue;
                    if (grenades.Distance > Globals.Config.Grenade.MaxDistance)
                        continue;

                    bool friendly = (Globals.LocalPlayer.Profile.Info.GroupId == grenades.Grenade.Player.Profile.Info.GroupId
                        && grenades.Grenade.Player.Profile.Info.GroupId != "0"
                        && grenades.Grenade.Player.Profile.Info.GroupId != ""
                        && grenades.Grenade.Player.Profile.Info.GroupId != null) || Globals.LocalPlayer == grenades.Grenade.Player;
                    int distancealpha = (int)Mathf.Lerp(255, 38, (float)grenades.Distance / (float)(Globals.Config.Grenade.MaxDistance + 1));
                    Color32 unitycolour = new Color32();
                    unitycolour = friendly ? Helpers.ColourHelper.GetColour("Grenade Friendly Colour") : Helpers.ColourHelper.GetColour("Grenade Text Colour");
                    if (Globals.Config.Grenade.CullOpacityWithDistance)
                        unitycolour.a = (byte)distancealpha;
                    Direct2DColor textcolour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                    unitycolour = Helpers.ColourHelper.GetColour("Grenade Duration Bar Colour");
                    Direct2DColor durationbarcolour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                    unitycolour = Helpers.ColourHelper.GetColour("Grenade Duration Bar Background Colour");
                    Direct2DColor durationbarbackgroundcolour = new Direct2DColor(unitycolour.r, unitycolour.g, unitycolour.b, unitycolour.a);
                    string[] EspTextBottom = new string[4];
                    string[] EspTextTop = new string[4];
                    string[] EspTextRight = new string[4];
                    string[] EspTextLeft = new string[4];
                    string throwername = grenades.Grenade.Player.Side == EPlayerSide.Savage ? "Scav" : grenades.Grenade.Player.Profile.Info.Nickname.Localized();

                    if (Globals.Config.Grenade.Name.Enable)
                    {
                        switch (Globals.Config.Grenade.Name.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Grenade.Name.Line] += grenades.Grenade.WeaponSource.ShortName.Localized();
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Grenade.Name.Line] += grenades.Grenade.WeaponSource.ShortName.Localized();
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Grenade.Name.Line] += grenades.Grenade.WeaponSource.ShortName.Localized();
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Grenade.Name.Line] += grenades.Grenade.WeaponSource.ShortName.Localized();
                                break;

                        }
                    }
                    if (Globals.Config.Grenade.Distance.Enable)
                    {
                        switch (Globals.Config.Grenade.Distance.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Grenade.Distance.Line] += string.Concat("[", grenades.Distance, "m]");
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Grenade.Distance.Line] += string.Concat("[", grenades.Distance, "m]");
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Grenade.Distance.Line] += string.Concat("[", grenades.Distance, "m]");
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Grenade.Distance.Line] += string.Concat("[", grenades.Distance, "m]");
                                break;

                        }
                    }
                    if (Globals.Config.Grenade.ThrowerName.Enable)
                    {
                        switch (Globals.Config.Grenade.ThrowerName.Alignment)
                        {
                            case 1:
                                EspTextBottom[Globals.Config.Grenade.ThrowerName.Line] += throwername;
                                break;
                            case 2:
                                EspTextLeft[Globals.Config.Grenade.ThrowerName.Line] += throwername;
                                break;
                            case 3:
                                EspTextRight[Globals.Config.Grenade.ThrowerName.Line] += throwername;
                                break;
                            case 4:
                                EspTextTop[Globals.Config.Grenade.ThrowerName.Line] += throwername;
                                break;

                        }
                    }
                    // float Health = (player.HealthController.GetBodyPartHealth(EBodyPart.Common, false).Current / player.HealthController.GetBodyPartHealth(EBodyPart.Common).Maximum) * 100f;
                    float elapsedtime = Time.time - grenades.StartTime;
                    float duration = (elapsedtime / grenades.Grenade.WeaponSource.GetExplDelay) * 100f;
                    if (Globals.Config.Grenade.Durationbar.Enable)
                    {
                        if (grenades.Distance < Globals.Config.Grenade.Durationbar.MaxDistance)
                        {
                            int line = 0;
                            switch (Globals.Config.Grenade.Durationbar.Alignment)
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
                                    renderer.DrawVerticalBar(duration, grenades.W2SPos.x - (30 / 2) - 2, (grenades.W2SPos.y + (16 * (line - 1))) + 3, 30 + 2, 2, 1, durationbarcolour, durationbarbackgroundcolour);
                                    break;
                                case 2:
                                    renderer.DrawHorizontalBar(duration, (grenades.W2SPos.x + 30 / 2) + 3, grenades.W2SPos.y - 10, 2, 30, 1, durationbarcolour, durationbarbackgroundcolour);
                                    break;
                                case 3:

                                    renderer.DrawHorizontalBar(duration, (grenades.W2SPos.x - 30 / 2) - 5, grenades.W2SPos.y - 10, 2, 30, 1, durationbarcolour, durationbarbackgroundcolour);
                                    break;
                                case 4:
                                    for (int i = 1; i <= 3; i++)
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
                                    renderer.DrawVerticalBar(duration, grenades.W2SPos.x - (30 / 2) - 2, ((grenades.W2SPos.y) - (17 * (line - 1))) - 14, 30 + 2, 2, 1, durationbarcolour, durationbarbackgroundcolour);
                                    break;
                            }

                        }
                    }
                    //  grenades.Grenade.OnExplosion();
                    //grenades.Grenade.Player.Profile.Info.Nickname
                    // grenades.Grenade.WeaponSource.ShortName.Localized()

                    //  + grenades.Grenade.WeaponSource.GetExplDelay -  compare it to this 


                    for (int i = 1; i <= 3; i++)
                    {
                        if (EspTextBottom[i] != null)
                            if (EspConstants.IsScreenPointVisible(grenades.W2SPos))
                                renderer.DrawTextCentered(EspTextBottom[i], grenades.W2SPos.x, grenades.W2SPos.y + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextRight[i] != null)
                            if (EspConstants.IsScreenPointVisible(grenades.W2SPos))
                                renderer.DrawText(EspTextRight[i], grenades.W2SPos.x + 20, (grenades.W2SPos.y + 16) + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextLeft[i] != null)
                            if (EspConstants.IsScreenPointVisible(grenades.W2SPos))
                                renderer.DrawTextLeft(EspTextLeft[i], grenades.W2SPos.x - 20, (grenades.W2SPos.y + 16) + (16 * (i - 1)), 11, font, textcolour);
                        if (EspTextTop[i] != null)
                            if (EspConstants.IsScreenPointVisible(grenades.W2SPos))
                                renderer.DrawTextCentered(EspTextTop[i], grenades.W2SPos.x, (grenades.W2SPos.y - 18) - (17 * (i - 1)), 11, font, textcolour);
                    }
              //      DrawPlayerList(grenades, EspTextBottom, EspTextTop, textcolour, renderer, font);
                }
                catch(Exception ex) { System.IO.File.WriteAllText("grenade esp 1", $"{ex.Message}\n{ex.ToString()}"); }
               
            }
        }
    }
}
