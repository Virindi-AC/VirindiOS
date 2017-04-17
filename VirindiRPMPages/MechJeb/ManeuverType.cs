﻿using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.MechJeb
{
    public interface ManeuverType
    {
        void AddOptionsToUI(TextUI.ScrollList uilist);
        void CreateManeuver();
        string GetTitle();
    }
}
