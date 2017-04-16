using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.DataRenderers
{
    public static class RendererLibrary
    {
        public delegate void delRender(PageDisplayBuffer screen);

        public class RendererInfo
        {
            public delRender RenderCall;
            public string DisplayName;

            public RendererInfo(delRender c, string disp)
            {
                RenderCall = c;
                DisplayName = disp;
            }
        }

        public class RendererPageTemplate
        {
            public string DisplayName = "???";
            public List<RendererInfo> Renderers = new List<RendererInfo>();
        }


        public static SortedDictionary<string, RendererInfo> Renderers = new SortedDictionary<string, RendererInfo>();
        public static List<RendererPageTemplate> PageTemplates = new List<RendererPageTemplate>();

        static bool isinit = false;
        public static void Initialize()
        {
            if (isinit) return;
            isinit = true;

            Stock.AddRenderers();
            KerbalEngineerR.AddRenderers();

            //Temp
            RendererPageTemplate ascentdata = new RendererPageTemplate();
            PageTemplates.Add(ascentdata);
            ascentdata.DisplayName = "Ascent Data";
            ascentdata.Renderers.Add(Renderers["Stock_VesselName"]);
            ascentdata.Renderers.Add(Renderers["Stock_Mass"]);
            ascentdata.Renderers.Add(Renderers["KerbalEngineer_Thermal_CriticalPart"]);
            ascentdata.Renderers.Add(Renderers["KerbalEngineer_Vessel_DeltaVStage"]);
            ascentdata.Renderers.Add(Renderers["KerbalEngineer_Vessel_DeltaVTotal"]);
            ascentdata.Renderers.Add(Renderers["Stock_EnginesStatus"]);
        }
    }
}

