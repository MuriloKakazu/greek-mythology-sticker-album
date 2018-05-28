using stickeralbum.Entities;
using stickeralbum.Generics;
using System;
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
    /// Interaction logic for Album.xaml
    /// </summary>
    public partial class Album : UserControl
    {
        protected AlbumPage[] Pages;
        protected AlbumPage CurrentPage;

        public Album()
            => InitializeComponent();

        public Album(Sticker[] stickers) : this() 
            => Setup(stickers);

        public Album(Entity[] entities) : this(GetStickersFromEntities(entities)) {}

        protected static Sticker[] GetStickersFromEntities(Entity[] entities) {
            var stickers = new LinkedList<Sticker>();
            entities.ToLinkedList().ForEach(x => stickers.Add(new Sticker(x)));
            return stickers.ToArray();
        }

        public void Setup(Sticker[] stickers) {
            var pagesList    = new LinkedList<AlbumPage>();
            var pagesCount   = Math.Ceiling(stickers.Length / 9d);
            var stickersList = stickers.ToLinkedList();
            for (var i = 0; i < pagesCount; i++) {
                pagesList.Add(new AlbumPage(
                    stickersList.Skip(i * 6).Take(6).ToArray()
                ));
                //pagesList[i].Stickers.ToLinkedList().ForEach(x => x.MouseWheel += Album_MouseWheel);
                pagesList[i].ListBox.MouseWheel += Album_MouseWheel;
            }
            this.Pages = pagesList.ToArray();
            this.CurrentPage = Pages[0];
            this.DockPanel.Children.Clear();
            this.DockPanel.Children.Add(this.CurrentPage);
        }

        private void Album_MouseWheel(object sender, MouseWheelEventArgs e) {
            Console.WriteLine(e.Delta);
            if (e.Delta > 0) {
                NextPage();
            } else if (e.Delta < 0) {
                PreviousPage();
            }
        }

        public Boolean SetPage(AlbumPage page) {
            if (page == null) return false;
            this.CurrentPage = page;
            this.DockPanel.Children.Clear();
            this.DockPanel.Children.Add(page);
            return true;
        }

        public Boolean NextPage() {
            var pageIndex = Pages.ToLinkedList().IndexOf(this.CurrentPage);
            if (pageIndex < this.Pages.Length - 1) {
                return SetPage(Pages[pageIndex + 1]);
            }
            return false;
        }

        public Boolean PreviousPage() {
            var pageIndex = Pages.ToLinkedList().IndexOf(this.CurrentPage);
            if (pageIndex > 0) {
                return SetPage(Pages[pageIndex - 1]);
            }
            return false;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e) {
            NextPage();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e) {
            PreviousPage();
        }
    }
}
