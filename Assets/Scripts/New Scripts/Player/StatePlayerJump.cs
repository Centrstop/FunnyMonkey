using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class StatePlayerJump : BaseStatePlayer
    {
        public StatePlayerJump(Player player, IPlayerStateSwitcher stateSwitcher) : base(player, stateSwitcher)
        {
            _nameState = "isJump";
        }

        public override void DiePlayer()
        {
            _player.SwitchState<StatePlayerDead>();
        }

        public override void JumpPlayer(bool jumpStarted)
        {
            return;
        }

        public override void MovePlayer(float horizontalMovement)
        {
            if (Mathf.Abs(horizontalMovement) > 0)
            {
                Move(horizontalMovement);
            }
        }

        public override void RebornPlayer()
        {
            _player.SwitchState<StatePlayerIdle>();
        }

        public override void Start()
        {
            base.Start();
            _player.JumpEffect();
            _player.StartCoroutine(Jump());
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TakeDamagePlayer()
        {
            _player.SwitchState<StatePlayerHit>();
        }

        private IEnumerator Jump()
        {
            if(_player.jumpCount < _player.allowedJumps)
            {
                SetLayerCollision(true);
                float time = 0.0f;
                _player.playerRigidbody.velocity = new Vector2(_player.playerRigidbody.velocity.x, 0);
                _player.playerRigidbody.AddForce(_player.transform.up * _player.jumpPower, ForceMode2D.Impulse);
                _player.jumpCount += 1;
                while(time < _player.jumpDuration)
                {
                    yield return null;
                    time += Time.deltaTime;
                }
                SetLayerCollision(false);
                if(_isActive)_player.SwitchState<StatePlayerFall>();
                yield break;
            }
        }

        private void SetLayerCollision(bool ignore)
        {
            foreach(string layerName in _player.passThroughLayers)
            {
                Physics2D.IgnoreLayerCollision(_player.playerLayer,
                                               LayerMask.NameToLayer(layerName),
                                               ignore);
            }
        }
    }
}