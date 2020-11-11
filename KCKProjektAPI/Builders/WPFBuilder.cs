using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KCKProjectAPI.Builders
{
    public class WPFBuilder : IBuilder
    {
        private class RectangleWallClone : ICloneable
        {
            private Rectangle rect;

            public RectangleWallClone(Rectangle rect)
            {
                this.rect = new Rectangle();

            }
            public object Clone()
            {
                this.rect.StrokeThickness = 1.0;
                this.rect.Fill = new SolidColorBrush(Colors.White);
                var inst = rect.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                return inst?.Invoke(rect, null);
            }
        }
        private class RectanglePathClone : ICloneable
        {
            private  Rectangle rect;
            public RectanglePathClone(Rectangle rect)
            {
                this.rect = rect;

            }
            public object Clone()
            {
                this.rect.StrokeThickness = 1.0;
                this.rect.Fill = new SolidColorBrush(Colors.Black);

                var inst = rect.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                return inst?.Invoke(rect, null);
            }
        }
        Grid mymap { get; set; }
        private readonly RectangleWallClone rectwall;
        private readonly RectanglePathClone rectpath;

        public WPFBuilder() : base()
        {
            mymap = new Grid();
            rectwall = new RectangleWallClone(new Rectangle());
            rectpath = new RectanglePathClone(new Rectangle());
        }
        public override void AddPath(int x, int y)
        {
            while (mymap.RowDefinitions.Count <= y)
            {
                mymap.RowDefinitions.Add(new RowDefinition());
            }
            while (mymap.ColumnDefinitions.Count <= x)
            {
                mymap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            Rectangle obj = new Rectangle();
            obj.Fill = new SolidColorBrush(Colors.Black);
            obj.StrokeThickness = 2.0;
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
                mymap.RowDefinitions.Add(new RowDefinition());
            }
            while (mymap.ColumnDefinitions.Count <= x)
            {
                mymap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            Rectangle rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Colors.White);
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
