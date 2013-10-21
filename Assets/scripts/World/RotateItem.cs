using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// collectible items can be made to spin by this script
    /// </summary>
    public class RotateItem : MonoBehaviour
    {
        /// <summary>
        /// GameObject that is being put in place of the collected item
        /// </summary>
        public GameObject gatheringCoinPrefab;
        // rotation speed of the item
        private int rotationSpeed = 200;

        void Update()
        {
            // rotate
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotation, 0f);
        }
    }
}