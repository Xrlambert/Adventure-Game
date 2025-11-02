using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Game
{
    public partial class Form1 : Form
    {
        // --- Core state -------------------------------------------------------------
        private int _currentPage = 1;
        private readonly Random _rng = new Random();
        private bool _isWaitingForPause = false;
        private bool _restarting = false;

        // --- Simple player model ----------------------------------------------------
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

        // --- Constructor ------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel();  // Shows initial player stats
            DisplayPage();        // Loads the first scene
        }

        // --- Option 1 handler ------------------------------------------------------
        private void btnOption1_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;

            // If we flagged restart on previous page, do the reset now.
            if (_restarting)
            {
                _player = new Player();
                _currentPage = 1;
                _isWaitingForPause = false;
                _restarting = false;
                UpdateStatusLabel();
                DisplayPage();
                return;
            }

            // Switch dispatch: each case updates player state and advances page(s).
            switch (_currentPage)
            {
                // Intro: ask for player name, then go to page 2
                case 1:
                    string name = PromptForName();
                    if (!string.IsNullOrWhiteSpace(name)) _player.Name = name;
                    _currentPage = 2;
                    break;

                // Life Support: spend power to gain time, then page 3
                case 2:
                    _player.Power -= 5;
                    _player.TimeRemaining += 1;
                    _currentPage = 3;
                    break;

                // Investigate sound -> jump to challenge entry (page 6)
                case 3:
                    _currentPage = 6;
                    break;

                // Activate AI: toggles trust / morality, costs time
                case 4:
                    _player.TrustAI = true;
                    _player.Morality += 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 5;
                    break;

                // Search cryo pods: probabilistic survivor find, costs time
                case 5:
                    if (_rng.Next(100) < 70)
                    {
                        _player.SurvivorFound = true;
                        _player.Morality += 1;
                    }
                    _player.TimeRemaining -= 1;
                    _currentPage = 10;
                    break;

                // Start the guessing/skill challenge (modal)
                case 6:
                    OpenGuessingChallenge();
                    return; // OpenGuessingChallenge updates UI itself

                default:
                    _currentPage++;
                    break;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        // --- Option 2 handler ------------------------------------------------------
        private void btnOption2_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;

            if (_currentPage == 2)
            {
                // Communications choice — increases knowledge, costs power
                _player.Power -= 5;
                _player.Knowledge += 1;
                _currentPage = 4;
            }
            else if (_currentPage == 3)
            {
                _currentPage = 7;
            }
            else if (_currentPage == 4)
            {
                _player.Morality -= 1;
                _player.TimeRemaining -= 1;
                _currentPage = 5;
            }
            else if (_currentPage == 5)
            {
                _player.Knowledge += 1;
                _player.TimeRemaining -= 1;
                _currentPage = 10;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        // --- Option 3 handler ------------------------------------------------------
        private void btnOption3_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;

            if (_currentPage == 2)
            {
                // Data Core: higher knowledge, higher cost, move to page 3
                _player.Power -= 8;
                _player.Knowledge += 2;
                _player.TimeRemaining -= 1;
                _currentPage = 3;
            }
            else if (_currentPage == 6)
            {
                // Stay and finish research — boosts morality, costs time
                _player.Morality += 2;
                _player.TimeRemaining -= 1;
                _currentPage = 9;
            }
            else
            {
                _currentPage++;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        // --- Restart button --------------------------------------------------------
        private void btnRestart_Click(object sender, EventArgs e)
        {
            _player = new Player();
            _currentPage = 1;
            _isWaitingForPause = false;
            _restarting = false;
            UpdateStatusLabel();
            DisplayPage();
        }

        // --- Guessing Challenge ----------------------------------------------------
        private void OpenGuessingChallenge()
        {
            if (_isWaitingForPause) return;

            try {
                using (var gue = new Guessing()) {
                    
                    var result = gue.ShowDialog(this);

                    if (result == DialogResult.OK || gue.skillComplete) {
                        if (gue.skillSuccess) { 
                            _player.Knowledge += 2;
                            _currentPage++;
                        }
                        else {
                            _player.TimeRemaining -= 1;
                            _currentPage += 2;
                        }
                    }
                    else {
                        if (_rng.Next(100) < 70) {
                            _player.Knowledge += 1;
                            _currentPage++;
                        }
                        else {
                            _player.TimeRemaining -= 1;
                            _currentPage += 2;
                        }
                    }
                }
            }
            catch
            {
                // Fallback if Guessing form is missing
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

        private void btnOpenGuessingForm_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            OpenGuessingChallenge();
        }

        // --- Name prompt -----------------------------------------------------------
        private string PromptForName()
        {
            string input = null;

            using (var prompt = new Form())
            {
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.Width = 420;
                prompt.Height = 150;
                prompt.Text = "Enter your name";

                var lbl = new Label() { Left = 12, Top = 12, Text = "Name:", Width = 380 };
                var txt = new TextBox() { Left = 12, Top = 36, Width = 380 };
                var ok = new Button() { Text = "OK", Left = 220, Width = 75, Top = 68, DialogResult = DialogResult.OK };
                var cancel = new Button() { Text = "Cancel", Left = 305, Width = 75, Top = 68, DialogResult = DialogResult.Cancel };

                prompt.AcceptButton = ok;
                prompt.CancelButton = cancel;

                prompt.Controls.Add(lbl);
                prompt.Controls.Add(txt);
                prompt.Controls.Add(ok);
                prompt.Controls.Add(cancel);

                if (prompt.ShowDialog(this) == DialogResult.OK)
                    input = txt.Text;
            }

            return input;
        }

        // --- Status label update ---------------------------------------------------
        private void UpdateStatusLabel()
        {
            lblStatus.Text =
                $"Power: {_player.Power}   Time: {_player.TimeRemaining}   Morality: {_player.Morality}   Knowledge: {_player.Knowledge}";

            var pageLbl = this.Controls != null ? this.Controls["lblPageNumber"] as Label : null;
            if (pageLbl != null)
                pageLbl.Text = $"Page: {_currentPage}";
        }

        // --- DisplayPage method -----------------------------------------------------
        private async void DisplayPage()
        {
            _isWaitingForPause = true;

            btnOption1.Visible = btnOption2.Visible = btnOption3.Visible = false;
            btnOption1.Enabled = btnOption2.Enabled = btnOption3.Enabled = false;
            if (btnOpenGuessingForm != null)
            {
                btnOpenGuessingForm.Visible = false;
                btnOpenGuessingForm.Enabled = false;
            }

            switch (_currentPage)
            {
                case 1:
                    lblNarrative.Text = "You wake up aboard the Aurora. You are alone. Click Continue.";
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                // Page 2: Power allocation — three option buttons:
                //   Option1 = Life Support (spend power, gain time)
                //   Option2 = Communications (spend power, gain knowledge)
                //   Option3 = Data Core (higher power cost, higher knowledge gain)
                case 2:
                    lblNarrative.Text = "Low Power Warning: choose where to divert power.";
                    btnOption1.Text = "Life Support";
                    btnOption2.Text = "Communications";
                    btnOption3.Text = "Data Core";
                    btnOption1.Visible = btnOption2.Visible = btnOption3.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = btnOption3.Enabled = true;
                    break;

                    // Page 3: Corridor choices — two option buttons:
                    //   Option1 = Investigate (follow the sound to the challenge)
                    //   Option2 = Return to control room (go back to the hub)
                 case 3:
                    lblNarrative.Text = "You move down the corridor. It is quiet...";
                    await Task.Delay(900);
                    lblNarrative.Text += "\nA strange sound echoes from the data core.";
                    btnOption1.Text = "Investigate";
                    btnOption2.Text = "Return to control room";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 4:
                    lblNarrative.Text = "You move down the corridor. An alarm is sounding...";
                    await Task.Delay(900);
                    lblNarrative.Text += "\nA hail sounds from the console";
                    btnOption1.Text = "Respond (70% Chance)";
                    btnOption2.Text = "Ignore";
                    btnOption1.Visible = true;
                    btnOption2.Visible = true;
                    break;

                    // Page 5: AI terminal — two option buttons:
                    //   Option1 = Activate AI (toggle trust, adjust morality/time)
                    //   Option2 = Leave it offline (decline activation, preserve status)
                case 5:
                    lblNarrative.Text = "You find the AI terminal. Do you want to activate it?";
                    btnOption1.Text = "Activate AI";
                    btnOption2.Text = "Leave it offline";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                // Page 6: Cryo pods — two option buttons:
                //   Option1 = Search pods (chance to find survivor, costs time)
                //   Option2 = Access security logs (gain information, costs time)
                case 6:
                    lblNarrative.Text = "You approach the cryo pods. What will you do?";
                    btnOption1.Text = "Search pods";
                    btnOption2.Text = "Access security logs";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                // Page 7: Verification challenge entry — two controls:
                //   Option1 = Begin Challenge (open guessing modal to resolve signal)
                case 7:
                    lblNarrative.Text = "You reach a terminal with encrypted data. You must verify the signal.";
                    await Task.Delay(700);
                    lblNarrative.Text += "\nInitiating verification challenge...";
                    if (btnOpenGuessingForm != null)
                    {
                        btnOpenGuessingForm.Text = "Begin Challenge";
                        btnOpenGuessingForm.Visible = btnOpenGuessingForm.Enabled = true;
                    }
                    btnOption1.Text = "Begin Challenge";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 8:
                    lblNarrative.Text = "You return to the control room. Systems are still unstable.";
                    btnOption1.Text = "Try to stabilize power";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 9:
                    lblNarrative.Text = "AI is now online. It begins analyzing the station's condition.";
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 10:
                    lblNarrative.Text = "You continue your research and gain insight.";
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 11:
                    lblNarrative.Text = "Task Successful. What’s next?";
                    btnOption1.Text = "Proceed to final decision";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                default:
                    lblNarrative.Text = "End of demo path. Use Restart to try again.";
                    btnOption1.Text = "Restart";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    _restarting = true;
                    break;
            }

            UpdateStatusLabel();
            _isWaitingForPause = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _currentPage--;
            UpdateStatusLabel();
            DisplayPage();
        }
    }
}
