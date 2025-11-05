// Form1.Designer.cs
// Auto-generated layout code for Adventure Game main form
// Adapted to the AdventureGame namespace (matches project in your repo).

namespace Adventure_Game
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // UI controls
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picPageImage;
        private System.Windows.Forms.Label lblNarrative;
        private System.Windows.Forms.Button btnOption1;
        private System.Windows.Forms.Button btnOption2;
        private System.Windows.Forms.Button btnOption3;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPageNumber;
        private System.Windows.Forms.Button btnOpenGuessingForm;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblTitle = new System.Windows.Forms.Label();
            this.picPageImage = new System.Windows.Forms.PictureBox();
            this.lblNarrative = new System.Windows.Forms.Label();
            this.btnOption1 = new System.Windows.Forms.Button();
            this.btnOption2 = new System.Windows.Forms.Button();
            this.btnOption3 = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPageNumber = new System.Windows.Forms.Label();
            this.btnOpenGuessingForm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPageImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(18, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(740, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ADVENTURE: AURORA STATION";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPageImage
            // 
            this.picPageImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPageImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picPageImage.Location = new System.Drawing.Point(520, 64);
            this.picPageImage.Name = "picPageImage";
            this.picPageImage.Size = new System.Drawing.Size(320, 360);
            this.picPageImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPageImage.TabIndex = 1;
            this.picPageImage.TabStop = false;
            // 
            // lblNarrative
            // 
            this.lblNarrative.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNarrative.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNarrative.Location = new System.Drawing.Point(18, 64);
            this.lblNarrative.Name = "lblNarrative";
            this.lblNarrative.Size = new System.Drawing.Size(486, 240);
            this.lblNarrative.TabIndex = 2;
            this.lblNarrative.Text = "Narrative text appears here. Multi-line, supports segments for pauses.";
            // 
            // btnOption1
            // 
            this.btnOption1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOption1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOption1.Location = new System.Drawing.Point(18, 320);
            this.btnOption1.Name = "btnOption1";
            this.btnOption1.Size = new System.Drawing.Size(300, 40);
            this.btnOption1.TabIndex = 3;
            this.btnOption1.Text = "Option 1";
            this.btnOption1.UseVisualStyleBackColor = true;
            this.btnOption1.Click += new System.EventHandler(this.btnOption1_Click);
            // 
            // btnOption2
            // 
            this.btnOption2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOption2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOption2.Location = new System.Drawing.Point(18, 370);
            this.btnOption2.Name = "btnOption2";
            this.btnOption2.Size = new System.Drawing.Size(300, 40);
            this.btnOption2.TabIndex = 4;
            this.btnOption2.Text = "Option 2";
            this.btnOption2.UseVisualStyleBackColor = true;
            this.btnOption2.Click += new System.EventHandler(this.btnOption2_Click);
            // 
            // btnOption3
            // 
            this.btnOption3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOption3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOption3.Location = new System.Drawing.Point(18, 420);
            this.btnOption3.Name = "btnOption3";
            this.btnOption3.Size = new System.Drawing.Size(300, 40);
            this.btnOption3.TabIndex = 5;
            this.btnOption3.Text = "Option 3";
            this.btnOption3.UseVisualStyleBackColor = true;
            this.btnOption3.Click += new System.EventHandler(this.btnOption3_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRestart.Location = new System.Drawing.Point(736, 436);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(104, 28);
            this.btnRestart.TabIndex = 6;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblStatus.Location = new System.Drawing.Point(18, 470);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(682, 36);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Power: 100   Time: 7   Morality: 0   Knowledge: 0";
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPageNumber.Location = new System.Drawing.Point(736, 24);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Size = new System.Drawing.Size(104, 20);
            this.lblPageNumber.TabIndex = 8;
            this.lblPageNumber.Text = "Page: 1";
            this.lblPageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenGuessingForm
            // 
            this.btnOpenGuessingForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenGuessingForm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOpenGuessingForm.Location = new System.Drawing.Point(620, 436);
            this.btnOpenGuessingForm.Name = "btnOpenGuessingForm";
            this.btnOpenGuessingForm.Size = new System.Drawing.Size(104, 28);
            this.btnOpenGuessingForm.TabIndex = 9;
            this.btnOpenGuessingForm.Text = "Open Guessing";
            this.btnOpenGuessingForm.UseVisualStyleBackColor = true;
            this.btnOpenGuessingForm.Click += new System.EventHandler(this.btnOpenGuessingForm_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.Location = new System.Drawing.Point(620, 476);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 28);
            this.button1.TabIndex = 10;
            this.button1.Text = "Back(TST)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.Location = new System.Drawing.Point(736, 478);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 28);
            this.button2.TabIndex = 11;
            this.button2.Text = "Fow(TST)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(864, 516);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOpenGuessingForm);
            this.Controls.Add(this.lblPageNumber);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnOption3);
            this.Controls.Add(this.btnOption2);
            this.Controls.Add(this.btnOption1);
            this.Controls.Add(this.lblNarrative);
            this.Controls.Add(this.picPageImage);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(880, 555);
            this.Name = "Form1";
            this.Text = "Adventure Game";
            ((System.ComponentModel.ISupportInitialize)(this.picPageImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
