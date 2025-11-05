using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace Adventure_Game
{
    public partial class Sound
    {
        public SoundPlayer backroundMusic;
        public SoundPlayer click;
        bool backPlaying = false;



        public async void StartBackgroundLoop()
        {
            backroundMusic = new SoundPlayer(Properties.Resources.back);
            backroundMusic.Play();
        }


        public void ClickSf()
        {

        }
    }
}