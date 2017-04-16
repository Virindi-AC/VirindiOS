using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.Pages
{
    public class UITestPage : TextUI.UIOnlyPage
    {
        TextUI.NumericEdit NumValue1;
        TextUI.NumericEdit NumValue2;
        TextUI.EditBase editb1;
        TextUI.NumericEdit NumValue3;
        TextUI.EditBase editb2;
        TextUI.LinkButton linkclear;

        public UITestPage()
        {
            VirindiRPMPages.TextUI.ScrollList sRootControl = new VirindiRPMPages.TextUI.ScrollList();
            RootControl = sRootControl;

            NumValue1 = new VirindiRPMPages.TextUI.NumericEdit();
            NumValue1.Label = "NumValue1";
            NumValue1.InputNumber = 12345;
            sRootControl.Items.Add(NumValue1);

            NumValue2 = new VirindiRPMPages.TextUI.NumericEdit();
            NumValue2.Label = "NumValue2";
            NumValue1.InputNumber = 6789;
            sRootControl.Items.Add(NumValue2);

            editb1 = new VirindiRPMPages.TextUI.EditBase();
            editb1.Label = "BlankEdit1";
            sRootControl.Items.Add(editb1);

            NumValue3 = new VirindiRPMPages.TextUI.NumericEdit();
            NumValue3.Label = "NumValue3";
            NumValue1.InputNumber = 0.3f;
            sRootControl.Items.Add(NumValue3);

            editb2 = new VirindiRPMPages.TextUI.EditBase();
            editb2.Label = "BlankEdit2";
            sRootControl.Items.Add(editb2);

            linkclear = new VirindiRPMPages.TextUI.LinkButton();
            linkclear.Label = "[Clear All!]";
            linkclear.OnClick += Linkclear_OnClick;
            sRootControl.Items.Add(linkclear);
        }

        void Linkclear_OnClick ()
        {
            NumValue1.InputNumber = 0;
            NumValue2.InputNumber = 0;
            NumValue3.InputNumber = 0;
        }

        public override string GetMenuItemName()
        {
            return "UI Test";
        }
        public override void GetHeader(System.Text.StringBuilder b)
        {
            b.Append("UI Test");
        }



    }
}

