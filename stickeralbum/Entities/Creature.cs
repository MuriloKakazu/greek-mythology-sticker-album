using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    class Creature : Entity {
        public Int32 DangerLevel { get; set; }

        public static new Creature Get(String ID)
            => Cache.Get(ID) as Creature;
    }
}
