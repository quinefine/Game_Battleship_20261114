using System;
using System.Collections.Generic;
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
    /// Interaction logic for StartGameWindow.xaml
    /// </summary>
    public partial class StartGameWindow : Window
    {
        public StartGameWindow()
        {
            InitializeComponent();
        }

        //still need to work on the size of the difficulty listbox for CPUs, shrinks when the window is resized by the user

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        private void btnHumanPlayer_Click(object sender, RoutedEventArgs e)
        {
            SetPlayer1Human();
        }

        private void btnCPUPlayer_Click(object sender, RoutedEventArgs e)
        {
            SetPlayer1CPU();
        }

        private void btnHumanPlayer2_Click(object sender, RoutedEventArgs e)
        {
            SetPLayer2Human();
        }

        private void btnCPUPlayer2_Click(object sender, RoutedEventArgs e)
        {
            SetPlayer2CPU();
        }

        private void btnPlaceShips_Click(object sender, RoutedEventArgs e)
        {
            //clicking place ships will take what is in the txtName field and save that as the players name if it is not null
            //if null, player name is Player 1 or Player 2, but this is not needed yet

            //then the start game window will close and a new window with the player's ocean grid appears for them to place their ships



            BoardGame p1Ocean = new BoardGame(DeterminePlayer(1), DeterminePlayer(2));
            p1Ocean.Show();

            this.Close();
        }

        private void SetPlayer1Human()
        {
            //clicking human player makes the human player options available
            //it has been a while but putting all the human options in one box would be more efficient as far as making everything visible/hidden

            lblPlayerName.Visibility = 0;
            txtPlayerName.Visibility = 0;

            //human player button makes CPU options hidden again
            lblDifficulty.Visibility = Visibility.Collapsed;
            lbxDifficulty.Visibility = Visibility.Collapsed;

            if (lblPlayerName2.Visibility == Visibility.Visible || lblDifficulty2.Visibility == Visibility.Visible)
            {
                btnPlaceShips.Visibility = Visibility.Visible;
                btnStartCPUGame.Visibility = Visibility.Collapsed;
            }
        }

        private void SetPlayer1CPU()
        {
            //clicking CPU player button makes the difficulty options available
            //next player button appears when setting up player 1


            lblDifficulty.Visibility = 0;
            lbxDifficulty.Visibility = 0;

            //maybe default difficulty to easy if one isnt chosen?


            //selecting cpu button makes the human options hidden
            lblPlayerName.Visibility = Visibility.Collapsed;
            txtPlayerName.Visibility = Visibility.Collapsed;

            if (lblPlayerName2.Visibility == Visibility.Visible)
            {
                btnPlaceShips.Visibility = Visibility.Visible;
                btnStartCPUGame.Visibility = Visibility.Collapsed;
            }
            else if (lblDifficulty2.Visibility == Visibility.Visible)
            {
                btnPlaceShips.Visibility = Visibility.Collapsed;
                btnStartCPUGame.Visibility = Visibility.Visible;
            }
        }

        private void SetPLayer2Human()
        {
            lblPlayerName2.Visibility = 0;
            txtPlayerName2.Visibility = 0;

            //human player button makes CPU options hidden again
            lblDifficulty2.Visibility = Visibility.Collapsed;
            lbxDifficulty2.Visibility = Visibility.Collapsed;

            if (lblPlayerName.Visibility == Visibility.Visible || lblDifficulty.Visibility == Visibility.Visible)
            {
                btnPlaceShips.Visibility = Visibility.Visible;
                btnStartCPUGame.Visibility = Visibility.Collapsed;
            }
        }

        private void SetPlayer2CPU()
        {
            lblDifficulty2.Visibility = 0;
            lbxDifficulty2.Visibility = 0;


            //selecting cpu button makes the human options hidden
            lblPlayerName2.Visibility = Visibility.Collapsed;
            txtPlayerName2.Visibility = Visibility.Collapsed;

            if (lblPlayerName.Visibility == Visibility.Visible)
            {
                btnPlaceShips.Visibility = Visibility.Visible;
                btnStartCPUGame.Visibility = Visibility.Collapsed;
            }
            else if (lblDifficulty.Visibility == Visibility.Visible)
            {
                btnPlaceShips.Visibility = Visibility.Collapsed;
                btnStartCPUGame.Visibility = Visibility.Visible;
            }
        }

        private Player DeterminePlayer(int playerNumber)
        {


            if (playerNumber == 1)
            {
                
                Player playerOne;
                if (lblPlayerName.Visibility == Visibility.Visible)
                {
                    playerOne = new HumanPlayer(txtPlayerName.Text, new Board(), new Board());
                }
                else
                {
                    playerOne = new CPUPlayer(Enum.GetName(typeof(Difficulty), lbxDifficulty.SelectedIndex) + " CPU", new Board(), new Board());
                }

                return playerOne;
            }
            else
            {
                Player playerTwo;
                if (lblPlayerName2.Visibility == Visibility.Visible)
                {
                    playerTwo = new HumanPlayer(txtPlayerName2.Text, new Board(), new Board());
                }
                else
                {
                    playerTwo = new CPUPlayer(Enum.GetName(typeof(Difficulty), lbxDifficulty2.SelectedIndex) + " CPU", new Board(), new Board());
                }

                return playerTwo;
            }
        }
    }
}
