using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb
{
    public class ManeuverPlanner : TextUI.UIOnlyPage
    {
        TextUI.ScrollList rootlist;
        public ManeuverPlanner()
        {
            rootlist = new VirindiRPMPages.TextUI.ScrollList();
            RootControl = rootlist;

            ManeuverTypeFactory.Initialize();

        }

        public override string GetMenuItemName()
        {
            return "MechJeb: Maneuver Planner";
        }
        public override void GetHeader(StringBuilder b)
        {
            b.Append("MechJeb: Maneuver Planner");
        }
    }
}

