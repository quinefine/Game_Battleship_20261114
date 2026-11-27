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

        private readonly string filePath = "Scoreboard.txt";
        
        private List<Player> currentPlayers;

        private List<Player> recordedPlayers;

        //each player has a Name, WinCount, and LossCount. These will be displayed on the scoreboard
        //the player also has a bool for WonGame

        //TODO A method to write the list of recordedPlayers onto a listbox or some display so it can be viewed

        public void UpdateScoreboard(Player winner, Player loser)
        {
            currentPlayers = new List<Player>();
            this.currentPlayers.Add(winner);
            this.currentPlayers.Add(loser);

            if (File.Exists(filePath))
            {
                recordedPlayers = ReadScoreboard();

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    //updates scores if on the recordedPlayers list
                    //adds the player to the recordedPlayers list if not already on it
                    this.recordedPlayers = CheckUpdateScoreboard(this.recordedPlayers, this.currentPlayers);

                    //plan is to re-write the text file after collecting the records on it
                    foreach (Player r in recordedPlayers)
                    {
                        sw.WriteLine($"{r.Name},{r.WinCount},{r.LossCount}");
                    }
                    
                }
            }
            else
            {
                //if file does not exist then we will create and write the first line to it
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    foreach (Player c in currentPlayers)
                    {
                        sw.WriteLine($"{c.Name},{c.WinCount},{c.LossCount}");
                    }
                    
                }
            }

        }

        public List<Player> CheckUpdateScoreboard(List<Player> existing, List<Player> incoming)
        {
            
            //for every player in the existing file
            foreach (Player i in incoming)
            {
                bool onFile = false;
                //checking both players in the incoming list
                foreach (Player e in existing)
                {
                    //if the name matches (will add toLower later to catch discrepancies for capitalization)
                    if (i.Name == e.Name)
                    {
                        onFile = true;

                        //if the incoming player won the game, increment the wincount
                        if (i.WonGame)
                        {
                            e.WinCount++;
                        }
                        //else increment the loss count
                        else
                        {
                            e.LossCount++;
                        }

                    }
                }

                //if the incoming name is not already on the existing list then we will add them to the list
                if (!onFile)
                {
                    existing.Add(new Player(i.Name, i.WinCount, i.LossCount));
                }
            }
            return existing;
        }

        /// <summary>
        /// reads the scoreboard file 
        /// </summary>
        /// <returns>returns the records on it as a list of players with names, wins, and losses</returns>
        public List<Player> ReadScoreboard()
        {
            //this method should only run if it has been confirmed that there is a file to check
            using (StreamReader sr = new StreamReader(filePath))
            {
                //reading the whole file and adding it to the list of recorded players
                //this works to happen everytime a game ends
                //new list is created but it will be reading from the existing file everytime (if there is one)
                this.recordedPlayers = new List<Player>();

                while (!sr.EndOfStream)
                {

                    string record = sr.ReadLine();
                    

                    if (!string.IsNullOrWhiteSpace(record))
                    {
                        // TODO make this work
                       
                        string[] parts = record.Split(',');

                        string name = parts[0];
                        int wins = int.Parse(parts[1]);
                        int losses = int.Parse(parts[2]);


                        Player local = new Player(name, wins, losses);

                        this.recordedPlayers.Add(local);

                    }
                }

                return this.recordedPlayers;
            }

            
        }

    }
}
