using Newtonsoft.Json;
using stickeralbum.Enums;
using stickeralbum.Extensions;
using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace stickeralbum.Design {
    public class Sprite : Cacheable {
        public String Path;

        [JsonIgnore]
        public BitmapSource Source;
        [JsonIgnore]
        public BitmapSource DarkenedSource;

        [JsonIgnore]
        public Boolean IsCustom;

        public void LoadImage() {
            Source = new BitmapImage(new Uri(Path));
            if (!IsCustom && Source.Width <= 512 && Source.Height <= 512) {
                DarkenedSource = Source.GetAllBlack();
            }
        }

        public static Sprite Get(String ID)
            => Cache.Get(ID) as Sprite;

        public static Sprite Get(Rarity rarity)
            => Get($"frame_{rarity.ToString().ToLower()}");

        public static Sprite Get(IconType type, IconColor color)
            => Get($"icon_{type.ToString().ToLower()}_{color.ToString().ToLower()}");

        public override String ToString() 
            => this.Path;

        public static LinkedList<Sprite> GetAll()
            => Cache.GetAll()
              .Where(x => x is Sprite)
              .Cast<Sprite>()
              .ToLinkedList();
    }
}
