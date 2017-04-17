using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb
{
    public enum TimeReference
    {
        None,
        COMPUTED, X_FROM_NOW, APOAPSIS, PERIAPSIS, ALTITUDE, EQ_ASCENDING, EQ_DESCENDING,
        REL_ASCENDING, REL_DESCENDING, CLOSEST_APPROACH,
        EQ_HIGHEST_AD, EQ_NEAREST_AD, REL_HIGHEST_AD, REL_NEAREST_AD
    }

    //Thanks MechJeb
    public class TimeSelector : TextUI.ScrollList
    {
        TimeReference[] reference;
        int currentreference = 0;
        TextUI.ChoiceBox mainchoice;

        public double universalTime;
        public double LeadTime;
        public double CircularizeAltitude;

        public TimeReference ResultReference
        {
            get
            {
                if (currentreference < 0 || currentreference >= reference.Length)
                    return TimeReference.None;
                else
                    return reference[currentreference];
            }
        }

        public TimeSelector(params TimeReference[] pref)
        {
            reference = pref;
            mainchoice = new VirindiRPMPages.TextUI.ChoiceBox();
            mainchoice.Label = "Burn: ";
            AddControl(mainchoice);

            for (int i = 0 ; i < reference.Length ; ++i)
            {
                switch (reference[i])
                {
                    case TimeReference.APOAPSIS: mainchoice.Options.Add("at the next apoapsis"); break;
                    case TimeReference.CLOSEST_APPROACH: mainchoice.Options.Add("at closest approach to target"); break;
                    case TimeReference.EQ_ASCENDING: mainchoice.Options.Add("at the equatorial AN"); break;
                    case TimeReference.EQ_DESCENDING: mainchoice.Options.Add("at the equatorial DN"); break;
                    case TimeReference.PERIAPSIS: mainchoice.Options.Add("at the next periapsis"); break;
                    case TimeReference.REL_ASCENDING: mainchoice.Options.Add("at the next AN with the target."); break;
                    case TimeReference.REL_DESCENDING: mainchoice.Options.Add("at the next DN with the target."); break;

                    case TimeReference.X_FROM_NOW: mainchoice.Options.Add("after a fixed time"); break;

                    case TimeReference.ALTITUDE: mainchoice.Options.Add("at an altitude"); break;

                    case TimeReference.EQ_NEAREST_AD: mainchoice.Options.Add("at the nearest equatorial AN/DN"); break;
                    case TimeReference.EQ_HIGHEST_AD: mainchoice.Options.Add("at the highest equatorial AN/DN"); break;
                    case TimeReference.REL_NEAREST_AD: mainchoice.Options.Add("at the nearest AN/DN with the target"); break;
                    case TimeReference.REL_HIGHEST_AD: mainchoice.Options.Add("at the highest AN/DN with the target"); break;

                    default:
                        mainchoice.Options.Add("??????");
                        break;
                }
            }

            mainchoice.OnChanged += Mainchoice_OnChanged;
        }

        void Mainchoice_OnChanged (VirindiRPMPages.TextUI.Control source)
        {
            //Delete everything but the first combo
            for (int i = Items.Count - 1; i > 0; --i)
            {
                Items.RemoveAt(i);
            }
            int currentreference = mainchoice.SelectedIndex;
            if (currentreference >= 0 && currentreference < reference.Length)
            {
                switch (reference[currentreference])
                {
                    case TimeReference.X_FROM_NOW:
                        {
                            TextUI.NumericEdit e = new VirindiRPMPages.TextUI.NumericEdit();
                            AddControl(e);
                            e.OnChange += LeadTime_OnChange;
                            e.InputNumber = 0;
                            e.PostLabel = " sec";
                        }
                        break;
                    case TimeReference.ALTITUDE:
                        {
                            TextUI.NumericEdit e = new VirindiRPMPages.TextUI.NumericEdit();
                            AddControl(e);
                            e.OnChange += Altitude_OnChange;
                            e.InputNumber = 0;
                            e.PostLabel = " km";
                        }
                        break;
                }
            }
        }

        void LeadTime_OnChange (VirindiRPMPages.TextUI.Control source)
        {
            LeadTime = (source as TextUI.NumericEdit).InputNumber;
        }

        void Altitude_OnChange (VirindiRPMPages.TextUI.Control source)
        {
            CircularizeAltitude = (source as TextUI.NumericEdit).InputNumber;
        }


    }
}

