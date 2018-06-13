using System;

namespace stickeralbum.IO {
    public static class Paths {
        public static String ApplicationDirectory
            => AppDomain.CurrentDomain.BaseDirectory;
        public static String AssetsDirectory
            => $@"{ApplicationDirectory}assets\";
        public static String MetadataDirectory
            => $@"{AssetsDirectory}metadata\";
        public static String AudioDirectory
            => $@"{AssetsDirectory}audio\";
        public static String IconsDirectory
            => $@"{AssetsDirectory}icons\";
        public static String SpritesDirectory
            => $@"{AssetsDirectory}sprites\";
        public static String SaveGameDirectory
            => $@"{ApplicationDirectory}saves\";
        public static String LogsDirectory
            => $@"{ApplicationDirectory}logs\";
        public static String CustomSpritesDirectory
            => $@"{SpritesDirectory}custom\";
        public static String CustomMetadataDirectory
            => $@"{MetadataDirectory}custom\";


        public static String SettingsFile
            => $"{ApplicationDirectory}settings.data";
        public static String GameSaveFile
            => $"{ApplicationDirectory}save.data";
        public static String IconsMetadata
            => $"{MetadataDirectory}icons.json";
        public static String ChaosMetadata
            => $"{MetadataDirectory}chaos.json";
        public static String GodsMetadata
            => $"{MetadataDirectory}gods.json";
        public static String TitansMetadata
            => $"{MetadataDirectory}titans.json";
        public static String CreaturesMetadata
            => $"{MetadataDirectory}creatures.json";
        public static String SemiGodsMetadata
            => $"{MetadataDirectory}semigods.json";
        public static String SpritesMetadata
            => $"{MetadataDirectory}sprites.json";
        public static String SoundFXMetadata
            => $"{MetadataDirectory}soundfx.json";
        public static String CustomGodsMetadata
            => $"{CustomMetadataDirectory}gods.json";
        public static String CustomTitansMetadata
            => $"{CustomMetadataDirectory}titans.json";
        public static String CustomCreaturesMetadata
            => $"{CustomMetadataDirectory}creatures.json";
        public static String CustomSemiGodsMetadata
            => $"{CustomMetadataDirectory}semigods.json";
        public static String CustomSpritesMetadata
            => $"{CustomMetadataDirectory}sprites.json";
    }
}
