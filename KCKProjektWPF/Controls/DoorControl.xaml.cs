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

namespace KCKProjektWPF.Controls
{
    /// <summary>
    /// Interaction logic for DoorControl.xaml
    /// </summary>
    public partial class DoorControl : UserControl
    {
        public DoorControl()
        {
            InitializeComponent();
            Rectangle rect = this.rect;

            rect.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($@"pack://application:,,,/KCKProjektWPF;component/Image/drzwi.png")),
                Stretch = Stretch.UniformToFill
            };
        }
    }
}
