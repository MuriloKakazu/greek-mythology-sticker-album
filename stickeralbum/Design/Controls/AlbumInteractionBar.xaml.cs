using stickeralbum.Debug;
using stickeralbum.Enums;
using stickeralbum.Game;
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

namespace stickeralbum.Design.Controls
{
    /// <summary>
    /// Interaction logic for AlbumInteractionBar.xaml
    /// </summary>
    public partial class AlbumInteractionBar : UserControl
    {
        public event          FilterChangedEventHandler FilterChanged;
        public delegate void  FilterChangedEventHandler(object sender, EventArgs e);
        public FilterSettings FilterSettings;

        public AlbumInteractionBar() {
            InitializeComponent();
            FilterSettings = new FilterSettings();
            FilterChanged += new FilterChangedEventHandler(OnFilterChanged);
            SetDefaultFilterSettings();
            ResetAllCheckboxes();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                try {
                    SearchIcon.Source = Sprite.Get(IconType.Search, IconColor.White).Source;
                } catch (Exception e) {
                    DebugUtils.LogError(e.Message);
                }
            }
        }

        private void ResetAllCheckboxes() {
            FilterCustom.IsChecked   = 
            FilterTitan.IsChecked    =
            FilterGod.IsChecked      =
            FilterSemiGod.IsChecked  =
            FilterCreature.IsChecked =
            FilterMale.IsChecked     =
            FilterFemale.IsChecked   = true;
        }

        private void SetDefaultFilterSettings() 
            => FilterSettings.Clear()
              .Add("custom",   true)
              .Add("titan",    true)
              .Add("god",      true)
              .Add("semigod",  true)
              .Add("creature", true)
              .Add("male",     true)
              .Add("female",   true);

        public virtual void OnFilterChanged(object sender, EventArgs e) { DebugUtils.Log("Filter Changed!"); }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) {
            FilterSettings.Query = (SearchBox.Text.Length > 0) ? SearchBox.Text : null;
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterCustom_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("custom", (Boolean)FilterCustom.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterCustom_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("custom", (Boolean)FilterCustom.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterTitan_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("titan", (Boolean)FilterTitan.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterTitan_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("titan", (Boolean)FilterTitan.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterGod_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("god", (Boolean)FilterGod.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterGod_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("god", (Boolean)FilterGod.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterSemiGod_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("semigod", (Boolean)FilterSemiGod.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterSemiGod_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("semigod", (Boolean)FilterSemiGod.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterCreature_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("creature", (Boolean)FilterCreature.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterCreature_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("creature", (Boolean)FilterCreature.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterMale_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("male", (Boolean)FilterMale.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterMale_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("male", (Boolean)FilterMale.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterFemale_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("female", (Boolean)FilterFemale.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }

        private void FilterFemale_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("female", (Boolean)FilterFemale.IsChecked);
            FilterChanged.Invoke(this, EventArgs.Empty);
        }
    }
}
