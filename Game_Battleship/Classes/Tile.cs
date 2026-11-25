using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Game_Battleship
{
    public class Tile : Button
    {
        private string xLocation;
        private string yLocation;
        private string location;
        //public bool isChosen = false;
        public void getLocation()
        {
            MessageBox.Show($"{this.yLocation}{this.xLocation}");
        }

        public string XLocation
        {
            get { return this.xLocation; }
            set { this.xLocation = value; }
        }

        public string YLocation
        {
            get { return this.yLocation; }
            set { this.yLocation = value; }
        }

        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        // Translates numerical coordinates to alphabetical coordinates

        public string numberToAlphabet(int y)
        {
            string yToString = "";
            switch (y)
            {
                case 1:
                    yToString = "A";
                    break;
                case 2:
                    yToString = "B";
                    break;
                case 3:
                    yToString = "C";
                    break;
                case 4:
                    yToString = "D";
                    break;
                case 5:
                    yToString = "E";
                    break;
                case 6:
                    yToString = "F";
                    break;
                case 7:
                    yToString = "G";
                    break;
                case 8:
                    yToString = "H";
                    break;
                case 9:
                    yToString = "I";
                    break;
                case 10:
                    yToString = "J";
                    break;
            }
            return yToString;
        
    }

        // Translates alphabetical coordinates to numerical coordinates

        public int AlphabetToNumber(string y)
        {
            int yToInt = 0;
            switch (y)
            {
                case "A":
                    yToInt = 1;
                    break;
                case "B":
                    yToInt = 2;
                    break;
                case "C":
                    yToInt = 3;
                    break;
                case "D":
                    yToInt = 4;
                    break;
                case "E":
                    yToInt = 5;
                    break;
                case "F":
                    yToInt = 6;
                    break;
                case "G":
                    yToInt = 7;
                    break;
                case "H":
                    yToInt = 8;
                    break;
                case "I":
                    yToInt = 9;
                    break;
                case "J":
                    yToInt = 10;
                    break;
            }
            return yToInt;
        }
    }
}
