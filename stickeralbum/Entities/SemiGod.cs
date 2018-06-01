using stickeralbum.IO;
using System;

namespace stickeralbum.Entities {
    public class SemiGod : Entity {

        public String RelatedGodID;
        public God RelatedGod { get; }

        public static new SemiGod Get(String ID)
            => Cache.Get(ID) as SemiGod;
    }
}
