using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;

    [SerializeField] private List<Grid> _grids;
    private GameManager _gameManager;
    private GridManager()
    {

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void onInitalize()
    {
        _gameManager = GameManager.getInstances();
    }

    public static GridManager getInstances()
    {
        return _instance;
    }

    public void checkGrids()
    {
        for (int i = 0; i < _grids.Count; i++)
        {
            if (!_grids[i].isCompeted)
            {
                return;
            }
        }
        _gameManager.setPanel(true);

    }
}
