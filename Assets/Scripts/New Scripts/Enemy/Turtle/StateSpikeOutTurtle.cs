using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Turtle/StateSpikeOutTurtle")]
    public class StateSpikeOutTurtle : BaseStateEnemy
    {
        public float timeInState = 0.0f;
        public StateSpikeOutTurtle(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isSpikeOut";
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
            enemyRef.SwitchState<StateEnemyIdle>();
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            if (enemyRef.damageControl != null)
            {
                enemyRef.damageControl.Activate();
            }
            enemyRef.StartCoroutine(TimeOutToState<StateEnemyAttack>(timeInState));
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