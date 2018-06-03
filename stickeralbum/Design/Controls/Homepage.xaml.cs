using stickeralbum.Audio;
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
    /// Interação lógica para Homepage.xam
    /// </summary>
    public partial class Homepage : UserControl {
        public Homepage() {
            InitializeComponent();

            if (!SoundTrack.Get("st_main").IsPlaying) {
                SoundPlayer.StopAll("st_blacksmith");
                SoundPlayer.Instance.Play(SoundTrack.Get("st_main"), loop: true);
            }
        }
    }
}
