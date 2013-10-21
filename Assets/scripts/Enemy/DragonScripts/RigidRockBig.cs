using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy.DragonScripts
{
    /// <summary>
    /// rock that gets destroyed if it lands on a game object 
    /// which is tagged as "easy". This tag is used, because this 
    /// option is just for a special case, and "easy" is normally not 
    /// used in the levels, only in the settings scene
    /// </summary>
    public class RigidRockBig : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// destroy this game objects, if it collides with an object
        /// tagged with "easy"
        /// </summary>
        /// <param name="c"></param>
        void OnTriggerEnter(Collider c)
        {
            switch (c.gameObject.tag)
            {
                case "easy":
                    Destroy(gameObject);
                    break;
            }
        }
    }
}