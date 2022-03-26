using System;
using UnityEngine;

public class PointsHolder : MonoBehaviour
{
    [SerializeField] private long _points;
    
    public event Action<long> onPointsChanged;

    public long Points
    {
        get => _points;
        set
        {
            if (_points == value)
                return;

            _points = value;
            onPointsChanged?.Invoke(_points);
        }
    }
}
