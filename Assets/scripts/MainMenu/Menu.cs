using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.InventoryScripts;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// This class contains the logic for the MainMenu. It switches the pages.
    /// </summary>
    public class Menu : MonoBehaviour
    {
        private string currentPage = "MainMenu";
        private readonly Dictionary<string, abstPage> pageList;

        /// <summary>
        /// declares GUI style
        /// </summary>
        public GUISkin ourStyle;

        private Texture pausingScreenTexture;
        private SceneFader sceneFader;
        private GameVariables variables;

        private bool hideMenu = false;
        private bool isMainMenu;

        /// <summary>
        /// Pages must be added in the constructor
        /// </summary>
        public Menu()
        {
            pageList = new Dictionary<string, abstPage>();
            // init main page
            pageList.Add(currentPage, new PageMain(currentPage));
            // init oter pages
            pageList.Add("Settings", new PageSettings("Settings"));
            pageList.Add("Score", new PageScore("Score"));
            pageList.Add("GameControl", new PageGameControl("GameControl"));
        }

        // Use this for initialization
        void Start()
        {
            if (Application.loadedLevelName == "GameOverMenu" || Application.loadedLevelName == "Load")
            {
                hideMenu = true;
                return;
            }

            variables = GameController.gameVariables;
            sceneFader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneFader>();
            Screen.showCursor = true;

            isMainMenu = variables.nonPlayableLevels.Contains(Application.loadedLevelName);

            pageList[currentPage].setVariables(GameController.gameVariables);
            pageList[currentPage].setInventory(InventoryManager.inventory);
            pageList[currentPage].setTextures((Texture)Resources.Load("Textures/GUI/GuiBackground"));
            pageList[currentPage].setStyle(ourStyle);
            pageList[currentPage].setIsMenu(isMainMenu);
            pageList[currentPage].setSceneFader(sceneFader);

            if (!GameController.gameVariables.initMisc && isMainMenu)
            {
                currentPage = "Score";
            }

            // if (!isMainMenu)
            {
                pausingScreenTexture = (Texture)Resources.Load("Textures/GUI/GuiBackground");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!hideMenu && ! isMainMenu && Input.GetKey(KeyCode.Escape) && Time.timeScale == 1)
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }

                {
                    Time.timeScale = 0;
                }
            }

            if (isMainMenu && ! hideMenu)
            {
                if (Input.GetKeyDown("1"))
                {
                    sceneFader.SwitchScene("Part1_Level1");
                }
                else if (Input.GetKeyDown("2"))
                {
                    sceneFader.SwitchScene("Part1_Level2");
                }
                else if (Input.GetKeyDown("3"))
                {
                    sceneFader.SwitchScene("DragonBossPart");
                }
                else if (Input.GetKeyDown("4"))
                {
                    sceneFader.SwitchScene("level1_Part3");
                }
                else if (Input.GetKeyDown("5"))
                {
                    sceneFader.SwitchScene("Demo");
                }
                else if (Input.GetKeyDown("6"))
                {
                    sceneFader.SwitchScene("Part1_Level2_boar");
                }
                else if (Input.GetKeyDown("7"))
                {
                    sceneFader.SwitchScene("Part1_Level2_squirrel");
                }
                else if (Input.GetKeyDown("8"))
                {
                    sceneFader.SwitchScene("level1_Part3_outro");
                }
            }
        }

        void OnGUI()
        {
            if (isMainMenu)
            {
                GUI.DrawTexture(new Rect(Screen.width / 20, Screen.height / 20, Screen.width * (18f / 20f), Screen.height * (18f / 20f)), pausingScreenTexture);
                string temp = pageList[currentPage].drawPage();
                if (temp != currentPage)
                {
                    pageList[currentPage].resetClass();
                }

                currentPage = temp;
            }

            else if (Time.timeScale == 0)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pausingScreenTexture);
                string temp = pageList[currentPage].drawPage();
                if (temp == "Resume")
                {
                    Time.timeScale = 1;
                    temp = "MainMenu";
                    pageList[currentPage].resetClass();
                }
                else if (temp != currentPage)
                {
                    pageList[currentPage].resetClass();
                }

                currentPage = temp;
            }
        }
    }

    /// <summary>
    /// Every Page in the Menu must be a child of this class
    /// </summary>
    public abstract class abstPage
    {
        #region variables

        /// <summary>
        /// if menu is used for main menu or pause menu
        /// </summary>
        protected static bool isMainMenu;

        /// <summary>
        /// global borders for pages
        /// </summary>
        protected static float borderX = Screen.width / 20;

        /// <summary>
        /// global borders for pages
        /// </summary>
        protected static float borderY = Screen.height / 20;

        /// <summary>
        /// global column size
        /// </summary>
        protected static float columnX = ((Screen.width - 4 * borderX) + 0.0f) / 4f;

        /// <summary>
        /// global column size
        /// </summary>
        protected static float columnY = ((Screen.height - 4 * borderY) + 0.0f) / 13f;

        /// <summary>
        /// design of GUI
        /// </summary>
        protected static GUISkin guiSkin;

        /// <summary>
        /// name of the current Page
        /// </summary>
        protected string pageName;

        /// <summary>
        /// Game variables
        /// </summary>
        protected static GameVariables vars;

        /// <summary>
        /// Game inventory
        /// </summary>
        protected static Inventory inventory;

        /// <summary>
        /// Scene fader
        /// </summary>
        protected static SceneFader sceneFader;

        /// <summary>
        /// Texture while entering the player name
        /// </summary>
        protected static Texture popUpTexture;

        #endregion

        #region getter setter

        /// <summary>
        /// Set the game variables for all pages
        /// </summary>
        /// <param name="var"></param>
        public void setVariables(GameVariables var)
        {
            vars = var;
        }

        /// <summary>
        /// set style for GUI
        /// </summary>
        /// <param name="skin"></param>
        public void setStyle(GUISkin skin)
        {
            guiSkin = skin;
        }

        /// <summary>
        /// Set the game inventory for all pages
        /// </summary>
        /// <param name="inv"></param>
        public void setInventory(Inventory inv)
        {
            inventory = inv;
        }

        /// <summary>
        /// Set the textures for all pages
        /// </summary>
        /// <param name="popUp"></param>
        public void setTextures(Texture popUp)
        {
            popUpTexture = popUp;
        }

        /// <summary>
        /// sets scenes as menu
        /// </summary>
        /// <param name="menu"></param>
        public void setIsMenu(bool menu)
        {
            isMainMenu = menu;
        }

        /// <summary>
        /// sets scene fader for changing scene
        /// </summary>
        /// <param name="fader"></param>
        public void setSceneFader(SceneFader fader)
        {
            sceneFader = fader;
            Screen.showCursor = true;
        }

        #endregion

        /// <summary>
        /// draw current page with header
        /// </summary>
        /// <returns>name of the next page that will be loaded</returns>
        public string drawPage()
        {
            GUI.skin = guiSkin;
            GUI.Label(new Rect(2 * borderX + columnX * 1.3f, 2 * borderY + columnY * 0, columnX, columnY), "Morpheus reloaded", getHeaderStyle());

            return draw();
        }

        /// <summary>
        /// draw current page
        /// </summary>
        /// <returns></returns>
        protected abstract string draw();

        /// <summary>
        /// reset page to initialize state
        /// </summary>
        public abstract void resetClass();
        
        /// <summary>
        /// header Style
        /// </summary>
        /// <returns></returns>
        public static GUIStyle getHeaderStyle()
        {
            GUIStyle header = new GUIStyle();
            header.fontSize = 50;
            header.fontStyle = FontStyle.Bold;
            // header.normal.textColor = Color.magenta;
            return header;
        }

        /// <summary>
        /// text Style
        /// </summary>
        /// <returns></returns>
        public static GUIStyle getTextStyle()
        {
            GUIStyle text = new GUIStyle();
            text.fontSize = 34;
            text.fontStyle = FontStyle.Normal;
            // text.normal.textColor = Color.magenta;
            return text;
        }
    }
}