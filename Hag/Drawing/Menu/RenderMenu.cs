using Hag.Helpers;
using Hag.Menu;
using Hag.Renderer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Hag.Menu.EnumSliders;
namespace Hag.Drawing.Menu
{
    class RenderMenu : MonoBehaviour
    {
        static bool ShowGUI = true;

        public static readonly List<string> Watermarks = new List<string>() { ""};
 
        public static string Watermark;
        public static int MaxFontSize;
       



        #region Brushes
        Direct2DBrush PrimaryColour;
        Direct2DBrush SecondaryColour;
        Direct2DBrush BackgroundColour;
        Direct2DFont PrimaryFont;
        Direct2DFont SecondaryFont;
        #endregion

        /*
         Ok so these functions are ran through a public void and then all the code is in a protected void, this is just for obfuscation reasons.
        This allows us to have public voids but hide the contents of the function as normally public voids and public static voids aren't obfuscated
         */


        public void Init(Direct2DRenderer renderer)
        {
            Initialize(renderer);
        }
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            StartObfuscate();
        }
        void StartObfuscate()
        {
            
            StartCoroutine(Control());
        }
        void Initialize(Direct2DRenderer renderer)
        {
            // called in the initialization of draw to prevent render getting called without these being initialized and destroying everything.
            Color32 OriginalPrimaryColour = ColourHelper.GetColour("Menu Primary Colour");
            Color32 OriginalSecondaryColour = ColourHelper.GetColour("Menu Secondary Colour");
            Color32 OriginalBackgroundyColour = ColourHelper.GetColour("Menu Background Colour");
            PrimaryFont = renderer.CreateFont("Verdana", 18);
            SecondaryFont = renderer.CreateFont("Verdana", 16);
            PrimaryColour = renderer.CreateBrush(OriginalPrimaryColour.r, OriginalPrimaryColour.g, OriginalPrimaryColour.b, OriginalPrimaryColour.a);
            SecondaryColour = renderer.CreateBrush(OriginalSecondaryColour.r, OriginalSecondaryColour.g, OriginalSecondaryColour.b, OriginalSecondaryColour.a);
            BackgroundColour = renderer.CreateBrush(OriginalBackgroundyColour.r, OriginalBackgroundyColour.g, OriginalBackgroundyColour.b, OriginalBackgroundyColour.a);
            InitializeMenu.CurrentMenu = new SubMenu("", ""); // clear the menu
            InitializeMenu init = new InitializeMenu();
           
           
            

        }
      

