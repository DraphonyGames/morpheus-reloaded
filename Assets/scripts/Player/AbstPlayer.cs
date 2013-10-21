using UnityEngine;
using System.Collections;
using Assets.scripts.InventoryScripts;
namespace Assets.scripts.Player
{
    /// <summary>
    /// Abstract Class where general settings are set( main Prefabs)
    /// This class also contains moving, jumping, transformations and collisions with objects
    /// </summary>
    public abstract class AbstPlayer : MonoBehaviour
    {
        #region misc
        /// <summary>
        /// declares player prefab
        /// </summary>
        public static GameObject playerPrefab;
        /// <summary>
        /// declares dragon prefab
        /// </summary>
        public static GameObject dragonPrefab;
        /// <summary>
        /// declares boar prefab
        /// </summary>
        public static GameObject boarPrefab;
        /// <summary>
        /// declares squirrel prefab
        /// </summary>
        public static GameObject squirrelPrefab;
        /// <summary>
        /// declares iceDragon prefab
        /// </summary>
        public static GameObject iceDragon;
        /// <summary>
        /// declares hit prefab
        /// </summary>
        public static GameObject HitPrefab;
        /// <summary>
        /// declares iceBlock prefab
        /// </summary>
        public static GameObject iceBlock;
        /// <summary>
        /// declares cloud prefab
        /// </summary>
        public static GameObject cloudPrefab;
        /// <summary>
        /// declares lava prefab
        /// </summary>
        public static GameObject lava;
        /// <summary>
        /// declares lava2 prefab
        /// </summary>
        public static GameObject lava2;
        /// <summary>
        /// declares in fight zone prefab
        /// </summary>
        public static GameObject infightZone;
        /// <summary>
        /// declares player heart texture
        /// </summary>
        public static Texture playerHeart;
        /// <summary>
        /// declares player heart 2/3 texture
        /// </summary>
        public static Texture playerHeart_2_3;
        /// <summary>
        /// declares player heart 1/3 texture
        /// </summary>
        public static Texture playerHeart_1_3;
        /// <summary>
        /// declares player heart empty texture
        /// </summary>
        public static Texture playerHeartEmpty;
        /// <summary>
        /// declares player texture
        /// </summary>
        public static Texture playerTexture;
        /// <summary>
        /// declares lava texture
        /// </summary>
        public static Texture lavaTexture;
        /// <summary>
        /// declares current transform
        /// </summary>
        public static Transform trans;
        /// <summary>
        /// declares keyBoardIsDisabled
        /// </summary>
        public bool keyBoardIsDisabled = false;
        /// <summary>
        /// declares the player Animator
        /// </summary>
        private Animator playerAnimator;
        /// <summary>
        /// declares the iceBlockWasSpawned
        /// </summary>
        private bool iceBlockWasSpawned = false;

        private Texture guiBackGround;
        private Texture energyBarTextureEmpty;
        private Texture energyBarTextureFill;

        ////private Texture lavaBarTextureEmpty;
        ////private Texture lavaBarTextureFill;
        private Texture greyFrame;
        private Texture redFrame;
        private Texture greenFrame;
        private Texture deathScreen;

        #endregion
        #region settings
        float gravity = -10f;

        /// <summary>
        /// indicates the transformation
        /// 0 = crouch
        /// 1 = human
        /// 2 = dragon
        /// 3 = squirrel
        /// 4 = boar
        /// </summary>
        public static int currentTransformation = 1;

        // Transformation cost
        /// <summary>
        /// declares amount of decreasing energy
        /// </summary>
        public static float currentTransformationCost = 0.5f;
        private static float maxEnergy = 5;
        private static float energy = 5;
        private static float transformCost = 0;
        private static float energyRecover = 1;
        /// <summary>
        /// is changed when the Player gets poisoned
        /// </summary>
        public static bool poisoned = false;
        /// <summary>
        /// time the Player is poisoned.
        /// </summary>
        private static float poisonTimer = 0;

        // canTakeDamage answers following question: can the player take damage at the moment?
        private bool canTakeDamage = true;
        // time after each hit for the player to react before he is taking damage again
        private float invulnerabilityTime = 0.5f;
        // blinking time after taking a hit (blinks 5 times), should be 5 * invulnerabilityTime so graphics and effect are the same
        private float damageEffectPause = 0.1f;
        // is used to get the renderer of the actual transformation for blinking effect
        private Component playerRendererComponent;

        /// <summary>
        /// inventory access
        /// </summary>
        public static Inventory inventory;
        /// <summary>
        /// game variables access
        /// </summary>
        public static GameVariables gameVariables;
        /// <summary>
        /// an enemy is near the player
        /// </summary>
        public static bool isInfight = false;

        // variables for jump calculations

