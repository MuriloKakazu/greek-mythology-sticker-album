using stickeralbum.Entities;
using stickeralbum.Game.Items;
using stickeralbum.Generics;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stickeralbum.Game {
    public class Inventory
    {
        public LinkedList<InventoryItem> Items;

        public Inventory() 
            => Items = new LinkedList<InventoryItem>();

        public Boolean ContainsKey(String itemKey) 
            => CountItem(itemKey) > 0;

        public Boolean HasConsumedSticker(Entity e)
            => (from   x
                in     Items
                where  x.ItemID == e.ID
                   && (x is SimpleSticker)
                   && (x as SimpleSticker).IsConsumed
                select x
               ).Count() > 0;

        public LinkedList<SimpleSticker> GetAllStickers()
            => Items.OfType<SimpleSticker>()
              .Cast<SimpleSticker>()
              .ToLinkedList();

        public void Add(InventoryItem item)
            => Items.Add(item);

        public Int32 CountItem(String itemKey)
            => Items.Where(x => x.ItemID == itemKey)
              .Count();
    }
}
