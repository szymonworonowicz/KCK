using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using KCKProjectAPI.Builders;

namespace KCKProjectAPI
{
    public class Map
    {
        public List<LinkedList<IField>> map { get; private set; }
        public int WidthMap { get; private set; }
        public int HeightMap { get; private set; }
        private int dGenerator = 1;
        private string path = "";
        public IBuilder builder;

        public Map()
        {

        }

        public Map(string path,IBuilder builder)
        {
            this.path = path;
            this.builder = builder;
            //this.path = path;
            //int y = 0;
            //using(StreamReader str = new StreamReader($"./maps/{path}.txt"))
            //{
            //    string line = "";
            //    while((line=str.ReadLine())!=null)
            //    {
            //        //LinkedList<IField> temp = new LinkedList<IField>();
                    
            //        for(int i=0;i<line.Length;i++)
            //        {
            //            switch (line[i]) 
            //            {
            //                case '#':
                                
            //                    builder.AddWall(i, y);
            //                    break;
            //                case ' ':
            //                    builder.AddPath(i, y);
            //                    break;
            //            }
            //        }
            //        y++;
            //        //map.Add(temp);
            //    }
            //}
            //map = builder.getMap();
            
        }
        public object getMap()
        {
            int y = 0;
            using (StreamReader str = new StreamReader($"./maps/{path}.txt"))
            {
                string line = "";
                while ((line = str.ReadLine()) != null)
                {
                    //LinkedList<IField> temp = new LinkedList<IField>();
                    HeightMap++;
                    int i = 0;
                    for (i = 0; i < line.Length; i++)
                    {
                        switch (line[i])
                        {
                            case '#':

                                builder.AddWall(i, y);
                                break;
                            case ' ':
                                builder.AddPath(i, y);
                                break;
                        }
                        
                    }
                    WidthMap = i;
                    y++;
                    //map.Add(temp);
                }
            }
            return builder.getMap();
        }
        public void GetElems(ref List<Key> keys, ref List<Door> doors, ref List<Coin> coins)
        {
            using (StreamReader str = new StreamReader($"./maps/{path}elems.txt"))
            {
                string line="";

                while ((line = str.ReadLine()) != null)
                {
                    var elems = line.Split(' ');

                    int.TryParse(elems[1], out int id);
                    int.TryParse(elems[2], out int x);
                    int.TryParse(elems[3], out int y);
                    switch (elems[0])
                    {
                        case "Key":
                            keys.Add(new Key(id,x,y));
                            break;
                        case "Door":
                            doors.Add(new Door(id,x,y));
                            break;
                        case "Coin":
                            coins.Add(new Coin(id,x,y));
                            break;
                    }
                }
            }
        }

        private void AddField(IField f)
        {
            foreach(LinkedList<IField> list in map)
            {
                if(list.Last.Value.x<WidthMap-1)
                {
                    list.AddLast(f);
                    return;
                }
            }
            
        }
        public IField GetField(int x, int y)
        {
            IField f_to_return = null;
            foreach (LinkedList<IField> list in map)
            {
                if (list.First.Value.y == y)
                {
                    foreach(IField f in list)
                    {
                        if(f.x==x)
                        {
                            f_to_return = f;
                        }
                        else if(f.x>x)
                        {
                           
                        }
                    }
                }
               
            }
            return f_to_return;
        }

        

        /*public LinkedList<LinkedList<IField>> GetFrame()
        {
            
            
        }*/
        public LinkedList<IField> GetFragmentLine(int x, int y, int len)
        {

            LinkedList<IField> fragment;
            fragment = new LinkedList<IField>();
            if(x > WidthMap || y> HeightMap || x+len>WidthMap || x<WidthMap || y < HeightMap)
            {
                //throw new Exception();
            }
            foreach(LinkedList<IField>fragList in map)
            {
                
                /*Console.Out.WriteLine(fragList.First.Value.y);
                Console.In.ReadLine();*/
                if(fragList.First.Value.y==y)
                {
                    foreach(IField field in fragList)
                    {
                       /* Console.Out.WriteLine(field.x);
                        Console.In.ReadLine();*/
                        if (field.x<x+len && field.x>=x)
                        {

                            fragment.AddLast(field);
                            
                            if(field.x==x+len)
                            {
                                break;
                            }
                        }
                        
                    }
                    break;
                }
            }
            return fragment;
        }
       
        public string FragmentToString(int x, int y, int len)
        {
            LinkedList<IField> fragment = GetFragmentLine(x, y, len);

            string fragmentString = "";
            if (fragment!=null)
            {
                fragmentString = String.Join("", fragment);

                
            }
            int minus = x;
            while(minus++ <0)
            {
                fragmentString = " " + fragmentString;
            }
            
            
            while(fragmentString.Length<len)
            {
                fragmentString += " ";
            }
            return fragmentString;
        }
        public override string ToString()
        {
            string list = "";

            foreach(LinkedList<IField> flist in map)
            {
                list += "\n" +String.Join(", ",flist);
            }
            return list;
        }
    }
}
