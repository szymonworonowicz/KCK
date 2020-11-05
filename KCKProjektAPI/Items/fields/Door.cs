using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKProjectAPI
{
    public class Door : IPickup
    {
        public Door(int _id,int _x,int _y):base(_id,_x,_y,"Door")
        {
        }

    }
}
