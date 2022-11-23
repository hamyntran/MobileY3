using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    
    public void RunStateMachine()
    {
        State nextState = currentState?.RunState();

        if (nextState != null && nextState!=currentState)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State state)
    {
        state.EnterState(this);
        Debug.Log("change to "+state);
        currentState = state;
    }

    public StateMachine(State _currentState)
    {
        this.currentState = _currentState;
        currentState.EnterState(this);
    }
}
