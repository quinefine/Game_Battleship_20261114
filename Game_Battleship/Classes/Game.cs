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

        private Player playerOne;

        private Player playerTwo;

        private Scoreboard score;


        private string gameState;
        
            public void setGameState(string state)
            {
                this.gameState = state;
            }

            public string getGameState()
            {
                return gameState;
            }

        public Player PlayerOne
        {
            get { return this.playerOne; } 
            set { this.playerOne = value; }
        }

        public Player PlayerTwo
        {
            get { return this.playerTwo; }
            set { this.playerTwo = value; }
        }

        public Scoreboard Score
        {
            get { return this.score; }
            set { this.score = value; }
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

            winPlayer.WinCount++;

            gameState = "GameOver";

            if (winPlayer == playerOne)
            {
                playerTwo.OceanBoard.changeVisibilty();
                playerTwo.TargetBoard.changeVisibilty();
            }
            else
            {
                playerOne.OceanBoard.changeVisibilty();
                playerOne.TargetBoard.changeVisibilty();
            }

            MessageBox.Show($"{winPlayer.Name} wins!!!");
        }

    }
}
