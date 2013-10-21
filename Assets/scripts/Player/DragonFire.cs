using UnityEngine;
using System.Collections;

/// <summary>
/// Class destroys the fire breath (a unity particle system) of the dragon after it's burned out.
/// </summary>
public class DragonFire : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // destroys the flames after particles disappeared
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
