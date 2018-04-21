using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace stickeralbum.Audio {
    public class SoundPlayer : IDisposable {
        #region Properties
        private static WaveOutEvent Device { get; set; }
        private static MixingSampleProvider Mixer { get; set; }
        public static readonly SoundPlayer Instance = new SoundPlayer(44100, 2);
        public static PlaybackState State { get { return Device.PlaybackState; } }
        public static Single Volume { get { return GlobalSettings.Volume; } set { GlobalSettings.Volume = Device.Volume = value; } }
        public static Boolean DestroyOnPlaybackEnd { get; set; }
        #endregion

        public SoundPlayer(Int32 sampleRate = 44100, Int32 channelCount = 2) {
            Device = new WaveOutEvent();
            Mixer  = new MixingSampleProvider(
                WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount)
            );
            Mixer.ReadFully = true;
            Volume = GlobalSettings.Volume;
            Device.Init(Mixer);
            Device.Play();
        }

        private void AddMixerInput(ISampleProvider input) {
            Mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input) {
            if (input.WaveFormat.Channels == Mixer.WaveFormat.Channels) return input;
            if (input.WaveFormat.Channels == 1 && Mixer.WaveFormat.Channels == 2) return new MonoToStereoSampleProvider(input);
            return null;
        }

        public void Play(String fileName) {
            if (String.IsNullOrWhiteSpace(fileName)) return;

            var input = new AudioFileReader(fileName);
            Volume = GlobalSettings.Volume;

            input.Volume = Volume;
            AddMixerInput(new AutoDisposableStream(input));
        }

        public void Play(SoundTrack track) {
            if (track == null) return;

            Volume = GlobalSettings.Volume;
            AddMixerInput(new SoundTrackSampleProvider(track));
        }

        public void Dispose() {
            Device.Dispose();
        }
    }
}
