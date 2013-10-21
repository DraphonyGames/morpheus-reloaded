using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// Adjusts the behavior of patrolling enemy
    /// </summary>
    public class EnemyPatrol2 : AbstEnemy
    {
        /// <summary>
        /// loads needed prefab
        /// </summary>
        public GameObject hitEnemyPrefab;
        /// <summary>
        /// works as a switch between this enemy chasing the Player or using his normal Patrol behavior
        /// </summary>
        public bool seePlayer = false;
        /// <summary>
        /// Direction describes the walking direction(view) of this enemy it is used to spawn shots, add force... in the right direction
        /// </summary>
        public int direction = -1;
        /// <summary>
        /// the distance to the Player
        /// </summary>
        public float distance;
        private float hitTime = 1.5f;
        private bool movementAllowedLeft = true;
        private bool movementAllowedRight = true;
        private float seeDistance = 10;
        private Animator animator;
        private bool hasToTurnAround = false;

        /// <summary>
        /// Thanks to the model the enemy has always to move Forward there in no more need to differentiate between left movement or right.
        /// </summary>
        public override void startEnemy()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// overrides the update class of abstract enemy
        /// </summary>
        public override void updateEnemy()
        {
            // get PlayerPosition to compare distance with own position and get Distance.
            float amtToMove = currentspeed * Time.deltaTime;
            float distance = Vector3.Distance(transform.position, AbstEnemy.playerPosition);
            // if player is too far away this enemy will Patrol
            if ((hasToTurnAround) && (!seePlayer))
            {
                transform.Rotate(0, 180, 0);
                hasToTurnAround = false;
                if (direction == 1)
                {
                    direction = -1;
                }

                else
                {
                    direction = 1;
                }
            }

            // if the enemy looses sight of the player he has to switch back to his patrol behavior
            if (distance >= seeDistance)
            {
                seePlayer = false;
                movementAllowedLeft = true;
                movementAllowedRight = true;
                animator.SetBool("IsAttacking", false);
            }

            // if Player is close enough the enemy starts to chase the player
            if (distance < seeDistance)
            {
                seePlayer = true;
                animator.SetBool("IsAttacking", true);
            }

            // Normal Patrol logic if the Enemy is not seeing the Player he Moves right or left (depending on the var direction)
            if ((direction == 1) && (!seePlayer) && (movementAllowedRight))
            {
                transform.Translate(Vector3.left * amtToMove);
            }

            else if ((direction == -1) && (!seePlayer) && (movementAllowedLeft))
            {
                transform.Translate(Vector3.left * amtToMove);
            }

            // cooldown for hit
            hitTime -= Time.deltaTime;
            if (seePlayer)
            {
                // If the Enemy is chasing the Player he will move in his direction and starts punching
                if (((AbstEnemy.playerPosition.x - transform.position.x) >= 0) && (movementAllowedRight))
                {
                    // if it is the direction the enemy is facing he will just move towards the Player
                    if (direction == 1)
                    {
                        transform.Translate(Vector3.left * amtToMove);
                    }

                    // if he looks into the "wrong" direction he turns around
                    if (direction == -1)
                    {
                        transform.Rotate(0, 180, 0);
                        direction = 1;
                    }
                }

                if (((AbstEnemy.playerPosition.x - transform.position.x) < 0) && (movementAllowedLeft))
                {
                    // if it is the direction the enemy is facing he will just move towards the Player
                    if (direction == -1)
                    {
                        transform.Translate(Vector3.left * amtToMove);
                    }

                    // if he looks into the "wrong" direction he turns around
                    if (direction == 1)
                    {
                        transform.Rotate(0, 180, 0);
                        direction = -1;
                    }
                }
            }
            // Punching all 0.5 seconds (if Enemy sees the Player)
            if (seePlayer && (hitTime < 0))
            {
                if (direction == -1)
                {
                    Vector3 positions = new Vector3(transform.position.x - transform.localScale.x - 0.5f, transform.position.y + 0.8f, transform.position.z);
                    Instantiate(hitEnemyPrefab, positions, Quaternion.identity);
                    hitTime = 1f;
                }

                if (direction == 1)
                {
                    Vector3 positions = new Vector3(transform.position.x + transform.localScale.x + 0.5f, transform.position.y + 0.8f, transform.position.z);
                    Instantiate(hitEnemyPrefab, positions, Quaternion.identity);
                    hitTime = 1f;
                }
            }
        }

        /// <summary>
        /// The enemy should change Directions if he hits the AIBorders even when he tries to chase the Player
        /// AI border logic GameObject has to change face direction and has to move in the other direction.
        /// also the enemy has to stop even if he chases the player.
        /// </summary>
        /// <param name="otherObject"> collider of objects the enemy affects </param>
        public override void OnTriggerEnter(Collider otherObject)
        {
            if ((otherObject.gameObject.tag == "AIBorderRight") && (direction == 1) && (!seePlayer))
            {
                direction = -1;
                transform.Rotate(0, 180, 0);
            }

            if ((otherObject.gameObject.tag == "AIBorderLeft") && (direction == -1) && (!seePlayer))
            {
                direction = 1;
                transform.Rotate(0, 180, 0);
            }

            if ((otherObject.gameObject.tag == "AIBorderRight") && (seePlayer))
            {
                movementAllowedRight = false;
                movementAllowedLeft = true;
                hasToTurnAround = true;
            }

            if ((otherObject.gameObject.tag == "AIBorderLeft") && (seePlayer))
            {
                movementAllowedRight = true;
                movementAllowedLeft = false;
                hasToTurnAround = true;
            }
        }
    }
}