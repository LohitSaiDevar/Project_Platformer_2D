using UnityEngine;

namespace Enemies.Bosses
{
    public class Phase2State : IBossState
    {
        public BossStateID GetID()
        {
            return BossStateID.Phase2;
        }
        public void Enter(Boss boss)
        {
            Debug.Log("Phase 2 started");
        }
        public void Update(Boss boss)
        {
            if (boss.healthSystem.GetHealthPercent() < 40)
            {
                Debug.Log("Less than 40%");
                boss.bossStateMachine.ChangeState(BossStateID.PhaseFinal);
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