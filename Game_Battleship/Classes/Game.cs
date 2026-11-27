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
            this.Scoreboard = new Scoreboard();
        }

        private Player playerOne;

        private Player playerTwo;

        private Scoreboard scoreboard;


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

        public Scoreboard Scoreboard
        {
            get { return this.scoreboard; }
            set { this.scoreboard = value; }
        }

        public void Start()
        {
            //TODO REFACTORING: remove this if not needed
        }

        public void SwitchTurn()
        {
            //TODO REFACTORING: move the switch turn mechanics over here?
        }

        //TODO Win counts are being incremented in EndGame (may not be necessary to increment win/loss count here)

        public void EndGame(Player winner, Player loser)
        {
            
            if (gameState == "GameOver")
                return;
            winner.MarkWinner();
            winner.WinCount++;
            loser.LossCount++;

            

            gameState = "GameOver";

            if (winner == playerOne)
            {
                playerTwo.OceanBoard.changeVisibilty();
                playerTwo.TargetBoard.changeVisibilty();
            }
            else
            {
                playerOne.OceanBoard.changeVisibilty();
                playerOne.TargetBoard.changeVisibilty();
            }

            MessageBox.Show($"{winner.Name} wins!!!");
        }

    }
}
