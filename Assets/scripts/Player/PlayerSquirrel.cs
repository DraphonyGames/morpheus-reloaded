using UnityEngine;
using System.Collections;

namespace Assets.scripts.Player
{
    /// <summary>
    /// This class includes the control of the player with the transformation as squirrel
    /// </summary>
    public class PlayerSquirrel : AbstPlayer
    {
        /// <summary>
        /// declare squirrel texture
        /// </summary>
        public Texture localPlayerTexture;
        private static Animator animator;

        // higher is slower
        private float glidingSpeed = 3f;
        private float climbingSpeed = 3;
        private bool canClimb;
        /// <summary>
        /// transformation sound of squirrel
        /// </summary>
        public AudioClip transformSound;

        new void Start()
        {
            base.Start();
            canClimb = false;
            currentTransformationCost = 0.3f;
            playerTexture = localPlayerTexture;
            playerSpeedX = 7;
            playerSpeedY = 3;
            slowDownGoingBack = 3f;

            audio.clip = transformSound;
            audio.Play();

            animator = GetComponent<Animator>();
        }

        #region KeyControll
        /// <summary>
        /// first special ability key is pressed down
        /// </summary>
        public override void fstDown()
        {
            animator.SetBool("isWalking", false);
        }
        /// <summary>
        /// first special ability key is let go
        /// </summary>
        public override void fstUp()
        {
            if (animator.GetBool("isFlying"))
            {
                animator.SetBool("isFlying", false);
            }
        }
        /// <summary>
        /// first special ability key is continuously pressed
        /// </summary>
        public override void fstPressed()
        {
            animator.SetBool("isFlying", true);
            rigidbody.velocity = new Vector3(3, rigidbody.velocity.y / glidingSpeed, 0);
        }
        /// <summary>
        /// second special ability key is pressed down
        /// </summary>
        public override void sndDown()
        {
        }
        /// <summary>
        /// second special ability key is let go
        /// </summary>
        public override void sndUp()
        {
            if (animator.GetBool("isClimbing"))
            {
                animator.SetBool("isClimbing", false);
            }
        }
        /// <summary>
        /// second special ability key is continuously pressed
        /// </summary>
        public override void sndPressed()
        {
            if (canClimb)
            {
                animator.SetBool("isClimbing", true);
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, climbingSpeed, 0);
            }
        }
        /// <summary>
        /// moving up key is down
        /// </summary>
        public override void upDown()
        {
            if (!(animator.GetBool("isFlying")) && !(animator.GetBool("isClimbing")))
            {
                animator.SetBool("isJumping", true);
            }
        }
        /// <summary>
        /// moving up key is up
        /// </summary>
        public override void upUp()
        {
            if (animator.GetBool("isJumping"))
            {
                animator.SetBool("isJumping", false);
            }
        }
        /// <summary>
        /// moving down key is down
        /// </summary>
        public override void downDown()
        {
        }
        /// <summary>
        /// moving down key is up
        /// </summary>
        public override void downUp()
        {
        }
        /// <summary>
        /// moving down key is pressed
        /// </summary>
        public override void downPressed()
        {
        }
        /// <summary>
        /// moving left key is down
        /// </summary>
        public override void leftDown()
        {
        }
        /// <summary>
        /// moving left key is up
        /// </summary>
        public override void leftUp()
        {
            if (animator.GetBool("isWalkingBack"))
            {
                animator.SetBool("isWalkingBack", false);
            }
        }
        /// <summary>
        /// moving left key is pressed
        /// </summary>
        public override void leftPressed()
        {
            if (!(animator.GetBool("isFlying")) && !(animator.GetBool("isClimbing")) && canJump)
            {
                animator.SetBool("isWalkingBack", true);
            }
        }
        /// <summary>
        /// moving right key is down
        /// </summary>
        public override void rightDown()
        {
        }
        /// <summary>
        /// moving right key is up
        /// </summary>
        public override void rightUp()
        {
            if (animator.GetBool("isWalking"))
            {
                animator.SetBool("isWalking", false);
            }
        }
        /// <summary>
        /// moving right key is pressed
        /// </summary>
        public override void rightPressed()
        {
            if (!(animator.GetBool("isFlying")) && !(animator.GetBool("isClimbing")) && canJump)
            {
                animator.SetBool("isWalking", true);
            }
        }

        #region collision
        /// <summary>
        /// additional collisions: climbing behavior
        /// </summary>
        /// <param name="c">object with which the squirrel collides</param>
        public override void extraTriggerEnter(Collider c)
        {
            switch (c.gameObject.tag)
            {
                case "ClimbingTree":
                    canClimb = true;
                    break;
            }
        }
        /// <summary>
        /// additional collisions: climbing behavior
        /// </summary>
        /// <param name="c">object with which the squirrel collides</param>
        public override void extraTriggerExit(Collider c)
        {
            switch (c.gameObject.tag)
            {
                case "ClimbingTree":
                    animator.SetBool("isClimbing", false);
                    canClimb = false;
                    break;
            }
        }
        #endregion
        #endregion
    }
}