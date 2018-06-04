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
using stickeralbum.Debug;
using stickeralbum.Entities;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interação lógica para StickerDetails.xam
    /// </summary>
    public partial class StickerDetails : UserControl {

        Entity TargetEntity;
        
        public StickerDetails(Entity e) {
            InitializeComponent();
            TargetEntity = e;
            Setup();
        }

        private void Setup() {
            LabelName.Content         = TargetEntity.Name;
            TextBlockDescription.Text = TargetEntity.Description;
            if(TargetEntity is God || TargetEntity is Titan) {
                if (TargetEntity.Father != null) {
                    var father = new Sticker(TargetEntity.Father);
                    father.ShowDescriptionOnDoubleClick = true;
                    StackPanelParents.Children.Add(father);
                }
                if (TargetEntity.Mother != null) {
                    var mother = new Sticker(TargetEntity.Mother);
                    mother.ShowDescriptionOnDoubleClick = true;
                    StackPanelParents.Children.Add(mother);
                }
            } else if(TargetEntity is SemiGod) {
                if((TargetEntity as SemiGod).RelatedGod != null) StackPanelParents.Children.Add(new Sticker((TargetEntity as SemiGod).RelatedGod));
            }
            if (TargetEntity.Father == null && TargetEntity.Mother == null) {
                ParentsLabel.Visibility = Visibility.Hidden;
            }
            DebugUtils.Log("TargetEntity => " + TargetEntity.ID);
            SelectedStickerPanel.Children.Add(new Sticker(TargetEntity));
        }
    }
}
