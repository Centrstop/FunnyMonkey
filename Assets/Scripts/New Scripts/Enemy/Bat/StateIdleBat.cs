using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Bat/StateIdleBat")]
    public class StateIdleBat : StateEnemyIdle
    {
        public StateIdleBat(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isIdle";
        }

        public override void Start()
        {
            _isActive = true;
            enemyRef.animator.SetBool(nameState, true);
            enemyRef.StartCoroutine(TimeOutToState<StateCeilingOutBat>(timeInState));
        }
    }
}