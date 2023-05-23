using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _startPanel;
    private GameManager _gameManager;

    protected override void Awake()
    {
        base.Awake();
        _gameManager = GameManager.Instance;
    }

    public void StartGame()
    {
        _gameManager.StartGame();
        _startPanel.SetActive(false);
    }
}
