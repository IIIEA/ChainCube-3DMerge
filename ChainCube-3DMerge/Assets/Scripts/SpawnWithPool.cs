using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWithPool : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private bool _autoQueueGrow = true;
    [Min(0)]
    [SerializeField] private int _cubesPoolCapacity = 20;
    [Space]
    [SerializeField] private GameObject _swipeDetectorObject;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private CubeRepresentor _prefabCube;

    private Queue<CubeRepresentor> _cubesQueue = new Queue<CubeRepresentor>();

    public static SpawnWithPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        InitializeCubesQueue();
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < _cubesPoolCapacity; i++)
        {
            AddCubeToQueue();
        }
    }

    private void AddCubeToQueue()
    {
        var cube = Instantiate(_prefabCube, _spawnPosition.position, Quaternion.identity, transform).GetComponent<CubeRepresentor>();

        cube.gameObject.SetActive(false);
        _cubesQueue.Enqueue(cube);
    }

    public CubeRepresentor Spawn(int number, Vector3 positin)
    {
        if (_cubesQueue.Count == 0)
        {
            if (_autoQueueGrow)
            {
                _cubesPoolCapacity++;
                AddCubeToQueue();
            }
            else
            {
                return null;
            }
        }

        CubeRepresentor cube = _cubesQueue.Dequeue();
        cube.transform.position = positin;

       // cube.SetNumbe(number);
        //cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public void DestroyCube(CubeRepresentor cube)
    {
       //cube.CubeRigidbody.velocity = Vector3.zero;
       // cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
       // cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        _cubesQueue.Enqueue(cube);
    }
}


