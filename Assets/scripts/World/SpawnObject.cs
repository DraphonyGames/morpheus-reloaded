using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// This class triggers a lava fountain
    /// </summary>
    public class SpawnObject : MonoBehaviour
    {
        /// <summary> 
        /// the name of the game object which is going to be spawned
        /// </summary>
        public string nameOfObjectToSpawn;

        /// <summary>
        /// time between the spawn in seconds
        /// </summary>
        public static int timeBetweenSpawn = 3;

        /// <summary>
        /// if this is true, the object that is spawned will be a meteor
        /// </summary>
        public bool isMeteor;

        // prefabs
        private GameObject objectToSpawn;
        private Transform trans;

        // Time to respawn the object
        private System.TimeSpan coolDown = new System.TimeSpan(0, 0, 0, 10, 0);
        private System.DateTime lastTimeSpawnd = System.DateTime.Now;

        private
            // Use this for initialization
        void Start()
        {
            if (isMeteor)
            {
                objectToSpawn = (GameObject)Resources.Load("Prefabs/LevelObjects/Empty");
            }
            else
            {
                objectToSpawn = (GameObject)Resources.Load("Prefabs/LevelObjects/Empty");
            }

            trans = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (lastTimeSpawnd + coolDown <= System.DateTime.Now)
            {
                lastTimeSpawnd = System.DateTime.Now;
                gameObjectSpawnTrigger();
            }
        }

        /// <summary>
        /// throws an empty game object on the killing plane, which spawns a lava fountain
        /// </summary>
        private void gameObjectSpawnTrigger()
        {
            Instantiate(objectToSpawn, trans.position, Quaternion.identity);
        }
    }
}