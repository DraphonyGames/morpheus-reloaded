using UnityEngine;
using System.Collections;
using Assets.scripts.InventoryScripts;
using Assets.scripts.Player;
namespace Assets.scripts
{
    /// <summary>
    /// This class manages the game over screen (for example: different images for 
    /// whichever transformation you happen to die in)
    /// </summary>
    public class GameOver : MonoBehaviour
    {
        // provides background texture for dead human
        private Texture deadHuman;
        // provides background texture for dead dragon
        private Texture deadDragon;
        // provides background texture for dead squirrel
        private Texture deadSquirrel;
        // provides background texture for dead boar
        private Texture deadBoar;

        // set width for replay button
        private int buttonWidth = 200;
        // set heigth for replay button
        private int buttonHeight = 50;

        // Load inventory manager to store inventory variables
        private Inventory inventory = InventoryManager.inventory;

        // Load game controller to store global variables
        // private GameVariables vars = GameController.gameVariables;

        // sets GUI elements
        void OnGUI()
        {
            // Inizilatize dead creatures textures
            deadHuman = (Texture)Resources.Load("Textures/GameOverScreen/DeadHuman");
            deadDragon = (Texture)Resources.Load("Textures/GameOverScreen/DeadDragon");
            deadSquirrel = (Texture)Resources.Load("Textures/GameOverScreen/DeadSquirrel");
            deadBoar = (Texture)Resources.Load("Textures/GameOverScreen/DeadBoar");

            // Check the current transformation, that a player has and decide which game over screen is displayed
            switch (AbstPlayer.currentTransformation)
            {
                // display dead human
                case 1:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deadHuman);
                    break;
                // display dead dragon
                case 2:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deadDragon);
                    break;
                // display dead squirrel
                case 3:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deadSquirrel);
                    break;
                // display dead deadBoar
                case 4:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deadBoar);
                    break;
            }

            // displaying coins
            GUI.TextField(new Rect(Screen.width / 2 - buttonWidth / 4,
                                Screen.height * 3 / 4, buttonWidth / 2, 20), "Score: " + inventory.GetItems("coins"));
            GUI.TextField(new Rect(Screen.width / 2 - buttonWidth / 4,
                                Screen.height * 3 / 5, buttonWidth / 2, 20), "Score: " + (int)(inventory.GetItems("coins") * GameController.gameVariables.difficultyMode));

            // displaying "play again" button
            if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
                                Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight), "Go to highscore"))
            {
                // enter his score
                Application.LoadLevel("MainMenu");
            }
        }
    }
}