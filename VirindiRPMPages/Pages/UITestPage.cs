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
        TextUI.ChoiceBox choicetest;
        TextUI.ChoiceBox choicetest2;

        public UITestPage()
        {
            VirindiRPMPages.TextUI.ScrollList sRootControl = new VirindiRPMPages.TextUI.ScrollList();
            RootControl = sRootControl;

            VirindiRPMPages.TextUI.ScrollList emptylist0 = new VirindiRPMPages.TextUI.ScrollList();
            sRootControl.AddControl(emptylist0);

            NumValue1 = new VirindiRPMPages.TextUI.NumericEdit();
            NumValue1.Label = "NumValue1";
            NumValue1.InputNumber = 12345;
            sRootControl.AddControl(NumValue1);

            NumValue2 = new VirindiRPMPages.TextUI.NumericEdit();
            NumValue2.Label = "NumValue2";
            NumValue1.InputNumber = 6789;
            sRootControl.AddControl(NumValue2);

            VirindiRPMPages.TextUI.ScrollList nestedlist = new VirindiRPMPages.TextUI.ScrollList();
            sRootControl.AddControl(nestedlist);

            editb1 = new VirindiRPMPages.TextUI.EditBase();
            editb1.Label = "BlankEdit1";
            nestedlist.AddControl(editb1);

            VirindiRPMPages.TextUI.ScrollList emptylist1 = new VirindiRPMPages.TextUI.ScrollList();
            nestedlist.AddControl(emptylist1);

            NumValue3 = new VirindiRPMPages.TextUI.NumericEdit();
            NumValue3.Label = "NumValue3";
            NumValue1.InputNumber = 0.3f;
            nestedlist.AddControl(NumValue3);

            editb2 = new VirindiRPMPages.TextUI.EditBase();
            editb2.Label = "BlankEdit2";
            nestedlist.AddControl(editb2);

            linkclear = new VirindiRPMPages.TextUI.LinkButton();
            linkclear.Label = "[Clear All!]";
            linkclear.OnClick += Linkclear_OnClick;
            sRootControl.AddControl(linkclear);

            choicetest = new VirindiRPMPages.TextUI.ChoiceBox();
            choicetest.Label = "Options: ";
            choicetest.Options.Add("Stuff");
            choicetest.Options.Add("A");
            choicetest.Options.Add("B");
            choicetest.Options.Add("C");
            sRootControl.AddControl(choicetest);

            choicetest2 = new VirindiRPMPages.TextUI.ChoiceBox();
            choicetest2.Label = "No Options: ";
            sRootControl.AddControl(choicetest2);

            VirindiRPMPages.TextUI.ScrollList emptylist2 = new VirindiRPMPages.TextUI.ScrollList();
            sRootControl.AddControl(emptylist2);
        }

        void Linkclear_OnClick ()
        {
            NumValue1.InputNumber = 0;
            NumValue2.InputNumber = 0;
            NumValue3.InputNumber = 0;
            choicetest.SelectedIndex = -1;
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

