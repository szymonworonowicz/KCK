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
    /// Interaction logic for EscapePg.xaml
    /// </summary>
    public partial class EscapePg : Page
    {
        MainWindow window;
        public EscapePg()
        {
            InitializeComponent();
            window = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.KeyDown += KeyDown;
            window.ResizeMode = ResizeMode.CanResize;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                window.KeyDown -= KeyDown;
                window.Content = new Startowa();
            }
        }
    }
}
