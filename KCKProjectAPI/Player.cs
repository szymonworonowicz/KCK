using System;
using System.Collections.Generic;
using System.Text;

namespace KCKProjectAPI
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Player()
        {
                
        }

        public Player(Player prev)
        {
            this.X = prev.X;
            this.Y = prev.Y;
        }

        public override string ToString()
        {
            return $"{X}{Y}";
        }
    }
}
