using System;

namespace stickeralbum {
    public static class GlobalPaths {
        public static String AssetsDirectory => "assets/";
        public static String AudioDirectory => $"{AssetsDirectory}audio/";
        public static String SpritesDirectory => $"{AssetsDirectory}sprites/";
        public static String SaveGameDirectory => "saves/";
    }
}
