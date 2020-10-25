using System;
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
        
        public static void ThreadProcPlayer(ref Player player,ref object mutex)
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                lock (mutex)
                {
                    if (key == ConsoleKey.W)
                    {
                        player.Y--;
                    }
                    else if (key == ConsoleKey.S)
                    {
                        player.Y++;
                    }
                    else if (key == ConsoleKey.A)
                    {
                        player.X--;
                    }
                    else if (key == ConsoleKey.D)
                    {
                        player.X++;
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
