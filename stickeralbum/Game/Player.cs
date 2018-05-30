using stickeralbum.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
