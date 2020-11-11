using KCKProjectAPI;
using KCKProjectAPI.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private Canvas canvas { get; set; }

        public GameViewPg()
        {
            InitializeComponent();
            Map m = new Map("map2", new WPFBuilder());
            canvas = m.getMap() as Canvas;
            double scale = (double)m.WidthMap / (double)m.HeightMap;
            for (int i = 0; i < 3; i++)
            {
                var row = new RowDefinition();
                if(i%2==0)
                {
                    row.Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    row.Height = new GridLength(m.HeightMap-2, GridUnitType.Star);
                }
                mygrid.RowDefinitions.Add(row);

            }
            for (int i = 0; i < 3; i++)
            {
                var column = new ColumnDefinition();
                if (i % 2 == 0)
                {
                    column.Width = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    column.Width = new GridLength(m.WidthMap - 2, GridUnitType.Star);
                }
                mygrid.ColumnDefinitions.Add(column);

            }

            canvas.SetValue(Grid.ColumnProperty, 1);
            canvas.SetValue(Grid.RowProperty, 1);
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            window.Height = (double)m.HeightMap * 23 ;
            window.Width = (double)m.WidthMap * 10.5 ;
            //this.Height = canvas.Height;
            //this.Width = canvas.Width;
            window.ResizeMode = ResizeMode.CanMinimize;
            mygrid.Children.Add(canvas);

        }
    }
}
