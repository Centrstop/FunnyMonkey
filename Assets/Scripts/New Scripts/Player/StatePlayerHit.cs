using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public class StatePlayerHit : BaseStatePlayer
    {
        private float _timeHit = default;
        public StatePlayerHit(Player player, IPlayerStateSwitcher stateSwitcher, float timeHit) : base(player, stateSwitcher)
        {
            _timeHit = timeHit;
            _nameState = "isHit";
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
            return;
        }

        public override void RebornPlayer()
        {
            _player.SwitchState<StatePlayerIdle>();
        }

        public override void Start()
        {
            base.Start();
            _player.StartCoroutine(TimeOutHit());
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void TakeDamagePlayer()
        {
            return;
        }

        private IEnumerator TimeOutHit()
        {
            float time = 0.0f;
            while(time < _timeHit)
            {
                yield return null;
                time += Time.deltaTime;
            }
            yield return null;
            if(_isActive) _player.SwitchState<StatePlayerIdle>();
            yield break;
        }
    }
}