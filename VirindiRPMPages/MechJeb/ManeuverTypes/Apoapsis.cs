using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb.ManeuverTypes
{
    public class Apoapsis : ManeuverType
    {
        VirindiRPMPages.TextUI.ScrollList mycontainer;

        TextUI.NumericEdit newAp = new VirindiRPMPages.TextUI.NumericEdit();
        TimeSelector attime = new TimeSelector(TimeReference.PERIAPSIS, TimeReference.APOAPSIS, TimeReference.X_FROM_NOW, TimeReference.EQ_DESCENDING, TimeReference.EQ_ASCENDING);

        #region ManeuverType implementation
        public void AddOptionsToUI(VirindiRPMPages.TextUI.ScrollList uilist)
        {
            mycontainer = uilist;
            mycontainer.AddControl(newAp);
            newAp.Label = "New AP: ";
            newAp.PostLabel = " km";
            mycontainer.AddControl(attime);
        }
        public string CreateManeuver()
        {
            return null;
        }
        public string GetTitle()
        {
            return "Change Apoapsis";
        }
        #endregion
        
    }
}

