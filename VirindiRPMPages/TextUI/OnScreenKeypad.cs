using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class OnScreenKeypad
    {
        public bool Visible = false;
        int Selection = 0;

        public delegate void delOnPadInput(char val);
        public event delOnPadInput OnPadInput;

        public void Render(PageDisplayBuffer buf)
        {
            if (!Visible) return;

            int oldcursorx = buf.CursorX;
            int oldcursory = buf.CursorY;
            Color oldcursorcolor = buf.CursorColor;

            buf.CursorColor = Color.yellow;
            buf.CursorX = -1;
            buf.CursorY = buf.Height - 2;
            string strheading = "On-Screen Keypad";
            for (int i = 0; i < (buf.Width - strheading.Length) / 2; ++i)
                buf.Append('-');
            buf.Append(strheading);
            for (int i = 0; i < (buf.Width - strheading.Length) / 2 + 1; ++i)
                buf.Append('-');
            buf.AppendLine();

            for (int i = 0; i < 11; ++i)
            {
                if (i == Selection)
                {
                    buf.Append('>');
                    buf.Append(GetIntAsChar(i));
                    buf.Append('<');
                }
                else
                {
                    buf.Append(' ');
                    buf.Append(GetIntAsChar(i));
                    buf.Append(' ');
                }
            }

            if (Selection == 11)
                buf.Append('>');
            else
                buf.Append(' ');
            buf.Append("Bsp");
            if (Selection == 11)
                buf.Append('<');
            else
                buf.Append(' ');

            buf.CursorX = oldcursorx;
            buf.CursorY = oldcursory;
            buf.CursorColor = oldcursorcolor;
        }

        char GetIntAsChar(int v)
        {
            switch (v)
            {
                case 0:
                    return('0');
                case 1:
                    return('1');
                case 2:
                    return('2');
                case 3:
                    return('3');
                case 4:
                    return('4');
                case 5:
                    return('5');
                case 6:
                    return('6');
                case 7:
                    return('7');
                case 8:
                    return('8');
                case 9:
                    return('9');
                case 10:
                    return('.');
                case 11:
                    return char.MaxValue;
                default:
                    return '?';
            }
        }

        public void Key(eMFDButton btn)
        {
            if (!Visible)
            {
                KSPLog.print("Key: return not visible.");
                return;
            }
            KSPLog.print("Key visible.");
            switch (btn)
            {
                case eMFDButton.Left:
                    {
                        Selection--;
                        if (Selection < 0) Selection = 11;
                    }
                    break;
                case eMFDButton.Right:
                    {
                        Selection++;
                        if (Selection > 11) Selection = 0;
                    }
                    break;
                case eMFDButton.Ok:
                    {
                        if (OnPadInput != null)
                        {
                            OnPadInput(GetIntAsChar(Selection));
                        }
                    }
                    break;
            }
        }

    }
}

