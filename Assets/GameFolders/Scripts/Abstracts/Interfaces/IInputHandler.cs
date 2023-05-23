using System;
using UnityEngine;

public interface IInputHandler :IFrameDependent
{
    event Action OnPlayerMove;
    event Action OnPlayerStop;
    Vector3 MovementVector { get; }
}
