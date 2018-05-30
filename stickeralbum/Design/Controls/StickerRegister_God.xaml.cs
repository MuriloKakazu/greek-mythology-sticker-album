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

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for StickerRegister.xaml
    /// </summary>
    public partial class StickerRegister_God : UserControl {

        Dictionary<String, Rarity> rarityOptions = new Dictionary<string, Rarity>{
            { "Common"   , Rarity.Common   },
            { "Uncommon" , Rarity.Uncommon },
            { "Rare"     , Rarity.Rare     },
            { "Very Rare", Rarity.VeryRare },
            { "Epic"     , Rarity.Epic     }
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

        private void StickerNewStricker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if(result == true) {
                // Open document 
                StickerNewStricker.StickerImage.Source = new BitmapImage(new Uri(dlg.FileName));

                //File.Copy(dlg.FileName, Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\";
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
    }
}
