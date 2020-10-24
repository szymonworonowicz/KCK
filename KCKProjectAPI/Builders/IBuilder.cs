using System;
using System.Collections.Generic;
using System.Text;

namespace KCKProjectAPI.Builders
{
    abstract class IBuilder
    {
        protected List<LinkedList<IField>> mymap { get; private set; }
        public abstract void AddWall(int x, int y);
        public abstract void AddPath(int x, int y);
        public List<LinkedList<IField>> getMap()
        {
            return mymap;
        }
        public IBuilder()
        {
            this.mymap = new List<LinkedList<IField>>();
           /* for(int i = 0;i<y;i++)
            {
                LinkedList<IField> temp = new LinkedList<IField>();
                mymap.Add(temp);
            }*/
        }
    }
}
