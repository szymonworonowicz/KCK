using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KCKProjectAPI.Builders
{
    public class WPFBuilder : IBuilder
    {


        Grid mymap { get; set; }


        public WPFBuilder() : base()
        {
            mymap = new Grid();

        }
        public override void AddPath(int x, int y)
        {
            while (mymap.RowDefinitions.Count <= y)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                mymap.RowDefinitions.Add(row);
            }
            while (mymap.ColumnDefinitions.Count <= x)
            {
               var col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                mymap.ColumnDefinitions.Add(col);
            }
            Rectangle obj = new Rectangle();
            obj.Fill = new SolidColorBrush(Colors.Black);
            obj.StrokeThickness = 1;
            obj.Stroke = new SolidColorBrush(Colors.Black);
            //Grid.SetRow(tb,y);
            //Grid.SetColumn(tb,x);
            mymap.Children.Add(obj);
            obj.SetValue(Grid.RowProperty, y);
            obj.SetValue(Grid.ColumnProperty, x);

        }

        public override void AddWall(int x, int y)
        {
            while (mymap.RowDefinitions.Count <= y)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                mymap.RowDefinitions.Add(row);
            }
            while (mymap.ColumnDefinitions.Count <= x)
            {
                var col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                mymap.ColumnDefinitions.Add(col);
            }
            Rectangle rect = new Rectangle();
            //rect.Fill = new SolidColorBrush(Colors.Red);
            string directory = Directory.GetCurrentDirectory();
            rect.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($@"{directory}/Builders/WpfGraphics/cegla.png")),
                Stretch = Stretch.UniformToFill
            };
            mymap.Children.Add(rect);
            rect.SetValue(Grid.RowProperty, y);
            rect.SetValue(Grid.ColumnProperty, x);


            //Grid.SetColumn(tb, x);
        }

        public override object getMap()
        {
            return mymap;
        }
    }
}
