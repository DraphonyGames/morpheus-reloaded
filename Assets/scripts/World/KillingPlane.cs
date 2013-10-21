using UnityEngine;
using System.Collections;
using Assets.scripts;

namespace Assets.scripts.World
{
    /// <summary>
    /// destroys every GameObject on collision
    /// </summary>
    public class KillingPlane : MonoBehaviour
    {
        // Lava fountain prefab
        private GameObject lavaFountain;
        // Lava fountain prefab with low collison
        private GameObject lavafountainWithLowCollision;

        private GameVariables variables;
        // pointer 
        private Transform trans;

        // if the player is closer then this distance, an other fountain will be spawned
        private float distanceWithHighCollsion = 20;

        // Use this for initialization
        void Start()
        {
            trans = transform;
            variables = GameController.gameVariables;

            lavaFountain = (GameObject)Resources.Load("Prefabs/LevelObjects/Lavafountain");
            lavafountainWithLowCollision = (GameObject)Resources.Load("Prefabs/LevelObjects/LavafountainWithLowCollision");
        }

        // special cases on collision
        void OnCollisionEnter(Collision collisionCollider)
        {
            switch (collisionCollider.gameObject.tag)
            {
                case "HitEnemy":
                    Destroy(collisionCollider.gameObject);
                    break;
                case "Rock":
                    collisionCollider.gameObject.rigidbody.AddForce(new Vector3(0, 30, 0), ForceMode.VelocityChange);
                    collisionCollider.gameObject.transform.gameObject.tag = "LavaRock";
                    instantiate(collisionCollider);
                    break;
                default:
                    instantiate(collisionCollider);
                    Destroy(collisionCollider.gameObject);
                    break;
            }
        }

        // instantiate fountain, with high or low collision 
        private void instantiate(Collision collisionCollider)
        {
            if (distanceWithHighCollsion < Mathf.Abs(trans.position.x - variables.playerPosition.x))
            {
                Instantiate(lavaFountain, collisionCollider.gameObject.transform.position, Quaternion.AngleAxis(90f, Vector3.left));
            }
            else
            {
                Instantiate(lavafountainWithLowCollision, collisionCollider.gameObject.transform.position, Quaternion.AngleAxis(90f, Vector3.left));
            }
        }
    }
}