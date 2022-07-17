using Assets.Scripts.New_Scripts;

public interface IEnemyStateSwitcher
{
    void SwitchState<T>() where T : BaseStateEnemy;
}
