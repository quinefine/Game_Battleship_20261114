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
        public BoardGame(Player playerOne, Player playerTwo)
        {
            //this is a comment
            InitializeComponent();
            game.PlayerOne = playerOne;
            game.PlayerTwo = playerTwo;
        }
        // Creates a new game object

        Game game = new Game("Player 1 placing");

        // Creates the counter to allow players to place the ships in a fixed linear order        (TEMPORARY!)

        public int playerOneShipIndex = 0;
        public int playerTwoShipIndex = 0;
        public int currentShip = 0;

        // Creates the labels that hold the win counts

        private Label playerOneWinCountLabel;
        private Label playerTwoWinCountLabel;

        private Button actionButton;


        private void BoardGameWindow_Loaded(object sender, RoutedEventArgs e)
        {

            // set up rows and columns

            createGrid();

            // TODO done
            // Creates the labels above the boards that will manage the scoring

            createWinCountLabels();

            updateWinLabels();

            // Creates the 4 boards required to play battleship

            CreateBoard("playerOneOcean", "Player 1 Ocean", 3, 1, game.PlayerOne.OceanBoard);
            CreateBoard("playerTwoOcean","Player 2 Ocean" , 3, 13, game.PlayerTwo.OceanBoard);

            CreateBoard("playerOneTarget", "Player 1 Target", 17, 1, game.PlayerOne.TargetBoard);
            CreateBoard("playerTwoTarget", "Player 2 Target", 17, 13, game.PlayerTwo.TargetBoard);


            game.PlayerTwo.OceanBoard.changeVisibilty();
            game.PlayerTwo.TargetBoard.changeVisibilty();

            // Make concede button, set text, location and tie click event to method
            createActionButton();
            createConcedeButton();

            checkPlayerOne();
        }

        private void checkPlayerOne()
        {
            // Places PLAYER ONE ships first in order from largest to smallest randomly
            if (game.PlayerOne is CPUPlayer)
            {
                game.PlayerOne.PlaceShip();
                btnAction_Click(actionButton, new RoutedEventArgs());
            }
        }

        private void checkPlayerTwo()
        {
            // Places PLAYER TWO ships first in order from largest to smallest randomly
            if (game.PlayerTwo is CPUPlayer)
            {
                game.PlayerTwo.PlaceShip();
                btnAction_Click(actionButton, new RoutedEventArgs());
            }
        }

        private void createGrid()
        {
            for (int i = 0; i < 30; i++)
            {
                MyGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 24; i++)
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

            Grid.SetRow(concede, 28);
            Grid.SetColumn(concede, 10);
            Grid.SetColumnSpan(concede, 3);

            concede.HorizontalAlignment = HorizontalAlignment.Center;
            MyGrid.Children.Add(concede);
            concede.Click += btnConcede_Click;
        }

        private void createActionButton()
        {
            Button action = new Button();
            action.Content = "done placing ships";

            Grid.SetRow(action, 14);
            Grid.SetColumn(action, 10);
            Grid.SetColumnSpan(action, 4);

            action.HorizontalAlignment = HorizontalAlignment.Center;
            MyGrid.Children.Add(action);
            action.IsEnabled = false;
            action.Click += btnAction_Click;
            actionButton = action;
        }

        private void updateWinLabels()
        {
            if (game.PlayerOne is HumanPlayer)
            {
                playerOneWinCountLabel.Content = $"{game.PlayerOne.Name} \t Wins: {game.PlayerOne.WinCount}";
            }
            if (game.PlayerTwo is HumanPlayer)
            {
                playerTwoWinCountLabel.Content = $"{game.PlayerTwo.Name} \t Wins: {game.PlayerTwo.WinCount}";
            }
            
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

            currentBoard.setTitle(title);

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

                currentBoard.addLabel(rowLetter, false, r);

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

                currentBoard.addLabel(colNumber, true, c);
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

                    tile.YLocation = tile.numberToAlphabet(r + 1);
                    tile.XLocation = (c + 1).ToString();
                    tile.Location = boardName;
                    tile.Background = Brushes.Aqua;

                    //put tile into array (to get it in the future)

                    currentBoard.addTile(tile);

                    // Names the tile
                    tile.Click += tile_Click;

                    MyGrid.Children.Add(tile);

                }
            }
        }

        public void CreatePlaceShipButtons(int rowStart, int colStartLeft, int colStartCenter, int colStartRight)
        {
            // this is base implementation for a future feature where players can pick ships
            for (int i = 0; i < 5; i++)
            {
                Button button = new Button();
                // Positions the tile within the grid starting in the top left corner

                Grid.SetRow(button, rowStart);
                Grid.SetColumn(button, colStartLeft + i);
                switch (i)
                {
                    case 0: button.Click += setDestroyer_Click; break;
                    case 1: button.Click += setSubmarine_Click; break;
                    case 2: button.Click += setCruiser_Click; break;
                    case 3: button.Click += setBattleShip_Click; break;
                    case 4: button.Click += setCarrier_Click; break;
                }
                MyGrid.Children.Add(button);
            }
        }

        private void tile_Click(object sender, RoutedEventArgs e)
        {

            Tile clickedTile = (Tile)sender;
            // Converts displayed coordinates to array indexes
            int row = clickedTile.AlphabetToNumber(clickedTile.YLocation)-1;
            int column = int.Parse(clickedTile.XLocation)-1;

            Ship ship;

            Board currentBoard;
            // Select the player and their next ship from the clicked ocean board

            if (clickedTile.Location == "playerOneOcean" && game.PlayerOne is HumanPlayer && game.getGameState() == "Player 1 placing")
            {
                if (playerOneShipIndex < 5)
                {
                    if (game.PlayerOne.PlaceShip(game.PlayerOne.OceanBoard, game.PlayerOne.Ships[playerOneShipIndex], row, column))
                    {
                        playerOneShipIndex++;
                        if(CheckPlacePhaseEnd(game.PlayerOne))
                        {
                            actionButton.IsEnabled = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("you already placed all your ships");
                }
            }

            else if (clickedTile.Location == "playerTwoOcean" && game.PlayerTwo is HumanPlayer && game.getGameState() == "Player 2 placing")
            {
                if (playerTwoShipIndex < 5)
                {
                    if (game.PlayerTwo.PlaceShip(game.PlayerTwo.OceanBoard, game.PlayerTwo.Ships[playerTwoShipIndex], row, column))
                    {
                        playerTwoShipIndex++;
                        if (CheckPlacePhaseEnd(game.PlayerTwo))
                        {
                            actionButton.IsEnabled = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("you already placed all your ships");
                }
            }

            else if (clickedTile.Location == "playerOneTarget" && game.getGameState() == "Player 1 attacking")
            {
                int tileContents;
                currentBoard = game.PlayerOne.TargetBoard;
                tileContents = game.PlayerTwo.OceanBoard.GetCoordinateValue(row,column);
                if (tileContents == 0) // 0 is water
                {
                    clickedTile.Background = Brushes.White;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    clickedTile.Content = "Miss";
                }
                else
                {
                    Ship hitShip = game.PlayerOne.Ships[tileContents-1];
                    clickedTile.Content = hitShip.Name;
                    clickedTile.Background = Brushes.Red;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    hitShip.UpdateDamage();
                    int sunkShips = 0;
                    foreach (Ship s in game.PlayerOne.Ships)
                    {
                        if (s.IsSunk)
                        {
                            actionButton.IsEnabled = true;
                        }
                    }

                    if (sunkShips == 5)
                    {
                        game.EndGame(game.PlayerOne);
                        updateWinLabels();
                    }
                    else
                    {
                        game.SwitchTurn();
                    }
                }

                actionButton.IsEnabled = true;
                game.setGameState("Inbetween 1");
                actionButton.Content = "Show Board";
                clickedTile.Click -= tile_Click;
            }

            else if (clickedTile.Location == "playerTwoTarget" && game.getGameState() == "Player 2 attacking")
            {
                int tileContents;
                currentBoard = game.PlayerTwo.TargetBoard;
                tileContents = game.PlayerOne.OceanBoard.GetCoordinateValue(row, column);
                if (tileContents == 0)
                {
                    clickedTile.Background = Brushes.White;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0,0,0));
                    clickedTile.Content = "Miss";
                }
                else
                {
                    Ship hitShip = game.PlayerTwo.Ships[tileContents-1]; // tile contents can hold water, ships don't, -1 is used to line these up
                    clickedTile.Content = hitShip.Name;
                    clickedTile.Background = Brushes.Red;
                    clickedTile.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    hitShip.UpdateDamage();
                    int sunkShips = 0;
                    foreach (Ship s in game.PlayerTwo.Ships)
                    {
                        if (s.IsSunk)
                        {
                            sunkShips++;
                        }
                    }

                    if (sunkShips == 5)
                    {
                        game.EndGame(game.PlayerTwo);
                        updateWinLabels();
                    }
                    else
                    {
                        game.SwitchTurn();
                    }
                }

                actionButton.IsEnabled = true;
                game.setGameState("Inbetween 2");
                actionButton.Content = "Show Board";
                clickedTile.Click -= tile_Click;
            }
        }

        private void btnConcede_Click(object sender, RoutedEventArgs e)
        {
            //concede closes the game
            //To Do: Prompt user with yes no to quit
            this.Close();
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            switch (game.getGameState())
            {
                case "Player 1 placing": 
                    game.setGameState("Player 2 placing");
                    actionButton.Content = "Start game";
                    changeAllVisibility();
                    checkPlayerTwo();
                    break;
                case "Player 2 placing":
                    game.setGameState("Inbetween2");
                    actionButton.Content = "Show board";
                    changeAllVisibility();
                    break;
                //case "Player 1 attacking":
                //    game.setGameState("Inbetween 1");
                //    actionButton.Content = "Show board";
                //    break;
                case "Inbetween 1":
                    game.setGameState("Player 2 attacking");
                    actionButton.Content = "Choose your fighter";
                    changeAllVisibility();
                    break;
                //case "Player 2 attacking":
                //    game.setGameState("Inbetween 2");
                //    actionButton.Content = "Show board";
                //    break;
                case "Inbetween 2":
                    game.setGameState("Player 1 attacking");
                    actionButton.Content = "Choose your fighter";
                    changeAllVisibility();
                    break;
            }
            actionButton.IsEnabled = false;
        }

        private bool CheckPlacePhaseEnd(Player player)
        {
            bool done = false;
            int placed = 0;
            foreach (Ship s in player.Ships)
            {
                if (s.IsPlaced == true)
                {
                    placed++;
                }
            }
            if (placed == 5)
            {
                done = true;
            }
            return done;
        }

        private void setDestroyer_Click(object sender, RoutedEventArgs e)
        {
            currentShip = 0;
        }

        private void setSubmarine_Click(object sender, RoutedEventArgs e)
        {
            currentShip = 1;
        }

        private void setCruiser_Click(object sender, RoutedEventArgs e)
        {
            currentShip = 2;
        }

        private void setBattleShip_Click(object sender, RoutedEventArgs e)
        {
            currentShip = 3;
        }

        private void setCarrier_Click(object sender, RoutedEventArgs e)
        {
            currentShip = 4;
        }

        private void changeAllVisibility()
        {
            game.PlayerOne.OceanBoard.changeVisibilty();
            game.PlayerOne.TargetBoard.changeVisibilty();
            game.PlayerTwo.OceanBoard.changeVisibilty();
            game.PlayerTwo.TargetBoard.changeVisibilty();
        }


        //still need to add:
        //Done placing ships button while gameState = placing
    }
}
