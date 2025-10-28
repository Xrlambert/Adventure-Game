using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Game
{
    public partial class Form1 : Form
    {
        // Private fields (use leading underscore convention)
        private int _currentPage = 1;
        private readonly Random _rng = new Random();
        private bool _isWaitingForPause = false;

        // Simple Player model (expand as needed)
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

        private Player _player = new Player();

        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel();
            DisplayPage();
        }

        private void btnOption1_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            // Example routing pattern: check page and apply effects
            if (_currentPage == 1)
            {
                // Page 1: Continue / ask name example
                string name = PromptForName();
                if (!string.IsNullOrWhiteSpace(name)) _player.Name = name;
                _currentPage = 2;
            }
            else if (_currentPage == 2)
            {
                // Example: Page 2 option 1 (Life Support)
                _player.Power -= 5;
                _player.TimeRemaining += 1;
                _currentPage = 3;
            }
            else
            {
                // Default forward
                _currentPage++;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            if (_currentPage == 2)
            {
                // Example: Page 2 option 2 (Communications)
                _player.Power -= 5;
                _player.Knowledge += 1;
                _currentPage = 4;
            }
            else
            {
                _currentPage++;
            }

            UpdateStatusLabel();
            DisplayPage();
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            if (_currentPage == 2)
            {
                // Example: Page 2 option 3 (Data Core)
                _player.Power -= 8;
                _player.Knowledge += 2;
                _currentPage = 5;
            }
            else if (_currentPage == 6)
            {
                // Example: page 6 option 3 (Stay and finish research)
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

        private void btnRestart_Click(object sender, EventArgs e)
        {
            // Reset player and page
            _player = new Player();
            _currentPage = 1;
            _isWaitingForPause = false;
            UpdateStatusLabel();
            DisplayPage();
        }

        private void btnOpenGuessingForm_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            // Open guessing form modal (assumes you have a GuessingForm in the project)
            try
            {
                using (var gue = new Guessing()) // if doesn't exist, create a simple one or remove this block
                {
                    var result = gue.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        bool complete = gue.skillComplete; // guessing form should expose a Success property
                        if (complete && gue.skillSuccess)
                        {
                            // handle success branch
                            _player.Knowledge += 2;
                            _currentPage++;
                        }
                        else
                        {
                            // handle failure branch
                            _player.TimeRemaining -= 1;
                            _currentPage += 2;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // If GuessingForm isn't available, fallback: run a random check
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

        // Helper to prompt for name (simple dialog)
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

        // Update the status label (lblStatus exists in Designer)
        private void UpdateStatusLabel()
        {
            try
            {
                lblStatus.Text = $"Power: {_player.Power}   Time: {_player.TimeRemaining}   Morality: {_player.Morality}   Knowledge: {_player.Knowledge}";
            }
            catch
            {
                // If lblStatus is missing in Designer, try to set a control named returnLabel (backwards compatibility)
                var f = this.GetType().GetField("returnLabel", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                if (f != null)
                {
                    var obj = f.GetValue(this) as Label;
                    if (obj != null) obj.Text = $"Power: {_player.Power}   Time: {_player.TimeRemaining}   Morality: {_player.Morality}   Knowledge: {_player.Knowledge}";
                }
            }

            // Also update page number label if present
            if (this.Controls != null)
            {
                var lbl = this.Controls["lblPageNumber"] as Label;
                if (lbl != null) lbl.Text = $"Page: {_currentPage}";
            }
        }

        // DisplayPage skeleton — expand each case with full narrative, images, audio, pauses, random outcomes
        private async void DisplayPage()
        {
            // Prevent input during rendering/pause
            _isWaitingForPause = true;
            // Basic reset of buttons
            btnOption1.Enabled = false;
            btnOption2.Enabled = false;
            btnOption3.Enabled = false;

            // Simple example of page rendering logic
            switch (_currentPage)
            {
                case 1:
                    lblNarrative.Text = "You wake up aboard the Aurora. You are alone. Click Continue.";
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    btnOption2.Visible = false;
                    btnOption3.Visible = false;
                    break;

                case 2:
                    lblNarrative.Text = "Power allocation: choose where to divert power.";
                    btnOption1.Text = "Life Support";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    btnOption2.Text = "Communications";
                    btnOption2.Visible = true;
                    btnOption2.Enabled = true;
                    btnOption3.Text = "Data Core";
                    btnOption3.Visible = true;
                    btnOption3.Enabled = true;
                    break;

                case 3:
                    // Example pause scene: show partial text then reveal more
                    lblNarrative.Text = "You move down the corridor. It is quiet...";
                    await Task.Delay(1000);
                    lblNarrative.Text += "\nA faint click echoes from the data core.";
                    btnOption1.Text = "Investigate";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    btnOption2.Visible = false;
                    btnOption3.Visible = false;
                    break;

                default:
                    lblNarrative.Text = "End of demo path. Use Restart to try again.";
                    btnOption1.Text = "Restart";
                    btnOption1.Visible = true;
                    btnOption1.Enabled = true;
                    btnOption2.Visible = false;
                    btnOption3.Visible = false;
                    break;
            }

            UpdateStatusLabel();
            _isWaitingForPause = false;
        }
    }
}
