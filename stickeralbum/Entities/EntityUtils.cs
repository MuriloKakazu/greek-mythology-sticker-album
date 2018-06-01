using stickeralbum.Generics;
using stickeralbum.IO;
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

        public static LinkedList<Titan> AllTitans()
            => Cache.GetAll().Where(x => x is Titan).Cast<Titan>().ToLinkedList();

        public static LinkedList<God> AllGods()
            => Cache.GetAll().Where(x => x is God).Cast<God>().ToLinkedList();

        public static LinkedList<SemiGod> AllSemiGods()
            => Cache.GetAll().Where(x => x is SemiGod).Cast<SemiGod>().ToLinkedList();

        public static LinkedList<Creature> AllCreatures()
            => Cache.GetAll().Where(x => x is Creature).Cast<Creature>().ToLinkedList();

        public static LinkedList<Entity> AllEntities()
            => Cache.GetAll().Where(x => x is Entity).Cast<Entity>().ToLinkedList();
    }
}
