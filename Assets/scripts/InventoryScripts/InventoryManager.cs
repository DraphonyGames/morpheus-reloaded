using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.scripts.InventoryScripts
{
    /// <summary>
    /// Links Inventory with scenes / player, e.g. makes access of the "scriptable object" inventory possible.
    /// The InventoryManager script must to be added in each scene (preferably with the GameController prefab). 
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        /// <summary>
        /// create variable for access of inventory class
        /// </summary>
        public static Inventory inventory;
        // variables for being able to use transformations, not used at the moment
        // public bool hasDragon = true;
        // public bool hasSquirrel = true;

        // Inventory Screen measurements: startingpoint x
        ////private int x = Screen.width / 3 * 2;
        // Inventory Screen measurements: startingpoint y
        ////private int y = Screen.height / (Screen.height / 2) + 15;
        // Inventory Screen measurements: height
        private int inventoryHeight;
        // determines whether inventory screen is shown or not
        private bool showInventoryScreen = false;
        // name of the button (e.g. name of the item that can be clicked)
        private string buttonLabel;

        // gameVariables
        GameVariables vars;

        private bool ableToOpenInventory = false;

        // helper variables to work with the inventory items
        private List<string> keys = new List<string>();
        private List<float> values = new List<float>();
        private List<string> ignoreItems = new List<string>() { "Squirrel", "Dragon", "Boar" };

        // initialization
        void Awake()
        {
            // is inventory already created? if not create instance
            if (inventory == null)
            {
                inventory = (Inventory)ScriptableObject.CreateInstance(typeof(Inventory));
                // init values
                inventory.SetItems("coins", 0);
                inventory.SetItems("Squirrel", 0);
                inventory.SetItems("Dragon", 0);
                inventory.SetItems("Boar", 0);

                // (de-) activate this item for level testing purposes - gives another heart when clicked
                inventory.SetItems("Life Potion", 100);
            }
        }

        void Start()
        {
            Screen.showCursor = true;
            vars = GameController.gameVariables;
            if (!vars.nonPlayableLevels.Contains(Application.loadedLevelName))
            {
                ableToOpenInventory = true;
            }
        }

        void Update()
        {   // store inventory.items dictionary (items / values) for later use
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<string, float> kvp in inventory.items)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }

            // keybinding for inventory
            if (Input.GetKeyDown("i"))
            {
                showInventoryScreen = !showInventoryScreen;
            }
        }
        // handles rendering of inventory screen / buttons
        void OnGUI()
        {
            if (showInventoryScreen && ableToOpenInventory)
            {
                float x = Screen.width / 6f;
                float y = Screen.height / 6f;
                GUI.Box(new Rect(x, y, 148, inventory.items.Count * 20 + 40), "Inventory");
                for (int i = 0; i < keys.Count; i++)
                {
                    // if the item is on ignorelist, don't show a button (e.g. the transformation squirrel needs the item Squirrel)
                    if (!ignoreItems.Contains(keys[i]) && values[i] > 0)
                    {
                        y += 20;
                        drawInventoryButtons(keys[i], values[i], x, y);
                    }
                }
            }
        }

        /// <summary>
        /// This method realizes the buttons which use the items from the inventory when clicked.
        /// </summary>
        /// <param name="k">name of the item</param>
        /// <param name="v">quantity of the item</param>
        /// <param name="x">x position of the button</param>
        /// <param name="y">y position of the button</param>
        private void drawInventoryButtons(string k, float v, float x, float y)
        {
            int cost = 0;
            switch (k)
            {
                case "coins":
                    {
                        cost = 100;
                        if (GUI.Button(new Rect(x, y, 128, 20), k + ": " + v) && v >= cost && vars.hearts < 5)
                        {
                            GameObject test = GameObject.FindGameObjectsWithTag("Player")[0];
                            test.audio.clip = (AudioClip)Resources.Load("Sounds/level-up");
                            test.audio.Play();
                            inventory.AddItems(k, -cost);
                            vars.hearts++;
                        }

                        break;
                    }

                case "Life Potion":
                    {
                        cost = 1;
                        if (GUI.Button(new Rect(x, y, 128, 20), k + ": " + v) && v >= cost && vars.hearts < 5)
                        {
                            inventory.AddItems(k, -cost);
                            vars.hearts++;
                        }

                        break;
                    }

                default: break;
            }
        }
    }
}