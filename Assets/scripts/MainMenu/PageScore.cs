using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// This class show the score page
    /// </summary>
    public class PageScore : abstPage
    {
        private List<Entry> list;

        // private float elementsHeight = 50;
        private bool enterName = true;
        private string playerName = "";

        private float colX = columnX / 2.5f;
        private float colY = columnY;

        /// <summary>
        /// set the page name
        /// </summary>
        /// <param name="name"></param>
        public PageScore(string name)
        {
            pageName = name;
        }

        /// <summary>
        /// reset page to initialize state
        /// </summary>
        public override void resetClass()
        {
            enterName = true;
            playerName = "";
        }

        /// <summary>
        /// initialize something
        /// </summary>
        public void init()
        {
            if (list == null)
            {
                list = Highscore.readScore();
                list.Sort();
            }
        }

        /// <summary>
        /// draw current page
        /// </summary>
        /// <returns></returns>
        protected override string draw()
        {
            init();
            string temp = null;
            // if is main menu, the player can enter his name and after that the player variables will be reseted
            if (isMainMenu) 
            {
                if (enterName && inventory.GetItems("coins") != 0)
                {
                    inputText();
                }
                else
                {
                    vars.resetVariables();
                    temp = drawTable();
                }
            }
            else
            {
                temp = drawTable();
            }

            return (temp == null) ? pageName : temp;
        }

        /// <summary>
        /// draw current page
        /// </summary>
        /// <returns></returns>
        private string drawTable()
        {
            // header line
            GUI.Label(new Rect(2 * borderX + colX * 0, 2 * borderY + colY * 3, colX, colY), "Position", getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 2, 2 * borderY + colY * 3, colX * 4, colY), "Player Name", getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 4, 2 * borderY + colY * 3, colX * 2, colY), "Hearts", getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 6, 2 * borderY + colY * 3, colX * 2, colY), "Score", getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 8, 2 * borderY + colY * 3, colX * 2, colY), "Difficulty", getTextStyle());
            
            // rows with first 10 entry
            for (int i = 0; !(i >= 8 || i >= list.Count); i++)
            {                                            
                GUI.Label(new Rect(2 * borderX + colX * 0, 2 * borderY + (colY * 3) + colY * (i + 1), colX, colY), "" + (i + 1), getTextStyle());
                GUI.Label(new Rect(2 * borderX + colX * 2, 2 * borderY + (colY * 3) + colY * (i + 1), colX, colY), list[i].name, getTextStyle());
                GUI.Label(new Rect(2 * borderX + colX * 4, 2 * borderY + (colY * 3) + colY * (i + 1), colX, colY), "" + list[i].lastLevel, getTextStyle());
                GUI.Label(new Rect(2 * borderX + colX * 6, 2 * borderY + (colY * 3) + colY * (i + 1), colX, colY), "" + list[i].score, getTextStyle());
                GUI.Label(new Rect(2 * borderX + colX * 8, 2 * borderY + (colY * 3) + colY * (i + 1), colX, colY), "" + (int)(list[i].difficulty), getTextStyle());
            }

            if (isMainMenu && inventory.GetItems("coins") == 0)
            {
                // draw Button to main Menu
                if (GUI.Button(new Rect(2 * borderX + colX * 0, 2 * borderY + (colY * 1) + colY * (10 + 1), columnX, columnY), "Main Menu"))
                {
                    return "MainMenu";
                }
            }

            if (!isMainMenu)
            {
                // draw Button to main Menu
                if (GUI.Button(new Rect(2 * borderX + colX * 0, 2 * borderY + (colY * 1) + colY * (10 + 1), columnX, columnY), "Pause Menu"))
                {
                    return "MainMenu";
                }
            }

            return null;
        }

        private void inputText()
        {
            // for key event in gui
            Event guiEvent = Event.current;

            // input popup
            GUI.DrawTexture(new Rect(2 * borderX + colX * 0, 2 * borderY + colY * 3.5f, columnX * 3, columnY * 3), popUpTexture);
            GUI.Label(new Rect(2 * borderX + colX * 1, 2 * borderY + colY * 4, columnX * 3, columnY * 3), "Enter your Name", getTextStyle());

            // set Focus on TextField
            GUI.SetNextControlName("TextField");
            playerName = GUI.TextField(new Rect(2 * borderX + colX * 3, 2 * borderY + colY * 5, columnX * 3, columnY * 3), playerName, 25, getTextStyle());
            GUI.FocusControl("TextField");

            // keyevent
            if (guiEvent.keyCode == KeyCode.Return)
            {
                Highscore.writeNewScore(new Entry(playerName, vars.hearts, (int)(inventory.GetItems("coins") * GameController.gameVariables.difficultyMode), GameController.gameVariables.difficultyMode));
                enterName = false;
                list = Highscore.readScore();
                list.Sort();
                vars.resetVariables();
            }
        }
    }
}
