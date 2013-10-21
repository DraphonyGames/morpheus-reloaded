using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// Adjusts the behavior of patrolling enemy
    /// </summary>
    public class EnemyPatrol : AbstEnemy
    {
        /// <summary>
        /// loads needed prefab
        /// </summary>
        public GameObject hitEnemyPrefab;
        /// <summary>
        /// Direction describes the walking direction(view) of this enemy it is used to spawn shots, add force... in the right direction
        /// </summary>
        public static int direction = 1;
        /// <summary>
        /// distance between enemy and player
        /// </summary>
        public float distance;
        /// <summary>
        /// works as enemy sight
        /// </summary>
        public RaycastHit hit;
        /// <summary>
        /// sets range of sight
        /// </summary>
        public int sightRange;

        private bool seePlayer = false;
        private float hitTime = 1;
        private bool movementAllowedLeft = true;
        private bool movementAllowedRight = true;
        private System.DateTime startSeePlayer;
        private System.TimeSpan cooldownSeePlayer;

        /// <summary>
        /// overrides start class of abstract enemy
        /// </summary>
        public override void startEnemy()
        {
            sightRange = 10;
            // determinates the cooldown time, if the player is outside of the enemy sight range
            cooldownSeePlayer = new System.TimeSpan(0, 0, 0, 0, 100);
        }

        /// <summary>
        /// overrides update class of abstract enemy
        /// </summary>
        public override void updateEnemy()
        {
            float amtToMove = currentspeed * Time.deltaTime;

            if (direction == 1)
            {
                if (Physics.Raycast(transform.position, Vector3.right, out hit, sightRange))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        seePlayer = true;
                        startSeePlayer = System.DateTime.Now;
                    }
                }
            }

            else
            {
                if (Physics.Raycast(transform.position, Vector3.left, out hit, sightRange))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        seePlayer = true;
                        startSeePlayer = System.DateTime.Now;
                    }
                }
            }

            if (System.DateTime.Now - startSeePlayer > cooldownSeePlayer)
            {
                seePlayer = false;
            }

            // Normal Patrol logic if the Enemy is not seeing the Player he Moves right or left (depending on the var direction)
            if ((direction == 1) && (!seePlayer))
            {
                transform.Translate(Vector3.right * amtToMove);
            }

            else if ((direction == -1) && (!seePlayer))
            {
                transform.Translate(Vector3.left * amtToMove);
            }

            // cooldown for hit
            hitTime -= Time.deltaTime;
            if (seePlayer)
            {
                // If the Enemy is chasing the Player he will move in his direction and starts punching
                if (((AbstEnemy.playerPosition.x - transform.position.x) > 0) && (movementAllowedRight))
                {
                    transform.Translate(Vector3.right * amtToMove);
                }

                if (((AbstEnemy.playerPosition.x - transform.position.x) < 0) && (movementAllowedLeft))
                {
                    transform.Translate(Vector3.left * amtToMove);
                }
            }

            // Punching all 0.5 seconds (if Enemy sees the Player)
            if (seePlayer && (hitTime < 0))
            {
                Vector3 positions = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z);
                Instantiate(hitEnemyPrefab, positions, Quaternion.identity);
                hitTime = 0.5f;
            }
        }
        /// <summary>
        /// The enemy should change Directions if he hits the AIBorders even when he tries to chase the Player
        /// </summary>
        /// <param name="otherObject"> collider of objects the enemy affects </param>
        public override void OnTriggerEnter(Collider otherObject)
        {
            if ((otherObject.gameObject.tag == "AIBorderRight") && (direction == 1))
            {
                direction = -1;
                movementAllowedLeft = true;
            }

            if ((otherObject.gameObject.tag == "AIBorderLeft") && (direction == -1))
            {
                direction = 1;
                movementAllowedRight = true;
            }

            if ((otherObject.gameObject.tag == "AIBorderRight") && (seePlayer))
            {
                movementAllowedRight = false;
                movementAllowedLeft = true;
            }

            if ((otherObject.gameObject.tag == "AIBorderLeft") && (seePlayer))
            {
                movementAllowedRight = true;
                movementAllowedLeft = false;
            }
        }
    }
}
