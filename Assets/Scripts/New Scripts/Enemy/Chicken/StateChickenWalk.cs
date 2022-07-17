using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    [CreateAssetMenu(menuName = "State/Enemy/Chicken/StateChickenWalk")]
    public class StateChickenWalk : StateEnemyWalk
    {
        public StateChickenWalk(Enemy enemy, IEnemyStateSwitcher stateSwitcher) : base(enemy, stateSwitcher)
        {
            nameState = "isWalk";
        }

        public override void Start()
        {
            base.Start();
            enemyRef.AttackEffect();
        }
    }
}