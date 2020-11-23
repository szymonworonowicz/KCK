using KCKProjectAPI;
using KCKProjectAPI.Builders;
using KCKProjectWPF;
using KCKProjektAPI.Items.fields;
using KCKProjektWPF.Controls;
using KCKProjektWPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KCKProjektAPI;
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
        private int Coins { get; set; } 
        private int Keys { get; set; } 
        private Rectangle player;
        private ImageBrush transformImageBrush;
        private bool _left = false;
        private List<Key> keys;
        private List<Door> doors;
        private List<Coin> coins;
        private List<Key> ownedKeys;
        private Exit exit;
        private const string Coinpath = @"pack://application:,,,/KCKProjektWPF;component/Image/coin.gif";
        private const string Keypath = @"pack://application:,,,/KCKProjektWPF;component/Image/key3.gif";

        public GameViewPg(string heroUrl, LevelEnum level)
        {
            transformImageBrush = new ImageBrush();
            InitializeComponent();
            Map m = new Map(level,new WPFBuilder());
            canvas = m.getMap() as Canvas;
            MainWindow window = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.KeyDown += CanvasKeyPreview;

            keys = new List<Key>();
            doors = new List<Door>();
            coins = new List<Coin>();
            ownedKeys = new List<Key>();

            m.GetElems(ref keys, ref doors, ref coins, ref exit);

            SetCoin(Coinpath);
            SetKeys(Keypath);
            SetDors();

            EscapeDoor escape = new EscapeDoor();
            canvas.Children.Add(escape);
            escape.SetValue(Canvas.LeftProperty, exit.x * 10.0 + 1.0);
            escape.SetValue(Canvas.TopProperty, 20.0 * exit.y + 1.0);


            InitializePlayer(heroUrl);

            InitializeMainGrid(m);

            canvas.SetValue(Grid.ColumnProperty, 1);
            canvas.SetValue(Grid.RowProperty, 1);

            window.Height = m.HeightMap * 25.0;
            window.Width = m.WidthMap * 10.5;
            window.ResizeMode = ResizeMode.CanMinimize;
            mygrid.Children.Add(canvas);

        }

        private void InitializeMainGrid(Map m)
        {
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
        }

        private void InitializePlayer(string _heroUrl)
        {
            ImageSource imageSource = new BitmapImage(new Uri(_heroUrl));
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
        }

        private void SetDors()
        {
            foreach (Door door in doors)
            {
                DoorControl control = new DoorControl();
                canvas.Children.Add(control);
                control.SetValue(Canvas.TopProperty, door.y * 20.0 + 1);
                control.SetValue(Canvas.LeftProperty, door.x * 10.0 + 1);
            }
        }

        private void SetKeys(string path)
        {
            foreach (Key elem in keys)
            {
                KeyControl key = new KeyControl(path);
                canvas.Children.Add(key);
                key.SetValue(Canvas.TopProperty, elem.y * 20.0 + 1);
                key.SetValue(Canvas.LeftProperty, elem.x * 10.0 + 1);
            }
        }

        private void SetCoin(string path)
        {
            foreach (Coin elem in coins)
            {
                CoinControl key = new CoinControl(path);
                canvas.Children.Add(key);
                key.SetValue(Canvas.TopProperty, elem.y * 20.0 + 1);
                key.SetValue(Canvas.LeftProperty, elem.x * 10.0 + 1);
            }
        }

        private void CanvasKeyPreview(object sender, KeyEventArgs e)
        {
            double beforeY = (double)player.GetValue(Canvas.TopProperty);//pozycja przed ruchem
            double beforeX = (double)player.GetValue(Canvas.LeftProperty);

            PlayerPositionTransform(e, beforeY, beforeX);

            double afterY = (double)player.GetValue(Canvas.TopProperty);
            double afterX = (double)player.GetValue(Canvas.LeftProperty);


            UserControl control = (UserControl)canvas.Children.OfType<UserControl>().FirstOrDefault(x =>
            {
                double controlx = (double)x.GetValue(Canvas.LeftProperty);
                double controly = (double)x.GetValue(Canvas.TopProperty);
                if (afterY == controly && afterX == controlx)
                    return true;
                return false;
            });


            CheckEnterField(control, afterY, afterX, beforeY, beforeX);
            Thread.Sleep(50);
        }

        private void CheckEnterField(UserControl control, double afterY, double afterX, double beforeY, double beforeX)
        {
            if (control is CoinControl coin)
            {
                Coins++;
                this.coinCount.Content = Coins.ToString();
                canvas.Children.Remove(coin);
            }
            else if (control is KeyControl key)
            {
                int keyy = (int) (afterY - 1.0) / 20;
                int keyx = (int) (afterX - 1.0) / 10;

                Key collectkey = PickUps.PickupKey(keyx, keyy, keys) as Key;

                ownedKeys.Add(collectkey);
                Keys++;
                this.keyCount.Content = Keys.ToString();
                canvas.Children.Remove(key);
            }
            else if (control is DoorControl door)
            {
                int doory = (int) (afterY - 1.0) / 20;
                int doorx = (int) (afterX - 1.0) / 10;
                if (!PickUps.unlockTheDoor(doorx, doory, doors, ownedKeys))
                {
                    Keys--;
                    keyCount.Content = Keys.ToString();
                    canvas.Children.Remove(door);
                }
                else
                {
                    player.SetValue(Canvas.TopProperty, beforeY);
                    player.SetValue(Canvas.LeftProperty, beforeX);
                }
            }
            else if (control is EscapeDoor escape)
            {
                if (Coins == coins.Count)
                {
                    MainWindow window =
                        (MainWindow) Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    window.KeyDown -= CanvasKeyPreview;
                    window.Content = new EscapePg();
                }
                else
                {
                    player.SetValue(Canvas.TopProperty, beforeY);
                    player.SetValue(Canvas.LeftProperty, beforeX);
                }
            }
        }

        private void PlayerPositionTransform(KeyEventArgs e, double beforeY, double beforeX)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Left:
                    TransformPlayerLeft(beforeY, beforeX);

                    break;
                case System.Windows.Input.Key.Right:
                    TransformPlayerRight(beforeX, beforeY);

                    break;
                case System.Windows.Input.Key.Down:
                    TransformPlayerDown(beforeY, beforeX);
                    break;
                case System.Windows.Input.Key.Up:
                    TransformPlayerUp(beforeY, beforeX);
                    break;
                case System.Windows.Input.Key.Q:
                    MainWindow window = e.OriginalSource as MainWindow;
                    window.Content = new Startowa();
                    window.KeyDown -= CanvasKeyPreview;
                    window.ResizeMode = ResizeMode.CanResize;
                    break;
                case System.Windows.Input.Key.P:
                    PauseWindow pause = new PauseWindow();
                    pause.ShowDialog();
                    break;
            }
        }


        private void TransformPlayerUp(double Y, double X)
        {
            Rectangle control = (Rectangle)canvas.Children.OfType<Rectangle>().FirstOrDefault(x =>
            {
                double controlx = (double)x.GetValue(Canvas.LeftProperty);
                double controly = (double)x.GetValue(Canvas.TopProperty);
                if (Y - 20 == controly && X == controlx)
                    return true;
                return false;
            });
            if (control == null)
            {
                player.SetValue(Canvas.TopProperty, Y - 20);
            }

        }

        private void TransformPlayerDown(double Y, double X)
        {
            Rectangle control = (Rectangle)canvas.Children.OfType<Rectangle>().FirstOrDefault(x =>
            {
                double controlx = (double)x.GetValue(Canvas.LeftProperty);
                double controly = (double)x.GetValue(Canvas.TopProperty);
                if (Y + 20 == controly && X == controlx)
                    return true;
                return false;
            });
            if (control == null)
            {
                player.SetValue(Canvas.TopProperty, Y + 20);
            }

        }

        private void TransformPlayerLeft(double Y, double X)
        {
            Rectangle control = (Rectangle)canvas.Children.OfType<Rectangle>().FirstOrDefault(x =>
            {
                double controlx = (double)x.GetValue(Canvas.LeftProperty);
                double controly = (double)x.GetValue(Canvas.TopProperty);
                if (Y == controly && X - 10 == controlx)
                    return true;
                return false;
            });
            if (control == null)
            {
                if (!_left)
                {
                    ScaleTransform scale = new ScaleTransform();
                    scale.CenterX = 5;
                    scale.CenterY = 10;
                    scale.ScaleX = -1;

                    transformImageBrush.Transform = scale;

                    player.Fill = transformImageBrush;
                    _left = true;
                }
                player.SetValue(Canvas.LeftProperty, X - 10);
            }

        }

        private void TransformPlayerRight(double X, double Y)
        {
            Rectangle control = (Rectangle)canvas.Children.OfType<Rectangle>().FirstOrDefault(x =>
            {
                double controlx = (double)x.GetValue(Canvas.LeftProperty);
                double controly = (double)x.GetValue(Canvas.TopProperty);
                if (Y == controly && X + 10 == controlx)
                    return true;
                return false;
            });
            if (control == null)
            {
                player.SetValue(Canvas.LeftProperty, X + 10);
                if (_left)
                {
                    ScaleTransform scale = new ScaleTransform();
                    scale.CenterX = 5;
                    scale.CenterY = 10;
                    scale.ScaleX = 1;

                    transformImageBrush.Transform = scale;

                    player.Fill = transformImageBrush;
                    _left = false;
                }
            }

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Image coinImage = this.CoinResult;

            var coinSource = new BitmapImage();
            coinSource.BeginInit();
            coinSource.UriSource = new Uri(Coinpath);
            coinSource.EndInit();
            ImageBehavior.SetAnimatedSource(coinImage, coinSource);

            Image keyImage = this.KeyResult;

            var keySouce = new BitmapImage();
            keySouce.BeginInit();
            keySouce.UriSource = new Uri(Keypath);
            keySouce.EndInit();
            ImageBehavior.SetAnimatedSource(keyImage, keySouce);
        }
    }
}
