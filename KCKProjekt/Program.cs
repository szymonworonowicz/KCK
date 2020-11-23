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
using KCKProjektAPI;

namespace KCKProjekt
{
    class Program
    {
        private static List<Key> keys = new List<Key>();
        private static List<Key> ownedKeys = new List<Key>();
        private static List<Door> doors = new List<Door>();
        private static List<Coin> coins = new List<Coin>();
        private static Exit exit;
        private static int points ;
        List<IField> listf = new List<IField>();
       
        

        static void Main(string[] args)
        {
            while (true)
            {
                points = 0;
                LevelEnum level = Menu.GetMenu();
                
                object writerMutex = new object(); 
                IBuilder builder = new ConsoleBuilder();
                var mlist = new Map(level, builder);


                var map = mlist.getMap() as List<LinkedList<IField>>;
                mlist.GetElems(ref keys, ref doors, ref coins,ref exit);
                Console.Clear();
                Console.SetWindowSize(150, 30);
                Console.Clear();

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
                Cursor.WriteString(map[0].Count+3, 1, "Points: 0");
                Cursor.WriteString(map[0].Count+3, 2, "Keys:   0");
                Console.ForegroundColor = ConsoleColor.White;
                Cursor.CursorFun(exit.x, exit.y, 'E');

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


                Thread CoinRotateThread = new Thread(() => ThreadProcClass.ThreadProcCoin(coins, ref writerMutex));
                CoinRotateThread.Start();

                Console.ForegroundColor = ConsoleColor.White;

                Console.CursorVisible = false;


                Player p = new Player { X = 2, Y = 9 };
                object mutex = new object();

                Thread PlayerThread = new Thread(() => ThreadProcClass.ThreadProcPlayer(ref p, ref mlist, ref ownedKeys, ref keys, ref doors, ref coins, ref exit, ref writerMutex,ref points));
                PlayerThread.Start();

                PlayerThread.Join();
               
                lock (writerMutex)
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
