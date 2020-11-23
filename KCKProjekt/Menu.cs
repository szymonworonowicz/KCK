using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using KCKProjecConsole;
using KCKProjectAPI;
using KCKProjektAPI;

namespace KCKProjektConsole
{
    public static class Menu
    {
        private delegate void MenuAction<T>(int x, int y, List<string> logo, ref T map);
        private static MenuAction<Napis[]> _menuprinter;
        private delegate LevelEnum OptionAction(int id);
        private static OptionAction _optionprinter;
        private static LevelEnum level = LevelEnum.Easy;
        static Menu()
        {
            _menuprinter = PrintMenu;
            _optionprinter = ChooseOptionMenu;
        }
        public static LevelEnum GetMenu(LevelEnum level = LevelEnum.Easy)
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
                str.Close();
            }
            int y = 36, x = 100;
            var napisy = new Napis[4];
            _menuprinter(x, y, logo, ref napisy);
            ConsoleKey key;
            int id = 0;
            CancellationTokenSource cancel = new CancellationTokenSource();

            Thread MainMenuRefreshThread = new Thread(() => UpdateConsoleMenuSize(_menuprinter, ref x, ref y, ref mutex, ref logo, ref napisy, ref id, cancel));

            MainMenuRefreshThread.Start();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "->");
                Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "<-");
                key = Console.ReadKey(true).Key;

                lock (mutex)
                {
                    switch (key)
                    {

                        case ConsoleKey.DownArrow:
                            Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id + 1) == 4 ? 0 : id + 1;
                            break;
                        case ConsoleKey.UpArrow:
                            Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id - 1) < 0 ? 3 : id - 1;
                            break;
                    }
                }



            } while (key != ConsoleKey.Enter);
            cancel.Cancel();
            return _optionprinter(id);
        }

        private static LevelEnum ChooseOptionMenu(int id)
        {
            switch (id)
            {
                case 0:
                    return level;
                case 1:
                    level = Option();
                    break;
                case 2:
                    About();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
            return level;
        }
        private static LevelEnum ChooseOptionOption(int id)
        {
            switch (id)
            {
                case 0:
                    level = LevelEnum.Easy;
                    break;
                case 1:
                    level = LevelEnum.Mid;
                    break;
                case 2:
                    level = LevelEnum.Hard;
                    break;
            }
            return MenuOption();
        }
        private static LevelEnum MenuOption()
        {
            Console.Clear();
            _menuprinter = PrintMenu;
            _optionprinter = ChooseOptionMenu;

            return GetMenu();
        }
        private static LevelEnum Option()
        {
            Console.Clear();
            _menuprinter = PrintDifficulties;
            _optionprinter = ChooseOptionOption;

            return GetMenu();
        }

        private static LevelEnum ChooseOptionAbout(int id) 
        {
            return MenuOption();
        }
        private static LevelEnum About()
        {
            Console.Clear();
            _menuprinter = PrintAbout;
            _optionprinter = ChooseOptionAbout;

            return GetMenu();
        }

        private static void PrintMenu(int x, int y, List<string> logo, ref Napis[] napisy)
        {
            try
            {
                Console.SetWindowSize(x, y);

            }
            catch (Exception)
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

                    Cursor.WriteString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw ex;
            }

            ConsoleHelper.SetCurrentFont("Consolas", 24);
            napisy[0] = new Napis(ox - 4, oy - 3, "Nowa Gra");
            napisy[1] = new Napis(ox - 2, oy - 1, "Opcje");
            napisy[2] = new Napis(ox - 2, oy + 1, "O grze");
            napisy[3] = new Napis(ox - 3, oy + 3, "Wyjscie");
            Cursor.WriteString(ox - 4, oy - 3, "Nowa Gra");
            Cursor.WriteString(ox - 2, oy - 1, "Opcje");
            Cursor.WriteString(ox - 2, oy + 1, "O grze");
            Cursor.WriteString(ox - 3, oy + 3, "Wyjscie");
        }
        private static void PrintAbout(int x, int y, List<string> logo, ref Napis[] napisy)
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

                    Cursor.WriteString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw ex;
            }

            ConsoleHelper.SetCurrentFont("Consolas", 24);
            napisy[0] = new Napis(ox - 8, oy - 3, "Szymon Woronowicz");
            napisy[1] = new Napis(ox - 6, oy - 1, "Julia Gejdel");
            napisy[2] = new Napis(ox - 7, oy + 1, "Paweł Krzywosz");
            napisy[3] = new Napis(ox - 3, oy + 3, "Wyjscie");
            Cursor.WriteString(ox - 8, oy - 3, "Szymon Woronowicz");
            Cursor.WriteString(ox - 6, oy - 1, "Julia Gejdel");
            Cursor.WriteString(ox - 7, oy + 1, "Paweł Krzywosz");
            Cursor.WriteString(ox - 3, oy + 3, "Wyjscie");
        }
        private static void PrintDifficulties(int x, int y, List<string> logo, ref Napis[] napisy)
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

                    Cursor.WriteString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw ex;
            }

            ConsoleHelper.SetCurrentFont("Consolas", 24);
            napisy[0] = new Napis(ox - 3, oy - 3, "łatwy");
            napisy[1] = new Napis(ox - 3, oy - 1, "średni");
            napisy[2] = new Napis(ox - 3, oy + 1, "trudny");
            napisy[3] = new Napis(ox - 3, oy + 3, "Wyjscie");
            Cursor.WriteString(ox - 3, oy - 3, "łatwy");
            Cursor.WriteString(ox - 3, oy - 1, "średni");
            Cursor.WriteString(ox - 3, oy + 1, "trudny");
            Cursor.WriteString(ox - 3, oy + 3, "Wyjscie");
        }
        private static void UpdateConsoleMenuSize(MenuAction<Napis[]> menu, ref int x, ref int y, ref object mutex, ref List<string> logo, ref Napis[] napisy, ref int id, CancellationTokenSource cancel)
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
                            menu(x, y, logo, ref napisy);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "->");
                            Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "<-");
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
