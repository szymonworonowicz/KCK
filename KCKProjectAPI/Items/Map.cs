using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KCKProjectAPI
{
    public class Map
    {
        public List<LinkedList<IField>> map { get; private set; }
        private int WidthMap { get; set; }
        private int HeightMap { get; set; }
        private int dGenerator = 1;



        public Map(string path)
        {
            map = new List<LinkedList<IField>>();
            int y = 0;
            using(StreamReader str = new StreamReader(path))
            {
                string line = "";
                while((line=str.ReadLine())!=null)
                {
                    LinkedList<IField> temp = new LinkedList<IField>();

                    for(int i=0;i<line.Length;i++)
                    {
                        switch (line[i]) 
                        {
                            case '#':
                                temp.AddLast(new Wall(i, y));
                                break;
                            case ' ':
                                temp.AddLast(new Path(i, y));
                                break;
                        }
                    }
                    map.Add(temp);
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
            string fragmentString = String.Join(", ", fragment);
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
