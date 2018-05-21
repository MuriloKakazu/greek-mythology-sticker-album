using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public sealed class Chaos : Entity {
        public static Chaos Get()
            => Cache.Get("chaos") as Chaos;
    }
}
