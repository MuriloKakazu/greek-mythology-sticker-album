using stickeralbum.IO;
using stickeralbum.Entities;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using stickeralbum.Enums;
using System.Collections.Generic;
using System.IO;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for StickerRegister.xaml
    /// </summary>
    public partial class StickerRegister_God : UserControl {

        Dictionary<String, Rarity> rarityOptions = new Dictionary<string, Rarity>{
            { "Comum"   , Rarity.Common   },
            { "Incomum" , Rarity.Uncommon },
            { "Rara"     , Rarity.Rare     },
            { "Muito rara", Rarity.VeryRare },
            { "Épica"     , Rarity.Epic     }
        };

        public StickerRegister_God() {
            InitializeComponent();
            StickerNewStricker.StickerImage.Source = Sprite.Get("unknown").Source;
            StickerNewStricker.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            ComboBoxRarity.ItemsSource = rarityOptions.Keys;
            ComboBoxGender.ItemsSource = new Generics.LinkedList<String>(){"Masculino", "Feminino", "None"};
        }
        private void _this_Loaded(object sender, System.Windows.RoutedEventArgs e) {
         
        }
        Microsoft.Win32.OpenFileDialog dlg;
        private void StickerNewStricker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

            dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if(result == true) {
                // Open document 
                StickerNewStricker.StickerImage.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void ComboBoxRarity_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Rarity rarity;
            if(rarityOptions.TryGetValue(ComboBoxRarity.SelectedItem.ToString(), out rarity)) {
                StickerNewStricker.StickerFrame.Source = Sprite.Get(rarity).Source;
            }
        }

        private void TextBoxName_KeyUp(object sender, KeyEventArgs e) {
            StickerNewStricker.StickerName.Content = TextBoxName.Text;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            App.ClientWindow.SetCurrentPage(new StickerRegister_TypeChoosing());
        }

        SolidColorBrush normalBg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE2C992"));
        SolidColorBrush pinkBg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffddcc"));
        SolidColorBrush redBg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff0000"));
        private void ButtonRegister_Click(object sender, System.Windows.RoutedEventArgs e) {
            bool hasError = false;
            if(TextBoxName.Text == null || TextBoxName.Text == "") {
                TextBoxName.Background = pinkBg;
                hasError = true;
            } else {
                TextBoxName.Background = normalBg;
            }
            if(TextBoxDescription.Text == null || TextBoxDescription.Text == "") {
                TextBoxDescription.Background = pinkBg;
                hasError = true;
            } else {
                TextBoxDescription.Background = normalBg;
            }
            if(StickerNewStricker.StickerImage.Source == Sprite.Get("unknown").Source){
                LabelTip.Foreground = redBg;
            } else {
                LabelTip.Foreground = new SolidColorBrush(Colors.Black);
            }
            if(hasError) {
                return;
            }
        }
    }
}
