using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace stickeralbum.Extensions {
    public static class BitmapSourceExtensions {
        public static WriteableBitmap GetAllBlack(this BitmapSource source) {
            Int32 stride = (Int32)(source.PixelWidth * source.Width);
            Byte[] data = new Byte[stride * source.PixelHeight];
            source.CopyPixels(data, stride, 0);
            var target = new WriteableBitmap(source);

            for (var i = 0; i < data.Length / 4; i++) {
                data[i * 4] = 0;
                data[i * 4 + 1] = 0;
                data[i * 4 + 2] = 0;
                //data[i * 4 + 3];
            }

            target.WritePixels(new Int32Rect(
                0, 0, source.PixelWidth, source.PixelHeight
            ), data, stride, 0);
            return target;
        }
    }
}
