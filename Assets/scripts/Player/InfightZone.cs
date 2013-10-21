using UnityEngine;
using System.Collections;
namespace Assets.scripts.Player
{
    /// <summary>
    /// Class checks if the player character is in a fight. Actually checks if the player collides with an enemy.
    /// </summary>
    public class InfightZone : MonoBehaviour
    {
        private int frames;

        // Update is called once per frame
        void Update()
        {
            frames++;
            if (frames > 1)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerStay(Collider otherObjekt)
        {
            if (otherObjekt.gameObject.tag == "Enemy")
            {
                AbstPlayer.isInfight = true;
            }
            else
            {
                AbstPlayer.isInfight = false;
            }
        }
    }
}