using System;
using System.Collections.Generic;
using System.Text;
using KCKProjectAPI.directory;
using KCKProjectAPI;
namespace KCKProjektConsole
{
    static class Menu
    {
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
    }
}
