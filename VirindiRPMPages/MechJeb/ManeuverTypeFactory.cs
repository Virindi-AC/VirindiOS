using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb
{
    public static class ManeuverTypeFactory
    {
        public static List<string> ManeuverTypeNames = new List<string>();
        public static List<Type> ManeuverTypes = new List<Type>();

        static void AddTyp(string n, Type t)
        {
            ManeuverTypeNames.Add(n);
            ManeuverTypes.Add(t);
        }

        static bool isinit = false;
        public static void Initialize()
        {
            if (isinit) return;
            isinit = true;

        }

        public static ManeuverType CreateManeuverType(string name)
        {
            int ind = ManeuverTypeNames.IndexOf(name);
            if (ind == -1) return null;

            Type t = ManeuverTypes[ind];
            return (ManeuverType)t.GetConstructor(new Type[0]).Invoke(null);
        }

    }
}

