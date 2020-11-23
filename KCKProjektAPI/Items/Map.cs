using KCKProjectAPI.Builders;
using KCKProjektAPI;
using KCKProjektAPI.Items.fields;
using System;
using System.Collections.Generic;
using System.IO;

namespace KCKProjectAPI
{
    public class Map
    {
        public List<LinkedList<IField>> map { get; private set; }
        public int WidthMap { get; private set; }
        public int HeightMap { get; private set; }
        private const string Path = "map2";
        private readonly int MapNr;
        public IBuilder builder;
        
        public Map(LevelEnum level,IBuilder builder)
        {
            this.MapNr = (int)level+1;
            this.builder = builder;
        }
        public object getMap()
        {
            int y = 0;
            using (StreamReader str = new StreamReader($"./maps/{Path}.txt"))
            {
                string line = "";
                while ((line = str.ReadLine()) != null)
                {
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
                str.Close();
            }
            return builder.getMap();
        }
        public void GetElems(ref List<Key> keys, ref List<Door> doors, ref List<Coin>coins,ref Exit exit)
        {
            using (StreamReader str = new StreamReader($"./maps/{Path}elems"+MapNr.ToString()+".txt"))
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
                        case "Exit":
                            exit = new Exit(x, y);
                            break;

                    }
                }
            }
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
