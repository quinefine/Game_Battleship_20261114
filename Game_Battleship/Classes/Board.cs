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
        private int[,] coordinates = new int[10, 10];

            public void SetCoordinates(int row, int col, int value)
            {
                coordinates[row, col] = value;
            }

            public int GetCoordinateValue(int row, int col)
            {
                return coordinates[row, col];
            }

        // TODO: Show team coordinateLabels array

        private Tile[,] tiles = new Tile[10, 10];
            public void addTile(Tile tile)
            {
                tiles[tile.AlphabetToNumber(tile.YLocation)-1, int.Parse(tile.XLocation) - 1] = tile;
            }
            public Tile getTile(int x, int y)
            {
                return tiles[x, y];
            }
        private Label[,] labels = new Label[2, 10];
        private Label title;

            public void setTitle(Label titl)
            {
                title = titl;
            }

            public Label getTitle()
            {
                return title;
            }
        public void addLabel(Label label, bool isNumber, int num)
            {
                if (isNumber)
                {
                    labels[0, num] = label;
                }
                else
                {
                    labels[1, num] = label;
                }
            }
        public Label getLabel(bool isNumber, int num)
            {
                if (isNumber)
                {
                    return labels[0, num];
                }
                else
                {
                    return labels[1, num];
                }
            }
        // Allows the user to place ships on the board

        //TODO
        // Randomizes ship locations on a player's board

        public bool RandomizeShipPlacement(Board oceanBoard, Ship ship)
        {

            // Limits the number of random placements attempted

            const int MAX_ATTEMPTS = 1000;
            Random rand = new Random();

            for (int i = 0; i < MAX_ATTEMPTS; i++)
            {

                // Randomizes the orientation of the ship

                if (rand.Next(2) == 0)
                {
                    ship.Orientation = 'H';
                }

                else
                {
                    ship.Orientation = 'V';
                }

                // Picks a random starting location

                int rowLocation = rand.Next(10);
                int colLocation = rand.Next(10);

                // Variable for checking that the ship can be placed there

                bool isValidSpot = true;

                // Ensures that no part of the ship can go outside the board

                if (ship.Orientation == 'V' && rowLocation + ship.Size > 10)
                    isValidSpot = false;

                else if (ship.Orientation == 'H' && colLocation + ship.Size > 10)
                    isValidSpot = false;

                // Checks to make sure that no part of the randomly placed ship conflicts with another ship

                for (int ii = 0; ii < ship.Size && isValidSpot == true; ii++)
                {

                    int rowToCheck = rowLocation;
                    int colToCheck = colLocation;

                    // Moves down a row for each part of a vertical ship

                    if (ship.Orientation == 'V')
                        rowToCheck += ii;

                    // Moves to the right of one column for each part of a horizontal ship
                    else if (ship.Orientation == 'H')
                        colToCheck += ii;

                    // A nonzero value means another ship already occupies this cell

                    if (oceanBoard.coordinates[rowToCheck, colToCheck] != 0)
                        isValidSpot = false;
                }

                // Adds ship information to board and tiles

                if (isValidSpot == true)
                {
                    for (int ii = 0; ii < ship.Size; ii++)
                    {

                        int shipRow = rowLocation;
                        int shipCol = colLocation;

                        // Checks the location of each part of the ship

                        if (ship.Orientation == 'V')
                        {
                            shipRow += ii;
                        }

                        else
                        {
                            shipCol += ii;
                        }

                        // Stores the ship's ID

                        oceanBoard.coordinates[shipRow, shipCol] = ship.Id;

                        // Stores the ship's name

                        oceanBoard.tiles[shipRow, shipCol].Content = ship.Name;

                    }

                    return true; 

                }

            }

            return false;

        }

        public void changeVisibilty()
        {
            foreach (Tile tile in tiles)
            {
                if (tile.Visibility == Visibility.Hidden)
                {
                    tile.Visibility = Visibility.Visible;
                }
                else
                {
                    tile.Visibility = Visibility.Hidden;
                }
            }
            foreach (Label label in labels)
            {
                if (label.Visibility == Visibility.Hidden)
                {
                    label.Visibility = Visibility.Visible;
                }
                else
                {
                    label.Visibility = Visibility.Hidden;
                }
            }
            if (title.Visibility == Visibility.Hidden)
            {
                title.Visibility = Visibility.Visible;
            }
            else
            {
                title.Visibility = Visibility.Hidden;
            }
        }
    }
}
