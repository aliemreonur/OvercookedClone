using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Delivery : Counter
{
    private OrderManager _orderManager;

    private void Start()
    {
        _orderManager = OrderManager.Instance;
        if (_orderManager == null)
            Debug.LogError("The delivery counter could not gather the order manager");
    }

    public override void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        if(CheckValidInteraction(playerInteractionHandler))
        {
            FoodsOrder fulfilledPlate = playerInteractionHandler.currentPlate.foodsOrder;
            _orderManager.OrderFulfilled(fulfilledPlate);
            if (playerInteractionHandler.currentPlate == null)
                return;
            playerInteractionHandler.currentPlate.DeActivate();
            base.GatherCollectableFromPlayer(playerInteractionHandler);
            IsFilled = false;
            hasPlate = false;
        }
    }

    public override void CollectableMovedToPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        //empty on purpose - this counter cannot give anything to the player
    }

    protected override bool CheckValidInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!playerInteractionHandler.HasPlateOnHand)
            return false;

        //else if (_orderManager.CheckPlate(playerInteractionHandler.currentPlate.foodsOrder))
        else if(_orderManager.activeOrders.Contains(playerInteractionHandler.currentPlate.foodsOrder))
            return true;
        else
            return false;
        
    }
}
