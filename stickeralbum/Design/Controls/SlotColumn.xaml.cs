using stickeralbum.Audio;
using stickeralbum.Debug;
using stickeralbum.Game;
using stickeralbum.Generics;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace stickeralbum.Design.Controls {
    /// <summary>
    /// Interaction logic for SlotColumn.xaml
    /// </summary>
    public partial class SlotColumn : System.Windows.Controls.UserControl {
        LinkedList<Sprite> AvailableSprites;
        Int32 CurrentIndex;
        Timer StopTimer;
        Timer SlideTimer;
        Sprite Row0Sprite;
        Sprite Row1Sprite;
        Sprite Row2Sprite;
        public event FinishedSpinEventHandler FinishedSpin;
        public delegate void FinishedSpinEventHandler(object sender, EventArgs e);
        static Random RNG = new Random();

        public SlotColumn() {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                SetAntiAliasing();
                AvailableSprites = Sprite.GetAll().Where(x => x.ID.StartsWith("spin_")).ToLinkedList();
                CurrentIndex = RNG.Next(AvailableSprites.Count);
                StopTimer = new Timer();
                SlideTimer = new Timer();
                SlideTimer.Interval = 25;
                StopTimer.Tick += StopTimer_Elapsed; ;
                SlideTimer.Tick += SlideTimer_Elapsed;
                FinishedSpin += OnFinishedSpin;
                Slide();
            }
        }

        public void SetAntiAliasing() {
            if (GameMaster.Settings.AntiAliasing) {
                RenderOptions.SetBitmapScalingMode(Row0, BitmapScalingMode.Fant);
                RenderOptions.SetClearTypeHint(Row0, ClearTypeHint.Enabled);
                RenderOptions.SetEdgeMode(Row0, EdgeMode.Aliased);
                RenderOptions.SetBitmapScalingMode(Row1, BitmapScalingMode.Fant);
                RenderOptions.SetClearTypeHint(Row1, ClearTypeHint.Enabled);
                RenderOptions.SetEdgeMode(Row1, EdgeMode.Aliased);
                RenderOptions.SetBitmapScalingMode(Row2, BitmapScalingMode.Fant);
                RenderOptions.SetClearTypeHint(Row2, ClearTypeHint.Enabled);
                RenderOptions.SetEdgeMode(Row2, EdgeMode.Aliased);
            } else {
                RenderOptions.SetBitmapScalingMode(Row0, BitmapScalingMode.LowQuality);
                RenderOptions.SetClearTypeHint(Row0, ClearTypeHint.Auto);
                RenderOptions.SetEdgeMode(Row0, EdgeMode.Unspecified);
                RenderOptions.SetBitmapScalingMode(Row1, BitmapScalingMode.LowQuality);
                RenderOptions.SetClearTypeHint(Row1, ClearTypeHint.Auto);
                RenderOptions.SetEdgeMode(Row1, EdgeMode.Unspecified);
                RenderOptions.SetBitmapScalingMode(Row2, BitmapScalingMode.LowQuality);
                RenderOptions.SetClearTypeHint(Row2, ClearTypeHint.Auto);
                RenderOptions.SetEdgeMode(Row2, EdgeMode.Unspecified);
            }
        }

        private void OnFinishedSpin(object sender, EventArgs e) {
            StopTimer.Stop();
            SlideTimer.Stop();
        }

        private void SlideTimer_Elapsed(object sender, EventArgs e) {
            Slide();
        }

        public void Spin(Int32 timeMs) {
            StopTimer.Interval = timeMs;
            SlideTimer.Interval = 25 + RNG.Next(10);
            SlideTimer.Start();
            StopTimer.Start();
        }

        public void Slide() {
            CurrentIndex++;
            var spritesCount = AvailableSprites.Count;
            if (CurrentIndex >= spritesCount) {
                CurrentIndex = 0;
            }
            Row0Sprite = AvailableSprites[CurrentIndex];
            Row1Sprite = AvailableSprites[
                (CurrentIndex >= spritesCount - 1) ? 0 : CurrentIndex + 1 
            ];
            Row2Sprite = AvailableSprites[
                (CurrentIndex >= spritesCount - 1) ? 1 : 
                (CurrentIndex >= spritesCount - 2) ? 0 : CurrentIndex + 2
            ];
            Row0.Source = Row0Sprite.Source;
            Row1.Source = Row1Sprite.Source;
            Row2.Source = Row2Sprite.Source;
        }

        public Sprite GetResult()
            => Row1Sprite;

        private void StopTimer_Elapsed(object sender, EventArgs e) {
            FinishedSpin.Invoke(this, EventArgs.Empty);
        }
    }
}
