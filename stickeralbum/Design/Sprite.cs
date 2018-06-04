using Newtonsoft.Json;
using stickeralbum.Debug;
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
            try {
                Source = new BitmapImage(new Uri(Path)) {
                    CacheOption = BitmapCacheOption.OnLoad
                };
                DebugUtils.LogIO($"Loaded file from <{ID}>");
            } catch (Exception e) {
                DebugUtils.LogError($"Error loading file from <{ID}>. Reason => {e.Message}");
            }
            if (!IsCustom && Source.Width <= 512 && Source.Height <= 512) {
                try {
                    DarkenedSource = Source.GetAllBlack();
                    DebugUtils.LogIO($"Loaded shadow for sprite <{ID}>");
                } catch (Exception e) {
                    DebugUtils.LogError($"Error loading shadow for sprite <{ID}>. Reason => {e.Message}");
                }
            } else {
                DebugUtils.LogIO($"Won't load shadow for sprite <{ID}>. The sprite is either custom or too big");
            }
        }

        public static Sprite Get(String ID)
            => Cache.Get(ID) as Sprite;

        public static Sprite Get(Rarity rarity)
            => Get($"frame_{rarity.ToString().ToLower()}");

        public static Sprite Get(IconType type)
            => Get($"icon_{type.ToString().ToLower()}");

        public override String ToString() 
            => this.Path;

        public static LinkedList<Sprite> GetAll()
            => Cache.GetAll()
              .Where(x => x is Sprite)
              .Cast<Sprite>()
              .ToLinkedList();
    }
}
