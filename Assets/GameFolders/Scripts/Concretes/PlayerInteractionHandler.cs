using UnityEngine;
using System;

//this would be better as mono.
public class PlayerInteractionHandler : IPlayerInteractionHandler
{
    #region Fields
    [SerializeField] private Vector3 _foodCarryPos;
    [SerializeField] private LayerMask _interactableLayers;
    public event Action OnAlternateEnd; //redo of inputhandler
    public event Action OnSuccessfulFoodTake;
    public bool HasFoodOnHand { get; private set; }
    public bool HasPlateOnHand { get; private set; }
    public bool IsAlternateInteracting { get; private set; }
    public Vector3 PlayerFoodPosition => _foodCarryPos;
    public ICollectable currentCollectable => _collectable;
    public IPlate currentPlate => _currentPlate;
    public Transform EntityTransform => _playerTransform;

    private IInteractable _currentInteractedObj;
    private IEntityController _playerController;
    private ICollectable _collectable;
    private IPlate _currentPlate;

    private const float InteractionDistance = 1f;
    private Transform _playerTransform;
    #endregion

    public PlayerInteractionHandler(IEntityController playerController)
    {
        _playerController = playerController;
        _playerTransform = _playerController.EntityTransform;
        _foodCarryPos = new Vector3(0, .8f, .9f); //the local position
    }

    public void DetectInteractable()
    {
        RaycastHit hitRay;
        if (CheckFrontForInteraction(out hitRay))
        {
            if (hitRay.transform.TryGetComponent(out IInteractable interactable))
            {
                DeselectCurrentObj();
                interactable.Select();
                _currentInteractedObj = interactable;
            }
        }
        else
        {
            DeselectCurrentObj();
            _currentInteractedObj = null;
        }
    }

    public void Interact()
    {
        //TODO: Fix 3 degrees
        if(_currentInteractedObj == null)
        {
            if (HasFoodOnHand)
            {
                _collectable.Throw();

                _collectable = null;
                HasFoodOnHand = false;
                if (HasPlateOnHand)
                {
                    HasPlateOnHand = false;
                    _currentPlate = null;
                }
            }
         
            else
                return;
        }
        else
            _currentInteractedObj.HandleInteraction(this);
    }

    public virtual void AlternateInteract() 
    {
        if (_currentInteractedObj == null)
            return;

        _currentInteractedObj.HandleAlternateInteraction(this);
    }

    public void GatherFood(ICollectable collectable)
    {
        //TODO: State Mechanism
        if (HasFoodOnHand || collectable == null)
            return;

        
        if (collectable.Id == 0 && !HasPlateOnHand)
        {
            HasPlateOnHand = true;
            AssignCollectable(collectable);
            _currentPlate = (IPlate)collectable;
        }
        else if (HasPlateOnHand)
        {
            bool hasSucceeded = false;
            _collectable = collectable; //?!?
            _currentPlate.AddFoodToPlate(this, out hasSucceeded);
            _collectable = _currentPlate;
            if (hasSucceeded)
                OnSuccessfulFoodTake?.Invoke();
            else
                DropFood();
        }
        else
        {
            HasFoodOnHand = true;
            AssignCollectable(collectable);
        }

        if (collectable != null)
            collectable.OnTrashed += DropFood;
       
    }

    public void SetAlternateInteract(bool isOn)
    {
        IsAlternateInteracting = isOn;
        if (!isOn)
            OnAlternateEnd?.Invoke();
    }

    public void DropFood()
    {
        HasPlateOnHand = false;
        _currentPlate = null;
        _collectable = null;
        HasFoodOnHand = false;
    }

    private void DeselectCurrentObj()
    {
        if (_currentInteractedObj != null)
            _currentInteractedObj.DeSelect();
    }

    private void AssignCollectable(ICollectable collectable)
    {
        _collectable = collectable;
        collectable.ChangePos(_foodCarryPos, _playerTransform);
        OnSuccessfulFoodTake?.Invoke();
    }

    private bool CheckFrontForInteraction(out RaycastHit hitRay)
    {
        Debug.DrawRay(_playerTransform.position, _playerTransform.forward, Color.blue);
        return Physics.Raycast(_playerTransform.position + Vector3.up, _playerTransform.forward, out hitRay, InteractionDistance, 1 << 6);
    }

}
