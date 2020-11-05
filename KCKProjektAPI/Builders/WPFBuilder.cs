using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

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
            TextBlock tb = new TextBlock();
            tb.Text = "a";
            mymap.Children.Add(tb);
            //Grid.SetRow(tb,y);
            //Grid.SetColumn(tb,x);
            tb.SetValue(Grid.RowProperty, y);
            tb.SetValue(Grid.ColumnProperty, x);

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
            TextBlock tb = new TextBlock();
            tb.Text = "b";
            mymap.Children.Add(tb);
            tb.SetValue(Grid.RowProperty, y);
            tb.SetValue(Grid.ColumnProperty, x);
           
            //Grid.SetColumn(tb, x);
        }

        public override object getMap()
        {
            return mymap;
        }
    }
}
