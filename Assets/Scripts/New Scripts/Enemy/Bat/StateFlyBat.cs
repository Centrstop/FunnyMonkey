using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Bat/StateFlyBat")]
    public class StateFlyBat : StateEnemyFly
    {
        public StateFlyBat(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isFly";
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
                    stateSwitcherRef.SwitchState<StateCeilingInBat>();
                }
            }
        }

    }
}