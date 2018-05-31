using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Extensions;
using stickeralbum.Game;
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
        protected Sticker[] AllStickers;

        public Album() {
            InitializeComponent();
            InteractionBar.FilterChanged += InteractionBar_FilterChanged;
            DebugUtils.Log("Default Album ctor");
        }

        private void InteractionBar_FilterChanged(object sender, EventArgs e) 
            => Filter(InteractionBar.FilterSettings);

        public Album(Sticker[] stickers) : this() 
            => Setup(stickers);

        public Album(Entity[] entities) : this(GetStickersFromEntities(entities)) {}

        protected static Sticker[] GetStickersFromEntities(Entity[] entities) {
            var stickers = new LinkedList<Sticker>();
            entities.ToLinkedList().ForEach(x => stickers.Add(new Sticker(x)));
            return stickers.ToArray();
        }

        public void Setup(Sticker[] stickers)
            => GeneratePagesForStickers((AllStickers = stickers)
              .OrderBy(x => x.Entity.Rarity).ToArray());

        public void Filter(FilterSettings settings) {
            var filtered = new LinkedList<Sticker>();
            var sourceCopy = new LinkedList<Sticker>();
            AllStickers.ToLinkedList().ForEach(x => sourceCopy.Add(new Sticker(x.Entity)));
            if ((Boolean) settings.Get("custom")) {
                filtered.Add(sourceCopy.Where(x => x.Entity.IsCustom && !filtered.Contains(x)));
            }
            if ((Boolean)settings.Get("titan")) {
                filtered.Add(sourceCopy.Where(x => x.Entity.IsTitan && !filtered.Contains(x)));
            }
            if ((Boolean)settings.Get("god")) {
                filtered.Add(sourceCopy.Where(x => x.Entity.IsGod && !filtered.Contains(x)));
            }
            if ((Boolean)settings.Get("semigod")) {
                filtered.Add(sourceCopy.Where(x => x.Entity.IsSemiGod && !filtered.Contains(x)));
            }
            if ((Boolean)settings.Get("creature")) {
                filtered.Add(sourceCopy.Where(x => x.Entity.IsCreature && !filtered.Contains(x)));
            }
            if ((Boolean)settings.Get("male") && (Boolean)settings.Get("female")) {
                //
            } else if ((Boolean) settings.Get("male")) {
                filtered = filtered.Where(x => x.Entity.Gender == Gender.Male).ToLinkedList();
            } else if ((Boolean )settings.Get("female")) {
                filtered = filtered.Where(x => x.Entity.Gender == Gender.Female).ToLinkedList();
            } else {
                filtered = filtered.Where(x => x.Entity.Gender != Gender.Male && x.Entity.Gender != Gender.Female).ToLinkedList();
            }
            if (settings.Query != null) {
                filtered = filtered.Where(x => x.Entity.Name.ToLower().Contains(settings.Query.ToLower())).ToLinkedList();
                DebugUtils.Log($"Filter query => {settings.Query}");
                DebugUtils.Log($"Filtered query result => {filtered.Count}");
            }
            GeneratePagesForStickers(filtered.OrderBy(x => x.Entity.Rarity).ToArray());
        }

        public void Refresh() {
            UpperPrevPage.Width = this.ActualWidth / 2f;
            UpperNextPage.Width = this.ActualWidth / 2f;
        }

        public void GeneratePagesForStickers(Sticker[] stickers) {
            stickers.ToLinkedList().ForEach(x => x.DetachParent());
            if (Pages != null) {
                Pages.ToLinkedList().ForEach(x => x.Clear());
            }
            var pagesList = new LinkedList<AlbumPage>();
            var pagesCount = Math.Ceiling(stickers.Length / 8d);
            var stickersList = stickers.ToLinkedList();
            for (var i = 0; i < pagesCount; i++) {
                pagesList.Add(new AlbumPage(
                    stickersList.Skip(i * 8).Take(8).ToArray()
                ));
            }
            this.Pages = pagesList.ToArray();
            this.DockPanel.Children.Clear();
            this.CurrentPage = null;
            if (Pages.Length > 0) {
                this.CurrentPage = Pages[0];
                this.DockPanel.Children.Add(this.CurrentPage);
            }
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

        private void UpperPrevPage_Click(object sender, RoutedEventArgs e) {
            PreviousPage();
        }

        private void UpperNextPage_Click(object sender, RoutedEventArgs e) {
            NextPage();
        }

        private void LowerPrevPage_Click(object sender, RoutedEventArgs e) {
            PreviousPage();
        }

        private void LowerNextPage_Click(object sender, RoutedEventArgs e) {
            NextPage();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            Refresh();
        }
    }
}
