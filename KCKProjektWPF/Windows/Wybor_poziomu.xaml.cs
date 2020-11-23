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
using KCKProjektAPI;

namespace KCKProjektWPF.Windows
{
    /// <summary>
    /// Interaction logic for Wybor_poziomu.xaml
    /// </summary>
    public partial class Wybor_poziomu : Window
    {
        public LevelEnum Level { get; set; } = LevelEnum.Easy;
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
                    Level = LevelEnum.Easy;
                    break;
                case "Sredni":
                    Level = LevelEnum.Mid;
                    break;
                case "Trudny":
                    Level = LevelEnum.Hard;
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
