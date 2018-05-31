using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Game.Items;
using stickeralbum.Generics;
using System;
using System.Linq;

namespace stickeralbum.Game {
    public class Inventory {
        private LinkedList<InventoryItem> Items => new LinkedList<InventoryItem>(Stickers);
        public  LinkedList<SimpleSticker> Stickers;

        public Inventory()
            => Stickers = new LinkedList<SimpleSticker>();

        public Boolean ContainsKey(String itemKey)
            => CountItem(itemKey) > 0;

        public Boolean HasConsumedSticker(Entity e)
            => GetConsumedStickers().Where(x => x.ItemID == e.ID).Count() > 0;

        public LinkedList<SimpleSticker> GetConsumedStickers()
            => Stickers.Where(x => x.IsConsumed).ToLinkedList();

        public LinkedList<SimpleSticker> GetUnconsumedStickers()
            => Stickers.Where(x => !x.IsConsumed).ToLinkedList();

        public void Add(InventoryItem item) {
            if (item is SimpleSticker) {
                Stickers.Add(item as SimpleSticker);
            } else {
                DebugUtils.LogWarning($"No suitable inventory for item => {item}");
            }
        }

        public Int32 CountItem(String itemKey)
            => Items.Where(x => x.ItemID == itemKey)
              .Count();
    }
}
