using UnityEngine;
using System.Collections;
namespace Assets.scripts
{
    /// <summary>
    /// part of the convoluted main menu - link to return to main menu
    /// </summary>
    public class ReturnToMainMenu : MonoBehaviour
    {
        private SceneFader sceneFader;
        void Awake()
        {
            sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
        }

        void OnMouseDown()
        {
            sceneFader.SwitchScene("MainMenu");
        }
    }
}