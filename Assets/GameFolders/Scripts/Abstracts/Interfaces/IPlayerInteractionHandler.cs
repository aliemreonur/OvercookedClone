using UnityEngine;
using System;

//TODO: This is far far too long
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
