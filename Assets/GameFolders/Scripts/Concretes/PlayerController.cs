using UnityEngine;
using System;

public class PlayerController : Singleton<PlayerController>, IPlayerController
{
    [SerializeField] private float _movementSpeed = 5f;

    public IPlayerInteractionHandler playerInteractionHandler => _playerInteractionHandler;
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

}
