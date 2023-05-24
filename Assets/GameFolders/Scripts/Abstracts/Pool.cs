using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pool
{
    [HideInInspector] public List<GameObject> poolObjects;
    public GameObject objPrefab;
    public int poolSize;
}