        IEnumerator Control()
        {
            for (; ; )
            {
                try
                {
                    if (InitializeMenu.CurrentMenu != null)
                    {
                        if (Input.GetKeyDown(KeyCode.Insert))
                            ShowGUI = !ShowGUI;
                        if (!ShowGUI)
                            goto END;
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        bool skip = false;
                        if ((InitializeMenu.Selected is TextField))
                        {
                            TextField txt = InitializeMenu.Selected as TextField;
                            if (txt.Selected)
                                skip = true;
                        }
                        if (!skip)
                        {
                            InitializeMenu.CurrentMenu.Index++;
                            if (InitializeMenu.CurrentMenu.Index > InitializeMenu.CurrentMenu.Items.Count - 1) // exceeds max value, push it to 0
                                InitializeMenu.CurrentMenu.Index = 0;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        bool skip = false;
                        if ((InitializeMenu.Selected is TextField))
                        {
                            TextField txt = InitializeMenu.Selected as TextField;
                            if (txt.Selected)
                                skip = true;
                        }
                        if (!skip)
                        {
                            InitializeMenu.CurrentMenu.Index--;
                            if (InitializeMenu.CurrentMenu.Index < 0)
                                InitializeMenu.CurrentMenu.Index = InitializeMenu.CurrentMenu.Items.Count - 1; // exceeds 0 so push it to max value
                        }
                    }


                    foreach (Entity entity in InitializeMenu.CurrentMenu.Items)
                    {

                        if (InitializeMenu.CurrentMenu.Index == InitializeMenu.CurrentMenu.Items.IndexOf(entity))
                            InitializeMenu.Selected = entity;
                        if (entity != InitializeMenu.Selected)
                            continue;

                    }



                    if (!(InitializeMenu.Selected is TextField) && Input.GetKeyDown(KeyCode.Backspace) && InitializeMenu.MenuHistory.Count > 1)
                    {
                        InitializeMenu.CurrentMenu = InitializeMenu.MenuHistory[InitializeMenu.MenuHistory.Count - 2];
                        InitializeMenu.MenuHistory.Remove(InitializeMenu.MenuHistory.Last<SubMenu>());
                        goto END;  // make sure we dont click through
                    }

                    if (InitializeMenu.Selected is SubMenu)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            InitializeMenu.CurrentMenu = InitializeMenu.MenuHistory[InitializeMenu.MenuHistory.Count - 2];
                            InitializeMenu.MenuHistory.Remove(InitializeMenu.MenuHistory.Last<SubMenu>());
                            goto END;  // make sure we dont click through
                        }
                        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Return)))
                        {
                            InitializeMenu.CurrentMenu = InitializeMenu.Selected as SubMenu;
                            InitializeMenu.MenuHistory.Add(InitializeMenu.Selected as SubMenu);
                            goto END;  // make sure we dont click through
                        }
                    }

                    if (InitializeMenu.Selected is Keybind)
                    {
                        Keybind bind = InitializeMenu.Selected as Keybind;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                            bind.Value = KeyCode.None;

                        if (bind.Value == KeyCode.None)
                            bind.Value = SetKey();

                    }


                    if (InitializeMenu.Selected is Toggle && Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Toggle toggle = InitializeMenu.Selected as Toggle;
                        toggle.Value = true;
                    }

                    if (InitializeMenu.Selected is Toggle && Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Toggle toggle = InitializeMenu.Selected as Toggle;
                        toggle.Value = false;
                    }

                    if (InitializeMenu.Selected is Button && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Return)))
                    {
                        Button button = InitializeMenu.Selected as Button;
                        button.Method();
                    }

                    if (InitializeMenu.Selected is IntSlider && Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        IntSlider slider = InitializeMenu.Selected as IntSlider;
                        int result = slider.Value + slider.IncrementValue;

                        if (result > slider.MaxValue)
                            slider.Value = slider.MaxValue;
                        else
                            slider.Value = result;
                    }

                    if (InitializeMenu.Selected is IntSlider && Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        IntSlider slider = InitializeMenu.Selected as IntSlider;
                        int result = slider.Value - slider.IncrementValue;

                        if (result < slider.MinValue)
                            slider.Value = slider.MinValue;
                        else
                            slider.Value = result;
                    }

                    if (InitializeMenu.Selected is FloatSlider && Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        FloatSlider slider = InitializeMenu.Selected as FloatSlider;
                        float result = slider.Value + slider.IncrementValue;

                        if (result > slider.MaxValue)
                            slider.Value = slider.MaxValue;
                        else
                            slider.Value = result;
                    }

                    if (InitializeMenu.Selected is FloatSlider && Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        FloatSlider slider = InitializeMenu.Selected as FloatSlider;
                        float result = slider.Value - slider.IncrementValue;

                        if (result < slider.MinValue)
                            slider.Value = slider.MinValue;
                        else
                            slider.Value = result;
                    }

                    if (InitializeMenu.Selected is TextAlignmentSlider)
                    {
                        TextAlignmentSlider slider = InitializeMenu.Selected as TextAlignmentSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }
                    if (InitializeMenu.Selected is OpacityTypeSlider)
                    {
                        OpacityTypeSlider slider = InitializeMenu.Selected as OpacityTypeSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }

                    if (InitializeMenu.Selected is CrosshairTypeSlider)
                    {
                        CrosshairTypeSlider slider = InitializeMenu.Selected as CrosshairTypeSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }


                    if (InitializeMenu.Selected is SkeletonTypeSlider)
                    {
                        SkeletonTypeSlider slider = InitializeMenu.Selected as SkeletonTypeSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }

                    if (InitializeMenu.Selected is FovTypeSlider)
                    {
                        FovTypeSlider slider = InitializeMenu.Selected as FovTypeSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }

                    if (InitializeMenu.Selected is BoneTypeSlider)
                    {
                        BoneTypeSlider slider = InitializeMenu.Selected as BoneTypeSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }
                    if (InitializeMenu.Selected is ChamTypeSlider)
                    {
                        ChamTypeSlider slider = InitializeMenu.Selected as ChamTypeSlider;
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            int result = slider.Value + slider.IncrementValue;
                            if (result > slider.MaxValue)
                                slider.Value = slider.MaxValue;
                            else
                                slider.Value = result;
                            slider.GetValue();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            int result = slider.Value - slider.IncrementValue;

                            if (result < slider.MinValue)
                                slider.Value = slider.MinValue;
                            else
                                slider.Value = result;

                            slider.GetValue();
                        }
                    }
                    if (InitializeMenu.Selected is TextField)
                    {
                        TextField txt = InitializeMenu.Selected as TextField;
                        bool takeinput = true;
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            takeinput = false;
                            txt.Selected = !txt.Selected;
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                            txt.Selected = true;
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                            txt.Selected = false;
                        if (txt.Selected)
                        {
                            if (Input.GetKeyDown(KeyCode.Backspace) && txt.Value.Length != 0)
                            {
                                txt.Value = txt.Value.Substring(0, txt.Value.Length - 1);
                            }
                            else
                            {
                                if (takeinput)
                                {
                                    string input = Input.inputString;
                                    if (!string.IsNullOrEmpty(input))
                                        txt.Value += input;
                                }

                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.Backspace) && InitializeMenu.MenuHistory.Count > 1)
                            {
                                InitializeMenu.CurrentMenu = InitializeMenu.MenuHistory[InitializeMenu.MenuHistory.Count - 2];
                                InitializeMenu.MenuHistory.Remove(InitializeMenu.MenuHistory.Last<SubMenu>());
                                goto END; // make sure we dont click through
                            }
                        }
                    }


                }
                catch { }
            
                END:
               yield return new WaitForEndOfFrame();
            }
        }
        KeyCode SetKey()
        {
            KeyCode Key = new KeyCode();
            Event e = Event.current;
            if (e.keyCode != KeyCode.RightArrow)
            {
                Key = e.keyCode;


            }
            else
            {
                Key = KeyCode.None;

            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                Key = KeyCode.Mouse0;

            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Key = KeyCode.Mouse1;

            }
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                Key = KeyCode.Mouse2;

            }
            if (Input.GetKeyDown(KeyCode.Mouse3))
            {
                Key = KeyCode.Mouse3;

            }
            if (Input.GetKeyDown(KeyCode.Mouse4))
            {
                Key = KeyCode.Mouse4;

            }
            if (Input.GetKeyDown(KeyCode.Mouse5))
            {
                Key = KeyCode.Mouse5;

            }
            if (Input.GetKeyDown(KeyCode.Mouse6))
            {
                Key = KeyCode.Mouse6;

            }
            return Key;
        }
        public void Render(Direct2DRenderer renderer)
        {

            Renderer(renderer);
        }
        void DrawHistory(Direct2DRenderer renderer)
        {
           
                string text = "";
                
                    foreach (SubMenu subMenu in InitializeMenu.MenuHistory)
                    {
                        if (subMenu != null)
                        {
                            if (subMenu == InitializeMenu.MenuHistory.Last<SubMenu>())
                            {
                                text += subMenu.Name + " v ";
                            }
                            else
                            {
                                text = text + subMenu.Name + " > ";
                            }
                        }
                    }
                
                renderer.DrawTextMenu(text, Globals.Config.Menu.MenuPos.x - 10, Globals.Config.Menu.MenuPos.y - 23, 12, 20,Globals.Config.Menu.DrawBackground,PrimaryFont, PrimaryColour,BackgroundColour);
           
        }
        void DrawDescription(Direct2DRenderer renderer)
        {
            if (InitializeMenu.Selected.Description != null)
                renderer.DrawTextMenu(InitializeMenu.Selected.Description, Globals.Config.Menu.MenuPos.x - 10, Globals.Config.Menu.MenuPos.y + (20f * (float)InitializeMenu.CurrentMenu.Items.Count) + 3, 12,20,Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour,BackgroundColour);
        }
        
       
        protected void Renderer(Direct2DRenderer renderer)
        {
            Color32 OriginalBackgroundyColour = ColourHelper.GetColour("Menu Background Colour");
            Color32 OriginalPrimaryColour = ColourHelper.GetColour("Menu Primary Colour");
            Color32 OriginalSecondaryColour = ColourHelper.GetColour("Menu Secondary Colour");
            PrimaryColour.Color = new Direct2DColor(OriginalPrimaryColour.r, OriginalPrimaryColour.g, OriginalPrimaryColour.b, OriginalPrimaryColour.a);
            SecondaryColour.Color = new Direct2DColor(OriginalSecondaryColour.r, OriginalSecondaryColour.g, OriginalSecondaryColour.b, OriginalSecondaryColour.a);
            BackgroundColour.Color = new Direct2DColor(OriginalBackgroundyColour.r, OriginalBackgroundyColour.g, OriginalBackgroundyColour.b, OriginalBackgroundyColour.a);
            renderer.DrawText(Watermark, 5, 5, 12, PrimaryFont, PrimaryColour);
           
            if (!ShowGUI)
                return;
            foreach (Entity entity in InitializeMenu.CurrentMenu.Items)
            {
               
                Draw(entity, renderer);
            }
            DrawDescription(renderer);
            DrawHistory(renderer);
          
        }
        void Draw(Entity entity, Direct2DRenderer renderer)
        {
            try
            {
                if (entity is SubMenu)
                {

                    if (InitializeMenu.Selected == entity)
                    {
                        renderer.DrawTextMenu(string.Concat(">", entity.Name), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat(">", entity.Name), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                    return;
                }
                if (entity is TextAlignmentSlider)
                {
                    TextAlignmentSlider slider = entity as TextAlignmentSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is OpacityTypeSlider)
                {
                    OpacityTypeSlider slider = entity as OpacityTypeSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is SkeletonTypeSlider)
                {
                    SkeletonTypeSlider slider = entity as SkeletonTypeSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is FovTypeSlider)
                {
                    FovTypeSlider slider = entity as FovTypeSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is CrosshairTypeSlider)
                {
                    CrosshairTypeSlider slider = entity as CrosshairTypeSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is BoneTypeSlider)
                {
                    BoneTypeSlider slider = entity as BoneTypeSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is ChamTypeSlider)
                {
                    ChamTypeSlider slider = entity as ChamTypeSlider;
                    string value = "";
                    if (slider.Value == slider.MinValue)
                        value = string.Concat("[", slider.GetValue(), "] >");
                    if (slider.Value == slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "]");
                    if (slider.Value > slider.MinValue && slider.Value < slider.MaxValue)
                        value = string.Concat("< [", slider.GetValue(), "] >");
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is IntSlider)
                {
                    IntSlider slider = entity as IntSlider;
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", slider.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", slider.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is Toggle)
                {
                    Toggle tog = entity as Toggle;
                    string ToggleStr = tog.Value ? "Enabled" : "Disabled";
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", ToggleStr), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", ToggleStr), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is FloatSlider)
                {
                    FloatSlider slider = entity as FloatSlider;
                    if (InitializeMenu.Selected == entity)
                    {

                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", Math.Round(slider.Value, 2)), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", Math.Round(slider.Value, 2)), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is Keybind)
                {
                    Keybind bind = entity as Keybind;
                    if (InitializeMenu.Selected == entity)
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", bind.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", bind.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is TextField)
                {
                    TextField text = entity as TextField;
                    if (InitializeMenu.Selected == entity && !text.Selected)
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", text.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    if (InitializeMenu.Selected == entity && text.Selected)
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": > ", text.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    if (InitializeMenu.Selected != entity && !text.Selected)
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name, ": ", text.Value), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
                if (entity is Button)
                {
                    if (InitializeMenu.Selected == entity)
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 14, 20, Globals.Config.Menu.DrawBackground, PrimaryFont, PrimaryColour, BackgroundColour);
                    }
                    else
                    {
                        renderer.DrawTextMenu(string.Concat("-", entity.Name), Globals.Config.Menu.MenuPos.x, Globals.Config.Menu.MenuPos.y + (20 * InitializeMenu.CurrentMenu.Items.IndexOf(entity)), 12, 20, Globals.Config.Menu.DrawBackground, SecondaryFont, SecondaryColour, BackgroundColour);
                    }
                }
            }
            catch { }
            }
    }
}
