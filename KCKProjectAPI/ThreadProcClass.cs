using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using KCKProjectAPI;

namespace KCKProjectAPI
{
    class ThreadProcClass
    {
        public static void ThreadProcCoin(Coin coin)
        {
            IField f = new Path(1,1,1);
            Action<int, int, char> act = Cursor.CursorFun;
            Action sleep = () => Thread.Sleep(1000);
            while (1 == 1)
            {

                for (int i = 0; i < 10; i++)
                {

                    /*Console.SetCursorPosition(((i + 10-1)%10)+x, 0+y);
                    for(int u = 0;u<f.ToString().Length;++u)
                    {
                        Console.Out.Write(" ");
                    
                    }*/
                    act(((i + 10 - 1) % 10) + coin.x, coin.y, ' ');
                    act(((i + 10) % 10) + coin.x, coin.y, 'd');

                    //Console.SetCursorPosition(((i+10)%10)+x, 0+y);
                    //Console.Out.Write(f.ToString());



                    //x.Insert((i-1+10)%10, null);

                    sleep();
                }
            }
        }

        public static void ThreadProcPlayer(Player player)
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    player.Y--;
                }
                else if (Console.ReadKey().Key == ConsoleKey.DownArrow)
                {
                    player.Y++;
                }
                else if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                {
                    player.X--;
                }
                else if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                {
                    player.X++;
                }
                else if (Console.ReadKey().Key == ConsoleKey.P)
                {
                    break;
                }
            }

        }

    }
}
