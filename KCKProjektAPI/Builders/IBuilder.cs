using System;
using System.Collections.Generic;
using System.Text;

namespace KCKProjectAPI.Builders
{
    public abstract class IBuilder
    {
        public abstract void AddWall(int x, int y);
        public abstract void AddPath(int x, int y);
        public abstract object getMap();
    }
}
