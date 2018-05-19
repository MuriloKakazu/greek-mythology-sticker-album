using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public class God : Entity {
        public String FatherID;
        public String MotherID;

        public static new God Get(String key) 
            => CastFrom(Cache.Get(key, out dynamic value));

        public Entity Father 
            => Entity.Get(FatherID);

        public Entity Mother
            => Entity.Get(MotherID);

        public static new God CastFrom(dynamic dynamicObj)
            => Convert.ChangeType(dynamicObj, Type.GetType("God"));
    }
}
