using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using KCKProjectAPI.directory;
using KCKProjectAPI;

namespace KCKProjektConsole
{
    public static class Menu
    {

        private struct napis
        {
            public int x;
            public int y;
            public string text;
            public napis(int x, int y, string napis)
            {
                this.x = x;
                this.y = y;
                this.text = napis;
            }
        }
        public static string getMap(int beginRow)
        {
            MapsFiles mf = new MapsFiles();
            mf.initFolders();
            string path = "";



            int id = 0;
            while (path == "")
            {

                for (int i = beginRow; i < beginRow + 5; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string s = mf.getMapByID(i + id - beginRow);
                    if (i == beginRow + 2)
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Cursor.writeString(0, i, s);
                    if (s == null)
                    {
                        Cursor.writeString(0, i, "                                                 ");
                    }
                    else
                        Cursor.writeString(s.Length, i, "                                                 ");


                }
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {

                    if (id > -2)
                        id--;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (mf.getMapByID(id + 3) != null)

                        id++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    path = mf.getMapByID(id + 2);
                }
            }
            Console.Out.WriteLine("read completed :" + path);
            Cursor.setCursorPosition(0, 0);
            return path;
        }
        public static void printMessage(string s)
        {
            Cursor.writeString(0, 0, s);
        }

        public static int getMenu(int poziom = 1)
        {
            object mutex = new object();//mutex
            Console.CursorVisible = false;
            List<string> logo = new List<string>();
            using (StreamReader str = new StreamReader("logo.txt"))
            {
                string line = "";

                while ((line = str.ReadLine()) != null)
                {
                    logo.Add(line);
                }
            }
            int y = 36, x = 100;
            napis[] napisy = new napis[4];
            printMenu(x, y, logo, ref napisy);

            ConsoleKey key;
            int id = 0;
            CancellationTokenSource cancel = new CancellationTokenSource();
            Thread mainmenu = new Thread(() => UpdateConsoleMenuSize(ref x, ref y, ref mutex, ref logo, ref napisy, ref id, cancel));//zmieniaanie asynchronicznie szerokosci okna

            mainmenu.Start();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Cursor.writeString(napisy[id].x - 2, napisy[id].y, "->");
                Cursor.writeString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "<-");
                key = Console.ReadKey(true).Key;

                lock (mutex)
                {
                    switch (key)
                    {

                        case ConsoleKey.DownArrow:
                            Cursor.writeString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.writeString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id + 1) == 4 ? 0 : id + 1;
                            break;
                        case ConsoleKey.UpArrow:
                            Cursor.writeString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.writeString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id - 1) < 0 ? 3 : id - 1;
                            break;
                    }
                }



            } while (key != ConsoleKey.Enter);
            cancel.Cancel();
            return ChooseOption(id);
        }

        private static int ChooseOption(int id)
        {
            if (id == 0)
            {
                return 1;
            }
            else if (id == 1)
            {
                return Option();
            }
            else if (id == 2)
            {
                About();
                getMenu();
            }
            else if (id == 3)
            {
                Environment.Exit(0);
            }
            return 1;
        }
        private static int Option()
        {
            return 1;
        }
        private static void About()
        {

        }
        private static void printMenu(int x, int y, List<string> logo, ref napis[] napisy)
        {
            Console.SetWindowSize(x, y);
            Console.ForegroundColor = ConsoleColor.Magenta;

            int ox = x / 2;
            int oy = y / 2;
            int logoWidth = logo[0].Length;
            int logoHeight = logo.Count;
            try
            {
                for (int i = 0; i < logo.Count; i++)
                {

                    Cursor.writeString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw ex;
            }

            ConsoleHelper.SetCurrentFont("Consolas", 24);
            napisy[0] = new napis(ox - 4, oy - 3, "Nowa Gra");
            napisy[1] = new napis(ox - 2, oy - 1, "Opcje");
            napisy[2] = new napis(ox - 2, oy + 1, "O grze");
            napisy[3] = new napis(ox - 3, oy + 3, "Wyjscie");
            Cursor.writeString(ox - 4, oy - 3, "Nowa Gra");
            Cursor.writeString(ox - 2, oy - 1, "Opcje");
            Cursor.writeString(ox - 2, oy + 1, "O grze");
            Cursor.writeString(ox - 3, oy + 3, "Wyjscie");
        }

        private static void UpdateConsoleMenuSize(ref int x, ref int y, ref object mutex, ref List<string> logo, ref napis[] napisy, ref int id, CancellationTokenSource cancel)
        {
            while (true)
            {
                if (x != Console.WindowWidth || y != Console.WindowHeight)
                {
                    lock (mutex)
                    {
                        try
                        {
                            Console.Clear();
                            Thread.Sleep(10);
                            Console.ForegroundColor = ConsoleColor.White;
                            printMenu(Console.WindowWidth, Console.WindowHeight, logo, ref napisy);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Cursor.writeString(napisy[id].x - 2, napisy[id].y, "->");
                            Cursor.writeString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "<-");
                            x = Console.WindowWidth;
                            y = Console.WindowHeight;
                        }
                        catch (ArgumentOutOfRangeException)
                        {

                            if (Console.WindowWidth < 0)
                            {
                                x = logo[0].Length;
                            }
                        }

                    }

                }
                if (cancel.IsCancellationRequested)
                    break;
            }
        }
    }

}
