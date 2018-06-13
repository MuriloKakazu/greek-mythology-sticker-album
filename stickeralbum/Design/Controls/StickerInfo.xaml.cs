using stickeralbum.Entities;
using stickeralbum.Enums;
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
    /// Interaction logic for StickerInfo.xaml
    /// </summary>
    public partial class StickerInfo : UserControl
    {
        Entity TargetEntity;

        public StickerInfo(Entity e)
        {
            InitializeComponent();
            TargetEntity = e;
            if (e != null) {
                Setup();
            }
        }

        private void Setup() {
            Sticker targetSticker = new Sticker(TargetEntity, overrideValidation: true) {
                Width = 200, Height = 300
            };
            TargetStickerContainer.Children.Add(targetSticker);
            DescriptionContainer.Text = TargetEntity.Description;

            if (TargetEntity.Mother != null) {
                var mother = new Sticker(TargetEntity.Mother) {
                    ShowDescriptionOnDoubleClick = true,
                    Width = 150, Height = 225
                };
                MotherContainer.Children.Add(mother);
            } else {
                MotherLabel.Visibility = Visibility.Hidden;
            }

            if (TargetEntity.Father != null) {
                var father = new Sticker(TargetEntity.Father) {
                    ShowDescriptionOnDoubleClick = true,
                    Width = 150, Height = 225
                };
                FatherContainer.Children.Add(father);
            } else {
                FatherLabel.Visibility = Visibility.Hidden;
            }

            Dictionary<Type, String> TypeToTranslation = new Dictionary<Type, String>{
                { typeof(Entity),   "Entidade" },
                { typeof(Chaos),    "Caos" },
                { typeof(Titan),    "Titã" },
                { typeof(God),      "Deus" },
                { typeof(SemiGod),  "SemiDeus" },
                { typeof(Creature), "Criatura" }
            };

            Dictionary<Rarity, String> RarityToTranslation = new Dictionary<Rarity, String>{
                { Rarity.Common,    "Comum" },
                { Rarity.Uncommon,  "Incomum" },
                { Rarity.Rare,      "Raro" },
                { Rarity.VeryRare,  "Muito Raro" },
                { Rarity.Epic,      "Épico" },
                { Rarity.Legendary, "Lendário" },
                { Rarity.Unknown,   "Desconhecido" }
            };

            String moreInfo = "";
            TypeToTranslation.TryGetValue(TargetEntity.GetType(), out String type);
            RarityToTranslation.TryGetValue(TargetEntity.Rarity,  out String rarity);
            moreInfo += "Tipo: "     + type   + Environment.NewLine;
            moreInfo += "Raridade: " + rarity + Environment.NewLine;
            if (TargetEntity is Chaos) {
                moreInfo += "Entidade Primordial" + Environment.NewLine;
            } else if (TargetEntity is Titan) {
                moreInfo += "Personificação: "   + (TargetEntity as Titan).Personification   + Environment.NewLine;
            } else if (TargetEntity is God) {
                moreInfo += "Personificação: "   + (TargetEntity as God).Personification     + Environment.NewLine;
            } else if (TargetEntity is SemiGod) {
                moreInfo += "Deus Relacionado: " + (TargetEntity as SemiGod).RelatedGod.Name + Environment.NewLine;
            } else if (TargetEntity is Creature) {
                moreInfo += "Nível de Perigo: "  + (TargetEntity as Creature).DangerLevel    + Environment.NewLine;
            }
            OtherInfoContainer.Children.Add(new Label() {
                Content    = moreInfo,
                FontSize   = 18f,
                FontWeight = FontWeights.Bold,
                FontFamily = (FontFamily) new FontFamilyConverter().ConvertFromString("Diogenes")
            });
        }
    }
}
