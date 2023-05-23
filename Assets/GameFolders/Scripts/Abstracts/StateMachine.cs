using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : IStateMachine 
{
    /// <summary>
    /// For now we only have 2 states, which could only be controlled by a single method on player walking status.
    /// 
    /// </summary>

    private IState _currentState;
    private IState _idleState;
    private IState _walkingState;
    private Animator _animator;
    private IInputHandler _inputHandler;

    public StateMachine(Animator animator, IInputHandler inputHandler)
    {
        _animator = animator;
        _inputHandler = inputHandler;
        _idleState = new Idle(_animator);
        _walkingState = new Walking(_animator);

        Subscribe();
        StartStates();
    }

    private void StartStates()
    {
        _currentState = _idleState;
        _idleState.Enter();
    }

    public void ChangeState(IState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Tick()
    {
        _currentState.Tick();
    }

    public void FixedTick()
    {
        _currentState.FixedTick();
    }

    public void Subscribe()
    {
        _inputHandler.OnPlayerMove += SwitchToWalkingState;
        _inputHandler.OnPlayerStop += SwitchToIdleState;
    }

    public void UnSubscribe()
    {
        _inputHandler.OnPlayerMove -= SwitchToWalkingState;
        _inputHandler.OnPlayerStop -= SwitchToIdleState;
    }

    private void SwitchToWalkingState()
    {
        ChangeState(_walkingState);
    }

    private void SwitchToIdleState()
    {
        ChangeState(_idleState);
    }


}
