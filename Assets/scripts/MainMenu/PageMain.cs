using UnityEngine;
using System.Collections;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// this class show the main page
    /// </summary>
    public class PageMain : abstPage
    {
        /// <summary>
        /// set the page name
        /// </summary>
        /// <param name="name"></param>
        public PageMain(string name)
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
            if (isMainMenu)
            {
                if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 3, columnX, columnY), "Play"))
                {
                    sceneFader.SwitchScene("Part1_Level1");
                }
            }
            else
            {
                if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 3, columnX, columnY), "Resume"))
                {
                    return "Resume";
                }
            }

            if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 6, columnX, columnY), "Score"))
            {
                return "Score";
            }

            if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 9, columnX, columnY), "Settings"))
            {
                return "Settings";
            }

            if (isMainMenu)
            {
                if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 12, columnX, columnY), "Close Game"))
                {
                    Application.Quit();
                }
            }
            else
            {
                if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 12, columnX, columnY), "Main Menu"))
                {
                    Time.timeScale = 1;
                    sceneFader.SwitchScene("MainMenu");
                }
            }

            return pageName;
        }
    }
}
