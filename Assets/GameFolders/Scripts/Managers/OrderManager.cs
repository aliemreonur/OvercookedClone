using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : Singleton<OrderManager>
{

    /// <summary>
    /// TODO: The all order - plate system has to be checked 
    /// </summary>
    [SerializeField] private int _numberOfMaxActiveOrders = 5;
    [SerializeField] private float _orderCoolDownTime = 15f; //
    public List<FoodsOrder> activeOrders = new List<FoodsOrder>();
    public Action<int, FoodsOrder> OnNewOrderGathered;
    public Action<FoodsOrder> OnOrderFulfilled;

    private int _orderId = 1;
    private float _lastSpawnTime;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnGameStart += AddNewOrder;
    }

    private void Update()
    {
        //check if we can spawn a new order
        if (!CheckSpawnable())
            return;

        if(Time.time >=_lastSpawnTime + _orderCoolDownTime)
        {
            AddNewOrder();
            _lastSpawnTime = Time.time;
        }

    }

    private bool CheckSpawnable()
    {
        return activeOrders.Count < _numberOfMaxActiveOrders;
    }

    private void AddNewOrder()
    {
        FoodsOrder newOrder = AssignRandomOrder();
        activeOrders.Add(newOrder);
        OnNewOrderGathered(_orderId, newOrder);
        _orderId++;
        //Update UI
    }

    public void OrderFulfilled(FoodsOrder foodsOrder)
    {
        if(activeOrders.Contains(foodsOrder))
        {
            activeOrders.Remove(foodsOrder);
            _gameManager.SuccessfulDelivery();
            OnOrderFulfilled?.Invoke(foodsOrder);
            //deactivate UI
            //start the timer for new order
        }
    }

    public bool CheckPlate(FoodsOrder foodsOrder)
    {
        foreach(var foods in activeOrders)
        {
            Debug.Log("Checking: " +  (int)foods + " with: " + (int)foodsOrder);
            if ((int)foodsOrder == (int)foods)
                return true;
        }
        return false;
    }

    private FoodsOrder AssignRandomOrder()
    {
        var numberOfEnums = Enum.GetNames(typeof(FoodsOrder)).Length;
        int randomId = UnityEngine.Random.Range(1, numberOfEnums);
        FoodsOrder newOrder = (FoodsOrder)randomId;
        return newOrder;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStart -= AddNewOrder;
    }

}
