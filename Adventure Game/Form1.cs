using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Game
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            //start the guessing game
            Guessing skilltest = new Guessing();
            skilltest.ShowDialog();

            //import guessing variables
            bool completeGuess = skilltest.skillComplete;
            bool succeedGuess = skilltest.skillSuccess;
            int guesses = skilltest.guessCount;
            if (completeGuess) 
            {
                await Task.Delay(500);
                returnLabel.Text = $"{guesses} guesses!";
            }

        }
    }
}
