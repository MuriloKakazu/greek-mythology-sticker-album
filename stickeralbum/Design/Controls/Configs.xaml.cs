using stickeralbum.IO;
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

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interação lógica para Configs.xam
    /// </summary>
    public partial class Configs : UserControl {

        public double volume;
        public bool autoContext = false;

        public Configs() {
            InitializeComponent();
            ButtonSave.IsEnabled = false;
            Console.WriteLine(Game.GameMaster.Settings.Volume);
            autoContext = true;
            volume = SliderVolume.Value = (double)Game.GameMaster.Settings.Volume;
            autoContext = false;

        }

        private void CheckBoxMuted_Click(object sender, RoutedEventArgs e) {
            autoContext = true;
            SliderVolume.Value = CheckBoxMuted.IsChecked.Value ? 0 : volume;
            autoContext = false;
            ButtonSave.IsEnabled = true;
        }

        private void ButtonRestore_Click(object sender, RoutedEventArgs e) {
            File.WriteAllText(Paths.CustomCreaturesMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomSemiGodsMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomGodsMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomTitansMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomSpritesMetadata, "[\n\n]");
            Console.WriteLine("[CACHE] - Metadata removed");
            DirectoryInfo di = new DirectoryInfo(Paths.CustomSpritesDirectory);
            Game.GameMaster.Player.Inventory.RemoveCustoms();
            Console.WriteLine("[CACHE] - Custom stickers removed");
            Console.WriteLine(Game.GameMaster.Player.Inventory.Stickers);
            Game.GameMaster.SaveAll();
            Cache.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            foreach(FileInfo file in di.GetFiles()) {
                //file.Delete();
            }
            Cache.Load();
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if(!autoContext) {
                if(CheckBoxMuted.IsChecked.Value) {
                    CheckBoxMuted.IsChecked = false;
                }
                Console.WriteLine(SliderVolume.Value);
                volume = SliderVolume.Value;
                ButtonSave.IsEnabled = true;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e) {
            Game.GameMaster.Settings.Volume = (float)volume;
            Game.GameMaster.Settings.AntiAliasing = CheckBoxAA.IsChecked.Value;
            Game.GameMaster.SaveAll();
            ButtonSave.IsEnabled = false;
        }

        private void CheckBoxAA_Click(object sender, RoutedEventArgs e) {
            ButtonSave.IsEnabled = true;
        }
    }
}
