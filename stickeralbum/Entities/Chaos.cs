using stickeralbum.IO;

namespace stickeralbum.Entities {
    public sealed class Chaos : Entity {
        public static Chaos Get()
            => Cache.Get("chaos") as Chaos;
    }
}
