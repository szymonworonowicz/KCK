using System;
using System.Collections.Generic;
using System.Text;
using KCKProjectAPI.Extensions;

namespace KCKProjectAPI
{
    public class PickUps
    {
        public  int PickUpCoin(int x, int y ,List<Coin> coins)
        {
            if (coins.GetId<Coin>(x, y) != -1)
            {
                return 1;
            }

            return 0;
        }

        public  IPickup PickupKey(int x, int y, List<Key> keys)
        {
            int id = keys.GetId(x, y);
            //Console.Out.WriteLine(id);
            if (id != -1)
            {

                Key p = keys.GetById(id) as Key;
                return p;
            }

            return null;
        }

        public  void unlockTheDoor(int x, int y, List<Door> doors, List<Key> ownedKeys)
        {
            int DoorId = doors.GetId(x, y);
            if (DoorId != -1)
            {
                if (ownedKeys.GetById(DoorId) != null)
                {
                    doors.RemoveById(DoorId);
                    ownedKeys.RemoveById(DoorId);
                }
            }
        }
    }
}
