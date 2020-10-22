using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKProjectAPI
{
    public class Key :IPickup
    {
        public Key(int _id,int _x,int _y):base(_id,_x,_y,"Key")
        {

        }

    }
}
