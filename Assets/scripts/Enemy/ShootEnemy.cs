using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// Provides the speed for enemy's shots.
    /// </summary>
    public class ShootEnemy : MonoBehaviour
    {
        float bulletspeed = 7;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            float amtToMove = bulletspeed * Time.deltaTime;
            transform.Translate(Vector3.left * amtToMove);
        }
    }
}
