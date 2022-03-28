using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}
