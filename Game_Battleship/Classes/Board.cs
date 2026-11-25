using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Game_Battleship
{
    public class Board
    {

        public Board()
        {  
        }
        public int[,] coordinates { get; set; } = new int[10, 10];

        // TODO: Show team coordinateLabels array

        private Tile[,] tiles { get; set; } = new Tile[10, 10];
        public void addTile(Tile tile)
        {
            tiles[tile.AlphabetToNumber(tile.yLocation)-1, int.Parse(tile.xLocation) - 1] = tile;
        }
        public Tile getTile(int x, int y)
        {
            return tiles[x, y];
        }
        // Allows the user to place ships on the board
        
        public bool PlaceShips(Board oceanBoard, Ship ship, int row, int column)
        {
            bool isValidSpot = true;
            // Makes sure the ship is placed within the board radius

            if (ship.orientation == 'V')
            {
                if (row + (ship.size) > 10)
                {
                    isValidSpot = false;
                    MessageBox.Show($"Can't place ship here!\n\nNot enough space for {ship.name}.");
                }

                // Checks if the space is already taken up by a ship
                for (int i = 0; i < ship.size; i++)
                {
                    if (isValidSpot)
                    {
                        if (oceanBoard.coordinates[row + i, column] != 0)
                        {
                            isValidSpot = false;
                            MessageBox.Show("Can't place ship here!\n\nSpot is taken by another ship.");
                        }
                    }
                }

                //if ship spot is valid place ship
                if (isValidSpot)
                {
                    for (int i = 0; i < ship.size; i++)
                    {
                        oceanBoard.coordinates[row + i, column] = ship.id;
                        oceanBoard.tiles[row + i, column].Content = ship.name;
                    }
                }
            }


            else {
                // Makes sure the ship is placed within the board radius
                if (column + (ship.size) > 10)
                {
                    isValidSpot = false;
                    MessageBox.Show($"Can't place ship here!\n\nNot enough space for {ship.name}.");
                }

                // Checks if the space is already taken up by a ship
                for (int i = 0; i < ship.size; i++)
                {
                    if (isValidSpot)
                    {
                        if (coordinates[row, column + i] != 0)
                        {
                            isValidSpot = false;
                            MessageBox.Show("Can't place ship here!\n\nSpot is taken by another ship.");
                        }
                    }
                }

                // If ship spot is valid place ship

                if (isValidSpot)
                {
                    for (int i = 0; i < ship.size; i++)
                    {
                        coordinates[row, column + i] = ship.id;
                        tiles[row, column + i].Content = ship.name;
                    }
                }
            }

            return isValidSpot;
        }

        //TODO
        // Randomizes ship locations on a player's board

        public bool RandomizeShipPlacement(Board oceanBoard, Ship ship, Random random)
        {

            // Limits the number of random placements attempted

            const int MAX_ATTEMPTS = 1000;

            for (int i = 0; i < MAX_ATTEMPTS; i++)
            {

                // Randomizes the orientation of the ship

                if (random.Next(2) == 0)
                {
                    ship.orientation = 'H';
                }

                else
                {
                    ship.orientation = 'V';
                }

                // Picks a random starting location

                int rowLocation = random.Next(10);
                int colLocation = random.Next(10);

                // Variable for checking that the ship can be placed there

                bool isValidSpot = true;

                // Ensures that no part of the ship can go outside the board

                if (ship.orientation == 'V' && rowLocation + ship.size > 10)
                    isValidSpot = false;

                else if (ship.orientation == 'H' && colLocation + ship.size > 10)
                    isValidSpot = false;

                // Checks to make sure that no part of the randomly placed ship conflicts with another ship

                for (int ii = 0; ii < ship.size && isValidSpot == true; ii++)
                {

                    int rowToCheck = rowLocation;
                    int colToCheck = colLocation;

                    // Moves down a row for each part of a vertical ship

                    if (ship.orientation == 'V')
                        rowToCheck += ii;

                    // Moves to the right of one column for each part of a horizontal ship
                    else if (ship.orientation == 'H')
                        colToCheck += ii;

                    // A nonzero value means another ship already occupies this cell

                    if (oceanBoard.coordinates[rowToCheck, colToCheck] != 0)
                        isValidSpot = false;
                }

                // Adds ship information to board and tiles

                if (isValidSpot == true)
                {
                    for (int ii = 0; ii < ship.size; ii++)
                    {

                        int shipRow = rowLocation;
                        int shipCol = colLocation;

                        // Checks the location of each part of the ship

                        if (ship.orientation == 'V')
                        {
                            shipRow += ii;
                        }

                        else
                        {
                            shipCol += ii;
                        }

                        // Stores the ship's ID

                        oceanBoard.coordinates[shipRow, shipCol] = ship.id;

                        // Stores the ship's name

                        oceanBoard.tiles[shipRow, shipCol].Content = ship.name;

                    }

                    return true; 

                }

            }

            return false;

        }


    }
}
