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
    }
}