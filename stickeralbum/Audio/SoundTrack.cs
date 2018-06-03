using NAudio.Wave;
using Newtonsoft.Json;
using stickeralbum.Debug;
using stickeralbum.Game;
using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.Linq;

namespace stickeralbum.Audio {
    public class SoundTrack : Cacheable {
        public String Path { get; set; }
        [JsonIgnore]
        public Boolean IsPlaying
            => SoundPlayer.AllTracksPlaying().Contains(this);

        public static SoundTrack Get(String id)
            => Cache.Get(id) as SoundTrack;

        public void Setup() {
            using (var audioFileReader = new AudioFileReader(Path)) {
                WaveFormat = audioFileReader.WaveFormat;
                var wholeFile = new LinkedList<Single>();
                var readBuffer = new Single[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;
                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                    wholeFile.Add(readBuffer.Take(samplesRead));
                }
                AudioData = wholeFile.ToArray();
            }
        }

        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }
    }
}
