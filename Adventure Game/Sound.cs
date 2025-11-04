using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace Adventure_Game
{
    public partial class Sound
    {
        public SoundPlayer backroundMusic;
        public SoundPlayer click;
        bool backPlaying = false;



        public void StartBackgroundLoop()
        {
            backroundMusic = new SoundPlayer(Properties.Resources.back);
            backroundMusic.PlayLooping();
            backPlaying = true;
        }


        public void ClickSf()
        {
            click = new SoundPlayer(Properties.Resources.click);
            click.Play();
        }
    }
}