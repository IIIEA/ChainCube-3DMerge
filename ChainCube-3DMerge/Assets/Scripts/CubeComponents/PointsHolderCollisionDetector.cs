using System;
using UnityEngine;

public class PointsHolderCollisionDetector : MonoBehaviour
{
    public event Action<PointsHolder, Collision> OnCollisionStart;
    public event Action<PointsHolder> OnCollisionContinue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PointsHolder>(out PointsHolder pointsHolder) == false)
            return;

        OnCollisionStart?.Invoke(pointsHolder, collision);
        OnCollisionContinue?.Invoke(pointsHolder);
    }
}
