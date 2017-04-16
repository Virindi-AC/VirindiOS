using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class ScrollList : Control
    {
        public List<Control> Items = new List<Control>();
        int CursorPosition = -1;

        public override void NotifyOfDisplay(PageDisplayBuffer buf)
        {
            base.NotifyOfDisplay(buf);

            for (int i = 0; i < Items.Count; ++i)
                Items[i].NotifyOfDisplay(buf);
        }

        public override void Render(PageDisplayBuffer buf)
        {
            base.Render(buf);

            for (int i = 0; i < Items.Count; ++i)
            {
                if (CursorPosition == i)
                    buf.Append("> ");
                else
                    buf.Append("  ");
                Items[i].Render(buf);
            }
        }

        public override void ButtonDown(eMFDButton btn)
        {
            base.ButtonDown(btn);

            if (btn != eMFDButton.Up && btn != eMFDButton.Down
                && CursorPosition != -1 && CursorPosition < Items.Count)
                Items[CursorPosition].ButtonDown(btn);
            else
            {
                if (btn == eMFDButton.Down)
                {
                    int oldpos = CursorPosition;
                    CursorPosition++;
                    if (CursorPosition >= Items.Count)
                        CursorPosition = Items.Count - 1;
                    int newpos = CursorPosition;

                    if (oldpos != newpos)
                    {
                        if (oldpos >= 0 && oldpos < Items.Count)
                            Items[oldpos].OnDefocus();
                        if (newpos >= 0 && newpos < Items.Count)
                            Items[newpos].OnFocus();
                    }
                }
                else if (btn == eMFDButton.Up)
                {
                    int oldpos = CursorPosition;
                    CursorPosition--;
                    if (CursorPosition < 0)
                        CursorPosition = 0;
                    int newpos = CursorPosition;

                    if (oldpos != newpos)
                    {
                        if (oldpos >= 0 && oldpos < Items.Count)
                            Items[oldpos].OnDefocus();
                        if (newpos >= 0 && newpos < Items.Count)
                            Items[newpos].OnFocus();
                    }
                }
            }

        }

        public override void ButtonUp(eMFDButton btn)
        {
            base.ButtonUp(btn);
            if (btn != eMFDButton.Up && btn != eMFDButton.Down
                && CursorPosition != -1 && CursorPosition < Items.Count)
                Items[CursorPosition].ButtonUp(btn);
        }

        public override void OnFocus()
        {
            base.OnFocus();

            if (CursorPosition < 0 && Items.Count > 0)
                CursorPosition = 0;

            if (CursorPosition >= 0 && CursorPosition < Items.Count)
                Items[CursorPosition].OnFocus();
        }

        public override void OnDefocus()
        {
            base.OnDefocus();

            if (CursorPosition >= 0 && CursorPosition < Items.Count)
                Items[CursorPosition].OnDefocus();
        }


    }
}

