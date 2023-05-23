using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Singleton<PlayerController>, IEntityController
{
    //better to mvoe these to a settings script
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _interactionCooldownTime = 0.5f;

    public Transform EntityTransform => transform;
    public bool IsWalking => _inputHandler.MovementVector != Vector3.zero;

    private IInputHandler _inputHandler;
    private IStateMachine _stateMachine;
    private IPlayerInteractionHandler _playerInteractionHandler;

    private Animator _animator;
    private GameManager _gameManager;

    protected override void Awake()
    {
        base.Awake();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("The player could not gather its Animator");

        _playerInteractionHandler = new PlayerInteractionHandler(this);
        _inputHandler = new InputHandler(this, _playerInteractionHandler, _movementSpeed);
        _stateMachine = new StateMachine(_animator, _inputHandler);
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (!_gameManager.IsRunning)
            return;

        _inputHandler.Tick();
        if(Time.frameCount %2 == 0)
            _playerInteractionHandler.DetectInteractable();

        if (_playerInteractionHandler.IsAlternateInteracting)
            _playerInteractionHandler.AlternateInteract();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(_playerInteractionHandler.HasFoodOnHand);
            Debug.Log("plate: " + _playerInteractionHandler.HasPlateOnHand);
        }

        TempBugFix();
    }

    private void FixedUpdate()
    {
        _inputHandler.FixedTick();
    }

    private void OnDisable()
    {
        if (_stateMachine == null)
            return;

        _stateMachine.UnSubscribe();
    }

    //BUG: boştayken interact!
    //this bug fix is for a bug that results in carrying an active object on hand 
    private void TempBugFix()
    {
        if(!_playerInteractionHandler.HasFoodOnHand && !_playerInteractionHandler.HasPlateOnHand)
        {
            if(transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
                Debug.Log("Bug prevented, find out!");
            }
        }
    }
}
