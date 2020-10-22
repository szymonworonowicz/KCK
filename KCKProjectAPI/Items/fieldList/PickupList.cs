using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KCKProjectAPI
{
    public class PickupList<T> where T : IPickup //: IPickupList
    {
        protected List<T> list;

        public PickupList()
        {
            list = new List<T>();

        }
        public void Add(T p)
        {
            list.Add(p);
        }
       /* public IPickup Interact(int x,int y)
        {
            IPickup p = list.Where(item => item.x == x && item.y ==y).ToList().First();
            IPickup toSend = null;
            if (p != null)
            {
                toSend = p;
            }
            list.Remove((Key)p);
            return toSend;
        }*/

        public int GetId(int x, int y)
        {
            List<T> p = list.Where(item => item.x == x && item.y == y).ToList();
            if (p != null && p.Any())
            {
                return p.First().id;
            }
            return -1;

        }
        public IPickup GetById(int id)
        {
            List<T> p = list.Where(item => item.id == id).ToList();
            if(p!= null  && p.Any())
            {
                return p.First();
            }
            return null;
        }
            
        public Boolean RemoveById(int id)
        {
            list.RemoveAll(item => item.id == id);
            return true;
        }
        public override string ToString()
        {
            return String.Join(", ", list);
        }

    }
}
