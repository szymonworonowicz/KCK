using System;
using System.Collections.Generic;
using System.Text;

namespace KCKProjectAPI.Builders
{
    public class ConsoleBuilder : IBuilder
    {



        public ConsoleBuilder() : base()
        {

        }
        public override void AddPath(int x, int y)
        {
            while (mymap.Count <= y)
            {
                mymap.Add(new LinkedList<IField>());
            }
            mymap[y].AddLast(new Path(x, y));
        }

        public override void AddWall(int x, int y)
        {
            while (mymap.Count <= y)
            {
                mymap.Add(new LinkedList<IField>());
            }
            mymap[y].AddLast(new Wall(x, y));
        }
    }
}
