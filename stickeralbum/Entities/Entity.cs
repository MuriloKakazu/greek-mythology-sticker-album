using stickeralbum.Design;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Entities {
    public abstract class Entity : Cacheable {
        public String BitmapID;
        public String Name;
        public Rarity Rarity;
        public Gender Gender;
        public String Description;

        public static Entity Get(String ID) 
            => CastFrom(Cache.Get(ID, out dynamic value));

        public Boolean IsGod 
            => (this is God);
        public Boolean IsSemiGod 
            => (this is SemiGod);
        public Boolean IsCreature 
            => (this is Creature);
        public Boolean IsTitan 
            => (this is Titan);

        public Bitmap Bitmap;

        public override string ToString() 
            => this.Name;

        public static new Entity CastFrom(dynamic dynamicObj)
            => Convert.ChangeType(dynamicObj, Type.GetType("Entity"));
    }
}
