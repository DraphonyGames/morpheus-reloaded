using UnityEngine;
using System.Collections;
namespace Assets.scripts.Player
{
    /// <summary>
    /// Class obsolete? Deactivated everything to see if errors do pop up.
    /// </summary>
    public class Cloud : MonoBehaviour
    {
        private GameObject player;
        private Transform trans;
        System.DateTime time;
            // Use this for initialization
        ////void Start()
        ////{
        ////    player = GameObject.Find("Player");
        ////    trans = transform;
        ////    time = System.DateTime.Now;
        ////}

        ////// Update is called once per frame
        ////void Update()
        ////{
        ////    trans.position = player.transform.position;
        ////    if (System.DateTime.Now.Millisecond - time.Millisecond > 900)
        ////    {
        ////        Destroy(gameObject);
        ////    }
        ////}
    }
}