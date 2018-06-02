using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Linq;

namespace stickeralbum.Entities {
    public class SemiGod : Entity {

        public String RelatedGodID;
        public God RelatedGod { get; }

        public static new SemiGod Get(String ID)
            => Cache.Get(ID) as SemiGod;

        public new static LinkedList<SemiGod> GetAll()
            => Cache.GetAll()
              .Where(x => x is SemiGod)
              .Cast<SemiGod>()
              .ToLinkedList();
    }
}
