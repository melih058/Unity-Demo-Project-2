using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    private bool _isEngineWork;
    [SerializeField] private float _speedFactor;
    [SerializeField] private float _breakDistance;
    private Transform _targetPathPoint;
    private Transform _previousTargetPathPoint;
    private float _initTotalDistance;
    private float distance;
    private Quaternion _initRotation;
    [SerializeField] private AnimationCurve _speedCurve;
    private float _speedEvaluateTime;
    private float _maxEvaluateTime;
    [SerializeField] private Image _checkedImage;
    public CarType carType;

    private void Start()
    {
        onInitialize();
    }

    private void Update()
    {
        if (!_isEngineWork)
            return;

        Vector3 distanceVector = _targetPathPoint.position - transform.position;
        distanceVector.y = 0f;

        distance = distanceVector.magnitude;
        Quaternion targetRotation = _targetPathPoint.rotation;

        if (_speedEvaluateTime < _maxEvaluateTime)
        {
            _speedEvaluateTime += _speedFactor * Time.deltaTime;
        }


        if (distance <= _breakDistance)
        {
            _previousTargetPathPoint = _targetPathPoint;
            Node previousNode = _previousTargetPathPoint.GetComponent<Node>();
            _targetPathPoint = previousNode._getAvailablePoint();

            if (_targetPathPoint == null)
            {
                previousNode.isOccupied = true;
                _isEngineWork = false;

                if (checkGridType())
                {
                    _checkedImage.enabled = true;
                    _checkedImage.transform.DOScale(1.2f, 0.1f).OnComplete(() =>
                    {
                        _checkedImage.transform.DOScale(1f, 0.1f);
                        GridManager gridManager = GridManager.getInstances();
                        gridManager.checkGrids();
                    });
                }

            }
            else
            {
                _initRotation = transform.rotation;
                _initTotalDistance = (_targetPathPoint.position - transform.position).magnitude;
            }
        }

        Vector3 directionVector = distanceVector.normalized;
        directionVector.y = 0f;

        transform.rotation = Quaternion.Slerp(_initRotation, targetRotation, ((_initTotalDistance - distance) / _initTotalDistance));


        transform.position += directionVector * _speedCurve.Evaluate(_speedEvaluateTime) * Time.deltaTime;
    }


    private void onInitialize()
    {
        _targetPathPoint = null;
        _isEngineWork = false;
        _speedEvaluateTime = 0;
        _maxEvaluateTime = _speedCurve.keys[1].time;
        _checkedImage.enabled = false;
    }

    private void setInitPath(Transform targetPathPoint)
    {
        _targetPathPoint = targetPathPoint;
        _isEngineWork = true;
    }

    private bool checkGridType()
    {
        int layerMask = 1 << 7;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, layerMask);
        if(colliders.Length > 0)
        {
            Grid grid = colliders[0].GetComponent<Grid>();
            if (grid.carType == carType)
            {
                grid.isCompeted = true;
                return true;
            }
        }

        return false;
    }

    public void _setInitPath(Transform targetPathPoint) => setInitPath(targetPathPoint);



}
