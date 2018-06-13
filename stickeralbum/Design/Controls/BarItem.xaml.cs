using stickeralbum.Audio;
using stickeralbum.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SideBarItem.xaml
    /// </summary>
    public partial class BarItem : UserControl {
        public BarItem() {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                SetAntiAliasing();
            }
        }

        public void SetAntiAliasing() {
            if (GameMaster.Settings.AntiAliasing) {
                RenderOptions.SetBitmapScalingMode(Icon, BitmapScalingMode.Fant);
                RenderOptions.SetClearTypeHint(Icon, ClearTypeHint.Enabled);
                RenderOptions.SetEdgeMode(Icon, EdgeMode.Aliased);
            } else {
                RenderOptions.SetBitmapScalingMode(Icon, BitmapScalingMode.LowQuality);
                RenderOptions.SetClearTypeHint(Icon, ClearTypeHint.Auto);
                RenderOptions.SetEdgeMode(Icon, EdgeMode.Unspecified);
            }
        }

        public void SetText(String text) 
            => Label.Content = text;

        public void SetIcon(Sprite icon) 
            => Icon.Source = icon.Source;

        private void UserControl_MouseEnter(object sender, MouseEventArgs e) 
            => Background.Opacity = 100;

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) 
            => Background.Opacity = 0;

        private void UserControl_Loaded(object sender, RoutedEventArgs e) 
            => Background.Opacity = 0;

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e) 
            => SoundPlayer.Instance.Play(SoundTrack.Get("sfx_ring"));
    }
}
