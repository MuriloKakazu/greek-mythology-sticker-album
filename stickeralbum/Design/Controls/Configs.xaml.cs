using stickeralbum.Debug;
using stickeralbum.Game;
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
            DirectoryInfo di = new DirectoryInfo(Paths.CustomSpritesDirectory);
            var emptyJson = "[\n\n]";

            File.WriteAllText(Paths.CustomGodsMetadata,      emptyJson);
            File.WriteAllText(Paths.CustomTitansMetadata,    emptyJson);
            File.WriteAllText(Paths.CustomSpritesMetadata,   emptyJson);
            File.WriteAllText(Paths.CustomSemiGodsMetadata,  emptyJson);
            File.WriteAllText(Paths.CustomCreaturesMetadata, emptyJson);

            GameMaster.Player.Inventory.RemoveCustoms();
            Cache.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            foreach (FileInfo file in di.GetFiles()) {
                try {
                    file.Delete();
                } catch (Exception ex) {
                    DebugUtils.LogError(ex.Message);
                }
            }

            Cache.Load();
        }
    }
}
