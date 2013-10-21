using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// deletes explosion prefab after particles disappeared
    /// </summary>
    public class EnemyExplosion : MonoBehaviour
    {
        private int frames;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            ParticleSystem particleSys = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
            if (!particleSys.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}