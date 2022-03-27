using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PointsHolder), typeof(Rigidbody), typeof(PointsHolderCollisionDetector))]
public class CollisionImpulse : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _impulseForse;
    [Min(0)]
    [SerializeField] private float _startHightOnCillision;

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

    private void OnCollisionStart(PointsHolder pointsHolder, Collision collision)
    {
        if (pointsHolder.Points == _pointsContainer.Points)
        {
            float randomValue = Random.Range(-20f, 20f);

            Vector3 randomDirection = Vector3.one * randomValue;
            Vector3 contactPoint = collision.contacts[0].point;

            transform.position = contactPoint + Vector3.up * _startHightOnCillision;
            _rigidbody.AddForce(Vector3.up * _impulseForse, ForceMode.Impulse);
            _rigidbody.AddTorque(randomDirection);
        }
    }
}

