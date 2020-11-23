using System;
using System.Collections.Generic;
using System.Text;

namespace KCKProjektConsole
{
    public struct Napis
    {
        public int x;
        public int y;
        public string text;
        public Napis(int x, int y, string napis)
        {
            this.x = x;
            this.y = y;
            this.text = napis;
        }
    }
}
