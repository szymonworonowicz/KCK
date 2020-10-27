using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using KCKProjectAPI;
using KCKProjectAPI.Extensions;

namespace KCKProjekt
{
    class Program
    {
        public static List<Key> keys = new List<Key>();
        public static List<Key> ownedKeys = new List<Key>();
        public static List<Door> doors = new List<Door>();
        public static List<Coin> coins = new List<Coin>();

        private static int points = 0;
        //private static ConditionVariable
        private static int x = 0;
        private static int y = 0;
        List<IField> listf = new List<IField>();
        public static int width=30;
        public static int height=30;
        static void Main(string[] args)
        {

            Map mlist = new Map("map2.txt");

            var map = mlist.map;


            for (int i = 0; i < map.Count; i++)
            {
                foreach (var elem in map[i])
                {
                    Console.Write(elem);
                }
                Console.Write('\n');
            }

            //Console.SetCursorPosition(2, 2);
            //Console.Write("K");
            Console.CursorVisible = false;

            ///Console.Out.WriteLine(mlist.ToString());
            /*for (int i = -10; i < 5; ++i)
            {
                Console.Out.WriteLine(mlist.FragmentToString(-10, i, 15));
            }*/


            Coin coin = new Coin(1, 15, 17);
            object moneyMutex = new object();

            Thread Coin = new Thread((() => ThreadProcClass.ThreadProcCoin(ref coin, in moneyMutex)));

            Coin.Start();

            //for (int i = 0; i < 10000; i++)
            //{
            //    lock (moneyMutex)
            //    {
            //        Console.WriteLine(coin.ToString());
            //        for (int r = 0; r < 10000; r++) ;
            //    }
            //}

            Player p = new Player{X = 2,Y=9};
            object mutex = new object();
            bool change = false;
            Thread player = new Thread(() => ThreadProcClass.ThreadProcPlayer(ref p, ref mutex,ref change,ref mlist));
            player.Start();
            List<String> prevMap = new List<String>();
            List<String> currentMap = new List<String>();
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
                        Cursor.CursorFun(prev.X,prev.Y,' ');
                        Cursor.CursorFun(p.X,p.Y,'P');
                        prev = new Player(p);
                        change = false;
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
