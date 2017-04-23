using System;

namespace VirindiRPMPages.TextUI
{
    public class Label : LinkButton
    {
        public override bool CanTakeFocus()
        {
            return false;
        }
    }
}

