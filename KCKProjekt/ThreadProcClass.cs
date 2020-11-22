using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KCKProjectAPI.Extensions;
using KCKProjektAPI.Items.fields;

namespace KCKProjectAPI
{
    public class ThreadProcClass
    {

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
                }

                Thread.Sleep(100);

            }
        }



        public static void ThreadProcPlayer(ref Player player, ref Map map, ref List<Key> ownedKeys, ref List<Key> keys, ref List<Door> doors, ref List<Coin> coins, ref Exit e, ref object CoinLock, ref int points)
        {
            var readmap = map.builder.getMap() as List<LinkedList<IField>>;
            Cursor.CursorFun(player.X, player.Y, 'P');
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
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X, player.Y - 1, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X, player.Y - 1, doors, ownedKeys);
                                if (status == true)

                                    continue;
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
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X, player.Y + 1, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X, player.Y + 1, doors, ownedKeys);
                                if (status == true)
                                    continue;
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
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X - 1, player.Y, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X - 1, player.Y, doors, ownedKeys);
                                if (status == true)
                                    continue;
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
                        }
                        else
                        {
                            IPickup mapKey = PickUps.PickupKey(player.X + 1, player.Y, keys);
                            if (mapKey != null)
                            {
                                ownedKeys.Add(mapKey as Key);

                            }
                            else
                            {
                                bool status = PickUps.unlockTheDoor(player.X + 1, player.Y, doors, ownedKeys);
                                if (status == true)
                                    continue;
                            }

                        }
                        player.X++;
                        change = true;

                    }

                }
                else if (key == ConsoleKey.P)
                {

                    break;
                }
                if (PickUps.getExit(player.X, player.Y, ref e))
                {
                    break;
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
