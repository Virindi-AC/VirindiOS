using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class Control
    {
        public enum eNestedScrollResult
        {
            Eaten,
            NotEaten,
        }

        public enum eScrollDirection
        {
            Up,
            Down,
        }

        public delegate void delControlEvent(Control source);

        protected PageDisplayBuffer CachedDisplay = null;
        bool ihasfocus = false;
        public bool HasFocus { get { return ihasfocus; } }

        public Color SelectedColor = new Color(0.623529f, 0.807843f, 1f);
        public Color UnselectedColor = Color.white;

        public bool OverrideCanTakeFocus = true;
        public virtual bool CanTakeFocus()
        {
            return OverrideCanTakeFocus;
        }

        public virtual void NotifyOfDisplay(PageDisplayBuffer buf)
        {
            CachedDisplay = buf;
        }

        public virtual void Render(PageDisplayBuffer buf)
        {
            if (HasFocus)
                buf.CursorColor = SelectedColor;
            else
                buf.CursorColor = UnselectedColor;
            
            //CachedDisplay = buf;
            //buf.OSK_Numeric.Visible = false;
        }

        public virtual void ButtonDown(eMFDButton btn) { }
        public virtual void ButtonUp(eMFDButton btn) { }
        public void RootButtonDown(eMFDButton btn)
        {
            if (btn == eMFDButton.Down)
                NestedScroll(eScrollDirection.Down);
            else if (btn == eMFDButton.Up)
                NestedScroll(eScrollDirection.Up);
            else
                ButtonDown(btn);
        }
        public void RootButtonUp(eMFDButton btn)
        {
            if ((btn != eMFDButton.Down) && (btn != eMFDButton.Up))
                ButtonUp(btn);
        }

        public virtual void OnFocus() { ihasfocus = true; }
        public virtual void OnDefocus() { ihasfocus = false; }
        public virtual eNestedScrollResult NestedScroll(eScrollDirection dir) { return eNestedScrollResult.NotEaten; }
    }
}

