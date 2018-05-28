using stickeralbum.Entities;
using stickeralbum.Enums;
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

namespace stickeralbum.Design.Controls
{
    /// <summary>
    /// Interaction logic for Sticker.xaml
    /// </summary>
    public partial class Sticker : UserControl
    {
        public Entity Entity { get; protected set; }

        public Sticker()
            => InitializeComponent();

        public Sticker(Entity e) : this() 
            => SetEntity(e);

        public void SetEntity(Entity e) {
            this.Entity = e;

            if (!Player.HasUnlocked(e)) {
                //this.Background          = new ImageBrush(entity.Background.Source);
                this.StickerFrame.Source = Sprite.Get(e.Rarity).Source;
                this.StickerImage.Source = e.Sprite.Source;
                this.StickerName.Content = e.Name;
                this.StickerName.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            } else {
                MakeSecret();
            }
        }

        public void MakeSecret() {
            this.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            this.StickerImage.Source = Sprite.Get("unknown").Source;
            this.StickerName.Content = String.Concat(Enumerable.Repeat("?", Entity.Name.Length));
            this.StickerName.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }
    }
}
