using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Linq;

namespace stickeralbum.Entities {
    public class Titan : Entity {
        public String Personification { get; set; }

        public static new Titan Get(String ID)
            => Cache.Get(ID) as Titan;

        public new static LinkedList<Titan> GetAll()
            => Cache.GetAll()
              .Where(x => x is Titan)
              .Cast<Titan>()
              .ToLinkedList();
    }
}
