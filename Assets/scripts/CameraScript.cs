using UnityEngine;
using System.Collections;
using System;

namespace Assets.scripts
{
    /// <summary>
    /// Class manages camera behavior (zoom, position)
    /// </summary>
    public class CameraScript : MonoBehaviour
    {
        // factor for screensize
        private float screenScale = 0.5f;
        // startposition of camera on y axis
        private float distanceY = -5;
        // startposition of camera on axis
        private float distanceZ = 15;
        // sets the minimal distance between player and camera
        private float min = 13;
        // sets the maximal distance between player and camera
        /// <summary>
        /// sets the maximal distance between player and camera
        /// </summary>
        private float max = 25;

        private Transform trans;
        private GameObject lava;
        GameVariables gameVariables;

        // Use this for initialization
        void Start()
        {
            gameVariables = GameController.gameVariables;
            lava = GameObject.Find("Lava");
            trans = transform;
        }

        /// <summary>
        /// sets position of camera while playing; zooms in when lava arrives player
        /// </summary>
        void LateUpdate()
        {
            float abs = Mathf.Abs(gameVariables.playerPosition.x - lava.transform.position.x) - distanceZ;
            float[] list = new float[] { min, abs, max };
            Array.Sort(list);

            float y = 0;
            if (abs < min - 4)
            {
                y = gameVariables.playerPosition.y;
            }
            else
            {
                y = distanceY + list[1] * screenScale + gameVariables.playerPosition.y;
            }

            float z = gameVariables.playerPosition.z - list[1];

            trans.position = new Vector3(gameVariables.playerPosition.x, y, z);
        }
    }
}