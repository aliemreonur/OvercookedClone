using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Interactable, ICounter
{
    /// <summary>
    /// Need to make sure that all of the interactable objs has the visual selected object as the getchild(1)
    /// </summary>
    public Vector3 CollectablePosition => collectablePosition;
    public bool IsFilled { get; set; }

    protected ICollectable collectable;
    protected IPlate plate;
    protected Vector3 collectablePosition = new Vector3(0, 1.3f, 0);
    protected bool hasPlate = false;
    private WaitForSeconds deregisterTime = new WaitForSeconds(1);


    //need to check if we are equipped with a plate

    public override void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!CheckValidInteraction(playerInteractionHandler))
            return;

        //if I am empty, and player has smt, take it to here
        //else if I have a plate and player is empty, give the plate to the player
        //else if I have a plate and player has a food, take the food

        if ((!IsFilled && !hasPlate) || (hasPlate && playerInteractionHandler.HasFoodOnHand))
        {
            GatherCollectableFromPlayer(playerInteractionHandler);
        }
        else
            CollectableMovedToPlayer(playerInteractionHandler);
        
    }

    //only for dispenser, shall it really placed in here?
    public virtual void FoodSpawned(ICollectable collectable)
    {
        if (this.collectable != null)
            return;

        IsFilled = true;
        this.collectable = collectable;
    }

    public virtual void CollectableMovedToPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (collectable == null)
            return;

        playerInteractionHandler.OnSuccessfulFoodTake += CounterEmpty;
        playerInteractionHandler.GatherFood(collectable);
        StartCoroutine(DeregisterFromEvent(playerInteractionHandler));
    }

    protected void CounterEmpty()
    {
        IsFilled = false;
        collectable = null;
        if (hasPlate)
            hasPlate = false;
    }

    public virtual void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        if(!hasPlate)
        {
            collectable = playerInteractionHandler.currentCollectable;
            collectable.ChangePos(collectablePosition, transform);
           
            //IsFilled = true;
            if (collectable.Id == 0)
            {
                plate = (IPlate)collectable;
                hasPlate = true;
            }
            else
                IsFilled = true;
            playerInteractionHandler.DropFood();
        }
        else //this counter has a plate
        {
            bool successfulTake = false;
            if (playerInteractionHandler.currentCollectable.Id == 0) //if the player also has a plate, skip
                return;
            plate.AddFoodToPlate(playerInteractionHandler, out successfulTake);
            //?!?
            
        }
    }

    //too many ifs here
    protected virtual bool CheckValidInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        bool hasFood = playerInteractionHandler.HasFoodOnHand;
        if(!hasPlate)
        {
            if (hasFood && IsFilled || (!hasFood && !IsFilled && !playerInteractionHandler.HasPlateOnHand))
                return false;
            else
                return true;
        }
        else
        {
            if (playerInteractionHandler.HasPlateOnHand)
            {
                return false;
            }
            else
                return true;

        }


    }

    private IEnumerator DeregisterFromEvent(IPlayerInteractionHandler playerInteractionHandler)
    {
        yield return deregisterTime;
        playerInteractionHandler.OnSuccessfulFoodTake -= CounterEmpty;
    }
}
