using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Linq;

namespace stickeralbum.Entities {
    public class God : Entity {
        public String Personification { get; set; }

        public static new God Get(String key) 
            => Cache.Get(key) as God;

        public new static LinkedList<God> GetAll()
            => Cache.GetAll()
              .Where(x => x is God)
              .Cast<God>()
              .ToLinkedList();
    }
}
