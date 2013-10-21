using UnityEngine;
using System.Collections;

namespace Assets.scripts.Player
{
    /// <summary>
    /// This class includes the control of the player with the transformation as dragon
    /// </summary>
    public class PlayerDragon : AbstPlayer
    {
        private static Animator animator;

        /// <summary>
        /// texture of the dragon
        /// </summary>
        public Texture localPlayerTexture;
        /// <summary>
        /// fire breath effect prefab
        /// </summary>
        public GameObject DragonFire;
        /// <summary>
        /// fire breath graphics effect
        /// </summary>
        public GameObject DragonFireLight;
        /// <summary>
        /// sound of the fire breath
        /// </summary>
        public AudioClip fireSound;
        /// <summary>
        /// sound of the dragon transformation
        /// </summary>
        public AudioClip transformSound;

        new void Start()
        {
            base.Start();
            currentTransformationCost = 1;
            playerTexture = localPlayerTexture;
            playerSpeedX = 5;
            playerSpeedY = 6;
            slowDownGoingBack = 3f;

            // start transform sound
            audio.clip = transformSound;
            audio.Play();

            animator = GetComponent<Animator>();
        }

        #region KeyControll
        /// <summary>
        /// "first ability" - which action should the boar execute on that key
        /// </summary>
        public override void fstDown()
        {
        }
        /// <summary>
        /// "first ability" - which action should the boar execute on that key
        /// </summary>
        public override void fstUp()
        {
            animator.SetBool("isBombing", false);
        }
        /// <summary>
        /// "first ability" - which action should the boar execute on that key
        /// </summary>
        public override void fstPressed()
        {
            if (!canJump)
            {
                animator.SetBool("isBombing", true);
            }
        }

        /// <summary>
        /// "second ability" - which action should the boar execute on that key
        /// </summary>
        public override void sndDown()
        {
        }
        /// <summary>
        /// "second ability" - which action should the boar execute on that key
        /// </summary>
        public override void sndUp()
        {
            animator.SetBool("isAttacking", false);
            audio.Stop();
        }
        /// <summary>
        /// "second ability" - which action should the boar execute on that key
        /// </summary>
        public override void sndPressed()
        {
            animator.SetBool("isAttacking", true);
            Vector3 position = new Vector3(transform.position.x + 3.5f, transform.position.y + 1.3f, transform.position.z);
            Instantiate(DragonFire, position, Quaternion.Euler(15f, 90f, 0f));

            Instantiate(DragonFireLight, position, Quaternion.AngleAxis(90f, Vector3.up));

            // start sound for fire
            if (audio.isPlaying && (audio.clip == transformSound))
            {
                audio.Stop();
            }

            if (!audio.isPlaying)
            {
                audio.clip = fireSound;
                audio.Play();
            }
        }

        /// <summary>
        /// "key up" - which action should the boar execute on that key
        /// </summary>
        public override void upDown()
        {
            animator.SetBool("isJumping", true);
        }
        /// <summary>
        /// "key up" - which action should the boar execute on that key
        /// </summary>
        public override void upUp()
        {
            animator.SetBool("isJumping", false);
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
            animator.SetBool("isWalking", false);
        }
        /// <summary>
        /// "key left" - which action should the boar execute on that key
        /// </summary>
        public override void leftPressed()
        {
            animator.SetBool("isWalking", true);
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
            animator.SetBool("isWalking", false);
        }
        /// <summary>
        /// "key right" - which action should the boar execute on that key
        /// </summary>
        public override void rightPressed()
        {
            animator.SetBool("isWalking", true);
        }
        #endregion

        void OnCollisionEnter(Collision collider)
        {
            switch (collider.gameObject.tag)
            {
                case "KillingPlane":
                    gettingInjured("KillingPlane");
                    respawnPlayer();
                    break;
            }
        }
    }
}
