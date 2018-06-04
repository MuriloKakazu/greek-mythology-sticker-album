using Newtonsoft.Json;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Game.Items;
using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace stickeralbum.Design.Controls
{
    /// <summary>
    /// Interação lógica para StickerRegister_Creature.xam
    /// </summary>
    public partial class StickerRegister_Creature : UserControl
    {
        public StickerRegister_Creature() {
            InitializeComponent();
            StickerNewSticker.StickerImage.Source = Sprite.Get("unknown").Source;
            //StickerNewStricker.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            ComboBoxRarity.ItemsSource = rarityOptions.Keys;
            ComboBoxGender.ItemsSource = genderOptions.Keys;
            ComboBoxRarity.SelectedIndex =  ComboBoxGender.SelectedIndex = 0;
        }

        Dictionary<String, Rarity> rarityOptions = new Dictionary<string, Rarity>{
            { "Comum"   , Rarity.Common   },
            { "Incomum" , Rarity.Uncommon },
            { "Raro"     , Rarity.Rare     },
            { "Muito Rara", Rarity.VeryRare },
            { "Épica"     , Rarity.Epic     }
        };

        Dictionary<String, Gender> genderOptions = new Dictionary<String, Gender>{
            { "Masculino"   , Gender.Male },
            { "Feminino"    , Gender.Female },
            { "Nenhum"      , Gender.None }
        };

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
                StickerNewSticker.StickerImage.Source = new BitmapImage(new Uri(dlg.FileName));

                //File.Copy(dlg.FileName, Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\";
            }
        }

        private void ComboBoxRarity_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Rarity rarity;
            if(rarityOptions.TryGetValue(ComboBoxRarity.SelectedItem.ToString(), out rarity)) {
                StickerNewSticker.StickerFrame.Source = Sprite.Get(rarity).Source;
            }
        }

        private void TextBoxName_KeyUp(object sender, KeyEventArgs e) {
            StickerNewSticker.StickerName.Content = TextBoxName.Text;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e) {
            App.ClientWindow.SetCurrentPage(new StickerRegister_TypeChoosing());
        }

        private void TextBoxDangerLevel_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) || (TextBoxDangerLevel.Text.Length == 1 && e.Text != "0") || TextBoxDangerLevel.Text.Length > 1;
        }

        SolidColorBrush normalBg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE2C992"));
        SolidColorBrush pinkBg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffddcc"));
        SolidColorBrush redBg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff0000"));
        private void ButtonRegister_Click(object sender, System.Windows.RoutedEventArgs e) {
            Console.WriteLine("-- FLAG --");
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
            if(StickerNewSticker.StickerImage.Source == Sprite.Get("unknown").Source) {
                LabelTip.Foreground = redBg;
                hasError = true;
            } else {
                LabelTip.Foreground = new SolidColorBrush(Colors.Black);
            }
            if(TextBoxDangerLevel.Text == null || TextBoxDangerLevel.Text == "") {
                TextBoxDangerLevel.Background = pinkBg;
                hasError = true;
            } else {
                TextBoxDangerLevel.Background = normalBg;
            }
            if(hasError) {
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

            Generics.LinkedList<Creature> customCreatures = Creature.GetAll().Where(x => x.IsCustom).ToLinkedList();

            Creature newCustomCreature = new Creature() {
                Name        = TextBoxName.Text,
                Description = TextBoxDescription.Text,
                ID          = Guid.NewGuid().ToString(),
                SpriteID    = imgGuid,
                IsCustom    = true,
                DangerLevel = Int16.Parse(TextBoxDangerLevel.Text)
            };

            rarityOptions.TryGetValue(ComboBoxRarity.Text, out newCustomCreature.Rarity);
            genderOptions.TryGetValue(ComboBoxGender.Text, out newCustomCreature.Gender);

            customCreatures.Add(newCustomCreature);

            File.WriteAllText(Paths.CustomCreaturesMetadata, JsonConvert.SerializeObject(customCreatures, Formatting.Indented));
            Cache.Clear();
            Cache.Load();
            Cache.DumpLog();
            Game.GameMaster.Player.Inventory.Add(new SimpleSticker() {
                ItemID = newCustomCreature.ID
            });
            Game.GameMaster.SaveAll();
            App.ClientWindow.SetCurrentPage(new StickerRegister_Finished(StickerNewSticker));
        }
    }
}
