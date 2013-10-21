using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy.DragonScripts
{
    /// <summary>
    /// rock without rigid body, can be set on fire
    /// </summary>
    public class DestoryAbleMeteor : MonoBehaviour
    {
        // optimize: pointer for GameObject.transform
        private Transform trans;
        // Use this for initialization
        void Start()
        {
            trans = transform;
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// to set object on fire
        /// </summary>
        private void reFire()
        {
            GameObject particleFire = trans.FindChild("ParticleFire").gameObject;
            ParticleSystem parti = particleFire.GetComponent<ParticleSystem>();
            parti.Play();
        }

        /// <summary>
        /// special cases on particle collision
        /// </summary>
        /// <param name="g"></param>
        void OnParticleCollision(GameObject collidingGameObject)
        {
            switch (collidingGameObject.tag)
            {
                case "Fire":
                case "Lava":
                    reFire();
                    break;
            }
        }

        // special cases on collision
        void OnCollisionEnter(Collision collisionCollider)
        {
            switch (collisionCollider.gameObject.tag)
            {
                case "Environment":
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}