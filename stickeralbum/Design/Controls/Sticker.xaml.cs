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
            this.InitializeComponent();
            this.DataDropped += this.OnDataDropped;
        }

        public Sticker(Entity e, Boolean overrideValidation = false) :
            this() => this.SetEntity(e, overrideValidation);

        public void SetEntity(Entity e, Boolean overrideValidation = false) {
            this.Entity = e;

            if (this.Entity.IsUnlocked || overrideValidation) {
                //this.Background = new ImageBrush(Entity.Background.Source);
                this.StickerFrame.Source = Sprite.Get(e.Rarity).Source;
                this.StickerImage.Source = e.Sprite.Source;
                this.StickerName.Content = e.Name;
                this.StickerName.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            } else {
                this.MakeSecret();
            }
            if (this.Entity.IsCustom) {
                this.StickerImage.Stretch = Stretch.Fill;
                this.StickerImage.StretchDirection = StretchDirection.Both;
                this.StickerImage.Width  = this.StickerFrame.ActualWidth;
                this.StickerImage.Height = this.StickerFrame.ActualHeight;
            }
        }

        public virtual void OnDataDropped(object sender, EventArgs e) { }

        public void Refresh() {
            this.StickerImage.Width  = this.StickerFrame.ActualWidth;
            this.StickerImage.Height = this.StickerFrame.ActualHeight;
            this.SpaceFiller.Height  = Math.Round((this.ActualHeight - this.StickerFrame.ActualHeight) / 2, 2);
            this.SpaceFiller.Width   = this.ActualWidth;
            this.StickerName.Margin  = new Thickness(0, SpaceFiller.Height, 0, 0);
            var targetFontSize  = 11f / 200f * (this.ActualHeight - this.SpaceFiller.Height);
            if (targetFontSize > 0) {
                this.StickerName.FontSize = targetFontSize;
                this.StickerName.FontWeight = FontWeights.Normal;
            }
        }

        public void MakeSecret() {
            this.StickerFrame.Source = Sprite.Get(Rarity.Unknown).Source;
            //this.StickerImage.Source = Sprite.Get("unknown").Source;
            this.StickerImage.Source = (this.Entity.IsCustom) ? Sprite.Get("unknown").Source : this.Entity.Sprite.DarkenedSource;
            this.StickerName.Content = this.Entity.Name;
            //this.StickerName.Content = String.Concat(Enumerable.Repeat("?", Entity.Name.Length));
            this.StickerName.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void Self_SizeChanged(object sender, SizeChangedEventArgs e) {
            this.Refresh();
        }

        private void StickerFrame_MouseEnter(object sender, MouseEventArgs e) {
            this.StickerImage.Width  = StickerFrame.ActualWidth  - 20;
            this.StickerImage.Height = StickerFrame.ActualHeight - 30;
            this.StickerName.FontWeight = FontWeights.Bold;
        }

        private void StickerFrame_MouseLeave(object sender, MouseEventArgs e) {
            this.Refresh();
        }

        private void Self_DragEnter(object sender, DragEventArgs e)
            => this.RenderTransform = new RotateTransform(2, 300, 200);

        private void Self_DragLeave(object sender, DragEventArgs e) 
            => this.RenderTransform = null;

        private void Self_DragOver(object sender, DragEventArgs e) {

        }

        private void Self_Drop(object sender, DragEventArgs e) {
            this.RenderTransform = null;
            var formats = e.Data.GetFormats();
            if (formats.Length > 0) {
                var data = e.Data.GetData(e.Data.GetFormats()[0]);
                if (this.AllowDrop && data is Sticker) {
                    var droppedSticker = data as Sticker;
                    var droppedEntity  = droppedSticker.Entity;
                    if (this.Entity.ID == droppedEntity.ID && !Entity.IsUnlocked) {
                        DebugUtils.Log($"Valid Entity Dropped => {droppedEntity.ID}");
                        GameMaster.Player.Inventory.Remove(droppedEntity.ID, 1);
                        GameMaster.Player.Unlock(droppedEntity.ID);
                        this.SetEntity(Entity);
                        droppedSticker.DataDropped.Invoke(droppedSticker, EventArgs.Empty);
                    }
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
            this.DragDropPreview.Top  = w32Mouse.Y;
        }

        private void CreateDragDropPreview(Visual dragElement) {
            this.DragDropPreview = new Window() {
                WindowStyle        = WindowStyle.None,
                AllowsTransparency = true,
                AllowDrop          = false,
                Background         = null,
                IsHitTestVisible   = false,
                SizeToContent      = SizeToContent.WidthAndHeight,
                Topmost            = true,
                ShowInTaskbar      = false,
                Opacity            = 0.75f
            };

            Rectangle rect = new Rectangle() {
                Width  = ((FrameworkElement)dragElement).ActualWidth,
                Height = ((FrameworkElement)dragElement).ActualHeight,
                Fill   = new VisualBrush(dragElement)
            };
            this.DragDropPreview.Content = rect;

            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            this.DragDropPreview.Left = w32Mouse.X;
            this.DragDropPreview.Top  = w32Mouse.Y;
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
            if (this.AllowDrag) {
                this.CreateDragDropPreview(this);
                this.RenderTransform = new RotateTransform(2, 300, 200);
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
                this.DragDropPreview.Close();
                this.DragDropPreview = null;
                this.RenderTransform = null;
            }
        }
    }
}
