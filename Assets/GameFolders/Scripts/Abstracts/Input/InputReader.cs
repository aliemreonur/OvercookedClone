using UnityEngine;
using System;

public class InputReader : IInputReader
{
    public event Action<bool> OnAlternateInteract;
    public event Action OnInteract;
    public Vector2 Movement => _movement;
    //public bool IsAlternateInteracting { get; private set; } 
    private Vector2 _movement;
    private PlayerInput _playerInput;

    public InputReader()
    {
        _playerInput = new PlayerInput();
        ReadMovementValues();
        DetectInteract();
        DetectAlternateInteract();
        _playerInput.Enable();
    }

    private void ReadMovementValues()
    {
        _playerInput.Player.Move.performed += (ctx) => _movement = ctx.ReadValue<Vector2>().normalized;
        _playerInput.Player.Move.canceled += (ctx) => _movement = Vector2.zero;
    }

    private void DetectInteract()
    {
        _playerInput.Player.Interact.performed += (ctx) => OnInteract?.Invoke();
    }

    private void DetectAlternateInteract()
    {
         _playerInput.Player.AlternateInteract.started += (ctx) => OnAlternateInteract?.Invoke(true);
        _playerInput.Player.AlternateInteract.canceled += (ctx) => OnAlternateInteract?.Invoke(false);
        // _playerInput.Player.AlternateInteract.started += (ctx) => _playerInteractionHandler.AlternateInteract();
        //_playerInput.Player.AlternateInteract.canceled += (ctx) => _playerInteractionHandler.AlternateInteract();
    }
}
