using NAudio.Wave;
using System;

namespace stickeralbum.Audio
{
    public class SoundSampleProvider : ISampleProvider {
        private readonly SoundTrack Track;
        private long Position;

        public SoundSampleProvider(SoundTrack track) 
            => this.Track = track;

        public int Read(Single[] buffer, Int32 offset, Int32 count) {
            var availableSamples = Track.AudioData.Length - Position;
            var samplesToCopy    = Math.Min(availableSamples, count);
            Array.Copy(Track.AudioData, Position, buffer, offset, samplesToCopy);
            this.Position += samplesToCopy;
            return (Int32)samplesToCopy;
        }

        public WaveFormat WaveFormat 
            => Track.WaveFormat;
    }
}
