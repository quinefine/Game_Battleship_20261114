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

        private string name;
        private int id = 0;
        private int size;
        private char orientation = 'V';
        private int hitsTaken = 0;
        private bool isSunk = false;
        private bool isPlaced = false;

        public void UpdateDamage()
        {
            hitsTaken++;
            if (hitsTaken == size)
            {
                isSunk = true;
                MessageBox.Show($"You sunk my {name}");
            }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public char Orientation
        {
            get { return this.orientation; }
            set { this.orientation = value; }
        }

        public int HitsTaken
        {
            get { return this.hitsTaken; }
            set { this.hitsTaken = value; }
        }

        public bool IsSunk
        {
            get { return this.isSunk; }
            set { this.isSunk = value; }
        }

        public bool IsPlaced
        {
            get { return this.isPlaced; }
            set { this.isPlaced = value; }
        }
    }
}
