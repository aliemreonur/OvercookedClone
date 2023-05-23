using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//too long!
//FAR TOO LONG!
public interface IPlayerInteractionHandler : IEntityController
{
    ICollectable currentCollectable { get; }
    event Action OnAlternateEnd;
    event Action OnSuccessfulFoodTake;
    Vector3 PlayerFoodPosition { get; }
    bool IsAlternateInteracting { get; }
    bool HasFoodOnHand { get; }
    bool HasPlateOnHand { get; }
    IPlate currentPlate { get; }

    void DetectInteractable();
    void Interact();
    void AlternateInteract();
    void GatherFood(ICollectable collectable);
    void SetAlternateInteract(bool isOn);
    void DropFood();
}
