using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for StickerRegister.xaml
    /// </summary>
    public partial class StickerRegister : UserControl {
        public StickerRegister() {
            InitializeComponent();
        }

        private void ImageSprite_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if(result == true) {
                // Open document 
                ImageSprite.Source = new BitmapImage(new Uri(dlg.FileName));
                //File.Copy(dlg.FileName, Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\";
            }
        }

        private void _this_Loaded(object sender, System.Windows.RoutedEventArgs e) {
         
        }
    }
}
