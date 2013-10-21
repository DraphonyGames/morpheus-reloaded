using UnityEngine;
using System.Collections;
using Assets.scripts;
namespace Assets.scripts.Enemy.DragonScripts
{
    /// <summary>
    /// this class controls the boss dragon 
    /// </summary>
    public class BossDragon : MonoBehaviour
    {
        #region public fields

        /// <summary>
        /// tells the position where to land, launch and the next
        /// one in a link list like structure
        /// </summary>
        public GameObject currentLandingZone;
        /// <summary>
        /// the position from which to launch
        /// </summary>
        public GameObject currentLaunchZone;

        #endregion

        #region private fields

        private bool win = false;

        private int livePoints = 2;

        private GameObject rock; // rockPrefab
        private GameObject currentRock; // rock holding
        private GameObject dragonFire;
        private GameObject dragonFireLight;
        private GameObject dragonTransformationItem;
        private GameObject enemyExplosion;
        private GameObject enemyGotHit;
        private GameObject destroyedRock;
        private GameObject coin100;
        private GameObject failText;

        private SceneFader sceneFader;

        private int repositionHeight = 30; // height the dragon flys away, if its higher he respawns later
        private float speed = 20; // dragon speed
        private bool droppedDown = false; // if the rock dropps down
        private Animator ani;

        private Transform trans;
        private int fightState;
        private float startHeight = 9f;
        private int changeStageAtXDifference = 20;
        private GameObject player;
        private bool textIsSet = false;
        private bool isDying = false;
        #endregion

        // Use this for initialization
        void Start()
        {
            rock = (GameObject)Resources.Load("Prefabs/LevelObjects/Rock");
            dragonFire = (GameObject)Resources.Load("Prefabs/Enemy/BossDragon/DragonFire");
            dragonFireLight = (GameObject)Resources.Load("Prefabs/Enemy/BossDragon/DragonFireLight");
            dragonTransformationItem = (GameObject)Resources.Load("Prefabs/Player/CollectTransformations/CollectDragon");
            enemyExplosion = (GameObject)Resources.Load("Prefabs/Enemy/EnemyExplosion");
            sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
            destroyedRock = (GameObject)Resources.Load("Prefabs/LevelObjects/RockCloud");
            enemyGotHit = (GameObject)Resources.Load("Prefabs/Enemy/EnemyExplosion2");
            coin100 = (GameObject)Resources.Load("Prefabs/Coins/Coin100");
            failText = (GameObject)Resources.Load("Prefabs/Enemy/BossDragon/FailText");

            trans = transform;

            ani = GetComponent<Animator>();
            ani.SetBool("isFlying", true);

            fightState = 1;
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            resetPos();
        }

        // Update is called once per frame
        void Update()
        {
            // check if dragon is still alive
            // else: decide which logic is used
            if (livePoints == 0)
            {
                fightState = 0;
            }

            // find player
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            else
            {
                switch (fightState)
                {
                    case 0:
                        die();
                        break;
                    case 1:
                        firstStageBehaviour();
                        break;
                    case 2:
                        setFlyingToWalkingLogic();
                        break;
                    case 3:
                        secondStageBehaviour();
                        break;
                    case 4:
                        setWalkingToFlyingLogic();
                        break;
                }
            }
        }

        // creates new rock at dragons current position
        private void getRock()
        {
            currentRock = (GameObject)Instantiate(rock, trans.position, Quaternion.identity);
            currentRock.transform.position = new Vector3(trans.position.x, trans.position.y - 1, 0f);
            currentRock.transform.parent = trans;
        }

        // throws rock, when dragon reaches the postion of the player
        private void throwRock()
        {
            droppedDown = true;
            currentRock.transform.parent = null;
            Rigidbody body = currentRock.AddComponent<Rigidbody>();
            body.AddForce(new Vector3(5, 0, 0), ForceMode.VelocityChange);
            body.constraints = RigidbodyConstraints.FreezePositionZ;
            body.mass = 500;
        }

