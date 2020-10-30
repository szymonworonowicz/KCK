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
        public string postacurl { get; set; } = "pack://application:,,,/KCKProjektWPF;component/Image/mario.png";
        public Wybor_postaci()
        {
            InitializeComponent();
        }

        private void Image_Clicked(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            postacurl = img.Source.ToString();

            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

    }
}
