using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trash : Counter
{
    public Action OnPlateTrash;

    public override void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        base.GatherCollectableFromPlayer(playerInteractionHandler);
        //can use dgtweening on trashed
        collectable.Trashed();

        if (collectable.Id == 0)
            Debug.Log("Plate trashed, need to spawn a new plate now");

        IsFilled = false;
        collectable = null;
    }

    
}