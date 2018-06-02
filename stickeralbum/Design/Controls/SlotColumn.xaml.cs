using stickeralbum.Generics;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for SlotColumn.xaml
    /// </summary>
    public partial class SlotColumn : UserControl {
        LinkedList<Sprite> AvailableSprites;
        Int32 CurrentIndex;
        Timer StopTimer;
        Timer SlideTimer;
        Sprite Row0Sprite;
        Sprite Row1Sprite;
        Sprite Row2Sprite;
        public event SlideFinishedEventHandler SlideFinished;
        public delegate void SlideFinishedEventHandler(object sender, EventArgs e);
        static Random RNG = new Random();

        public SlotColumn() {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                AvailableSprites = Sprite.GetAll().Where(x => x.ID.StartsWith("spin_")).ToLinkedList();
                CurrentIndex = RNG.Next(AvailableSprites.Count);
                StopTimer = new Timer();
                SlideTimer = new Timer();
                SlideTimer.Interval = 50;
                StopTimer.Elapsed += StopTimer_Elapsed;
                SlideTimer.Elapsed += SlideTimer_Elapsed;
                SlideFinished += OnSlideFinished;
                Slide();
            }
        }

        private void OnSlideFinished(object sender, EventArgs e) {
            StopTimer.Stop();
            SlideTimer.Stop();
        }

        private void SlideTimer_Elapsed(object sender, ElapsedEventArgs e) {
            Slide();
        }

        public void Spin(Double timeMs) {
            StopTimer.Interval = timeMs;
            SlideTimer.Start();
            StopTimer.Start();
        }

        private void Slide() {
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

        private void StopTimer_Elapsed(object sender, ElapsedEventArgs e) {
            SlideFinished.Invoke(this, EventArgs.Empty);
        }
    }
}
