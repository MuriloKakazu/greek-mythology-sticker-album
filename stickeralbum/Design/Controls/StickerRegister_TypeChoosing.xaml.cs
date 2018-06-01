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
    /// Interação lógica para StrickerRegister_TypeChoosing.xam
    /// </summary>
    public partial class StickerRegister_TypeChoosing : UserControl {
        public StickerRegister_TypeChoosing() {
            InitializeComponent();
            ComboBoxType.ItemsSource = new Generics.LinkedList<String>() {
                "Criatura",
                "Semideus",
                "Deus",
                "Titã"
            };
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            switch(ComboBoxType.SelectedItem.ToString()) {
                case "Deus":
                    App.ClientWindow.SetCurrentPage(new StickerRegister_God());
                    return;
                case "Semideus":
                    App.ClientWindow.SetCurrentPage(new StickerRegister_Semigod());
                    return;
                case "Criatura":
                    App.ClientWindow.SetCurrentPage(new StickerRegister_Creature());
                    return;
                case "Titã":
                    App.ClientWindow.SetCurrentPage(new StickerRegister_Titan());
                    return;
            }
        }
    }
}
