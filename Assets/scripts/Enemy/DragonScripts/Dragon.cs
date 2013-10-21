using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy.DragonScripts
{
    /// <summary>
    /// This class controls the flying dragon, with throws rocks at the player
    /// </summary>
    public class Dragon : MonoBehaviour
    {
        private GameObject rock; // rockPrefab
        private GameObject currentRock; // rock holding
        private GameVariables var;
        private GameObject lava;

        private int height = 100; // height the dragon flys away, if its higher he respawns later
        private float speed = 15; // dragon speed
        private bool droppedDown = false; // if the rock dropps down
        private bool isCollided = false; // when dragon collides with anything
        private Animator ani;

        private string currentScene;

        private Transform trans;

        // Use this for initialization
        void Start()
        {
            currentScene = Application.loadedLevelName;

            rock = (GameObject)Resources.Load("Prefabs/LevelObjects/Rock");
            trans = transform;

            ani = GetComponent<Animator>();
            ani.SetBool("isFlying", true);

            lava = GameObject.Find("Lava");
            var = GameController.gameVariables;
            resetPos();
        }
        /// <summary>
        /// creates new rock at dragons current position
        /// </summary>
        private void getRock()
        {
            currentRock = (GameObject)Instantiate(rock, trans.position, Quaternion.identity);
            currentRock.transform.Translate(-1f, 0, 0);
            currentRock.transform.parent = trans;
        }

        /// <summary>
        /// throws rock, when dragon reaches the position of the player
        /// </summary>
        private void throwRock()
        {
            droppedDown = true;
            currentRock.transform.parent = null;
            if (currentRock.GetComponent<Rigidbody>() == null)
            {
                Rigidbody body = currentRock.AddComponent<Rigidbody>();
                if (body != null)
                {
                    body.AddForce(new Vector3(5, 0, 0), ForceMode.VelocityChange);
                    body.constraints = RigidbodyConstraints.FreezePositionZ;
                    body.mass = 500;
                }
            }
        }

        /// <summary>
        /// resets dragons position above the lava after throwing a rock
        /// </summary>
        private void resetPos()
        {
            trans.position = lava.transform.position;
            if (currentScene == "Part1_Level1")
            {
                trans.transform.Translate(0, 1, 0);
            }
            else
            {
                trans.transform.Translate(0, 15, 0);
            }

            droppedDown = false;
            isCollided = false;
            getRock();
        }

        // Update is called once per frame
        void Update()
        {
            // reset dragon
            if (trans.position.y > height)
            {
                resetPos();
            }
            // flies away
            if (droppedDown && !isCollided)
            {
                trans.transform.Translate(Time.deltaTime * speed, Time.deltaTime * speed, 0, Space.World);
            }
            // flies away
            if (isCollided)
            {
                trans.transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
            }
            else
            {
                // flies to player
                if (trans.position.x < var.playerPosition.x)
                {
                    trans.transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
                }

                if (trans.position.x >= var.playerPosition.x)
                {
                    throwRock();
                }
            }
        }
        /// <summary>
        /// dragon flies away after colliding with objects
        /// </summary>
        /// <param name="collider"></param>
        void OnCollisionEnter(Collision collider)
        {
            if (collider.gameObject.tag != "Environment")
            {
                isCollided = true;
                throwRock();
            }
        }
    }
}