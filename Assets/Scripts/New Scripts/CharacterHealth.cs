using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.New_Scripts;

public enum TeamType
{
    Neutral,
    Player,
    Enemy
}

public class CharacterHealth : MonoBehaviour
{
    private event Action<int, int> _dieDelegate;
    private event Action<int> _takeDamageDelegate;
    private event Action<int, int> _respawnDelegate;
    private event Action<int> _livesOverDelegate;
    private event Action<int, int> _livesOrHealthChangeDelegate;

    [Header("Team Settings")]
    [Tooltip("The team associated with this damage")]
    public TeamType team = TeamType.Neutral;

    [Header("Health Settings")]
    [Tooltip("The default health value")]
    [SerializeField] private int _defaultHealth = 1;
    public int defaultHealth
    {
        get { return _defaultHealth; }
        set { _defaultHealth = (value > _maximumHealth) ? _maximumHealth : value; }
    }
    [Tooltip("The maximum health value")]
    [SerializeField] private int _maximumHealth = 1;
    public int maximumHealth
    {
        get { return _maximumHealth; }
        set { _maximumHealth = (value < _currentHealth) ? _currentHealth : value; }
    }
    [Tooltip("The current in game health value")]
    [SerializeField] private int _currentHealth = 1;
    public int currentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value <= 0)
            {
                _currentHealth = 0;
            }else if(value <= _maximumHealth)
            {
                _currentHealth = value;
            }
        }
    }
        
    [Tooltip("Invulnerability duration, in seconds, after taking damage")]
    [SerializeField] private float _invincibilityTime = 3f;

    [Header("Lives settings")]
    [Tooltip("Current number of lives this health has")]
    [SerializeField] private int _currentLives = 3;
    public int currentLives
    {
        get { return _currentLives; }
        set
        {
            if(value <= 0)
            {
                _currentLives = 0;
            }else if(value <= _maximumLives)
            {
                _currentLives = value;
            }
        }
    }
    [Tooltip("The maximum number of lives this health has")]
    [SerializeField] private int _maximumLives = 5;
    public int maximumLives
    {
        get { return _maximumLives; }
        set { _maximumLives = (value < _currentLives) ? _currentLives : value; }
    }
    [Tooltip("The amount of time to wait before respawning")]
    [SerializeField] private float _respawnWaitTime = 3f;

    [Header("Effects")]
    [Tooltip("The effect to create when this health dies")]
    [SerializeField] private EffectHandler _deathEffect;
    [Tooltip("The effect to create when this health is damaged (but does not die)")]
    [SerializeField] private EffectHandler _hitEffect;

    public bool isDeath
    {
        get { return _currentHealth <= 0; }
    }

    public bool isLivesOver
    {
        get { return _currentLives <= 0; }
    }

    private bool _isInvincible;

    // Start is called before the first frame update
    void Start()
    {
        _isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Description:
    /// Event subscription character died
    /// Input:
    /// Action<int> onDie
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="onDie">The method called on event</param>
    public void SubscribeDie(Action<int, int> onDie)
    {
        _dieDelegate += onDie;
    }

    /// <summary>
    /// Description:
    /// Event subscription character take damage
    /// Input:
    /// Action<int> onTakeDamage
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="onTakeDamage">The method called on event</param>
    public void SubscribeTakeDamage(Action<int> onTakeDamage)
    {
        _takeDamageDelegate += onTakeDamage;
    }

    /// <summary>
    /// Description:
    /// Event subscription character respawn
    /// Input:
    /// Action<int,int> onRespawn
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="onRespawn">The method called on event</param>
    public void SubscribeRespawn(Action<int,int> onRespawn)
    {
        _respawnDelegate += onRespawn;
    }

    /// <summary>
    /// Description:
    /// Event subscription character lives over
    /// Input:
    /// Action<int> onLivesOver
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="onLivesOver">The method called on event</param>
    public void SubscribeLivesOver(Action<int> onLivesOver)
    {
        _livesOverDelegate += onLivesOver;
    }

    /// <summary>
    /// Description:
    /// Event subscription character lives or health change
    /// Input:
    /// Action<int,int> onLivesOrHealthChange
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="onLivesOrHealthChange">The method called on event</param>
    public void SubscribeLivesOrHealthChange(Action<int,int> onLivesOrHealthChange)
    {
        _livesOrHealthChangeDelegate += onLivesOrHealthChange;
    }

    /// <summary>
    /// Description:
    /// Waiting for end of time invincibility
    /// Input:
    /// float invincibilityTime
    /// Returns:
    /// IEnumerator
    /// </summary>
    /// <param name="invincibilityTime">Invincibility time in seconds</param>
    private IEnumerator WaitInvincibilityTime(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        _isInvincible = false;
    }

    /// <summary>
    /// Description:
    /// Waiting for end of time respawn character
    /// Input:
    /// float respawnWaitTime
    /// Returns:
    /// IEnumerator
    /// </summary>
    /// <param name="respawnWaitTime">Respawn time in seconds</param>
    private IEnumerator WaitRespawnTime(float respawnWaitTime)
    {
        yield return new WaitForSeconds(respawnWaitTime);
        Respawn();
        _respawnDelegate?.Invoke(_currentLives, _currentHealth);
    }

    /// <summary>
    /// Description:
    /// Applies damage to the health unless the health is invincible.
    /// Input:
    /// int damageAmount
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="damageAmount">The amount of damage to take</param>
    public void TakeDamage(int damageAmount) //Можно сделать интерфейс
    {
        if(_isInvincible || isDeath)
        {
            return;
        }
        else
        {
            currentHealth -= damageAmount;
            if (isDeath)
            {
                Die();
            }
            else
            {
                _isInvincible = true;
                StartCoroutine(WaitInvincibilityTime(_invincibilityTime));
                _takeDamageDelegate?.Invoke(currentHealth);
                ShowEffect(_hitEffect);
            }
        }
    }

    /// <summary>
    /// Description:
    /// Resets the health to the default value
    /// Input:
    /// None
    /// Returns:
    /// void (no return)
    private void Respawn()
    {
        currentHealth = defaultHealth;
    }

    /// <summary>
    /// Description:
    /// Applies healing to the health, capped out at the maximum health.
    /// Input:
    /// int healingAmount
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="healingAmount">How much healing to apply</param>
    public void ReceiveHealing(int healingAmount)
    {
        currentHealth += healingAmount;
        _livesOrHealthChangeDelegate?.Invoke(currentLives, currentHealth);
    }

    /// <summary>
    /// Description:
    /// Gives the health script more lives if the health is using lives
    /// Input:
    /// int bonusLives
    /// Return:
    /// void
    /// </summary>
    /// <param name="bonusLives">The number of lives to add</param>
    public void AddLives(int bonusLives)
    {
        currentLives += bonusLives;
        _livesOrHealthChangeDelegate?.Invoke(currentLives, currentHealth);
    }

    /// <summary>
    /// Description:
    /// Handles the death of the health
    /// Input:
    /// None
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Die()
    {
        currentLives -= 1;
        ShowEffect(_deathEffect);
        if (isLivesOver)
        {
            LivesOver();
        }
        else
        {
            _dieDelegate?.Invoke(currentLives, currentHealth);
            StartCoroutine(WaitRespawnTime(_respawnWaitTime));
        }

    }

    private void LivesOver()
    {
        _dieDelegate?.Invoke(currentLives, currentHealth);
        _livesOverDelegate?.Invoke(currentLives);
    }

    private void ShowEffect(EffectHandler effect)
    {
        if (effect != null)
        {
            effect.ShowEffect();
        }
    }


}
