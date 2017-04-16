using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public abstract class UIOnlyPage : IMultiplexedPage
    {
        bool exit = false;
        public TextUI.Control RootControl = null;

        #region IMultiplexedPage implementation
        public virtual void Activate()
        {
            if (RootControl != null)
                RootControl.OnFocus();
        }
        public virtual void Deactivate()
        {
            if (RootControl != null)
                RootControl.OnDefocus();
        }
        public virtual void Update()
        {

        }
        public virtual void ButtonDown(eMFDButton btn)
        {
            if (btn != eMFDButton.Esc)
                RootControl.ButtonDown(btn);
        }
        public virtual void ButtonUp(eMFDButton btn)
        {
            if (btn == eMFDButton.Esc)
                exit = true;
            else
                RootControl.ButtonUp(btn);
        }
        public void NotifyOfDisplay(PageDisplayBuffer b)
        {
            if (RootControl != null)
                RootControl.NotifyOfDisplay(b);
        }
        public virtual eMultiplexedPageRenderResult Render(PageDisplayBuffer b)
        {
            if (RootControl != null)
                RootControl.Render(b);

            if (exit)
            {
                exit = false;
                return eMultiplexedPageRenderResult.ExitThisPage;
            }
            else
                return eMultiplexedPageRenderResult.Continue;
        }
        public abstract string GetMenuItemName();
        public abstract void GetHeader(System.Text.StringBuilder b);

        public virtual bool IsActivationAllowed()
        {
            return true;
        }
        #endregion

    }
}
