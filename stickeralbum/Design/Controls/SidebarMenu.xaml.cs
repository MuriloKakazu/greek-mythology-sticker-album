using stickeralbum.Audio;
using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Enums;
using stickeralbum.Game;
using stickeralbum.Generics;
using stickeralbum.IO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for SidebarMenu.xaml
    /// </summary>
    public partial class SidebarMenu : UserControl {
        public ControlState State { get; protected set; }

        public SidebarMenu() 
            => InitializeComponent();

        public void ChangeState(ControlState newState) {
            State = newState;
            if (State == ControlState.Compact) {
                AnimateCompact();
            } else if (State == ControlState.Expanded) {
                AnimateExpand();
            }
        }

        private void AnimateExpand() 
            => BeginStoryboard(FindResource("Expand") as Storyboard);

        private void AnimateCompact() 
            => BeginStoryboard(FindResource("Compact") as Storyboard);

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            ChangeState(ControlState.Compact);
            HomeButton.SetText("Home");
            QuitButton.SetText("Sair");
            AlbumButton.SetText("Álbum");
            MinigameButton.SetText("Minigame");
            SettingsButton.SetText("Configurações");
            //ShopButton.SetText("Loja de Figurinhas");
            NewStickerButton.SetText("Forja de Figurinhas");
            SaveButton.SetText("Salvar");
            GiftButton.SetText("Recompensa Diária!");

            if (DesignerProperties.GetIsInDesignMode(this)) return;
            QuitButton.SetIcon(Sprite.Get(IconType.Exit));
            HomeButton.SetIcon(Sprite.Get(IconType.Home));
            MinigameButton.SetIcon(Sprite.Get(IconType.Gamepad));
            SettingsButton.SetIcon(Sprite.Get(IconType.Settings));
            NewStickerButton.SetIcon(Sprite.Get(IconType.AddItem));
            //ShopButton.SetIcon(Sprite.Get(IconType.ShoppingBasket, IconColor.White));
            AlbumButton.SetIcon(Sprite.Get(IconType.StickerCollection));
            SaveButton.SetIcon(Sprite.Get(IconType.Save));
            GiftButton.SetIcon(Sprite.Get(IconType.NewWarning));

            if (DateTime.Today > GameMaster.Player.LastGift) {
                GiftButton.Visibility = Visibility.Visible;
            } else {
                GiftButton.Visibility = Visibility.Hidden;
            }
        }

        private void Expander_MouseDown(object sender, MouseButtonEventArgs e) {
            if (State == ControlState.Compact)
                ChangeState(ControlState.Expanded);
            else if (State == ControlState.Expanded)
                ChangeState(ControlState.Compact);
        }

        private void QuitButton_MouseDown(object sender, MouseButtonEventArgs e) {
            App.Current.Shutdown(0);
        }

        private void Self_MouseLeave(object sender, MouseEventArgs e) 
            => ChangeState(ControlState.Compact);

        private void Self_MouseEnter(object sender, MouseEventArgs e) 
            => ChangeState(ControlState.Expanded);

        private void AlbumButton_MouseDown(object sender, MouseButtonEventArgs e) {
            ChangeState(ControlState.Compact);
            App.ClientWindow.SetCurrentPage(new Album(Entity.GetAll().ToArray()));
        }

        private void NewStickerButton_MouseDown(object sender, MouseButtonEventArgs e) {
            ChangeState(ControlState.Compact);
            App.ClientWindow.SetCurrentPage(new StickerRegister_TypeChoosing());
        }

        private void MinigameButton_MouseDown(object sender, MouseButtonEventArgs e) {
            ChangeState(ControlState.Compact);
            App.ClientWindow.SetCurrentPage(new SlotMinigame());
        }

        private void OptionsButton_MouseDown(object sender, MouseButtonEventArgs e) {
            ChangeState(ControlState.Compact);
            App.ClientWindow.SetCurrentPage(new Configs());
        }

        private void HomeButton_MouseDown(object sender, MouseButtonEventArgs e) {
            ChangeState(ControlState.Compact);
            App.ClientWindow.SetCurrentPage(new Homepage());
        }

        private void SaveButton_MouseDown(object sender, MouseButtonEventArgs e) {
            ChangeState(ControlState.Compact);
            GameMaster.SaveAll();
            DebugUtils.FlushBuffer();
        }

        private void GiftButton_MouseDown(object sender, MouseButtonEventArgs e) {
            if (DateTime.Today > GameMaster.Player.LastGift) {
                ChangeState(ControlState.Compact);
                GiftButton.Visibility = Visibility.Hidden;
                GameMaster.Player.LastGift = DateTime.Today;
                GameMaster.Player.Coins += 10;
                SoundPlayer.Instance.Play(SoundTrack.Get("sfx_coins"));
            }
        }
    }
}
