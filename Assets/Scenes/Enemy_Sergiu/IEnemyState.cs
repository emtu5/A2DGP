public interface IEnemyState
{
    void Enter(IEnemyStateMachine enemy);
    void Update(IEnemyStateMachine enemy);
    void Exit(IEnemyStateMachine enemy);
}