using System.Collections.Generic;
using UnityEngine;
using System;

public class Processable : Collectable, IProcessable
{
    #region Fields
    public event Action OnProcessFinished;
    public float TotalProcessTime => _totalProcessTime;
    public float ElapsedProcessTime => _usedProcessTime;

    protected bool isProcessActive;
    private List<GameObject> _childObjects = new List<GameObject>();
    private IPlayerInteractionHandler _playerInteractionHandler;
    private bool _processed;
    private float _totalProcessTime = 1.5f; //better to gather this via settings as a const?
    private float _usedProcessTime = 0;
    private int _currentId;
    private int _childCount;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _childCount = transform.childCount;
        SetMainVisual();
    }
    override protected void Update()
    {
        base.Update();
        if(isProcessActive)
            CheckProcessTime();
    }
    private void OnDisable()
    {
        _currentId = 0;
        IsProcessable = true;
        _usedProcessTime = 0;
        _processed = false;
    }
    private void OnEnable()
    {
        SetInitialVisual();
    }
    public void ProcessActive()
    {
        if (_usedProcessTime < _totalProcessTime)
            isProcessActive = true;
    }

    public void ProcessStopped()
    {
        isProcessActive = false;
    }

    private void Processed()
    {
        _processed = true;
        if (_childCount <= _currentId + 1)
            return;

        OnProcessFinished?.Invoke(); //this is being called 400 times?
        UpdateProcessedVisual();

        //if we have a next state, it needs to continue
        if (CheckForNextState())
        {
            _processed = false;
            _usedProcessTime = 0;
            ProcessActive();
        }
        else
            IsProcessable = false;
    }
    protected void UpdateProcessedVisual()
    {
        _childObjects[_currentId].SetActive(false);
        _currentId++;
        StateUpdated();
        _childObjects[_currentId].SetActive(true);
    }
    protected bool CheckForNextState()
    {
        if (_currentId >= _childObjects.Count - 1)
            return false;
        else
            return true;
    }

    protected void SetMainVisual()
    {
        if (_childCount == 1)
            return;

        if (_childObjects.Count == 0)
        {
            AddChildsToList();
        }
        SetInitialVisual();
    }

    private void SetInitialVisual()
    {
        foreach (GameObject child in _childObjects)
        {
            child.SetActive(false);
        }
        _childObjects[0].SetActive(true);
    }

    private void AddChildsToList()
    {
        for (int i = 0; i < _childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            _childObjects.Add(obj);
        }
    }

    protected bool CheckProcessable()
    {
        if (_currentId >= _childCount - 1)
        {
            IsProcessable = false;
            return false;
        }

        else
            return true;
    }

    protected bool CheckProcessedTime()
    {
        if (_usedProcessTime >= _totalProcessTime)
            return false;
        else
            return true;
    }

    private void CheckProcessTime()
    {
        _usedProcessTime += Time.deltaTime;
        if (_usedProcessTime >= _totalProcessTime && !_processed)
        {
            _processed = true;
            isProcessActive = false;
            _usedProcessTime = _totalProcessTime;
            Processed();
        }
    }
}
