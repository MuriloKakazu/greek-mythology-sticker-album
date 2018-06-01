using Newtonsoft.Json;
using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for Sticker.xaml
    /// </summary>
    public partial class Sticker : UserControl {
        public Entity Entity { get; protected set; }
        public Boolean AllowDrag { get; set; }
        protected Window DragDropPreview;
        public event DataDroppedEventHandler DataDropped;
        public delegate void DataDroppedEventHandler(object sender, EventArgs e);

        public Sticker() {
            InitializeComponent();
            DataDropped += OnDataDropped;
        }

        public Sticker(Entity e, Boolean overrideValidation = false) :
            this() => SetEntity(e, overrideValidation);

        public void SetEntity(Entity e, Boolean overrideValidation = false) {
            this.Entity = e;

            if (Entity.IsUnlocked || overrideValidation) {
                //this.Background = new ImageBrush(Entity.Background.Source);
                this.StickerFrame.Source = Sprite.Get(e.Rarity).Source;
                RenderOptions.SetEdgeMode(this.StickerImage, EdgeMode.Aliased);
                RenderOptions.SetBitmapScalingMode(this.StickerImage, BitmapScalingMode.Fant);
                RenderOptions.SetCachingHint(this.StickerImage, CachingHint.Cache);
                RenderOptions.SetClearTypeHint(this.StickerImage, ClearTypeHint.Enabled);
                this.StickerImage.Source = e.Sprite.Source;
                this.StickerName.Content = e.Name;
                this.StickerName.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            } else {
                MakeSecret();
            }
        }

        public virtual void OnDataDropped(object sender, EventArgs e) { }

        public void Refresh() {
            StickerImage.Width = StickerFrame.ActualWidth;
            StickerImage.Height = StickerFrame.ActualHeight;
            SpaceFiller.Height = Math.Round((this.ActualHeight - StickerFrame.ActualHeight) / 2, 2);
            SpaceFiller.Width = this.ActualWidth;
            StickerName.Margin = new Thickness(0, SpaceFiller.Height, 0, 0);
            var targetFontSize = 11f / 200f * (ActualHeight - SpaceFiller.Height);
            if (targetFontSize <= 0) return;
            StickerName.FontSize = targetFontSize;
            StickerName.FontWeight = FontWeights.Normal;
        }

        public void MakeSecret() {
            this.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            //this.StickerImage.Source = Sprite.Get("unknown").Source;
            this.StickerImage.Source = Entity.Sprite.DarkenedSource;
            this.StickerName.Content = Entity.Name;
            //this.StickerName.Content = String.Concat(Enumerable.Repeat("?", Entity.Name.Length));
            this.StickerName.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        public WriteableBitmap GetAllBlackVersionOfBitmap(BitmapSource source) {
            var stride = (Int32)(source.PixelWidth * source.Width);
            var data = new byte[stride * source.PixelHeight];
            source.CopyPixels(data, stride, 0);
            var target = new WriteableBitmap(source);

            for (var i = 0; i < data.Length / 4; i++) {
                data[i * 4] = 0;
                data[i * 4 + 1] = 0;
                data[i * 4 + 2] = 0;
                //data[i * 4 + 3];
            }

            target.WritePixels(new Int32Rect(
                0, 0, source.PixelWidth, source.PixelHeight
            ), data, stride, 0);
            return target;
        }

        private void Self_SizeChanged(object sender, SizeChangedEventArgs e) {
            Refresh();
        }

        private void StickerFrame_MouseEnter(object sender, MouseEventArgs e) {
            StickerImage.Width = StickerFrame.ActualWidth - 20;
            StickerImage.Height = StickerFrame.ActualHeight - 30;
            StickerName.FontWeight = FontWeights.Bold;
        }

        private void StickerFrame_MouseLeave(object sender, MouseEventArgs e) {
            Refresh();
        }

        private void Self_DragEnter(object sender, DragEventArgs e) {
            this.RenderTransform = new RotateTransform(2.0, 300, 200);
        }

        private void Self_DragLeave(object sender, DragEventArgs e) {
            this.RenderTransform = null;
        }

        private void Self_DragOver(object sender, DragEventArgs e) {

        }

        private void Self_Drop(object sender, DragEventArgs e) {
            this.RenderTransform = null;
            if (AllowDrop) {
                Console.WriteLine((sender as Sticker).Entity.ID);
                var droppedSticker = e.Data.GetData(e.Data.GetFormats()[0]) as Sticker;
                var droppedEntity = droppedSticker.Entity;
                if (this.Entity.ID == droppedEntity.ID && !Entity.IsUnlocked) {
                    DebugUtils.Log($"Valid Entity Dropped => {droppedEntity.ID}");
                    GameMaster.Player.Inventory.Remove(droppedEntity.ID, 1);
                    GameMaster.Player.Unlock(droppedEntity.ID);
                    SetEntity(Entity);
                    droppedSticker.DataDropped.Invoke(droppedSticker, EventArgs.Empty);
                }
            }
        }

        private void Self_MouseDown(object sender, MouseButtonEventArgs e) {
            
        }

        private void Self_MouseMove(object sender, MouseEventArgs e) {

        }

        private void Self_GiveFeedback(object sender, GiveFeedbackEventArgs e) {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            this.DragDropPreview.Left = w32Mouse.X;
            this.DragDropPreview.Top = w32Mouse.Y;
        }

        private void CreateDragDropWindow(Visual dragElement) {
            this.DragDropPreview = new Window();
            DragDropPreview.WindowStyle = WindowStyle.None;
            DragDropPreview.AllowsTransparency = true;
            DragDropPreview.AllowDrop = false;
            DragDropPreview.Background = null;
            DragDropPreview.IsHitTestVisible = false;
            DragDropPreview.SizeToContent = SizeToContent.WidthAndHeight;
            DragDropPreview.Topmost = true;
            DragDropPreview.ShowInTaskbar = false;

            Rectangle r = new Rectangle();
            r.Width = ((FrameworkElement)dragElement).ActualWidth;
            r.Height = ((FrameworkElement)dragElement).ActualHeight;
            r.Fill = new VisualBrush(dragElement);
            this.DragDropPreview.Content = r;


            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);


            this.DragDropPreview.Left = w32Mouse.X;
            this.DragDropPreview.Top = w32Mouse.Y;
            this.DragDropPreview.Show();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point {
            public Int32 X;
            public Int32 Y;
        };

        private void Self_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (AllowDrag) {

                // create the visual feedback drag and drop item
                CreateDragDropWindow(this);
                this.RenderTransform = new RotateTransform(2, 300, 200);
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
                DragDropPreview.Close();
                DragDropPreview = null;
                this.RenderTransform = null;
            }
        }
    }
}
