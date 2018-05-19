using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    class SemiGod : Entity {
        public God RelatedGod { get; set; }

        public static new SemiGod Get(String ID)
            => CastFrom(Cache.Get(ID, out dynamic value));

        public static new SemiGod CastFrom(dynamic dynamicObj)
           => Convert.ChangeType(dynamicObj, Type.GetType("SemiGod"));
    }
}