        /// <summary>
        /// declares the maximum of velocity
        /// </summary>
        public float maxVelocityChange = 10.0f;
        private float jumpHeight = 9.0f;
        private float gravityFac = 3f;
        /// <summary>
        /// length of the ray which checks for isGrounded property
        /// </summary>
        public static float rayLength;
        /// <summary>
        /// adjust center of the capsule (blender problems)
        /// </summary>
        public static Vector3 adjustVector;
        Vector3 rayLeftCenter;
        Vector3 rayRightCenter;

        /// <summary>
        /// if the player is grounded
        /// </summary>
        public static bool canJump = false;

        private static bool jumpNow = false;
        private static System.TimeSpan jumpCoolDown = new System.TimeSpan(0, 0, 0, 0, 250);
        private static System.DateTime jumpLastTime = System.DateTime.Now - jumpCoolDown;

        // autoRecoveryPlayer
        /// <summary>
        /// saves the time when player were hit the last time
        /// </summary>
        public static System.DateTime lastHitTime = System.DateTime.Now;
        /// <summary>
        /// timespan till the next hit is possible
        /// </summary>
        public static System.TimeSpan coolDownEnemyHit = new System.TimeSpan(0, 0, 0, 5, 0);

        // deathScreen
        bool isDeathScreen;
        private static System.TimeSpan coolDownDeathScreen = new System.TimeSpan(0, 0, 0, 1, 0);
        private System.DateTime deathScreenstart;

        // following variable will be set in each player
        /// <summary>
        /// player speed on x axis
        /// </summary>
        public static float playerSpeedX;
        /// <summary>
        /// player speed n y axis
        /// </summary>
        public static float playerSpeedY;
        /// <summary>
        /// slower speed while moving backward
        /// </summary>
        public static float slowDownGoingBack;

        // energy bar settings
        private System.TimeSpan transformationTime = new System.TimeSpan(0, 0, 0, 10, 0);
        private System.TimeSpan energyRecoveryTime = new System.TimeSpan(0, 0, 0, 5, 0);
        private System.DateTime transLastTime = System.DateTime.Now - new System.TimeSpan(0, 0, 0, 5, 0);

        private static float directionByHorizontal = 0;
        /// <summary>
        /// left  = -1
        /// right = 1
        /// </summary>
        public static int direction = RIGHT;
        private const int RIGHT = 1;
        private const int LEFT = -1;

        private bool onCollision = false;

        #endregion

        #region transformation specific code
        #region human

        // for sprint - don't change sprintSpeedNormal
        /// <summary>
        /// sprint speed on x axis
        /// </summary>
        public static float sprintSpeedNormalX = 1;
        /// <summary>
        /// sprint speed on y axis
        /// </summary>
        public static float sprintSpeedNormalY = 1;

        #endregion
        #endregion

        #region Start()
        /// <summary>
        /// provides start behavior for player
        /// </summary>
        public virtual void Start()
        {
            rigidbody.freezeRotation = true;
            rigidbody.useGravity = false;
            adjustVector = new Vector3(0, 0.2f, 0);
            // change the base for is-the-player-on-the-ground calculations (rayLength and adjustVector) 
            // according to current transformation
            switch (currentTransformation)
            {
                case 2:
                    adjustVector = new Vector3(0, 0.2f, 0);
                    rayLength = collider.bounds.extents.y + 0.3f;
                    break;
                case 3:
                    rayLength = collider.bounds.extents.x + 0.3f;
                    break;
                case 4:
                    adjustVector = new Vector3(-1.5f, -1.5f, 3f);
                    rayLength = collider.bounds.extents.y + 0.1f;
                    break;
                default:
                    rayLength = collider.bounds.extents.y + 0.1f;
                    break;
            }

            rayLeftCenter = new Vector3(-collider.bounds.extents.x + 0.1f, 0, 0);
            rayRightCenter = new Vector3(collider.bounds.extents.x - 0.1f, 0, 0);

            // load static Prefabs
            playerPrefab = (GameObject)Resources.Load("Prefabs/Player/PlayerHuman");
            boarPrefab = (GameObject)Resources.Load("Prefabs/Player/PlayerBoar");
            dragonPrefab = (GameObject)Resources.Load("Prefabs/Player/PlayerDragon");
            squirrelPrefab = (GameObject)Resources.Load("Prefabs/Player/PlayerSquirrel");
            cloudPrefab = (GameObject)Resources.Load("Prefabs/Player/TransformCloud");
            iceBlock = (GameObject)Resources.Load("Prefabs/LevelObjects/Outro/IceBlock");

            infightZone = (GameObject)Resources.Load("Prefabs/Player/InfightZone");

            inventory = InventoryManager.inventory;
            gameVariables = GameController.gameVariables;

            // load static Textures
            playerHeart = (Texture)Resources.Load("Textures/Heart");
            playerHeart_2_3 = (Texture)Resources.Load("Textures/Heart_2-3");
            playerHeart_1_3 = (Texture)Resources.Load("Textures/Heart_1-3");
            playerHeartEmpty = (Texture)Resources.Load("Textures/HeartEmpty");
            guiBackGround = (Texture)Resources.Load("Textures/GUI/GuiBackground");

            greyFrame = (Texture)Resources.Load("Textures/RegenerateHP/GreyFrame");
            redFrame = (Texture)Resources.Load("Textures/RegenerateHP/RedFrame");
            greenFrame = (Texture)Resources.Load("Textures/RegenerateHP/GreenFrame");
            deathScreen = (Texture)Resources.Load("Textures/RegenerateHP/DeathScreen");

            energyBarTextureEmpty = (Texture)Resources.Load("Bars/EnergyBarTextureEmpty");
            energyBarTextureFill = (Texture)Resources.Load("Bars/EnergyBarTextureFill");

            ////lavaBarTextureEmpty = (Texture)Resources.Load("Bars/EnergyBarTextureEmpty");
            ////lavaBarTextureFill = (Texture)Resources.Load("Bars/LavaBarTextureFill");

            lavaTexture = (Texture)Resources.Load("Textures/GUI/LavaIcon");

            // find other gameobjects
            lava = GameObject.Find("Lava");
            // find second Lava if the first Level is loaded
            if (Application.loadedLevelName == "Part1_Level1")
            {
                lava2 = GameObject.Find("Lava2");
            }

            trans = transform;

            if (gameVariables.hearts <= 0)
            {
                Application.LoadLevel("GameOverMenu");
            }
            else
            {
                if (gameVariables.initMisc)
                {
                    gameVariables.initMisc = false;
                    gameVariables.savePoint(trans.position, lava.transform.position, energy);
                }

                if (gameVariables.wasRespawned)
                {
                    // reset Level
                    gameVariables.wasRespawned = false;
                    trans.position = gameVariables.lastRespawn;
                    lava.transform.position = gameVariables.lastLavaPos;
                    if (Application.loadedLevelName == "Part1_Level1")
                    {
                        lava2.transform.position = gameVariables.lastLavaPos2;
                    }

                    energy = gameVariables.lastEnergy;
                    gameVariables.respawnVariables();
                }
            }
            // reset Transformation Cost
            currentTransformationCost = 1;
        }
        #endregion

