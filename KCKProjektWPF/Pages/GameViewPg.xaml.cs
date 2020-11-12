﻿using KCKProjectAPI;
using KCKProjectAPI.Builders;
using KCKProjektWPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;
using Key = KCKProjectAPI.Key;

namespace KCKProjektWPF.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy GameViewPg.xaml
    /// </summary>
    public partial class GameViewPg : Page
    {

        private Canvas canvas { get; set; }
        private string postacUrl;
        private int poziom;
        private Rectangle player;
        private ImageBrush transformImageBrush;
        private bool left = false;
        private List<Key> keys;
        private List<Door> doors;
        private List<Coin> coins;
        private List<Key> ownedKeys;

        public GameViewPg(string postacUrl, int poziom)
        {
            this.postacUrl = postacUrl;
            this.poziom = poziom;
            transformImageBrush = new ImageBrush();
            InitializeComponent();
            Map m = new Map("map2", new WPFBuilder());
            canvas = m.getMap() as Canvas;
            MainWindow window = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.KeyDown += CanvasKeyPreview;

            keys = new List<Key>();
            doors = new List<Door>();
            coins = new List<Coin>();

            m.GetElems(ref keys, ref doors, ref coins);

            SetAnimationElement(coins, @"pack://application:,,,/KCKProjektWPF;component/Image/coin.gif");
            SetAnimationElement(keys, @"pack://application:,,,/KCKProjektWPF;component/Image/key3.gif");
            SetDors();

            ImageSource imageSource = new BitmapImage(new Uri(postacUrl));
            transformImageBrush.ImageSource = imageSource;
            player = new Rectangle
            {
                Fill = new ImageBrush()
                {
                    ImageSource = imageSource,
                    Stretch = Stretch.UniformToFill
                },
                Width = 10,
                Height = 20
            };

            canvas.Children.Add(player);
            player.SetValue(Canvas.TopProperty, 201.0);
            player.SetValue(Canvas.LeftProperty, 21.0);

            double scale = (double)m.WidthMap / (double)m.HeightMap;
            for (int i = 0; i < 3; i++)
            {
                var row = new RowDefinition();
                if (i % 2 == 0)
                {
                    row.Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    row.Height = new GridLength(m.HeightMap - 2, GridUnitType.Star);
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

            window.Height = (double)m.HeightMap * 23;
            window.Width = (double)m.WidthMap * 10.5;
            window.ResizeMode = ResizeMode.CanMinimize;
            mygrid.Children.Add(canvas);

        }

        private void SetDors()
        {
            foreach (Door door in doors)
            {
                Image img = new Image()
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/KCKProjektWPF;component/Image/drzwi.png")),
                    Width = 10,
                    Height = 20
                };
                canvas.Children.Add(img);
                img.SetValue(Canvas.TopProperty, door.y * 20.0 + 1);
                img.SetValue(Canvas.LeftProperty, door.x * 10.0 + 1);
            }
        }

        private void SetAnimationElement<T>(List<T> listofElems, string pathToElem) where T : IPickup
        {
            foreach (T elem in listofElems)
            {
                Image img = new Image()
                {
                    Height = 30,
                    Width = 20
                };
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(pathToElem);
                image.EndInit();
                ImageBehavior.SetAnimatedSource(img, image);
                canvas.Children.Add(img);
                img.SetValue(Canvas.TopProperty, elem.y * 20.0 + 1);
                img.SetValue(Canvas.LeftProperty, elem.x * 10.0 + 1);
            }
        }

        private void CanvasKeyPreview(object sender, KeyEventArgs e)
        {
            double Y = (double)player.GetValue(Canvas.TopProperty);
            double X = (double)player.GetValue(Canvas.LeftProperty);

            switch (e.Key)
            {
                case System.Windows.Input.Key.Left:
                    TransformPlayerLeft(Y, X);

                    break;
                case System.Windows.Input.Key.Right:
                    TransformPlayerRight(X, Y);

                    break;
                case System.Windows.Input.Key.Down:
                    TransformPlayerDown(Y, X);
                    break;
                case System.Windows.Input.Key.Up:
                    TransformPlayerUp(Y, X);
                    break;
                case System.Windows.Input.Key.Q:
                    MainWindow window= e.OriginalSource as MainWindow;
                    window.Content = new Startowa();
                    window.KeyDown -= CanvasKeyPreview;
                    break;
                case System.Windows.Input.Key.P:
                    PauseWindow pause = new PauseWindow();
                    pause.ShowDialog();
                    break;
            }
            Thread.Sleep(50);

        }

        private void TransformPlayerUp(double Y, double X)
        {
            foreach (UIElement element in canvas.Children)
            {
                if (element is Rectangle rectangle)
                {
                    double y = (double)rectangle.GetValue(Canvas.TopProperty);
                    double x = (double)rectangle.GetValue(Canvas.LeftProperty);
                    if (y == Y - 20 && x == X)
                    {
                        try
                        {
                            if (((SolidColorBrush)rectangle.Fill).Color == Colors.Black)
                            {
                                player.SetValue(Canvas.TopProperty, Y - 20);
                            }
                        }
                        catch (InvalidCastException)
                        {
                            return;
                        }
                    }
                }

            }
        }

        private void TransformPlayerDown(double Y, double X)
        {
            foreach (UIElement element in canvas.Children)
            {
                if (element is Rectangle rectangle)
                {
                    double y = (double)rectangle.GetValue(Canvas.TopProperty);
                    double x = (double)rectangle.GetValue(Canvas.LeftProperty);
                    if (y == Y + 20 && x == X)
                    {
                        try
                        {
                            if (((SolidColorBrush)rectangle.Fill).Color == Colors.Black)
                            {
                                player.SetValue(Canvas.TopProperty, Y + 20);
                            }
                        }
                        catch (InvalidCastException)
                        {
                            return;
                        }
                    }
                }

            }
        }

        private void TransformPlayerLeft(double Y, double X)
        {
            foreach (UIElement element in canvas.Children)
            {
                if (element is Rectangle rectangle)
                {
                    double y = (double)rectangle.GetValue(Canvas.TopProperty);
                    double x = (double)rectangle.GetValue(Canvas.LeftProperty);
                    if (y == Y && x == X - 10)
                    {
                        try
                        {
                            if (((SolidColorBrush)rectangle.Fill).Color == Colors.Black)
                            {
                                if (!left)
                                {
                                    ScaleTransform scale = new ScaleTransform();
                                    scale.CenterX = 5;
                                    scale.CenterY = 10;
                                    scale.ScaleX = -1;

                                    transformImageBrush.Transform = scale;

                                    player.Fill = transformImageBrush;
                                    left = true;
                                }

                                player.SetValue(Canvas.LeftProperty, X - 10);
                            }
                        }
                        catch (InvalidCastException)
                        {
                            return;
                        }
                    }
                }

            }
        }

        private void TransformPlayerRight(double X, double Y)
        {
            foreach (UIElement element in canvas.Children)
            {
                if (element is Rectangle rectangle)
                {
                    double y = (double)rectangle.GetValue(Canvas.TopProperty);
                    double x = (double)rectangle.GetValue(Canvas.LeftProperty);
                    if (y == Y && x == X + 10)
                    {
                        try
                        {
                            if (((SolidColorBrush)rectangle.Fill).Color == Colors.Black)
                            {
                                if (left)
                                {
                                    ScaleTransform scale = new ScaleTransform();
                                    scale.CenterX = 5;
                                    scale.CenterY = 10;
                                    scale.ScaleX = 1;

                                    transformImageBrush.Transform = scale;

                                    player.Fill = transformImageBrush;
                                    left = false;
                                }

                                player.SetValue(Canvas.LeftProperty, X + 10);
                            }
                        }
                        catch (InvalidCastException)
                        {
                            return;
                        }
                    }
                }

            }
        }
    }
}
