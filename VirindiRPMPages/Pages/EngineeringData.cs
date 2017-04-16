using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class EngineeringData : IMultiplexedPage
    {
        bool exitnow = false;

        struct sEngineInfo
        {
            public bool ismultimode;
            public bool issinglemode;
            public ModuleEngines activeengine;
            public float currentthrust;
            public string name;
        }

        static bool HasModule<T>(Part part) where T : PartModule
        {
            for (int i = 0; i < part.Modules.Count; i++)
            {
                if (part.Modules[i] is T)
                    return true;
            }
            return false;
        }

        static T GetModule<T>(Part part) where T : PartModule
        {
            for (int i = 0; i < part.Modules.Count; i++)
            {
                PartModule pm = part.Modules[i];
                if (pm is T)
                    return (T)pm;
            }
            return null;
        }

        public void Activate()
        {

        }

        public void Deactivate()
        {

        }

        public void Update()
        {
            KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.RequestUpdate();
            KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.RequestUpdate();
        }

        public void ButtonDown(eMFDButton btn)
        {

        }

        public void ButtonUp(eMFDButton btn)
        {
            if (btn == eMFDButton.Esc)
                exitnow = true;
        }

        public void NotifyOfDisplay(PageDisplayBuffer b)
        {
        }

        public eMultiplexedPageRenderResult Render(PageDisplayBuffer pagebuilder)
        {
            //TODO: check for Engineer present
            return RenderReal(pagebuilder);
        }

        eMultiplexedPageRenderResult RenderReal(PageDisplayBuffer pagebuilder)
        {
            if (exitnow)
            {
                exitnow = false;
                return eMultiplexedPageRenderResult.ExitThisPage;
            }
              
            /*
            pagebuilder.Append("Engineering Data:");
            pagebuilder.AppendLine();
            pagebuilder.Append("-----------------");
            pagebuilder.AppendLine();
            */

            double critthermpct = KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.CriticalTemperaturePercentage;
            if (critthermpct < 0.6)
                pagebuilder.CursorColor = Color.Lerp(Color.green, Color.yellow, (float)(critthermpct / 0.6f));
            else
                pagebuilder.CursorColor = Color.Lerp(Color.yellow, Color.red, (float)((critthermpct - 0.6f) / 0.4f));
            pagebuilder.AppendFormat("Critical part:     {0:0.0}%   {1}", critthermpct * 100d, KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.CriticalPartName);
            pagebuilder.CursorColor = Color.white;
            pagebuilder.AppendLine();
            string dvstr = KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.deltaV.ToString("N0") + "m/s (" + KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.time.ToString("0.0") + ")";
            pagebuilder.AppendFormat("Stage DeltaV:      " + dvstr);
            pagebuilder.AppendLine();
            dvstr = KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.totalDeltaV.ToString("N0") + "m/s (" + KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.totalTime.ToString("0.0") + ")";
            pagebuilder.AppendFormat("Total DeltaV:      " + dvstr);
            pagebuilder.AppendLine();

            pagebuilder.AppendLine();
            pagebuilder.Append("Engine Status:");
            pagebuilder.AppendLine();
            pagebuilder.Append("--------------");
            pagebuilder.AppendLine();

            Vessel v = FlightGlobals.ActiveVessel;
            if (v != null)
            {
                List<sEngineInfo> enginesinfo = new List<sEngineInfo>();
                for (int i=0;i<v.parts.Count;++i)
                {
                    Part p = v.parts[i];
                    bool ismultimode = HasModule<MultiModeEngine>(p);
                    bool issinglemode = HasModule<ModuleEngines>(p);

                    if (ismultimode || issinglemode)
                    {
                        sEngineInfo ifo = new sEngineInfo();
                        ifo.ismultimode = ismultimode;
                        ifo.issinglemode = issinglemode;

                        ifo.name = "???";
                        if (p.partInfo != null)
                            ifo.name = p.partInfo.title;

                        //Find the running engine.
                        PartModuleList modlist = p.Modules;
                        int modcount = modlist.Count;
                        ifo.activeengine = null;
                        if (ismultimode)
                        {
                            MultiModeEngine mme = GetModule<MultiModeEngine>(p);
                            string mode = "???";
                            if (mme != null)
                                mode = mme.mode;
                            else
                            {
                                pagebuilder.AppendFormat("MME NULL! {0}", ifo.name);
                                pagebuilder.AppendLine();
                            }
                            for (int j = 0; j < modcount; j++)
                            {
                                ModuleEngines engine = modlist[j] as ModuleEngines;
                                if (engine != null && engine.engineID == mode)
                                {
                                    ifo.activeengine = engine;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < modcount; j++)
                            {
                                ModuleEngines engine = modlist[j] as ModuleEngines;
                                if (engine != null)
                                {
                                    ifo.activeengine = engine;
                                    break;
                                }
                            }
                        }
                        if (ifo.activeengine != null)
                        {
                            ifo.currentthrust = ifo.activeengine.GetCurrentThrust();
                        }
                        else
                            ifo.currentthrust = 0f;

                        enginesinfo.Add(ifo);
                    }
                }

                //We have the engines info. Sort.
                enginesinfo.Sort(EngineInfoComparison);
                int samecount = 0;
                for (int i=0;i<enginesinfo.Count;++i)
                {
                    if ((i==enginesinfo.Count-1)
                        || (Math.Abs(enginesinfo[i].currentthrust-enginesinfo[i+1].currentthrust) > 0.1d)
                        || (enginesinfo[i].name!=enginesinfo[i+1].name))
                    {
                        pagebuilder.AppendFormat("{0:0.0}kN (x{1}) {2}", enginesinfo[i].currentthrust,samecount+1,enginesinfo[i].name);
                        pagebuilder.AppendLine();
                        samecount = 0;
                    }
                    else
                    {
                        ++samecount;
                    }
                }
            }

            return eMultiplexedPageRenderResult.Continue;
        }

        public string GetMenuItemName()
        {
            return "Engines & Heat";
        }

        public void GetHeader(StringBuilder b)
        {
            b.Append("Engines & Heat");
        }

        public bool IsActivationAllowed()
        {
            return true;
        }

        static int EngineInfoComparison(sEngineInfo a, sEngineInfo b)
        {
            int c1 = b.currentthrust.CompareTo(a.currentthrust);
            if (c1 != 0)
                return c1;
            return a.name.CompareTo(b.name);
        }

    }
}

