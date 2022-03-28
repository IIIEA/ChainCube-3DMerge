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
            var position = transform.position;

            _pointsContainer.Points *= 2;

            Score.Instance.SpawnPopUpText(_pointsContainer.Points, position);
            Score.Instance.UpdateScore(_pointsContainer.Points);

            Destroy(pointsHolder.gameObject);
            OnCubeDestroyed?.Invoke();
        }
    }

    private void OnDestroy()
    {
        try
        {
            _detector.OnCollisionContinue -= OnPointsContainerCollision;
        }
        catch (Exception e)
        {
            Debug.LogError("NullReference collision");
        }
    }
}
