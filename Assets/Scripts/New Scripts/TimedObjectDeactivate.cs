using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class which destroys it's gameobject after a certain amount of time
/// </summary>
public class TimedObjectDeactivate : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The lifetime of this gameobject")]
    [SerializeField] private float _lifetime = 5.0f;

    // The amount of time this gameobject has already existed in play mode
    private float timeAlive = 0.0f;

    void Start()
    {
        timeAlive = 0.0f;
    }

    void OnEnable()
    {
        timeAlive = 0.0f;
    }

    void Update()
    {
        if (timeAlive > _lifetime)
        {
            timeAlive = 0.0f;
            gameObject.SetActive(false);
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
    }
}
