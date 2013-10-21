using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// it's a script for bushes
    /// </summary>
    public class Bush : MonoBehaviour
    {
        // pointer to GameObject.transform
        private bool isBurning = true;
        private ParticleSystem parti;
        private GameObject particleFire;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            GameObject particleFire = transform.FindChild("burningObjectsParticles").gameObject;
            parti = particleFire.GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            if (isBurning && parti.isStopped)
            {
                parti.Play();
            }
            else if (!isBurning && parti.isPlaying)
            {
                parti.Stop();
            }
        }

        // special cases on particle collision
        void OnParticleCollision(GameObject collidingGameObject)
        {
            switch (collidingGameObject.tag)
            {
                case "Fire":
                case "Lava":
                    isBurning = true;
                    break;
            }
        }

        // special cases on collision
        void OnCollisionEnter(Collision collisionCollider)
        {
            switch (collisionCollider.gameObject.tag)
            {
                case "Fire":
                case "Lava":
                    isBurning = true;
                    break;
            }
        }
    }
}