using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// This class manages the ultimate game over condition - the pursuing lava
    /// </summary>
    public class Lava : MonoBehaviour
    {
        /// <summary>
        /// speed of the lava wave, also accessed by difficulty settings
        /// </summary>
        public float lavaSpeed;
        // pointer to transform to optimize memory
        private Transform trans;

        /// <summary>
        /// game variables access
        /// </summary>
        public static GameVariables gameVariables;

        // Use this for initialization
        void Start()
        {
            gameVariables = GameController.gameVariables;
            trans = transform;
            // check the difficulty mode and adjust the lava speed to it
            lavaSpeed = GameController.gameVariables.difficultyMode;
        }

        // Update is called once per frame
        void Update()
        {
            trans.position = new Vector3(lavaSpeed * Time.deltaTime + trans.position.x, trans.position.y, trans.position.z);
        }
    }
}