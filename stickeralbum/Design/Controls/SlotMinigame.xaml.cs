using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Game;
using stickeralbum.Game.Items;
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
    /// Interaction logic for SlotMinigame.xaml
    /// </summary>
    public partial class SlotMinigame : UserControl {
        Random RNG = new Random();
        Int32 Spins;
        public SlotMinigame() {
            InitializeComponent();
            Spins = 0;
            Spin();
            Slot0.FinishedSpin += Slot_FinishedSpin;
            Slot1.FinishedSpin += Slot_FinishedSpin;
            Slot2.FinishedSpin += Slot_FinishedSpin;
        }

        private void Slot_FinishedSpin(object sender, EventArgs e) {
            Spins++;
            if (Spins == 3) {
                Int32 dice;
                Entity prize = null;
                var res0 = Slot0.GetResult();
                var res1 = Slot1.GetResult();
                var res2 = Slot2.GetResult();
                dice = RNG.Next(100);
                if (res0 == res1 && res0 == res2) {
                    if (dice < 30) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Rare).Random();
                    } else if (dice < 65) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.VeryRare).Random();
                    } else if (dice <= 100) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Epic).Random();
                    }
                } else if (res0 == res1 || res1 == res2 || res0 == res2) {
                    if (dice < 25) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Uncommon).Random();
                    } else if (dice < 50) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Rare).Random();
                    } else if (dice < 80) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.VeryRare).Random();
                    } else if (dice <= 100) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Epic).Random();
                    }
                } else {
                    if (dice < 25) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Common).Random();
                    } else if (dice < 50) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Uncommon).Random();
                    } else if (dice < 80) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Rare).Random();
                    } else if (dice < 90) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.VeryRare).Random();
                    } else if (dice <= 100) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Epic).Random();
                    }
                }
                GameMaster.Player.Inventory.Add(new SimpleSticker() { ItemID = prize.ID });
                DebugUtils.Log($"Prize => {prize.ID} - {prize.Rarity}!");
            }
        }

        public void Spin() {
            Spins = 0;
            var t = RNG.Next(1, 5) * 1000;
            Slot0.Spin(t);
            Slot1.Spin(t + 2 * 1000);
            Slot2.Spin(t + 4 * 1000);
        }

        private void JackpotMachine_SizeChanged(object sender, SizeChangedEventArgs e) {

        }
    }
}
