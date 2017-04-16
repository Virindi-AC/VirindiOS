using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class KeyboardTest : IMultiplexedPage
	{
        List<string> debugtemplog = new List<string>();
        int debugtemplognum = 0;
        bool exitnow = false;

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
            ++debugtemplognum;
            debugtemplog.Add("[" + debugtemplognum.ToString() + "] DOWN " + btn.ToString());
        }

        public void ButtonUp(eMFDButton btn)
        {
            ++debugtemplognum;
            debugtemplog.Add("[" + debugtemplognum.ToString() + "] UP   " + btn.ToString());

            if (btn == eMFDButton.Esc)
                exitnow = true;
        }

        public void NotifyOfDisplay(PageDisplayBuffer b)
        {
        }

        public eMultiplexedPageRenderResult Render(PageDisplayBuffer pagebuilder)
        {
            if (exitnow)
            {
                exitnow = false;
                return eMultiplexedPageRenderResult.ExitThisPage;
            }


            pagebuilder.Append("Keyboard Log:");
            pagebuilder.Append(Environment.NewLine);
            pagebuilder.Append("-------------");
            pagebuilder.Append(Environment.NewLine);

            int linecount = 2;
            for (int i = debugtemplog.Count - 1; i >= 0; --i)
            {
                ++linecount;
                if (linecount >= 20 - 1)
                    break;
                pagebuilder.Append(debugtemplog[i]);
                pagebuilder.Append(Environment.NewLine);
            }


            return eMultiplexedPageRenderResult.Continue;
        }

        public string GetMenuItemName()
        {
            return "Keyboard Test";
        }

        public void GetHeader(StringBuilder b)
        {
            b.Append("Keyboard Test");
        }

        public bool IsActivationAllowed()
        {
            return true;
        }

        #endregion

		//KerbalEngineer.VesselSimulator v;
        //KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor thermal;
        /*
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

		public void Start()
		{
			
		}

        public void ClickProcessor(int buttonID)
        {
        
        }

        public void ReleaseProcessor(int buttonID)
        {

        }

		public string ShowTestPage(int screenWidth, int screenHeight)
		{
			StringBuilder pagebuilder = new StringBuilder();
            try{

                pagebuilder.Append(Environment.NewLine);
                pagebuilder.Append("Engineering Data:");
                pagebuilder.Append(Environment.NewLine);
                pagebuilder.Append("-----------------");
                pagebuilder.Append(Environment.NewLine);

                pagebuilder.AppendFormat("Critical part:     {0:0.0}%   {1}", KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.CriticalTemperaturePercentage*100d,KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.CriticalPartName);
                pagebuilder.Append(Environment.NewLine);
                string dvstr = KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.deltaV.ToString("N0") + "m/s (" + KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.time.ToString() + ")";
                pagebuilder.AppendFormat("Stage DeltaV:      " + dvstr);
                pagebuilder.Append(Environment.NewLine);
                dvstr = KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.totalDeltaV.ToString("N0") + "m/s (" + KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.totalTime.ToString() + ")";
                pagebuilder.AppendFormat("Total DeltaV:      " + dvstr);
                pagebuilder.Append(Environment.NewLine);

                pagebuilder.Append(Environment.NewLine);
                pagebuilder.Append("Engine Status:");
                pagebuilder.Append(Environment.NewLine);
                pagebuilder.Append("-----------------");
                pagebuilder.Append(Environment.NewLine);

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
                                    pagebuilder.Append(Environment.NewLine);
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
                            pagebuilder.Append(Environment.NewLine);
                            samecount = 0;
                        }
                        else
                        {
                            ++samecount;
                        }
                    }
                }


            } catch (Exception e)
            {
                pagebuilder = new StringBuilder();
                pagebuilder.Append(Environment.NewLine);
                string[] vv = e.ToString().Split('\n');
                foreach (string v in vv)
                {
                    bool one = false;
                    for (int i = 0; i < v.Length; i+=screenWidth-1)
                    {
                        if (one)
                            pagebuilder.Append(">");
                        else
                            pagebuilder.Append(" ");
                        if (v.Length - i > screenWidth - 1)
                        {
                            pagebuilder.Append(v.Substring(i, screenWidth-1));
                        }
                        else
                        {
                            pagebuilder.Append(v.Substring(i));
                        }
                        pagebuilder.Append(Environment.NewLine);
                        one = true;
                    }
                }
            }
			return pagebuilder.ToString();
		}

        static int EngineInfoComparison(sEngineInfo a, sEngineInfo b)
        {
            int c1 = b.currentthrust.CompareTo(a.currentthrust);
            if (c1 != 0)
                return c1;
            return a.name.CompareTo(b.name);
        }


        public override void OnUpdate()
        {
            KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.RequestUpdate();
            KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.RequestUpdate();
        }
        */



	}
}

