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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace KCKProjektWPF.Controls
{
    /// <summary>
    /// Interaction logic for KeyControl.xaml
    /// </summary>
    public partial class KeyControl : UserControl
    {
        public KeyControl(string pathToElem)
        {
            InitializeComponent();

            Image img = this.image;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(pathToElem);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(img, image);
        }
    }
}
