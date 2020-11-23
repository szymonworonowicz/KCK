using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KCKProjectAPI.Extensions;
using KCKProjektAPI.Items.fields;

namespace KCKProjectAPI
{
    public class PickUps
    {
        public static int PickUpCoin(int x, int y ,List<Coin> coins,ref object coinLock)
        {
            lock(coinLock)
            try
            {

                int id = coins.GetId(x, y);
                   // if (System.Windows.Application.Current == null)
                       // Cursor.CursorFun(x, y,' ' ) ;
                coins.RemoveById(id);
                
                    return 1;
                
            }
            catch(KeyNotFoundException )
            {

                return 0;
            }

        }
        public static bool getExit(int x, int y , ref Exit e)
        {
            
            
                if(x==e.x && y==e.y)
                {
                    return true;
                }
                return false;
            
        }
        public static  IPickup PickupKey(int x, int y, List<Key> keys) 
        {
            try
            {
              //  int id = keys.GetId(x, y);
                IPickup key = keys.GetByCoords(x, y);
                //if (System.Windows.Application.Current == null)
                    //Cursor.CursorFun(x, y,' ');
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

               // if (System.Windows.Application.Current == null)
                 //   Cursor.CursorFun(x, y,' ');
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
