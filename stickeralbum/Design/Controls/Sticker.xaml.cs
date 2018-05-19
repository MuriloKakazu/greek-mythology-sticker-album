using stickeralbum.Entities;
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

        public Sticker() => InitializeComponent();

        public void SetEntity(Entity entity) {
            this.Entity                 = entity;
            this.Image.Source           = entity.Bitmap.Source;
            this.Title.Content          = entity.Name;
            this.Description.Content    = entity.Description;
        }
    }
}
