using System;

namespace stickeralbum.Game.Items {
    public abstract class InventoryItem {

        public InventoryItem() { }

        public InventoryItem(String itemID):
            this() => ItemID = itemID;

        public String ItemID { get; set; }
    }
}