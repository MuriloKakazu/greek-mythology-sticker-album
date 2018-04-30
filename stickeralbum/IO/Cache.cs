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
using DefaultGenerics = System.Collections.Generic;

namespace stickeralbum.IO
{
    public static class Cache {
        private static DefaultGenerics.Dictionary<String, ICacheable> CachedObjects = new DefaultGenerics.Dictionary<String, ICacheable>();
        private static String IconsJsonFile => Paths.AssetsDirectory + "icons.json";
        private static String GodsJsonFile => Paths.AssetsDirectory + "gods.json";
        private static String TitansJsonFile => Paths.AssetsDirectory + "titans.json";

        public static void Populate() {
            Generate();
            PopulateIcons();
            PopulateGods();
            PopulateTitans();

            Console.WriteLine("Cached objects:");
            for (Int32 i = 0; i < CachedObjects.Values.Count; i++) {
                Console.WriteLine(String.Format($"{CachedObjects.Keys.ToList()[i]} - {CachedObjects.Values.ToList()[i]}"));
            }
            Console.WriteLine("Cache populated!");
        }

        private static void Generate() {
            //LinkedList<ICacheable> values = new LinkedList<ICacheable>();
            //values.Add(new God() {
            //    ID = "god_zeus",
            //    BitmapID = "sprite_zeus",
            //    Description = "Sample Description",
            //    Gender = Gender.Male,
            //    FatherID = "titan_cronus",
            //    MotherID = "titan_rhea",
            //    Rarity = Rarity.Epic,
            //    Name = "Zeus"
            //});

            //var jsonBody = JsonConvert.SerializeObject(values, Formatting.Indented);
            //File.WriteAllText("values.json", jsonBody);
        }

        private static void PopulateIcons() {
            var values = JsonConvert.DeserializeObject<LinkedList<Bitmap>>(File.ReadAllText(IconsJsonFile));
            values.ForEach(x => x.Path = Paths.IconsDirectory + x.Path);
            values.ForEach(x => Add(x));
        }

        private static void PopulateGods() {
            var values = JsonConvert.DeserializeObject<LinkedList<God>>(File.ReadAllText(GodsJsonFile));
            values.ForEach(x => Add(x));
        }

        private static void PopulateTitans() {
            var values = JsonConvert.DeserializeObject<LinkedList<Titan>>(File.ReadAllText(TitansJsonFile));
            values.ForEach(x => Add(x));
        }

        private static void Add(ICacheable value) =>
            CachedObjects.Add(value.ID, value);

        private static void AddAll(LinkedList<ICacheable> values) => 
            values.ForEach(x => Add(x));

        public static Boolean TryGet(String key, out ICacheable value)  => 
            CachedObjects.TryGetValue(key, out value);

        public static ICacheable Get(String key) => 
            TryGet(key, out ICacheable value) ? value : null;

        public static Bitmap GetBitmap(String key) =>
            TryGet(key, out ICacheable value) ? ((value is Bitmap) ? (value as Bitmap) : null) : null;

        public static Entity GetEntity(String key) =>
            TryGet(key, out ICacheable value) ? ((value is Entity) ? (value as Entity) : null) : null;

        public static God GetGod(String key) =>
            TryGet(key, out ICacheable value) ? ((value is God) ? (value as God) : null) : null;

        public static Bitmap GetIcon(IconType type, IconColor color) => 
            GetBitmap(String.Format($"{type.ToString().ToLower()}_{color.ToString().ToLower()}"));
    }
}
