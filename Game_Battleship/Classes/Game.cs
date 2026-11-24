using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game_Battleship
{
    public class Game
    {

        public HumanPlayer playerOne { get; set; }

        public HumanPlayer playerTwo { get; set; }

        public CPUPlayer playerOneCPU { get; set; }

        public CPUPlayer playerTwoCPU { get; set; }

        public static int playerOneWins { get; set; }

        public static int playerTwoWins { get; set; }

        public string gameState { get; set; }

        public event EventHandler WinCountsChanged;

        public void Start()
        {

        }

        public void SwitchTurn()
        {

        }

        // TODO: Win counts are being incremented in EndGame

        public void EndGame(int winPlayer)
        {

            if (gameState == "GameOver")
                return;

            string winner;

            if (winPlayer == 1)
            {

                playerOneWins++;
                playerOne.winCount = playerOneWins;

                winner = playerOne.name;

            }

            else if (winPlayer == 2) 
            {

                playerTwoWins++;
                playerTwo.winCount = playerTwoWins;

                winner = playerTwo.name;

            }

            else
            {
                return;
            }

            gameState = "GameOver";

            WinCountsChanged?.Invoke(this, EventArgs.Empty);

            MessageBox.Show($"{winner} wins!!!");

        }

    }
}
