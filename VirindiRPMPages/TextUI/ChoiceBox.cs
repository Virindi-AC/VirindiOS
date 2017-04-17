using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class ChoiceBox : Control
    {
        public string Label = "";
        public List<string> Options = new List<string>();
        public int SelectedIndex = -1;

        public override void ButtonUp(eMFDButton btn)
        {
            base.ButtonUp(btn);

            if (Options.Count == 0)
                SelectedIndex = -1;
            else if (btn == eMFDButton.Ok)
            {
                ++SelectedIndex;
                if (SelectedIndex >= Options.Count)
                    SelectedIndex = -1;
            }
            else if (btn == eMFDButton.Right)
            {
                ++SelectedIndex;
                if (SelectedIndex >= Options.Count)
                    SelectedIndex = Options.Count - 1;
            }
            else if (btn == eMFDButton.Left)
            {
                --SelectedIndex;
                if (SelectedIndex < -1)
                    SelectedIndex = -1;
            }
        }

        public override void Render(PageDisplayBuffer buf)
        {
            base.Render(buf);

            buf.Append(Label);
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

