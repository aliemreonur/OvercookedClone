using UnityEngine;
using System;
using System.Collections;

public class InputHandler : IInputHandler
{
    public Vector3 MovementVector => _movementVector;
    public event Action OnPlayerMove;
    public event Action OnPlayerStop;

    private IInputReader _inputReader;
    private IEntityController _playerController;
    private IPlayerInteractionHandler _playerInteractionHandler;
    private Vector3 _movementVector;
    private float _movementSpeed;

    public InputHandler(IEntityController playerController, IPlayerInteractionHandler playerInteractionHandler, float movementSpeed)
    {
        _playerController = playerController;
        _playerInteractionHandler = playerInteractionHandler;
        _inputReader = new InputReader();
        _movementSpeed = movementSpeed;
        RegisterInteractionEvents();
    }

    public void Tick()
    {
        ReadMovement();
        UpdateTriggers();
    }

    public void FixedTick()
    {
        HandleMovement();
    }

    private void ReadMovement()
    {
        _movementVector = new Vector3(_inputReader.Movement.x, 0, _inputReader.Movement.y);
    }

    private void HandleMovement()
    {
        if (_movementVector == Vector3.zero)
            return;

        _playerController.EntityTransform.position += _movementVector * _movementSpeed * Time.deltaTime;
        _playerController.EntityTransform.forward = Vector3.Slerp(_playerController.EntityTransform.forward, _movementVector, Time.deltaTime * 10f) ; //for rotation
    }

    private void RegisterInteractionEvents()
    {
        _inputReader.OnInteract += _playerInteractionHandler.Interact;
        _inputReader.OnAlternateInteract += _playerInteractionHandler.SetAlternateInteract;
    }

    private void UpdateTriggers()
    {
        if (_movementVector != Vector3.zero)
        {
            OnPlayerMove?.Invoke();
        }
        else if ( _movementVector == Vector3.zero)
        {
            OnPlayerStop?.Invoke();
        }
    }
}
