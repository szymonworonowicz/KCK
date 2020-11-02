using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using KCKProjectAPI;
using KCKProjectAPI.Builders;
using KCKProjectAPI.Extensions;
using KCKProjectAPI.Items;
using KCKProjectAPI.directory;
using KCKProjektConsole;
namespace KCKProjekt
{
    class Program
    {
        private static List<Key> keys = new List<Key>();
        private static List<Key> ownedKeys = new List<Key>();
        private static List<Door> doors = new List<Door>();
        private static List<Coin> coins = new List<Coin>();
        private static List<ThreadInfo> coinThreads = new List<ThreadInfo>();

        private static int points = 0;
        //private static ConditionVariable
        private static int x = 0;
        private static int y = 0;
        List<IField> listf = new List<IField>();
       
        

        static void Main(string[] args)
        {
            int level =  Menu.getMenu();
            Menu.printMessage("proszę wybrać mapę");
            string path = Menu.getMap(8);
            
            object writer = new object(); // mutex do wyisywania
            IBuilder builder = new ConsoleBuilder();
            Map mlist = new Map(path, builder);


            var map = mlist.map;
            mlist.GetElems(ref keys, ref doors, ref coins);

            //write map
            for (int i = 0; i < map.Count; i++)
            {
                foreach (var elem in map[i])
                {
                    Console.Write(elem);
                }
                Console.Write('\n');
            }

            //write elems
            foreach (var key in keys)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Cursor.CursorFun(key.x, key.y, 'K');
            }

            foreach (var door in doors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Cursor.CursorFun(door.x, door.y, 'D');
            }


            foreach (var coin in coins)
            {
                ThreadInfo info = new ThreadInfo()
                {
                    CoinId = coin.id,
                    Thread = new Thread(() => ThreadProcClass.ThreadProcCoin(coin, ref writer))
                };
                info.Thread.Start();
                coinThreads.Add(info);
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorVisible = false;

            ///Console.Out.WriteLine(mlist.ToString());
            /*for (int i = -10; i < 5; ++i)
            {
                Console.Out.WriteLine(mlist.FragmentToString(-10, i, 15));
            }*/



            Player p = new Player { X = 2, Y = 9 };
            object mutex = new object();

            bool change = false;
            Thread player = new Thread(() => ThreadProcClass.ThreadProcPlayer(ref p, ref mutex, ref change, ref mlist));
            player.Start();
            List<string> prevMap = new List<string>();
            List<string> currentMap = new List<string>();
            //for(int i = 0;i<height;++i)
            //{
            //    currentMap.Add(new String());
            //    for(int u = 0;u<height;++u)
            //    {

            //    }
            //}
            //Cursor c = new Cursor();
            Cursor.CursorFun(p.X, p.Y, 'P');
            Player prev = new Player(p);
            while (true)
            {
                // prevMap = currentMap;
                //currentMap = new List<string>();
                lock (mutex)
                {

                    if (change == true)
                    {
                        lock (writer)
                        {
                            Cursor.CursorFun(prev.X, prev.Y, ' ');
                        Cursor.CursorFun(p.X, p.Y, 'P');
                        prev = new Player(p);
                        change = false;
                        }
                    }
                    else
                    {
                        p = new Player(prev);
                    }
                    //NIE DOKONCZONE NIE DZIALA JESZCZE
                    /* int consoleX = 0;
                     int consoleY = 0;
                     for ( int i = p.Y - 10; i < p.Y + 10; ++i)
                     {
                         currentMap[consoleY] = mlist.FragmentToString(x, i, 21);
                         for(int u=0;u<currentMap[i].Length;++i)
                         {
                             if (currentMap[consoleY][consoleX] != prevMap[consoleY][consoleX])
                             {
                                 Cursor.CursorFun(consoleX, consoleY, currentMap[consoleY][consoleX]);
                             }
                             consoleY++;
                         }
                         consoleX++;

                     }*/
                    //Thread.Sleep(100);
                }

                if (!player.IsAlive)
                {
                    break;
                }

            }


            //Key k = new Key(10, 1, 1);
            //keys.Add(k);

            //k = new Key(7, 2, 2);
            //keys.Add(k);

            //Door d = new Door(10, 3, 3);
            //doors.Add(d);

            //d = new Door(7, 5, 5);

            //doors.Add(d);

            //PickUps pick = new PickUps();
            ////zbierz klucz z mapy

            ////sprawdz czy mozesz otworzyc drzwi
            //IPickup own = pick.PickupKey(1, 1, keys);
            //ownedKeys.Add((Key)own);
            //Console.Out.WriteLine(keys.ToStringExtend());
            //Console.Out.WriteLine(ownedKeys.ToStringExtend());
            //Console.Out.WriteLine(doors.ToStringExtend());

            //pick.unlockTheDoor(3, 3, doors, ownedKeys);

            //Console.Out.WriteLine(keys.ToStringExtend());
            //Console.Out.WriteLine(ownedKeys.ToStringExtend());
            //Console.Out.WriteLine(doors.ToStringExtend());

            //pick.unlockTheDoor(5, 5, doors, ownedKeys);

            //Console.Out.WriteLine(keys.ToString());
            //Console.Out.WriteLine(ownedKeys.ToString());
            //Console.Out.WriteLine(doors.ToString());






            Console.In.ReadLine();
        }
    }
}
