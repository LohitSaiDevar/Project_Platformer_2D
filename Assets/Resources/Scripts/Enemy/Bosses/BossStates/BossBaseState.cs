namespace Enemies.Bosses
{
    public enum BossStateID
    {
        Phase1,
        Phase2,
        PhaseFinal
    }

    public interface IBossState
    {
        BossStateID GetID();
        void Enter(Boss boss);
        void Update(Boss boss);
        void FixedUpdate(Boss boss);
        void Exit(Boss boss);
    }
}