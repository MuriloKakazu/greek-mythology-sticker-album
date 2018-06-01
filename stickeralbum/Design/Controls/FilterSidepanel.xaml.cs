using stickeralbum.Debug;
using stickeralbum.Enums;
using stickeralbum.Game;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for FilterSidepanel.xaml
    /// </summary>
    public partial class FilterSidepanel : UserControl {
        public event FilterChangedEventHandler FilterChanged;
        public delegate void FilterChangedEventHandler(object sender, EventArgs e);
        public FilterSettings FilterSettings;

        public FilterSidepanel() {
            FilterSettings = new FilterSettings();
            InitializeComponent();
            SetDefaultFilterSettings();
            ResetAllCheckboxes();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                try {
                    FilterChanged += new FilterChangedEventHandler(OnFilterChanged);
                    SearchIcon.Source = Sprite.Get(IconType.Search, IconColor.White).Source;
                } catch (Exception e) {
                    DebugUtils.LogError(e.Message);
                }
            }
        }

        private void ResetAllCheckboxes() 
            => FilterTitan.IsChecked    =
               FilterGod.IsChecked      =
               FilterSemiGod.IsChecked  =
               FilterCreature.IsChecked =
               FilterCommon.IsChecked   =
               FilterUncommon.IsChecked =
               FilterRare.IsChecked     =
               FilterVeryRare.IsChecked =
               FilterEpic.IsChecked     =
               FilterMale.IsChecked     =
               FilterFemale.IsChecked   = 
               FilterCustom.IsChecked   = true;

        private void SetDefaultFilterSettings()
            => FilterSettings.Clear()
              .Add("titan",     true)
              .Add("god",       true)
              .Add("semigod",   true)
              .Add("creature",  true)
              .Add("common",    true)
              .Add("uncommon",  true)
              .Add("rare",      true)
              .Add("veryrare",  true)
              .Add("epic",      true)
              .Add("male",      true)
              .Add("female",    true)
              .Add("custom",    true);

        public virtual void OnFilterChanged(object sender, EventArgs e) { DebugUtils.Log("Filter Changed!"); }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) {
            FilterSettings.Query = (SearchBox.Text.Length > 0) ? SearchBox.Text : null;
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterTitan_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("titan", (Boolean)FilterTitan.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterGod_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("god", (Boolean)FilterGod.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterSemiGod_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("semigod", (Boolean)FilterSemiGod.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterCreature_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("creature", (Boolean)FilterCreature.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterCommon_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("common", (Boolean)FilterCommon.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterUncommon_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("uncommon", (Boolean)FilterUncommon.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterRare_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("rare", (Boolean)FilterRare.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterVeryRare_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("veryrare", (Boolean)FilterVeryRare.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterEpic_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("epic", (Boolean)FilterEpic.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterMale_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("male", (Boolean)FilterMale.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterFemale_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("female", (Boolean)FilterFemale.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterCustom_Checked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("custom", (Boolean)FilterCustom.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        //

        private void FilterTitan_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("titan", (Boolean)FilterTitan.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterGod_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("god", (Boolean)FilterGod.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterSemiGod_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("semigod", (Boolean)FilterSemiGod.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterCreature_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("creature", (Boolean)FilterCreature.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterCommon_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("common", (Boolean)FilterCommon.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterUncommon_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("uncommon", (Boolean)FilterUncommon.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterRare_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("rare", (Boolean)FilterRare.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterVeryRare_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("veryrare", (Boolean)FilterVeryRare.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterEpic_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("epic", (Boolean)FilterEpic.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterMale_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("male", (Boolean)FilterMale.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterFemale_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("female", (Boolean)FilterFemale.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FilterCustom_Unchecked(object sender, RoutedEventArgs e) {
            FilterSettings.SetKey("custom", (Boolean)FilterCustom.IsChecked);
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
