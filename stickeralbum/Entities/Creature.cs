using stickeralbum.IO;
using System;

namespace stickeralbum.Entities {
    public class Creature : Entity {
        public Int32 DangerLevel { get; set; }

        public static new Creature Get(String ID)
            => Cache.Get(ID) as Creature;
    }
}
