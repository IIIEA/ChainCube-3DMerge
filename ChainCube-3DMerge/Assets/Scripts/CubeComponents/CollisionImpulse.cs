using System;
using UnityEngine;

[RequireComponent(typeof(PointsHolder), typeof(Rigidbody), typeof(PointsHolderCollisionDetector))]
public class CollisionImpulse : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _impulseForse;

    private PointsHolder _pointsContainer;
    private Rigidbody _rigidbody;
    private PointsHolderCollisionDetector _detector;

    private void Start()
    {
        _pointsContainer = GetComponent<PointsHolder>();
        _rigidbody = GetComponent<Rigidbody>();
        _detector = GetComponent<PointsHolderCollisionDetector>();

        _detector.OnCollisionStart += OnCollisionStart;
    }

    private void OnDestroy()
    {
        try
        {
            _detector.OnCollisionStart -= OnCollisionStart;
        }
        catch (Exception e)
        {
            Debug.LogError("NullReference impulse");
        }
    }

    private void OnCollisionStart(PointsHolder collision)
    {
        if (collision.Points == _pointsContainer.Points)
            _rigidbody.AddForce(new Vector3(0, .3f, 1f) * _impulseForse, ForceMode.Impulse);
    }
}

