using stickeralbum.Entities;
using stickeralbum.Generics;

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
