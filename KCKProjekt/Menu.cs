﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using KCKProjectAPI;

namespace KCKProjektConsole
{
    public static class Menu
    {
        private delegate void menuAction<T>(int x , int y, List<string> logo, ref T map);
        private static menuAction<napis[]> menuprinter;
        private delegate int optionAction(int id);
        private static optionAction optionprinter;
        private static int levelid = 1;
        static Menu()
        {
            menuprinter = new menuAction<napis[]>(printMenu);
            optionprinter = new optionAction(ChooseOptionMenu);
        }
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
            menuprinter(x, y, logo, ref napisy);
            ConsoleKey key;
            int id = 0;
            CancellationTokenSource cancel = new CancellationTokenSource();

            Thread mainmenu = new Thread(() => UpdateConsoleMenuSize(menuprinter,ref x, ref y, ref mutex, ref logo, ref napisy, ref id, cancel));//zmieniaanie asynchronicznie szerokosci okna

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
            return optionprinter(id);
        }

        private static int ChooseOptionMenu(int id)
        {
            switch(id)
            {
                case 0:
                    return levelid;
                case 1:
                    levelid =  Option();
                    break;
                case 2:
                    About();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
            return levelid;
        }
        private static int ChooseOptionOption(int id)
        {
            switch(id)
            {
                case 0:
                    levelid= 1;
                    break;
                case 1:
                    levelid = 2;
                    break;
                case 2:
                    levelid = 3;
                    break;
            }
            return MenuOption();
        }
        private static int MenuOption()
        {
            Console.Clear();
            menuprinter = new menuAction<napis[]>(printMenu);
            optionprinter = new optionAction(ChooseOptionMenu);

            return getMenu();
        }
        private static int Option()
        {
            Console.Clear();
            menuprinter = new menuAction<napis[]>(printDifficulties);
            optionprinter = new optionAction(ChooseOptionOption);

            return getMenu();
        }

        private static int ChooseOptionAbout(int id) // parametr tylko poto zeby pod delegata moc podpiac
        {
            return MenuOption();
        }
        private static int About()
        {
            Console.Clear();
            menuprinter = new menuAction<napis[]>(printAbout);
            optionprinter = new optionAction(ChooseOptionAbout);

            return getMenu();
        }
        
        private static void printMenu(int x, int y, List<string> logo, ref napis[] napisy)
        {
            try
            {
            Console.SetWindowSize(x, y);

            }
            catch(Exception e)
            {
                Console.SetWindowSize(150, 100);
                x = 100;
                y = 50;
            }
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
        private static void printGameEnd(int x, int y, List<string> logo, ref napis[] napisy)
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
            napisy[0] = new napis(ox - 12, oy - 3, "Ukończyłeś/ukończyłaś grę");
            napisy[1] = new napis(ox - 3, oy - 1, "Wyjście");

            Cursor.writeString(ox - 12, oy - 3, "Ukończyłeś/ukończyłaś grę");
            Cursor.writeString(ox - 3, oy - 1, "Wyjście");

        }
            private static void printAbout(int x, int y, List<string> logo, ref napis[] napisy)
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
            napisy[0] = new napis(ox - 8, oy - 3, "Szymon Woronowicz");
            napisy[1] = new napis(ox - 6, oy - 1, "Julia Gejdel");
            napisy[2] = new napis(ox - 7, oy + 1, "Paweł Krzywosz");
            napisy[3] = new napis(ox - 3, oy + 3, "Wyjscie");
            Cursor.writeString(ox - 8, oy - 3, "Szymon Woronowicz");
            Cursor.writeString(ox - 6, oy - 1, "Julia Gejdel");
            Cursor.writeString(ox - 7, oy + 1, "Paweł Krzywosz");
            Cursor.writeString(ox - 3, oy + 3, "Wyjscie");
        }
        private static void printDifficulties(int x, int y, List<string> logo, ref napis[] napisy)
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
            napisy[0] = new napis(ox - 3, oy - 3, "łatwy");
            napisy[1] = new napis(ox - 3, oy - 1, "średni");
            napisy[2] = new napis(ox - 3, oy + 1, "trudny");
            napisy[3] = new napis(ox - 3, oy + 3, "Wyjscie");
            Cursor.writeString(ox - 3, oy - 3, "łatwy");
            Cursor.writeString(ox - 3, oy - 1, "średni");
            Cursor.writeString(ox - 3, oy + 1, "trudny");
            Cursor.writeString(ox - 3, oy + 3, "Wyjscie");
        }
        private static void UpdateConsoleMenuSize(menuAction<napis[]> menu, ref int x, ref int y, ref object mutex, ref List<string> logo, ref napis[] napisy, ref int id, CancellationTokenSource cancel)
        {
            while (true)
            {
                if (x != Console.WindowWidth || y != Console.WindowHeight)
                {
                    lock (mutex)
                    {
                        try
                        {
                            Console.CursorVisible = false;
                            Console.Clear();
                            Thread.Sleep(10);
                            Console.ForegroundColor = ConsoleColor.White;

                            //printMenu(Console.WindowWidth, Console.WindowHeight, logo, ref napisy);


                            menu(x, y, logo, ref napisy);
                            //switch (currentmenu)
                            //{
                            //    case "main menu":

                            //        printMenu(x, y, logo, ref napisy);
                            //        break;

                            //    case "difficulties":

                            //        printDifficulties(x, y, logo, ref napisy);
                            //        break;
                            //    case "about":
                            //        printAbout(x, y, logo, ref napisy);
                            //        break;
                            //}
                            //printDifficulties(Console.WindowWidth, Console.WindowHeight, logo, ref napisy);
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
