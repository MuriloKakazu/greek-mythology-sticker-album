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

        public float volume;

        public Configs() {
            InitializeComponent();
            volume = Game.GameMaster.Settings.Volume;
        }

        private void CheckBoxMuted_Click(object sender, RoutedEventArgs e) {
            SliderVolume.Value = CheckBoxMuted.IsChecked.Value ? 0 : volume;
        }

        private void ButtonRestore_Click(object sender, RoutedEventArgs e) {
            File.WriteAllText(Paths.CustomCreaturesMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomSemiGodsMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomGodsMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomTitansMetadata, "[\n\n]");
            File.WriteAllText(Paths.CustomSpritesMetadata, "[\n\n]");
            DirectoryInfo di = new DirectoryInfo(Paths.CustomSpritesDirectory);
            Game.GameMaster.Player.Inventory.RemoveCustoms();
            Cache.Clear();
            foreach(FileInfo file in di.GetFiles()) {
                file.Delete();
            }
            Cache.Load();
        }
    }
}
