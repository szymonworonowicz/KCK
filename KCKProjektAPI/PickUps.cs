using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KCKProjectAPI.Extensions;
using KCKProjectAPI.Items;

namespace KCKProjectAPI
{
    public class PickUps
    {
        public static int PickUpCoin(int x, int y ,List<Coin> coins)
        {
            try
            {

                int id = coins.GetId(x, y);
                Cursor.writeString(x, y, " ");
                coins.RemoveById(id);
                
                /*try
                {
                    ThreadInfo t = threads.First(item => item.CoinId == id);
                    t.Thread.Abort();

                }
                catch(Exception e)
                {
                    //throw new Exception("thread not found");
                }
                finally
                {

                }*/
                
                    return 1;
                
            }
            catch(KeyNotFoundException )
            {

                return 0;
            }

        }

        public static  IPickup PickupKey(int x, int y, List<Key> keys) 
        {
            try
            {
              //  int id = keys.GetId(x, y);
                IPickup key = keys.GetByCoords(x, y);
                Cursor.writeString(x, y, " ");
                //Console.Out.WriteLine(id);
                return key;
            }   
            catch(KeyNotFoundException )
            {
                return null;
            }
            
           
        }

        public static bool unlockTheDoor(int x, int y, List<Door> doors, List<Key> ownedKeys)
        {
            
            try
            {
                int DoorId = doors.GetId(x, y); //if exc mozna przejsc - false
                try
                {
                    ownedKeys.GetById(DoorId); // jesli exc nie mozna przejsc -true
                    
                }
                catch(KeyNotFoundException )
                {
                    return true;
                }


                Cursor.writeString(x, y, " ");
                doors.RemoveById(DoorId);
                ownedKeys.RemoveById(DoorId);
                return false;
            }
            catch(KeyNotFoundException )
            {
                return false;
            }
            
            
        }
        
    }
}
