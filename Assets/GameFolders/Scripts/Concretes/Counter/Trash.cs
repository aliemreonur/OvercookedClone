using UnityEngine;
using System;

public class Trash : Counter
{
    public override void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        base.GatherCollectableFromPlayer(playerInteractionHandler);
        collectable.Trashed();

        if (collectable.Id == 0)
            Debug.Log("Plate trashed, need to spawn a new plate now");

        IsFilled = false;
        collectable = null;
    }

    
}