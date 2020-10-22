using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKProjectAPI
{
    public class Coin : IPickup
    {
        public Coin(int id,int x,int y):base(id,x,y,"x")
        {

        }

        public void Rotate()
        {
            if (this.type == "x")
            {
                this.type = "+";
                return;
            }

            this.type = "x";
        }

        public override string ToString()
        {
            return type;
        }
    }
}
