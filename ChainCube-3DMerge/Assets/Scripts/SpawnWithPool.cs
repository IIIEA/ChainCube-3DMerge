using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWithPool : MonoBehaviour
{
    [SerializeField] private int _cubesQueueCapacity = 20;
    [SerializeField] private bool _autoQueueGrow = true;
    [SerializeField] private float _spawnDelay = 0.3f;
    [Header("Components links"), Space]
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _swipeDetectorObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ObjectDependencyInjector _cubeDependencies;

    private ISwipe _swipeDetector;
    private Queue<GameObject> _cubesQueue = new Queue<GameObject>();
    private Coroutine _spawnRoutine;
    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);

        InitializeCubesQueue();
        _swipeDetector = _swipeDetectorObject.GetComponent<ISwipe>();
        _swipeDetector.OnSwipeEnd += OnSwipeEnd;
    }

    private void OnDestroy()
    {
        _swipeDetector.OnSwipeEnd -= OnSwipeEnd;

        if (_cubesQueue.Count != 0)
        {
            foreach (var cube in _cubesQueue)
            {
                var cubeComponent = cube.GetComponent<CollisionMergePointsHolder>();
                cubeComponent.OnCubeDestroyed -= AddCubeToQueue;
            }
        }
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        if (_spawnRoutine == null)
        {
            if (_cubesQueue.Count == 0)
            {
                if (_autoQueueGrow)
                {
                    _cubesQueueCapacity++;
                    AddCubeToQueue();
                }
                else
                {
                    Debug.LogError("Pool is empty");
                }
            }
            _spawnRoutine = StartCoroutine(SpawnWithDelay());
        }
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < _cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }

    private void AddCubeToQueue()
    {
        var cube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity, this.transform);

        if (cube.TryGetComponent<CollisionMergePointsHolder>(out CollisionMergePointsHolder cubeComponent))
        {
            cube.gameObject.SetActive(false);
            _cubesQueue.Enqueue(cube);
            cubeComponent.OnCubeDestroyed += AddCubeToQueue;
        }
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return null;
        yield return _delay;
        var cube = _cubesQueue.Dequeue();
        cube.gameObject.SetActive(true);
        InjectCube(cube.gameObject);
        _spawnRoutine = null;
    }

    private void InjectCube(GameObject cube)
    {
        _cubeDependencies.GameObject = cube;
    }
}

