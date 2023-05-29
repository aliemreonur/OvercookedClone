using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodOrderMatcher 
{
    private static List<int> orderList = new List<int>();

    public static List<int> DetermineFoodItems(FoodsOrder foodsOrder)
    {
        orderList.Clear();

        switch(foodsOrder)
        {
            case FoodsOrder.None:
                break;
            case FoodsOrder.Salad:
                {
                    orderList.Add(FoodIdGetter.ReturnFoodId("Lettuce"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Tomato"));
                    break;
                }
            case FoodsOrder.Meat:
                {
                    orderList.Add(FoodIdGetter.ReturnFoodId("Bread"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Meat"));
                    break;
                }
            case FoodsOrder.MeatCheese:
                {
                    orderList.Add(FoodIdGetter.ReturnFoodId("Bread"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Meat"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Cheese"));
                    break;
                }
            case FoodsOrder.MeatCheeseTomato:
                {
                    orderList.Add(FoodIdGetter.ReturnFoodId("Bread"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Meat"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Cheese"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Tomato"));
                    break;
                }
            case FoodsOrder.MeatCheeseLettuce:
                {
                    orderList.Add(FoodIdGetter.ReturnFoodId("Bread"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Meat"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Cheese"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Lettuce"));
                    break;
                }
            default: //all
                {
                    orderList.Add(FoodIdGetter.ReturnFoodId("Bread"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Meat"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Cheese"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Lettuce"));
                    orderList.Add(FoodIdGetter.ReturnFoodId("Tomato"));
                    break;
                }
        }

        return orderList;
    }
}
