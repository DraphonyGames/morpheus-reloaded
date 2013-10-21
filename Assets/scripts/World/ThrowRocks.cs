using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// This class instantiate a rock at the position of the script added position
    /// </summary>
    public class ThrowRocks : MonoBehaviour
    {
        // prefabs
        private GameObject rock;
        private GameObject bigRock;
        private GameObject meteor;
        private Transform trans;
        /// <summary>
        /// if the variable is true, the block will be big, that means, the size/scale=4 
        /// and the rock will have a "lava" tag, so that the player is destroyed if he touches 
        /// the big rock
        /// </summary>
        public bool isBigRock;

        /// <summary>
        /// changes rock style to meteor style
        /// </summary>
        public bool isMeteor;

        /// <summary>
        /// Duration in seconds
        /// </summary>
        public int respawnTime;
        /// <summary>
        /// Wait in milliseconds until starts
        /// </summary>
        public int waitTimeMilli;

        // Time to respawn the rock
        // private long rockCoolDown;
        private float rockLastTimeThrown = 0;
        
        // Use this for initialization
        void Start()
        {
            // rockCoolDown = new System.TimeSpan(0, 0, 0, respawnTime, waitTimeMilli);
            
            rock = (GameObject)Resources.Load("Prefabs/LevelObjects/RigidRock");
            bigRock = (GameObject)Resources.Load("Prefabs/LevelObjects/RigidRockBig");
            meteor = (GameObject)Resources.Load("Prefabs/LevelObjects/DestoryAbleMeteor");
            trans = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (rockLastTimeThrown == 0)
            {
                int random = Random.Range(0, waitTimeMilli);
                rockLastTimeThrown = Time.time + random;
            }

            if (rockLastTimeThrown + respawnTime <= Time.time)
            {
                rockLastTimeThrown = Time.time;
                throwRock();
            }
        }

        /// <summary>
        /// throws rock
        /// </summary>
        private void throwRock()
        {
            if (isMeteor)
            {
                Instantiate(meteor, trans.position, Quaternion.identity);
            }
            else if (isBigRock)
            {
                Instantiate(bigRock, trans.position, Quaternion.identity);
            }
            else
            {
                Instantiate(rock, trans.position, Quaternion.identity);
            }
        }
    }
}