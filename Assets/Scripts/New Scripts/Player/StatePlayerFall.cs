using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class StatePlayerFall : BaseStatePlayer
    {
        public StatePlayerFall(Player player, IPlayerStateSwitcher stateSwitcher) : base(player, stateSwitcher)
        {
            _nameState = "isFall";
        }

        public override void DiePlayer()
        {
            _player.SwitchState<StatePlayerDead>();
        }

        public override void JumpPlayer(bool jumpStarted)
        {
            if (jumpStarted)
            {
                if (_player.jumpCount < _player.allowedJumps)
                {
                    _player.SwitchState<StatePlayerJump>();
                }
            }
        }

        public override void MovePlayer(float horizontalMovement)
        {
            if (_player.grounded)
            {
                _player.SwitchState<StatePlayerIdle>();
            }
            else
            {
                if (Mathf.Abs(horizontalMovement) > 0)
                {
                    Move(horizontalMovement);
                }
            }
        }

        public override void RebornPlayer()
        {
            _player.SwitchState<StatePlayerIdle>();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TakeDamagePlayer()
        {
            _player.SwitchState<StatePlayerHit>();
        }
    }
}