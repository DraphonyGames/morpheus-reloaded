using UnityEngine;
using System.Collections;
namespace Assets.scripts.Player
{
    /// <summary>
    /// This Class manages the graphics effect of the dragon fire. It limits the number of fires in the air,
    /// i.e. the number of prefabs "DragonLightFire".
    /// </summary>
    public class DragonFireLight : MonoBehaviour
    {
        private int frames;

        // Update is called once per frame
        void Update()
        {
            // destroys the flames after an amount of frames
            frames++;
            if (frames > 10)
            {
                Destroy(gameObject);
            }
        }
    }
}