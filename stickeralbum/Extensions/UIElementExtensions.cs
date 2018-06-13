using stickeralbum.Design.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace stickeralbum.Extensions {
    public static class UIElementExtensions {
        public static void DetachParent(this UIElement uiElement) {
            var parent = VisualTreeHelper.GetParent(uiElement);
            var parentAsPanel = parent as Panel;
            if (parentAsPanel != null) {
                parentAsPanel.Children.Remove(uiElement);
            }
            var parentAsContentControl = parent as ContentControl;
            if (parentAsContentControl != null) {
                parentAsContentControl.Content = null;
            }
            var parentAsDecorator = parent as Decorator;
            if (parentAsDecorator != null) {
                parentAsDecorator.Child = null;
            }
            var parentAsAlbumPage = parent as AlbumPage;
            if (parentAsAlbumPage != null) {
                parentAsAlbumPage.Clear();
            }
        }
    }
}
