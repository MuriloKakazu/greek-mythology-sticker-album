using NAudio.Wave;
using stickeralbum.Audio;
using stickeralbum.Design;
using stickeralbum.Design.Controls;
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
            Cache.Load();
            Cache.DumpLog();
            GameMaster.LoadAll();
            //GameMaster.Player.Inventory.Add(new SimpleSticker() { ItemID = "god_zeus" });
            //GameMaster.SaveAll();
            InitializeComponent();
            App.ClientWindow.SetCurrentPage(new Homepage());
            soundPlayer = new SoundPlayer();
            this.Background = new ImageBrush(Sprite.Get("bg_oldpaper").Source);
            var reader = new Mp3FileReader(Paths.AudioDirectory + "st_album.mp3");
            var waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
            //soundPlayer.Play(Paths.AudioDirectory + "fx_album.mp3");
            //TestUtil.RunAllTests();
        }

        public void SetCurrentPage(UserControl control) {
            Page.Children.Clear();
            Page.Children.Add(control);
        }
    }
}
