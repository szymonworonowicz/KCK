using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCKProjectAPI;

namespace KCKProjectAPI
{
    
    public class FieldType
    { 
        private int x;
        private int y;
        private int id;
        private IField prototype;
        private string type;
        public FieldType(IField prototype)
        {
            this.prototype = prototype;
            type = prototype.ToString();
        }
        public IField getNewField()
        {
            return (IField)prototype.Clone();

        }
        public string ToString()
        {
            return type;
        }
    }
}
