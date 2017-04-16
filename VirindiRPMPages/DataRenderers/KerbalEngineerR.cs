using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.DataRenderers
{
    public static class KerbalEngineerR
    {
        public static void AddRenderers()
        {
            RendererLibrary.Renderers.Add("KerbalEngineer_Thermal_CriticalPart", new RendererLibrary.RendererInfo(CriticalPart, "Engineer > Thermal > Critical Part"));
            RendererLibrary.Renderers.Add("KerbalEngineer_Vessel_DeltaVStage", new RendererLibrary.RendererInfo(DeltaVStage, "Engineer > Vessel > DeltaV (Stage)"));
            RendererLibrary.Renderers.Add("KerbalEngineer_Vessel_DeltaVCurrent", new RendererLibrary.RendererInfo(DeltaVCurrent, "Engineer > Vessel > DeltaV (Current)"));
            RendererLibrary.Renderers.Add("KerbalEngineer_Vessel_DeltaVTotal", new RendererLibrary.RendererInfo(DeltaVTotal, "Engineer > Vessel > DeltaV (Total)"));
            RendererLibrary.Renderers.Add("KerbalEngineer_Vessel_CT", new RendererLibrary.RendererInfo(DeltaVCT, "Engineer > Vessel > DeltaV (C/T)"));

        }

        static void CriticalPart(PageDisplayBuffer screen)
        {
            KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.RequestUpdate();

            double critthermpct = KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.CriticalTemperaturePercentage;
            Color dispcolor;
            if (critthermpct < 0.6)
                dispcolor = Color.Lerp(Color.green, Color.yellow, (float)(critthermpct / 0.6f));
            else
                dispcolor = Color.Lerp(Color.yellow, Color.red, (float)((critthermpct - 0.6f) / 0.4f));
            string partname = KerbalEngineer.Flight.Readouts.Thermal.ThermalProcessor.CriticalPartName;
            if (partname != null && partname.Length > 10)
                partname = partname.Substring(0, 10);
            screen.WriteLeftAndRight("Critical part:", Color.white, string.Format("{0:0.0}%  {1}", critthermpct * 100d, partname), dispcolor);
        }

        static void DeltaVStage(PageDisplayBuffer screen)
        {
            KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.RequestUpdate();

            string dvstr = KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.deltaV.ToString("N0") + "m/s (" + KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.time.ToString("0.0") + "s)";
            screen.WriteLeftAndRight("Stage DeltaV:", dvstr);
        }

        static void DeltaVCurrent(PageDisplayBuffer screen)
        {

        }

        static void DeltaVTotal(PageDisplayBuffer screen)
        {
            KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.RequestUpdate();

            string dvstr = KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.totalDeltaV.ToString("N0") + "m/s (" + KerbalEngineer.Flight.Readouts.Vessel.SimulationProcessor.LastStage.totalTime.ToString("0.0") + "s)";
            screen.WriteLeftAndRight("Total DeltaV:", dvstr);
        }

        static void DeltaVCT(PageDisplayBuffer screen)
        {

        }

    }
}

