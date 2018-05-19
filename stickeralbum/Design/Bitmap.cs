using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace stickeralbum.Design {
    public class Bitmap : Cacheable {
        public String Path;

        [NonSerialized]
        public BitmapSource Source;

        public static Bitmap Get(String ID)
            => CastFrom(Cache.Get(ID, out dynamic value));

        public static Bitmap Get(IconType type, IconColor color)
            => Get($"{type.ToString().ToLower()}_{color.ToString().ToLower()}");

        public override string ToString() 
            => this.Path;

        public static new Bitmap CastFrom(dynamic dynamicObj)
           => Convert.ChangeType(dynamicObj, Type.GetType("Bitmap"));
    }
}
