using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.New_Scripts
{
    public class InputManager : MonoBehaviour
    {
        private event Action<float, float> _movementDelegate;
        private event Action<bool, bool> _jumpDelegate;
        private event Action _pauseDelegate;
        private event Action _unpauseDelegate;
        private event Action<float, float> _lookAxisDelegate;

        [Header("Movement Input")]
        [Tooltip("The horizontal movmeent input of the player.")]
        [SerializeField] private float _horizontalMovement;
        [Tooltip("The vertical movmeent input of the player.")]
        [SerializeField] private float _verticalMovement;

        [Header("Jump Input")]
        [Tooltip("Whether a jump was started this frame.")]
        [SerializeField] private bool _jumpStarted = false;
        [Tooltip("Whether the jump button is being held.")]
        [SerializeField] private bool _jumpHeld = false;

        [Header("Pause Input")]
        [Tooltip("The state of the pause button")]
        [SerializeField] private bool _pauseButton = false;
        [SerializeField] private bool _pauseFlag = false;

        [Header("Mouse Input")]
        [Tooltip("The horizontal mouse input of the player.")]
        [SerializeField] private float _horizontalLookAxis;
        [Tooltip("The vertical mouse input of the player.")]
        [SerializeField] private float _verticalLookAxis;

        private void Awake()
        {
            ResetValuesToDefault();
        }

        public void SubscribeMovement(Action<float, float> onMove)
        {
            _movementDelegate += onMove;
        }

        public void UnsubscribeMovement(Action<float, float> onMove)
        {
            _movementDelegate -= onMove;
        }

        public void SubscribeJump(Action<bool,bool> onJump)
        {
            _jumpDelegate += onJump;
        }

        public void UnsubscribeJump(Action<bool, bool> onJump)
        {
            _jumpDelegate -= onJump;
        }

        public void SubscribePause(Action onPause)
        {
            _pauseDelegate += onPause;
        }

        public void UnsubscribePause(Action onPause)
        {
            _pauseDelegate -= onPause;
        }

        public void SubscribeUnPause(Action onUnPause)
        {
            _unpauseDelegate += onUnPause;
        }

        public void UnsubscribeUnPause(Action onUnPause)
        {
            _unpauseDelegate -= onUnPause;
        }

        public void SubscribeLookAxis(Action<float, float> onLookAxis)
        {
            _lookAxisDelegate += onLookAxis;
        }

        public void UnsubscribeLookAxis(Action<float, float> onLookAxis)
        {
            _lookAxisDelegate -= onLookAxis;
        }

        private void ResetValuesToDefault()
        {
            _horizontalMovement = default;
            _verticalMovement = default;

            _horizontalLookAxis = default;
            _verticalLookAxis = default;

            _jumpStarted = default;
            _jumpHeld = default;

            _pauseButton = default;
        }



        public void GetMovementInput(InputAction.CallbackContext callbackContext)
        {
            if (isActiveAndEnabled)
            {
                Vector2 movementVector = callbackContext.ReadValue<Vector2>();
                _horizontalMovement = movementVector.x;
                _verticalMovement = movementVector.y;
                _movementDelegate?.Invoke(_horizontalMovement, _verticalMovement);
            }
        }

        public void GetJumpInput(InputAction.CallbackContext callbackContext)
        {
            if (isActiveAndEnabled)
            {
                _jumpStarted = !callbackContext.canceled;
                _jumpHeld = !callbackContext.canceled;
                _jumpDelegate?.Invoke(_jumpStarted, _jumpHeld);
                StartCoroutine(ResetJumpStart());
            }
        }

        private IEnumerator ResetJumpStart()
        {
            yield return new WaitForEndOfFrame();
            _jumpStarted = false;
            _jumpDelegate?.Invoke(_jumpStarted, _jumpHeld);
        }

        public void GetPauseInput(InputAction.CallbackContext callbackContext)
        {
            if (isActiveAndEnabled)
            {
                _pauseButton = callbackContext.canceled;
                TogglePause(_pauseButton);
            }
        }

        private void TogglePause(bool value)
        {
            if(value)
            {
                _pauseFlag = !_pauseFlag;
                if (_pauseFlag)
                {
                    _pauseDelegate?.Invoke();
                }
                else
                {
                    _unpauseDelegate?.Invoke();
                }
            }
        }

        public void GetMouseLookInput(InputAction.CallbackContext callbackContext)
        {
            if (isActiveAndEnabled)
            {
                Vector2 mouseLookVector = callbackContext.ReadValue<Vector2>();
                _horizontalLookAxis = mouseLookVector.x;
                _verticalLookAxis = mouseLookVector.y;
                _lookAxisDelegate?.Invoke(_horizontalLookAxis, _verticalLookAxis);
            }
        }
    }
}