        // resets dragons position above the lava after throwing a rock
        private void resetPos()
        {
            trans.position = new Vector3(player.transform.position.x - 60f, startHeight, 0f);
            trans.transform.Translate(0, startHeight, 0f);
            droppedDown = false;
            getRock();
        }

        #region stage moving logic

        // logic for boss while the player is on stage1
        private void firstStageBehaviour()
        {
            ani.SetBool("isFlying", true);
            // reset dragon
            if (trans.position.y > repositionHeight)
            {
                resetPos();
            }
            // flys away
            if (droppedDown)
            {
                trans.transform.Translate(Time.deltaTime * speed, Time.deltaTime * (speed / 5f), 0, Space.World);
            }
            else
            {
                // flys to player
                if (trans.position.x < player.transform.position.x + 5)
                {
                    trans.transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
                }
                else
                {
                    throwRock();
                }

                if (currentLandingZone == null)
                {
                    endgame();
                }
                else
                {
                    bool playerIsInPosition = ((currentLandingZone.transform.position.x - changeStageAtXDifference) < player.transform.position.x);
                    bool dragonIsInPosition = (trans.position.x + speed) < currentLandingZone.transform.position.x;
                    if (playerIsInPosition && dragonIsInPosition)
                    {
                        if (!droppedDown)
                        {
                            throwRock();
                        }

                        fightState = 2;
                    }
                }
            }
        }

        // logic for boss while the player is on stage2
        private void secondStageBehaviour()
        {
            ani.SetBool("isEndingFlying", false);
            // player is on higher ground; else: player is in front of the dragon
            if ((player.transform.position.y) > (trans.position.y + 6))
            {
                ani.SetBool("isWalking", true);
                trans.transform.Translate(Time.deltaTime * (speed / 6), 0, 0, Space.World);
            }
            else
            {
                ani.SetBool("isWalking", true);
                float dragonPlayerDistance = player.transform.position.x - trans.position.x;
                trans.transform.Translate(Time.deltaTime * (speed / 5), 0, 0, Space.World);
                if (dragonPlayerDistance < 15 && dragonPlayerDistance > 0)
                {
                    breathFire();
                }
            }

            bool dragonIsInPosition = trans.position.x > currentLaunchZone.transform.position.x;
            if (dragonIsInPosition)
            {
                fightState = 4;
            }
        }

        // sets boss from flying to walking
        private void setFlyingToWalkingLogic()
        {
            // fly to landing Zone
            if (trans.position.x < currentLandingZone.transform.position.x)
            {
                trans.transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
            }
            else if (!(player.transform.position.x > trans.position.x))
            {
            }
            else
            {
                landingLogic(0.6f);
            }
        }

        // sets boss from walking to flying
        private void setWalkingToFlyingLogic()
        {
            // walk to launch zone
            if ((trans.position.x + speed) < currentLaunchZone.transform.position.x)
            {
                trans.transform.Translate(Time.deltaTime * (speed / 2), 0, 0, Space.World);
            }
            else
            {
                // start flying
                ani.SetBool("isWalking", false);
                ani.SetBool("isStartingFlying", true);
                // fly up
                if ((trans.position.x - player.transform.position.x) < changeStageAtXDifference)
                {
                    if (ani.GetBool("isStartingFlying"))
                    {
                        ani.SetBool("isStartingFlying", false);
                        ani.SetBool("isFlying", true);
                    }
                    else
                    {
                        ani.SetBool("isWalking", false);
                        ani.SetBool("isFlying", true);
                    }

                    trans.transform.Translate(0f, Time.deltaTime * (speed / 5f), 0f, Space.World);
                }

                // reset dragon 
                if (trans.position.y > repositionHeight)
                {
                    resetPos();
                    currentLaunchZone = currentLaunchZone.GetComponent<LaunchZone>().nextLaunchZone;
                    fightState = 1;
                }
            }
        }

