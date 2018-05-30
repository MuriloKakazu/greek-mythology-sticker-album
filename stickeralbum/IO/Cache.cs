using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using stickeralbum.Design;
using stickeralbum.Entities;
using stickeralbum.Generics;
using stickeralbum.Debug;
using STDGEN = System.Collections.Generic;

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

        public static void Load() {
            try {
                DebugUtils.LogIO("Populating cache...");
                LoadGods();
                LoadIcons();
                LoadTitans();
                LoadSprites();
                //LoadSemiGods();
                //LoadCreatures();
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

        private static void LoadIcons()
            => JsonConvert.DeserializeObject<LinkedList<Sprite>>
              (File.ReadAllText(Paths.IconsMetadata))
              .ForEach(x => x.Path = Paths.IconsDirectory + x.Path)
              .ForEach(x => x.LoadImage())
              .ForEach(x => Add(x));

        private static void LoadGods() 
            => JsonConvert.DeserializeObject<LinkedList<God>>
              (File.ReadAllText(Paths.GodsMetadata))
              .ForEach(x => Add(x));

        private static void LoadTitans() 
            => JsonConvert.DeserializeObject<LinkedList<Titan>>
              (File.ReadAllText(Paths.TitansMetadata))
              .ForEach(x => Add(x));

        private static void LoadSprites()
            => JsonConvert.DeserializeObject<LinkedList<Sprite>>
              (File.ReadAllText(Paths.SpritesMetadata))
              .ForEach(x => x.Path = Paths.SpritesDirectory + x.Path)
              .ForEach(x => x.LoadImage())
              .ForEach(x => Add(x));

        private static void LoadCreatures() 
            => JsonConvert.DeserializeObject<LinkedList<Creature>>
              (File.ReadAllText(Paths.CreaturesMetadata))
              .ForEach(x => Add(x));

        private static void LoadSemiGods() 
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
