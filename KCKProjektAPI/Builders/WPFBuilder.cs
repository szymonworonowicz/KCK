using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KCKProjectAPI.Builders
{
    public class WPFBuilder :IBuilder
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
                mymap.RowDefinitions.Add(new RowDefinition());
            }
            while (mymap.ColumnDefinitions.Count <= y)
            {
                mymap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            Rectangle rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Colors.White);
            mymap.Children.Add(rect);
            //Grid.SetRow(tb,y);
            //Grid.SetColumn(tb,x);
            rect.SetValue(Grid.RowProperty, y);
            rect.SetValue(Grid.ColumnProperty, x);

        }

        public override void AddWall(int x, int y)
        {
            while (mymap.RowDefinitions.Count <= y)
            {
                mymap.RowDefinitions.Add(new RowDefinition());
            }
            while (mymap.ColumnDefinitions.Count <= y)
            {
                mymap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            //TextBlock tb = new TextBlock();
            //tb.Text = "b";
            Rectangle rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Colors.Black);
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
