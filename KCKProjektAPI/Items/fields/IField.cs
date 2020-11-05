using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKProjectAPI
{
    public abstract class IField: ICloneable
    {
        public int x { get; set; }
        public int y { get; set; }
        string type { get; set; }

        public IField(int _x, int _y, string _type)
        {
            this.x = _x;
            this.y = _y;
            this.type = _type;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return type;
        }
    }
}
