using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Assets.scripts.InventoryScripts
{
    /// <summary>
    /// "Inventory": keep Power ups, Coins in a stash from level to level
    /// </summary>
    public class Inventory : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// Dictionary as inventory. Key is the item name and value the quantity of aforementioned item.
        /// </summary>
        public Dictionary<string, float> items = new Dictionary<string, float>();

        /// <summary>
        /// Adds Items to Inventory, if they already exist the quantity will be increased.
        /// </summary>
        /// <param name="itemName">key/name of the item</param>
        /// <param name="quantity">quantity of the items</param>
        public void AddItems(string itemName, float quantity)
        {
            float oldValue = 0;
            // uncomment if you want to use lower case for inventory (avoid duplicate item listings in dictionary)
            // itemName = itemName.ToLower();
            if (items.TryGetValue(itemName, out oldValue))
            {
                quantity += oldValue;
                items[itemName] = quantity;
            }
            else
            {
                items.Add(itemName, quantity);
            }
        }
        /// <summary>
        /// Set item to a specific value
        /// </summary>
        /// <param name="itemName">key/name of the item</param>
        /// <param name="quantity">number of items</param>
        public void SetItems(string itemName, float quantity)
        {
            float oldValue = 0;
            // uncomment if you want to use lower case for inventory (avoid duplicate item listings in dictionary)
            // itemName = itemName.ToLower();
            if (items.TryGetValue(itemName, out oldValue))
            {
                items[itemName] = quantity;
            }
            else
            {
                items.Add(itemName, quantity);
            }
        }
        /// <summary>
        /// Check Inventory for an item
        /// </summary>
        /// <param name="itemName">search: key/name of item</param>
        /// <returns>number of items in the inventory</returns>
        public float GetItems(string itemName)
        {
            float currentValue = 0;
            float result = 0;
            // uncomment if you want to use lower case for inventory (avoid duplicate item listings in dictionary)
            // itemName = itemName.ToLower();
            if (items.TryGetValue(itemName, out currentValue))
            {
                result = currentValue;
            }

            return result;
        }
        #endregion
    }
}