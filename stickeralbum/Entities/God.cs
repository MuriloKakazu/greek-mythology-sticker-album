using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    class God : Entity {
        public String FatherID;
        public String MotherID;

        public Entity Father => 
            Cache.TryGet(FatherID, out ICacheable value) ? (value as Entity) : null;

        public Entity Mother => 
            Cache.TryGet(MotherID, out ICacheable value) ? (value as Entity) : null;
    }
}
