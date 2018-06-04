using stickeralbum.Entities;
using stickeralbum.Generics;
using System;

namespace stickeralbum.Game {
    public class Player
    {
        public Int64 Coins { get; set; }
        public Inventory Inventory { get; set; }
        public LinkedList<String> UnlockedIDs { get; set; }
        public DateTime LastGift;

        public Player() {
            Inventory = new Inventory();
            UnlockedIDs = new LinkedList<String>();
        }

        public Boolean HasUnlocked(Entity e) //=> true;
            => UnlockedIDs.Contains(e.ID);

        public void Unlock(String id) 
            => UnlockedIDs.Add(id);
    }
}
