using UnityEngine;
using System.Collections;
namespace Assets.scripts.InventoryScripts
{
    /// <summary>
    /// Manages collection of items
    /// everything that ends up in the Inventory needs this Script assigned to Prefabs
    /// </summary>
    public class InventoryCollider : MonoBehaviour
    {
        /// <summary>
        /// name of the item that is being collected
        /// </summary>
        public string itemName = "";
        /// <summary>
        /// quantity of the item that is being collected (quiver of arrows or 1 potion)
        /// </summary>
        public float value = 1;
        /// <summary>
        /// using the gatheringCoinPrefab in unity to make a cloud appear after items have been collected and a sound is played
        /// </summary>
        public GameObject gatheringCoinPrefab;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                InventoryManager.inventory.AddItems(itemName, value);
                Destroy(gameObject);
                Instantiate(gatheringCoinPrefab, transform.position, Quaternion.identity);
            }
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0f, Time.deltaTime * 50f, 0f, Space.World);
        }
    }
}