using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KCKProjektWPF.Windows
{
    /// <summary>
    /// Interaction logic for Wybor_poziomu.xaml
    /// </summary>
    public partial class Wybor_poziomu : Window
    {
        public int poziom { get; set; } = 1;
        public Wybor_poziomu()
        {
            InitializeComponent();
        }

        private void Level_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Name)
            {
                case "Latwy":
                    poziom = 1;
                    break;
                case "Sredni":
                    poziom = 2;
                    break;
                case "Trudny":
                    poziom = 3;
                    break;
            }

            this.Close();
        }

        private void Exit_Click(object sender , RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
