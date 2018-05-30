using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public class SemiGod : Entity {
        public God RelatedGod { get; set; }

        public static new SemiGod Get(String ID)
            => Cache.Get(ID) as SemiGod;
    }
}
