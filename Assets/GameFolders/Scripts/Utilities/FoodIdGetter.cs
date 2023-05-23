using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodIdGetter 
{
    private static Dictionary<string, int> _foodIdPairs = new Dictionary<string, int> {
        {"Bread", 1},
        {"Lettuce", 2},
        {"Cheese", 3},
        {"Meat", 4},
        {"Tomato", 5}
    };

    public static int ReturnFoodId(string foodName)
    {
        int valueToReturn = 0;
        _foodIdPairs.TryGetValue(foodName, out valueToReturn);
        return valueToReturn;     
    }

}
