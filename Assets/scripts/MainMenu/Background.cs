using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.scripts.MainMenu
{
    /// <summary>
    /// Set background images in the menu
    /// </summary>
    public class Background : MonoBehaviour
    {
        private string foldername = "MenuBackground/";
        private bool makeTexture = true;
        private List<Texture2D> pictures = new List<Texture2D>();
        private bool loop = true;
        private int counter = 0;
        private bool film = true;
        private float pictureRateInSeconds = 1;
        private float nextPic = 0;

        // Use this for initialization
        void Start()
        {
            if (film)
            {
                pictureRateInSeconds = 4f;
            }

            Object[] textures = Resources.LoadAll(foldername);
            for (int i = 0; i < textures.Length; i++)
            {
                pictures.Add((Texture2D)textures[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > nextPic)
            {
                nextPic = Time.time + pictureRateInSeconds;
                counter += 1;
                if (counter >= pictures.Count)
                {
                    if (loop)
                    {
                        counter = 0;
                    }
                }
                else if (makeTexture)
                {
                    renderer.material.mainTexture = pictures[counter];
                }
            }
        }
    }
}
