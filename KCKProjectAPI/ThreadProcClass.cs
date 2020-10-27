using System;
using System.Linq;
using System.Threading;

namespace KCKProjectAPI
{
    public class ThreadProcClass
    {
       
        public static void ThreadProcCoin(ref Coin coin,in object m)
        {
            Action<int, int, char> act = Cursor.CursorFun;
            while (true)
            {
                lock (m)
                {
                    coin.Rotate();
                    for (int i = 0; i < 10000; i++);
                }

            }
        }
        
        public static void ThreadProcPlayer(ref Player player,ref object mutex,ref bool change,ref Map map)
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                lock (mutex)
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (map.map[player.Y - 1].ElementAt(player.X) is Path)
                        {
                            player.Y--;
                            change = true;
                        }

                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (map.map[player.Y + 1].ElementAt(player.X) is Path)
                        {
                            player.Y++;
                            change = true;
                        }

                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        if (map.map[player.Y].ElementAt(player.X - 1) is Path)
                        {
                            player.X--;
                            change = true;
                        }
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        if (map.map[player.Y].ElementAt(player.X + 1) is Path)
                        {
                            player.X++;
                            change = true;
                        }

                    }
                    else if (key == ConsoleKey.P)
                    {
                        break;
                    }

                }
                
            }

        }

    }
}
