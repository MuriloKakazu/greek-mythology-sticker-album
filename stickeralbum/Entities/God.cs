using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    class God : Entity {
        public Entity Father { get; set; }
        public Entity Mother { get; set; }  
    }
}
