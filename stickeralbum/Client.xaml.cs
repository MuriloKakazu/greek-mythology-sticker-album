using NAudio.Wave;
using stickeralbum.Audio;
using stickeralbum.Design;
using stickeralbum.Entities;
using stickeralbum.Game;
using stickeralbum.Game.Items;
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

namespace stickeralbum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Client : Window
    {
        public SoundPlayer soundPlayer;
        public Client() {
            GameMaster.LoadAll();
            Cache.Load();
            //Cache.DumpLog();
            InitializeComponent();
            this.Background = new ImageBrush(Sprite.Get("bg_oldpaper").Source);
            if (!SoundTrack.Get("st_main").IsPlaying) {
                SoundPlayer.StopAll("st_blacksmith");
                SoundPlayer.Instance.Play(SoundTrack.Get("st_main"), loop: true);
            }
            //TestUtil.RunAllTests();
        }

        public void SetCurrentPage(UserControl control) {
            Page.Children.Clear();
            Page.Children.Add(control);
        }
    }
}
