using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_Battleship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            #region NatRegion

            //code added 11.7.2026 by Natalie Borges Blondet

            //added title to MainWindow to be "Main Menu" (Main Form does not have a name, I believe we need to give it a name)
            //changed color of main window (the white was bothering me)

            //my thought process is that pressing start game opens a new window called Start Game, with the option to choose Human or CPU
            //if Human is chosen then a text box appears to add their name, but it is by default Player 1, a button that says Place Ships also comes up when Human is chosen
            //if CPU is chosen then radio buttons appear with the available difficulties, once a difficulty is selected then "Next Player" button appears





            //also coded the about and close button functionality
            #endregion

            StartGameWindow p1 = new StartGameWindow();
            p1.ShowDialog();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRules_Click(object sender, RoutedEventArgs e)
        {
            RulesWindow rules = new RulesWindow();
            rules.ShowDialog();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        private void btnScoreboard_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "Scoreboard.txt";
            FileInfo info = new FileInfo(filePath);

            //if the file exists and is not empty
            if (File.Exists(filePath) && info.Length != 0)
            {
                ScoreboardWindow scoreboard = new ScoreboardWindow();
                

                string record;
                string[] playerInfo;
                string header = "Player Name \t Wins-Losses";
                string line;

                scoreboard.lbxScoreboard.Items.Add(header);

                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        record = sr.ReadLine();
                        playerInfo = record.Split(',');

                        line = $"{playerInfo[0]} \t\t {playerInfo[1]}-{playerInfo[2]}";

                        scoreboard.lbxScoreboard.Items.Add(line);
                    }
                }

                scoreboard.ShowDialog();
            }

            //else a message box is displayed
            else
            {
                MessageBox.Show("No scores to show!");
            }
        }
    }
}
