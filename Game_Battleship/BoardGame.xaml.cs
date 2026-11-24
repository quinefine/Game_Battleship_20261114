using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Battleship
{
    /// <summary>
    /// Interaction logic for BoardGame.xaml
    /// </summary>
    public partial class BoardGame : Window
    {
        public BoardGame()
        {
            InitializeComponent();
        }

        // Creates a new game object

        Game game = new Game();

        // Gives each player their fleet

        public Ship[] playerOneShips =
        {
            new Ship { name = "Destroyer", size = 2, id = 1},
            new Ship { name = "Submarine", size = 3, id = 2 },
            new Ship { name = "Cruiser", size = 3, id = 3 },
            new Ship { name = "Battleship", size = 4, id = 4 },
            new Ship { name = "Carrier", size = 5, id = 5 }
        };

        public Ship[] playerTwoShips =
        {
            new Ship { name = "Destroyer", size = 2, id = 1},
            new Ship { name = "Submarine", size = 3, id = 2 },
            new Ship { name = "Cruiser", size = 3, id = 3 },
            new Ship { name = "Battleship", size = 4, id = 4 },
            new Ship { name = "Carrier", size = 5, id = 5 }
        };

        // Creates the counter to allow players to place the ships in a fixed linear order        (TEMPORARY!)

        public int playerOneShipIndex = 0;
        public int playerTwoShipIndex = 0;

        // Creates the labels that hold the win counts

        private Label playerOneWinCountLabel;
        private Label playerTwoWinCountLabel;

        private void BoardGameWindow_Loaded(object sender, RoutedEventArgs e)
        {

            // set up rows and columns

            for (int i = 0; i < 26; i++)
            {
                MyGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 23; i++)
            {
                MyGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Creates both players

            game.playerOne = new HumanPlayer();
            game.playerTwo = new HumanPlayer();

            // TODO
            // Creates the labels above the boards that will manage the scoring

            playerOneWinCountLabel = new Label();
            playerTwoWinCountLabel = new Label();

            playerOneWinCountLabel.HorizontalAlignment = HorizontalAlignment.Center;
            playerTwoWinCountLabel.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetRow(playerOneWinCountLabel, 0);
            Grid.SetColumn(playerOneWinCountLabel, 0);
            Grid.SetColumnSpan(playerOneWinCountLabel, 10);

            Grid.SetRow(playerTwoWinCountLabel, 0);
            Grid.SetColumn(playerTwoWinCountLabel, 11);
            Grid.SetColumnSpan(playerTwoWinCountLabel, 10);

            MyGrid.Children.Add(playerOneWinCountLabel);
            MyGrid.Children.Add(playerTwoWinCountLabel);

            game.WinCountsChanged += Game_WinCountsChanged;
            UpdateWinCountLabels();

            // Sets the game state to the placing phase

            game.gameState = "Placing";

            // Set player variables

            game.playerOne.name = "Player 1";
            game.playerTwo.name = "Player 2";

            // Creates boards for each player

            game.playerOne.oceanBoard = new Board();
            game.playerOne.targetBoard = new Board();

            game.playerTwo.oceanBoard = new Board();
            game.playerTwo.targetBoard = new Board();

            // Creates arrays for boards

            game.playerOne.oceanBoard.tiles = new Tile[10, 10];
            game.playerTwo.oceanBoard.tiles = new Tile[10, 10];
            game.playerOne.targetBoard.tiles = new Tile[10, 10];
            game.playerTwo.targetBoard.tiles = new Tile[10, 10];

            // Creates the 4 boards required to play battleship

            CreateBoard("playerOneOcean", "Player 1 Ocean", 3, 1, game.playerOne.oceanBoard);
            CreateBoard("playerOneTarget", "Player 1 Target", 3, 13, game.playerOne.targetBoard);

            CreateBoard("playerTwoOcean", "Player 2 Ocean", 16, 1, game.playerTwo.oceanBoard);
            CreateBoard("playerTwoTarget", "Player 2 Target", 16, 13, game.playerTwo.targetBoard);

            //TODO
            // Create the randomizer

            Random randomizer = new Random();

            // Places PLAYER ONE ships first in order from largest to smallest randomly

            foreach (Ship ship in playerOneShips.OrderByDescending(s => s.size))
            {
                if (!game.playerOne.oceanBoard.RandomizeShipPlacement(game.playerOne.oceanBoard, ship, randomizer))
                {
                    return;
                }
            }

            // Places PLAYER TWO ships first in order from largest to smallest randomly

            foreach (Ship ship in playerTwoShips.OrderByDescending(s => s.size))
            {
                if (!game.playerTwo.oceanBoard.RandomizeShipPlacement(game.playerTwo.oceanBoard, ship, randomizer))
                {
                    return;
                }
            }

            // Saves the coordinates for tiles

            playerOneShipIndex = playerOneShips.Length;
            playerTwoShipIndex = playerTwoShips.Length;

            // Make concede button, set text, location and tie click event to method

            Button concede = new Button();
            concede.Content = "Concede";

            Grid.SetRow(concede, 13);
            Grid.SetColumn(concede, 0);
            Grid.SetColumnSpan(concede, 21);

            concede.HorizontalAlignment = HorizontalAlignment.Center;
            MyGrid.Children.Add(concede);
            concede.Click += btnConcede_Click;

        }

        private void UpdateWinCountLabels()
        {
            playerOneWinCountLabel.Content = $"Player One Wins: {Game.playerOneWins}";
            playerTwoWinCountLabel.Content = $"Player Two Wins: {Game.playerTwoWins}";
        }

        private void Game_WinCountsChanged(object sender, EventArgs e)
        {
            UpdateWinCountLabels();
        }

        public void CreateBoard(string boardName, string boardTitle, int rowStart, int colStart, Board currentBoard)
        {

            // Creates appropriate label above each board

            Label title = new Label();

            title.Content = boardTitle;
            title.HorizontalContentAlignment = HorizontalAlignment.Center;
            
            //when rowStart is passed as a 0, it doesn't show the labels, when rowStart is passed as 1 then subtracted by 1, they show up
            
            Grid.SetRow(title, rowStart - 2);             
            Grid.SetColumn(title, colStart);
            Grid.SetColumnSpan(title, 10);

            MyGrid.Children.Add(title);

            // TODO
            // Generates the row letter coordinates (A-J)

            for (int r = 0; r < 10; r++)
            {
                Label rowLetter = new Label();

                rowLetter.Content = new Tile().numberToAlphabet(r + 1);

                rowLetter.HorizontalContentAlignment = HorizontalAlignment.Center;
                rowLetter.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetRow(rowLetter, rowStart + r);
                Grid.SetColumn(rowLetter, colStart - 1);

                MyGrid.Children.Add(rowLetter);

            }

            // Generates the column number coordinates (1-10)

            for (int c = 0; c < 10; c++)
            {
                Label colNumber = new Label();

                colNumber.Content = (c + 1).ToString();

                colNumber.HorizontalContentAlignment = HorizontalAlignment.Center;
                colNumber.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetRow(colNumber, rowStart - 1);
                Grid.SetColumn(colNumber, colStart + c);

                MyGrid.Children.Add(colNumber);

            }

            // Generates a board for the user/users playing

            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {

                    Tile tile = new Tile();

                    // Positions the tile within the grid starting in the top left corner

                    Grid.SetRow(tile, rowStart + r);
                    Grid.SetColumn(tile, colStart + c);

                    // Stores the ship coordinates

                    tile.yLocation = tile.numberToAlphabet(r + 1);
                    tile.xLocation = (c + 1).ToString();
                    tile.location = boardName;
                    tile.Background = Brushes.Aqua;

                    //put tile into array (to get it in the future)

                    currentBoard.tiles[r,c] = tile;

                    // Names the tile
                    tile.Name = boardName + "Tile_" + r + "_" + c;
                    tile.Click += tile_Click;

                    MyGrid.Children.Add(tile);

                }
            }
        }

        private void tile_Click(object sender, RoutedEventArgs e)
        {

            Tile clickedTile = (Tile)sender;
            // Converts displayed coordinates to array indexes
            int row = clickedTile.AlphabetToNumber(clickedTile.yLocation)-1;
            int column = int.Parse(clickedTile.xLocation)-1;

            Ship ship = new Ship();

            Board currentBoard = new Board();
            // Select the player and their next ship from the clicked ocean board




            if (clickedTile.location == "playerOneOcean")
            {
                currentBoard = game.playerOne.oceanBoard;
                if (playerOneShipIndex < 5)
                {
                    ship = playerOneShips[playerOneShipIndex];
                    if (currentBoard.PlaceShips(currentBoard, ship, row, column))
                    {
                        playerOneShipIndex++;
                    }
                }
                else
                {
                    MessageBox.Show("you already placed all your ships");
                }
            }

            else if (clickedTile.location == "playerTwoOcean")
            {
                currentBoard = game.playerTwo.oceanBoard;
                if (playerTwoShipIndex < 5)
                {
                    ship = playerTwoShips[playerTwoShipIndex];
                    if (currentBoard.PlaceShips(currentBoard, ship, row, column))
                    {
                        playerTwoShipIndex++;
                    }
                }
                else
                {
                    MessageBox.Show("you already placed all your ships");
                }
            }

            else if (clickedTile.location == "playerOneTarget")
            {
                int tileContents;
                currentBoard = game.playerOne.targetBoard;
                tileContents = game.playerTwo.oceanBoard.coordinates[row, column];
                if (tileContents == 0)
                {
                    clickedTile.Background = Brushes.White;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    clickedTile.Content = "Miss";
                }
                else if (clickedTile.Content != "")
                {
                    Ship hitShip = playerTwoShips[tileContents-1];
                    clickedTile.Content = hitShip.name;
                    clickedTile.Background = Brushes.Red;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    hitShip.UpdateDamage();
                    int sunkShips = 0;
                    foreach (Ship s in playerTwoShips)
                    {
                        if (s.isSunk)
                        {
                            sunkShips++;
                        }
                    }

                    if (sunkShips == 5)
                    {
                        game.EndGame(1);
                    }
                    else
                    {
                        game.SwitchTurn();
                        //change
                    }
                }
            }

            else if (clickedTile.location == "playerTwoTarget")
            {
                int tileContents;
                currentBoard = game.playerTwo.targetBoard;
                tileContents = game.playerOne.oceanBoard.coordinates[row, column];
                if (tileContents == 0)
                {
                    clickedTile.Background = Brushes.White;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0,0,0));
                    clickedTile.Content = "Miss";
                }
                else
                {
                    Ship hitShip = playerOneShips[tileContents-1];
                    clickedTile.Content = hitShip.name;
                    clickedTile.Background = Brushes.Red;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    hitShip.UpdateDamage();
                    int sunkShips = 0;
                    foreach (Ship s in playerOneShips)
                    {
                        if (s.isSunk)
                        {
                            sunkShips++;
                        }
                    }

                    if (sunkShips == 5)
                    {
                        game.EndGame(2);
                    }
                }
            }
        }

        //public void Update

        //public bool CheckSpotForShip(Board oceanBoard, Ship ship, int row, int col)
        //{
        //    bool spotTaken = false;
        //    if (!oceanBoard.PlaceShips(ship, row, col))
        //    {
        //        spotTaken = true;
        //    }

        //    return spotTaken;
        //}

        private void btnConcede_Click(object sender, RoutedEventArgs e)
        {
            //concede closes the game
            //To Do: Prompt user with yes no to quit
            this.Close();
        }

        //still need to add:
        //Done placing ships button while gameState = placing
    }
}
