using UnityEngine;
using System.Collections;
namespace Assets.scripts.Player
{
    /// <summary>
    /// This class includes the control of the player with the transformation as human
    /// </summary>
    public class PlayerHuman : AbstPlayer
    {
        /// <summary>
        /// declares hit prefab
        /// </summary>
        public GameObject hitPrefab;
        /// <summary>
        /// declares empty bar texture
        /// </summary>
        public Texture sprintBarTextureEmpty;
        /// <summary>
        /// declares filled bar texture
        /// </summary>
        public Texture sprintBarTextureFill;
        /// <summary>
        /// declares jump sound
        /// </summary>
        public AudioClip jumpSound;

        private static Animator animator;
        private CapsuleCollider capsule;
        private bool isInAir;
        private float hitAllowed = 0.5f;

        // private bool isCrouching = false;
        private float playerCrouchingSpeedX = 3;
        private float originalPlayerSpeedX = 1;

        #region sprintSettings

        // init sprint variables
        private bool sprinting = false;
        private float sprintCost = 4;
        private float sprintEnergy = 0;
        private float sprintMaxEnergy = 4;
        private float sprintRecoveryEnergy = 1;

        private float sprintSpeedX = 2f;
        private float sprintSpeedY = 1.5f;

        #endregion

        #region Properties
        /// <summary>
        /// gets human player animator
        /// </summary>
        /// <returns></returns>
        public Animator getHumanPlayerAnimator()
        {
            return animator;
        }
        #endregion

        #region sprintMethods

        private void sprintStart()
        {
            if (sprintEnergy == sprintMaxEnergy)
            {
                sprinting = true;
                sprintSpeedNormalX = sprintSpeedX;
                sprintSpeedNormalY = sprintSpeedY;
                animator.speed = 2;
            }
        }

        private void sprintStop()
        {
            sprintSpeedNormalX = 1;
            sprintSpeedNormalY = 1;
            animator.speed = 1;
        }

        private void sprintIsOver()
        {
            if (! sprinting && canJump)
            {
                sprintStop();
            }
        }
        #endregion

        #region Start()
        void Start()
        {
            base.Start();
            playerTexture = (Texture)Resources.Load("Textures/PlayerHuman");
            playerSpeedX = 10;
            playerSpeedY = 7f;
            originalPlayerSpeedX = playerSpeedX;
            slowDownGoingBack = 4;

            // normal value collider.bounds.extents.y + 0.1f makes air jumping possible
            rayLength = 0.25f;

            animator = GetComponent<Animator>();
            capsule = GetComponent<CapsuleCollider>();
        }
        #endregion

        #region Update()

        #region KeyControll
        // key fst
        /// <summary>
        /// overrides first down class of abstract player
        /// </summary>
        public override void fstDown() 
        { 
        }
        /// <summary>
        /// overrides first up class of abstract player
        /// </summary>
        public override void fstUp() 
        { 
        }
        /// <summary>
        /// overrides first pressed class of abstract player
        /// </summary>
        public override void fstPressed() 
        { 
        }

        // key snd
        /// <summary>
        /// overrides second down class of abstract player
        /// </summary>
        public override void sndDown() 
        { 
        }
        /// <summary>
        /// overrides second up class of abstract player
        /// </summary>
        public override void sndUp() 
        { 
        }
        /// <summary>
        /// overrides second pressed class of abstract player
        /// </summary>
        public override void sndPressed() 
        { 
        }

        // key up
        /// <summary>
        /// overrides up key down class of abstract player
        /// </summary>
        public override void upDown() 
        { 
        }
        /// <summary>
        /// overrides up key up class of abstract player
        /// </summary>
        public override void upUp() 
        { 
        }

        // key down
        /// <summary>
        /// overrides down key down class of abstract player
        /// </summary>
        public override void downDown() 
        { 
        }
        /// <summary>
        /// overrides down key up class of abstract player
        /// </summary>
        public override void downUp()
        { 
        }
        /// <summary>
        /// overrides down key pressed class of abstract player
        /// </summary>
        public override void downPressed() 
        { 
        }

        // key left
        /// <summary>
        /// overrides left key down class of abstract player
        /// </summary>
        public override void leftDown() 
        { 
        }
        /// <summary>
        /// overrides left key up class of abstract player
        /// </summary>
        public override void leftUp() 
        { 
        }
        /// <summary>
        /// overrides left key pressed class of abstract player
        /// </summary>
        public override void leftPressed() 
        { 
        }

        // key right
        /// <summary>
        /// overrides right key down class of abstract player
        /// </summary>
        public override void rightDown() 
        { 
        }
        /// <summary>
        /// overrides right key up class of abstract player
        /// </summary>
        public override void rightUp() 
        { 
        }
        /// <summary>
        /// overrides right key pressed class of abstract player
        /// </summary>
        public override void rightPressed() 
        { 
        }

        #endregion

