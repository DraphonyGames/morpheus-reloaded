using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.InventoryScripts;

namespace Assets.scripts
{
    /// <summary>
    /// This class keeps game data: difficulty setting, player variables etc.
    /// </summary>
    public class GameVariables : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// initialize variables like lava, player position, energy
        /// </summary>
        public bool initMisc = true;
        /// <summary>
        /// maximum of players life points
        /// </summary>
        public int maxLifePoints = 3;
        /// <summary>
        /// current life points of player
        /// </summary>
        public int lifePoints = 3;
        /// <summary>
        /// starts a new game with 3 hearts
        /// </summary>
        public int initHearts = 3;
        /// <summary>
        /// minimum of 3 hearts are shown on GUI
        /// </summary>
        public int minShowHearts = 3;
        /// <summary>
        /// maximum of 5 hearts are shown on GUI
        /// </summary>
        public int maxShowHearts = 5;
        /// <summary>
        /// current number of hearts
        /// </summary>
        public int hearts = 3;
        /// <summary>
        /// saves last save point position
        /// </summary>
        public Vector3 lastRespawn; // = new Vector3(0, 1.79f, 0);
        /// <summary>
        /// saves lava position at last save point
        /// </summary>
        public Vector3 lastLavaPos; // = new Vector3(-40, 2, 0);
        /// <summary>
        /// saves lava position at last save point (only in Part1_Level1)
        /// </summary>
        public Vector3 lastLavaPos2; 
        /// <summary>
        /// saves current energy at last save point
        /// </summary>
        public float lastEnergy;
        /// <summary>
        /// saves current items
        /// </summary>
        public Dictionary<string, float> lastItems = new Dictionary<string, float>();

        ////private bool respawnPointInitialized = false;

        /// <summary>
        /// whether player is spawned again
        /// </summary>
        public bool wasRespawned = false;

        /// <summary>
        /// find player position for enemies
        /// </summary>
        public Vector3 playerPosition = Vector3.zero;

        /// <summary>
        /// sets difficulty mode
        /// </summary>
        public float difficultyMode = 2;
        ////public string difficultyMode = "easy";

        /// <summary>
        /// A list of levels in which you can't open inventory or pause screen (e.g. main menu).
        /// </summary>
        public List<string> nonPlayableLevels = new List<string>() { "MainMenu", "Settings", "GameControl", "GameOverMenu", "Score", "Load" };
        #endregion

        #region Functions
        /// <summary>
        /// subtract one life from the player - one heart
        /// </summary>
        public void loseHeart()
        {
            hearts--;
            lifePoints = maxLifePoints;
        }

        ////public void nextLvl()
        ////{
        ////    respawnPointInitialized = false;
        ////}

        /// <summary>
        /// saves variables at save point
        /// </summary>
        /// <param name="playerPos"></param>
        /// <param name="lavaPos"></param>
        /// <param name="energy"></param>
        public void savePoint(Vector3 playerPos, Vector3 lavaPos, float energy)
        {
            lastRespawn = new Vector3(playerPos.x, playerPos.y, 0);
            lastLavaPos = new Vector3(lavaPos.x, lavaPos.y, lavaPos.z);
            if (Application.loadedLevelName == "Part1_Level1")
            {
                lastLavaPos2 = new Vector3(lavaPos.x, lavaPos.y - 20, lavaPos.z);
            }

            lastEnergy = energy;
            lastItems.Clear();
            foreach (KeyValuePair<string, float> kvp in InventoryManager.inventory.items)
            {
                lastItems.Add(kvp.Key, kvp.Value);
            }

            // player was Respawned is true
            ////respawnPointInitialized = true;
        }

        /// <summary>
        /// reset Variables back to last save point
        /// </summary>
        public void respawnVariables()
        {
            InventoryManager.inventory.items.Clear();
            foreach (KeyValuePair<string, float> kvp in lastItems)
            {
                InventoryManager.inventory.items.Add(kvp.Key, kvp.Value);
            }

            lifePoints = maxLifePoints;
        }

        /// <summary>
        /// reset life points for a new game
        /// </summary>
        public void resetVariables()
        {
            initMisc = true;
            wasRespawned = false;
            lifePoints = maxLifePoints;
            hearts = initHearts;
            InventoryManager.inventory.items.Clear();
        }

        #endregion
    }
}