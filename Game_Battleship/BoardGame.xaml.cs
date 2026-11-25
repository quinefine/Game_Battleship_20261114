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
        public BoardGame(Player playerOne, Player PlayerTwo)
        {
            InitializeComponent();
            game.playerOne = playerOne;
            game.playerTwo = PlayerTwo;
        }
        // Creates a new game object

        Game game = new Game("Placing");

        // Creates the counter to allow players to place the ships in a fixed linear order        (TEMPORARY!)

        public int playerOneShipIndex = 0;
        public int playerTwoShipIndex = 0;

        // Creates the labels that hold the win counts

        private Label playerOneWinCountLabel;
        private Label playerTwoWinCountLabel;


        private void BoardGameWindow_Loaded(object sender, RoutedEventArgs e)
        {

            // set up rows and columns

            createGrid();

            // TODO done
            // Creates the labels above the boards that will manage the scoring

            createWinCountLabels();

            updateWinLabels();

            // Creates the 4 boards required to play battleship

            CreateBoard("playerOneOcean", "Player 1 Ocean", 3, 1, game.playerOne.oceanBoard);
            CreateBoard("playerOneTarget", "Player 1 Target", 3, 13, game.playerOne.targetBoard);

            CreateBoard("playerTwoOcean", "Player 2 Ocean", 16, 1, game.playerTwo.oceanBoard);
            CreateBoard("playerTwoTarget", "Player 2 Target", 16, 13, game.playerTwo.targetBoard);

            //TODO done
            // Create the randomizer

            // Places PLAYER ONE ships first in order from largest to smallest randomly

            if (placeShips(game.playerOne))
            {
                playerOneShipIndex = game.playerOne.ships.Length;
            }

            // Places PLAYER TWO ships first in order from largest to smallest randomly
            if (placeShips(game.playerTwo))
            {
                playerTwoShipIndex = game.playerTwo.ships.Length;
            }

            // Make concede button, set text, location and tie click event to method

            createConcedeButton();
        }

        private void createGrid()
        {
            for (int i = 0; i < 26; i++)
            {
                MyGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 23; i++)
            {
                MyGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void createWinCountLabels()
        {
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
        }

        private void createConcedeButton()
        {
            Button concede = new Button();
            concede.Content = "Concede";

            Grid.SetRow(concede, 13);
            Grid.SetColumn(concede, 0);
            Grid.SetColumnSpan(concede, 21);

            concede.HorizontalAlignment = HorizontalAlignment.Center;
            MyGrid.Children.Add(concede);
            concede.Click += btnConcede_Click;
        }

        private bool placeShips(Player player)
        {
            Random rand = new Random();
            if (player.GetType().ToString() == "Game_Battleship.CPUPlayer")
            {
                foreach (Ship ship in player.ships.OrderByDescending(s => s.size))
                {
                    if (!player.oceanBoard.RandomizeShipPlacement(player.oceanBoard, ship, rand))
                    {
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        private void updateWinLabels()
        {
            playerOneWinCountLabel.Content = $"{game.playerOne.name} \t Wins: {game.playerOne.winCount}";
            playerTwoWinCountLabel.Content = $"{game.playerTwo.name} \t Wins: {game.playerTwo.winCount}";
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

            // TODO done
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

                    currentBoard.addTile(tile);

                    // Names the tile
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

            Ship ship;

            Board currentBoard;
            // Select the player and their next ship from the clicked ocean board




            if (clickedTile.location == "playerOneOcean")
            {
                currentBoard = game.playerOne.oceanBoard;
                if (playerOneShipIndex < 5)
                {
                    ship = game.playerOne.ships[playerOneShipIndex];
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
                    ship = game.playerTwo.ships[playerTwoShipIndex];
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
                if (tileContents == 0) // 0 is water
                {
                    clickedTile.Background = Brushes.White;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    clickedTile.Content = "Miss";
                }
                else
                {
                    Ship hitShip = game.playerOne.ships[tileContents-1];
                    clickedTile.Content = hitShip.name;
                    clickedTile.Background = Brushes.Red;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    hitShip.UpdateDamage();
                    int sunkShips = 0;
                    foreach (Ship s in game.playerOne.ships)
                    {
                        if (s.isSunk)
                        {
                            sunkShips++;
                        }
                    }

                    if (sunkShips == 5)
                    {
                        game.EndGame(game.playerOne);
                        updateWinLabels();
                    }
                    else
                    {
                        game.SwitchTurn();
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
                    Ship hitShip = game.playerTwo.ships[tileContents-1]; // tile contents can hold water, ships don't, -1 is used to line these up
                    clickedTile.Content = hitShip.name;
                    clickedTile.Background = Brushes.Red;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    hitShip.UpdateDamage();
                    int sunkShips = 0;
                    foreach (Ship s in game.playerTwo.ships)
                    {
                        if (s.isSunk)
                        {
                            sunkShips++;
                        }
                    }

                    if (sunkShips == 5)
                    {
                        game.EndGame(game.playerTwo);
                        updateWinLabels();
                    }
                    else
                    {
                        game.SwitchTurn();
                    }
                }
            }
            clickedTile.Click -= tile_Click;
        }

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
