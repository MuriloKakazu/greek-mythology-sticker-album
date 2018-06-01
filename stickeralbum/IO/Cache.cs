using System;
using System.IO;
using Newtonsoft.Json;
using stickeralbum.Design;
using stickeralbum.Entities;
using stickeralbum.Generics;
using stickeralbum.Debug;
using STDGEN = System.Collections.Generic;
using stickeralbum.Extensions;

namespace stickeralbum.IO
{
    public static class Cache {
        private static STDGEN.Dictionary<String, Cacheable> CachedObjects 
                 = new STDGEN.Dictionary<String, Cacheable>();

        private class ObjectNotFoundInCacheException : Exception {
            private String Key;
            public new String Message 
                => $"No object with key <{Key}> found in cache.";
            public ObjectNotFoundInCacheException(String key) 
                => Key = key;
        }

        public static void Clear() {
            CachedObjects.Clear();
        }

        public static void Load() {
            try {
                DebugUtils.LogIO("Populating cache...");
                LoadDefaults();
                LoadCustoms();
                DebugUtils.LogIO("Cache populated!");
            } catch (Exception e) {
                DebugUtils.LogError($"Error populating cache. Reason: {e.Message}");
            }
        }

        public static void DumpLog() {
            DebugUtils.LogCache("Dumping Cache State...");
            CachedObjects.Values.ToLinkedList()
           .ForEach(x => DebugUtils.LogCache($"{x.ID} =>\n " +
            $"{JsonConvert.SerializeObject(x, Formatting.Indented)}"));
            DebugUtils.LogCache("Cache State Dumped!");
        }

        public static void LoadDefaults() {
            LoadGods();
            LoadIcons();
            LoadTitans();
            var sprites = LoadSprites();
            LoadSemiGods();
            //LoadCreatures();
        }

        public static void LoadCustoms() {
            LoadCustomGods();
            //LoadCustomTitans();
            var sprites = LoadCustomSprites();
            LoadCustomSemiGods();
            //LoadCustomCreatures();
        }

        private static LinkedList<Creature> LoadCustomCreatures()
            => JsonConvert.DeserializeObject<LinkedList<Creature>>
              (File.ReadAllText(Paths.CustomCreaturesMetadata))
              .ForEach(x => x.IsCustom = true)
              .ForEach(x => Add(x));

        private static LinkedList<SemiGod> LoadCustomSemiGods()
            => JsonConvert.DeserializeObject<LinkedList<SemiGod>>
              (File.ReadAllText(Paths.CustomSemiGodsMetadata))
              .ForEach(x => x.IsCustom = true)
              .ForEach(x => Add(x));

        private static LinkedList<Sprite> LoadCustomSprites()
            => JsonConvert.DeserializeObject<LinkedList<Sprite>>
              (File.ReadAllText(Paths.CustomSpritesMetadata))
              .ForEach(x => x.Path = Paths.CustomSpritesDirectory + x.Path)
              .ForEach(x => x.LoadImage())
              .ForEach(x => x.IsCustom = true)
              .ForEach(x => Add(x));

        private static LinkedList<Titan> LoadCustomTitans()
            => JsonConvert.DeserializeObject<LinkedList<Titan>>
              (File.ReadAllText(Paths.CustomTitansMetadata))
              .ForEach(x => x.IsCustom = true)
              .ForEach(x => Add(x));

        private static LinkedList<God> LoadCustomGods() 
            => JsonConvert.DeserializeObject<LinkedList<God>>
              (File.ReadAllText(Paths.CustomGodsMetadata))
              .ForEach(x => x.IsCustom = true)
              .ForEach(x => Add(x));

        private static LinkedList<Sprite> LoadIcons()
            => JsonConvert.DeserializeObject<LinkedList<Sprite>>
              (File.ReadAllText(Paths.IconsMetadata))
              .ForEach(x => x.Path = Paths.IconsDirectory + x.Path)
              .ForEach(x => x.LoadImage())
              .ForEach(x => Add(x));

        private static LinkedList<God> LoadGods() 
            => JsonConvert.DeserializeObject<LinkedList<God>>
              (File.ReadAllText(Paths.GodsMetadata))
              .ForEach(x => Add(x));

        private static LinkedList<Titan> LoadTitans() 
            => JsonConvert.DeserializeObject<LinkedList<Titan>>
              (File.ReadAllText(Paths.TitansMetadata))
              .ForEach(x => Add(x));

        private static LinkedList<Sprite> LoadSprites()
            => JsonConvert.DeserializeObject<LinkedList<Sprite>>
              (File.ReadAllText(Paths.SpritesMetadata))
              .ForEach(x => x.Path = Paths.SpritesDirectory + x.Path)
              .ForEach(x => x.LoadImage())
              .ForEach(x => Add(x));

        private static LinkedList<Creature> LoadCreatures() 
            => JsonConvert.DeserializeObject<LinkedList<Creature>>
              (File.ReadAllText(Paths.CreaturesMetadata))
              .ForEach(x => Add(x));

        private static LinkedList<SemiGod> LoadSemiGods() 
            => JsonConvert.DeserializeObject<LinkedList<SemiGod>>
              (File.ReadAllText(Paths.SemiGodsMetadata))
              .ForEach(x => Add(x));

        private static void Add(Cacheable value) 
            => CachedObjects.Add(value.ID.ToString(), value);

        private static void AddRange(LinkedList<Cacheable> values) 
            => values.ForEach(x => Add(x));

        public static Boolean ContainsKey(String key)  
            => CachedObjects.ContainsKey(key);

        public static Cacheable Get(String key)
            => (CachedObjects.TryGetValue(key, out Cacheable value)) ? 
                value : throw new ObjectNotFoundInCacheException(key);

        public static LinkedList<Cacheable> GetAll()
            => CachedObjects.Values.ToLinkedList();
    }
}
