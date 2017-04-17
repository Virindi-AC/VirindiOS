using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class ChoiceBox : Control
    {
        public List<string> Options = new List<string>();
        public int SelectedIndex = -1;

        public override void ButtonUp(eMFDButton btn)
        {
            base.ButtonUp(btn);

            if (btn == eMFDButton.Ok && Options.Count > 0)
            {
                ++SelectedIndex;
                if (SelectedIndex >= Options.Count)
                    SelectedIndex = 0;
            }
        }

        public override void Render(PageDisplayBuffer buf)
        {
            base.Render(buf);

            buf.CursorColor = Color.white;
            if (SelectedIndex < 0 || SelectedIndex >= Options.Count)
                buf.AppendLine("[---SELECT---]");
            else
            {
                buf.Append("[");
                buf.Append(Options[SelectedIndex]);
                buf.AppendLine("]");
            }
        }


    }
}

