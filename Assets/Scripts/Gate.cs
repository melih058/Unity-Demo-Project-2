using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _gateDoor;
    [SerializeField] private float _gateDoorDuration;
    [SerializeField] private Transform _initQueueTransform;
    [SerializeField] private int _carCount;
    [SerializeField] private CarType _carType;
    [SerializeField] private Transform _initNodePoint;
    private List<Car> _cars;

    private void Start()
    {
        setQueue();
    }

    public void onInteract()
    {
        _gateDoor.DOLocalRotate(Vector3.forward * -90f, _gateDoorDuration).OnComplete(() =>
        {
            Car car = _cars[0];
            car._setInitPath(_initNodePoint);
            _cars.Remove(car);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                StartCoroutine(moveQueueRoutine());
                _gateDoor.DOLocalRotate(Vector3.zero, _gateDoorDuration);
            });

        });
    }

    private void setQueue()
    {
        GateManager instance = GateManager.getInstances();

        _cars = new List<Car>();

        for (int i = 0; i < _carCount; i++)
        {
            Transform car = Instantiate(instance.getRandomCar(_carType), _initQueueTransform).transform;
            car.localPosition = Vector3.forward * -30f * i;
            car.localRotation = Quaternion.identity;
            Car carComponent = car.GetComponent<Car>();
            carComponent.carType = _carType;
            _cars.Add(carComponent);
        }

    }

    private IEnumerator moveQueueRoutine()
    {
        float duration = 0f;
        while (duration <= 1)
        {
            for (int i = 0; i < _cars.Count; i++)
            {
                Transform car = _cars[i].transform;
                car.position += car.forward * 30f * Time.deltaTime;
            }
            duration += Time.deltaTime;
            yield return null;
        }
    }
}
