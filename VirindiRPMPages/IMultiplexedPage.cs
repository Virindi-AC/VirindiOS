using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages
{
    public interface IMultiplexedPage
    {
        void Activate();
        void Deactivate();
        void Update();
        void ButtonDown(eMFDButton btn);
        void ButtonUp(eMFDButton btn);
        void NotifyOfDisplay(PageDisplayBuffer b);
        eMultiplexedPageRenderResult Render(PageDisplayBuffer b);
        string GetMenuItemName();
        void GetHeader(StringBuilder b);
        bool IsActivationAllowed();
    }

    public enum eMultiplexedPageRenderResult
    {
        Continue,
        ExitThisPage,
    }
}

