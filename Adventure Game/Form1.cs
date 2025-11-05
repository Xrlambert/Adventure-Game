using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;


namespace Adventure_Game
{
    public partial class Form1 : Form
    {
        // --- Core state -------------------------------------------------------------
        private int _currentPage = 1;
        private readonly Random _rng = new Random();
        private bool _isWaitingForPause = false;
        private bool _restarting = false;


        // --- Flags for key events ------------------------------------------------------
        private bool _AIdone = false;
        private bool _repairDone = false;
        private bool _dataCoreAccessed = false;
        private bool lifeSupportComp = false;


        // --- Simple player model ---------------------------------------------------- 
        public class Player
        {
            public string Name = "Player";
            public int Power = 100;
            public int TimeRemaining = 45;
            public int difficulty = -1;
            public int Morality = 0;
            public int Knowledge = 0;
            public bool TrustAI = false;
            public bool SurvivorFound = false;
        }

        // --- Timer for Time element --------------------------------------------------

        private System.Windows.Forms.Timer _gameTimer;

        // Ending selector computed when reaching an ending
        private int _endingType = 0; // 0 = default/lose, 1 = escape, 2 = AI, 3 = rescue


        // Active player instance
        private Player _player = new Player();

        // --- Constructor ------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel();  // Shows initial player stats
            DisplayPage();        // Loads the first scene

            /*MXB.URL = @"slow-down-244244";
            MXB.settings.playCount = 9999;
            MXB.Ctlcontrols.stop();
            MXB.Visible = true;*/
        }



        // --- Option 1 handler ------------------------------------------------------
        private async void btnOption1_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            _rng.Next(1, 101);

            //Sound.();
            // If we're on the Restart prompt, Option1 restarts the game
            if (_restarting)
            {
                _player = new Player();
                _currentPage = 1;
                _restarting = false;
                UpdateStatusLabel();
                DisplayPage();
                return;
            }

            //cases for all pages where button 1 is relevant
            switch (_currentPage)
            {
                // Page 1: Continue -> go to page 2
                case 1:
                    string name = PromptForName();/*_player*/
                    if (!string.IsNullOrWhiteSpace(name)) _player.Name = name;
                    _currentPage = 2;
                    break;

                // Page 2: Life Support
                case 2:
                    _player.Power -= 5;
                    _player.TimeRemaining += 2;
                    _currentPage = 3;
                    break;

                // Page 3: Investigate sound -> see own empty pod (page 15)
                case 3:
                    _currentPage = 8;
                    break;

                // Page 4: Respond -> accept contact, trust AI path -> go to AI terminal (page 6)
                case 4:
                    _player.Knowledge += 2;
                    _player.TimeRemaining -= 1;
                    if (_rng.Next(100) < 70)
                    {
                        _currentPage = 18;
                    }
                    else
                    {
                        _currentPage = 19;
                    }
                    break;

                // Page 5: Attempt Repair via skill game (opens modal, return early)
                case 5:
                    await OpenGuessingChallenge(); // opens form; callback handles result and navigation                                                            ***********Needs to open Guessing form***********
                    return;

                // Page 6: Corridor advance for AI
                case 6:
                    _currentPage = 7;
                    break;

                // Page 7: Activate AI -> go to AI online summary (page 10)
                case 7:
                    _player.TrustAI = true;
                    _player.Morality += 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 10;
                    break;

                // Page 8: Search pods -> chance to find survivor, then return to control room (page 9)
                case 8:
                    if (_rng.Next(100) < 70)
                    {
                        _player.SurvivorFound = true;
                        _player.Morality += 1;
                    }
                    _player.TimeRemaining -= 1;
                    _currentPage = 9;
                    break;

                // Page 9: Begin Challenge -> open guessing form (returns early)
                case 9:
                    await OpenGuessingChallenge();
                    return;

                // Page 10: Try to stabilize power -> effect and continue to AI online page (page 10)
                case 10:
                    _player.Power += 5;
                    _player.TimeRemaining -= 1;
                    _currentPage = 2;
                    break;

                // Page 11: Continue -> page 11
                case 11:
                    _currentPage = 11;
                    break;

                // Page 12: Continue -> page 12
                case 12:
                    _currentPage = 12;
                    break;

                // Page 13: Return to control room -> go to page 2
                case 13:
                    _currentPage = 2;
                    break;

                // Page 14: Continue -> advance to after 3 choices
                case 14:
                    _currentPage = 6;
                    break;

                // Page 15: Continue searching -> see data stick (page 16)
                case 15:
                    _currentPage = 16;
                    break;

                // Page 16: Continue -> back to control room (page 17)
                case 16:
                    _currentPage = 17;
                    break;

                //page 17: Continue -> back to control room (page 2)
                case 17:
                    _currentPage = 2;
                    break;

                // Default: advance
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

            //Sound.PlayClick();
            //cases for all pages where button 2 is relevant
            switch (_currentPage)
            {
                // Page 2: Communications
                case 2:
                    _player.Power -= 5;
                    _player.Knowledge += 1;
                    _currentPage = 4;
                    break;

                // Page 3: Return to control room
                case 3:
                    _currentPage = 2;
                    break;

                // Page 4: Ignore hail -> penalty then go to AI terminal (page 6)
                case 4:
                    _player.Morality -= 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 10;
                    break;

                // Page 5: Return to control room
                case 5:
                    _currentPage = 2;
                    break;

                // Page 6: Leave AI offline -> preserve TrustAI=false and continue (page 10)
                case 6:
                    _player.TrustAI = false;
                    _player.TimeRemaining -= 1;
                    _currentPage = 10;
                    break;

                // Page 7: Return to control room 
                case 7:
                    _player.Knowledge += 1;
                    _player.TimeRemaining -= 1;
                    _currentPage = 2;
                    _AIdone = true;
                    break;

                // Page 8: Access security logs 

                // Page 8: (no Option2 shown) fallthrough to default advance
                default:
                    _currentPage++;
                    break;
            }

            UpdateStatusLabel();
            DisplayPage();
        }


