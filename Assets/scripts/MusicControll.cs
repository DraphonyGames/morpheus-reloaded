using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Assets.scripts
{
    /// <summary>
    /// Class manages (background) music
    /// </summary>
    public class MusicControll : MonoBehaviour
    {
        private static MusicControll musicControll = null; // for check the instance
        private static int currentTrack = 1; // for iterate sounds
        private static AudioClip menuSound; // save the menuesound
        private static AudioClip[] backgound = new AudioClip[2]; // save the backgroundsound
        private static int lastLvl = -1; // for check if lvl chnaged
        private static List<int> menueLevels = new List<int>(); // for looking up if the lvl is a menue lvl

        /// <summary>
        /// property to get the instance of the class
        /// </summary>
        public static MusicControll instance
        {
            get { return musicControll; }
        }

        void Awake()
        {
            if (musicControll != null && musicControll != this)
            {
                Destroy(transform.gameObject);
                return;
            }
            else
            {
                musicControll = this;
            }

            DontDestroyOnLoad(this);
            // each menueLevel muss be add
            if (menueLevels.Count == 0)
            {
                for (int i = 0; i <= 2; i++)
                {
                    menueLevels.Add(i);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            menuSound = ((AudioClip)Resources.Load("Sounds/Background/Menue/CWNC_-_Alternate_Universe"));
            backgound[0] = ((AudioClip)Resources.Load("Sounds/Background/Game/Andertaker_-_Total_destruction"));
            backgound[1] = ((AudioClip)Resources.Load("Sounds/Background/Game/Applewise_-_Destruction__feat_Sony_Ericson_"));
        }

        // Update is called once per frame
        void Update()
        {
            // if lvl change
            if (lastLvl != Application.loadedLevel || ! audio.isPlaying)
            {
                // check if current lvl contains to the menue
                if (menueLevels.Contains(Application.loadedLevel))
                {
                    if (!audio.isPlaying || !menueLevels.Contains(lastLvl))
                    {
                        playMenuSound();
                    }
                }
                else
                {
                    // no sound or last lvl was menue lvl
                    if (!audio.isPlaying || menueLevels.Contains(lastLvl))
                    {
                        playGameSound();
                    }
                }

                lastLvl = Application.loadedLevel;
            }
        }

        /// <summary>
        /// start menu sound
        /// </summary>
        private void playMenuSound()
        {
            audio.Stop();
            audio.clip = menuSound;
            audio.Play();
        }
        /// <summary>
        /// start game background sound
        /// </summary>
        private void playGameSound()
        {
            audio.Stop();
            audio.clip = backgound[(currentTrack++) % backgound.Length];
            audio.Play();
        }
    }
}