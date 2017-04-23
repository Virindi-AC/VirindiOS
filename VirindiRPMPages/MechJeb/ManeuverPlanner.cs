using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using MuMech;
using System.Linq;

namespace VirindiRPMPages.MechJeb
{
    public class ManeuverPlanner : TextUI.UIOnlyPage
    {
        TextUI.ScrollList rootlist;
        //TextUI.Label statuslabel;
        TextUI.ChoiceBox maneuvertypechoice;
        TextUI.ScrollList maneuvercustomitemlist;
        TextUI.LinkButton b_addnode;
        TextUI.LinkButton b_executenext;
        TextUI.LinkButton b_deletenodes;
        TextUI.Label errorlabel;
        TextUI.Label pendingnodeslabel;
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
            b_addnode.OnClick += B_addnode_OnClick;

            b_executenext = new VirindiRPMPages.TextUI.LinkButton();
            b_executenext.Label = "[Exec. Next Node]";
            rootlist.AddControl(b_executenext);
            b_executenext.OnClick += B_executenext_OnClick;

            b_deletenodes = new VirindiRPMPages.TextUI.LinkButton();
            b_deletenodes.Label = "[Delete All Nodes]";
            b_deletenodes.OnClick+= B_deletenodes_OnClick;
            rootlist.AddControl(b_deletenodes);

            //statuslabel = new VirindiRPMPages.TextUI.Label();
            //statuslabel.Label = "Status: Idle";
            //rootlist.AddControl(statuslabel);

            errorlabel = new VirindiRPMPages.TextUI.Label();
            errorlabel.Label = "---";
            errorlabel.UnselectedColor = Color.green;
            rootlist.AddControl(errorlabel);

            pendingnodeslabel = new VirindiRPMPages.TextUI.Label();
            pendingnodeslabel.Label = "";
            rootlist.AddControl(pendingnodeslabel);
        }


        void Maneuvertypechoice_OnChanged (VirindiRPMPages.TextUI.Control source)
        {
            currentmaneuver = null;
            maneuvercustomitemlist.Clear();
            errorlabel.Label = "";

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

        public MuMech.MechJebCore GetJeb(Vessel v)
        {
            return MuMech.VesselExtensions.GetMasterMechJeb(v);
        }

        public List<ManeuverNode> GetManeuverNodes(MuMech.MechJebCore jeb)
        {
            Vessel v = FlightGlobals.ActiveVessel;
            if (v == null) return new List<ManeuverNode>();

            MechJebModuleLandingPredictions predictor = jeb.GetComputerModule<MechJebModuleLandingPredictions>();
            if (predictor == null) return v.patchedConicSolver.maneuverNodes;
            else return v.patchedConicSolver.maneuverNodes.Where(n => n != predictor.aerobrakeNode).ToList();
        }

        void AddNode ()
        {
            if (currentmaneuver == null)
            {
                errorlabel.Label = "You must select a m. type.";
                return;
            }

            Vessel v = FlightGlobals.ActiveVessel;
            MuMech.MechJebCore jeb = GetJeb(v);
            List<ManeuverNode> maneuverNodes = GetManeuverNodes(jeb);
            bool anyNodeExists = maneuverNodes.Any();

            double UT = jeb.vesselState.time;
            Orbit o = v.orbit;
            if (anyNodeExists)
            {
                ManeuverNode last = maneuverNodes.Last();
                UT = last.UT;
                o = last.nextPatch;
            }


            ManeuverParameters res = null;
            try
            {
                res = currentmaneuver.CreateManeuver(o, UT, jeb.target);
            }
            catch (OperationException x)
            {
                errorlabel.UnselectedColor = Color.red;
                errorlabel.Label = x.Message;
                return;
            }

            if (res != null)
            {
                v.PlaceManeuverNode(o, res.dV, res.UT);
                errorlabel.UnselectedColor = Color.green;
                errorlabel.Label = "Node created.";
            }
        }
            
        void B_addnode_OnClick ()
        {
            AddNode();
        }

        void B_executenext_OnClick ()
        {
            Vessel v = FlightGlobals.ActiveVessel;
            MuMech.MechJebCore jeb = GetJeb(v);

            if (jeb.node == null)
            {
                //Blank button for now
            }
            else
            {
                if (jeb.node.enabled)
                {
                    jeb.node.Abort();
                }
                else
                {
                    jeb.node.ExecuteOneNode(this);
                }
            }

            errorlabel.UnselectedColor = Color.green;
            errorlabel.Label = "---";
        }

        void B_deletenodes_OnClick ()
        {
            Vessel v = FlightGlobals.ActiveVessel;
            v.RemoveAllManeuverNodes();

            errorlabel.UnselectedColor = Color.green;
            errorlabel.Label = "---";
        }

        public override eMultiplexedPageRenderResult Render(PageDisplayBuffer b)
        {
            Vessel v = FlightGlobals.ActiveVessel;
            MuMech.MechJebCore jeb = GetJeb(v);
            double UT = jeb.vesselState.time;

            if (jeb.node == null)
            {
                b_executenext.Label = "[]";
            }
            else if (!jeb.node.enabled)
            {
                b_executenext.Label = "[Exec. Next Node]";
            }
            else
            {
                b_executenext.Label = "[Stop Node Exec.]";
            }

            List<ManeuverNode> maneuverNodes = GetManeuverNodes(jeb);
            StringBuilder sb = StringBuilderCache.Acquire(2048);
            sb.AppendLine("---Pending Nodes---");
            if (jeb.node != null && jeb.node.enabled)
            {
                sb.AppendLine("***EXECUTING***");
            }
            for (int i = 0; i < maneuverNodes.Count; ++i)
            {
                ManeuverNode n = maneuverNodes[i];
                sb.Append("dV: ");
                sb.Concat((float)n.DeltaV.magnitude, 1);
                sb.Append(" m/s,  ETA: ");
                sb.Concat((float)(n.UT - UT), 1);
                sb.Append(" sec");
                sb.AppendLine();
            }
            pendingnodeslabel.Label = StringBuilderCache.ToStringAndRelease(sb);

            return base.Render(b);
        }



    }
}

