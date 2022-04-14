using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject _successPanel;
    private GridManager _gridManager;

    private GameManager()
    {

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        onInitialize();
    }

    private void onInitialize()
    {
        _gridManager = GridManager.getInstances();
        _gridManager.onInitalize();
        _successPanel.SetActive(false);
    }

    public static GameManager getInstances()
    {
        return _instance;
    }

    public void setPanel(bool isLevelSuccess)
    {
        if (isLevelSuccess)
        {
            _successPanel.SetActive(true);
        }
        else
        {
            // fail state
        }
    }
}
