using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game_Battleship
{
    public class Scoreboard
    {
        public Scoreboard(Player player)
        {
            //constructor
            this.winner = player.Name;

        }

        private string winner;

        public string Winner
        {
            get { return this.winner; }
        }

        public void UpdateScoreboard()
        {

            using (StreamWriter fs = new StreamWriter("Scoreboard.bin"))
            {
                fs.WriteLine();
                //get the code for what is getting written
            }
        }



    }
}
