using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.InventoryScripts;

namespace Assets
{   
    /// <summary>
    /// initiates loading screen
    /// </summary>
    public class Load : MonoBehaviour
    {
        private SceneFader sceneFader;

        // Use this for initialization
        void Start()
        {
            sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
            Screen.showCursor = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > 3)
            {
                sceneFader.SwitchScene("MainMenu");
            }
        }
    }
}