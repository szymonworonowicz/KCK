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

namespace KCKProjektConsole
{
    public static class EndGameMenu
    {
        static EndGameMenu()
        {
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
        
        public static int getMenu(int points,int poziom = 1)
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
            logo.Add(" ");
            logo.Add(" ");
            int y = 36, x = 100;
            napis[] napisy = new napis[4];
            printMenu(x, y, logo, ref napisy,points);
            ConsoleKey key;
            int id = 0;
            CancellationTokenSource cancel = new CancellationTokenSource();

            Thread mainmenu = new Thread(() => UpdateConsoleMenuSize(ref x, ref y, ref mutex, ref logo, ref napisy, ref id, cancel,points));//zmieniaanie asynchronicznie szerokosci okna

            mainmenu.Start();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "->");
                Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "<-");
                key = Console.ReadKey(true).Key;

            } while (key != ConsoleKey.Enter);
            
            cancel.Cancel();
            return 1;
        }
        
        private static void printMenu(int x, int y, List<string> logo, ref napis[] napisy,int points)
        {
            try
            {
                Console.SetWindowSize(x, y);

            }
            catch(Exception )
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
            string pointsString = "";
            string gameendString = "";
            for(int i = 0;i<logoWidth/2-5;++i)
            {
                pointsString += " ";
                gameendString +=" ";
            }
            gameendString += "gra skończona";
            pointsString += "punkty: " + points.ToString();
            logo[logo.Count-1] =pointsString;
            logo[logo.Count - 2] = gameendString;
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

            
            napisy[0] = new napis(ox - 6, oy + 3, "Wróć do menu");

            Cursor.WriteString(ox - 6, oy + 3, "Wróć do menu");
        }
        //private static void printGameEnd(int x, int y, List<string> logo, ref napis[] napisy)
        //{
        //    Console.SetWindowSize(x, y);
        //    Console.ForegroundColor = ConsoleColor.Magenta;

        //    int ox = x / 2;
        //    int oy = y / 2;
        //    int logoWidth = logo[0].Length;
        //    int logoHeight = logo.Count;
        //    try
        //    {
        //        for (int i = 0; i < logo.Count; i++)
        //        {

        //            Cursor.WriteString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
        //        }
        //    }
        //    catch (ArgumentOutOfRangeException ex)
        //    {

        //        throw ex;
        //    }

        //    ConsoleHelper.SetCurrentFont("Consolas", 24);
        //    napisy[0] = new napis(ox - 12, oy - 3, "Ukończyłeś/ukończyłaś grę");
        //    napisy[1] = new napis(ox - 3, oy - 1, "Wyjście");

        //    Cursor.WriteString(ox - 12, oy - 3, "Ukończyłeś/ukończyłaś grę");
        //    Cursor.WriteString(ox - 3, oy - 1, "Wyjście");

        //}
        //private static void printAbout(int x, int y, List<string> logo, ref napis[] napisy)
        //{
        //    Console.SetWindowSize(x, y);
        //    Console.ForegroundColor = ConsoleColor.Magenta;

        //    int ox = x / 2;
        //    int oy = y / 2;
        //    int logoWidth = logo[0].Length;
        //    int logoHeight = logo.Count;
        //    try
        //    {
        //        for (int i = 0; i < logo.Count; i++)
        //        {

        //            Cursor.WriteString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
        //        }
        //    }
        //    catch (ArgumentOutOfRangeException ex)
        //    {

        //        throw ex;
        //    }

        //    ConsoleHelper.SetCurrentFont("Consolas", 24);
        //    napisy[0] = new napis(ox - 8, oy - 3, "Szymon Woronowicz");
        //    napisy[1] = new napis(ox - 6, oy - 1, "Julia Gejdel");
        //    napisy[2] = new napis(ox - 7, oy + 1, "Paweł Krzywosz");
        //    napisy[3] = new napis(ox - 3, oy + 3, "Wyjscie");
        //    Cursor.WriteString(ox - 8, oy - 3, "Szymon Woronowicz");
        //    Cursor.WriteString(ox - 6, oy - 1, "Julia Gejdel");
        //    Cursor.WriteString(ox - 7, oy + 1, "Paweł Krzywosz");
        //    Cursor.WriteString(ox - 3, oy + 3, "Wyjscie");
        //}
        //private static void printDifficulties(int x, int y, List<string> logo, ref napis[] napisy)
        //{
        //    Console.SetWindowSize(x, y);
        //    Console.ForegroundColor = ConsoleColor.Magenta;

        //    int ox = x / 2;
        //    int oy = y / 2;
        //    int logoWidth = logo[0].Length;
        //    int logoHeight = logo.Count;
        //    try
        //    {
        //        for (int i = 0; i < logo.Count; i++)
        //        {

        //            Cursor.WriteString(ox - logoWidth / 2, oy - 10 + i, logo[i]);
        //        }
        //    }
        //    catch (ArgumentOutOfRangeException ex)
        //    {

        //        throw ex;
        //    }

        //    ConsoleHelper.SetCurrentFont("Consolas", 24);
        //    napisy[0] = new napis(ox - 3, oy - 3, "łatwy");
        //    napisy[1] = new napis(ox - 3, oy - 1, "średni");
        //    napisy[2] = new napis(ox - 3, oy + 1, "trudny");
        //    napisy[3] = new napis(ox - 3, oy + 3, "Wyjscie");
        //    Cursor.WriteString(ox - 3, oy - 3, "łatwy");
        //    Cursor.WriteString(ox - 3, oy - 1, "średni");
        //    Cursor.WriteString(ox - 3, oy + 1, "trudny");
        //    Cursor.WriteString(ox - 3, oy + 3, "Wyjscie");
        //}
        private static void UpdateConsoleMenuSize(ref int x, ref int y, ref object mutex, ref List<string> logo, ref napis[] napisy, ref int id, CancellationTokenSource cancel,int points)
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




                            printMenu(x, y, logo, ref napisy,points);
                           
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
