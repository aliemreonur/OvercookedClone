using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrderUIManager : MonoBehaviour
{
    private List<OrderUI> _orderUIs = new List<OrderUI>();
    private OrderManager _orderManager;

    /// <summary>
    /// TODO: Need to order the orders by the order ID's.
    /// </summary>
    

    private void Start()
    {

        AddChildsToList();
        _orderManager = OrderManager.Instance;
        _orderManager.OnNewOrderGathered += OrderGathered;
        _orderManager.OnOrderFulfilled += OrderFulfilled;
    }

    private void AddChildsToList()
    {
        foreach(Transform childObj in transform)
        {
            if(childObj.gameObject.TryGetComponent(out OrderUI orderUIObj))
            {
                _orderUIs.Add(orderUIObj);
                orderUIObj.gameObject.SetActive(false);
            }
        }
    }

    private void OrderGathered(int orderNo, FoodsOrder ingredients)
    {
        foreach(OrderUI childObj in _orderUIs)
        {
            if (!childObj.isActiveAndEnabled)
            {
                childObj.SetOrderDetails(orderNo, ingredients);
                //give the order details to the obj
                childObj.gameObject.SetActive(true);
                break;
            }
   
        }

        _orderUIs = _orderUIs.OrderBy(go => go.orderID).ToList();
    }

    private void OrderFulfilled(FoodsOrder fulfilledOrder)
    {
        foreach(var orderUI in _orderUIs)
        {
            if (orderUI.currentOrder == fulfilledOrder)
                orderUI.OrderFulFilled();
        }
    }

    private void OnDisable()
    {
        _orderManager.OnNewOrderGathered -= OrderGathered;
        _orderManager.OnOrderFulfilled -= OrderFulfilled;
    }
}
