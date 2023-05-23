using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine :IFrameDependent
{
    void Subscribe();
    void UnSubscribe();
}
