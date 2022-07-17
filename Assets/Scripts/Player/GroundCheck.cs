using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.New_Scripts;
using UnityEngine;

/// <summary>
/// Component on gameobjects with colliders which determines if there is
/// a collider overlapping them which is on a specific layer.
/// Used to check for ground.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The layers which are considered \"Ground\".")]
    [SerializeField] private LayerMask _groundLayers = new LayerMask();
    
    private Collider2D _groundCheckCollider = null;

    [Header("Effect Settings")]
    [Tooltip("The effect to create when landing")]
    [SerializeField] private EffectHandler _landingEffect;


    // Whether or not the player was grounded last check
    //[HideInInspector]
    private bool groundedLastCheck = false;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first update
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    private void Start()
    {
        // When this component starts up, ensure that the collider is not null, if possible
        GetCollider();
    }

    /// <summary>
    /// Description:
    /// Attempts to setup the collider to be used with ground checking,
    /// if one is not already set up.
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    private void GetCollider()
    {
        if (_groundCheckCollider == null)
        {
            _groundCheckCollider = gameObject.GetComponent<Collider2D>();

        }
    }
    
    public bool CheckGrounded()
    {

        GetCollider();

        // Find the colliders that overlap this one
        Collider2D[] overlaps = new Collider2D[5];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = _groundLayers;
        _groundCheckCollider.OverlapCollider(contactFilter, overlaps);

        foreach (Collider2D overlapCollider in overlaps)
        {
            if (overlapCollider != null)
            {
                int match = contactFilter.layerMask.value & (int)Mathf.Pow(2, overlapCollider.gameObject.layer);
                if (match > 0)
                {
                    if (!groundedLastCheck)
                    {
                        if(_landingEffect != null)
                        {
                            _landingEffect.ShowEffect();
                        }
                    }
                    groundedLastCheck = true;
                    return true;
                }
            }
        }
        groundedLastCheck = false;
        return false;
    }
}
