using Newtonsoft.Json;
using stickeralbum.Entities;
using System;

namespace stickeralbum.Game.Items
{
    public class SimpleSticker : InventoryItem
    {
        public Boolean IsConsumed { get; set; }
        [JsonIgnore]
        public Entity Entity
            => Entity.Get(ItemID);
        public void Consume() 
            => IsConsumed = true;
    }
}
