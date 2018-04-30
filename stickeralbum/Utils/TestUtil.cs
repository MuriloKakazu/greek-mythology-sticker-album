using stickeralbum.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Utils {
    public static class TestUtil {
        public static void RunAllTests() {
            Tests.JsonTest.Test();
        }
    }
}
