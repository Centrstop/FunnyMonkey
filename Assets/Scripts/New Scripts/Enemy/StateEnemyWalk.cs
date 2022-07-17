using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Base/StateEnemyWalk")]
    public class StateEnemyWalk : BaseStateEnemy
    {
        public StateEnemyWalk(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isWalk";
        }

        public override void DieEnemy()
        {
            enemyRef.SwitchState<StateEnemyDead>();
        }

        public override void MoveEnemy(Vector2 movement)
        {
            if(movement.x > 0)
            {
                if (enemyRef.edgeRight && (!enemyRef.wallRight))
                {
                    Move(movement.x);
                }
                else
                {
                    enemyRef.SwitchState<StateEnemyIdle>();
                }
            }
            else if(movement.x < 0)
            {
                if(enemyRef.edgeLeft && (!enemyRef.wallLeft))
                {
                    Move(movement.x);
                }
                else
                {
                    enemyRef.SwitchState<StateEnemyIdle>();
                }
            }
            else
            {
                enemyRef.SwitchState<StateEnemyIdle>();
            }
        }

        public override void RebornEnemy()
        {
            enemyRef.SwitchState<StateEnemyIdle>();
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
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