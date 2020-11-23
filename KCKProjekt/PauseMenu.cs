using System;
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
    public static class PauseMenu
    {
        private delegate void menuAction<T>(int x, int y, List<string> logo, ref T map,int points);
        private static menuAction<napis[]> menuprinter;
        private delegate int optionAction(int id);
        private static optionAction optionprinter;
        private static int levelid = 1;
        static PauseMenu()
        {
            menuprinter = new menuAction<napis[]>(printMenu);
            //optionprinter = new optionAction(ChooseOptionMenu);
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
            
            logo.Add("");
            int y = 36, x = 100;
            napis[] napisy = new napis[4];
            menuprinter(x, y, logo, ref napisy,points);
            ConsoleKey key;
            int id = 0;
            CancellationTokenSource cancel = new CancellationTokenSource();

            Thread mainmenu = new Thread(() => UpdateConsoleMenuSize(menuprinter, ref x, ref y, ref mutex, ref logo, ref napisy, ref id, cancel,points));//zmieniaanie asynchronicznie szerokosci okna

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
                            id = (id + 1) == 2 ? 0 : id + 1;
                            break;
                        case ConsoleKey.UpArrow:
                            Cursor.writeString(napisy[id].x - 2, napisy[id].y, "  ");
                            Cursor.writeString(napisy[id].x + napisy[id].text.Length, napisy[id].y, "  ");
                            id = (id - 1) < 0 ? 1 : id - 1;
                            break;
                    }
                }



            } while (key != ConsoleKey.Enter);
            cancel.Cancel();

            return id;
            
            //return 1;
        }

   
    
      
     

       
        

        private static void printMenu(int x, int y, List<string> logo, ref napis[] napisy,int points)
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

                    Cursor.writeString(ox - logoWidth / 2, oy - 10 + i, logo[i]);

                }
            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw ex;
            }

            ConsoleHelper.SetCurrentFont("Consolas", 24);
            
            //napisy[0] = new napis(ox - 4, oy - 1, "punkty: "+ points.ToString() );
            
            napisy[0] = new napis(ox - 6, oy -1, "Wróć do gry");
            napisy[1] = new napis(ox - 6, oy + 1, "Wróć do menu");



            
            //Cursor.writeString(ox - 4, oy - 1, "punkty: " + points.ToString());
            Cursor.writeString(ox - 6, oy - 1, "Wróć do gry");
            Cursor.writeString(ox - 6, oy + 1, "Wróć do menu");
        }
       
      
        
        private static void UpdateConsoleMenuSize(menuAction<napis[]> menu, ref int x, ref int y, ref object mutex, ref List<string> logo, ref napis[] napisy, ref int id, CancellationTokenSource cancel,int points)
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

                            


                            menu(x, y, logo, ref napisy,points);
                           
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
