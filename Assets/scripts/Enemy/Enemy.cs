using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy
{
    /// <summary>
    /// 
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        float currentspeed = 4;
        /// <summary>
        /// direction 1 for right -1 for left
        /// </summary>
        private int direction = 1;

        /// <summary>
        /// Enemy will move "count"frames in one direction
        /// </summary>
        private int directionCount = 60;
        private bool isGrounded = true;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            float amtToMove = currentspeed * Time.deltaTime;

            // changing direction after a certain count
            if ((this.directionCount == -1) && (isGrounded))
            {
                this.directionCount = Random.Range(30, 100);
                this.direction = 1;
            }
            else if ((this.directionCount == 1) && (isGrounded))
            {
                this.directionCount = Random.Range(-30, -100);
                this.direction = -1;
            }

            else if ((this.direction == 1) && (isGrounded))
            {
                this.directionCount--;
                transform.Translate(Vector3.right * amtToMove);
            }
            else if ((this.direction == -1) && (isGrounded))
            {
                this.directionCount++;
                transform.Translate(Vector3.left * amtToMove);
            }
        }
        /// <summary>
        /// Different collision detections.
        /// One for KIBorders and one for Level borders
        /// </summary>
        /// <param name="otherObject"></param>
        void OnCollisionEnter(Collision otherObject)
        {
            if (otherObject.gameObject.tag == "KillingPlane")
            {
                // if enemy collides with level borders he will be destroyed
                Destroy(this.gameObject);
            }

            if ((otherObject.gameObject.tag == "Enemy") && (this.direction == -1) && (isGrounded))
            {
                this.direction = 1;
                this.directionCount = Random.Range(30, 100);
            }

            if ((otherObject.gameObject.tag == "Enemy") && (this.direction == 1) && (isGrounded))
            {
                this.direction = -1;
                this.directionCount = Random.Range(-30, -100);
            }

            if (otherObject.gameObject.tag == "Environment")
            {
                this.isGrounded = true;
            }
        }

        void OnTriggerEnter(Collider otherObject)
        {
            if (((otherObject.gameObject.tag == "AIBorderRight") && (this.direction == 1) && (isGrounded)))
            {
                this.direction = -1;
                this.directionCount = Random.Range(-30, -100);
            }

            if (((otherObject.gameObject.tag == "AIBorderLeft") && (this.direction == -1) && (isGrounded)))
            {
                this.direction = 1;
                this.directionCount = Random.Range(30, 100);
            }
        }

        void OnCollisionExit(Collision otherObject)
        {
            if (otherObject.gameObject.tag == "Environment")
            {
                this.isGrounded = false;
            }
        }
    }
}