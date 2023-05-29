using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    public FoodsOrder currentOrder;
    public int orderID;
    private TextMeshProUGUI _orderIDText;
    private List<GameObject> _childObjects = new List<GameObject>();

    private void Awake()
    {
        _orderIDText = GetComponentInChildren<TextMeshProUGUI>();
        if (_orderIDText == null)
            Debug.LogError("The order could not gather its text");
        GatherObjImages();
    }

    public void OrderFulFilled()
    {
        foreach(GameObject obj in _childObjects)
        {
            obj.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void SetOrderDetails(int orderId, FoodsOrder ingredients)
    {
        orderID = orderId;
        currentOrder = ingredients;
        _orderIDText.gameObject.SetActive(true);
        _orderIDText.text = "# " + orderId;
        ActivateOrderObjects(FoodOrderMatcher.DetermineFoodItems(ingredients));
    }

    private void GatherObjImages()
    {
        foreach(Transform childObj in transform)
        {
            //first obj is always the text
            _childObjects.Add(childObj.gameObject);
            childObj.gameObject.SetActive(false);
        }

    }

    private void ActivateOrderObjects(List<int> objectIds)
    {
        foreach(int i in objectIds)
        {
            _childObjects[i].gameObject.SetActive(true);
        }
    }

    
}
