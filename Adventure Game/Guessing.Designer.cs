namespace Adventure_Game
{
    partial class Guessing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GuessBox = new System.Windows.Forms.TextBox();
            this.GuessButton = new System.Windows.Forms.Button();
            this.feedback = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.testDisp = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.countBox = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(108, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Verification";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(59, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter a Number Between 1 and 100";
            // 
            // GuessBox
            // 
            this.GuessBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuessBox.Location = new System.Drawing.Point(176, 175);
            this.GuessBox.Name = "GuessBox";
            this.GuessBox.Size = new System.Drawing.Size(38, 29);
            this.GuessBox.TabIndex = 2;
            this.GuessBox.TextChanged += new System.EventHandler(this.GuessBox_TextChanged);
            // 
            // GuessButton
            // 
            this.GuessButton.BackColor = System.Drawing.Color.Black;
            this.GuessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuessButton.ForeColor = System.Drawing.Color.White;
            this.GuessButton.Location = new System.Drawing.Point(152, 229);
            this.GuessButton.Name = "GuessButton";
            this.GuessButton.Size = new System.Drawing.Size(84, 45);
            this.GuessButton.TabIndex = 3;
            this.GuessButton.Text = "Guess";
            this.GuessButton.UseVisualStyleBackColor = false;
            this.GuessButton.Click += new System.EventHandler(this.GuessButton_Click);
            // 
            // feedback
            // 
            this.feedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback.ForeColor = System.Drawing.Color.White;
            this.feedback.Location = new System.Drawing.Point(12, 304);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(364, 20);
            this.feedback.TabIndex = 4;
            this.feedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.feedback.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(63, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Please Enter A Number";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // testDisp
            // 
            this.testDisp.AutoSize = true;
            this.testDisp.ForeColor = System.Drawing.Color.Cornsilk;
            this.testDisp.Location = new System.Drawing.Point(16, 22);
            this.testDisp.Name = "testDisp";
            this.testDisp.Size = new System.Drawing.Size(16, 13);
            this.testDisp.TabIndex = 6;
            this.testDisp.Text = "---";
            this.testDisp.Visible = false;
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.Black;
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.ForeColor = System.Drawing.Color.White;
            this.resetButton.Location = new System.Drawing.Point(289, 175);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(66, 29);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Close";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Visible = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 8;
            // 
            // countBox
            // 
            this.countBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countBox.ForeColor = System.Drawing.Color.White;
            this.countBox.Location = new System.Drawing.Point(24, 184);
            this.countBox.Name = "countBox";
            this.countBox.Size = new System.Drawing.Size(108, 23);
            this.countBox.TabIndex = 9;
            this.countBox.Text = "Guess #:";
            this.countBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.countBox.Visible = false;
            this.countBox.Click += new System.EventHandler(this.countBox_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(155, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Give Up";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // Guessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(388, 360);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.countBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.testDisp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.feedback);
            this.Controls.Add(this.GuessButton);
            this.Controls.Add(this.GuessBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Guessing";
            this.Text = "Guessing Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Guessing_FormClosing);
            this.Load += new System.EventHandler(this.Guessing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox GuessBox;
        private System.Windows.Forms.Button GuessButton;
        private System.Windows.Forms.Label feedback;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label testDisp;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label countBox;
        private System.Windows.Forms.Button button1;
    }
}

