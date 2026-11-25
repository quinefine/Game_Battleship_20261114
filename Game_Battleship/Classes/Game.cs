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
        public Game(string gameState)
        {
            this.gameState = gameState;
        }

        public Player playerOne { get; set; }

        public Player playerTwo { get; set; }

        private string gameState { get; set; }

        public void setGameState(string state)
        {
            this.gameState = state;
        }

        public void Start()
        {

        }

        public void SwitchTurn()
        {
            //take the turns. SWITHC TEM
            //PUSH T HE TOWNWNWNWNWW
        }

        // TODO: Win counts are being incremented in EndGame

        public void EndGame(Player winPlayer)
        {

            if (gameState == "GameOver")
                return;

            winPlayer.winCount++;

            gameState = "GameOver";

            MessageBox.Show($"{winPlayer.name} wins!!!");
        }

    }
}
