using UnityEngine;
using System.Collections;
namespace Assets.scripts
{
    /// <summary>
    /// This class manages the behavior of the obstacle moss wall.
    /// </summary>
    public class MossWall : MonoBehaviour
    {
        /// <summary>
        /// Graphical destruction effect.
        /// </summary>
        public GameObject explosionPrefab;
        // Use this for initialization
        void Start()
        {
            GameObject fire = transform.FindChild("MossFire").gameObject;
            ParticleSystem particles = fire.GetComponent<ParticleSystem>();
            particles.loop = false;
        }

        // Update is called once per frame
        void Update()
        {
        }

        void OnParticleCollision(GameObject collidingGameObject)
        {
            switch (collidingGameObject.tag)
            {
                case "Fire":
                    GameObject fire = transform.FindChild("MossFire").gameObject;
                    ParticleSystem particles = fire.GetComponent<ParticleSystem>();
                    particles.Play();
                    particles.loop = true;
                    StartCoroutine("onFire");
                    break;
            }
        }

        IEnumerator onFire()
        {
            GameObject fire = transform.FindChild("MossFire").gameObject;
            ParticleSystem particles = fire.GetComponent<ParticleSystem>();
            particles.loop = true;
            yield return new WaitForSeconds(3);
            Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}