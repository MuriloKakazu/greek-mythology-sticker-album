using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.IO
{
    public class Cacheable
    {
        public String ID;

        public static Cacheable CastFrom(dynamic dynamicObj)
            => Convert.ChangeType(dynamicObj, Type.GetType("Cacheable"));
    }
}
