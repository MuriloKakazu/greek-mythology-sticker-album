using NAudio.Wave;
using System;

namespace stickeralbum.Audio {
    class AutoDisposableStream : ISampleProvider {
        private readonly AudioFileReader reader;
        private Boolean IsDisposed;

        public AutoDisposableStream(AudioFileReader reader) {
            this.reader = reader;
            this.WaveFormat = reader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count) {
            if (IsDisposed) return 0;
            var read = reader.Read(buffer, offset, count);
            if (read == 0) {
                reader.Dispose();
                IsDisposed = true;
            }
            return read;
        }

        public WaveFormat WaveFormat { get; private set; }
    }
}
