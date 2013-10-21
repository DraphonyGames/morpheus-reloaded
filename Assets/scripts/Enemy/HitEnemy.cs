using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// Destroys hit prefab of enemy after hitting the player.
    /// </summary>
    public class HitEnemy : MonoBehaviour
    {
        private float stay = 0.05f;
        // Use this for initialization
        void Start()
        {
            renderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            stay -= Time.deltaTime;
            if (stay <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}