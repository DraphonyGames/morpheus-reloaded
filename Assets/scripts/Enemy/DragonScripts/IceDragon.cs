using UnityEngine;
using System.Collections;

namespace Assets.scripts.Enemy.DragonScripts
{   
    /// <summary>
    /// This class manages the Ice Dragon enemy.
    /// </summary>
    public class IceDragon : AbstEnemy
    {
        /// <summary>
        /// see player serves as a simple trigger for the dragon to fire when the player is near
        /// </summary>
        public bool seePlayer = false;
        /// <summary>
        /// ice breath of the dragon
        /// </summary>
        public GameObject iceFire;
        /// <summary>
        /// lighting effect of the ice breath
        /// </summary>
        public GameObject dragonFireLight;
        /// <summary>
        /// 
        /// </summary>
        public int direction = -1;
        /// <summary>
        /// 
        /// </summary>
        public float distance;
        ////private float hitTime = 1.5f;
        private float seeDistance = 30;
        private bool isFrozen = false;
        private Animator animator;
        private SceneFader sceneFader;
        private float speed = 6;
        private GameObject continueText;
        private bool continueTextIsSet = false;
        /// <summary>
        /// add start function for enemy which is called by the parent class
        /// </summary>
        public override void startEnemy()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isFlying", false);
            animator.SetBool("isEndingFlying", true);

            sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
            continueText = (GameObject)Resources.Load("Prefabs/LevelObjects/Outro/continueText");
        }

        /// <summary>
        /// add update function for enemy which is called by the parent class
        /// </summary>
        public override void updateEnemy()
        {
            distance = Vector3.Distance(transform.position, AbstEnemy.playerPosition);
            if ( distance < seeDistance)
            {
                seePlayer = true;
                ////animator.SetBool("IsAttacking", true);
            }
            else
            {
                seePlayer = false;
            }

            if (seePlayer == true)
            {
                animator.SetBool("isEndingFlying", false);
                animator.SetBool("isFlying", true);
                takePlayer();
            }
        }

        private void breathIce()
        {
            Vector3 position = new Vector3(transform.position.x + -9f, transform.position.y + 4.6f, transform.position.z);
            if (iceFire != null && dragonFireLight != null)
            {
                Instantiate(iceFire, position, Quaternion.Euler(10f, 270f, 0f));
                Instantiate(dragonFireLight, position, Quaternion.Euler(10f, 270f, 0f));
            }
        }

        private void takePlayer()
        {
            if (!isFrozen)
            {
                breathIce();
            }

            GameObject iceBlock = GameObject.FindWithTag("IceBlock");
            if (iceBlock != null)
            {
                isFrozen = true;
            }

            if (!(transform.position.x <= playerPosition.x - 2.5f))
            {
                transform.Translate(Time.deltaTime * -speed, Time.deltaTime * (speed / 9f), 0f, Space.World);
            }
            else
            {
                turnAndGrapPlayer(iceBlock);
            }
        }

        private void turnAndGrapPlayer(GameObject iceBlock)
        {
            if (iceBlock != null)
            {
                flyAwayWithPlayer(iceBlock);
            }
            else
            {
                transform.Translate(0f, speed * 2 * Time.deltaTime, 0f, Space.World);
                if (transform.position.y > 20)
                {
                    sceneFader.SwitchScene("MainMenu");
                }
            }
        }

        private void flyAwayWithPlayer(GameObject iceBlock)
        {
            if (transform.position.y < 20)
            {
                transform.Translate(0f, speed * Time.deltaTime, 0f, Space.World);
                iceBlock.transform.position = new Vector3(transform.position.x + 2, transform.position.y - 1.3f, transform.position.z);
                if (!continueTextIsSet)
                {
                    Vector3 spawnTextPos = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
                    Instantiate(continueText, spawnTextPos, Quaternion.identity);
                    continueTextIsSet = true;
                }
            }
            else
            {
                sceneFader.SwitchScene("MainMenu");
            }
        }

        /// <summary>
        /// overrides on trigger enter class in abstract enemy
        /// </summary>
        /// <param name="collidingObject"></param>
        public override void OnTriggerEnter(Collider collidingObject)
        { 
        }
    }
}