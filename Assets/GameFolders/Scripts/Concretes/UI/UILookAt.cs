using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILookAt : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    RectTransform _rectTransform;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(_mainCamera.transform);
        //_rectTransform.rotation = Quaternion.identity;
    }
}
