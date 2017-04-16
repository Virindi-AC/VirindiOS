using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class NumericEdit : EditBase
    {
        OnScreenKeypad cachedkeypad = null;

        public NumericEdit()
        {
            InputString = "0";
        }

        void Keypad_OnPadInput (char val)
        {
            if (val == char.MaxValue)
            {
                if (InputString.Length > 0)
                    InputString = InputString.Substring(0, InputString.Length - 1);
            }
            else if (!(val == '.' && InputString.Contains(".")))
                AppendToInputString(val);
        }

        public float InputNumber
        {
            get
            {
                float res = 0f;
                float.TryParse(InputString, out res);
                return res;
            }
            set
            {
                InputString = value.ToString();
            }
        }

        public override void ButtonDown(eMFDButton btn)
        {
            base.ButtonDown(btn);

            switch (btn)
            {
                case eMFDButton.Num0:
                    AppendToInputString('0');
                    break;
                case eMFDButton.Num1:
                    AppendToInputString('1');
                    break;
                case eMFDButton.Num2:
                    AppendToInputString('2');
                    break;
                case eMFDButton.Num3:
                    AppendToInputString('3');
                    break;
                case eMFDButton.Num4:
                    AppendToInputString('4');
                    break;
                case eMFDButton.Num5:
                    AppendToInputString('5');
                    break;
                case eMFDButton.Num6:
                    AppendToInputString('6');
                    break;
                case eMFDButton.Num7:
                    AppendToInputString('7');
                    break;
                case eMFDButton.Num8:
                    AppendToInputString('8');
                    break;
                case eMFDButton.Num9:
                    AppendToInputString('9');
                    break;
                case eMFDButton.Dec:
                    if (!InputString.Contains("."))
                        AppendToInputString('.');
                    break;
                case eMFDButton.Left:
                case eMFDButton.Right:
                case eMFDButton.Ok:
                    if (cachedkeypad != null)
                    {
                        //KSPLog.print("Forwarding key.");
                        cachedkeypad.Key(btn);
                    }
                    else
                    {
                        //KSPLog.print("Keypad null!");
                    }
                    break;
            }
        }

        public override void Render(PageDisplayBuffer buf)
        {
            base.Render(buf);

            if (!buf.InfoKeyMap.HasNumpad && HasFocus)
            {
                if (cachedkeypad == null)
                {
                    cachedkeypad = buf.OSK_Numeric;
                    cachedkeypad.OnPadInput += Keypad_OnPadInput;
                }

                buf.OSK_Numeric.Visible = true;
                buf.OSK_Numeric.Render(buf);
            }
        }

        public override void OnDefocus()
        {
            base.OnDefocus();

            if (cachedkeypad != null)
            {
                cachedkeypad.OnPadInput -= Keypad_OnPadInput;
                cachedkeypad.Visible = false;
                cachedkeypad = null;
            }
        }




    }
}

