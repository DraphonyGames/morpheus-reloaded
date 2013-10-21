using UnityEngine;
using System.Collections;
using Assets.scripts;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// This class show the Game control
    /// </summary>
    public class PageGameControl : abstPage
    {
        private float colX = columnX / 2.5f;
        private float colY = columnY;

        /// <summary>
        /// set the page name
        /// </summary>
        /// <param name="name"></param>
        public PageGameControl(string name)
        {
            pageName = name;
        }

        /// <summary>
        /// reset page to initialize state
        /// </summary>
        public override void resetClass()
        {
        }

        private void drawRow(string a, string b, string c, string d, string e, int row)
        {
            row++;
            GUI.Label(new Rect(2 * borderX + colX * 0, 2 * borderY + (colY * 1) + colY * (row), colX, colY), a, getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 3, 2 * borderY + (colY * 1) + colY * (row), colX, colY), b, getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 5, 2 * borderY + (colY * 1) + colY * (row), colX, colY), c, getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 7, 2 * borderY + (colY * 1) + colY * (row), colX, colY), d, getTextStyle());
            GUI.Label(new Rect(2 * borderX + colX * 9, 2 * borderY + (colY * 1) + colY * (row), colX, colY), e, getTextStyle());
        }

        /// <summary>
        /// draw current page
        /// </summary>
        /// <returns></returns>
        protected override string draw()
        {
            drawRow("Game Control:", "", "", "", "", 0);
            drawRow("Basic Movement", "Left", "Right", "Up", "Down", 2);
            drawRow("Transformation:", "1: Human", "2: Dragon", "3: Squirrel", "4: Boar", 4);
            drawRow("First Ability: E", "Push", "Fire", "Gliding", "Tackle", 5);
            drawRow("Second Ability: Q", "Runup", "Bodycheck", "Climbing", "Push", 6);
            drawRow("Inventory:", "I", "", "", "", 7);
            drawRow("Pause:", "Esc", "", "", "", 8);

            if (GUI.Button(new Rect(2 * borderX + columnX * 0, 2 * borderY + columnY * 12, columnX, columnY), "Settings"))
            {
                return "Settings";
            }

            return pageName;
        }
    }
}
