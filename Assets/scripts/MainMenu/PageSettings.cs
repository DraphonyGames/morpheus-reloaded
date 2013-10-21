using UnityEngine;
using System.Collections;
using Assets.scripts;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// This class show the settings page
    /// </summary>
    public class PageSettings : abstPage
    {
        /// <summary>
        /// set the page name
        /// </summary>
        /// <param name="name"></param>
        public PageSettings(string name)
        {
            pageName = name;
        }

        /// <summary>
        /// reset page to initialize state
        /// </summary>
        public override void resetClass()
        {
        }

        /// <summary>
        /// draw current page
        /// </summary>
        /// <returns></returns>
        protected override string draw()
        {
            GUI.Label(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 3, columnX, columnY), "Difficulty - Lava Speed: " + (int)(vars.difficultyMode), getTextStyle());
            if (isMainMenu)
            {
                vars.difficultyMode = GUI.HorizontalSlider(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 4, columnX, columnY), (int)vars.difficultyMode, 1.0f, 8.0f);
            }
            // GUI.Label(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 6, columnX, columnY), "Sound:", getTextStyle());
            if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 6, columnX, columnY), (AudioListener.pause) ? "Sound: Turn on" : "Sound: Turn off"))
            {
                AudioListener.pause = !AudioListener.pause;
            }

            if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 9, columnX, columnY), "Game control"))
            {
                return "GameControl";
            }

            if (isMainMenu)
            {
                if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 12, columnX, columnY), "Main Menu"))
                {
                    return "MainMenu";
                }
            }

            else
            {
                if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 12, columnX, columnY), "Pause Menu"))
                {
                    return "MainMenu";
                }
            }

            return pageName;
        }
    }
}
