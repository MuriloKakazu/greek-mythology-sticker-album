using stickeralbum.IO;
using System;

namespace stickeralbum.Entities {
    public class God : Entity {
        public static new God Get(String key) 
            => Cache.Get(key) as God;
    }
}
