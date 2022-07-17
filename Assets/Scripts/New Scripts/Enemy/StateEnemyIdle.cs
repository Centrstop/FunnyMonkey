using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Base/StateEnemyIdle")]
    public class StateEnemyIdle : BaseStateEnemy
    {
        public float timeInState = 0.0f;
        public StateEnemyIdle(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isIdle";
        }

        public override void DieEnemy()
        {
            enemyRef.SwitchState<StateEnemyDead>();
        }

        public override void MoveEnemy(Vector2 movement)
        {
            return;
        }

        public override void RebornEnemy()
        {
            return;
        }

        public override void Start()
        {
            enemyRef.animator.SetBool(nameState, true);
            _isActive = true;
            if (enemyRef.edgeRight && (!enemyRef.wallRight))
            {
                enemyRef.direction = 1.0f;
            }
            else if (enemyRef.edgeLeft && (!enemyRef.wallLeft))
            {
                enemyRef.direction = -1.0f;
            }
            enemyRef.StartCoroutine(TimeOutToState<StateEnemyWalk>(timeInState));
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
    }
}