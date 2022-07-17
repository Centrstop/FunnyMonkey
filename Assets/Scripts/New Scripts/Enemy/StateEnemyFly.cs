using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Base/StateEnemyFly")]
    public class StateEnemyFly : BaseStateEnemy
    {
        public float timeInState = 0.0f;
        protected bool _inFly = false;

        public StateEnemyFly(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isFly";
        }

        public override void DieEnemy()
        {
            enemyRef.SwitchState<StateEnemyDead>();
        }

        public override void MoveEnemy(Vector2 movement)
        {
            if (_inFly)
            {
                enemyRef.movementControl.StartMovement();
            }
            else
            {
                if (enemyRef.movementControl.StopMovement())
                {
                    stateSwitcherRef.SwitchState<StateEnemyIdle>();
                }
            }
        }

        public override void RebornEnemy()
        {
            return;
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeInFly(timeInState));
            enemyRef.AttackEffect();
        }

        public override void Stop()
        {
            _isActive = false;
            enemyRef.animator.SetBool(nameState, false);
        }

        public override void TakeDamage()
        {
            enemyRef.SwitchState<StateEnemyHit>();
        }

        public IEnumerator TimeInFly(float timeout)
        {
            float time = 0.0f;
            _inFly = true;
            while (time < timeout)
            {
                yield return null;
                time += Time.deltaTime;
            }
            _inFly = false;
            yield break;
        }

    }
}