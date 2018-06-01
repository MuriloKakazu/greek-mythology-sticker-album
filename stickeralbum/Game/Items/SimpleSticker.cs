using Newtonsoft.Json;
using stickeralbum.Entities;
using System;

namespace stickeralbum.Game.Items
{
    public class SimpleSticker : InventoryItem
    {
        public SimpleSticker() { }

        public SimpleSticker(String itemID): 
            base(itemID) { }

        [JsonIgnore]
        public Entity Entity
            => Entity.Get(ItemID);
    }
}
