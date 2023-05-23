using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Delivery : Counter
{
    private GameManager _gameManager;
    private IPlate _deliveredPlate;

    protected override void Awake()
    {
        base.Awake();
        _gameManager = GameManager.Instance;
    }

    public override void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {

        if(CheckValidInteraction(playerInteractionHandler))
        {
            _deliveredPlate = playerInteractionHandler.currentPlate;
            base.GatherCollectableFromPlayer(playerInteractionHandler);
            _gameManager.SuccessfulDelivery();
            Debug.Log("Valid delivery");

            if(_deliveredPlate != null)
            {
                _deliveredPlate.DeActivate();
            }

            IsFilled = false;
            hasPlate = false;
            _deliveredPlate = null;
            playerInteractionHandler.DropFood();

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

        if (playerInteractionHandler.currentPlate.NumberOfFoodsActive < 6)
            return false;

        else if (playerInteractionHandler.currentPlate.foodsOrder == FoodsOrder.All)
            return true;
        else
            return false;
        
    }
}
