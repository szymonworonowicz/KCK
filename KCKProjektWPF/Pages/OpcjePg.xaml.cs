using KCKProjektWPF.Windows;
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
using KCKProjektAPI;

namespace KCKProjektWPF
{
    /// <summary>
    /// Interaction logic for OpcjePg.xaml
    /// </summary>
    public partial class OpcjePg : Page
    {
        public string _HeroUrl = "";
        public LevelEnum Level = LevelEnum.Easy;
        public OpcjePg()
        {
            InitializeComponent();

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            window.Content = new Startowa();
        }

        private void Wybor_Postaci_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window =(MainWindow) Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            Wybor_postaci win = new Wybor_postaci()
            {
                Owner = window,
            };
            win.ShowDialog();
            window._HeroUrl = win.postacUrl;
        }

        private void Poziom_Trudnosci_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window =(MainWindow) Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            Wybor_poziomu win = new Wybor_poziomu()
            {
                Owner = window,
            };
            win.ShowDialog();
            window.Level = win.Level;
        }
    }
}
