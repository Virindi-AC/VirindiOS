using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class MenuPage : IMultiplexedPage
    {
        string VersionString = null;
        string Name = "???";
        List<IMultiplexedPage> Pages = new List<IMultiplexedPage>();
        int CurrentPageID = -1;
        int CursorPosition = 0;
        FXGroup errorsound;
        bool doexit = false;
        public bool CanExit = true;

        public static MenuPage CreateMainMenu()
        {
            MenuPage ret = new MenuPage();
            ret.Name = "Main";
            ret.CanExit = false;

            DataRenderers.RendererLibrary.Initialize();
            for (int i = 0; i < DataRenderers.RendererLibrary.PageTemplates.Count; ++i)
            {
                ret.Pages.Add(new DataRendererPage(DataRenderers.RendererLibrary.PageTemplates[i]));
            }

            ret.Pages.Add(new MechJeb.ManeuverPlanner());
            ret.Pages.Add(CreateDebugMenu());
            return ret;
        }

        public static MenuPage CreateDebugMenu()
        {
            MenuPage ret = new MenuPage();
            ret.Name = "Debug";
            ret.Pages.Add(new AboutPage());
            ret.Pages.Add(new EngineeringData());
            ret.Pages.Add(new UITestPage());
            ret.Pages.Add(new KeyboardTest());
            ret.Pages.Add(new UnEnterableTestPage());
            return ret;
        }

        #region IMultiplexedPage implementation

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }

        public void Update()
        {
            if (CurrentPageID != -1)
                Pages[CurrentPageID].Update();
        }

        public void ButtonDown(eMFDButton btn)
        {
            if (CurrentPageID != -1)
                Pages[CurrentPageID].ButtonDown(btn);
        }

        public void ButtonUp(eMFDButton btn)
        {
            if (CurrentPageID != -1)
                Pages[CurrentPageID].ButtonUp(btn);
            else
            {
                switch (btn)
                {
                    case eMFDButton.Down:
                        CursorPosition++;
                        if (CursorPosition >= Pages.Count)
                            CursorPosition = Pages.Count - 1;
                        break;
                    case eMFDButton.Up:
                        CursorPosition--;
                        if (CursorPosition < 0)
                            CursorPosition = 0;
                        break;
                    case eMFDButton.Ok:
                        if (Pages[CursorPosition].IsActivationAllowed())
                        {
                            CurrentPageID = CursorPosition;
                            Pages[CursorPosition].Activate();
                        }
                        else
                        {
                            if (errorsound != null)
                                errorsound.audio.Play();
                        }
                        break;
                    case eMFDButton.Esc:
                        {
                            if (CanExit)
                                doexit = true;
                        }
                        break;
                }
            }
        }

        public void NotifyOfDisplay(PageDisplayBuffer b)
        {
            for (int i = 0; i < Pages.Count; ++i)
                Pages[i].NotifyOfDisplay(b);
        }

        public eMultiplexedPageRenderResult Render(PageDisplayBuffer pagebuilder)
        {
            if (errorsound == null)
            {
                errorsound = JSI.JUtil.SetupIVASound(pagebuilder.InfoInternalProp, "ASET/ASET_Props/Sounds/beep_G_short_x1", 0.5f, false);
            }

            if (doexit)
            {
                doexit = false;
                return eMultiplexedPageRenderResult.ExitThisPage;
            }

            if (CurrentPageID != -1)
            {
                eMultiplexedPageRenderResult res = Pages[CurrentPageID].Render(pagebuilder);
                if (res == eMultiplexedPageRenderResult.ExitThisPage)
                {
                    CurrentPageID = -1;
                    Pages[CurrentPageID].Deactivate();
                }
                else
                    return eMultiplexedPageRenderResult.Continue;
            }
                
            for (int i = 0; i < Pages.Count; ++i)
            {
                if (i == CursorPosition)
                    pagebuilder.Append('>');
                else
                    pagebuilder.Append(' ');
                if (Pages[i].IsActivationAllowed())
                {
                    pagebuilder.Append(' ');
                    pagebuilder.Append(Pages[i].GetMenuItemName());
                    pagebuilder.Append(' ');
                }
                else
                {
                    pagebuilder.CursorColor = Color.red;
                    pagebuilder.Append('X');
                    pagebuilder.CursorColor = Color.white;
                    pagebuilder.Append(Pages[i].GetMenuItemName());
                    pagebuilder.CursorColor = Color.red;
                    pagebuilder.Append('X');
                    pagebuilder.CursorColor = Color.white;
                }
                if (i == CursorPosition)
                    pagebuilder.Append('<');
                pagebuilder.AppendLine();

            }

            if (VersionString == null)
            {
                StringBuilder sb = new StringBuilder();
                string virindi = "Virindi OS v." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                for (int i = 0; i < (pagebuilder.Width - virindi.Length) / 2; ++i)
                    sb.Append('-');
                sb.Append(virindi);
                for (int i = 0; i < (pagebuilder.Width - virindi.Length) / 2 + 1; ++i)
                    sb.Append('-');
                sb.AppendLine();
                VersionString = sb.ToString();
            }
            pagebuilder.CursorX = -1;
            pagebuilder.CursorY = pagebuilder.Height - 1;
            pagebuilder.CursorColor = Color.white;
            pagebuilder.Append(VersionString);

            return eMultiplexedPageRenderResult.Continue;
        }

        public string GetMenuItemName()
        {
            return Name + " (Menu)";
        }

        public void GetHeader(StringBuilder b)
        {
            b.Append(Name);
            if (CurrentPageID != -1)
            {
                b.Append(" > ");
                Pages[CurrentPageID].GetHeader(b);
            }
        }

        public bool IsActivationAllowed()
        {
            return true;
        }

        #endregion
    }
}

