using UnityEngine;
using System.Collections;
namespace Assets.scripts.Player
{
    /// <summary>
    /// This class contains anything that relates to the player transformation boar. Pushing / destroying rocks, animations
    /// </summary>
    public class PlayerBoar : AbstPlayer
    {
        private static Animator animator;
        private static GameObject rockCloud;

        /// <summary>
        /// Audio clip that is played when the player transforms into the boar.
        /// </summary>
        public AudioClip transformSound;
        /// <summary>
        /// audio clip plays, when rock is destroyed
        /// </summary>
        public AudioClip rockBang;

        new void Start()
        {
            base.Start();
            currentTransformationCost = 0.5f;
            playerTexture = (Texture)Resources.Load("Textures/Boar");
            playerSpeedX = 8;
            playerSpeedY = 5;
            slowDownGoingBack = 3f;

            audio.clip = transformSound;
            audio.Play();

            animator = GetComponent<Animator>();
            rockCloud = (GameObject)Resources.Load("Prefabs/LevelObjects/RockCloud");
        }

        #region Update()

        new void FixedUpdate()
        {
            if (Time.timeScale > 0)
            {
                base.FixedUpdate();
                if (boarPush)
                {
                   // rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                    boarCollisionObject.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 800f, ForceMode.Impulse);
                    boarPush = false;
                }
            }
        }

        #region KeyControll
        /// <summary>
        /// "1st ability" - which action should the boar execute on that key
        /// </summary>
        public override void fstDown()
        {
        }
        /// <summary>
        /// "1st ability" - which action should the boar execute on that key
        /// </summary>
        public override void fstUp()
        {
            animator.SetBool("isPushing", false);
        }
        /// <summary>
        /// "1st ability" - which action should the boar execute on that key
        /// </summary>
        public override void fstPressed()
        {
            if (!(animator.GetBool("isJumping")))
            {
                animator.SetBool("isPushing", true);
            }
        }

        /// <summary>
        /// "2. ability" - which action should the boar execute on that key
        /// </summary>
        public override void sndDown()
        {
        }
        /// <summary>
        /// "2. ability" - which action should the boar execute on that key
        /// </summary>
        public override void sndUp()
        {
            animator.SetBool("isTakkeling", false);
        }
        /// <summary>
        /// "2. ability" - which action should the boar execute on that key
        /// </summary>
        public override void sndPressed()
        {
            if (!(animator.GetBool("isJumping")))
            {
                animator.SetBool("isTakkeling", true);
            }
        }

        /// <summary>
        /// "key up" - which action should the boar execute on that key
        /// </summary>
        public override void upDown()
        {
        }
        /// <summary>
        /// "key up" - which action should the boar execute on that key
        /// </summary>
        public override void upUp()
        {
            if (animator.GetBool("isJumping"))
            {
                animator.SetBool("isJumping", false);
            }
        }

        /// <summary>
        /// "key down" - which action should the boar execute on that key
        /// </summary>
        public override void downDown()
        {
        }
        /// <summary>
        /// "key down" - which action should the boar execute on that key
        /// </summary>
        public override void downUp()
        {
        }
        /// <summary>
        /// "key down" - which action should the boar execute on that key
        /// </summary>
        public override void downPressed()
        {
        }

        /// <summary>
        /// "key left" - which action should the boar execute on that key
        /// </summary>
        public override void leftDown()
        {
        }
        /// <summary>
        /// "key left" - which action should the boar execute on that key
        /// </summary>
        public override void leftUp()
        {
            if (animator.GetBool("isWalkingBack"))
            {
                animator.SetBool("isWalkingBack", false);
            }
        }
        /// <summary>
        /// "key left" - which action should the boar execute on that key
        /// </summary>
        public override void leftPressed()
        {
            if (!(animator.GetBool("isJumping")) && !(animator.GetBool("isTakkeling")))
            {
                animator.SetBool("isWalkingBack", true);
            }
        }

        /// <summary>
        /// "key right" - which action should the boar execute on that key
        /// </summary>
        public override void rightDown()
        {
        }
        /// <summary>
        /// "key right" - which action should the boar execute on that key
        /// </summary>
        public override void rightUp()
        {
            if (animator.GetBool("isWalking"))
            {
                animator.SetBool("isWalking", false);
            }
        }
        /// <summary>
        /// "key right" - which action should the boar execute on that key
        /// </summary>
        public override void rightPressed()
        {
            if (!(animator.GetBool("isJumping")) && !(animator.GetBool("isTakkeling")))
            {
                animator.SetBool("isWalking", true);
            }
        }
        #endregion

        private bool boarPush = false;
        private Collision boarCollisionObject;
        #region collision
        /// <summary>
        /// additional collisions: pushing / tackling behavior
        /// </summary>
        /// <param name="c">object with which the boar collides</param>
        public override void extraCollisionEnter(Collision c)
        {
            switch (c.gameObject.tag)
            {
                case "LavaRock":
                case "Rock":
                    if (animator.GetBool("isTakkeling"))
                    {
                        rockDest(c.gameObject);
                        return;
                    }

                    if (animator.GetBool("isPushing"))
                    {
                        boarPush = true;
                        boarCollisionObject = c;
                        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    }

                    break;
            }
        }
        /// <summary>
        /// additional collisions: pushing / tackling behavior
        /// </summary>
        /// <param name="c">object with which the boar collides</param>
        public override void extraCollisionStay(Collision c)
        {
            switch (c.gameObject.tag)
            {
                case "Rock":
                    if (animator.GetBool("isTakkeling"))
                    {
                        rockDest(c.gameObject);
                    }

                    if (animator.GetBool("isPushing"))
                    {
                        boarPush = true;
                        boarCollisionObject = c;
                      // rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                    }

                    break;
            }
        }
        /// <summary>
        /// provides extra on collision exit class
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public override void extraCollisionExit(Collision c)
        {
            if ((c.gameObject.tag == "Rock")||(c.gameObject.tag =="LavaRock"))
            {
                rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
        }

        private void rockDest(GameObject c)
        {
            Destroy(c.gameObject);
            audio.clip = rockBang;
            audio.Play();
            Instantiate(rockCloud, c.gameObject.transform.position, Quaternion.identity);
        }
        #endregion
        #endregion
    }
}
