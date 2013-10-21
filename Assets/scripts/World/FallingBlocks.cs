using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// manages behavior of "trap" blocks - after being touched by the player they start to spin and fall down
    /// </summary>
    public class FallingBlocks : MonoBehaviour
    {
        // whether the player has already touched the block
        private bool touchedByPlayer = false;

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 1)
            {
                if (touchedByPlayer)
                {
                    this.gameObject.transform.Rotate(0, 5, 0);
                }
            }
        }

        void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.tag == "Player")
            {
                StartCoroutine("fall");
            }
        }

        IEnumerator fall()
        {
            touchedByPlayer = true;
            yield return new WaitForSeconds(0.5f);
            if (GetComponent("Rigidbody") == null)
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}