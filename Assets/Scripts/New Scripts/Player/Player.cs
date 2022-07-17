using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.New_Scripts;
using System.Linq;
using System;

/// <summary>
/// Class which handles player
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character, IPlayerStateSwitcher
{
    private event Action<int> _updateLivesDelegate;
    private event Action<int> _updateHealthDelegate;
    private event Action _levelCompletedDelegate;
    private event Action _gameOverDelegate;

    [Tooltip("The inputManager scripts")]
    [SerializeField] private InputManager _inputManager = null;

    [Tooltip("The animator component that controls the player's animations")]
    [SerializeField] private Animator _animator = null;

    [Tooltip("The sprite renderer that represents the player.")]
    [SerializeField] private SpriteRenderer _spriteRenderer = null;

    [Tooltip("The sprite renderer that represents the player.")]
    [SerializeField] private GroundCheck _groundCheck = null;

    [Header("Movement Settings")]
    [Tooltip("The speed at which to move the player horizontally"), Range(1.0f, 100.0f)]
    [SerializeField] private float _movementSpeed = 4.0f;

    [Header("Jump Settings")]
    [Tooltip("The force with which the player jumps."), Range(1.0f, 100.0f)]
    [SerializeField] private float _jumpPower = 10.0f;

    [Tooltip("The number of jumps that the player is alowed to make."), Range(1, 10)]
    [SerializeField] private int _allowedJumps = 1;

    [Tooltip("The duration that the player spends in the \"jump\" state"), Range(0.1f, 3.0f)]
    [SerializeField] private float _jumpDuration = 0.1f;

    [Tooltip("Layers to pass through when moving upwards")]
    [SerializeField] private List<string> _passThroughLayers = new List<string>();

    [Header("Effect Settings")]
    [Tooltip("The effect to create when score is collected")]
    [SerializeField] private EffectHandler _scoreEffect;

    [Tooltip("The effect to spawn when the player jumps")]
    [SerializeField] private EffectHandler _jumpEffect = null;

    private BaseStatePlayer _currentState = null;
    private List<BaseStatePlayer> _allStates;

    private Rigidbody2D _playerRigidbody = null;

    private Vector3 _playerPosition;

    private int _playerLayer;

    private int _jumpCount = default;

    public int playerLayer
    {
        get { return _playerLayer; }
    }

    public Rigidbody2D playerRigidbody
    {
        get
        {
            return _playerRigidbody;
        }
    }

    public int jumpCount
    {
        get { return _jumpCount; }
        set { _jumpCount = (value >= 0) ? value : 0; }
    }

    public bool grounded
    {
        get
        {
            if(_groundCheck != null)
            {
                return _groundCheck.CheckGrounded();
            }
            else
            {
                return false;
            }
        }
    }

    public float movementSpeed
    {
        get
        {
            return _movementSpeed;
        }
    }

    public float jumpPower
    {
        get
        {
            return _jumpPower;
        }
    }

    public int allowedJumps
    {
        get
        {
            return _allowedJumps;
        }
    }

    public float jumpDuration
    {
        get
        {
            return _jumpDuration;
        }
    }


    public List<string> passThroughLayers
    {
        get
        {
            return _passThroughLayers;
        }
    }

    public Animator animator
    {
        get
        {
            return _animator;
        }
    }

    public SpriteRenderer spriteRenderer
    {
        get { return _spriteRenderer; }
    }

    public void LevelCompleted()
    {
        _levelCompletedDelegate?.Invoke();
    }

    public void GetCurrentLivesAndHealth()
    {
        if(_healthCharacter != null)
        {
            _updateLivesDelegate?.Invoke(_healthCharacter.currentLives);
            _updateHealthDelegate?.Invoke(_healthCharacter.currentHealth);
        }
    }


    private void CheckComponentBeforeStart()
    {
        if(_inputManager == null)
        {
            Debug.LogError("Input manager object not found, Please check the script settings");
        }
        if(_animator == null)
        {
            Debug.LogError("Animator object not found, Please check the script settings");
        }
        if(_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer object not found, Please check the script settings");
        }
        if(_groundCheck == null)
        {
            Debug.LogError("Ground check object not found, Please check the script settings");
        }

    }

    void Awake()
    {
        CheckComponentBeforeStart();
    }

    void Start()
    {
        _allStates = new List<BaseStatePlayer>()
        {
            new StatePlayerIdle(this, stateSwitcher:this),
            new StatePlayerWalk(this, stateSwitcher:this),
            new StatePlayerJump(this, stateSwitcher:this),
            new StatePlayerFall(this, stateSwitcher:this),
            new StatePlayerHit(this, stateSwitcher:this, timeHit:0.2f),
            new StatePlayerDead(this, stateSwitcher:this)
        };
        _currentState = _allStates[0];

        _playerRigidbody = GetComponent<Rigidbody2D>();
        SavePosition();

        _healthCharacter.SubscribeDie(OnDieEvent);
        _healthCharacter.SubscribeTakeDamage(OnTakeDamageEvent);
        _healthCharacter.SubscribeRespawn(OnRespawnEvent);
        _healthCharacter.SubscribeLivesOrHealthChange(OnLivesOrHealthChangeEvent);
        _healthCharacter.SubscribeLivesOver(OnLivesOverEvent);
        _inputManager.SubscribeMovement(OnMovement);
        _inputManager.SubscribeJump(OnJump);

        _playerLayer = transform.gameObject.layer;
    }

    private void LateUpdate()
    {
        _currentState.JumpPlayer(_jumpStarted);
        _currentState.MovePlayer(_horizontalMovement);
        _currentState.SpriteDirection(_horizontalMovement);
    }

    protected override void OnDieEvent(int remainingLives, int remainingHealth)
    {
        _currentState.DiePlayer();
        _updateLivesDelegate?.Invoke(remainingLives);
        _updateHealthDelegate?.Invoke(remainingHealth);

    }

    protected override void OnTakeDamageEvent(int remainingHealth)
    {
        _currentState.TakeDamagePlayer();
        _updateHealthDelegate?.Invoke(remainingHealth);
    }

    protected override void OnRespawnEvent(int remainingLives, int remainingHealth)
    {
        _currentState.RebornPlayer();
        transform.position = _playerPosition;
        _updateLivesDelegate?.Invoke(remainingLives);
        _updateHealthDelegate?.Invoke(remainingHealth);
    }

    protected override void OnLivesOrHealthChangeEvent(int currentLives, int currentHealth)
    {
        _updateLivesDelegate?.Invoke(currentLives);
        _updateHealthDelegate?.Invoke(currentHealth);
    }

    protected override void OnLivesOverEvent(int currentLives)
    {
        _updateLivesDelegate?.Invoke(currentLives);
        _gameOverDelegate?.Invoke();
    }

    private float _horizontalMovement;
    private void OnMovement(float horizontalMovement, float verticalMovement)
    {
        _horizontalMovement = horizontalMovement;
    }

    private bool _jumpStarted;
    private void OnJump(bool jumpStarted, bool jumpHeld)
    {
        _jumpStarted = jumpStarted;
    }

    public void SwitchState<T>() where T : BaseStatePlayer
    {
        var state = _allStates.FirstOrDefault(predicate: (BaseStatePlayer s) => s is T);
        _currentState.Stop();
        state.Start();
        _currentState = state;
    }

    public void Bounce()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
        playerRigidbody.AddForce(transform.up * (jumpPower / 2), ForceMode2D.Impulse);
    }

    public void SubscribeUpdateLives(Action<int> updateLivesDelegate)
    {
        _updateLivesDelegate += updateLivesDelegate;
    }

    public void UnsubscribeUpdateLives(Action<int> updateLivesDelegate)
    {
        _updateLivesDelegate -= updateLivesDelegate;
    }

    public void SubscribeUpdateHealth(Action<int> updateHealthDelegate)
    {
        _updateHealthDelegate += updateHealthDelegate;
    }

    public void UnsubscribeUpdateHealth(Action<int> updateHealthDelegate)
    {
        _updateHealthDelegate -= updateHealthDelegate;
    }

    public void SubscribeLevelCompleted(Action levelCompletedDelegate)
    {
        _levelCompletedDelegate += levelCompletedDelegate;
    }

    public void UnsubscribeLevelCompleted(Action levelCompletedDelegate)
    {
        _levelCompletedDelegate -= levelCompletedDelegate;
    }

    public void SubscribeGameOver(Action gameOverDelegate)
    {
        _gameOverDelegate += gameOverDelegate;
    }

    public void UnsubscribeGameOver(Action gameOverDelegate)
    {
        _gameOverDelegate -= gameOverDelegate;
    }

    public void ScoreEffect()
    {
        if (_scoreEffect != null)
        {
            _scoreEffect.ShowEffect();
        }
    }

    public void JumpEffect()
    {
        if(_jumpEffect != null)
        {
            _jumpEffect.ShowEffect();
        }
    }

    public void SavePosition()
    {
        _playerPosition = transform.position;
    }
}
