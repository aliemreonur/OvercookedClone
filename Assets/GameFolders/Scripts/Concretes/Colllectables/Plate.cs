using System.Collections.Generic;
using UnityEngine;
using System;

public class Plate : Collectable, IPlate
{
    #region Fields
    public event Action OnPlateDelivered;
    public Action<int> OnIngredientAdded;
    public int NumberOfFoodsActive => GatherCurrentActiveChildNumber();
    public FoodsOrder foodsOrder => _plateState.plateFoodsOrder;
    private IPlateState _plateState;

    private List<Transform> _allCollectables = new List<Transform>();
    private List<int> _activeCollectables = new List<int>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        SetInitials(0, false, 0);
        GetAllChildren();
    }
    private void OnEnable()
    {
        _plateState = new PlateState();
    }

    public override void DeActivate()
    {
        base.DeActivate();
        _activeCollectables.Clear();
        DeActivateChildren();
        InitialSetUp();
    }

    public void AddFoodToPlate(IPlayerInteractionHandler playerInteractionHandler, out bool hasSucceeded)
    {
        if (CheckForSuitableFood(playerInteractionHandler.currentCollectable))
        {
            playerInteractionHandler.currentCollectable.ChangePos(this.transform.position, transform);
            ICollectable collectable = playerInteractionHandler.currentCollectable;
            if(!playerInteractionHandler.HasPlateOnHand)
                playerInteractionHandler.DropFood();
            collectable.DeActivate();
            hasSucceeded = true;
            _plateState.UpdateFoodsOnPlate(_activeCollectables); 
        }
        else
            hasSucceeded = false;
    }

    public void Delivered()
    {
        OnPlateDelivered?.Invoke();
    }

    private void GetAllChildren()
    {
        foreach (Transform child in transform)
        {
            _allCollectables.Add(child);
            child.gameObject.SetActive(false);
        }
        InitialSetUp();
    }

    private void InitialSetUp()
    {
        if (_allCollectables.Count > 0)
        {
            _allCollectables[0].gameObject.SetActive(true);
            _activeCollectables.Add(0);
        }
    }

    private bool CheckForSuitableFood(ICollectable collectable)
    {
        if (_allCollectables[collectable.Id].gameObject.activeSelf)
            return false;

        else
        {
            //check if they are suitable for putting into the burger.
            if (!collectable.IsReadyToServe)
                return false;
            else
            {
                _allCollectables[collectable.Id].gameObject.SetActive(true);
                _activeCollectables.Add(collectable.Id);
                OnIngredientAdded?.Invoke(collectable.Id);
                return true;
            }
        }
    }

    private int GatherCurrentActiveChildNumber()
    {
        int currentActive = 0;
        foreach(Transform child in transform)
        {
            if(child.gameObject.activeInHierarchy)
            {
                currentActive++;
            }
        }
        return currentActive;
    }

    private void DeActivateChildren()
    {
        foreach(Transform childObj in _allCollectables)
        {
            childObj.gameObject.SetActive(false);
        }
    }

}
