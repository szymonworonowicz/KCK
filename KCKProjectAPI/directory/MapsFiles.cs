using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KCKProjectAPI.directory
{
    public class MapsFiles
    {
        string path;
        private IEnumerable<string> filesMaps { get; set; }
        private IEnumerable<string> filesElems { get; set; }
        public MapsFiles()
        {
           
            //filesMaps = Directory.GetFiles("./maps/");
           
           // List<string> filesMaps = filesMaps.ToArray();
        }
        public void initFolders()
        {
            filesMaps = Directory.GetFiles("./maps/", "*.*", SearchOption.AllDirectories).Where(s => !s.EndsWith("elems.txt"));
            //filesElems = Directory.GetFiles("./maps/", "*.*", SearchOption.AllDirectories).Where(s => !s.EndsWith("elems.txt"));
        }
        
        public IEnumerable<string> getMaps()
        {
            return filesMaps;
        }
        public string getMapByID(int id)
        {
            string  noext= filesMaps.ElementAtOrDefault(id);
            noext =System.IO.Path.GetFileNameWithoutExtension(noext);
            return noext;
        }

    }
}
