using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// Class implements the Tiger enemy.
    /// </summary>
    public class Tiger : AbstEnemy
    {
        /// <summary>
        /// describes whether the player is seen
        /// </summary>
        public bool seePlayer = false;
        /// <summary>
        /// jumping direction
        /// </summary>
        public int direction = -1;
        /// <summary>
        /// distance to player
        /// </summary>
        public float distance;
        // private static bool seePlayer = true;
        private float jumpTime = 4f;
        // public static int direction = 1;
        private bool movementAllowedLeft = true;
        private bool movementAllowedRight = true;
        private float seeDistance = 15;
        private Animator animator;
        ////private bool HasToTurnAround = false;
        private int sprintSpeed = 9;
        private bool jump = false;

        /// <summary>
        /// Thanks to the model the enemy has always to move Forward there in no more need to differentiate between left movement or right.
        /// </summary>
        public override void startEnemy()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isWalking", true);
        }
        /// <summary>
        /// Update method for tiger class, is executed every frame.
        /// </summary>
        public override void updateEnemy()
        {
            // get PlayerPosition to compare distance with own position and get Distance.
            float amtToMove = currentspeed * Time.deltaTime;
            distance = Vector3.Distance(transform.position, AbstEnemy.playerPosition);
            if (jump)
            {
                jump = false;
                transform.gameObject.tag = "Tiger";
                StartCoroutine("waitForEnd");
                // transform.Translate(Vector3.left * amtToMove * (sprintSpeed/2));
            }
            // if player is too far away this enemy will Patrol
            if (distance >= seeDistance)
            {
                if (!animator.GetBool("isWalking"))
                {
                    animator.SetBool("isWalking", true);
                }

                seePlayer = false;
                transform.gameObject.tag = "Enemy";
                movementAllowedLeft = true;
                movementAllowedRight = true;
            }
            // if Player is close enough the enemy starts to chase the player
            if (distance < seeDistance)
            {
                seePlayer = true;
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
            jumpTime -= Time.deltaTime;
            if (seePlayer)
            {
                // If the Enemy is chasing the Player he will move in his direction and starts jumping
                if (((AbstEnemy.playerPosition.x - transform.position.x) > 0) && (movementAllowedRight))
                {
                    if (direction == 1)
                    {
                        jump = true;
                    }

                    if (direction == -1)
                    {
                        transform.Rotate(0, 180, 0);
                        direction = 1;
                        jump = true;
                    }

                    movementAllowedLeft = false;
                    movementAllowedRight = false;
                }

                if (((AbstEnemy.playerPosition.x - transform.position.x) < 0) && (movementAllowedLeft))
                {
                    if (direction == -1)
                    {
                        jump = true;
                    }

                    if (direction == 1)
                    {
                        transform.Rotate(0, 180, 0);
                        direction = -1;
                        jump = true;
                    }

                    movementAllowedRight = false;
                    movementAllowedLeft = false;
                }
            }
            //// jumpAttack all 4 seconds (if Enemy sees the Player)
            ////     if (seePlayer && (jumpTime<0))
            ////     {
            //// toDo jump preparation
            ////         StartCoroutine("waitForEnd");

            ////    }
        }

        /// <summary>
        /// The enemy should change Directions if he hits the AIBorders even when he tries to chase the Player
        /// </summary>
        /// <param name="otherObject">object the tiger collides with</param>
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
        }
        /// <summary>
        /// Waits for the end of the prepare to jump Animation than jumps
        /// </summary>
        /// <returns></returns>
        IEnumerator waitForEnd()
        {
            // float amtToMove = currentspeed * Time.deltaTime;
            animator.SetBool("isWalking", false);
            yield return new WaitForSeconds(1);
            distance = Vector3.Distance(transform.position, AbstEnemy.playerPosition);

            if (distance < seeDistance)
            {
                gameObject.rigidbody.AddForce(sprintSpeed * direction * 2, 9, 0, ForceMode.Impulse);
            }
            else
            {
                seePlayer = false;
            }
        }
    }
}