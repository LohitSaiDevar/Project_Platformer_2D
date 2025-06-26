using UnityEngine;

namespace Enemies.Bosses
{
    public class BossStateMachine
    {
        public IBossState[] states;
        public Boss boss;
        public BossStateID currentState;
        public BossStateID previousState;
        public BossStateMachine(Boss boss)
        {
            this.boss = boss;
            int numStates = System.Enum.GetNames(typeof(BossStateID)).Length;
            states = new IBossState[numStates];
        }
        public void RegisterState(IBossState state)
        {
            int index = (int)state.GetID();
            states[index] = state;
        }
        public IBossState GetState(BossStateID stateID)
        {
            int index = (int)stateID;
            return states[index];
        }
        public void Update()
        {
            GetState(currentState)?.Update(boss);
        }

        public void FixedUpdate()
        {
            GetState(currentState).FixedUpdate(boss);
        }

        public void ChangeState(BossStateID newState)
        {
            previousState = currentState;
            GetState(currentState)?.Exit(boss);
            currentState = newState;
            GetState(currentState)?.Enter(boss);
            Debug.Log("Changed state to " + newState);
        }
    }
}