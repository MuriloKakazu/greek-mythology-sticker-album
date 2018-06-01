using Newtonsoft.Json;
using stickeralbum.Debug;
using stickeralbum.Entities;
using stickeralbum.Game.Items;
using stickeralbum.Generics;
using System;
using System.Linq;

namespace stickeralbum.Game {
    public class Inventory {
        public LinkedList<SimpleSticker> Stickers;

        public Inventory()
            => Stickers = new LinkedList<SimpleSticker>();

        public Boolean ContainsKey(String itemKey)
            => CountItem(itemKey) > 0;

        public void Add(InventoryItem item) {
            if (item is SimpleSticker) {
                Stickers.Add(item as SimpleSticker);
            } else {
                DebugUtils.LogWarning($"No suitable inventory for item => {item}");
            }
        }

        public void Remove(String itemKey, Int32 quantity) {
            for (var i = 0; i < quantity; i++)
                if (ContainsKey(itemKey))
                    Stickers.Remove(Stickers.Where(x => x.ItemID == itemKey).ToArray()[0]);
        }

        public Int32 CountItem(String itemKey)
            => Stickers.Where(x => x.ItemID == itemKey)
              .Count();
    }
}
