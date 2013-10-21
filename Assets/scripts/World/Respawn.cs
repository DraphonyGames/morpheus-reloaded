using UnityEngine;
using System.Collections;
namespace Assets.scripts.World
{
    /// <summary>
    /// class manages spawning behavior of the player
    /// </summary>
    public class Respawn : MonoBehaviour
    {
        private Animator animator;

        // Use this for initialization
        void Start()
        {
            if (GetComponent<Animator>() != null)
            {
                animator = GetComponent<Animator>();
                animator.SetBool("isRaised", false);
            }
            // Physics.IgnoreCollision(this.collider, Player.collider);
        }
    }
}