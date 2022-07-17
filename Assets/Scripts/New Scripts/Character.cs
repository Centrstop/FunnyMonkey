using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Support scripts")]
    [Tooltip("The health scripts")]
    [SerializeField] protected CharacterHealth _healthCharacter;
    
    protected virtual void OnDieEvent(int remainingLives, int remainingHealth)
    {
        Debug.Log(string.Format("Remaining lives : {0}", remainingLives));
    }

    protected virtual void OnTakeDamageEvent(int remainingHealt)
    {
        Debug.Log(string.Format("Remaining health : {0}", remainingHealt));
    }

    protected virtual void OnRespawnEvent(int remainingLives, int remainingHealth)
    {
        Debug.Log(string.Format("Remaining lives : {0}, Remaining health : {1}", remainingLives, remainingHealth));
    }

    protected virtual void OnLivesOrHealthChangeEvent(int currentLives, int currentHealth)
    {
        Debug.Log(string.Format("Current lives : {0}, Current health : {1}", currentLives, currentHealth));
    }

    protected virtual void OnLivesOverEvent(int currentLives)
    {
        Debug.Log(string.Format("Character lives over : {0}", currentLives));
    }
}
