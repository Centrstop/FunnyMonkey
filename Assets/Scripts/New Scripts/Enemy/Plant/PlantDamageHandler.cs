using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    
    public class PlantDamageHandler : BaseDamageHandler
    {
        [Header("Component References")]
        [Tooltip("The projectile to be fired.")]
        [SerializeField] private GameObject _projectilePrefab = null;
        [Tooltip("The transform in the heirarchy which holds projectiles if any")]
        [SerializeField] private Transform _projectileHolder = null;
        [Tooltip("The Direction of movement of the projectile.")]
        [SerializeField] private DirectionProjectle _directionProjectle = DirectionProjectle.Left;
        [Tooltip("The distance this projectile will move each second.")]
        [SerializeField] private float _projectileSpeed = 3.0f;
        [Tooltip("Delete outside radius")]
        [SerializeField] private bool _deleteOutRadius = false;
        [Tooltip("The radius this projectile will move"), Range(0.0f, 100.0f)]
        [SerializeField] private float _projectileRadius = 30.0f;

        public override void Activate()
        {
            SpawnProjectle();
        }

        public override void Deactivate()
        {
            return;
        }


        private void SpawnProjectle()
        {
            if (_projectilePrefab != null)
            {
                // Create the projectile
                GameObject projectileGameObject = Instantiate(_projectilePrefab, transform.position, transform.rotation, null);
                Projectle projectle = projectileGameObject.GetComponent<Projectle>();
                projectle.directionProjectle = _directionProjectle;
                projectle.deleteOutRadius = _deleteOutRadius;
                projectle.projectileSpeed = _projectileSpeed;
                projectle.projectileRadius = _projectileRadius;
                // Keep the heirarchy organized
                if (_projectileHolder != null)
                {
                    projectileGameObject.transform.SetParent(_projectileHolder);
                }
            }
        }
    }
}