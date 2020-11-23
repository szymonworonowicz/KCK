using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KCKProjectAPI;
using KCKProjectAPI.Extensions;
using KCKProjektAPI.Items.fields;
using KCKProjektConsole;

namespace KCKProjectConsole
{
    public class ThreadProcClass
    {
        private static bool pause;
        private static bool areCoinsPaused;
        static ThreadProcClass()
        {
            areCoinsPaused = false;
            pause = false;
        }
        public static void ThreadProcCoin(List<Coin> coins, ref object writer)
        {
            int numberOFCoins = coins.Count;
            while (true)
            {
                lock (writer)//wypisywanie
                {
                    for (int i = 0; i < coins.Count; i++)
                    {
                        coins[i].Rotate();
                        Thread.Sleep(50);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Cursor.CursorFun(coins[i].x, coins[i].y, coins[i].type[0]);
                    }
                    if(pause==true)
                    {
                        areCoinsPaused = true;
                        while (pause == true)
                        {
                            Thread.Sleep(100);
                        
                        }
                        areCoinsPaused = false;
                    }
                }
                
                if(coins.Count==0)
                {
                    break;
                }
                Thread.Sleep(100);

            }
        }



        public static void ThreadProcPlayer(ref Player player, ref Map map, ref List<Key> ownedKeys, ref List<Key> keys, ref List<Door> doors, ref List<Coin> coins, ref Exit e, ref object CoinLock, ref int points)
        {
            var readmap = map.builder.getMap() as List<LinkedList<IField>>;
            Cursor.CursorFun(player.X, player.Y, 'P');
            int startCoinsCount = coins.Count;
            Player prev = new Player(player);
            bool change = false;
            while (true)
            {
                ConsoleKey key;
                key = Console.ReadKey(true).Key;
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    //break;
                }

                //lock (mutex)
                //{

                if (key == ConsoleKey.UpArrow)
                {
                    if (readmap[player.Y - 1].ElementAt(player.X) is Path)
                    {

                        if (PickUps.PickUpCoin(player.X, player.Y - 1, coins, ref CoinLock) == 1)
                        {
                            ++points;

                            lock (CoinLock)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Cursor.writeString(readmap[0].Count + 11, 1, points.ToString());
                            }
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X, player.Y - 1, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                                lock (CoinLock)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString());
                                }
                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X, player.Y - 1, doors, ownedKeys);
                                if (status == true)

                                    continue;
                                else
                                {

                                    lock (CoinLock)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString() + " ");
                                    }
                                }
                            }

                        }
                        player.Y--;
                        change = true;


                    }


                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (readmap[player.Y + 1].ElementAt(player.X) is Path)
                    {
                        if (PickUps.PickUpCoin(player.X, player.Y + 1, coins, ref CoinLock) == 1)
                        {
                            ++points;

                            lock (CoinLock)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Cursor.writeString(readmap[0].Count + 11, 1, points.ToString());
                            }
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X, player.Y + 1, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                                lock (CoinLock)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString());
                                }
                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X, player.Y + 1, doors, ownedKeys);
                                if (status == true)
                                    continue;
                                else
                                {

                                    lock (CoinLock)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString() + " ");
                                    }
                                }
                            }


                        }
                        player.Y++;
                        change = true;
                    }

                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (readmap[player.Y].ElementAt(player.X - 1) is Path)
                    {
                        if (PickUps.PickUpCoin(player.X - 1, player.Y, coins, ref CoinLock) == 1)
                        {
                            ++points;

                            lock (CoinLock)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Cursor.writeString(readmap[0].Count + 11, 1, points.ToString());
                            }
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X - 1, player.Y, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                                lock (CoinLock)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString());
                                }
                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X - 1, player.Y, doors, ownedKeys);
                                if (status == true)
                                    continue;
                                else
                                {

                                    lock (CoinLock)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString() + " ");
                                    }
                                }
                            }


                        }
                        player.X--;

                        change = true;
                    }
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    if (readmap[player.Y].ElementAt(player.X + 1) is Path)
                    {
                        if (PickUps.PickUpCoin(player.X + 1, player.Y, coins, ref CoinLock) == 1)
                        {
                            ++points;

                            lock (CoinLock)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Cursor.writeString(readmap[0].Count + 11, 1, points.ToString());
                            }

                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X + 1, player.Y, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);
                                lock (CoinLock)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString());
                                }
                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X + 1, player.Y, doors, ownedKeys);
                                if (status == true)
                                    continue;
                                else
                                {
                                    lock (CoinLock)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Cursor.writeString(readmap[0].Count + 11, 2, ownedKeys.Count.ToString() + " ");
                                    }
                                }
                            }

                        }
                        player.X++;
                        change = true;

                    }

                }
                else if (key == ConsoleKey.P)
                {

                    pause = true;
                    while(areCoinsPaused==false)
                    {
                        Thread.Sleep(50);
                    }
                    int result = PauseMenu.getMenu(points);
                    Console.Clear();
                    Console.SetWindowSize(150, 30);
                    
                    if (result == 0)
                    {
                        
                        for (int i = 0; i < readmap.Count; i++)
                        {
                            foreach (var elem in readmap[i])
                            {
                                if (elem is KCKProjectAPI.Path)
                                {
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else if (elem is Wall)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                Console.Write(elem);
                            }
                            Console.Write('\n');
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Cursor.CursorFun(player.X, player.Y, 'P');
                        Console.ForegroundColor = ConsoleColor.White;
                        Cursor.writeString(readmap[0].Count + 3, 1, "Punkty: "+points.ToString());
                        Cursor.writeString(readmap[0].Count + 3, 2, "Klucze: "+ownedKeys.Count.ToString());
                        Console.ForegroundColor = ConsoleColor.White;
                        Cursor.CursorFun(e.x, e.y, 'E');
                        //write elems
                        foreach (var Mapkey in keys)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Cursor.CursorFun(Mapkey.x, Mapkey.y, 'K');
                        }

                        foreach (var door in doors)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Cursor.CursorFun(door.x, door.y, 'D');
                        }
                        pause = false;
                    }
                    else if(result ==1)
                    {
                        pause = false;
                        break;
                    }
                    
                    
                }
               
                if (PickUps.getExit(player.X, player.Y, ref e) && points == startCoinsCount)
                {
                    break;
                }
                else if(PickUps.getExit(player.X, player.Y, ref e))
                {
                    change = false;
                }
                //}
                if (change == true)
                {
                    lock (CoinLock)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Cursor.CursorFun(prev.X, prev.Y, ' ');
                        Cursor.CursorFun(player.X, player.Y, 'P');
                        prev = new Player(player);
                        change = false;
                    }
                }
                else
                {
                    player = new Player(prev);
                }

            }

        }

    }
}
