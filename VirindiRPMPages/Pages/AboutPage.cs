using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class AboutPage : IMultiplexedPage
    {
        bool exit = false;
        string aboutstring = null;

        #region IMultiplexedPage implementation
        public void Activate()
        {

        }
        public void Deactivate()
        {

        }
        public void Update()
        {

        }
        public void ButtonDown(eMFDButton btn)
        {

        }
        public void ButtonUp(eMFDButton btn)
        {
            exit = true;
        }
        public void NotifyOfDisplay(PageDisplayBuffer b)
        {
        }
        public eMultiplexedPageRenderResult Render(PageDisplayBuffer b)
        {
            if (exit)
            {
                exit = false;
                return eMultiplexedPageRenderResult.ExitThisPage;
            }
            else
            {
                if (aboutstring == null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Virindi OS");
                    sb.AppendLine("By Virindi's Snacks & Avionics, Inc.");
                    sb.AppendLine("RasterPropMonitor by JSI");
                    sb.AppendLine("DSKY terminal by A.S.E.T.");
                    sb.Append("Version: ");
                    sb.AppendLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    sb.Append("Display Buffer Size: ");
                    sb.AppendFormat("{0} x {1}", b.Width, b.Height);
                    sb.AppendLine();
                    sb.Append("Is Monochrome: ");
                    sb.Append(b.IsMonochrome.ToString());
                    sb.AppendLine();
                    sb.Append("Keymap: ");
                    sb.AppendLine(b.InfoKeyMap.KeymapString);
                    sb.Append("Has Numpad: ");
                    sb.AppendLine(b.InfoKeyMap.HasNumpad.ToString());
                    sb.Append("Has Alphabet: ");
                    sb.AppendLine(b.InfoKeyMap.HasAlphabet.ToString());
                    sb.AppendLine("Licensed to: Jebediah Kerman (TRIAL VERSION)");
                    aboutstring = sb.ToString();
                }

                b.Append(aboutstring);


                return eMultiplexedPageRenderResult.Continue;
            }
        }
        public string GetMenuItemName()
        {
            return "About";
        }
        public void GetHeader(System.Text.StringBuilder b)
        {
            b.Append("About");
        }
        public bool IsActivationAllowed()
        {
            return true;
        }
        #endregion

    }
}