        private void setAnimation(string s, bool b)
        {
            animator.SetBool(s, b);
        }

        private bool setWalking()
        {
            if (Input.GetKey("left"))
            {
                setAnimation("isWalkingBack", true);
            }

            else if (Input.GetKey("right"))
            {
                setAnimation("isWalking", true);
            }

            else
            {
                return false;
            }

            return true;
        }

        void Update()
        {
            if (Time.timeScale > 0 && ! keyBoardIsDisabled)
            {
                base.Update();

                // calc sprint cost
                if (sprinting)
                {
                    sprintEnergy -= Time.deltaTime * sprintCost;
                }

                if (sprintEnergy <= 0)
                {
                    sprintEnergy = 0;
                    sprinting = false;
                }

                if (sprintEnergy < 4 && !sprinting)
                {
                    sprintEnergy += Time.deltaTime * sprintRecoveryEnergy;
                }

                if (sprintEnergy > 4)
                {
                    sprintEnergy = sprintMaxEnergy;
                    sprinting = false;
                }

                // check if he can stand up if he crouches
                if (!checkAbove() && animator.GetBool("isCrouching"))
                {
                    animator.SetBool("isCrouching", false);
                }

                if (AbstPlayer.poisoned)
                {
                    sprintEnergy = 0;
                }

                // play sound
                if (Input.GetKeyDown("up") && !animator.GetBool("isJumping"))
                {
                    audio.clip = jumpSound;
                    audio.Play();
                }

                if (hitAllowed >= 0)
                {
                    hitAllowed -= Time.deltaTime;
                }

                if (Input.GetKeyUp("left"))
                {
                    setAnimation("isWalkingBack", false);
                }

                if (Input.GetKeyUp("right"))
                {
                    setAnimation("isWalking", false);
                }

                if (Input.GetKeyUp("down"))
                {
                    bool somethingIsAbove = false;
                    somethingIsAbove = checkAbove();

                    if (!somethingIsAbove)
                    {
                        setAnimation("isCrouching", false);
                    }
                }

                if (Input.GetKeyUp("e"))
                {
                    setAnimation("isHitting", false);
                }

                //-----------------------
                if (canJump && (Input.GetKey("down") || Input.GetKeyDown("down")))
                {
                    capsule.direction = 0;
                    capsule.center = new Vector3(0, 2, 0);
                    setAnimation("isJumping", false);
                    setAnimation("isCrouching", true);
                    playerSpeedX = playerCrouchingSpeedX;
                    setWalking();
                }

                else
                {
                    capsule.direction = 1;
                    capsule.center = new Vector3(0, 5, 0);
                    playerSpeedX = originalPlayerSpeedX;
                    if (!canJump)
                    {
                        setAnimation("isJumping", true);
                        setAnimation("isWalkingBack", false);
                        setAnimation("isWalking", false);
                    }

                    else
                    {
                        setAnimation("isJumping", false);
                        if (setWalking())
                        {
                            if (Input.GetKeyDown("q") && (!AbstPlayer.poisoned))
                            {
                                sprintStart();
                            }
                        }
                    }

                    if (Input.GetKeyDown("e"))
                    {
                        if (hitAllowed <= 0)
                        {
                            hitAllowed = 0.5f;
                            setAnimation("isHitting", true);
                            StartCoroutine("hitDelay");
                        }
                    }
                }

                sprintIsOver();
            }
        }

        private bool checkAbove()
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.up, 5f);
            int counter = 0;
            while (counter < hits.Length)
            {
                RaycastHit hit = hits[counter];
                if (hit.collider.gameObject.tag != "Player")
                {
                    return true;
                }

                counter++;
            }

            return false;
        }

        void beforeTransformationChange()
        {
            sprintStop();
        }

        #endregion

        void OnGUI()
        {
            base.OnGUI();
            int barLength = 133;
            // runnup bar
            GUI.DrawTexture(new Rect(20, 100, barLength, 10), sprintBarTextureEmpty, ScaleMode.StretchToFill, true);

            GUI.DrawTexture(new Rect(20, 100, sprintEnergy / sprintMaxEnergy * barLength, 10), sprintBarTextureFill, ScaleMode.StretchToFill, true);

            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = 10;
            myStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(20, 100, 1, 20), "Runnup", myStyle);

            // string st = "sprintTime " + sprintTime;
            // string p = "sprintrecovery " + sprintRecoveryTime;
            // GUI.Label(new Rect(0, 4 * (Screen.height / 20), Screen.width, 2 * Screen.height / 10), p);
            // GUI.Label(new Rect(0, 5 * (Screen.height / 20), Screen.width, 2 * Screen.height / 10), st);
        }

        IEnumerator hitDelay()
        {
            yield return new WaitForSeconds(0.3f);
            Vector3 positions = new Vector3(transform.position.x + 2f, transform.position.y + 1.5f, transform.position.z);
            Instantiate(hitPrefab, positions, Quaternion.identity);
        }
    }
}