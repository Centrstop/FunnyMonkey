using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class FinishPickUp : PickUpBase
    {
        protected override void DoOnPickUp(GameObject collisionGameObject)
        {
            if (collisionGameObject.TryGetComponent<Player>(out Player player))
            {
                player.LevelCompleted();
                gameObject.SetActive(false);
            }
        }
    }
}