using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public abstract class PickUpBase : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            DoOnPickUp(collision.gameObject);
        }

        protected abstract void DoOnPickUp(GameObject collisionGameObject);
    }
}