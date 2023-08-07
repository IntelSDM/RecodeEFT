using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Hag.Helpers;
using EFT.Interactive;
using EFT;
using System.Reflection;
using System.Collections;

namespace Hag.Misc
{
    class LocalPlayer : MonoBehaviour
    {
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscated();
        }
        void StartObfuscated()
        {
            //   Application.lowMemory += OnLowMemory;
            StartCoroutine(ChamManager());
            StartCoroutine(Visuals());
            //    StartCoroutine(DisableGC());
        }
        IEnumerator ChamManager()
        {
            for (; ; )
            {

                if (Globals.GameWorld == null || Globals.LocalPlayer == null)
                    goto END;
                if (!Globals.EndedFrame)
                    continue;
                Chams();
              
            END:
                yield return new WaitForSeconds(1f);
            }
        }
        IEnumerator Visuals()
        {
            for (; ; )
            {
                if (Globals.GameWorld == null || Globals.LocalPlayer == null)
                    goto END;   
                if (Globals.Config.LocalPlayer.NoVisor)
                {
                    Globals.MainCamera.GetComponent<VisorEffect>().Intensity = 0f;
                    Globals.MainCamera.GetComponent<VisorEffect>().enabled = true;
                }
                if (Globals.Config.LocalPlayer.UnlockViewAngles)
                {
                    Globals.LocalPlayer.MovementContext.PitchLimit = new Vector2(-90, 90);
                    Globals.LocalPlayer.MovementContext.SetYawLimit(new Vector2(-360, 360));
                }
              
                if (Globals.Config.LocalPlayer.NightVision)
                    Globals.MainCamera.GetComponent<BSG.CameraEffects.NightVision>().SetPrivateField("_on", true);
                else
                    Globals.MainCamera.GetComponent<BSG.CameraEffects.NightVision>().SetPrivateField("_on", false);

                if (Globals.Config.LocalPlayer.ThermalVision)
                    Globals.MainCamera.GetComponent<ThermalVision>().On = true;
                else
                    Globals.MainCamera.GetComponent<ThermalVision>().On = false;

                END:
                yield return new WaitForEndOfFrame();
            }
        }
        public void Chams()
        {
            try
            {
                UnityEngine.Shader shader = Helpers.ShaderHelper.Flat;
                if (Globals.Config.LocalPlayer.ChamsType == 0)
                    shader = Helpers.ShaderHelper.Flat;
                if (Globals.Config.LocalPlayer.ChamsType == 1)
                    shader = Helpers.ShaderHelper.Textured;
                if (Globals.Config.LocalPlayer.ChamsType == 2)
                    shader = Helpers.ShaderHelper.Pulse;
                if (Globals.Config.LocalPlayer.ChamsType == 3)
                    shader = Helpers.ShaderHelper.Rainbow;
                if (Globals.Config.LocalPlayer.ChamsType == 4)
                    shader = Helpers.ShaderHelper.Wireframe;
                if (Globals.Config.LocalPlayer.ChamsType == 5)
                    shader = Helpers.ShaderHelper.TransparentShader;
                Color32 primarycolour = new Color32();
                Color32 secondarycolour = new Color32();

                primarycolour = ColourHelper.GetColour("LocalPlayer Chams Primary Colour");
                secondarycolour = ColourHelper.GetColour("LocalPlayer Chams Secondary Colour");

                if (Globals.Config.LocalPlayer.Chams)
                {
                    if (Globals.Config.LocalPlayer.ChamsOnGun)
                    {
                        foreach (var r in Globals.LocalPlayer.GetComponentsInChildren<UnityEngine.Renderer>())
                        {


                            if (Globals.Config.LocalPlayer.ChamsType == 6)
                                r.material = ShaderHelper.GalaxyMat;
                            if (Globals.Config.LocalPlayer.ChamsType == 0 || Globals.Config.LocalPlayer.ChamsType == 1)
                            {
                                r.material.SetColor("_ColorVisible", secondarycolour);
                                r.material.SetColor("_ColorBehind", primarycolour);
                            }
                            if (Globals.Config.LocalPlayer.ChamsType == 2)
                            {
                                r.material.SetColor("_Emissioncolour", primarycolour);
                            }
                            if (Globals.Config.LocalPlayer.ChamsType == 4)
                            {
                                r.material.SetColor("_WireColor", primarycolour);
                            }
                            if (Globals.Config.LocalPlayer.ChamsType == 5)
                            {
                                r.material.SetColor("_Color", primarycolour);
                            }
                            foreach (Material m in r.materials)
                            {

                                if (!Globals.Shaders.ContainsKey(m.name))
                                {
                                    Globals.Shaders.Add(m.name, m.shader);
                                }

                                if (m.shader != shader && Globals.Config.LocalPlayer.ChamsType != 6)
                                {
                                    m.shader = shader;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var skin in Globals.LocalPlayer?.PlayerBody?.BodySkins.Values)
                        {
                            if (skin == null)
                                continue;
                            // Loop The Renderers In The Skins
                            foreach (var r in skin.GetRenderers())
                            {
                                if (Globals.Config.LocalPlayer.ChamsType == 6)
                                    r.material = ShaderHelper.GalaxyMat;
                                if (Globals.Config.LocalPlayer.ChamsType == 0 || Globals.Config.LocalPlayer.ChamsType == 1)
                                {
                                    r.material.SetColor("_ColorVisible", secondarycolour);
                                    r.material.SetColor("_ColorBehind", primarycolour);
                                }
                                if (Globals.Config.LocalPlayer.ChamsType == 2)
                                {
                                    r.material.SetColor("_Emissioncolour", primarycolour);
                                }
                                if (Globals.Config.LocalPlayer.ChamsType == 4)
                                {
                                    r.material.SetColor("_WireColor", primarycolour);
                                }
                                if (Globals.Config.LocalPlayer.ChamsType == 5)
                                {
                                    r.material.SetColor("_Color", primarycolour);
                                }
                                foreach (Material m in r.materials)
                                {
                                    if (!Globals.Shaders.ContainsKey(m.name))
                                    {
                                        Globals.Shaders.Add(m.name, m.shader);
                                    }

                                    if (m.shader != shader && Globals.Config.LocalPlayer.ChamsType != 6)
                                    {
                                        m.shader = shader;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var r in Globals.LocalPlayer.GetComponentsInChildren<UnityEngine.Renderer>())
                    {

                        string name = r.material.name;
                        foreach (Material m in r.materials)
                        {
                            if (Globals.Shaders.ContainsKey(m.name))
                            {
                                m.shader = Globals.Shaders[m.name];

                            }
                        }
                    }
                }
            }
            catch { }
            }
    }
}
