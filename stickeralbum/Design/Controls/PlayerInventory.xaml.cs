using stickeralbum.Debug;
using stickeralbum.Enums;
using stickeralbum.Extensions;
using stickeralbum.Game;
using stickeralbum.Generics;
using System;
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

namespace stickeralbum.Design.Controls
{
    /// <summary>
    /// Interaction logic for PlayerInventory.xaml
    /// </summary>
    public partial class PlayerInventory : UserControl
    {
        public Sticker[] Stickers;

        public PlayerInventory() { 
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                TrashBin.Source = Sprite.Get("trashbin").Source;
                CoinIcon.Source = Sprite.Get("coin").Source;
                Setup();
            }
        }

        public void Setup() {
            Clear();
            var stickerList = new LinkedList<Sticker>();
            GameMaster.Player.Inventory.Stickers.ForEach(x => stickerList.Add(new Sticker(x.Entity, overrideValidation: true)));
            this.Stickers = stickerList.ToArray();
            stickerList.ForEach(x => x.DetachParent())
                       .ForEach(x => x.AllowDrag = true)
                       .ForEach(x => x.AllowDrop = false)
                       .ForEach(x => x.DataDropped += Sticker_DataDropped)
                       .ForEach(x => Collection.Children.Add(x));
        }

        private void Sticker_DataDropped(object sender, EventArgs e) {
            (sender as Sticker).DetachParent();
            DebugUtils.Log($"Data dropped => {(sender as Sticker).Entity}");
        }

        public virtual void Refresh() {
            Collection.InvalidateVisual();
        }

        public void Clear()
            => Collection.Children.Clear();

        private void TrashBin_Drop(object sender, DragEventArgs e) {
            TrashBin.RenderTransform = null;
            var formats = e.Data.GetFormats();
            if (formats.Length > 0) {
                var data = e.Data.GetData(e.Data.GetFormats()[0]);
                if (TrashBin.AllowDrop && data is Sticker) {
                    var droppedSticker = data as Sticker;
                    var droppedEntity = droppedSticker.Entity;
                    var player = GameMaster.Player;
                    player.Coins += droppedEntity.GetCoinValue();
                    DebugUtils.Log($"Entity Trashed => {droppedEntity.ID}");
                    GameMaster.Player.Inventory.Remove(droppedEntity.ID, 1);
                    droppedSticker.DetachParent();
                }
            }
            CoinIcon.Visibility     = Visibility.Hidden;
            CoinQuantity.Visibility = Visibility.Hidden;
        }

        private void TrashBin_DragEnter(object sender, DragEventArgs e) {
            TrashBin.RenderTransform = new RotateTransform(2, 300, 200);
            var formats = e.Data.GetFormats();
            if (formats.Length > 0) {
                var data = e.Data.GetData(e.Data.GetFormats()[0]);
                if (TrashBin.AllowDrop && data is Sticker) {
                    var draggingSticker     = data as Sticker;
                    var draggingEntity      = draggingSticker.Entity;
                    CoinQuantity.Content    = $"+{draggingEntity.GetCoinValue()}";
                    CoinIcon.Visibility     = Visibility.Visible;
                    CoinQuantity.Visibility = Visibility.Visible;
                }
            }
        }

        private void TrashBin_DragLeave(object sender, DragEventArgs e) {
            TrashBin.RenderTransform = null;
            CoinIcon.Visibility     = Visibility.Hidden;
            CoinQuantity.Visibility = Visibility.Hidden;
        }
    }
}
