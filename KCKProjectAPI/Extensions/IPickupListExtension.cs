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
            IPickup temp = list.FirstOrDefault(item => item.x == x && item.y == y);

            if (temp != null)
            {
                return temp.id;
            }

            throw new KeyNotFoundException("nie znaleziono elementu o danym id");
        }

        public static IPickup GetById<T>(this List<T> list, int id) where T : IPickup
        {
            IPickup temp = list.FirstOrDefault(item => item.id == id);

            if (temp != null)
            {
                return temp;
            }

            throw new KeyNotFoundException("nie znaleziono elementu o danym id");
        }

        public static void RemoveById<T>(this List<T> list, int id) where T : IPickup
        {
            list.RemoveAll(item => item.id == id);
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