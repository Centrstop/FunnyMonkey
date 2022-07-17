using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class MovementPath : MonoBehaviour
    {
        public enum PathType
        {
            linear,
            loop
        }

        [Header("Path Settings")]
        [Tooltip("The type of movement path")]
        [SerializeField] private PathType _pathType = PathType.linear;
        [Tooltip("The points of movement path")]
        [SerializeField] private List<Transform> _pathPoints = null;

        private List<Vector3> _pathPointsPosition = null;
        // Use this for initialization
        /*void Awake()
        {
            if(_pathPoints == null)
            {
                _pathPoints = new List<Transform>();
            }
            FindAllPoints();
        }*/

        private void FindAllPoints()
        {
            _pathPoints.Clear();
            for(int i = 0; i < transform.childCount; ++i)
            {
                _pathPoints.Add(transform.GetChild(i));
            }
        }

        private void GetAllPointPosition()
        {
            if(_pathPoints!=null && _pathPoints.Count > 0)
            {
                _pathPointsPosition.Clear();
                foreach(var point in _pathPoints)
                {
                    _pathPointsPosition.Add(point.position);
                }
            }
        }

        private void InitPathPoint()
        {
            if (_pathPoints == null)
            {
                _pathPoints = new List<Transform>();
            }
            FindAllPoints();
        }
        
        private void InitPathPointPosition()
        {
            if(_pathPointsPosition == null)
            {
                _pathPointsPosition = new List<Vector3>();
            }
            GetAllPointPosition();
        }

        public IEnumerator<Vector3> GetNextPoint()
        {
            InitPathPoint();
            InitPathPointPosition();
            if ((_pathPointsPosition != null) && (_pathPointsPosition.Count > 0))
            {
                int indexPoint = 0;
                int moveDirection = 1;
                while (true)
                {
                    yield return _pathPointsPosition[indexPoint];
                    indexPoint += moveDirection;
                    if ((indexPoint < 0) || (indexPoint + 1 > _pathPoints.Count))
                    {
                        if (_pathType == PathType.linear)
                        {
                            indexPoint = (moveDirection > 0) ? (_pathPoints.Count - 2) : 0;
                            moveDirection = (moveDirection > 0) ? -1 : 1;
                        }
                        else
                        {
                            indexPoint = 0;
                            moveDirection = 1;
                        }
                    }
                }
            }
            else
            {
                yield break;
            }
        }
    }

}