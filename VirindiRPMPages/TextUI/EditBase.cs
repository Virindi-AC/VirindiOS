using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class EditBase : Control
    {
        public string Label = "";
        string iinputstring = "";
        protected bool NewlyFocused = false;

        protected string InputString
        {
            get
            {
                return iinputstring;
            }
            set
            {
                iinputstring = value;
                NewlyFocused = false;
            }
        }

        protected void AppendToInputString(char s)
        {
            if (NewlyFocused)
            {
                InputString = "";
                NewlyFocused = false;
            }
            InputString += s;
        }

        public override void Render(PageDisplayBuffer buf)
        {
            base.Render(buf);

            buf.Append(Label);
            buf.Append(" [");
            buf.Append(InputString);
            while (buf.CursorX < buf.Width - 2)
            {
                buf.Append(' ');
            }
            buf.Append(']');
            buf.AppendLine();

            buf.CursorColor = Color.white;
        }

        public override void OnFocus()
        {
            base.OnFocus();

            if (CachedDisplay != null && !CachedDisplay.InfoKeyMap.HasButton(eMFDButton.Backspace))
                NewlyFocused = true;
        }


    }
}

