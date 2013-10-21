using UnityEngine;
using System.Collections;

namespace Assets.World
{
    /// <summary>
    /// permanently spawns a fountain
    /// </summary>
    public class LavaFountainPermanent : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!particleSystem.IsAlive())
            {
                timer();
                particleSystem.Play();
            }
        }

        IEnumerator timer()
        {
            yield return new WaitForSeconds(3);
        }
    }
}
