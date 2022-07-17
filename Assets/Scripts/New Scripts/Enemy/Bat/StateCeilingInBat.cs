using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Bat/StateCeilingInBat")]
    public class StateCeilingInBat : BaseStateEnemy
    {
        public float timeInState = 0.0f;
        public StateCeilingInBat(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isCeilingIn";
            stateSwitcherRef = stateSwitcher;
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
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeOutToState<StateEnemyIdle>(timeInState));
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