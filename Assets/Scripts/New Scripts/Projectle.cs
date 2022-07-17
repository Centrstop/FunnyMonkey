using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public enum DirectionProjectle
    {
        Left,
        Right
    }
    public class Projectle : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The direction of movement of the projectile.")]
        public DirectionProjectle directionProjectle = DirectionProjectle.Left;
        [Tooltip("The distance this projectile will move each second.")]
        public float projectileSpeed = 3.0f;
        [Tooltip("Delete outside radius")]
        public bool deleteOutRadius = false;
        [Tooltip("The radius this projectile will move"), Range(0.0f, 100.0f)]
        public float projectileRadius = 30.0f;
        [Header("Effect Settings")]
        [Tooltip("Prefab Delete Effect")]
        [SerializeField] protected GameObject _deleteEffect = null;

        private Vector3 _startPosition;
        private float direction = 1.0f;

        // Use this for initialization
        void Start()
        {
            _startPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (deleteOutRadius)
            {
                if((Mathf.Abs(transform.position.x - _startPosition.x)>= projectileRadius))
                {
                    Instantiate(_deleteEffect, transform.position, transform.rotation, null);
                    Destroy(gameObject);
                }
            }
            direction = (directionProjectle == DirectionProjectle.Left) ? 1.0f : -1.0f;
            transform.position = transform.position + Vector3.left * projectileSpeed * Time.deltaTime * direction;
        }
    }
}