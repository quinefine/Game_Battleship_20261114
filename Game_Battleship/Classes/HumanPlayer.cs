using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Battleship
{
    public class HumanPlayer : Player
    {

        public void PlaceShip(Ship Ship, int x, int y)
        {

        }

        public HumanPlayer(string name, Board oceanBoard, Board targetBoard) : base(name, oceanBoard, targetBoard)
        {
        }
    }
}
