using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// Adjusts the behavior of shooting enemy
    /// </summary>
    public class EnemyTurret : AbstEnemy
    {
        #region fields
        /// <summary>
        /// loads enemy's shot prefab
        /// </summary>
        public GameObject shootEnemyPrefab;
        /// <summary>
        /// sets direction of shots
        /// </summary>
        public static int direction = -1;
        /// <summary>
        /// change state when the player approaches
        /// </summary>
        public bool seePlayer = false;
        /// <summary>
        /// distance between player and enemy
        /// </summary>
        public float distance;
        /// <summary>
        /// loads enemy's animator
        /// </summary>
        public Animator animator;

        private float seeDistance = 15;
        private float hitTime = 2.2f;
        #endregion

        /// <summary>
        /// Use this for initialization
        /// </summary>
        public override void startEnemy()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        public override void updateEnemy()
        {
            // Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            float distance = Vector3.Distance(transform.position, AbstEnemy.playerPosition);

            // if player is too far away this enemy will wait
            if (distance >= seeDistance)
            {
                seePlayer = false;
                animator.SetBool("isAttacking", seePlayer);
            }

            // if Player is close enough the enemy starts to shoot
            if (distance < seeDistance)
            {
                seePlayer = true;
                animator.SetBool("isAttacking", seePlayer);
            }

            hitTime -= Time.deltaTime;
            if (seePlayer && (hitTime < 0))
            {
                Vector3 positions = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y + 2.5f, transform.position.z);
                Instantiate(shootEnemyPrefab, positions, Quaternion.identity);
                hitTime = 1.8f;
            }
        }
        /// <summary>
        /// collision with enemy turret
        /// </summary>
        /// <param name="otherObject">object that collides with turret</param>
        public override void OnTriggerEnter(Collider otherObject)
        {
        }
    }
}