using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform followingTarget = null;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVerticalParallax = true;
    private Vector3 _previousPosition;
    private Vector3 _delta;

    // Start is called before the first frame update
    void Start()
    {
        if (followingTarget != null)
        {
            _previousPosition = followingTarget.position;
            _delta = Vector3.zero;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followingTarget != null)
        {
            _delta = followingTarget.position - _previousPosition;
            if (disableVerticalParallax)
            {
                _delta.y = 0;
            }
            if(_delta.magnitude > 0){
                _previousPosition = followingTarget.position;
                transform.position += _delta * parallaxStrength;
            }
        }
    }
}
