using System;

namespace stickeralbum.IO {
    public static class Paths {
        public static String ApplicationDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public static String AssetsDirectory => $@"{ApplicationDirectory}assets\";
        public static String AudioDirectory => $@"{AssetsDirectory}audio\";
        public static String IconsDirectory => $@"{AssetsDirectory}icons\";
        public static String SpritesDirectory => $@"{AssetsDirectory}sprites\";
        public static String SaveGameDirectory => $@"{ApplicationDirectory}saves\";
    }
}
