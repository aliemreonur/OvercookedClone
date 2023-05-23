using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //TODO: Again, like spagetti code in between stove - processable and this.

    [SerializeField] private Image _fillImg;
    private IProcessable _processable;
    private float _currentRatio;
    private Color _currentColor;

    private void Awake()
    {
        _currentColor = _fillImg.color;
    }

    private void Update()
    {
        if (_processable == null)
            return;
        UpdateFill();
    }

    private void UpdateFill()
    {
        _currentRatio = _processable.ElapsedProcessTime / _processable.TotalProcessTime;
        if (_currentRatio > 1)
            _currentRatio = 1;
        _fillImg.fillAmount = _currentRatio;

        UpdateFillColor();
    }

    private void UpdateFillColor()
    {
        if (_currentRatio >= 1)
            _fillImg.color = Color.red;
        else
            _fillImg.color = _currentColor;
    }

    public void SetProcessable(IProcessable processable)
    {
        _processable = processable;
    }
}
