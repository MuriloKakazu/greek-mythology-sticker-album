using NAudio.Wave;
using System;
using System.Linq;

namespace stickeralbum.Audio {
    public class SoundTrack {
        public string FileName { get; set; }
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }

        public SoundTrack(string fileName) {
            FileName = fileName;

            using (var audioFileReader = new AudioFileReader(GlobalPaths.AudioDirectory + FileName)) {
                this.WaveFormat = audioFileReader.WaveFormat;
                var wholeFile = new Single[] { ((int)(audioFileReader.Length / 4)) };
                var readBuffer = new Single[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;

                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                    wholeFile.ToList().AddRange(readBuffer.Take(samplesRead));
                }
                this.AudioData = wholeFile.ToArray();
            }
        }
    }
}
