using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public Player(string name, int winCount, int lossCount)
        {
            this.name = name;
            this.winCount = winCount;
            this.lossCount = lossCount;
        }
        private int winCount = 0;

        private int lossCount = 0;

        private Board targetBoard;

        private Board oceanBoard;

        private string name;

        private Ship[] ships =
        {
            new Ship { Name = "Destroyer", Size = 2, Id = 1},
            new Ship { Name = "Submarine", Size = 3, Id = 2 },
            new Ship { Name = "Cruiser", Size = 3, Id = 3 },
            new Ship { Name = "Battleship", Size = 4, Id = 4 },
            new Ship { Name = "Carrier", Size = 5, Id = 5 }
        };

        public int WinCount
        {
            get { return this.winCount; }
            set { this.winCount = value; }
        }

        public int LossCount
        {
            get { return this.lossCount; }
            set { this.lossCount = value; }
        }

        public Board TargetBoard
        {
            get { return this.targetBoard; }
            set { this.targetBoard = value; }
        }

        public Board OceanBoard
        {
            get { return this.oceanBoard; }
            set { this.oceanBoard = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Ship[] Ships
        {
            get { return this.ships; }
            set { this.ships = value; }
        }

        public virtual void PlaceShip()
        {

        }

        public virtual bool PlaceShip(Board oceanBoard, Ship ship, int row, int column)
        {
            MessageBox.Show("an error occurred \n a parent class virtual method fired");
            return false;
        }

    }
}
