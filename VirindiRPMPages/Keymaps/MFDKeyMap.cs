using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages
{
    public class MFDKeyMap
    {
        Dictionary<int, eMFDButton> ScanCodeToButton = new Dictionary<int, eMFDButton>();
        Dictionary<eMFDButton, List<int>> ButtonToScanCode = new Dictionary<eMFDButton, List<int>>();
        bool ihasnumpad = false;
        bool ihasalphabet = false;

        public string KeymapString { get { return keymapid; } }

        string keymapid;
        public static MFDKeyMap GetMap(string pkeymap)
        {
            return new MFDKeyMap(pkeymap);
        }

        private MFDKeyMap(string pmap)
        {
            keymapid = pmap;

            switch (keymapid.ToLowerInvariant())
            {
                case "vdsky":
                    ScanCodeToButton[(int)eVDskyButton.Num0] = eMFDButton.Num0;
                    ScanCodeToButton[(int)eVDskyButton.Num1] = eMFDButton.Num1;
                    ScanCodeToButton[(int)eVDskyButton.Num2] = eMFDButton.Num2;
                    ScanCodeToButton[(int)eVDskyButton.Num3] = eMFDButton.Num3;
                    ScanCodeToButton[(int)eVDskyButton.Num4] = eMFDButton.Num4;
                    ScanCodeToButton[(int)eVDskyButton.Num5] = eMFDButton.Num5;
                    ScanCodeToButton[(int)eVDskyButton.Num6] = eMFDButton.Num6;
                    ScanCodeToButton[(int)eVDskyButton.Num7] = eMFDButton.Num7;
                    ScanCodeToButton[(int)eVDskyButton.Num8] = eMFDButton.Num8;
                    ScanCodeToButton[(int)eVDskyButton.Num9] = eMFDButton.Num9;
                    ScanCodeToButton[(int)eVDskyButton.Esc] = eMFDButton.Esc;
                    ScanCodeToButton[(int)eVDskyButton.Ok] = eMFDButton.Ok;
                    ScanCodeToButton[(int)eVDskyButton.Up] = eMFDButton.Up;
                    ScanCodeToButton[(int)eVDskyButton.Down] = eMFDButton.Down;
                    break;
                case "alcormfd40x20":
                    ScanCodeToButton[(int)e40x20Button.ArrowUp] = eMFDButton.Up;
                    ScanCodeToButton[(int)e40x20Button.ArrowDown] = eMFDButton.Down;
                    ScanCodeToButton[(int)e40x20Button.Left] = eMFDButton.Left;
                    ScanCodeToButton[(int)e40x20Button.Right] = eMFDButton.Right;
                    ScanCodeToButton[(int)e40x20Button.ArrowDownPrev] = eMFDButton.Prev;
                    ScanCodeToButton[(int)e40x20Button.ArrowUpNext] = eMFDButton.Next;
                    ScanCodeToButton[(int)e40x20Button.GreenLeftArrow] = eMFDButton.Ok;
                    ScanCodeToButton[(int)e40x20Button.RedX] = eMFDButton.Esc;
                    break;
            }

            foreach (KeyValuePair<int, eMFDButton> kp in ScanCodeToButton)
            {
                if (!ButtonToScanCode.ContainsKey(kp.Value))
                    ButtonToScanCode[kp.Value] = new List<int>();
                ButtonToScanCode[kp.Value].Add(kp.Key);
            }

            ihasnumpad = ButtonToScanCode.ContainsKey(eMFDButton.Num0)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num1)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num2)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num3)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num4)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num5)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num6)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num7)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num8)
            && ButtonToScanCode.ContainsKey(eMFDButton.Num9);

            ihasalphabet = ButtonToScanCode.ContainsKey(eMFDButton.A)
            && ButtonToScanCode.ContainsKey(eMFDButton.B)
            && ButtonToScanCode.ContainsKey(eMFDButton.C)
            && ButtonToScanCode.ContainsKey(eMFDButton.D)
            && ButtonToScanCode.ContainsKey(eMFDButton.E)
            && ButtonToScanCode.ContainsKey(eMFDButton.F)
            && ButtonToScanCode.ContainsKey(eMFDButton.G)
            && ButtonToScanCode.ContainsKey(eMFDButton.H)
            && ButtonToScanCode.ContainsKey(eMFDButton.I)
            && ButtonToScanCode.ContainsKey(eMFDButton.J)
            && ButtonToScanCode.ContainsKey(eMFDButton.K)
            && ButtonToScanCode.ContainsKey(eMFDButton.L)
            && ButtonToScanCode.ContainsKey(eMFDButton.M)
            && ButtonToScanCode.ContainsKey(eMFDButton.N)
            && ButtonToScanCode.ContainsKey(eMFDButton.O)
            && ButtonToScanCode.ContainsKey(eMFDButton.P)
            && ButtonToScanCode.ContainsKey(eMFDButton.Q)
            && ButtonToScanCode.ContainsKey(eMFDButton.R)
            && ButtonToScanCode.ContainsKey(eMFDButton.S)
            && ButtonToScanCode.ContainsKey(eMFDButton.T)
            && ButtonToScanCode.ContainsKey(eMFDButton.U)
            && ButtonToScanCode.ContainsKey(eMFDButton.V)
            && ButtonToScanCode.ContainsKey(eMFDButton.W)
            && ButtonToScanCode.ContainsKey(eMFDButton.X)
            && ButtonToScanCode.ContainsKey(eMFDButton.Y)
            && ButtonToScanCode.ContainsKey(eMFDButton.Z);
        }

        public bool HasNumpad
        {
            get
            {
                return ihasnumpad;
            }
        }

        public bool HasAlphabet
        {
            get
            {
                return ihasalphabet;
            }
        }

        public eMFDButton Translate(int scancode)
        {
            if (ScanCodeToButton.ContainsKey(scancode))
                return ScanCodeToButton[scancode];
            else
                return eMFDButton.Unknown;
        }

        public bool HasButton(eMFDButton btn)
        {
            return ButtonToScanCode.ContainsKey(btn);
        }


    }
}

