using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class StatePlayerDead : BaseStatePlayer
    {
        public StatePlayerDead(Player player, IPlayerStateSwitcher stateSwitcher) : base(player, stateSwitcher)
        {
            _nameState = "isDead";
        }

        public override void DiePlayer()
        {
            return;
        }

        public override void JumpPlayer(bool jumpStarted)
        {
            return;
        }

        public override void MovePlayer(float horizontalMovement)
        {
            return;
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
            return;
        }

        public override void SpriteDirection(float horizontalMovement)
        {
            return;
        }
    }
}