using System;

namespace stickeralbum.Game {
    public class Player
    {
        public Int64 Coins { get; set; }
        public Inventory Inventory { get; set; }

        public Player() {
            Inventory = new Inventory();
        }
    }
}
