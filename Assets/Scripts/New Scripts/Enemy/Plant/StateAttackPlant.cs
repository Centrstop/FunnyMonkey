using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Plant/StateAttackPlant")]
    public class StateAttackPlant : StateEnemyAttack
    {
        public StateAttackPlant(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isAttack";
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeOutToState<StateEnemyIdle>(timeInState));
            enemyRef.AttackEffect();
        }

        public override void Stop()
        {
            base.Stop();
            if (enemyRef.damageControl != null)
            {
                enemyRef.damageControl.Activate();
            }
        }
    }
}