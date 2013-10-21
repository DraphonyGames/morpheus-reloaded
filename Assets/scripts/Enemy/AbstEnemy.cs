using UnityEngine;
using System.Collections;
using Assets.scripts.Player;

namespace Assets.scripts.Enemy
{
    /// <summary>
    /// provides functionality of enemy classes
    /// </summary>
    public abstract class AbstEnemy : MonoBehaviour
    {
        /// <summary>
        /// declares burning enemy prefab
        /// </summary>
        public static GameObject burningEnemy;
        /// <summary>
        /// declares explosion prefab
        /// </summary>
        public GameObject explosion;
        /// <summary>
        /// declares explosion2 prefab
        /// </summary>
        public GameObject explosion2;
        /// <summary>
        /// declares coin prefab
        /// </summary>
        public GameObject coin;
        /// <summary>
        /// switches state, when enemy is set on fire by player
        /// </summary>
        public bool isOnFire = false;
        /// <summary>
        /// switches state, when enemy already burns
        /// </summary>
        public bool allowedToBurn = true;
        /// <summary>
        /// speed of enemy
        /// </summary>
        public float currentspeed = 4;
        /// <summary>
        /// saves player position
        /// </summary>
        public static Vector3 playerPosition;
        /// <summary>
        /// saves enemy position
        /// </summary>
        public Vector3 enemyPosition;
        /// <summary>
        /// saves game variables
        /// </summary>
        public GameVariables gameVariables;
        /// <summary>
        /// sets amount of spawned coins, when enemy dies
        /// </summary>
        protected int enemyValue = 5;

        void Start()
        {
            // load static Prefabs
            burningEnemy = (GameObject)Resources.Load("Prefabs/Enemy/BurningEnemy");
            explosion = (GameObject)Resources.Load("Prefabs/Enemy/EnemyExplosion");
            explosion2 = (GameObject)Resources.Load("Prefabs/Enemy/EnemyExplosion2");
            coin = (GameObject)Resources.Load("Prefabs/Coins/Coin");

            startEnemy();
            gameVariables = GameController.gameVariables;
        }

        void Update()
        {
            if (isOnFire && allowedToBurn)
            {
                burning();
                allowedToBurn = false;
            }

            playerPosition = gameVariables.playerPosition;
            enemyPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
            updateEnemy();
        }

        /// <summary>
        /// provides start behavior for enemies
        /// </summary>
        public abstract void startEnemy();

        /// <summary>
        /// provides behavior for enemies
        /// </summary>
        public abstract void updateEnemy();

        /// <summary>
        /// provides on trigger enter class for enemy
        /// </summary>
        /// <param name="otherObject"> collider for objects the enemy affects </param>
        public abstract void OnTriggerEnter(Collider otherObject);

        void OnCollisionEnter(Collision otherObject)
        {
            switch (otherObject.gameObject.tag)
            {
                case "KillingPlane":
                    // if enemy collides with level borders he will be destroyed
                    Destroy(gameObject);
                    break;
                case "Player":

                    if (AbstPlayer.currentTransformation == 2)
                    {
                        Animator otherObjectAnimator = otherObject.gameObject.GetComponent<Animator>();
                        bool isBombing = otherObjectAnimator.GetBool("isBombing");
                        if (isBombing == true)
                        {
                            Instantiate(explosion2, new Vector3(transform.position.x, transform.position.y), Quaternion.AngleAxis(270, new Vector3(1, 0, 0)));
                            enemyDiesAndSpawnsCoins();
                        }
                    }

                    break;
            }
        }

        void OnParticleCollision(GameObject otherObject)
        {
            if (otherObject.gameObject.tag == "Fire" || otherObject.tag == "RockFire")
            {
                isOnFire = true;
            }
        }

        void burning()
        {
            GameObject obj = (GameObject)Instantiate(burningEnemy, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            obj.transform.parent = transform;
            StartCoroutine("counter");
        }

        IEnumerator counter()
        {
            yield return new WaitForSeconds(3f);
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            enemyDiesAndSpawnsCoins();
        }

        /// <summary>
        /// spawns coins ( e.g. on death )
        /// </summary>
        protected void spawnCoinsPerEnemyValue()
        {
            for (int coins = 0; coins < enemyValue; coins++)
            {
                Vector3 coinPosition = new Vector3(transform.position.x + coins, transform.position.y + (coins % 2), transform.position.z);
                Instantiate(coin, coinPosition, Quaternion.identity);
            }
        }

        /// <summary>
        /// spawn coins on death
        /// </summary>
        protected void enemyDiesAndSpawnsCoins()
        {
            spawnCoinsPerEnemyValue();
            Destroy(gameObject);
        }
    }
}