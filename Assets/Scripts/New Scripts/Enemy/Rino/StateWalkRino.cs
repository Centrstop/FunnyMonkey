using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Rino/StateWalkRino")]
    public class StateWalkRino : StateEnemyWalk
    {
        public StateWalkRino(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isWalk";
        }

        public override void MoveEnemy(Vector2 movement)
        {
            if (movement.x > 0)
            {
                if (enemyRef.edgeRight && (!enemyRef.wallRight))
                {
                    Move(movement.x);
                }
                else
                {
                    enemyRef.SwitchState<StateBreakWallRino>();
                }
            }
            else if (movement.x < 0)
            {
                if (enemyRef.edgeLeft && (!enemyRef.wallLeft))
                {
                    Move(movement.x);
                }
                else
                {
                    enemyRef.SwitchState<StateBreakWallRino>();
                }
            }
            else
            {
                enemyRef.SwitchState<StateEnemyIdle>();
            }
        }

        public override void Start()
        {
            base.Start();
            if (enemyRef.damageControl != null)
            {
                enemyRef.damageControl.Activate();
            }
            enemyRef.AttackEffect();
        }

    }
}
