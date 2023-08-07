using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using EFT.Interactive;
using System.Collections;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using Hag.Helpers;
using System.Reflection;

namespace Hag.Misc
{
    class Binds : MonoBehaviour
    {
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {

            StartCoroutine(UpdateBinds());
          
        }
        IEnumerator UpdateBinds()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null)
                    goto END;
                if (Input.GetKeyDown(Globals.Config.BattleMode.BattleModeKey))
                {
                    Globals.BattleMode = !Globals.BattleMode;

                }
                if (Input.GetKeyDown(Globals.Config.Player.ContentsListKey))
                  Globals.PlayerContents = !Globals.PlayerContents;

                if (Input.GetKeyDown(Globals.Config.Scav.ContentsListKey))
                    Globals.ScavContents = !Globals.ScavContents;
                if (Input.GetKeyDown(Globals.Config.ScavPlayer.ContentsListKey))
                    Globals.ScavPlayerContents = !Globals.ScavPlayerContents;
                if (Input.GetKeyDown(Globals.Config.Boss.ContentsListKey))
                    Globals.ScavPlayerContents = !Globals.ScavPlayerContents;
                if (Input.GetKeyDown(Globals.Config.Container.ContainerKey))
                    Globals.ContainerContents = !Globals.ContainerContents;
                END:
                yield return new WaitForEndOfFrame();
            }
        }
      
    }
}
