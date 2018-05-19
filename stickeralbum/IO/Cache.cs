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
using STDGEN = System.Collections.Generic;

namespace stickeralbum.IO
{
    public static class Cache {
        private static STDGEN.Dictionary<String, dynamic> CachedObjects 
                 = new STDGEN.Dictionary<String, dynamic>();

        public static void Load() {
            LoadGods();
            LoadIcons();
            LoadTitans();
            //LoadSemiGods();
            //LoadCreatures();
        }

        public static void Debug() {
            Console.WriteLine("Cached objects:");
            CachedObjects.Values.ToList()
                .ForEach(x => Console.WriteLine($"{x.ID} - {x.ToString()}"));
            Console.WriteLine("Cache populated!");
        }

        private static LinkedList<dynamic> LoadObjectsFrom(String jsonPath)
            => JsonConvert.DeserializeObject<LinkedList<dynamic>>(File.ReadAllText(jsonPath))
              .ForEach(x => Add(x));

        private static void LoadIcons()
            => LoadObjectsFrom(Paths.IconsMetadata)
              .ForEach(x => x.Path = Paths.IconsDirectory + x.Path);

        private static void LoadTitans()
            => LoadObjectsFrom(Paths.TitansMetadata);

        private static void LoadGods()
            => LoadObjectsFrom(Paths.GodsMetadata);

        private static void LoadSemiGods()
            => LoadObjectsFrom(Paths.SemiGodsMetadata);

        private static void LoadCreatures()
            => LoadObjectsFrom(Paths.CreaturesMetadata);

        private static void Add(dynamic value) 
            => CachedObjects.Add(value.ID.ToString(), value);

        private static void AddRange(LinkedList<dynamic> values) 
            => values.ForEach(x => Add(x));

        public static Boolean ContainsKey(String key)  
            => CachedObjects.ContainsKey(key);

        public static dynamic Get(String key, out dynamic value)
            => CachedObjects.TryGetValue(key, out value);
    }
}
