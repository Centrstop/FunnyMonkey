using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class LivesPickUp : PickUpBase
    {
        [Header("Settings")]
        [Tooltip("How many bonus lives will be added")]
        [SerializeField] private int _bonusLives = 1;
        [Tooltip("The effect to create when lives is collected")]
        [SerializeField] private EffectHandler _effect = null;
        protected override void DoOnPickUp(GameObject collisionGameObject)
        {
            if (collisionGameObject.TryGetComponent<CharacterHealth>(out CharacterHealth health))
            {
                if(health.team == TeamType.Player)
                {
                    health.AddLives(_bonusLives);
                    _effect?.ShowEffect();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}