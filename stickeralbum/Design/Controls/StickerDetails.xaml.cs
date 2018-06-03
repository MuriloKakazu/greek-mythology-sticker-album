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
using stickeralbum.Entities;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interação lógica para StickerDetails.xam
    /// </summary>
    public partial class StickerDetails : UserControl {

        
        public StickerDetails(Sticker _sticker) {
            InitializeComponent();
            StickerSelected = _sticker;
            Setup();

        }

        private void Setup() {
            LabelName.Content = StickerSelected.StickerName.Content;
            TextBlockDescription.Text = StickerSelected.Entity.Description;
            Entity stickerEntity = StickerSelected.Entity;
            if(stickerEntity is God || stickerEntity is Titan) {
                if(stickerEntity.Father != null) StackPanelParents.Children.Add(new Sticker(stickerEntity.Father));
                if(stickerEntity.Mother != null) StackPanelParents.Children.Add(new Sticker(stickerEntity.Mother));
            } else if(stickerEntity is SemiGod) {
                if((stickerEntity as SemiGod).RelatedGod != null) StackPanelParents.Children.Add(new Sticker((stickerEntity as SemiGod).RelatedGod));
            }
        }
    }
}
