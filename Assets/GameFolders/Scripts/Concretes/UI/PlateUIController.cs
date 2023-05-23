using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateUIController : MonoBehaviour
{
    private Plate _currentPlate;
    private List<Transform> _allChilds = new List<Transform>();

    private void Awake()
    {
        //GatherAllUIChilds();
        _currentPlate = GetComponentInParent<Plate>();
        if (_currentPlate == null)
            Debug.LogError("The plate is null");

        _currentPlate.OnIngredientAdded += ActivateIngredient;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnDisable()
    {
        _allChilds.Clear();
    }

    private void OnEnable()
    {
        GatherAllUIChilds();
    }


    private void ActivateIngredient(int id)
    {
        if (_allChilds.Count < 1 || _allChilds.Count<id-1)
            return;

        _allChilds[id-1].gameObject.SetActive(true);
    }

    private void GatherAllUIChilds()
    {
        foreach(Transform child in transform)
        {
            _allChilds.Add(child);
            child.gameObject.SetActive(false);
        }
    }
    
}
