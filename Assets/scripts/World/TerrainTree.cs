using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// manages the burning of preordained trees in the background (on terrain)
    /// </summary>
    public class TerrainTree : MonoBehaviour
    {
        /// <summary>
        /// tree objects prefabs to be assigned in unity
        /// </summary>
        public GameObject fire4TreePrefab;
        /// <summary>
        /// tree objects prefabs to be assigned in unity
        /// </summary>
        public GameObject fire4TreeInitPrefab;

        private Transform trans;
        private GameObject fire1 = null;
        private GameObject fire2 = null;

        private ParticleSystem particle1;
        private ParticleSystem particle2;

        private float timeToFall = 4;
        private bool fall = false;
        // Use this for initialization
        void Start()
        {
            trans = transform;
        }

        // Update is called once per frame
        void Update()
        {
            // particle destroy logic
            if (fire1 != null && fire2 != null)
            {
                if ((!particle1.IsAlive()) && (!particle2.IsAlive()))
                {
                    Destroy(fire1);
                    Destroy(fire2);
                    Destroy(gameObject);
                }
            }

            if (fall)
            {
                trans.Rotate(new Vector3(0, 0, -(90/timeToFall) * Time.deltaTime), Space.World);
                if (trans.rotation.z < -0.7f)
                {
                    fall = false;
                }
            }
        }

        void OnTriggerEnter(Collider c)
        {
            switch (c.gameObject.tag)
            {
                case "Lava":
                    if (fire1 == null && fire2 == null)
                    {
                        fall = true;
                        trans.parent = null;

                        fire1 = (GameObject)Instantiate(fire4TreeInitPrefab, trans.position, Quaternion.Euler(new Vector3(270, 0, 0)));
                        fire2 = (GameObject)Instantiate(fire4TreePrefab, trans.position, Quaternion.Euler(new Vector3(270, 0, 0)));

                        particle1 = (ParticleSystem)fire1.GetComponent("ParticleSystem");
                        particle2 = (ParticleSystem)fire2.GetComponent("ParticleSystem");
                    }

                    break;
            }
        }
    }
}