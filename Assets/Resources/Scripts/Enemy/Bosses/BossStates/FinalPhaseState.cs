using UnityEngine;

namespace Enemies.Bosses
{
    public class FinalPhaseState : IBossState
    {
        public BossStateID GetID()
        {
            return BossStateID.PhaseFinal;
        }
        public void Enter(Boss boss)
        {
            Debug.Log("Final Phase started");
        }
        public void Update(Boss boss)
        {

        }
        public void FixedUpdate(Boss boss)
        {

        }
        public void Exit(Boss boss)
        {

        }
    }
}