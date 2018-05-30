using Newtonsoft.Json;
using stickeralbum.Entities;
using stickeralbum.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
