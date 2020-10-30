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
using System.Windows.Shapes;

namespace KCKProjektWPF
{
    /// <summary>
    /// Interaction logic for Wybor_postaci.xaml
    /// </summary>
    public partial class Wybor_postaci : Window
    {
        string postacurl = "./../Image/mario";
        public Wybor_postaci()
        {
            InitializeComponent();
        }

        private void Image_Clicked(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            postacurl = img.Source.ToString();

            Exit();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            window.Content = new OpcjePg(postacurl);
        }
    }
}
