using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Turtle/StateSpikeInTurtle")]
    public class StateSpikeInTurtle : BaseStateEnemy
    {
        public float timeInState = 0.0f;

        public StateSpikeInTurtle(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isSpikeIn";
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
                enemyRef.damageControl.Deactivate();
            }
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