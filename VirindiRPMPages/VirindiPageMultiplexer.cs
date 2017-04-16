using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages
{
    public class VirindiPageMultiplexer : InternalModule
    {
        public IMultiplexedPage CurrentPage;
        PageDisplayBuffer Buffer = null;
        MFDKeyMap keymapobj = null;

        [KSPField]
        public string keymap;
        [KSPField]
        public int hpad;
        [KSPField]
        public int vpad;
        [KSPField]
        public bool ismonochrome = false;

        public void Start()
        {
            CurrentPage = Pages.MenuPage.CreateMainMenu();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (CurrentPage != null)
                CurrentPage.Update();
        }

        public void ButtonClickProcessor(int buttonID)
        {
            if (CurrentPage != null)
            {
                CurrentPage.ButtonDown(keymapobj.Translate(buttonID));
            }
        }

        public void ButtonReleaseProcessor(int buttonID)
        {
            if (CurrentPage != null)
            {
                CurrentPage.ButtonUp(keymapobj.Translate(buttonID));
            }
        }

        public string ShowPage(int screenWidth, int screenHeight)
        {
            if (keymapobj == null)
                keymapobj = MFDKeyMap.GetMap(keymap);

            if (Buffer == null
                || (Buffer.Width != screenWidth - 2 * hpad)
                || (Buffer.Height != screenHeight - 2 * vpad - 2)
                || (Buffer.IsMonochrome != ismonochrome)
            )
            {
                Buffer = new PageDisplayBuffer(screenWidth - 2 * hpad, screenHeight - 2 * vpad - 2, ismonochrome);
                Buffer.InfoInternalProp = internalProp;
                Buffer.InfoKeyMap = keymapobj;
                Buffer.InfoMainController = this;

                if (CurrentPage != null)
                    CurrentPage.NotifyOfDisplay(Buffer);
            }

            //string res = Environment.NewLine + " " + keymap + ", " + hpad.ToString() + ", " + vpad.ToString() + Environment.NewLine;
            string res;
            if (CurrentPage != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < vpad; ++i)
                    sb.AppendLine();
                for (int i = 0; i < hpad; ++i)
                    sb.Append(' ');
                CurrentPage.GetHeader(sb);
                sb.AppendLine();
                for (int i = 0; i < hpad; ++i)
                    sb.Append(' ');
                for (int i = 0; i < screenWidth - 2 * hpad; ++i)
                    sb.Append('-');
                sb.AppendLine();

                Buffer.Clear();
                CurrentPage.Render(Buffer);
                Buffer.AppendToBuilder(sb, hpad);

                res = sb.ToString();
            }
            else
                res = "?????";
            return res;
        }
    }
}

