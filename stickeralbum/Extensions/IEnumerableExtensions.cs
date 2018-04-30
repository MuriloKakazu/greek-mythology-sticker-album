using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Generics {
    public static class IEnumerableExtensions {
        public static T Random<T>(this IEnumerable<T> enumerable) {
            if (enumerable == null) {
                throw new ArgumentNullException(nameof(enumerable));
            }
            var r = new Random();
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default(T) : list[r.Next(0, list.Count)];
        }

    }
}
