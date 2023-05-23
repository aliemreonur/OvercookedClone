using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : IState
{
    private Animator _animator;
    private const string IS_WALKING = "IsWalking";

    public Walking(Animator animator)
    {
        _animator = animator;
      
    }

    public void Enter()
    {
        _animator.SetBool(IS_WALKING, true);
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
