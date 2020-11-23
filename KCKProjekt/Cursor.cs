using System;

namespace KCKProjecConsole
{
    public class Cursor
    {
        public static void CursorFun(int x, int y, char writeThis)
        {

            Console.SetCursorPosition(x, y);
            Console.Out.Write(writeThis);

        }
        public static void WriteString (int x,int y,string writeThis)
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
    }
}