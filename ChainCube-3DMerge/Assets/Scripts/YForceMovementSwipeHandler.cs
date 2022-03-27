using UnityEngine;

public class YForceMovementSwipeHandler : MonoBehaviour, IMovableObjectHandler
{
    [SerializeField] private float _force = 1.0f;

    private Rigidbody _rigidBody;
    private ISwipe _swipeDetector;

    private void Start()
    {
        if (TryGetComponent<ISwipe>(out ISwipe iSwipe))
        {
            _swipeDetector = iSwipe;
            _swipeDetector.OnSwipeEnd += OnSwipeEnd;
        }
        else
        {
            Debug.LogError("ISwipe component in null");
        }
    }

    private void OnDestroy()
    {
        _swipeDetector.OnSwipeEnd -= OnSwipeEnd;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        if (_rigidBody == null)
            return;

        _rigidBody.AddForce(_rigidBody.transform.forward * _force, ForceMode.Impulse);
        _rigidBody = null;
    }

    public void Inject(GameObject dependency)
    {
        if (dependency.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            _rigidBody = rigidbody;
    }
}

