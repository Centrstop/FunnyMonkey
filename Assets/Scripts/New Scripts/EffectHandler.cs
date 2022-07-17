using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class EffectHandler : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The effect to create")]
        [SerializeField] private GameObject _prefabEffect = null;
        [Tooltip("The transform holder")]
        [SerializeField] private Transform _parent = null;
        private GameObject _instantiatedEffect = null;
        private Vector3 _position = Vector3.zero;
        public Transform parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public GameObject effect
        {
            get
            {
                if (_instantiatedEffect != null)
                {
                    _instantiatedEffect.SetActive(false);
                }
                else
                {
                    if (_prefabEffect != null)
                    {
                        _position = (_parent != null) ? _parent.position : transform.position;
                        _instantiatedEffect = Instantiate(_prefabEffect, _position, Quaternion.identity, parent);
                        _instantiatedEffect.SetActive(false);
                    }
                }
                return _instantiatedEffect;
            }
        }

        public void ShowEffect()
        {
            effect?.SetActive(true);
        }
    }
}