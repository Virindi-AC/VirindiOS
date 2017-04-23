using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages.TextUI
{
    public class ScrollList : Control
    {
        public List<Control> Items = new List<Control>();
        public int CursorPosition = -1;

        public void AddControl(Control c)
        {
            Items.Add(c);
            if (CachedDisplay != null)
                c.NotifyOfDisplay(CachedDisplay);
        }

        public void Clear()
        {
            Items.Clear();
        }

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
                ScrollList cl = Items[i] as ScrollList;

                if (cl == null)
                {
                    if (CursorPosition == i && HasFocus)
                        buf.Append("> ");
                    else
                        buf.Append("  ");
                }
                
                Items[i].Render(buf);
            }
        }

        public override bool CanTakeFocus()
        {
            for (int i = 0; i < Items.Count; ++i)
            {
                if (Items[i].CanTakeFocus())
                    return true;
            }
            return false;
        }

        public override eNestedScrollResult NestedScroll(eScrollDirection dir)
        {
            if (CursorPosition >= 0 && CursorPosition < Items.Count
                && Items[CursorPosition].NestedScroll(dir) == eNestedScrollResult.Eaten)
                return eNestedScrollResult.Eaten;

            //Apply scroll at this level.
            int oldpos = CursorPosition;
            if (dir == eScrollDirection.Down)
            {
                Control cur;
                do
                {
                    CursorPosition++;

                    //Skip empty lists.
                    if (CursorPosition >= 0 && CursorPosition < Items.Count)
                        cur = Items[CursorPosition];
                    else
                        cur = null;
                }
                while (cur != null && !cur.CanTakeFocus());

                //If we ran off the end, it means no valid selections exist past oldpos.
                if (CursorPosition >= Items.Count)
                    CursorPosition = oldpos;
            }
            else if (dir == eScrollDirection.Up)
            {
                Control cur;
                do
                {
                    CursorPosition--;

                    //Skip empty lists.
                    if (CursorPosition >= 0 && CursorPosition < Items.Count)
                        cur = Items[CursorPosition];
                    else
                        cur = null;
                }
                while (cur != null && !cur.CanTakeFocus());

                //If we ran off the end, it means no valid selections exist past oldpos.
                if (CursorPosition < 0)
                    CursorPosition = oldpos;
            }

            if (oldpos != CursorPosition)
            {
                if (oldpos >= 0 && oldpos < Items.Count)
                    Items[oldpos].OnDefocus();
                if (CursorPosition >= 0 && CursorPosition < Items.Count)
                    Items[CursorPosition].OnFocus();
                return eNestedScrollResult.Eaten;
            }
            else
            {
                return eNestedScrollResult.NotEaten;
            }

        }

        public override void ButtonDown(eMFDButton btn)
        {
            base.ButtonDown(btn);

            if (CursorPosition != -1 && CursorPosition < Items.Count)
                Items[CursorPosition].ButtonDown(btn);
        }

        public override void ButtonUp(eMFDButton btn)
        {
            base.ButtonUp(btn);
            if (CursorPosition != -1 && CursorPosition < Items.Count)
                Items[CursorPosition].ButtonUp(btn);
        }

        public override void OnFocus()
        {
            base.OnFocus();

            //Try to select one if we are off the top.
            if (CursorPosition < 0 && Items.Count > 0)
                NestedScroll(eScrollDirection.Down);
            //Or notify the current selection.
            else if (CursorPosition >= 0 && CursorPosition < Items.Count)
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

