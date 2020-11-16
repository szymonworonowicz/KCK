﻿using System;
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
        Canvas mymap { get; set; }

        public WPFBuilder() : base()
        {
            mymap = new Canvas();

        }
        public override void AddPath(int x, int y)
        {
            //Rectangle obj = new Rectangle();
            //obj.Fill = new SolidColorBrush(Colors.Black);
            //obj.Width = 10;
            //obj.Height = 20;
            //mymap.Children.Add(obj);
            //obj.SetValue(Canvas.TopProperty, y==0?1.0:y*20+1);
            //obj.SetValue(Canvas.LeftProperty, x == 0 ? 1.0 : x * 10 + 1);

        }

        public override void AddWall(int x, int y)
        {
            Rectangle rect = new Rectangle();
            //rect.Fill = new SolidColorBrush(Colors.Red);
            string directory = Directory.GetCurrentDirectory();
            rect.Width = 10;
            rect.Height = 20;
            rect.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($@"{directory}/Builders/WpfGraphics/cegla.png")),
                Stretch = Stretch.UniformToFill
            };
            mymap.Children.Add(rect);
            rect.SetValue(Canvas.TopProperty, y == 0 ? 1.0 : y * 20 + 1);
            rect.SetValue(Canvas.LeftProperty, x == 0 ? 1.0 : x * 10 + 1);


            //Grid.SetColumn(tb, x);
        }

        public override object getMap()
        {
            return mymap;
        }
    }
}
