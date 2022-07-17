using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class StatePlayerIdle : BaseStatePlayer
    {
        public StatePlayerIdle(Player player, IPlayerStateSwitcher stateSwitcher) : base(player, stateSwitcher)
        {
            _nameState = "isIdle";
        }

        public override void DiePlayer()
        {
            _player.SwitchState<StatePlayerDead>();
        }

        public override void JumpPlayer(bool jumpStarted)
        {
            if (jumpStarted)
            {
                _player.SwitchState<StatePlayerJump>();
            }
        }

        public override void MovePlayer(float horizontalMovement)
        {
            if (_player.grounded)
            {
                _player.jumpCount = 0;
                if (Mathf.Abs(horizontalMovement) > 0)
                {
                    _player.SwitchState<StatePlayerWalk>();
                }
            }
            else
            {
                _player.SwitchState<StatePlayerFall>();
            }
        }

        public override void RebornPlayer()
        {
            return;
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