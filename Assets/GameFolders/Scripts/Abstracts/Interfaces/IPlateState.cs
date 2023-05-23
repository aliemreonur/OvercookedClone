using System;
using System.Collections.Generic;

public interface IPlateState
{
    FoodsOrder plateFoodsOrder { get; }
    void UpdateFoodsOnPlate(List<int> childList);
}