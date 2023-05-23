using System;
using UnityEngine;

public interface IInputReader 
{
    event Action<bool> OnAlternateInteract;
    event Action OnInteract;
    Vector2 Movement { get; }
}
