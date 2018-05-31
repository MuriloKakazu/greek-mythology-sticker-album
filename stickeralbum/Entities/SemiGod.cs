using stickeralbum.IO;
using System;

namespace stickeralbum.Entities {
    public class SemiGod : Entity {
        public God RelatedGod { get; set; }

        public static new SemiGod Get(String ID)
            => Cache.Get(ID) as SemiGod;
    }
}
