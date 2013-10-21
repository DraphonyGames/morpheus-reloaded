using UnityEngine;
using System.Collections;
namespace Assets.scripts
{
    /// <summary>
    /// Fading in and out of a scene from clear / black screen instead of hard transitions.
    /// </summary>
    public class SceneFader : MonoBehaviour
    {
        /// <summary>
        /// texture used in the screen fading
        /// </summary>
        public Texture2D texture;
        /// <summary>
        /// speed of fading in / out of a scene
        /// </summary>
        public float fadeSpeed = 5.0f;
        /// <summary>
        /// name of the next level that is being loaded
        /// </summary>
        public string nextLevel = "MainMenu";
        // rectangle being drawn to cover the whole sccreen
        private Rect screenRect;
        // current color (clear or black) used for the fading
        private Color currentColor;
        // describes whether the scene is just being loaded
        private bool isStarting = true;
        // describes whether we leave the scene
        private bool isEnding = false;
        /// <summary>
        /// saves needed variables for re spawn
        /// </summary>
        public static GameVariables gameVariables;

        // Awake() is run before Start(), initializing values
        void Awake()
        {
            gameVariables = GameController.gameVariables;
            screenRect = new Rect(0, 0, Screen.width, Screen.height);
            currentColor = Color.black;
        }

        // Update is called once per frame
        void Update()
        {
            if (isStarting)
            {
                FadeIn();
            }

            if (isEnding)
            {
                FadeOut();
            }
        }

        // GUI elements being drawn, in this case the rectangle
        void OnGUI()
        {
            if (isStarting || isEnding)
            {
                GUI.depth = 0;
                GUI.color = currentColor;
                GUI.DrawTexture(screenRect, texture, ScaleMode.StretchToFill);
            }
        }
        // fade into the scene - change color from black to clear
        void FadeIn()
        {
            currentColor = Color.Lerp(currentColor, Color.clear, fadeSpeed * Time.deltaTime);
            if (currentColor.a <= 0.05f)
            {
                currentColor = Color.clear;
                isStarting = false;
            }
        }

        // fade out of the sccene - change color of the rectangle from clear to black
        void FadeOut()
        {
            currentColor = Color.Lerp(currentColor, Color.black, fadeSpeed * Time.deltaTime);
            if (currentColor.a >= 0.95f)
            {
                currentColor.a = 1;
                Application.LoadLevel(nextLevel);
                // would make sense logically, but only gives another unimpaired glimpse of the scene before transition
                // isEnding = false;
            }
        }

        /// <summary>
        /// method to load the next scene with resetting the variables which say whether we enter/leave a scene
        /// </summary>
        /// <param name="nextSceneName">name of the scene being loaded</param>
        public void SwitchScene(string nextSceneName)
        {
            nextLevel = nextSceneName;
            isEnding = true;
            isStarting = false;
        }
    }
}