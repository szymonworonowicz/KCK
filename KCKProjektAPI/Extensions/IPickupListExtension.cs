using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace KCKProjectAPI.Extensions
{
    public static class IPickupListExtension
    {
        public static int GetId<T>(this List<T> list, int x, int y) where T : IPickup
        {
            try
            {
                IPickup temp = list.First(item => item.x == x && item.y == y);
                return temp.id;

            }
            catch(Exception)
            {
                
                throw new KeyNotFoundException("nie znaleziono elementu o danym id");
            }

            

            
        }
        
        public static IPickup GetById<T>(this List<T> list, int id) where T : IPickup
        {
            try
            {
                IPickup temp = list.First(item => item.id == id);
                return temp;
            }
            
            catch(Exception)
            {
                throw new KeyNotFoundException("nie znaleziono elementu o danym id");
            }
            

            
        }
        public static IPickup GetByCoords<T>(this List<T> list , int x, int y) where T: IPickup
        {
            try
            {
                IPickup temp = list.First(item => item.x == x && item.y == y);
                return temp;
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("nie znaleziono elementu o danym id");
            }
            
            
            
        }
        public static void RemoveById<T>(this List<T> list, int id) where T : IPickup
        {
            
            list.RemoveAll(item => item.id == id);

        }
        public static void RemoveByCoord<T>(this List<T> list, int x, int y) where T :IPickup
        {
            list.RemoveAll(item => item.x == x && item.y == y);
        }
        public static string ToStringExtend<T>(this List<T> list) where T : IPickup
        {
            StringBuilder str = new StringBuilder();

            foreach (var elem in list)
            {
                str.Append(elem.ToString());
            }

            return str.ToString();
        }
    }

}