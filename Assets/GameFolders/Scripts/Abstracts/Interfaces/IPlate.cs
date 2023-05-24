using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlate : ICollectable
{
    event Action OnPlateDelivered;
    FoodsOrder foodsOrder { get; }
    int NumberOfFoodsActive { get; }
    void AddFoodToPlate(IPlayerInteractionHandler playerInteractionHandler, out bool hasSucceeded);
    void Delivered();
}
