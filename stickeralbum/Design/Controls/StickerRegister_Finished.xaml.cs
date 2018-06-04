using stickeralbum.Audio;
using stickeralbum.Entities;
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
    /// Interação lógica para StickerRegister_Finished.xam
    /// </summary>
    public partial class StickerRegister_Finished : UserControl {

        public StickerRegister_Finished(Sticker sticker) {
            InitializeComponent();
            StickerNewSticker.StickerFrame.Source = sticker.StickerFrame.Source;
            StickerNewSticker.StickerImage.Source = sticker.StickerImage.Source;
            StickerNewSticker.StickerName.Content = sticker.StickerName.Content;
            SoundPlayer.Instance.Play(SoundTrack.Get("sfx_forge"));
        }

        private void ButtonAlbum_Click(object sender, RoutedEventArgs e) {
            var entities = Entity.GetAll().ToArray();
            App.ClientWindow.SetCurrentPage(new Album(entities));
        }

        private void ButtonNewSticker_Click(object sender, RoutedEventArgs e) {
            App.ClientWindow.SetCurrentPage(new StickerRegister_TypeChoosing());
        }
    }
}
