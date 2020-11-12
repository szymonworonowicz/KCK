using KCKProjektWPF.Pages;
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

namespace KCKProjektWPF
{
    /// <summary>
    /// Interaction logic for Startowa.xaml
    /// </summary>
    public partial class Startowa : Page
    {

        public Startowa()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Content = new OpcjePg();
        }

        private void start_game(object sender, RoutedEventArgs e)
        {
            MainWindow window =(MainWindow) Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            
            window.Content = new GameViewPg(window.postacUrl,window.poziom);
        }
    }
}
