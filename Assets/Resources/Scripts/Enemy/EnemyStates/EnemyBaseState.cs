namespace Enemies
{
    public enum EnemyStateID
    {
        Idle,
        Run,
        Attack,
        Hurt,
        Patrol,
        Chase,
        Death,
        Fall
    }

    public interface IEnemyState
    {
        EnemyStateID GetID();
        void Enter(Enemy enemy);
        void Update(Enemy enemy);
        void FixedUpdate(Enemy enemy);
        void Exit(Enemy enemy);
    }
}