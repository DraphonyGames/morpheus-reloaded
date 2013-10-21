using UnityEngine;
using System.Collections;

namespace Assets.World
{
    /// <summary>
    /// this class destroys old fogs
    /// </summary>
    public class Fog : MonoBehaviour
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
                Destroy(gameObject);
            }
        }
    }
}
