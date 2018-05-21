﻿using Newtonsoft.Json;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace stickeralbum.Design {
    public class Sprite : Cacheable {
        public  String      Path;
        private BitmapImage Image;

        [JsonIgnore]
        public BitmapSource Source
            => Image;

        public void LoadImage() 
            => Image = new BitmapImage(new Uri(Path));

        public static Sprite Get(String ID)
            => Cache.Get(ID) as Sprite;

        public static Sprite Get(IconType type, IconColor color)
            => Get($"icon_{type.ToString().ToLower()}_{color.ToString().ToLower()}");

        public override String ToString() 
            => this.Path;
    }
}