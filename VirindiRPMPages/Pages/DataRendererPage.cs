using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class DataRendererPage : IMultiplexedPage
    {
        DataRenderers.RendererLibrary.RendererPageTemplate templ;

        public DataRendererPage(DataRenderers.RendererLibrary.RendererPageTemplate ptempl)
        {
            templ = ptempl;
        }

        #region IMultiplexedPage implementation

        bool exit = false;
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
            if (btn == eMFDButton.Esc)
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

            for (int i = 0; i < templ.Renderers.Count; ++i)
            {
                templ.Renderers[i].RenderCall(b);
            }

            return eMultiplexedPageRenderResult.Continue;
        }

        public string GetMenuItemName()
        {
            return templ.DisplayName;
        }

        public void GetHeader(StringBuilder b)
        {
            b.Append(templ.DisplayName);
        }

        public bool IsActivationAllowed()
        {
            return true;
        }

        #endregion
    }
}

