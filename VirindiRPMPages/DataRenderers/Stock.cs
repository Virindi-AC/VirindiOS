using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.DataRenderers
{
    public static class Stock
    {
        public static void AddRenderers()
        {
            RendererLibrary.Renderers.Add("Stock_VesselName", new RendererLibrary.RendererInfo(VesselName, "Standard > Vessel Name"));
            RendererLibrary.Renderers.Add("Stock_Mass", new RendererLibrary.RendererInfo(Mass, "Standard > Mass"));



            RendererLibrary.Renderers.Add("Stock_EnginesStatus", new RendererLibrary.RendererInfo(EnginesStatus, "Standard > Engines Status"));
        }

        static void VesselName(PageDisplayBuffer screen)
        {
            Vessel v = FlightGlobals.ActiveVessel;
            if (v == null) return;
            screen.WriteLeftAndRight("Vessel Name:", v.GetName());
        }

        static void Mass(PageDisplayBuffer screen)
        {
            Vessel v = FlightGlobals.ActiveVessel;
            if (v == null) return;
            screen.WriteLeftAndRight("Vessel Mass:", v.GetTotalMass().ToString("0.0"));
        }

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

        static int EngineInfoComparison(sEngineInfo a, sEngineInfo b)
        {
            int c1 = b.currentthrust.CompareTo(a.currentthrust);
            if (c1 != 0)
                return c1;
            return a.name.CompareTo(b.name);
        }

        static void EnginesStatus(PageDisplayBuffer screen)
        {
            Vessel v = FlightGlobals.ActiveVessel;
            if (v != null)
            {
                screen.AppendLine("Engines Status:");
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
                                screen.AppendFormat("MME NULL! {0}", ifo.name);
                                screen.AppendLine();
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
                        screen.AppendFormat("{0:0.0}kN (x{1}) {2}", enginesinfo[i].currentthrust,samecount+1,enginesinfo[i].name);
                        screen.AppendLine();
                        samecount = 0;
                    }
                    else
                    {
                        ++samecount;
                    }
                }
            }
        }






    }
}

