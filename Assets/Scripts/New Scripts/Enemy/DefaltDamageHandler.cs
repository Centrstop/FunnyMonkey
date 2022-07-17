using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class DefaltDamageHandler : BaseDamageHandler
    {
        [Header("Damage Settings")]
        [Tooltip("Damage object script")]
        [SerializeField] private Damage _damageObject = null;
        public override void Activate()
        {
            if (_damageObject != null)
            {
                _damageObject.dealDamageOnCollision = false;
                _damageObject.dealDamageOnTriggerEnter = true;
                _damageObject.dealDamageOnTriggerStay = true;
            }
        }

        public override void Deactivate()
        {
            if (_damageObject != null)
            {
                _damageObject.dealDamageOnCollision = false;
                _damageObject.dealDamageOnTriggerEnter = false;
                _damageObject.dealDamageOnTriggerStay = false;
            }
        }
    }
}