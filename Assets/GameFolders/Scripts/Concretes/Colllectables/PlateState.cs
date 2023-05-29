using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateState : IPlateState
{
    public FoodsOrder plateFoodsOrder => _foodsOrder;
    private FoodsOrder _foodsOrder = FoodsOrder.None;

    public void UpdateFoodsOnPlate(List<int> _allChilds)
    {
        for(int i=0; i<_allChilds.Count; i++)
        {
            if (_allChilds.Count == 6)
                _foodsOrder = FoodsOrder.All;
            else if (_allChilds.Contains(0) && _allChilds.Contains(1) && _allChilds.Contains(3) && _allChilds.Contains(4) && _allChilds.Contains(5))
                _foodsOrder = FoodsOrder.MeatCheeseTomato;
            else if (_allChilds.Contains(0) && _allChilds.Contains(1) && _allChilds.Contains(3) && _allChilds.Contains(4) && _allChilds.Contains(2))
                _foodsOrder = FoodsOrder.MeatCheeseLettuce;
            else if (_allChilds.Contains(0) && _allChilds.Contains(1) && _allChilds.Contains(3) && _allChilds.Contains(4))
                _foodsOrder = FoodsOrder.MeatCheese;
            else if (_allChilds.Contains(0) && _allChilds.Contains(1) && _allChilds.Contains(4))
                _foodsOrder = FoodsOrder.Meat;
            else if (_allChilds.Contains(2) && _allChilds.Contains(5))
                _foodsOrder = FoodsOrder.Salad;
            else
                _foodsOrder = FoodsOrder.None;
        }
    }

}
