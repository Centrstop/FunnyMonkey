using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [ExecuteAlways]
    public class DrawMovementPath : MonoBehaviour
    {

    private List<Transform> _pathPoints = null;
#if UNITY_EDITOR

        private void Update()
        {
            if (!Application.IsPlaying(this))
            {
                if (_pathPoints == null)
                {
                    _pathPoints = new List<Transform>();
                }
                FindAllPoints();
            }
        }
        private void FindAllPoints()
        {
            _pathPoints.Clear();
            for (int i = 0; i < transform.childCount; ++i)
            {
                _pathPoints.Add(transform.GetChild(i));
            }
        }

        public void OnDrawGizmos()
        {
            if (_pathPoints != null)
            {
                if (_pathPoints.Count > 1)
                {
                    for (int i = 0; i + 1 < _pathPoints.Count; ++i)
                    {
                        Gizmos.DrawLine(_pathPoints[i].position, _pathPoints[i + 1].position);
                    }
                }
            }
        }


#endif
    }
}