using UnityEngine;

[RequireComponent(typeof(PointsHolder))]
public class RandomPointsSetter : MonoBehaviour
{   
    [Min(0)]
    [SerializeField] private int _minDegree = 1;
    [SerializeField] private int _maxDegree = 4;

    private PointsHolder _pointsHolder;

    private const int DefaultMinDegree = 1;
    private const int DefaultMaxDegree = 4;

    private void Start()
    {
        _pointsHolder = GetComponent<PointsHolder>();
        _pointsHolder.Points = (int)Mathf.Pow(2, Random.Range(_minDegree, _maxDegree));
    }

    private void OnValidate()
    {
        if (_maxDegree < _minDegree)
        {
            _minDegree = DefaultMinDegree;
            _maxDegree = DefaultMaxDegree;
        }
    }
}
