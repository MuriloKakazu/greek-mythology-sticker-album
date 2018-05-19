using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for StickerRegister.xaml
    /// </summary>
    public partial class StickerRegister : UserControl {
        public StickerRegister() {
            //BackGround = IO.Paths.AssetsDirectory + "GreekBackground.png";
            BackGround = "assets\\GreekBackground.png";
            Console.WriteLine(BackGround);
            InitializeComponent();
        }
        public string BackGround  {get; set;}

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
                //imgSticker.Source = new BitmapImage(new Uri(dlg.FileName));
                //File.Copy(dlg.FileName, Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\";
            }
        }
    }
}
