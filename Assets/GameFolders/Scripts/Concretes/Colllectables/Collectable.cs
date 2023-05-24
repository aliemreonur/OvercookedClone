using UnityEngine;
using System;
using DG.Tweening;

public class Collectable : MonoBehaviour, ICollectable, IThrowable
{
    #region Fields
    public event Action OnTrashed;
    public Transform EntityTransform => transform; 
    public ICuttable processable;
    public bool IsProcessable { get; set; }
    public bool IsReadyToServe { get; private set; }
    public int ServableState { get; private set; }
    public int InitialPoolSize => _initialPoolSize;
    [field: SerializeField] public int Id { get; private set; }


    private int _currentState = 0;
    private int _initialPoolSize = 8;
    private bool _isGrounded = false;
    private Transform _parentTransform;
    private GameManager _gameManager;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    #endregion

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
        _collider.enabled = false;
        _parentTransform = transform.parent;
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
            _collider.enabled = true;
        }

    }

    public virtual void DeActivate()
    {
        IsReadyToServe = false;
        _currentState = 0;
        _collider.enabled = false;
        gameObject.SetActive(false);
        transform.position = new Vector3(500, 500, 500);
        transform.parent = _parentTransform;

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
        _collider.enabled = false;
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

    public void Throw() //more like drop to floor
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
        //for now, rather than interacting, player will automatically gather the collectable if in contact range
        if (other.TryGetComponent(out IPlayerController playerController))
        {
            IPlayerInteractionHandler playerInteractionHandler = playerController.playerInteractionHandler;
            if (playerInteractionHandler == null)
                return;

            if (playerInteractionHandler.HasPlateOnHand || !playerInteractionHandler.HasFoodOnHand)
                playerInteractionHandler.GatherFood(this);
        }
    }

}
