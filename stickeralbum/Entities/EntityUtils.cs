using stickeralbum.Generics;
using System.Linq;

namespace stickeralbum.Entities
{
    public static class EntityUtils
    {
        public static LinkedList<Entity> GetFemales(this LinkedList<Entity> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Female).ToLinkedList();

        public static LinkedList<Entity> GetMales(this LinkedList<Entity> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Male).ToLinkedList();

        public static LinkedList<God> GetFemales(this LinkedList<God> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Female).ToLinkedList();

        public static LinkedList<God> GetMales(this LinkedList<God> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Male).ToLinkedList();

        public static LinkedList<SemiGod> GetFemales(this LinkedList<SemiGod> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Female).ToLinkedList();

        public static LinkedList<SemiGod> GetMales(this LinkedList<SemiGod> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Male).ToLinkedList();

        public static LinkedList<Titan> GetFemales(this LinkedList<Titan> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Female).ToLinkedList();

        public static LinkedList<Titan> GetMales(this LinkedList<Titan> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Male).ToLinkedList();

        public static LinkedList<Creature> GetFemales(this LinkedList<Creature> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Female).ToLinkedList();

        public static LinkedList<Creature> GetMales(this LinkedList<Creature> entityList)
            => entityList.Where(x => x.Gender == Enums.Gender.Male).ToLinkedList();

        public static LinkedList<God> GetGods(this LinkedList<Entity> entityList)
            => entityList.OfType<God>().Cast<God>().ToLinkedList();

        public static LinkedList<SemiGod> GetSemiGods(this LinkedList<Entity> entityList)
            => entityList.OfType<SemiGod>().Cast<SemiGod>().ToLinkedList();

        public static LinkedList<Titan> GetTitans(this LinkedList<Entity> entityList)
            => entityList.OfType<Titan>().Cast<Titan>().ToLinkedList();

        public static LinkedList<Creature> GetCreatures(this LinkedList<Entity> entityList)
            => entityList.OfType<Creature>().Cast<Creature>().ToLinkedList();
    }
}
