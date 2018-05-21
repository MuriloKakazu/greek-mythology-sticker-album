using stickeralbum.Entities;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Design
{
    public class Frame : Sprite {
        public static new Frame Get(String ID)
            => Cache.Get(ID) as Frame;

        public static Frame Get(Rarity rarity)
            => Cache.Get($"frame_{rarity.ToString().ToLower()}") as Frame;
    }
}
