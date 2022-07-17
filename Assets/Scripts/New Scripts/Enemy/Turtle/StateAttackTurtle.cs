using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Turtle/StateAttackTurtle")]
    public class StateAttackTurtle : StateEnemyAttack
    {
        public StateAttackTurtle(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isAttack";
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeOutToState<StateSpikeInTurtle>(timeInState));
            enemyRef.AttackEffect();
        }


    }
}