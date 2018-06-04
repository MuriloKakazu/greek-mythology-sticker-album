using stickeralbum.Audio;
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

        public double volume;
        public bool autoContext = false;

        public Configs() {
            InitializeComponent();
            ButtonSave.IsEnabled = false;
            Console.WriteLine(GameMaster.Settings.Volume);
<<<<<<< HEAD
            CheckBoxAA.IsChecked = GameMaster.Settings.AntiAliasing;
=======
>>>>>>> adb373a6ab161c13d4c6d8ccd668fea27c65396a
            autoContext = true;
            volume = SliderVolume.Value = GameMaster.Settings.Volume;
            autoContext = false;
            volume = GameMaster.Settings.Volume;

            if (!SoundTrack.Get("st_main").IsPlaying) {
                SoundPlayer.StopAll();
                SoundPlayer.Instance.Play(SoundTrack.Get("st_main"), loop: true);
            }
        }

        private void CheckBoxMuted_Click(object sender, RoutedEventArgs e) {
            autoContext = true;
            SliderVolume.Value = CheckBoxMuted.IsChecked.Value ? 0 : volume;
            autoContext = false;
            ButtonSave.IsEnabled = true;
        }

        private void ButtonRestore_Click(object sender, RoutedEventArgs e) {
            var emptyJson = "[\n\n]";
            File.WriteAllText(Paths.CustomCreaturesMetadata, emptyJson);
            File.WriteAllText(Paths.CustomSemiGodsMetadata,  emptyJson);
            File.WriteAllText(Paths.CustomGodsMetadata,      emptyJson);
            File.WriteAllText(Paths.CustomTitansMetadata,    emptyJson);
            File.WriteAllText(Paths.CustomSpritesMetadata,   emptyJson);
            //Console.WriteLine("[CACHE] - Metadata removed");
            DebugUtils.LogCache("Metadata removed");
            DirectoryInfo di = new DirectoryInfo(Paths.CustomSpritesDirectory);
            GameMaster.Player.Inventory.RemoveCustoms();
            //Console.WriteLine("[CACHE] - Custom stickers removed");
            DebugUtils.LogCache("Custom stickers removed");
            //Console.WriteLine(Game.GameMaster.Player.Inventory.Stickers);
            DebugUtils.Log(GameMaster.Player.Inventory.Stickers);
            GameMaster.SaveAll();
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
            GameMaster.Settings.Volume = (float)volume;
            GameMaster.Settings.AntiAliasing = CheckBoxAA.IsChecked.Value;
            GameMaster.SaveAll();
            ButtonSave.IsEnabled = false;
        }

        private void CheckBoxAA_Click(object sender, RoutedEventArgs e) {

            //////////ButtonSave.IsEnabled = true;*-
        }
    }
}
