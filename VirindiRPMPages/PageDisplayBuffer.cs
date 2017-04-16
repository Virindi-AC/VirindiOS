using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VirindiRPMPages
{
    public class PageDisplayBuffer
    {
        public struct sDisplayCharacter
        {
            public char ch;
            public Color32 color;
        }

        bool iMonochrome;
        int iWidth;
        int iHeight;

        public int Width{ get{ return iWidth; }}
        public int Height{ get{ return iHeight; }}
        public bool IsMonochrome { get { return iMonochrome; } }
        public sDisplayCharacter[,] Buffer;
        public int CursorX = 0;
        public int CursorY = 0;
        public Color CursorColor = Color.white;

        public InternalProp InfoInternalProp;
        public MFDKeyMap InfoKeyMap;
        public TextUI.OnScreenKeypad OSK_Numeric = new TextUI.OnScreenKeypad();
        public VirindiPageMultiplexer InfoMainController;

        public PageDisplayBuffer(int w, int h, bool monochrome)
        {
            iWidth = w;
            iHeight = h;
            iMonochrome = monochrome;
            Buffer = new sDisplayCharacter[w, h];
        }

        public void Clear()
        {
            CursorX = -1;
            CursorY = 0;
            CursorColor = Color.white;
            for (int x = 0; x < iWidth; ++x)
            {
                for (int y = 0; y < iHeight; ++y)
                {
                    Buffer[x, y].ch = ' ';
                    Buffer[x, y].color = Color.white;
                }
            }
        }

        public void AppendWrapped(char ch)
        {
            if (ch == '\n')
            {
                CursorX = -1;
                ++CursorY;
            }
            else if (ch == '\r')
            {

            }
            else
            {
                ++CursorX;
                if (CursorX >= iWidth)
                {
                    CursorX = 0;
                    ++CursorY;
                }

                //Off the bottom of the screen?
                if (CursorY >= iHeight)
                    return;
            
                Buffer[CursorX, CursorY].ch = ch;
                Buffer[CursorX, CursorY].color = CursorColor;
            }
        }

        public void Append(char ch)
        {
            if (ch == '\n')
            {
                CursorX = -1;
                ++CursorY;
            }
            else if (ch == '\r')
            {

            }
            else
            {
                ++CursorX;

                //Off the right of the screen?
                if (CursorX >= iWidth)
                    return;

                //Off the bottom of the screen?
                if (CursorY >= iHeight)
                    return;

                Buffer[CursorX, CursorY].ch = ch;
                Buffer[CursorX, CursorY].color = CursorColor;
            }
        }

        public void Append(string s)
        {
            //Off the bottom of the screen?
            if (CursorY >= iHeight)
                return;

            for (int i = 0; i < s.Length; ++i)
                Append(s[i]);
        }

        public void AppendFormat(string s, params object[] p)
        {
            //Off the bottom of the screen?
            if (CursorY >= iHeight)
                return;

            string sout = string.Format(s, p);

            for (int i = 0; i < sout.Length; ++i)
                Append(sout[i]);
        }

        public void AppendWrapped(string s)
        {
            //Off the bottom of the screen?
            if (CursorY >= iHeight)
                return;

            for (int i = 0; i < s.Length; ++i)
                AppendWrapped(s[i]);
        }

        public void AppendFormatWrapped(string s, params object[] p)
        {
            //Off the bottom of the screen?
            if (CursorY >= iHeight)
                return;

            string sout = string.Format(s, p);

            for (int i = 0; i < sout.Length; ++i)
                AppendWrapped(sout[i]);
        }

        public void AppendLine(string s)
        {
            //Off the bottom of the screen?
            if (CursorY >= iHeight)
                return;

            for (int i = 0; i < s.Length; ++i)
                Append(s[i]);

            AppendLine();
        }

        public void AppendLine()
        {
            CursorX = -1;
            ++CursorY;
        }

        static void AppendColorTag(Color32 color, StringBuilder sb)
        {
            sb.Append("[");
            sb.Append(XKCDColors.ColorTranslator.ToHexA(color));
            sb.Append("]");
        }

        public void AppendToBuilder(StringBuilder sb, int leftmargin)
        {
            Color lastcolor = Color.white;
            if (!iMonochrome)
                AppendColorTag(lastcolor, sb);
            for (int y = 0; y < iHeight; ++y)
            {
                for (int i = 0; i < leftmargin; ++i)
                    sb.Append(' ');
                
                for (int x = 0; x < iWidth; ++x)
                {
                    if (!iMonochrome && Buffer[x, y].color != lastcolor)
                    {
                        lastcolor = Buffer[x, y].color;
                        AppendColorTag(lastcolor, sb);
                    }
                    sb.Append(Buffer[x, y].ch);
                }
                sb.AppendLine();
                lastcolor = Color.white;
            }
        }

        public void WriteLeftAndRight(string left, Color leftcolor, string right, Color rightcolor)
        {
            //Off the bottom of the screen?
            if (CursorY >= iHeight)
                return;

            CursorColor = leftcolor;
            Append(left);

            //Right justify...
            CursorX = Width - 1 - right.Length;
            CursorColor = rightcolor;
            AppendLine(right);
        }

        public void WriteLeftAndRight(string left, string right)
        {
            WriteLeftAndRight(left, Color.white, right, Color.white);
        }

    }
}

