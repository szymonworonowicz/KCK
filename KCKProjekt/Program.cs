using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using KCKProjectAPI;
using KCKProjectAPI.Builders;
using KCKProjectAPI.Extensions;

using KCKProjektConsole;
using KCKProjektAPI.Items.fields;
using KCKProjecConsole;
using KCKProjectConsole;

namespace KCKProjekt
{
    class Program
    {
        private static List<Key> keys = new List<Key>();
        private static List<Key> ownedKeys = new List<Key>();
        private static List<Door> doors = new List<Door>();
        private static List<Coin> coins = new List<Coin>();
        private static Exit exit;
        private static int points = 0;
        List<IField> listf = new List<IField>();
       
        

        static void Main(string[] args)
        {
            while (1 == 1)
            {

                int level = Menu.getMenu();
                // Menu.printMessage("proszę wybrać mapę");
                // string path = Menu.getMap(8);

                object writer = new object(); // mutex do wyisywania
                IBuilder builder = new ConsoleBuilder();
                Map mlist = new Map("map2", builder);


                var map = mlist.getMap() as List<LinkedList<IField>>;
                mlist.GetElems(ref keys, ref doors, ref coins);
                mlist.GetExit(ref exit);
                Console.Clear();
                Console.SetWindowSize(150, 30);
                //write map
                for (int i = 0; i < map.Count; i++)
                {
                    foreach (var elem in map[i])
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
                Cursor.CursorFun(exit.x, exit.y, 'E');
                //write elems
                foreach (var key in keys)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Cursor.CursorFun(key.x, key.y, 'K');
                }

                foreach (var door in doors)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Cursor.CursorFun(door.x, door.y, 'D');
                }


                Thread coin = new Thread(() => ThreadProcClass.ThreadProcCoin(coins, ref writer));
                coin.Start();

                Console.ForegroundColor = ConsoleColor.White;

                Console.CursorVisible = false;


                Player p = new Player { X = 2, Y = 9 }; ///2 9. -- 1 19
                object mutex = new object();

                Thread player = new Thread(() => ThreadProcClass.ThreadProcPlayer(ref p, ref mlist, ref ownedKeys, ref keys, ref doors, ref coins, ref exit, ref writer,ref points));
                player.Start();
                List<string> prevMap = new List<string>();
                List<string> currentMap = new List<string>();


                player.Join();
                lock (writer)
                {

                    coins.Clear();
                }
                Console.Clear();
                EndGameMenu.getMenu(points);
                Console.Clear();
            }
            
        }

    }
}
