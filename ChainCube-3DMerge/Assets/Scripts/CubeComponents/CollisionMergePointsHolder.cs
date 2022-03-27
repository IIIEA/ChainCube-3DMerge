using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PointsHolderCollisionDetector), typeof(PointsHolder))]
public class CollisionMergePointsHolder : MonoBehaviour
{
    private PointsHolder _pointsContainer;
    private PointsHolderCollisionDetector _detector;

    public event UnityAction OnCubeDestroyed;

    private void Start()
    {
        _pointsContainer = GetComponent<PointsHolder>();
        _detector = GetComponent<PointsHolderCollisionDetector>();

        _detector.OnCollisionContinue += OnPointsContainerCollision;
    }

    private void OnPointsContainerCollision(PointsHolder pointsHolder)
    {
        if (pointsHolder.Points == _pointsContainer.Points)
        {
            _pointsContainer.Points *= 2;
            Destroy(pointsHolder.gameObject);
            OnCubeDestroyed?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _detector.OnCollisionContinue -= OnPointsContainerCollision;
    }
}
