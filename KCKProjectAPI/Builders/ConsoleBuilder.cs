using System;
using System.Collections.Generic;
using System.Text;

namespace KCKProjectAPI.Builders
{
    public class ConsoleBuilder : IBuilder
    {
        List<LinkedList<IField>> mymap { get; set; }


        public ConsoleBuilder() : base()
        {
            mymap = new List<LinkedList<IField>>();
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

        public override object getMap()
        {
            return mymap;
        }
    }
}
