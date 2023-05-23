using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Collectable : MonoBehaviour, ICollectable, IThrowable
{
    public event Action OnTrashed;
    public Transform EntityTransform => transform; 
    public ICuttable processable;
    [field: SerializeField] public int Id { get; private set; }
    public bool IsProcessable { get; set; }
    public bool IsReadyToServe { get; private set; }
    public int ServableState { get; private set; }
    public int InitialPoolSize => _initialPoolSize;

    public static Dictionary<int, Collectable> collectablesPool = new Dictionary<int, Collectable>();

    private GameManager _gameManager;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    private int _currentState = 0;
    private int _initialPoolSize = 8;

    private bool _isGrounded = false;

    protected virtual void Awake()
    {
        _gameManager = GameManager.Instance;
        if (_gameManager == null)
            Debug.Log("The game manager is null");

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            Debug.Log("The collectable could not gather its rigidbody");

        _collider = GetComponent<BoxCollider>();

        if (_collider == null)
            Debug.Log("Collider is null");

    }

    public virtual void DeActivate()
    {
        gameObject.SetActive(false);
        IsReadyToServe = false;
        _currentState = 0;
    }

    public void SetInitials(int id, bool processable, int servableState)
    {
        Id = id;
        IsProcessable = processable;
        ServableState = servableState;
        if (servableState == 0)
            IsReadyToServe = true;
    }

    public void ChangePos(Vector3 posToMove, Transform parentTransform)
    {
        transform.parent = parentTransform;
        transform.localPosition = posToMove;
        transform.rotation = Quaternion.identity;
    }

    public void Trashed()
    {
        transform.DOMoveY(0, 0.9f);
        transform.DOScale(0, 1f).OnComplete(() => DeActivate());
        OnTrashed?.Invoke();
        //also a trash sound effect
    }

    public void Pickup(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (playerInteractionHandler.HasFoodOnHand)
            return;

        ChangePos(playerInteractionHandler.PlayerFoodPosition, playerInteractionHandler.EntityTransform);
    }


    protected virtual void Update()
    {
        if (_isGrounded || !_gameManager.IsRunning)
            return;

        if (transform.position.y < 0.1f)
        {
            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
            _isGrounded = true;
        }
            
    }

    public void Throw()
    {
        
        if (_rigidbody == null)
            return;
        
        transform.parent = null;
        //_collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _isGrounded = false;
        //_rigidbody.AddRelativeForce(Vector3.forward * 50000f * Time.deltaTime);
        
    }

    public void StateUpdated()
    {
        _currentState++;
        if (_currentState == ServableState)
            IsReadyToServe = true;
        else
            IsReadyToServe = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IEntityController playerController))
        {
            //Debug.Log("Player!");
        }
    }

}
