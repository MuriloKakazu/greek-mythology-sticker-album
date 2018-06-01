using stickeralbum.Debug;
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
    }
}
