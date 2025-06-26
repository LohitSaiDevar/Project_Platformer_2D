using UnityEngine;

namespace Enemies.Bosses
{
    public class Phase1State : IBossState
    {
        public BossStateID GetID()
        {
            return BossStateID.Phase1;
        }
        public void Enter(Boss boss)
        {
            Debug.Log("Phase 1 started");
        }
        public void Update(Boss boss)
        {
            if (boss.healthSystem.GetHealthPercent() < 70)
            {
                Debug.Log("Less than 70%");
                boss.bossStateMachine.ChangeState(BossStateID.Phase2);
            }
        }
        public void FixedUpdate(Boss boss)
        {

        }
        public void Exit(Boss boss)
        {

        }
    }
}