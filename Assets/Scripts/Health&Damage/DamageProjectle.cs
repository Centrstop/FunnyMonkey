using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Health_Damage
{
    public class DamageProjectle : Damage
    {
        [Header("Effect Settings")]
        [Tooltip("Prefab Hit Effect")]
        [SerializeField] protected GameObject _hitEffect = null;

        protected override void DealDamage(GameObject collisionGameObject)
        {
            CharacterHealth collidedHealth = collisionGameObject.GetComponent<CharacterHealth>();
            if (collidedHealth != null)
            {
                if ((collidedHealth.team != team) &&
                    (collidedHealth.team != TeamType.Neutral) &&
                    (team != TeamType.Neutral))
                {
                    collidedHealth.TakeDamage(damageAmount);
                    if (destroyAfterDamage)
                    {
                        if (team != TeamType.Player)
                        {
                            Instantiate(_hitEffect, transform.position, transform.rotation, null);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
}