        #region Update()
        /// <summary>
        /// fixed update behavior for player
        /// </summary>
        public virtual void FixedUpdate()
        {
            // check if the game is paused
            if (Time.timeScale > 0 && !keyBoardIsDisabled)
            {
                playerMovement();
            }
            else
            {
                playerMovementWithoutKeyinput();
            }
        }

        /// <summary>
        /// Emulates part of physics behavior for our player: moving left, right and jumping.
        /// </summary>
        void playerMovement()
        {
            if (onCollision && !canJump)
            {
                directionByHorizontal = 0;
            }

            else
            {
                directionByHorizontal = -playerSpeedX * Input.GetAxis("Horizontal") * sprintSpeedNormalX;
            }
           
            if (directionByHorizontal > 0)
            {
                direction = LEFT;
                directionByHorizontal = directionByHorizontal / slowDownGoingBack;
            }
            else if (directionByHorizontal < 0)
            {
                direction = RIGHT;
            }
            // calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(directionByHorizontal, 0, 0);
            targetVelocity = trans.TransformDirection(targetVelocity);

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            // actuallyJumping
            if (jumpNow)
            {
                jumpNow = false;
                jumpLastTime = System.DateTime.Now;
                ////debugJump();
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed() * playerSpeedY * sprintSpeedNormalY / 6f, 0);
            }
            // apply gravity manually for more tuning control
            rigidbody.AddForce(new Vector3(0, gravity * rigidbody.mass * gravityFac, 0));
        }
        /// <summary>
        /// Emulates part of physics behavior for our player without keyboard input
        /// </summary>
        void playerMovementWithoutKeyinput()
        {
            // calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(directionByHorizontal, 0, 0);
            targetVelocity = trans.TransformDirection(targetVelocity);

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            // actuallyJumping
            if (jumpNow)
            {
                jumpNow = false;
                jumpLastTime = System.DateTime.Now;
                ////debugJump();
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed() * playerSpeedY * sprintSpeedNormalY / 6f, 0);
            }
            // apply gravity manually for more tuning control
            rigidbody.AddForce(new Vector3(0, gravity * rigidbody.mass * gravityFac, 0));

