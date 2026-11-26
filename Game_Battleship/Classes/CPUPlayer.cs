using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_Battleship
{
    public class CPUPlayer : Player
    {

        private char difficulty;

        public void CalculateMove()
        {

        }

        public override void PlaceShip() //places all ships at once
        {

            Random rand = new Random();
            
                foreach (Ship ship in this.Ships.OrderByDescending(s => s.Size))
                {
                    this.OceanBoard.RandomizeShipPlacement(this.OceanBoard, ship);
                }
        }

        public CPUPlayer(string name, Board oceanBoard, Board targetBoard) : base(name, oceanBoard, targetBoard)
        {
        }
    }
}
