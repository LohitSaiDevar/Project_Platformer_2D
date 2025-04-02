using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public IEnemyState[] states;
    public Enemy enemy;
    public EnemyStateID currentState;
    public EnemyStateMachine(Enemy enemy)
    {
        this.enemy = enemy;
        int numStates = System.Enum.GetNames(typeof(EnemyStateID)).Length;
        states = new IEnemyState[numStates];
    }
    public void RegisterState(IEnemyState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }
    public IEnemyState GetState(EnemyStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }
    public void Update()
    {
        GetState(currentState)?.Update(enemy);
    }

    public void ChangeState(EnemyStateID newState)
    {
        GetState(currentState)?.Exit(enemy);
        currentState = newState;
        GetState(currentState)?.Enter(enemy);
        Debug.Log("Changed state to " + newState);
    }
}
