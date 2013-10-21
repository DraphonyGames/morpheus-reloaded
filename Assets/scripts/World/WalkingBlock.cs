using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// This class manages the world destruction by the lava wave. Once hit, the blocks (or objects) with the script fall down
    /// and can no longer be used for progressing in the level
    /// </summary>
    public class WalkingBlock : MonoBehaviour
    {
        private int minDestroy = 0;
        private int maxDestroy = 10;
        /// <summary>
        /// fog object prefab to be assigned in unity (burning stones)
        /// </summary>
        public GameObject fogPrefab;
        private bool spawnFog = true;
        /// <summary>
        /// random world destruction "explosion" of a block after being hit by a burning meteor
        /// </summary>
        public GameObject BlockExplosion;

        // Use this for initialization
        void Start()
        {
            BlockExplosion = (GameObject)Resources.Load("Prefabs/Enemy/BlockExplosion");
        }

        // Block falls after one second when he made Contact with Lava.
        void OnParticleCollision(GameObject collidingObject)
        {
            if ((collidingObject.gameObject.tag == "Lava") && (spawnFog))
            {
                StartCoroutine("BlockFalls");
                // spawnfog prevents spawning fog for every particle
                this.spawnFog = false;
            }

            if (collidingObject.gameObject.tag == "RockFire")
            {
                int random = Random.Range(minDestroy, maxDestroy);
                if (random > 7)
                {
                    Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Instantiate(BlockExplosion, position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }
        // BlockFalls waits for x Seconds and adds a Rigidbody to the Block(thanks to gravity it falls) and spawns Fog
        IEnumerator BlockFalls()
        {
            yield return new WaitForSeconds(2);
            if (GetComponent("Rigidbody") == null)
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
            // Vector3 positionLeft = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y, transform.position.z);
            Vector3 positionRight = new Vector3(transform.position.x + transform.localScale.x / 2, transform.position.y - transform.localScale.y, transform.position.z);
            // Vector3 middle = new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z);
            // fogParticle1 = (GameObject)Instantiate(fogPrefab, positionLeft, Quaternion.AngleAxis(270, new Vector3(1, 0, 0)));
            Instantiate(fogPrefab, positionRight, Quaternion.AngleAxis(270, new Vector3(1, 0, 0)));
            // fogParticle3 = (GameObject)Instantiate(fogPrefab, middle, Quaternion.AngleAxis(270, new Vector3(1, 0, 0)));
            // Destroy(this.gameObject);
        }
    }
}