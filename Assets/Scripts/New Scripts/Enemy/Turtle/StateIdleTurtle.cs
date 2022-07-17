using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Turtle/StateIdleTurtle")]
    public class StateIdleTurtle : StateEnemyIdle
    {
        public StateIdleTurtle(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isIdle";
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeOutToState<StateSpikeOutTurtle>(timeInState));
        }
    }
}