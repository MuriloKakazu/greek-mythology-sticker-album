using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace stickeralbum.Design {
    public class Bitmap : ICacheable {
        public String ID { get; set; }
        public String Path;

        [NonSerialized]
        public BitmapSource Source;

        public override string ToString() => this.Path;
    }
}
