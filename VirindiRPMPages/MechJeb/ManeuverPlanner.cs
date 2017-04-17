using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb
{
    public class ManeuverPlanner : TextUI.UIOnlyPage
    {
        TextUI.ScrollList rootlist;
        TextUI.ChoiceBox maneuvertypechoice;
        TextUI.ScrollList maneuvercustomitemlist;
        TextUI.LinkButton b_addnode;
        TextUI.LinkButton b_addnodeandexecute;
        TextUI.LinkButton b_stopexecution;
        TextUI.LinkButton b_deletenodes;
        ManeuverType currentmaneuver = null;

        public ManeuverPlanner()
        {
            rootlist = new VirindiRPMPages.TextUI.ScrollList();
            RootControl = rootlist;

            ManeuverTypeFactory.Initialize();
            maneuvertypechoice = new VirindiRPMPages.TextUI.ChoiceBox();
            for (int i = 0; i < ManeuverTypeFactory.ManeuverTypeNames.Count; ++i)
            {
                maneuvertypechoice.Options.Add(ManeuverTypeFactory.ManeuverTypeNames[i]);
            }
            maneuvertypechoice.Label = "M. Type: ";
            maneuvertypechoice.OnChanged += Maneuvertypechoice_OnChanged;
            rootlist.AddControl(maneuvertypechoice);

            maneuvercustomitemlist = new VirindiRPMPages.TextUI.ScrollList();
            rootlist.AddControl(maneuvercustomitemlist);

            b_addnode = new VirindiRPMPages.TextUI.LinkButton();
            b_addnode.Label = "[Create Node]";
            rootlist.AddControl(b_addnode);

            b_addnodeandexecute = new VirindiRPMPages.TextUI.LinkButton();
            b_addnodeandexecute.Label = "[Create Node & Exec.]";
            rootlist.AddControl(b_addnodeandexecute);

            b_stopexecution = new VirindiRPMPages.TextUI.LinkButton();
            b_stopexecution.Label = "[Stop Node Exec.]";
            rootlist.AddControl(b_stopexecution);

            b_deletenodes = new VirindiRPMPages.TextUI.LinkButton();
            b_deletenodes.Label = "[Delete All Nodes]";
            rootlist.AddControl(b_deletenodes);
        }

        void Maneuvertypechoice_OnChanged (VirindiRPMPages.TextUI.Control source)
        {
            currentmaneuver = null;
            maneuvercustomitemlist.Clear();

            if ((maneuvertypechoice.SelectedIndex >= 0) && (maneuvertypechoice.SelectedIndex < maneuvertypechoice.Options.Count))
            {
                string newtype_str = maneuvertypechoice.Options[maneuvertypechoice.SelectedIndex];
                currentmaneuver = ManeuverTypeFactory.CreateManeuverType(newtype_str);
                if (currentmaneuver == null)
                {
                    KSPLog.print("VRPM-MJ: Cannot create maneuver of type '" + newtype_str + "'!!");
                    return;
                }
                currentmaneuver.AddOptionsToUI(maneuvercustomitemlist);
            }
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

