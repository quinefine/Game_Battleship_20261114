using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game_Battleship
{
    public class Ship
    {

        public string name { get; set; }
        public int id { get; set; } = 0;
        public int size { get; set; }
        public char orientation { get; set; } = 'V';
        public int hitsTaken { get; set; } = 0;
        public bool isSunk { get; set; } = false;

        public void UpdateDamage()
        {
            hitsTaken++;
            if (hitsTaken == size)
            {
                isSunk = true;
                MessageBox.Show($"You sunk my {name}");
            }
        }

    }
}
