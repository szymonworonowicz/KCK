using System;

namespace KCKProjectAPI
{
    public class Cursor
    {
        public static void CursorFun(int x, int y, char writeThis)
        {

            Console.SetCursorPosition(x, y);
            Console.Out.Write(writeThis);

        }
        public static void writeString (int x,int y,string writeThis)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Out.Write(writeThis);
            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw ex;
            }


        }
        public static void setCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}