using UnityEngine;
using System.Collections;
namespace Assets.scripts.Player
{
    /// <summary>
    /// This Class manages the hitting behavior of the (human) player character. 
    /// The prefab "Hit" is what actually collides with the enemies.
    /// </summary>
    public class Hit : MonoBehaviour
    {
        private int force = 10;
        private float stay = 0.2f;

        // Update is called once per frame
        void Update()
        {
            stay -= Time.deltaTime;
            if (stay <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter(Collider otherObject)
        {
            if (otherObject.gameObject.tag == "Enemy")
            {
                otherObject.rigidbody.AddForce(force, force / 2, 0, ForceMode.Impulse);
            }
        }
    }
}