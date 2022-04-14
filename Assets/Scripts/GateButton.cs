using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IInteractable
{
    void onInteract();
}

public class GateButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Gate _connectedGate;
    [SerializeField] private Transform _buttonTransform;
    private bool _canInteract;
    [SerializeField] private float _buttonDuration;
    [SerializeField] private float _pushedPosition;
    [SerializeField] private float _nonPushedPosition;


    private void Start()
    {
        _canInteract = true;
    }
    public void onInteract()
    {
        if (_canInteract)
        {
            _canInteract = false;
            _buttonTransform.DOLocalMoveY(_pushedPosition, _buttonDuration).OnComplete(() =>
            {
                _connectedGate.onInteract();
                _buttonTransform.DOLocalMoveY(_nonPushedPosition, _buttonDuration).OnComplete(() => { _canInteract = true; });
            });
        }

    }
}
