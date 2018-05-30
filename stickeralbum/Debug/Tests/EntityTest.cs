using stickeralbum.Entities;
using stickeralbum.Generics;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Debug.Tests
{
    public static class EntityTest
    {
        public static void TestEntities() {
            LinkedList<Entity> entities = new LinkedList<Entity>();
            entities.Add(new God());
            entities.Add(new SemiGod());
            entities.GetGods().GetFemales();
        }
    }
}
