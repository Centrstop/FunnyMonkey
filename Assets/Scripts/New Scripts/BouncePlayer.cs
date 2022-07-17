using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BouncePlayer : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The player script object")]
        [SerializeField] private Player _player = null;
        [Tooltip("Whether or not to apply bounce when triggers collide")]
        public bool bouncePlayerOnTriggerEnter = false;
        [Tooltip("Whether or not to apply damage when triggers stay, for damage over time")]
        public bool bouncePlayerOnTriggerStay = false;
        [Tooltip("Whether or not to apply damage on non-trigger collider collisions")]
        public bool bouncePlayerOnCollision = false;

        /// <summary>
        /// Description:
        /// Standard unity function called whenever a Collider2D enters any attached 2D trigger collider
        /// Input:
        /// Collider2D collision
        /// Return:
        /// void (no return)
        /// </summary>
        /// <param name="collision">The collider that entered the trigger<</param>
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (bouncePlayerOnTriggerEnter)
            {
                OnBouncePlayer(collision.gameObject);
            }
        }

        /// <summary>
        /// Description:
        /// Standard Unity function called every frame a Collider2D stays in any attached 2D trigger collider
        /// Input:
        /// Collider2D collision
        /// Return:
        /// void (no return)
        /// </summary>
        /// <param name="collision">The collider that is still in the trigger</param>
        protected void OnTriggerStay2D(Collider2D collision)
        {
            if (bouncePlayerOnTriggerStay)
            {
                OnBouncePlayer(collision.gameObject);
            }
        }

        /// <summary>
        /// Description:
        /// Standard Unity function called when a Collider2D hits another Collider2D (non-triggers)
        /// Input:
        /// Collision2D collision
        /// Return:
        /// void (no return)
        /// </summary>
        /// <param name="collision">The Collider2D that has hit this Collider2D</param>
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (bouncePlayerOnCollision)
            {
                OnBouncePlayer(collision.gameObject);
            }
        }


        private void OnBouncePlayer(GameObject collisionGameObject)
        {
            if(_player != null)
            {
                CharacterHealth collidedHealth = collisionGameObject.GetComponent<CharacterHealth>();
                if(collidedHealth != null)
                {
                    _player.Bounce();
                }
            }
        }
    }
}