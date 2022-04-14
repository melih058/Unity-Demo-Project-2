using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Transform> _nextDirections;
    [SerializeField] private bool _isOccupied;

    private void Start()
    {
        onInitialize();
    }

    private void onInitialize()
    {
        _isOccupied = false;
    }

    private Transform getAvailablePoint()
    {
        if (_nextDirections == null || _nextDirections.Count <= 0)
        {
            return null;
        }


        for (int i = 0; i < _nextDirections.Count; i++)
        {
            Transform nextDirection = _nextDirections[i];
            if (!nextDirection.GetComponent<Node>().isOccupied)
            {
                return nextDirection;
            }
        }

        return null;
    }

    public bool isOccupied
    {
        get
        {
            return _isOccupied;
        }

        set
        {
            _isOccupied = value;
        }
    }
    public Transform _getAvailablePoint() => getAvailablePoint();

}
