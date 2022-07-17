using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Base/StateEnemyDead")]
    public class StateEnemyDead : BaseStateEnemy
    {
        public float timeInState = 0.0f;
        public StateEnemyDead(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isDead";
        }

        public override void DieEnemy()
        {
            return;
        }

        public override void MoveEnemy(Vector2 movement)
        {
            return;
        }

        public override void RebornEnemy()
        {
            enemyRef.SwitchState<StateEnemyIdle>();
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            if(enemyRef.currentLive == 0)
            {
                enemyRef.StartCoroutine(TimeEnemyDie(timeInState));
            }
        }

        public override void Stop()
        {
            _isActive = false;
            enemyRef.animator.SetBool(nameState, false);
        }

        public override void TakeDamage()
        {
            return;
        }

        private IEnumerator TimeEnemyDie(float timeout)
        {
            float time = 0.0f;
            while (time < timeout)
            {
                yield return null;
                time += Time.deltaTime;
            }
            enemyRef.Die();
            yield break;
        }

        public override void SpriteDirection(float horizontalMovement)
        {
            return;
        }
    }
}