using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    private Animator _animator;
    private const string IS_WALKING = "IsWalking";

    public Idle(Animator animator)
    {
        _animator = animator;
        Enter();
    }

    public void Enter()
    {
        _animator.SetBool(IS_WALKING, false);
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
       
    }
}
