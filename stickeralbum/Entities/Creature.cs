using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Linq;

namespace stickeralbum.Entities {
    public class Creature : Entity {
        public Int32 DangerLevel { get; set; }

        public static new Creature Get(String ID)
            => Cache.Get(ID) as Creature;

        public new static LinkedList<Creature> GetAll()
            => Cache.GetAll()
              .Where(x => x is Creature)
              .Cast<Creature>()
              .ToLinkedList();
    }
}
