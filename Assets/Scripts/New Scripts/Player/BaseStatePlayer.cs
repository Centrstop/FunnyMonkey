using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public abstract class BaseStatePlayer
    {
        protected readonly Player _player;
        protected readonly IPlayerStateSwitcher _stateSwitcher;
        protected string _nameState;
        protected bool _isActive;
        protected BaseStatePlayer(Player player, IPlayerStateSwitcher stateSwitcher)
        {
            _player = player;
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Start()
        {
            _isActive = true;
            _player.animator.SetBool(_nameState, true);
        }
        public virtual void Stop()
        {
            _isActive = false;
            _player.animator.SetBool(_nameState, false);
        }

        public abstract void MovePlayer(float horizontalMovement);

        public abstract void JumpPlayer(bool jumpStarted);

        public abstract void TakeDamagePlayer();

        public abstract void DiePlayer();

        public abstract void RebornPlayer();

        public virtual void SpriteDirection(float horizontalMovement)
        {
            if (horizontalMovement > 0)
            {
                _player.spriteRenderer.flipX = false;
            }
            else if (horizontalMovement < 0)
            {
                _player.spriteRenderer.flipX = true;
            }
        }

        protected virtual void Move(float horizontalMovement)
        {
            float movementForce = _player.transform.right.x * _player.movementSpeed * horizontalMovement;
            float horizontalVelocity = movementForce;
            float verticalVelocity = _player.playerRigidbody.velocity.y;
            _player.playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }

    }
}