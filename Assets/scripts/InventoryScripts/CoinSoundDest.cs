using UnityEngine;
using System.Collections;

namespace Assets.InventoryScripts
{
    /// <summary>
    /// this class destroys the soundObject form the coin
    /// </summary>
    public class CoinSoundDest : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!audio.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
