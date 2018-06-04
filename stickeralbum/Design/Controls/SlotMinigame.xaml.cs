using stickeralbum.Audio;
using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Game;
using stickeralbum.Game.Items;
using stickeralbum.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Boolean IsSpinning;

        public SlotMinigame() {
            InitializeComponent();
            Spins = 0;
            Slot0.FinishedSpin += Slot_FinishedSpin;
            Slot1.FinishedSpin += Slot_FinishedSpin;
            Slot2.FinishedSpin += Slot_FinishedSpin;
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                this.SpinButton.Source = Sprite.Get("redbutton_released").Source;
                this.CoinIcon.Source = Sprite.Get("coin").Source;
                Refresh();
            }

            if (!SoundTrack.Get("st_main").IsPlaying) {
                SoundPlayer.StopAll("st_blacksmith");
                SoundPlayer.Instance.Play(SoundTrack.Get("st_main"), loop: true);
            }
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
                    if (dice < 65) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.VeryRare).Random();
                    } else if (dice < 80) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Epic).Random();
                    } else if (dice <= 95) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Legendary).Random();
                    }
                } else if (res0 == res1 || res1 == res2 || res0 == res2) {
                    if (dice < 30) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Uncommon).Random();
                    } else if (dice < 60) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Rare).Random();
                    } else if (dice < 90) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.VeryRare).Random();
                    } else if (dice <= 100) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Epic).Random();
                    }
                } else {
                    if (dice < 50) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Common).Random();
                    } else if (dice < 70) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Uncommon).Random();
                    } else if (dice < 95) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Rare).Random();
                    } else if (dice < 99) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.VeryRare).Random();
                    } else if (dice <= 100) {
                        prize = Entity.GetAll().Where(x => x.Rarity == Rarity.Epic).Random();
                    }
                }
                var prizeSticker = new Sticker(prize, overrideValidation: true);
                prizeSticker.Width  = 150;
                prizeSticker.Height = 225;
                LastPrizePanel.Children.Clear();
                LastPrizePanel.Children.Add(prizeSticker);
                GameMaster.Player.Inventory.Add(new SimpleSticker() { ItemID = prize.ID });
                DebugUtils.Log($"Minigame prize => <{prize.ID}> => {prize.Rarity}!");
                SoundPlayer.Instance.Play(SoundTrack.Get("sfx_coins"));
                IsSpinning = false;
            }
        }

        public void Spin() {
            Spins = 0;
            var t = 1000;
            Slot0.Spin(t);
            Slot1.Spin(t * 2);
            Slot2.Spin(t * 3);
            SoundPlayer.Instance.Play(SoundTrack.Get("sfx_slot" + RNG.Next(0, 3)));
        }

        private void JackpotMachine_SizeChanged(object sender, SizeChangedEventArgs e) {

        }

        public void Refresh() {
            CoinsCount.Content = GameMaster.Player.Coins;
        }

        private void SpinButton_MouseDown(object sender, MouseButtonEventArgs e) {
            this.SpinButton.Source = Sprite.Get("redbutton_pressed").Source;
            var player = GameMaster.Player;
            if (!IsSpinning && player.Coins > 0) {
                IsSpinning = true;
                player.Coins -= 1;
                Refresh();
                Spin();
            }
        }

        private void SpinButton_MouseUp(object sender, MouseButtonEventArgs e) {
            this.SpinButton.Source = Sprite.Get("redbutton_released").Source;
        }
    }
}
