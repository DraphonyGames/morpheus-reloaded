using UnityEngine;
using System.Collections;
namespace Assets.scripts.Enemy.DragonScripts
{
    /// <summary>
    /// fire particles of burning objects
    /// </summary>
    public class RockParticle : MonoBehaviour
    {
        ParticleSystem parti;
        // Use this for initialization
        void Start()
        {
            parti = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        // special cases on particle collision
        void OnParticleCollision(GameObject collidingGameObject)
        {
            switch (collidingGameObject.tag)
            {
                case "Environment":
                    parti.loop = false;
                    break;
            }
        }
    }
}