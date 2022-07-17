using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class CheckPointPickUp : PickUpBase
    {
        [Header("Settings")]
        [Tooltip("The animator component that controls the check point animations")]
        [SerializeField] private Animator _animator = null;
        [Tooltip("The effect to create when check point is collected")]
        [SerializeField] private EffectHandler _effect = null;
        private void Start()
        {
            SetCheckedState(false);
        }

        private void SetCheckedState(bool isChecked)
        {
            if(_animator != null)
            {
                _animator.SetBool("isChecked", isChecked);
            }
        }

        protected override void DoOnPickUp(GameObject collisionGameObject)
        {
            if (collisionGameObject.TryGetComponent<Player>(out Player player))
            {
                player.SavePosition();
                SetCheckedState(true);
                _effect?.ShowEffect();
                gameObject.SetActive(false);
            }
        }
    }
}