using Newtonsoft.Json;
using stickeralbum.Debug;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Game
{
    public static class GameMaster
    {
        public static Player Player { get; set; }
        public static Settings Settings { get; set; }

        public static void SaveAll() {
            try {
                File.WriteAllText(Paths.GameSaveFile, JsonConvert.SerializeObject(Player, Formatting.Indented));
                DebugUtils.LogIO($"Saved game => {Paths.GameSaveFile}");
            } catch (Exception e) {
                DebugUtils.LogError($"Couldn't save game. Reason: {e.Message}");
            }
            try {
                File.WriteAllText(Paths.SettingsFile, JsonConvert.SerializeObject(Settings, Formatting.Indented));
                DebugUtils.LogIO($"Saved settings => {Paths.SettingsFile}");
            } catch (Exception e) {
                DebugUtils.LogError($"Couldn't save settings. Reason: {e.Message}");
            }
        }

        public static void LoadAll() {
            try {
                Player = JsonConvert.DeserializeObject<Player>(File.ReadAllText(Paths.GameSaveFile));
                DebugUtils.LogIO($"Loaded save game => {Paths.GameSaveFile}");
            } catch (Exception e) {
                Player = new Player();
                Player.Coins = 10;
                DebugUtils.LogError($"Couldn't load game save. Reason: {e.Message}");
            }
            try {
                Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Paths.SettingsFile));
                DebugUtils.LogIO($"Loaded settings => {Paths.SettingsFile}");
            } catch (Exception e) {
                Settings = new Settings();
                Settings.Volume = 100f;
                Settings.Theme = Design.Theme.Dark;
                DebugUtils.LogError($"Couldn't load settings. Reason: {e.Message}");
            }
        }
    }
}
