using stickeralbum.Generics;
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
    /// Interaction logic for AlbumPage.xaml
    /// </summary>
    public partial class AlbumPage : UserControl {
        public Sticker[] Stickers;

        public AlbumPage()
            => InitializeComponent();

        public AlbumPage(Sticker[] stickers) : this() 
            => Setup(stickers);

        public void Setup(Sticker[] stickers) {
            this.Stickers = stickers;
            stickers.ToLinkedList().ForEach(x => x.Width = 225);
            stickers.ToLinkedList().ForEach(x => x.Height = 337.5);
            stickers.ToLinkedList().ForEach(x => PageContent.Items.Add(x));
            Refresh();
        }

        public virtual void Refresh() {

        }

        //protected Vector GetBestLayoutConfiguration(Array array)
        //    => (array.Length == 1) ? new Vector(1, 1) :
        //       (array.Length == 2) ? new Vector(2, 1) :
        //       (array.Length <= 4) ? new Vector(2, 2) :
        //       (array.Length <= 6) ? new Vector(3, 2) :
        //       (array.Length <= 9) ? new Vector(3, 3) :
        //       throw new Exception("No suitable layout format found for array size => " + array.Length);
    }
}
