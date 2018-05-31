using Newtonsoft.Json;
using stickeralbum.Design;
using stickeralbum.Enums;
using stickeralbum.IO;
using System;

namespace stickeralbum.Entities {
    public abstract class Entity : Cacheable {
        public String FatherID;
        public String MotherID;
        public String SpriteID;
        public String BackgroundID;
        public String Name;
        public String Description;
        public Rarity Rarity;
        public Gender Gender;

        public Boolean IsCustom { get; set; }

        public static Entity Get(String ID) 
            => Cache.Get(ID) as Entity;

        [JsonIgnore]
        public Boolean IsGod 
            => (this is God);
        [JsonIgnore]
        public Boolean IsSemiGod 
            => (this is SemiGod);
        [JsonIgnore]
        public Boolean IsCreature 
            => (this is Creature);
        [JsonIgnore]
        public Boolean IsTitan 
            => (this is Titan);
        [JsonIgnore]
        public Sprite Sprite
            => Sprite.Get(SpriteID);
        [JsonIgnore]
        public Sprite Background
            => Sprite.Get(SpriteID);
        [JsonIgnore]
        public Entity Father
            => Get(FatherID);
        [JsonIgnore]
        public Entity Mother
            => Get(MotherID);
    }
}
