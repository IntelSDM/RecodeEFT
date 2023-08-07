using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hag.Renderer;
using SharpDX;
using SharpDX.Direct2D1;
using UnityEngine;
using EFT.Interactive;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Hag.Helpers;


namespace Hag.Drawing
{
    class Draw
    {
        #region import
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        #endregion
        public static OverlayWindow Overlay;
        private static IntPtr GameWindow;
        Direct2DRenderer Renderer;
        private static int DrawTime;
        public const string WINDOW_NAME = "EscapeFromTarkov";
        Rect windowSize = new Rect();
        public static Direct2DColor CrosshairColor;
        Direct2DBrush whiteSolid;
        Direct2DFont infoFont;
        Direct2DFont Tahoma;
        Direct2DFont Tahoma2;
        Direct2DFont Tahoma3;
        Direct2DFont Tahoma4;
        int cnt = 0;
        public void Start()
        {

            GameWindow = FindWindow(null, WINDOW_NAME);

            GetWindowRect(GameWindow, ref windowSize);
            OverlayCreationOptions overlayOptions = new OverlayCreationOptions()
            {
                BypassTopmost = true,
                Height = windowSize.Bottom - windowSize.Top,
                Width = windowSize.Right - windowSize.Left,
                WindowTitle = HelperMethods.GenerateRandomString(5, 11),
                X = windowSize.Left,
                Y = windowSize.Top
            };

            StickyOverlayWindow overlay = new StickyOverlayWindow(GameWindow, overlayOptions);
            Overlay = overlay;
            var rendererOptions = new Direct2DRendererOptions()
            {
                AntiAliasing = true,
                Hwnd = overlay.WindowHandle,
                MeasureFps = true,
                VSync = false

            };

            Renderer = new Direct2DRenderer(rendererOptions);
            whiteSolid = Renderer.CreateBrush(255, 255, 255, 255);
            //   var RedSolid = Renderer.CreateBrush(255, 0, 0, 200);
            infoFont = Renderer.CreateFont("Consolas", 11);
            Tahoma = Renderer.CreateFont("Tahoma", 10);
            Tahoma2 = Renderer.CreateFont("Tahoma", 9);
            Tahoma3 = Renderer.CreateFont("Tahoma", 12);
            Tahoma4 = Renderer.CreateFont("Tahoma", 10);
            Globals.MenuInstance.Init(Renderer);
            //   Task.Factory.StartNew(() => Render());

            /*       new Thread(delegate ()
                   {

                       Render();
                   }).Start();*/
          
            Thread renderthread = new Thread(() => Render());
            renderthread.Start();
            renderthread.Priority = System.Threading.ThreadPriority.Highest;
        }
        private void Render()
        {
            Esp.ExfilEsp exfilesp = new Esp.ExfilEsp();
            Esp.PlayerEsp playeresp = new Esp.PlayerEsp();
            Esp.GrenadeEsp grenadeesp = new Esp.GrenadeEsp();
            Esp.ItemEsp itemesp = new Esp.ItemEsp();
            Esp.ContainerEsp containeresp = new Esp.ContainerEsp();
            Esp.CorpseEsp corpseesp = new Esp.CorpseEsp();
            Aimbot.Aimbot aimbot = new Aimbot.Aimbot();
            Misc.Hud hud = new Misc.Hud();
            Esp.Radar radar = new Esp.Radar();
            Direct2DColor primarycolour = new Direct2DColor(255,0,0,255);
            while (true)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                #region Start

                try
                {
                    Renderer.BeginScene();
                    Renderer.ClearScene();
                }
                catch { }

                #endregion
                try
                {

                    exfilesp.Draw(Renderer, Tahoma);
                           corpseesp.Draw(Renderer, Tahoma);
                            itemesp.Draw(Renderer, Tahoma);
                      containeresp.Draw(Renderer, Tahoma);
                      grenadeesp.Draw(Renderer, Tahoma);
                    playeresp.Draw(Renderer, Tahoma);
                 aimbot.Draw(Renderer, Tahoma);
                    hud.Draw(Renderer,Tahoma);
                    radar.DrawRadarEnemy(Renderer, Tahoma);
                }
                catch { }
                Globals.MenuInstance.Render(Renderer);
                Renderer.DrawTextCentered(string.Concat("Game: ", (int)(1.0f / Time.deltaTime), "FPS | Overlay: ", Renderer.FPS, "FPS ", "[", DrawTime, "ms] ", windowSize.Right - windowSize.Left, "x", windowSize.Bottom - windowSize.Top, "-",Globals.IPAddress,":",Globals.Port), (windowSize.Right - windowSize.Left) / 2, 5, 13, Tahoma, primarycolour);
                //   Renderer.DrawTextCentered($"Game: | Overlay: {Renderer.FPS}[{DrawTime}ms], {windowSize.Right - windowSize.Left}x{ windowSize.Bottom - windowSize.Top}", Globals.MainCamera.pixelWidth / 2,5, Tahoma, primarycolour);
                #region End
                try
                {
                    Renderer.EndScene();
                }
                catch { }
                #endregion
                timer.Stop();
            
            }


        }
    }
}
