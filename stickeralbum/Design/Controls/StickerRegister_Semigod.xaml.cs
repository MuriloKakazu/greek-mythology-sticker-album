using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
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
    /// Interação lógica para Page1.xam
    /// </summary>
    public partial class StickerRegister_Semigod : UserControl {

        Dictionary<String, Rarity> rarityOptions = new Dictionary<string, Rarity>{
            { "Comum"   , Rarity.Common   },
            { "Incomum" , Rarity.Uncommon },
            { "Raro"     , Rarity.Rare     },
            { "Muito Rara", Rarity.VeryRare },
            { "Épica"     , Rarity.Epic     }
        };

        Dictionary<String, String> name_x_id = new Dictionary<string, string>();

        public StickerRegister_Semigod() {
            InitializeComponent();
            EntityUtils.AllGods().ForEach(x => name_x_id.Add(x.Name, x.ID));
            StickerNewStricker.StickerImage.Source = Sprite.Get("unknown").Source;
            //StickerNewStricker.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            ComboBoxRarity.ItemsSource = rarityOptions.Keys;
            ComboBoxGender.ItemsSource = new Generics.LinkedList<String>() { "Masculino", "Feminino", "None" };
            ComboBoxRelatedGod.ItemsSource = name_x_id.Keys;
            ComboBoxRarity.SelectedIndex = ComboBoxRelatedGod.SelectedIndex = ComboBoxGender.SelectedIndex = 0;
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e) {
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
            if(StickerNewStricker.StickerImage.Source == Sprite.Get("unknown").Source) {
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
