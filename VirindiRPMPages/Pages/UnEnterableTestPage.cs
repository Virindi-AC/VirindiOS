using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class UnEnterableTestPage : IMultiplexedPage
    {
        #region IMultiplexedPage implementation
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
            
        }
        public void NotifyOfDisplay(PageDisplayBuffer b)
        {
        }
        public eMultiplexedPageRenderResult Render(PageDisplayBuffer b)
        {
            return eMultiplexedPageRenderResult.ExitThisPage;
        }
        public string GetMenuItemName()
        {
            return "Test Unenterable";
        }
        public void GetHeader(System.Text.StringBuilder b)
        {
            b.Append("Test Unenterable");
        }
        public bool IsActivationAllowed()
        {
            return false;
        }
        #endregion
        
    }
}

