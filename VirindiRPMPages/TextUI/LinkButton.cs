using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class LinkButton : Control
    {
        public delegate void delOnClick();
        public event delOnClick OnClick;

        public string Label = "";

        public override void ButtonUp(eMFDButton btn)
        {
            base.ButtonUp(btn);

            if (btn == eMFDButton.Ok && OnClick != null)
                OnClick();
        }

        public override void Render(PageDisplayBuffer buf)
        {
            base.Render(buf);

            buf.AppendLine(Label);
        }
    }
}

