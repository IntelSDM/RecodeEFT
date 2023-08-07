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
using Hag.Renderer;
using UnityEditor;
using EFT.Weather;
using System.Runtime;
namespace Hag.Misc
{
    class QualityOfLife : MonoBehaviour
    {
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            StartCoroutine(EnableGC());
            StartCoroutine(DisableGC());
            StartCoroutine(Lighting());
        }
        #region fullbright from maoci
        public bool Enabled = false;

        public GameObject lightGameObject;

        public Light FullBrightLight;

        public bool _LightEnabled = true;

        public bool lightCalled;

        private Vector3 tempPosition = Vector3.zero;
        public void FullBright_SpawnObject()
        {
            if (Globals.LocalPlayer != null && !lightCalled && Enabled)
            {
                lightGameObject = new GameObject("Fullbright");
                FullBrightLight = lightGameObject.AddComponent<Light>();
                FullBrightLight.color = ColourHelper.GetColour("Atmosphere FullBright Colour");
                // FullBrightLight.color = new Color(1f, 0, 0, 1f);
                FullBrightLight.range = 2000f;
                FullBrightLight.intensity = 0.6f;
                lightCalled = true;
            }
        }

        public void FullBright_UpdateObject(bool set)
        {
            if (Globals.LocalPlayer != null)
            {
                Enabled = set;
                if (set)
                {
                    if (FullBrightLight == null)
                    {
                        return;
                    }
                    tempPosition = Globals.LocalPlayer.Transform.position;
                    tempPosition.y = tempPosition.y + 0.2f;
                    FullBrightLight.color = ColourHelper.GetColour("Atmosphere FullBright Colour");
                    lightGameObject.transform.position = tempPosition;
                    return;
                }
                else
                {
                    if (FullBrightLight != null)
                    {
                        UnityEngine.Object.Destroy(FullBrightLight);
                    }
                    lightCalled = false;
                }
            }
        }
        #endregion
        IEnumerator Lighting()
        {
            for (; ; )
            {
                if (Globals.LocalPlayer == null || Globals.GameWorld == null || TOD_Sky.Instance == null || WeatherController.Instance == null)
                {
                    //  FullBrightCalled = false;
                    if (FullBrightLight != null)
                    {
                        UnityEngine.Object.Destroy(FullBrightLight);
                    }
                    lightCalled = false;
                    goto END;
                }

                FullBright_UpdateObject(Globals.Config.Atmosphere.FullBright);
                FullBright_SpawnObject();
                if (Globals.Config.Atmosphere.SetTime)
                {
                    TOD_Sky.Instance.Components.Time.GameDateTime = null;
                   TOD_Sky.Instance.Cycle.Hour = Globals.Config.Atmosphere.Time;
                }
                    if (Globals.Config.Atmosphere.ClearWeather)
                    {

                        WeatherController.Instance.WeatherDebug.Enabled = true;
                        WeatherController.Instance.WeatherDebug.CloudDensity = -0.7f;
                        WeatherController.Instance.WeatherDebug.LightningThunderProbability = 0f;
                        WeatherController.Instance.WeatherDebug.Rain = 0f;
                     WeatherController.Instance.WeatherDebug.WindMagnitude = 0f;
                }
                    if (Globals.Config.Atmosphere.FogModifier)
                    {
                    
                        WeatherController.Instance.WeatherDebug.Enabled = true;
                        WeatherController.Instance.WeatherDebug.Fog = Globals.Config.Atmosphere.Fog; // 0.1
                    }
                



            END:
                yield return new WaitForEndOfFrame();
            }
        }
        IEnumerator EnableGC()
        {
            for (; ; )
            {
            if(Globals.Config.Menu.GarbageCollection)
              UnityEngine.Scripting.GarbageCollector.GCMode = UnityEngine.Scripting.GarbageCollector.Mode.Enabled;

                GCSettings.LatencyMode = GCLatencyMode.Interactive;
             //   GC.CollectionFrequency
                yield return new WaitForSeconds(15f);
            }
        }
       
        IEnumerator DisableGC()
        {
            for (; ; )
            {
                UnityEngine.Scripting.GarbageCollector.GCMode = UnityEngine.Scripting.GarbageCollector.Mode.Disabled;
                yield return new WaitForSeconds(7f);
            }
        }
        void OnApplicationQuit()
        {
            StopCoroutine(EnableGC());
            StopCoroutine(DisableGC());
            Environment.Exit(0);


        }
        private void OnLowMemory()
        {
            try
            {
                Application.Unload();
              
            }
            catch { }
            try
            {
                Resources.UnloadUnusedAssets();
            }
            catch { }
            UnityEngine.Scripting.GarbageCollector.GCMode = UnityEngine.Scripting.GarbageCollector.Mode.Enabled;
        }
    }
}
