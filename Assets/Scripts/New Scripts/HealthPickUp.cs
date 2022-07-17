using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class HealthPickUp : PickUpBase
    {
        [Header("Settings")]
        [Tooltip("How many bonus health will be added")]
        [SerializeField] private int _bonusHealth = 1;
        [Tooltip("The effect to create when health is collected")]
        [SerializeField] private EffectHandler _effect = null;

        protected override void DoOnPickUp(GameObject collisionGameObject)
        {
            if (collisionGameObject.TryGetComponent<CharacterHealth>(out CharacterHealth health))
            {
                if (health.team == TeamType.Player)
                {
                    health.ReceiveHealing(_bonusHealth);
                    _effect?.ShowEffect();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}