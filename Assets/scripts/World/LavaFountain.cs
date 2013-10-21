using UnityEngine;
using System.Collections;

namespace Assets.World
{
    /// <summary>
    /// THis class kills old LavaFountains
    /// </summary>
    public class LavaFountain : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // destroy fountain after particles disappear
            if (!particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