        // --- Option 3 handler ------------------------------------------------------
        private void btnOption3_Click(object sender, EventArgs e)
        {
            if (_isWaitingForPause) return;
            //Sound.PlayClick();
            //cases for all pages where button 3 is relevant
            switch (_currentPage)
            {
                // Page 2: Data Core (third option) -> heavy power cost, higher knowledge, go to Data Core result (page 5)
                case 2:
                    _player.Power -= 10;
                    _player.Knowledge += 2;
                    _currentPage = 5;
                    lifeSupportComp = true;
                    break;

                // Other pages: not used, just advance
                default:
                    _currentPage++;
                    break;
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
        private async Task OpenGuessingChallenge()
        {
            if (_isWaitingForPause) return;

            try
            {
                using (var gue = new Guessing())
                {

                    var result = gue.ShowDialog(this);

                    if (result == DialogResult.OK || gue.skillComplete)
                    {
                        if (gue.skillSuccess)
                        {
                            _player.Knowledge += 2;
                            lblNarrative.Text += "\nVerification Successful!";
                            await Task.Delay(1200);
                            _currentPage++;
                        }
                        else
                        {
                            _player.TimeRemaining -= 1;
                            lblNarrative.Text += "\nVerification Failed!";
                            await Task.Delay(1200);
                            _currentPage += 2;
                        }
                    }
                    else
                    {
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
                    _player.TimeRemaining -= 3;
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
        private string PromptForName()  /*Player player*/
        {
            string input = null;
            int selectedStartTime = -1;
            int selectedDifficultyId = -1;

            using (var prompt = new Form())
            {
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.Width = 420;
                prompt.Height = 190;
                prompt.Text = "Enter your name and choose difficulty";

                var lbl = new Label() { Left = 12, Top = 12, Text = "Name:", Width = 380 };
                var txt = new TextBox() { Left = 12, Top = 36, Width = 380 };

                var btnEasy = new Button() { Text = "Easy (50s)", Left = 12, Width = 110, Top = 72 };
                var btnMedium = new Button() { Text = "Medium (45s)", Left = 132, Width = 110, Top = 72 };
                var btnHard = new Button() { Text = "Hard (30s)", Left = 252, Width = 110, Top = 72 };

                var ok = new Button() { Text = "OK", Left = 220, Width = 75, Top = 112, DialogResult = DialogResult.OK };
                var cancel = new Button() { Text = "Cancel", Left = 305, Width = 75, Top = 112, DialogResult = DialogResult.Cancel };

                // Difficulty click handlers: record selection, then close with OK
                btnEasy.Click += (s, e) =>
                {
                    selectedStartTime = 50;
                    selectedDifficultyId = 0;
                    prompt.DialogResult = DialogResult.OK;
                    prompt.Close();
                };
                btnMedium.Click += (s, e) =>
                {
                    selectedStartTime = 45;
                    selectedDifficultyId = 1;
                    prompt.DialogResult = DialogResult.OK;
                    prompt.Close();
                };
                btnHard.Click += (s, e) =>
                {
                    selectedStartTime = 30;
                    selectedDifficultyId = 2;
                    prompt.DialogResult = DialogResult.OK;
                    prompt.Close();
                };

                prompt.AcceptButton = ok;
                prompt.CancelButton = cancel;

                prompt.Controls.Add(lbl);
                prompt.Controls.Add(txt);
                prompt.Controls.Add(btnEasy);
                prompt.Controls.Add(btnMedium);
                prompt.Controls.Add(btnHard);
                prompt.Controls.Add(ok);
                prompt.Controls.Add(cancel);

                if (prompt.ShowDialog(this) == DialogResult.OK)
                    input = txt.Text;
            }

            // If a difficulty was chosen, record it on the form-level state and assign player's TimeRemaining
            if (selectedStartTime != -1)
            {
                // 
                try
                {
                    _player.TimeRemaining = selectedStartTime;
                }
                catch
                {
                    // ignore reflection errors; these assignments are optional depending on your surrounding class
                }

                // If you have a _player instance with a TimeRemaining property, set it here.
                try
                {
                    var playerField = this.GetType().GetField("_player", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (playerField != null)
                    {
                        var player = playerField.GetValue(this);
                        if (player != null)
                        {
                            var timeProp = player.GetType().GetProperty("TimeRemaining");
                            if (timeProp != null && timeProp.CanWrite)
                                timeProp.SetValue(player, selectedStartTime);
                        }
                    }
                }
                catch
                {
                    // ignore if _player or TimeRemaining are not present
                }
            }
            Timer_Tick(null, null); // Start the timer
            return input;
        }


        // --- Status label update ---------------------------------------------------
        private async void UpdateStatusLabel()
        {
            lblStatus.Text =
                $"Power: {_player.Power}   Time: {_player.TimeRemaining}   Morality: {_player.Morality}   Knowledge: {_player.Knowledge}";

            var pageLbl = this.Controls != null ? this.Controls["lblPageNumber"] as Label : null;
            if (pageLbl != null)
                pageLbl.Text = $"Page: {_currentPage}";

            /*lblStatus.ForeColor = System.Drawing.Color.Red;
            await Task.Delay(250);
            lblStatus.ForeColor = System.Drawing.Color.Black;*/
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // initialize the timer the first time this runs
            if (_gameTimer == null)
            {
                _gameTimer = new System.Windows.Forms.Timer();
                _gameTimer.Interval = 3000; // 3 seconds
                _gameTimer.Tick += Timer_Tick;
                _gameTimer.Start();
                return;
            }

            // on each tick, decrement player's remaining time
            if (_player != null)
            {
                _player.TimeRemaining = Math.Max(0, _player.TimeRemaining - 1);

                // optional: react to time running out
                if (_player.TimeRemaining == 0)
                {
                    _currentPage = 25; ; // force to end state
                }
            }
            UpdateStatusLabel();
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
                    // Option1 = Continue to page 2 (prompt for name handled in btnOption1_Click)
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                // Page 2: Power allocation — three option buttons:
                // Option1 = Life Support (spend power, gain time)
                // Option2 = Communications (spend power, gain knowledge)
                // Option3 = Data Core (higher power cost, higher knowledge gain)
                case 2:
                    if (!lifeSupportComp) { 
                    lblNarrative.Text = "Low Power Warning: choose where to divert power.";
                    } else
                    {
                        lblNarrative.Text = "Console Room: Choose where to continue your exploration.";
                    }
                    // Button labels correspond to handlers' switch(case 2) mapping:
                    // btnOption1 triggers Life Support in btnOption1_Click
                    // btnOption2 triggers Communications in btnOption2_Click
                    // btnOption3 triggers Data Core in btnOption3_Click
                    btnOption1.Text = "Data Core";
                    btnOption2.Text = "Communications";
                    if (lifeSupportComp)
                        btnOption3.Text = "Corridor";
                    else
                        btnOption3.Text = "Life Support";
                    btnOption1.Visible = btnOption2.Visible = btnOption3.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = btnOption3.Enabled = true;
                    break;

                // Page 3: Corridor choices — two option buttons:
                // Option1 = Investigate (follow the sound to the challenge)
                // Option2 = Return to control room (go back to the hub)
                case 3:
                    lblNarrative.Text = "You move down the corridor. It is quiet...";
                    await Task.Delay(900);
                    lblNarrative.Text += "\nA strange sound echoes from the data core.";
                    // btnOption1 maps to Investigate (btnOption1_Click -> case 3 sets _currentPage = 8)
                    // btnOption2 maps to Return to control room (btnOption2_Click -> case 3 sets _currentPage = 2)
                    btnOption1.Text = "Investigate";
                    btnOption2.Text = "Return to control room";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                // Page 4: Incoming hail — two option buttons:
                // Option1 = Respond (accept contact; increases TrustAI/morality, costs time)
                // Option2 = Ignore (decline contact; decreases morality, costs time)
                case 4:
                    lblNarrative.Text = "You move down the corridor. An alarm is sounding...";
                    await Task.Delay(900);
                    lblNarrative.Text += "\nA hail sounds from the console.";
                    // btnOption1 (Respond) handled in btnOption1_Click -> sets TrustAI true and navigates to page 6
                    // btnOption2 (Ignore) handled in btnOption2_Click -> reduces morality and navigates to page 6
                    btnOption1.Text = "Respond (70% Chance)";
                    btnOption2.Text = "Ignore";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                // Page 5: Life Support Fix — two option buttons:
                // Option1 = Repair systems (play skill/verification mini-game to repair; success restores Power/Time)
                // Option2 = Return to corridor (go back to corridor/hub)
                case 5:
                    lblNarrative.Text = "You divert power to life support. Systems are unstable but repairable.";
                    await Task.Delay(700);
                    lblNarrative.Text += "\nYou can attempt a manual verification/skill check to repair the systems.";
                    // btnOption1 begins the repair mini-game (handled via OpenGuessingChallenge in btnOption1_Click)
                    // btnOption2 returns to control room (handled in btnOption2_Click -> case 5 sets _currentPage = 2)
                    btnOption1.Text = "Attempt Repair (Skill)";
                    btnOption2.Text = "Return to control room";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 6:
                    lblNarrative.Text = "You continue down the corridor and spot an AI terminal.";
                    await Task.Delay(700);
                    lblNarrative.Text += "\nIt begins pulsing at your approach.";
                    // Option1 = Continue deeper (btnOption1 leads to page 7 via btnOption1_Click case 6)
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                // Page 7: AI terminal — two option buttons:
                // Option1 = Activate AI (toggle trust, adjust morality/time)
                // Option2 = Leave it offline (decline activation, preserve status)
                case 7:
                    lblNarrative.Text = "You reach the AI terminal. Do you want to activate it?";
                    // btnOption1 (Activate AI) handled in btnOption1_Click -> sets TrustAI true and navigates to page 10
                    // btnOption2 (Leave offline) handled in btnOption2_Click -> keeps TrustAI false and navigates to page 2 (also sets _AIdone true)
                    btnOption1.Text = "Activate AI";
                    btnOption2.Text = "Leave it offline";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                // Page 8: Cryo pods — two option buttons:
                // Option1 = Search pods (chance to find survivor, costs time)
                // Option2 = Access security logs (gain information, costs time)
                case 8:
                    lblNarrative.Text = "You approach the cryo pods. What will you do?";
                    // btnOption1 (Search pods) handled in btnOption1_Click -> case 8 performs RNG chance and sets SurvivorFound then goes to page 9
                    // btnOption2 (Access logs) handled in btnOption2_Click -> not currently defined per-case 8 (falls through default)
                    btnOption1.Text = "Search pods";
                    btnOption2.Text = "Access security logs";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                // Page 9: Verification challenge entry — controls:
                // Option1 = Begin Challenge (open guessing modal to resolve signal)
                case 9:
                    lblNarrative.Text = "You reach a terminal with encrypted data. You must verify the signal.";
                    await Task.Delay(700);
                    lblNarrative.Text += "\nInitiating verification challenge...";
                    // Provide both a visible Begin Challenge button and an alternate dedicated button (btnOpenGuessingForm) if present
                    if (btnOpenGuessingForm != null)
                    {
                        btnOpenGuessingForm.Text = "Begin Challenge";
                        btnOpenGuessingForm.Visible = btnOpenGuessingForm.Enabled = true;
                    }
                    // btnOption1 also begins the challenge (btnOption1_Click -> case 9 calls OpenGuessingChallenge)
                    btnOption1.Text = "Begin Challenge";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 10:
                    lblNarrative.Text = "You return to the control room. Systems are still unstable.";
                    // Option1 = Try to stabilize power (btnOption1_Click -> case 10 increases Power and navigates to page 11)
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 11:
                    lblNarrative.Text = "AI is now online. It begins analyzing the station's condition.";
                    // Option1 = Continue to next AI dialog (btnOption1_Click -> case 11 keeps page 11 or advances in code path)
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 12:
                    lblNarrative.Text = "You continue your research and gain insight.";
                    // Option1 = Continue research (btnOption1_Click -> case 12 keeps page 12)
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 13:
                    lblNarrative.Text = "Task Successful, the AI is managing the ship. What’s next?";
                    // Option1 = Return to control room; Option2 = Go to escape pods
                    btnOption1.Text = "Return to control room";
                    btnOption2.Text = "Go to escape pods";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    // btnOption2 is shown but its handler must set the appropriate page when selected
                    break;

                case 14:
                    lblNarrative.Text = "Life Support restored.";
                    // Option1 = Continue after life support (btnOption1_Click -> case 14 sets next page)
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 15:
                    lblNarrative.Text = "You find your own empty pod. The station is deserted.";
                    // Option1 = Continue searching (btnOption1_Click -> case 15 navigates to page 16)
                    // Option2 = Return to control room (btnOption2_Click -> case 15 should navigate to page 2)
                    btnOption1.Text = "Continue searching";
                    btnOption2.Text = "Return to control room";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 16:
                    lblNarrative.Text = "You spot a data stick on the floor by an exit...";
                    // Option1 = Continue (btnOption1_Click -> case 16 navigates forward)
                    btnOption1.Text = "Continue";
                    btnOption1.Visible = btnOption1.Enabled = true;
                    break;

                case 17:
                    lblNarrative.Text = "You return to the control room, power is temporarily stable.";
                    // Option1 = Continue; Option2 = Check Data Core (btnOption2 should route to data core page)
                    btnOption1.Text = "Continue";
                    btnOption2.Text = "Check Data Core";
                    btnOption1.Visible = btnOption2.Visible = true;
                    btnOption1.Enabled = btnOption2.Enabled = true;
                    break;

                case 25:
                    lblNarrative.Text = "The ship begins to rumble and you feel a sense of weightlessness";
                    await Task.Delay(900);
                    lblNarrative.Text += "\n Through the veiwport, you see a massive ship looming over you.";
                    btnOption1.Text = "Continue";
                    break;

                case 26:
                    lblNarrative.Text = "You make your way to the escape pods";
                    await Task.Delay(700);
                    lblNarrative.Text = "\nYou ar confident there is nothing more you can do ot help the ship, and it is time to take your leave.";
                    await Task.Delay(5000);
                    _currentPage = 0;

                    break;

                default:
                    lblNarrative.Text = "End of demo path. Use Restart to try again.";
                    // Default shows Restart on Option1; pressing it sets _restarting true so btnOption1_Click resets the player
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

        private void button2_Click(object sender, EventArgs e)
        {
            _currentPage++;
            UpdateStatusLabel();
            DisplayPage();
        }
    }
}
