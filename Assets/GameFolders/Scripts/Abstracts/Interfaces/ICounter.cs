using UnityEngine;

public interface ICounter
{
    Vector3 CollectablePosition { get; }
    bool IsFilled { get; set; } //set is on
    void FoodSpawned(ICollectable collectable);
    void CollectableMovedToPlayer(IPlayerInteractionHandler playerInteractionHandler);
    void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler);
}
