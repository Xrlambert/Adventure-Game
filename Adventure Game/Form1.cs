using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Game
{
    public partial class Form1 : Form
    {
        // Tracks the current page of the story
        private int _currentPage = 1;

        // Random generator for fallback outcomes and chance-based events
        private readonly Random _rng = new Random();

        // Prevents user interaction during transitions or pauses
        private bool _isWaitingForPause = false;

        private bool _restarting = false;
        // Represents the player's state throughout the game
        private class Player
        {
            public string Name = "Player";
            public int Power = 100;
            public int TimeRemaining = 7;
            public int Morality = 0;
            public int Knowledge = 0;
            public bool TrustAI = false;
            public bool SurvivorFound = false;
        }

        // Active player instance
        private Player _player = new Player();

        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel();  // Show initial stats
            DisplayPage();        // Render first page
        }

        // Handles Option 1 button click based on current page
        private void btnOption1_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;

            switch (_currentPage)
            {
                case 1:
                    // Ask for player name
                    string name = PromptForName();
                    if (!string.IsNullOrWhiteSpace(name)) _player.Name = name;
                    _currentPage = 2;
                    break;

                case 2:
                    // Divert power to Life Support
                    _player.Power -= 5;
                    _player.TimeRemaining += 1;
                    _currentPage = 3;
                    break;

                case 3:
                    // Investigate corridor sound
                    _currentPage = 6;
                    break;

                case 4:
                    // Activate AI
                    _player.TrustAI = true;
                    _player.Morality += 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 5;
                    break;

                case 5:
                    // Search cryo pods (70% success)
                    if (_rng.Next(100) < 70)
                    {
                        _player.SurvivorFound = true;
                        _player.Morality += 1;
                    }
                    _player.TimeRemaining -= 1;
                    _currentPage = 10;
                    break;

                case 6:
                    // Launch Guessing Game challenge
                    OpenGuessingChallenge();
                    break;

                default:
                    _currentPage++;
                    break;
            }
            if (_restarting)
            {
                _player = new Player();
                _currentPage = 1;
                _isWaitingForPause = false;
                UpdateStatusLabel();
                DisplayPage();
                _restarting= false;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        // Handles Option 2 button click based on current page
        private void btnOption2_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;

            switch (_currentPage)
            {
                case 2:
                    // Divert power to Communications
                    _player.Power -= 5;
                    _player.Knowledge += 1;
                    _currentPage = 4;
                    break;

                case 3:
                    // Return to control room
                    _currentPage = 7;
                    break;

                case 4:
                    // Leave AI offline
                    _player.Morality -= 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 5;
                    break;

                case 5:
                    // Access security logs
                    _player.Knowledge += 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 10;
                    break;

                default:
                    _currentPage++;
                    break;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        // Handles Option 3 button click (used on pages with 3 choices)
        private void btnOption3_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;

            switch (_currentPage)
            {
                case 2:
                    // Divert power to Data Core
                    _player.Power -= 8;
                    _player.Knowledge += 2;
                    _player.TimeRemaining -= 1;
                    _currentPage = 3;
                    break;

                default:
                    _currentPage++;
                    break;
            }

            UpdateStatusLabel();
            DisplayPage();
        }
        // Handles the button click to open the guessing challenge form
        private void btnOpenGuessingForm_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            OpenGuessingChallenge(); // This method handles the guessing logic
        }


        // Resets game state to initial values
        private void btnRestart_Click(object sender, EventArgs e)
        {
            _player = new Player();
            _currentPage = 1;
            _isWaitingForPause = false;
            UpdateStatusLabel();
            DisplayPage();
        }

        // Launches the guessing challenge form or fallback logic
        private void OpenGuessingChallenge()
        {
            try
            {
                using (var guessForm = new Guessing())
                {
                    var result = guessForm.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        if (guessForm.skillComplete && guessForm.skillSuccess)
                        {
                            // Success: boost knowledge and progress
                            _player.Knowledge += 2;
                            _currentPage++;
                        }
                        else
                        {
                            // Failure: lose time and skip ahead
                            _player.TimeRemaining -= 1;
                            _currentPage += 2;
                        }
                    }
                }
            }
            catch
            {
                // Fallback random outcome if form fails
                if (_rng.Next(100) < 70)
                {
                    _player.Knowledge += 1;
                    _currentPage++;
                }
                else
                {
                    _player.TimeRemaining -= 1;
                    _currentPage += 2;
                }
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        // Prompts user to enter their name
        private string PromptForName()
        {
            string input = null;
            using (var prompt = new Form())
            {
                prompt.Width = 400;
                prompt.Height = 140;
                prompt.Text = "Enter your name";
                var lbl = new Label() { Left = 12, Top = 12, Text = "Name:", Width = 360 };
                var txt = new TextBox() { Left = 12, Top = 36, Width = 360 };
                var ok = new Button() { Text = "OK", Left = 220, Width = 75, Top = 68, DialogResult = DialogResult.OK };
                var cancel = new Button() { Text = "Cancel", Left = 300, Width = 75, Top = 68, DialogResult = DialogResult.Cancel };
                prompt.AcceptButton = ok;
                prompt.CancelButton = cancel;
                prompt.Controls.Add(lbl);
                prompt.Controls.Add(txt);
                prompt.Controls.Add(ok);
                prompt.Controls.Add(cancel);
                if (prompt.ShowDialog() == DialogResult.OK) input = txt.Text;
            }
            return input;
        }

        // Updates status labels with current player stats and page number
        private void UpdateStatusLabel()
        {
            lblStatus.Text = $"Power: {_player.Power}   Time: {_player.TimeRemaining}   Morality: {_player.Morality}   Knowledge: {_player.Knowledge}";

            var lblPage = this.Controls["lblPageNumber"] as Label;
            if (lblPage != null) lblPage.Text = $"Page: {_currentPage}";
        }

        // Renders the current page's narrative and options
        private async void DisplayPage()
        {
            _isWaitingForPause = true;

            // Reset button visibility
            btnOption1.Visible = btnOption2.Visible = btnOption3.Visible = false;
            btnOption1.Enabled = btnOption2.Enabled = btnOption3.Enabled = false;

            switch (_currentPage)
            {
                case 1:
                    lblNarrative.Text = "You wake up aboard the Aurora. You are alone. Click Continue.";
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 2:
                    lblNarrative.Text = "Power allocation: choose where to divert power.";
                    btnOption1.Text = "Life Support";
                    btnOption2.Text = "Communications";
                    btnOption3.Text = "Data Core";
                    btnOption1.Visible = btnOption2.Visible = btnOption3.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = btnOption3.Enabled = true;
                    break;

                case 3:
                    lblNarrative.Text = "You move down the corridor. It is quiet...";
                    await Task.Delay(1000);
                    lblNarrative.Text += "\nA strange sound echoes from the data core.";
                    btnOption1.Text = "Investigate";
                    btnOption2.Text = "Return to control room";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 4:
                    lblNarrative.Text = "You find the AI terminal. Do you want to activate it?";
                    btnOption1.Text = "Activate AI";
                    btnOption2.Text = "Leave it offline";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 5:
                    lblNarrative.Text = "You approach the cryo pods. What will you do?";
                    btnOption1.Text = "Search pods";
                    btnOption2.Text = "Access security logs";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 6:
                    lblNarrative.Text = "You reach a terminal with encrypted data. You must verify the signal.";
                    await Task.Delay(1000);
                    lblNarrative.Text += "\nInitiating verification challenge...";
                    btnOption1.Text = "Begin Challenge";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    break;

                case 7:
                    lblNarrative.Text = "You return to the control room. Systems are still unstable.";
                    btnOption1.Text = "Try to stabilize power";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    break;

                case 8:
                    lblNarrative.Text = "AI is now online. It begins analyzing the station's condition.";
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    break;

                case 10:
                    lblNarrative.Text = "You’ve completed the signal verification. What’s next?";
                    btnOption1.Text = "Proceed to final decision";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    break;

                default:
                    lblNarrative.Text = "End of demo path. Use Restart to try again.";
                    btnOption1.Text = "Restart";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    _restarting = true;
                    break;
            }

            UpdateStatusLabel();
            _isWaitingForPause = false;
        }
    }
}