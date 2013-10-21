using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// initiates burning effect to enemy
    /// </summary>
    public class Burning : MonoBehaviour
    {
        private int frames;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            frames++;
            if (frames > 300)
            {
                Destroy(gameObject);
            }
        }
    }
}