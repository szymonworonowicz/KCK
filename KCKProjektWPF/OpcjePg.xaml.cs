﻿using System;
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
    /// Interaction logic for OpcjePg.xaml
    /// </summary>
    public partial class OpcjePg : Page
    {
        
        public OpcjePg()
        {
            InitializeComponent();

        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            window.Content = new Startowa();
        }
    }
}
