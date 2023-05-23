using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IProcessable 
{
    event Action OnProcessFinished;
    float ElapsedProcessTime { get; }
    float TotalProcessTime { get; }
    void ProcessActive();
    void ProcessStopped();
}
