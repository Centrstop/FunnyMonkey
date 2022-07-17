using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public abstract class MovementHandler : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Transform movement object")]
        [SerializeField] protected Transform _movementObject = null;
        public abstract bool StartMovement();

        public abstract bool StopMovement();

    }
}