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
using Comfort.Common;
using EFT.UI;
using UnityEngine.Scripting;
using Hag.Helpers;
namespace Hag.Esp
{
    class QuestEsp  : MonoBehaviour
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

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
