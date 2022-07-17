using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : Character, IEnemyStateSwitcher, ISubscribeScore
    {
        private event Action<int> _addScoreDelegate;
        public enum TypeEnemy { Static, Walking, Flying }

        [Header("Settings")]
        [Tooltip("Sprite renderer for this enemy")]
        [SerializeField] private TypeEnemy _typeEnemy;

        [Tooltip("Score points per enemy"), Range(0, 100)]
        [SerializeField] private int _scorePoint;

        [Tooltip("How fast this enemy moves")]
        [SerializeField] private float _movementSpeed = 2f;

        [Tooltip("Sprite renderer for this enemy")]
        [SerializeField] private SpriteRenderer _spriteRenderer = null;

        [Tooltip("Sprite renderer for this enemy")]
        [SerializeField] private bool _spriteLookRight = false;

        [Tooltip("The animator component that controls the enemy's animations")]
        [SerializeField] private Animator _animator = null;

        [Header("References")]
        [Tooltip("The ground check component used to test whether this enemy has hit a wall to the left.")]
        [SerializeField] GroundCheck _wallLeft;
        [Tooltip("The ground check component used to test whether this enemy has hit a wall to the right.")]
        [SerializeField] GroundCheck _wallRight;
        [Tooltip("Left ground check component used to determine when the enemy has reached an edge on its left.")]
        [SerializeField] GroundCheck _edgeLeft;
        [Tooltip("Right ground check component used to determine when the enemy has reached an edge on its right.")]
        [SerializeField] GroundCheck _edgeRight;

        [Tooltip("List scriptable object enemy state")]
        public List<BaseStateEnemy> scriptableStates;

        [Header("Effect")]
        [Tooltip("The effect to spawn when the enemy attack")]
        [SerializeField] private EffectHandler _attackEffect;

        [Header("Variably")]
        [Tooltip("If you need to control damage")]
        [SerializeField] private BaseDamageHandler _damageControl = null;

        [Tooltip("If you need to control movement")]
        [SerializeField] private MovementHandler _movementControl = null;

        private BaseStateEnemy _currentState = null;

        private List<BaseStateEnemy> _allStates;

        private float _direction = default;

        private Rigidbody2D _enemyRigidbody = null;

        private int _currentLive = default;

        private Vector3 _enemyPosition = default;

        private Vector3 _enemyOffsetPosition = default;

        public Vector3 spawnPosition
        {
            get { return _enemyPosition; }
        }
        public BaseDamageHandler damageControl
        {
            get { return _damageControl; }
        }

        public MovementHandler movementControl
        {
            get { return _movementControl; }
        }

        public int currentLive
        {
            get { return _currentLive; }
        }

        public bool spriteLookRight
        {
            get { return _spriteLookRight; }
        }

        public float direction
        {
            get { return _direction; }
            set { _direction = (value>0)? 1.0f: -1.0f; }
        }

        public Rigidbody2D enemyRigidbody
        {
            get { return _enemyRigidbody; }
        }

        public SpriteRenderer spriteRenderer
        {
            get { return _spriteRenderer; }
        }

        public Animator animator
        {
            get { return _animator; }
        }

        public float movementSpeed
        {
            get { return _movementSpeed; }
        }

        public bool wallLeft
        {
            get
            {
                if (_wallLeft != null)
                {
                    return _wallLeft.CheckGrounded();

                }
                else
                {
                    return false;
                }
            }
        }

        public bool wallRight
        {
            get
            {
                if(_wallRight != null)
                {
                    return _wallRight.CheckGrounded();
                }
                else
                {
                    return false;
                }
            }
        }

        public bool edgeLeft
        {
            get
            {
                if(_edgeLeft != null)
                {
                    return _edgeLeft.CheckGrounded();
                }
                else
                {
                    return false;
                }
            }
        }

        public bool edgeRight
        {
            get
            {
                if(_edgeRight != null)
                {
                    return _edgeRight.CheckGrounded();
                }
                else
                {
                    return false;
                }
            }
        }

        private void CheckComponentBeforeStart()
        {
            if (_spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer object not found, Please check the script settings");
            }
            if (_animator == null)
            {
                Debug.LogError("Animator object not found, Please check the script settings");
            }
            if (_wallLeft == null)
            {
                Debug.LogError("Wall left check object not found, Please check the script settings");
            }
            if (_wallRight == null)
            {
                Debug.LogError("Wall right check object not found, Please check the script settings");
            }
            if (_edgeLeft == null)
            {
                Debug.LogError("Edge left check object not found, Please check the script settings");
            }
            if (_edgeRight == null)
            {
                Debug.LogError("Edge right check object not found, Please check the script settings");
            }
        }

        void Awake()
        {
            CheckComponentBeforeStart();
        }

        // Use this for initialization
        void Start()
        {
            _allStates = new List<BaseStateEnemy>();
            InstantiateState();

            direction = UnityEngine.Random.Range(0.0f, 2.0f) - 1.0f;
            _enemyRigidbody = GetComponent<Rigidbody2D>();
            if (_healthCharacter != null)
            {
                _healthCharacter.SubscribeDie(OnDieEvent);
                _healthCharacter.SubscribeTakeDamage(OnTakeDamageEvent);
                _healthCharacter.SubscribeRespawn(OnRespawnEvent);
                _currentLive = _healthCharacter.currentLives;
            }
            _enemyPosition = transform.position;
            _enemyOffsetPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            _currentState.MoveEnemy(GetMovement());
            _currentState.SpriteDirection(GetCharacterOffset());
        }

        private float GetCharacterOffset()
        {
            Vector3 result = transform.position - _enemyOffsetPosition;
            _enemyOffsetPosition = transform.position;
            return result.x;
        }

        private void InstantiateState()
        {
            if(scriptableStates.Count != 0)
            {
                foreach (var state in scriptableStates)
                {
                    var tempState = Instantiate(state);
                    tempState.enemyRef = this;
                    tempState.stateSwitcherRef = this;
                    _allStates.Add(tempState);
                }
                _currentState = _allStates[0];
                _currentState.Start();
            }
            else
            {
                Debug.LogError("Not found scriptable object state");
            }
        }

        protected override void OnDieEvent(int remainingLives, int remainingHealth)
        {
            _currentLive = remainingLives;
            _currentState.DieEnemy();
        }

        protected override void OnTakeDamageEvent(int remainingHealt)
        {
            _currentState.TakeDamage();
        }

        protected override void OnRespawnEvent(int remainingLives, int remainingHealth)
        {
            _currentLive = remainingLives;
            transform.position = _enemyPosition;
            _currentState.RebornEnemy(); 
        }

        public void SwitchState<T>() where T : BaseStateEnemy
        {
            var state = _allStates.FirstOrDefault(predicate: (BaseStateEnemy s) => s is T);
            _currentState.Stop();
            state.Start();
            _currentState = state;
        }

        private Vector2 GetMovement()
        {
            switch (_typeEnemy)
            {
                case TypeEnemy.Static:
                    return Vector2.zero;
                case TypeEnemy.Walking:
                    return Vector2.right *_movementSpeed * direction;
                case TypeEnemy.Flying:
                    return Vector2.zero;
                default:
                    Debug.LogWarning("Unknown type Enemy");
                    return Vector2.zero;
            }
        }

        public void Die()
        {
            _addScoreDelegate?.Invoke(_scorePoint);
            Destroy(gameObject);
        }

        public void SubscribeAddScore(Action<int> addScoreDelegate)
        {
            _addScoreDelegate += addScoreDelegate;
        }

        public void AttackEffect()
        {
            if(_attackEffect != null)
            {
                _attackEffect.ShowEffect();
            }
        }

        public void UnsubscribeAddScore(Action<int> addScoreDelegate)
        {
            _addScoreDelegate -= addScoreDelegate;
        }
    }
}