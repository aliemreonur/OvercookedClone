using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState : IFrameDependent
{
    void Enter();
    void Exit();
}
