using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Plant/StateIdlePlant")]
    public class StateIdlePlant : StateEnemyIdle
    {
        public StateIdlePlant(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isIdle";
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeOutToState<StateEnemyAttack>(timeInState));
        }
    }
}