        #endregion

        private void landingLogic(float seeHeight)
        {
            // look below and then land!
            bool groundIsBelow = false;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, -Vector3.up, seeHeight);
            int counter = 0;
            while (counter < hits.Length)
            {
                RaycastHit hit = hits[counter];
                if (hit.collider.gameObject.tag == "Environment")
                {
                    groundIsBelow = true;
                }

                counter++;
            }

            if (!groundIsBelow)
            {
                trans.transform.Translate(0, -(Time.deltaTime * (speed / 2f)), 0, Space.World);
                if (!Physics.Raycast(transform.position, -Vector3.up, 30f))
                {
                    trans.transform.Translate(Time.deltaTime * speed, 0f, 0f, Space.World);
                }
            }
            else
            {
                if (seeHeight == 0.6f)
                {
                    ani.SetBool("isFlying", false);
                    ani.SetBool("isEndingFlying", true);
                    currentLandingZone = currentLandingZone.GetComponent<LandingZone>().nextLandingZone;
                    fightState = 3;
                }
            }
        }

        // dragon breath fire
        private void breathFire()
        {
            Vector3 position = new Vector3(transform.position.x + 3f, transform.position.y + 1.3f, transform.position.z);
            if (dragonFire != null && dragonFireLight != null)
            {
                Instantiate(dragonFire, position, Quaternion.Euler(0f, 90f, 0f));
                Instantiate(dragonFireLight, position, Quaternion.Euler(0f, 90f, 0f));
            }
        }

        // dragon dies
        private void die()
        {
            win = true;
            // can i land ?
            if ((trans.position.x > player.transform.position.x + 10) || isDying)
            {
                isDying = true;
                landingLogic(1000f);
                if (Physics.Raycast(transform.position, -Vector3.up, 1f))
                {
                    GameObject dragonEgg = (GameObject)Instantiate(dragonTransformationItem, trans.position, Quaternion.identity);
                    dragonEgg.AddComponent("NextLevelCollider");
                    dragonEgg.GetComponent<NextLevelCollider>().nextLevelName = "level1_Part3";
                    dragonEgg.transform.position = new Vector3(dragonEgg.transform.position.x, dragonEgg.transform.position.y + 1, dragonEgg.transform.position.z);
                    for (int counter = 0; counter < 3; counter++)
                    {
                        Vector3 spawnPosition = new Vector3(trans.position.x - counter- 2, trans.position.y + 1, trans.position.z);
                        Instantiate(coin100, spawnPosition, Quaternion.identity);
                    }

                    Instantiate(enemyExplosion, trans.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else
            {
                // fly behind player
                trans.transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
            }
        }

        // the lvl is done and rest logic has to be done
        private void endgame()
        {
            if (!win)
            {
                // restart 
                Vector3 spawnTextPos = new Vector3(player.transform.position.x - 5, player.transform.position.y + 5f, 0f);
                if (!textIsSet)
                {
                    Instantiate(failText, spawnTextPos, Quaternion.identity);
                    textIsSet = true;
                }

                StartCoroutine(wait(5));               
            }
            else
            {
                // win screen or some
                sceneFader.SwitchScene("level1_Part3");
            }
        }

        // well things collide and we want to know what and react to it
        void OnCollisionEnter(Collision collisionCollider)
        {
            switch (collisionCollider.gameObject.tag)
            {
                case "LavaRock":
                    // play hit animation
                    livePoints--;
                    Instantiate(enemyGotHit, trans.position, Quaternion.identity);
                    Instantiate(destroyedRock, collisionCollider.gameObject.transform.position, Quaternion.identity);
                    Destroy(collisionCollider.gameObject);
                    break;
            }
        }

        private IEnumerator wait(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            sceneFader.SwitchScene("DragonBossPart");
        }
    }
}