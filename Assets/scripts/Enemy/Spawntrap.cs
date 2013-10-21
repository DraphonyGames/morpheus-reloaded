using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// spawns snakes when player passes the trap's position
    /// </summary>
    public class Spawntrap : MonoBehaviour
    {
        /// <summary>
        /// initiates enemy
        /// </summary>
        public GameObject Enemyprefab;
        // Use this for initialization
        void Start()
        {
        }
        // Update is called once per frame
        void Update()
        {
        }
        /// <summary>
        /// If the Player moves into the Zone there is a chance a Snake Spawns
        /// </summary>
        /// <param name="otherObject"></param>
        void OnTriggerEnter(Collider otherObject)
        {
            int random;
            if (otherObject.gameObject.tag == "Player")
            {
                random = Random.Range(0, 10);
                if (random > 2)
                {
                    Vector3 positions = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
                    Instantiate(Enemyprefab, positions, Quaternion.identity);
                }
            }
        }
    }
}