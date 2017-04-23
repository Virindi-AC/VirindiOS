using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using MuMech;

namespace VirindiRPMPages.MechJeb
{
    public interface ManeuverType
    {
        void AddOptionsToUI(TextUI.ScrollList uilist);
        ManeuverParameters CreateManeuver(Orbit o, double universalTime, MechJebModuleTargetController target);
        string GetTitle();
    }
}

