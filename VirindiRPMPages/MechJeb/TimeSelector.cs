using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb
{
    public enum TimeReference
    {
        COMPUTED, X_FROM_NOW, APOAPSIS, PERIAPSIS, ALTITUDE, EQ_ASCENDING, EQ_DESCENDING,
        REL_ASCENDING, REL_DESCENDING, CLOSEST_APPROACH,
        EQ_HIGHEST_AD, EQ_NEAREST_AD, REL_HIGHEST_AD, REL_NEAREST_AD
    }

    //Thanks MechJeb
    public class TimeSelector : TextUI.ChoiceBox
    {
        TimeReference[] reference;
        int currentreference = 0;

        public double universalTime;

        public TimeSelector(params TimeReference[] pref)
        {
            reference = pref;
            /*
            for (int i = 0 ; i < reference.Length ; ++i)
            {
                switch (reference[i])
                {
                    case TimeReference.APOAPSIS: timeRefNames[i] = "at the next apoapsis"; break;
                    case TimeReference.CLOSEST_APPROACH: timeRefNames[i] = "at closest approach to target"; break;
                    case TimeReference.EQ_ASCENDING: timeRefNames[i] = "at the equatorial AN"; break;
                    case TimeReference.EQ_DESCENDING: timeRefNames[i] = "at the equatorial DN"; break;
                    case TimeReference.PERIAPSIS: timeRefNames[i] = "at the next periapsis"; break;
                    case TimeReference.REL_ASCENDING: timeRefNames[i] = "at the next AN with the target."; break;
                    case TimeReference.REL_DESCENDING: timeRefNames[i] = "at the next DN with the target."; break;

                    case TimeReference.X_FROM_NOW: timeRefNames[i] = "after a fixed time"; break;

                    case TimeReference.ALTITUDE: timeRefNames[i] = "at an altitude"; break;

                    case TimeReference.EQ_NEAREST_AD: timeRefNames[i] = "at the nearest equatorial AN/DN"; break;
                    case TimeReference.EQ_HIGHEST_AD: timeRefNames[i] = "at the highest equatorial AN/DN"; break;
                    case TimeReference.REL_NEAREST_AD: timeRefNames[i] = "at the nearest AN/DN with the target"; break;
                    case TimeReference.REL_HIGHEST_AD: timeRefNames[i] = "at the highest AN/DN with the target"; break;
                }
            }
            */
        }
    }



}

