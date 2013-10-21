using UnityEngine;
using System.Collections;
namespace Assets.scripts
{
    /// <summary>
    /// This class manages the transition to the next Level.
    /// </summary>
    public class NextLevelCollider : MonoBehaviour
    {
        /// <summary>
        /// saves game variables for re-spawn
        /// </summary>
        public static GameVariables gameVariables;
        private SceneFader sceneFader;
        private int maxLife;
        void Awake()
        {
            sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
        }

        void Start()
        {
            gameVariables = GameController.gameVariables;
        }

        /// <summary>
        /// Name of the level that should be loaded next.
        /// </summary>
        public string nextLevelName = "";
        // private string tag = "Player";

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameVariables.lifePoints = 3;
                NextLevel();
            }
        }

        void NextLevel()
        {
            sceneFader.SwitchScene(nextLevelName);
        }
    }
}