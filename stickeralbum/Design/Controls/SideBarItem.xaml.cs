using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for SideBarItem.xaml
    /// </summary>
    public partial class SideBarItem : UserControl {
        public SideBarItem() 
            => InitializeComponent();

        public void SetText(String text) 
            => Label.Content = text;

        public void SetIcon(Sprite icon) 
            => Icon.Source = icon.Source;

        private void UserControl_MouseEnter(object sender, MouseEventArgs e) {
            Background.Opacity = 100;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) {
            Background.Opacity = 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            Background.Opacity = 0;
        }
    }
}
