using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Bomberman
{
    class Sound
    {
        #region Singleton
        private static Sound instance;
        public Sound(ContentManager Content)
        {
            MediaPlayer.Stop();

            //sampleMediaLibrary = new MediaLibrary();
            //rand = new Random();
            //int i = rand.Next(0, sampleMediaLibrary.Albums.Count - 1);
            //for(int j=0; j < sampleMediaLibrary.Albums.Count; j++)
            //    Debug.WriteLine(sampleMediaLibrary.Albums[j].Songs[0].Name);
            //MediaPlayer.Play(sampleMediaLibrary.Albums[i].Songs[0]);
            //SoundEffect bgEffect;
            //bgEffect = Game.Content.Load<SoundEffect>("EpicUnease");
            //SoundEffectInstance instance = bgEffect.CreateInstance();
            //instance.IsLooped = true;
            //bgEffect.Play(0.1f, 0.0f, 0.0f);

            Song s = Content.Load<Song>("bg");
            MediaPlayer.Play(s);
            MediaPlayer.IsRepeating = true;

            Sfx = true;
            foreach (SoundBankItem sbi in bank)
            {
                sbi.sound = Content.Load<SoundEffect>(sbi.name);
            }
        }
        public static Sound Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

     
        #endregion

        ~Sound()
        {
            MediaPlayer.Stop();
        }

        class SoundBankItem
        {
            public SoundBankItem(String text)
            {
                name = text;
            }
            public SoundEffect sound;
            public String name;
        }


        public bool Sfx
        {
            get;
            set;
        }

        SoundBankItem[] bank = { new SoundBankItem("Sbomb"),
                               new SoundBankItem("Spup"),
                               new SoundBankItem("Stick"), 
                               new SoundBankItem("Sdeath"), 
                               new SoundBankItem("Send") };

        MediaLibrary sampleMediaLibrary;
        Random rand;

        public SoundEffect this[String s] 
        {
            get
            {
                foreach (SoundBankItem sbi in bank)
                {
                    if (sbi.name.Equals(s))
                        return sbi.sound;
                }
                return null;
            }
            set
            {
            }
        }

        public void Play(string name)
        {
            if(Sfx)
                this[name].Play();
        }
    }
}
