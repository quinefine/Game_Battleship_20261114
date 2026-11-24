using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Battleship
{
    public class Player
    {
        public int winCount { get; set; } = 0;
        public Board targetBoard { get; set; }
        public Board oceanBoard { get; set; }

    }
}
