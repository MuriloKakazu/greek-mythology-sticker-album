using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public abstract class Entity {
        public Int32 ID { get; set; }
        public String ImagePath { get; set; }
        public String Name { get; set; }
        public Rarity Rarity { get; set; }
        public String Description { get; set; }

        public override string ToString() => this.Name;
    }
}
