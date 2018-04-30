using stickeralbum.Design;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public abstract class Entity : ICacheable {
        public String ID { get; set; }
        public String BitmapID;
        public String Name;
        public Rarity Rarity;
        public Gender Gender;
        public String Description;

        public static Entity Get(String key) =>
            Cache.GetEntity(key);

        public Boolean IsGod => (this is God);
        public Boolean IsSemiGod => (this is SemiGod);

        public Bitmap Bitmap;

        public override string ToString() => this.Name;
    }
}
