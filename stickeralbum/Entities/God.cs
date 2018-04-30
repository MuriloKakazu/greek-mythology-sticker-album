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

        public static new God Get(String key) =>
            Cache.GetGod(key);

        public Entity Father =>
            Entity.Get(FatherID);

        public Entity Mother =>
            Entity.Get(MotherID);
    }
}
