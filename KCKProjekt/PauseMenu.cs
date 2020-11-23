using KCKProjecConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace KCKProjektConsole
{
    public static class PauseMenu
    {
        static PauseMenu()
        {

        }

        
        public static PauseEnum getMenu(int points)
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
            
            logo.Add("");
            int y = 36, x = 100;
            Napis[] napisy = new Napis[4];
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

                

                lock (mutex)
                {
                    switch (key)
                    {

                        case ConsoleKey.DownArrow:
                            Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id + 1) == 2 ? 0 : id + 1;
                            break;
                        case ConsoleKey.UpArrow:
                            Cursor.WriteString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.WriteString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id - 1) < 0 ? 1 : id - 1;
                            break;
                    }
                }



            } while (key != ConsoleKey.Enter);
            cancel.Cancel();

            return id == 0 ? PauseEnum.Resume:PauseEnum.Exit;
            
        }


        private static void printMenu(int x, int y, List<string> logo, ref Napis[] napisy,int points)
        {
            try
            {
            Console.SetWindowSize(x, y);

            }
            catch(Exception)
            {
                Console.SetWindowSize(150, 100);
                x = 100;
                y = 50;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            int ox = x / 2;
            int oy = y / 2;
            int logoWidth = logo[0].Length;
            int logoHeight = logo.Count;
            string pointsString = "";
            for (int i = 0; i < logoWidth / 2 - 5; ++i)
            {
                pointsString += " ";
            }
            pointsString += "punkty: " + points.ToString();
            logo[logo.Count - 1] = pointsString;
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
            
            
            napisy[0] = new Napis(ox - 6, oy -1, "Wróć do gry");
            napisy[1] = new Napis(ox - 6, oy + 1, "Wróć do menu");

            Cursor.WriteString(ox - 6, oy - 1, "Wróć do gry");
            Cursor.WriteString(ox - 6, oy + 1, "Wróć do menu");
        }
       
      
        
        private static void UpdateConsoleMenuSize( ref int x, ref int y, ref object mutex, ref List<string> logo, ref Napis[] napisy, ref int id, CancellationTokenSource cancel,int points)
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
