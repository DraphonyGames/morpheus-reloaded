using UnityEngine;
using System.Collections;
namespace Assets.scripts
{
    /// <summary>
    /// Links the class GameVariables with individual scenes. Makes access of the "scriptable Object" class GameVariables possible. 
    /// </summary>
    public class GameController : MonoBehaviour
    {
        /// <summary>
        /// access to GameVariables
        /// </summary>
        static public GameVariables gameVariables;

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Functions
        void Awake()
        {
            // is gameVariables already created? if not create instance
            if (gameVariables == null)
            {
                gameVariables = (GameVariables)ScriptableObject.CreateInstance(typeof(GameVariables));
            }
        }
        #endregion
    }
}