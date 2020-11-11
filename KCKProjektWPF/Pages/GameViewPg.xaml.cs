using KCKProjectAPI;
using KCKProjectAPI.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KCKProjektWPF.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy GameViewPg.xaml
    /// </summary>
    public partial class GameViewPg : Page
    {

        private Grid grid { get; set; }
        public GameViewPg()
        {
            //Map m = new Map("map2", new ConsoleBuilder());
            //Map m = new Map("map2", new WPFBuilder());
            Map m = new Map("map2", new WPFBuilder());
            grid = m.getMap() as Grid;
            InitializeComponent();
            mygrid.Children.Add(grid);
            
        }
    }
}
