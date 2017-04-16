using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class Control
    {
        protected PageDisplayBuffer CachedDisplay = null;
        bool ihasfocus = false;
        public bool HasFocus { get { return ihasfocus; } }

        public virtual void NotifyOfDisplay(PageDisplayBuffer buf)
        {
            CachedDisplay = buf;
        }

        public virtual void Render(PageDisplayBuffer buf)
        {
            //CachedDisplay = buf;
            //buf.OSK_Numeric.Visible = false;
        }

        public virtual void ButtonDown(eMFDButton btn) { }
        public virtual void ButtonUp(eMFDButton btn) { }
        public virtual void OnFocus() { ihasfocus = true; }
        public virtual void OnDefocus() { ihasfocus = false; }
    }
}

