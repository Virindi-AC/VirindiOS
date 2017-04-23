using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using MuMech;

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
        public ManeuverParameters CreateManeuver(Orbit o, double universalTime, MechJebModuleTargetController target)
        {
            double inputnumber = newAp.InputNumber * 1000d; //km
            double UT = attime.ComputeManeuverTime(o, universalTime, target);
            if (o.referenceBody.Radius + inputnumber < o.Radius(UT))
            {
                string burnAltitude = MuUtils.ToSI(o.Radius(UT) - o.referenceBody.Radius) + "m";
                throw new OperationException("new apoapsis cannot be lower than the altitude of the burn (" + burnAltitude + ")");
            }

            return new ManeuverParameters(OrbitalManeuverCalculator.DeltaVToChangeApoapsis(o, UT, inputnumber + o.referenceBody.Radius), UT);
        }
        public string GetTitle()
        {
            return "Change Apoapsis";
        }
        #endregion
        
    }
}

