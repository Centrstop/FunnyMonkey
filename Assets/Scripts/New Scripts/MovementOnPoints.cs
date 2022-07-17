using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class MovementOnPoints : MovementHandler
    {

        [Tooltip("Movement points")]
        [SerializeField] private MovementPath _movementPoints = null;

        [Tooltip("Movement speed")]
        [SerializeField] private float _moveSpeed = 1.0f;

        private bool _flagStartMovement = false;
        private Vector3 _homePosition;
        private Vector3 _targetPosition;
        [SerializeField] private IEnumerator<Vector3> _pointInPath;
        public override bool StartMovement()
        {
            bool toNextPoint = MoveToPoint(_targetPosition);
            if (toNextPoint)
            {
                _targetPosition = GetTargetPosition();
            }          
            return toNextPoint;
        }

        public override bool StopMovement()
        {
            return MoveToPoint(_homePosition);
        }

        private bool MoveToPoint(Vector3 targetPoint)
        {
            bool flagResult = CheckDistance(_movementObject.position, targetPoint, 0.01f);
            if (!flagResult)
            {
                Move(targetPoint);
            }
            return flagResult;
        }

        // Use this for initialization
        void Start()
        {
            _homePosition = (_movementObject != null) ? _movementObject.position : transform.position;
            if(_movementPoints == null)
            {
                _targetPosition = _homePosition;
            }
            else
            {
                _pointInPath = _movementPoints.GetNextPoint();
                _targetPosition = GetTargetPosition();
            }
        }

        private Vector3 GetTargetPosition()
        {
            _pointInPath.MoveNext();
            Vector3 result = (_pointInPath.Current != null) ? _pointInPath.Current : _homePosition;
            return result;
        }

        private void Move(Vector3 target)
        {
            _movementObject.position = Vector3.MoveTowards(_movementObject.position, target, Time.deltaTime * _moveSpeed);
        }

        private bool CheckDistance(Vector3 objPosition, Vector3 targetPosition, float delta)
        {
            var distanceSqure = (objPosition - targetPosition).sqrMagnitude;
            return distanceSqure < delta;
        }
    }
}