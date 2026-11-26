using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Shapes;

namespace Game_Battleship
{
    public class Scoreboard
    {
        public Scoreboard()
        {
            //default constructor
        }

        
        private List<Player> players;

        //each player has a Name, WinCount, and LossCount. These will be displayed on the scoreboard

        public void UpdateScoreboard(Player winner, Player loser)
        {

            using (StreamWriter sw = new StreamWriter("Scoreboard.txt", append:true))
            {
                sw.WriteLine($"{winner.Name},{winner.WinCount},{winner.LossCount}");
            }
        }

        public void ReadScoreBoard()
        {

            string filePath = "Scoreboard.txt";

            using (StreamReader sr = new StreamReader("Scoreboard.txt"))
            {
                while (!sr.EndOfStream)
                {

                    string record = sr.ReadLine();

                    if (!string.IsNullOrWhiteSpace(record))
                    {

                        ///////we are aware this is not working yet!!!!!
                        /// TODO make this work
                        string name = null;
                        int win = 0;
                        int loss = 0;

                        string[] lines = record.Split(',');

                        for (int i = 0; i < lines.Length; i += 3)
                        {
                            name = lines[i];
                        }

                        for (int i = 1; i < lines.Length; i += 3)
                        {
                            win = int.Parse(lines[i]);
                            //get the wincount from the read file
                        }

                        for (int i = 2; i < lines.Length; i += 3)
                        {
                            loss = int.Parse(lines[i]);
                            //get the losscount from the read file
                        }

                        Player local = new Player(name, win, loss);

                    }
                }
            }

        }

    }
}
