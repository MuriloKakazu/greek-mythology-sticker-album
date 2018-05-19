using System;
using System.Collections.Generic;
using System.IO;
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

namespace stickeralbum.Design.Controls
{
    /// <summary>
    /// Interaction logic for StickerRegister.xaml
    /// </summary>
    public partial class StickerRegister : UserControl
    {
        public StickerRegister()
        {
            InitializeComponent();
        }
        public string imgPath = null;

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true) {
                // Open document 
                imgStickerImage.Source = new BitmapImage(new Uri(dlg.FileName));
                //File.Copy(dlg.FileName, Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\";
            }
        }
    }
}
