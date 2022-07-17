using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class StatePlayerWalk : BaseStatePlayer
    {
        public StatePlayerWalk(Player player, IPlayerStateSwitcher stateSwitcher) : base(player, stateSwitcher)
        {
            _nameState = "isWalk";
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
                if (Mathf.Abs(horizontalMovement) > 0)
                {
                    Move(horizontalMovement);
                }
                else
                {
                    _player.SwitchState<StatePlayerIdle>();
                }
            }
            else
            {
                _player.SwitchState<StatePlayerFall>();
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