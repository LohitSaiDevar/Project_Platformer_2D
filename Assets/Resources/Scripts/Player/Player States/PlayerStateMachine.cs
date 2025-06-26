using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine
    {
        public IPlayerState[] states;
        public PlayerController player;
        public PlayerStateID currentState;
        public PlayerStateID previousState;
        public PlayerStateMachine(PlayerController player)
        {
            this.player = player;
            int numStates = System.Enum.GetNames(typeof(PlayerStateID)).Length;
            states = new IPlayerState[numStates];
        }
        public void RegisterState(IPlayerState state)
        {
            int index = (int)state.GetID();
            states[index] = state;
        }
        public IPlayerState GetState(PlayerStateID stateID)
        {
            int index = (int)stateID;
            return states[index];
        }
        public void Update()
        {
            GetState(currentState)?.Update(player);
        }

        public void FixedUpdate()
        {
            GetState(currentState)?.FixedUpdate(player);
        }

        public void ChangeState(PlayerStateID newState)
        {
            previousState = currentState;
            GetState(currentState)?.Exit(player);
            currentState = newState;
            GetState(currentState)?.Enter(player);
            //Debug.Log("Changed state from " + previousState + " to " + newState);
        }
    }
}