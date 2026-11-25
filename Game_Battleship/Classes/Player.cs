using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//make change and save it in repo. go to 'git changes' next to 'solution explorer'
namespace Game_Battleship
{
    public class Player
    {
        public Player(string name, Board oceanBoard, Board targetBoard)
        {
            this.name = name;
            this.oceanBoard = oceanBoard;
            this.targetBoard = targetBoard;
        }
        public int winCount { get; set; } = 0;
        public Board targetBoard { get; set; }
        public Board oceanBoard { get; set; }
        public string name { get; set; }
        public Ship[] ships =
        {
            new Ship { name = "Destroyer", size = 2, id = 1},
            new Ship { name = "Submarine", size = 3, id = 2 },
            new Ship { name = "Cruiser", size = 3, id = 3 },
            new Ship { name = "Battleship", size = 4, id = 4 },
            new Ship { name = "Carrier", size = 5, id = 5 }
        };
    }
}
