using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    public struct Pool
    {
        [HideInInspector] public List<GameObject> poolObjects;
        public GameObject objPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] _allObjPools; //All collectables
    public Vector3 spawnPos => _spawnPos;
    private Vector3 _spawnPos = new Vector3(500, 500, 500);

    protected override void Awake()
    {
        base.Awake();

        InitialSpawn();
    }

    public GameObject RequestObjFromThePool(int objId)
    {
        if (_allObjPools.Length < objId - 1)
            return null;

        foreach(var obj in _allObjPools[objId].poolObjects)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return ReturnNewObj(objId);
    }

    private void InitialSpawn()
    {

        for(int i =0; i<_allObjPools.Length; i++)
        {
            for(int j=0; j<_allObjPools[i].poolSize; j++)
            {
                GameObject spawnedObj = Instantiate(_allObjPools[i].objPrefab, _spawnPos, Quaternion.identity, transform);
                _allObjPools[i].poolObjects.Add(spawnedObj);
                spawnedObj.SetActive(false);
            }
        }

    }


    private GameObject ReturnNewObj(int id)
    {
        GameObject newObj = Instantiate(_allObjPools[id].objPrefab, _spawnPos, Quaternion.identity, transform);
        _allObjPools[id].poolObjects.Add(newObj);
        return newObj;
    }

}
