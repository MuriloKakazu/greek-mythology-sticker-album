using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public class Titan : Entity {

        public static new Titan Get(String ID)
            => CastFrom(Cache.Get(ID, out dynamic value));

        public static new Titan CastFrom(dynamic dynamicObj)
           => Convert.ChangeType(dynamicObj, Type.GetType("Titan"));
    }
}
