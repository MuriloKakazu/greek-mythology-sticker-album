using stickeralbum.IO;
using System;

namespace stickeralbum.Entities {
    public class Titan : Entity {
        public static new Titan Get(String ID)
            => Cache.Get(ID) as Titan;
    }
}
