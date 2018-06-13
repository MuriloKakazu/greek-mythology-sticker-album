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
using System.Linq;
using Newtonsoft.Json;
using stickeralbum.Generics;
using stickeralbum.Game.Items;
using stickeralbum.Debug;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for StickerRegister.xaml
    /// </summary>
    public partial class StickerRegister_God : UserControl {

        Dictionary<String, Rarity> rarityOptions = new Dictionary<String, Rarity>{
            { "Comum"   , Rarity.Common   },
            { "Incomum" , Rarity.Uncommon },
            { "Rara"     , Rarity.Rare     },
            { "Muito rara", Rarity.VeryRare },
            { "Épica"     , Rarity.Epic     }
        };

        Dictionary<String, Gender> genderOptions = new Dictionary<String, Gender>{
            { "Masculino"   , Gender.Male },
            { "Feminino"    , Gender.Female },
            { "Nenhum"      , Gender.None }
        };

        Dictionary<String, String> fatherName_x_id = new Dictionary<string, string>();
        Dictionary<String, String> motherName_x_id = new Dictionary<string, string>();

        public StickerRegister_God() {
            InitializeComponent();
            Titan.GetAll().GetMales().ForEach(x => fatherName_x_id.Add(x.Name, x.ID));
            God.GetAll().GetMales().ForEach(x => fatherName_x_id.Add(x.Name, x.ID));
            Titan.GetAll().GetFemales().ForEach(x => motherName_x_id.Add(x.Name, x.ID));
            God.GetAll().GetFemales().ForEach(x => motherName_x_id.Add(x.Name, x.ID));
            StickerNewStricker.StickerImage.Source = Sprite.Get("unknown").Source;
            //StickerNewStricker.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            ComboBoxRarity.ItemsSource = rarityOptions.Keys;
            ComboBoxGender.ItemsSource = genderOptions.Keys;
            ComboBoxFather.ItemsSource = fatherName_x_id.Keys;
            ComboBoxMother.ItemsSource = motherName_x_id.Keys;
            ComboBoxRarity.SelectedIndex = ComboBoxFather.SelectedIndex = ComboBoxGender.SelectedIndex = ComboBoxMother.SelectedIndex = 0;
        }
        private void _this_Loaded(object sender, System.Windows.RoutedEventArgs e) {
         
        }
        Microsoft.Win32.OpenFileDialog dlg;
        private void StickerNewStricker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

            dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png";


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
            try {
                bool hasError = false;
                if (TextBoxName.Text == null || TextBoxName.Text == "") {
                    TextBoxName.Background = pinkBg;
                    hasError = true;
                } else {
                    TextBoxName.Background = normalBg;
                }
                if (TextBoxDescription.Text == null || TextBoxDescription.Text == "") {
                    TextBoxDescription.Background = pinkBg;
                    hasError = true;
                } else {
                    TextBoxDescription.Background = normalBg;
                }
                if (StickerNewStricker.StickerImage.Source == Sprite.Get("unknown").Source) {
                    LabelTip.Foreground = redBg;
                    hasError = true;
                } else {
                    LabelTip.Foreground = new SolidColorBrush(Colors.Black);
                }
                if (hasError) {
                    return;
                }
                String imgGuid = Guid.NewGuid().ToString();
                File.Copy(dlg.FileName, Path.Combine(Paths.CustomSpritesDirectory, imgGuid));
                Generics.LinkedList<Sprite> spritesMetadata = JsonConvert.DeserializeObject<Generics.LinkedList<Sprite>>(File.ReadAllText(Paths.CustomSpritesMetadata));
                spritesMetadata.Add(new Sprite() {
                    ID = imgGuid,
                    Path = imgGuid,
                    IsCustom = true
                });
                File.WriteAllText(Paths.CustomSpritesMetadata, JsonConvert.SerializeObject(spritesMetadata, Formatting.Indented));

                Generics.LinkedList<God> customGods = God.GetAll().Where(x => x.IsCustom).ToLinkedList();

                God newCustomGod = new God() {
                    Name = TextBoxName.Text,
                    Description = TextBoxDescription.Text,
                    ID = Guid.NewGuid().ToString(),
                    SpriteID = imgGuid,
                    IsCustom = true
                };

                fatherName_x_id.TryGetValue(ComboBoxFather.Text, out newCustomGod.FatherID);
                motherName_x_id.TryGetValue(ComboBoxMother.Text, out newCustomGod.MotherID);
                rarityOptions.TryGetValue(ComboBoxRarity.Text, out newCustomGod.Rarity);
                genderOptions.TryGetValue(ComboBoxGender.Text, out newCustomGod.Gender);

                customGods.Add(newCustomGod);

                File.WriteAllText(Paths.CustomGodsMetadata, JsonConvert.SerializeObject(customGods, Formatting.Indented));

                Cache.Clear();
                Cache.Load();
                Cache.DumpLog();

                Game.GameMaster.Player.Inventory.Add(new SimpleSticker() {
                    ItemID = newCustomGod.ID
                });

                App.ClientWindow.SetCurrentPage(new StickerRegister_Finished(StickerNewStricker));
            } catch (Exception ex) {
                DebugUtils.LogError("Error creating sticker => " + ex.Message);
            }
        }
    }
}
