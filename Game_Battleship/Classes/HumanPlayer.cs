using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game_Battleship
{
    public class HumanPlayer : Player
    {

        public override bool PlaceShip(Board oceanBoard, Ship ship, int row, int column)
        {
            bool isValidSpot = true;
            // Makes sure the ship is placed within the board radius

            if (ship.Orientation == 'V')
            {
                if (row + (ship.Size) > 10)
                {
                    isValidSpot = false;
                    MessageBox.Show($"Can't place ship here!\n\nNot enough space for {ship.Name}.");
                }

                // Checks if the space is already taken up by a ship
                for (int i = 0; i < ship.Size; i++)
                {
                    if (isValidSpot)
                    {
                        if (oceanBoard.GetCoordinateValue(row + i, column) != 0)
                        {
                            isValidSpot = false;
                            MessageBox.Show("Can't place ship here!\n\nSpot is taken by another ship.");
                        }
                    }
                }

                //if ship spot is valid place ship
                if (isValidSpot)
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        oceanBoard.SetCoordinates(row + i, column, ship.Id);
                        oceanBoard.getTile(row + i, column).Content = ship.Name;
                        ship.IsPlaced = true;
                    }
                }
            }


            else
            {
                // Makes sure the ship is placed within the board radius
                if (column + (ship.Size) > 10)
                {
                    isValidSpot = false;
                    MessageBox.Show($"Can't place ship here!\n\nNot enough space for {ship.Name}.");
                }

                // Checks if the space is already taken up by a ship
                for (int i = 0; i < ship.Size; i++)
                {
                    if (isValidSpot)
                    {
                        if (oceanBoard.GetCoordinateValue(row, column + i) != 0)
                        {
                            isValidSpot = false;
                            MessageBox.Show("Can't place ship here!\n\nSpot is taken by another ship.");
                        }
                    }
                }

                // If ship spot is valid place ship

                if (isValidSpot)
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        oceanBoard.SetCoordinates(row, column + i, ship.Id);
                        oceanBoard.getTile(row, column + i).Content = ship.Name;
                        ship.IsPlaced = true;
                    }
                }
            }

            return isValidSpot;
        }

        public HumanPlayer(string name, Board oceanBoard, Board targetBoard) : base(name, oceanBoard, targetBoard)
        {

        }
    }
}