            // set position
            gameVariables.playerPosition = trans.position;
        }
        /// <summary>
        /// from the jump height and gravity we deduce the upwards speed for the character to reach at the apex of the jump curve
        /// </summary>
        /// <returns>float: velocity.y of the rigid body movement when jumping</returns>
        float CalculateJumpVerticalSpeed()
        {
            return Mathf.Sqrt(2 * jumpHeight * -gravity);
        }

        /// <summary>
        /// gives a lot of variables to see what goes wrong with jumping if anything does
        /// </summary>
        public void debugJump()
        {
            float distanceToGround = 100f;
            Debug.Log("canJump: " + canJump);
            RaycastHit hit;
            if (Physics.Raycast(transform.position + adjustVector, Vector3.down, out hit))
            {
                distanceToGround = hit.distance;
            }

            Debug.DrawLine(transform.position + adjustVector, 10 * Vector3.down);
            Debug.Log("distanceToGround: " + distanceToGround + "mit aktueller Transformation" + currentTransformation);
            Debug.Log("rayLength: " + rayLength);
            Debug.Log("transform.position: " + transform.position);
            Debug.Log("adjustVector: " + adjustVector);
        }

        /// <summary>
        /// updates behavior of player
        /// </summary>
        public virtual void Update()
        {
            // check if the game is paused, if so deactivate player controls
            if (Time.timeScale > 0 && !keyBoardIsDisabled)
            {
                // player is grounded if one of the 3 downward rays (left end, middle and right end of the model) hits the ground
                canJump = Physics.Raycast(transform.position + adjustVector + rayLeftCenter, -Vector3.up, rayLength) ||
                  Physics.Raycast(transform.position + adjustVector, -Vector3.up, rayLength) ||
                  Physics.Raycast(transform.position + adjustVector + rayRightCenter, -Vector3.up, rayLength);

                if (poisoned)
                {
                    poisonTimer -= Time.deltaTime;
                }

                if (poisonTimer <= 0)
                {
                    poisoned = false;
                }

                playerAutoRecovery();
                // transformation
                calcTransformStayCost();
                for (int i = 1; i <= 4; i++)
                {
                    if (Input.GetKeyDown("" + i))
                    {
                        transformTo(i);
                        break;
                    }
                }

                if (jumpLastTime + jumpCoolDown < System.DateTime.Now && canJump && (Input.GetKeyDown("w") || Input.GetKeyDown("up")))
                {
                    jumpNow = true;
                }

                // get keyinput
                // key fst
                string fst = "q";
                if (Input.GetKeyDown(fst))
                {
                    fstDown();
                }
                else if (Input.GetKeyUp(fst))
                {
                    fstUp();
                }

                if (Input.GetKey(fst))
                {
                    fstPressed();
                }

                // key snd
                string snd = "e";
                if (Input.GetKeyDown(snd))
                {
                    sndDown();
                }
                else if (Input.GetKeyUp(snd))
                {
                    sndUp();
                }

                if (Input.GetKey(snd))
                {
                    sndPressed();
                }

                // key up
                string up1 = "up";
                string up2 = "w";
                if (Input.GetKeyDown(up1) || Input.GetKeyDown(up2))
                {
                    upDown();
                }
                else if (Input.GetKeyUp(up1) || Input.GetKeyUp(up2))
                {
                    upUp();
                }

                if (Input.GetKey(up1) || Input.GetKey(up2))
                {
                    upPressed();
                }

                // key down
                string down1 = "down";
                string down2 = "s";
                if (Input.GetKeyDown(down1) || Input.GetKeyDown(down2))
                {
                    downDown();
                }
                else if (Input.GetKeyUp(down1) || Input.GetKeyUp(down2))
                {
                    downUp();
                }

                if (Input.GetKey(down1) || Input.GetKey(down2))
                {
                    downPressed();
                }

                // key right
                string right1 = "d";
                string right2 = "right";
                if (Input.GetKeyDown(right1) || Input.GetKeyDown(right2))
                {
                    rightDown();
                }
                else if (Input.GetKeyUp(right1) || Input.GetKeyUp(right2))
                {
                    rightUp();
                }

                if (Input.GetKey(right1) || Input.GetKey(right2))
                {
                    rightPressed();
                }

                // key left
                string left1 = "a";
                string left2 = "left";
                if (Input.GetKeyUp(left1) || Input.GetKeyUp(left2))
                {
                    leftUp();
                }

                else if (Input.GetKeyDown(left1) || Input.GetKeyDown(left2))
                {
                    leftDown();
                }

                if (Input.GetKey(left1) || Input.GetKey(left2))
                {
                    leftPressed();
                }

                // spawn infightZone
                Instantiate(infightZone, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                // set position
                gameVariables.playerPosition = trans.position;
            }
        }

        #region KeyControll
        // key fst
        /// <summary>
        /// first special ability key is down
        /// </summary>
        public abstract void fstDown();
        /// <summary>
        /// first special ability key is up
        /// </summary>
        public abstract void fstUp();
        /// <summary>
        /// first special ability key is pressed
        /// </summary>
        public abstract void fstPressed();

        // key snd
        /// <summary>
        /// second special ability key is down
        /// </summary>
        public abstract void sndDown();
        /// <summary>
        /// second special ability key is up
        /// </summary>
        public abstract void sndUp();
        /// <summary>
        /// second special ability key is pressed
        /// </summary>
        public abstract void sndPressed();

        // key up
        /// <summary>
        /// moving up key is down
        /// </summary>
        public abstract void upDown();
        /// <summary>
        /// moving up key is up
        /// </summary>
        public abstract void upUp();
        /// <summary>
        /// moving up key is pressed
        /// </summary>
        public virtual void upPressed() 
        { 
        }

        // key down
        /// <summary>
        /// moving down key is down
        /// </summary>
        public abstract void downDown();
        /// <summary>
        /// moving down key is up
        /// </summary>
        public abstract void downUp();
        /// <summary>
        /// moving down key is pressed
        /// </summary>
        public abstract void downPressed();

        // key left
        /// <summary>
        /// moving left key is down
        /// </summary>
        public abstract void leftDown();
        /// <summary>
        /// moving left key is up
        /// </summary>
        public abstract void leftUp();
        /// <summary>
        /// moving left key is pressed
        /// </summary>
        public abstract void leftPressed();

        // key right
        /// <summary>
        /// moving right key is down
        /// </summary>
        public abstract void rightDown();
        /// <summary>
        /// moving right key is up
        /// </summary>
        public abstract void rightUp();
        /// <summary>
        /// moving right key is pressed
        /// </summary>
        public abstract void rightPressed();
        #endregion

        /// <summary>
        /// this will called before the player transform
        /// </summary>
        public virtual void beforeTransformationChange() 
        { 
        }

        #endregion

        #region collision

        #region OnParticle
        /// <summary>
        /// get the collision with the particle systems
        /// </summary>
        /// <param name="g"></param>
        void OnParticleCollision(GameObject g)
        {
            extraParticleCollision(g);
            switch (g.tag)
            {
                case "Lava":
                    gettingInjured("Lava");
                    break;
                case "RockFire":
                    gettingInjured("HitEnemy");
                    break;
                case "HitEnemy":
                    gettingInjured("HitEnemy");
                    break;
                // ToDo game end Screen
                case "LavaFountain":
                    gettingInjured("HitEnemy");
                    break;
                case "IceFire":
                    if (!iceBlockWasSpawned)
                    {
                        spawnIceBlock();
                    }

                    break;
            }
        }

        /// <summary>
        /// spawn the IceBlock if the player gets hit by the iceFire and destroys the player
        /// </summary>
        private void spawnIceBlock()
        {
            iceBlockWasSpawned = true;
            // spawn iceblock on the player position if player gets hitten by iceFire
            GameObject iceBlocktmp = (GameObject)Instantiate(iceBlock, new Vector3(trans.position.x, trans.position.y + 1.5f, trans.position.z), Quaternion.identity);
            iceBlocktmp.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            // destroy player
            Destroy(this.gameObject);
        }

        /// <summary>
        /// creates the cut scene where the player gets frozen and carried away 
        /// by the ice dragon with an screen "to be continued ..." faded in
        /// </summary>
        public void startCutscene()
        {
            // freeze input
            keyBoardIsDisabled = true;

            // if player is transformed and not the human player, transform back to the human player
            if (currentTransformation != 1)
            {
                currentTransformation = 1;
            }

            // load the player animator
            playerAnimator = playerPrefab.GetComponent<PlayerHuman>().getHumanPlayerAnimator();

            /*
            // disable all playe animation
             playerAnimator.SetBool("walkingAnimation", false);
             playerAnimator.SetBool("standCrouching", false);
             playerAnimator.SetBool("jumpAnimation", false);
             playerAnimator.SetBool("hitAnimation", false);
             playerAnimator.SetBool("walkingBackAnimation", false);
             playerAnimator.SetBool("crouchBackAnimation", false);
             playerAnimator.SetBool("crouchingAnimation", false);
      
            // set Walking Animation
            playerAnimator.SetBool("walkingAnimation", true);
            */

            // move player to icedragon
            transform.Translate(0f, playerSpeedY * Time.deltaTime, 0f, Space.World);

            // move player to the ground 
            if (!canJump)
            {
                transform.Translate(playerSpeedX * Time.deltaTime, 0f, 0f, Space.World);
            }

            // transform.Translate(transform.position.x + 10, transform.position.y - 5, transform.position.z);
            // get iceDragon Object
            iceDragon = GameObject.Find("IceDragon");

            // stop the fire of the ice dragon
            // gameVariables.playerPosition = new Vector3(trans.position.x - 10, trans.position.y, trans.position.z);
        }

        /// <summary>
        /// provides extra particle collision class for player
        /// </summary>
        /// <param name="g"> particle which collides </param>
        public void extraParticleCollision(GameObject g) 
        { 
        }
        #endregion

        #region OnCollision

        void OnCollisionEnter(Collision c)
        {
            extraCollisionEnter(c);
            switch (c.gameObject.tag)
            {
                case "KillingPlane":
                    gettingInjured("KillingPlane");
                    respawnPlayer();
                    break;
                case "HitEnemy":
                    // trans.position = (new Vector3(trans.position.x - 2, trans.position.y, trans.position.z));
                    // rigidbody.AddForce( -300, 300, 0, ForceMode.Acceleration);
                    // rigidbody.velocity = new Vector3(-5, 5, 0);
                    gettingInjured("HitEnemy");
                    break;
                case "Poison":
                    gettingInjured("Poison");
                    break;
                case "Tiger":
                    gettingInjured("Tiger");
                    break;
                case "Enemy":
                    gettingInjured("HitEnemy");
                    break;
                case "Environment":
                    onCollision = true;
                    break;
                case "CutsceneTrigger":
                    startCutscene();
                    Destroy(c.gameObject);
                    break;
            }
        }

        /// <summary>
        /// while in collision state
        /// </summary>
        /// <param name="c"> object to collide with </param>
        void OnCollisionStay(Collision c)
        {
            if (c.gameObject.tag == "HitEnemy")
            {
                gettingInjured("HitEnemy");
            }

            extraCollisionStay(c);
        }

        /// <summary>
        /// leaving the current collision
        /// </summary>
        /// <param name="c"> object to collide with </param>
        void OnCollisionExit(Collision c)
        {
            onCollision = false;
            extraCollisionExit(c);
        }

        /// <summary>
        /// provides extra collision enter class
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public virtual void extraCollisionEnter(Collision c) 
        { 
        }

        /// <summary>
        /// provides extra collision stay class
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public virtual void extraCollisionStay(Collision c) 
        { 
        }

        /// <summary>
        /// provides extra collision exit class
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public virtual void extraCollisionExit(Collision c) 
        { 
        }

        #endregion

        #region OnTrigger

        void OnTriggerEnter(Collider c)
        {
            switch (c.gameObject.tag)
            {
                case "Respawn":
                    gameVariables.savePoint(trans.position, lava.transform.position, energy);
                    // gameVariables.lastRespawn = c.gameObject.transform.position;
                    // gameVariables.lastLavaPos = lava.transform.position;
                    // gameVariables.lastEnergy = energy;
                    Animator flagAnimator = c.gameObject.GetComponent<Animator>();
                    if (flagAnimator != null)
                    {
                        flagAnimator.SetBool("isRaised", true);
                    }

                    else
                    {
                        Destroy(c.gameObject);
                    }

                    break;
                case "HitEnemy":
                    // trans.position = (new Vector3(trans.position.x - 2, trans.position.y, trans.position.z));
                    // rigidbody.AddForce( -300, 300, 0, ForceMode.Acceleration);
                    // rigidbody.velocity = new Vector3(-5, 5, 0);
                    gettingInjured("HitEnemy");
                    break;
                case "Poison":
                    gettingInjured("Poison");
                    break;
            }

            extraTriggerEnter(c);
        }

        void OnTriggerStay(Collider c)
        {
            extraTiggerStay(c);
        }

        void OnTriggerExit(Collider c)
        {
            extraTriggerExit(c);
        }

        /// <summary>
        /// provides extra on trigger stay class for player
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public virtual void extraTiggerStay(Collider c) 
        { 
        }

        /// <summary>
        /// provides extra on trigger enter class for player
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public virtual void extraTriggerEnter(Collider c) 
        { 
        }

        /// <summary>
        /// provides extra on trigger exit class for player
        /// </summary>
        /// <param name="c"> object to collide with </param>
        public virtual void extraTriggerExit(Collider c) 
        { 
        }

        #endregion

        #endregion

        #region Energy & Transformation
        private void calcTransformStayCost()
        {
            // Recover energy else loose energy
            if (currentTransformation <= 1 && energy < maxEnergy)
            {
                energy += Time.deltaTime * energyRecover;
            }
            else if (currentTransformation > 1)  
            {
                energy -= Time.deltaTime * currentTransformationCost;
            }

            if (energy <= 0)
            {
                reTransform();
            }

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }

        /// <summary>
        /// transform to normal transformation
        /// </summary>
        private void reTransform()
        {
            // energy = 0;
            transformTo(1);
        }

        private float checkTransform(int i)
        {
            switch (i)
            {
                case 2:
                    return inventory.GetItems("Dragon");
                case 3:
                    return inventory.GetItems("Squirrel");
                case 4:
                    return inventory.GetItems("Boar");
            }

            return 1;
        }

        /// <summary>
        /// i == new transformation
        /// </summary>
        /// <param name="i"> number of transform </param>
        private void transformTo(int i)
        {
            if (i == currentTransformation || 0 == checkTransform(i))
            {
                return;
            }

            beforeTransformationChange();

            // if can't transform 
            if (energy < maxEnergy)
            {
                i = 1;
                // if player switched to human and he has less energy: we don't show an extra transfrom animation
                if (i == currentTransformation)
                {
                    return;
                }
            }
            else
            {
                energy -= transformCost;
            }

            if (i <= 1)
            {
                // energy = 0;
                if (currentTransformation > 1 && i > 1)
                {
                    return;
                }
            }

            GameObject newTrans = instantiateTransform(i);
            GameObject clound = (GameObject)Instantiate(cloudPrefab, trans.position, Quaternion.identity);
            clound.transform.parent = newTrans.transform;
            transformStart();
        }

        /// <summary>
        /// initiates transformation
        /// </summary>
        /// <param name="i"> number of transformation </param>
        /// <returns></returns>
        public GameObject instantiateTransform(int i)
        {
            GameObject temp = playerPrefab;
            Quaternion transformationQuaternion = Quaternion.Euler(new Vector3(0, 0, 0));
            Vector3 transformationPosition = trans.position;
            switch (i)
            {
                case 1:
                    if (currentTransformation == 4)
                    {
                        transformationPosition = new Vector3(trans.position.x, trans.position.y - 2f, 0f);
                    }

                    else
                    {
                        transformationPosition = new Vector3(trans.position.x, trans.position.y + 0f, 0f);
                    }

                    transformationQuaternion = Quaternion.Euler(new Vector3(0, 180, 0));
                    break;
                case 2:
                    transformationQuaternion = Quaternion.Euler(new Vector3(90, 0, 180));
                    transformationPosition = new Vector3(trans.position.x, trans.position.y + 0.8f, 0f);
                    temp = dragonPrefab;
                    break;
                case 3:
                    transformationQuaternion = Quaternion.Euler(new Vector3(90, 90, 270));
                    transformationPosition = new Vector3(trans.position.x, trans.position.y + 0f, 0.5f);
                    temp = squirrelPrefab;
                    break;
                case 4:
                    transformationQuaternion = Quaternion.Euler(new Vector3(90, 180, 0));
                    transformationPosition = new Vector3(trans.position.x + 2f, trans.position.y + 3.5f, trans.position.z - 3.2f);
                    temp = boarPrefab;
                    break;
            }

            currentTransformation = i;
            Destroy(gameObject);
            return (GameObject)Instantiate(temp, transformationPosition, transformationQuaternion);
        }

        private void transformStart()
        {
            if (transLastTime <= System.DateTime.Now - energyRecoveryTime)
            {
                transLastTime = System.DateTime.Now;
            }
        }

        #endregion
        /// <summary>
        /// if a Player gets hit by poison he will be slowed for 2(PoisonTimer) seconds.
        /// </summary>
        public void gettingPoisoned()
        {
            poisonTimer = 2f;
            poisoned = true;
        }

        /// <summary>
        /// calculates the damage for the player by the object of which he gets hit (enemy, lava, etc)
        /// </summary>
        /// <param name="enemyname"> the name of the object by which the player gets hit </param>
        public void gettingInjured(string enemyname)
        {
            lastHitTime = System.DateTime.Now;
            switch (enemyname)
            {
                case "Tiger":
                    gameVariables.lifePoints = 0;
                    deathScreenstart = System.DateTime.Now;
                    isDeathScreen = true;
                    break;
                case "Lava":
                    gameVariables.hearts = 0;
                    deathScreenstart = System.DateTime.Now;
                    isDeathScreen = true;
                    break;
                case "HitEnemy":
                    if (canTakeDamage)
                    {
                        canTakeDamage = false;
                        gameVariables.lifePoints--;
                        StartCoroutine(waitTime(invulnerabilityTime));
                        StartCoroutine(damageEffect());
                    }

                    break;
                case "KillingPlane":
                    deathScreenstart = System.DateTime.Now;
                    isDeathScreen = true;
                    gameVariables.loseHeart();
                    break;
                case "Poison":
                    gettingPoisoned();
                    break;
            }

            if (gameVariables.hearts <= 0 || (gameVariables.lifePoints == 0 && gameVariables.hearts == 1))
            {
                gameVariables.hearts = 0;
                Application.LoadLevel("GameOverMenu");
            }

            else if (gameVariables.lifePoints <= 0)
            {
                deathScreenstart = System.DateTime.Now;
                isDeathScreen = true;
                gameVariables.loseHeart();
                respawnPlayer();
            }

            /* if (gameVariables.hearts <= 0)
             {
                 //gameVariables.resetVariables();
                 Application.LoadLevel("GameOverMenu");
             }*/
        }

        /// <summary>
        /// makes player visible/invisible (blinks) after taking a hit
        /// </summary>
        /// <returns></returns>
        public IEnumerator damageEffect()
        {
            playerRendererComponent = (SkinnedMeshRenderer)this.GetComponentInChildren(typeof(SkinnedMeshRenderer));
            playerRendererComponent.renderer.enabled = false;
            yield return new WaitForSeconds(damageEffectPause);
            playerRendererComponent.renderer.enabled = true;
            yield return new WaitForSeconds(damageEffectPause);
            playerRendererComponent.renderer.enabled = false;
            yield return new WaitForSeconds(damageEffectPause);
            playerRendererComponent.renderer.enabled = true;
            yield return new WaitForSeconds(damageEffectPause);
            playerRendererComponent.renderer.enabled = false;
            yield return new WaitForSeconds(damageEffectPause);
            playerRendererComponent.renderer.enabled = true;
        }

        /// <summary>
        /// Waiting routine for temporary immortality
        /// </summary>
        /// <param name="seconds"> time to wait </param>
        /// <returns></returns>
        IEnumerator waitTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canTakeDamage = true;
        }
        /// <summary>
        /// re-spawns the player to the last re-spawn point
        /// </summary>
        public void respawnPlayer()
        {
            transLastTime = System.DateTime.Now - new System.TimeSpan(0, 0, 0, 5, 0);
            gameVariables.wasRespawned = true;
            poisoned = false;
            currentTransformation = 1;
            Application.LoadLevel(Application.loadedLevel);
        }

        /// <summary>
        /// Recovery the Player after a certain time (modify coolDownEnemyHit and coolDownRecovery to change that)
        /// </summary>
        public void playerAutoRecovery()
        {
            System.DateTime now = System.DateTime.Now;
            if (((now - lastHitTime) > coolDownEnemyHit) && gameVariables.maxLifePoints > gameVariables.lifePoints && !isInfight)
            {
                gameVariables.lifePoints++;
                lastHitTime = System.DateTime.Now;
            }
        }

        #region OnGUI
        /// <summary>
        /// provides GUI elements of player
        /// </summary>
        public virtual void OnGUI()
        {
            // Decides which color the screen gets, depending on the amount of livepoints
            // if you get hidden once (so that you have 2 lifepoints left) the screen gets grey
            // if you get hidden twice (so that you have 1 lifepoint left) the screen gets red

            switch (gameVariables.lifePoints)
            {
                case 2:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), greyFrame);
                    break;
                case 1:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), redFrame);
                    break;
            }

            switch (poisoned)
            {
                case true:
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), greenFrame);
                    break;
            }

            // if there are no lifepoints left, you are dying (and resporning) and the screen gets red for a while
            // you can adjust the time by editing the "coolDownDeathScreen" variable

            if (isDeathScreen)
            {
                if (System.DateTime.Now - deathScreenstart > coolDownDeathScreen)
                {
                    isDeathScreen = false;
                }

                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deathScreen);
            }

            // HUD variables (grey box left upper corner)
            int guiHeight = 120;
            int guiLength = 155;
            GUI.DrawTexture(new Rect(5, 5, guiLength, guiHeight), guiBackGround);

            float lavaDistance = Mathf.Abs(lava.transform.position.x - trans.position.x);
            if (lavaDistance + 55 < guiLength)
            {
                GUI.DrawTexture(new Rect(15 + lavaDistance, 10, 40, 40), playerTexture);
            }
            else
            {
                GUI.DrawTexture(new Rect(guiLength - 40, 10, 40, 40), playerTexture);
            }

            GUI.DrawTexture(new Rect(10, 10, 40, 40), lavaTexture);

            int i = 0;
            for (; i < gameVariables.hearts - 1 && i < gameVariables.maxShowHearts - 1; i++)
            {
                GUI.DrawTexture(new Rect(20 + i * 20, 52, 20, 20), playerHeart);
            }

            switch (gameVariables.lifePoints)
            {
                case 3:
                    GUI.DrawTexture(new Rect(20 + i * 20, 52, 20, 20), playerHeart);
                    break;
                case 2:
                    GUI.DrawTexture(new Rect(20 + i * 20, 52, 20, 20), playerHeart_2_3);
                    break;
                case 1:
                    GUI.DrawTexture(new Rect(20 + i * 20, 52, 20, 20), playerHeart_1_3);
                    break;
            }

            for (; i < gameVariables.minShowHearts; i++)
            {
                GUI.DrawTexture(new Rect(20 + i * 20, 52, 20, 20), playerHeartEmpty);
            }

            // Display the energybar this contains two bars, the first one has a fixed size and the second
            // one gets scaled in realation to the variables "energy" 
            GUI.DrawTexture(new Rect(20, 85, 133, 10), energyBarTextureEmpty, ScaleMode.StretchToFill, true);
            GUI.DrawTexture(new Rect(20, 85, (energy / maxEnergy) * 133 / 1, 10), energyBarTextureFill, ScaleMode.StretchToFill, true);
            // GUI Text "transformation Time"
            GUIStyle ourStyle = new GUIStyle();
            ourStyle.fontSize = 10;
            ourStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(20, 85, 1, 20), "Transformation Time", ourStyle);

            // Display the number of current coins 
            GUIStyle coinStyle = new GUIStyle();
            coinStyle.normal.textColor = Color.black;
            string c = "Coins : " + inventory.GetItems("coins");
            GUI.Label(new Rect(20, 70, 2 * Screen.width, 4 * Screen.height / 10), c, coinStyle);
        }
        #endregion
    }
}