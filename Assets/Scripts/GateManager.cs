using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarType
{
    Purple = 0,
    Yellow,

}

public class GateManager : MonoBehaviour
{
    private static GateManager _instance;
    [SerializeField] private List<GameObject> _carListPurple;
    [SerializeField] private List<GameObject> _carListYellow;

    private GateManager()
    {

    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }


    public static GateManager getInstances()
    {
        return _instance;
    }

    public GameObject getRandomCar(CarType carType)
    {
        GameObject selectedCar = null;
        switch (carType)
        {
            case CarType.Purple:
                int randP = Random.Range(0, _carListPurple.Count);
                selectedCar = _carListPurple[randP];
                break;
            case CarType.Yellow:
                int randY = Random.Range(0, _carListYellow.Count);
                selectedCar = _carListYellow[randY];
                break;

        }

        return selectedCar;
        
    }
}
