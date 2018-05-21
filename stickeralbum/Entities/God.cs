using Newtonsoft.Json;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace stickeralbum.Entities {
    public class God : Entity {
        public static new God Get(String key) 
            => Cache.Get(key) as God;
    }
}
