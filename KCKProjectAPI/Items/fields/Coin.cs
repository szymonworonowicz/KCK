using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKProjectAPI
{
    public class Coin : IPickup
    {
        public Coin(int id,int x,int y):base(id,x,y,"+")
        {

        }

        public void Rotate()
        {
            if (this.type == "+")
            {
                this.type = "x";
                return;
            }

            this.type = "+";
        }

        public override string ToString()
        {
            return type;
        }
    }
}
