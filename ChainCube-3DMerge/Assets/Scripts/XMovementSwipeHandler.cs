using System;
using UnityEngine;

public class XMovementSwipeHandler : MonoBehaviour, IMovableObjectHandler
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [Range(0.5f, 1.5f)]
    [SerializeField] private float _normalizedCoefficient = 1.0f;

    private GameObject _movableObject;
    private ISwipe _swipeDetector;

    private void Start()
    {
        _swipeDetector = GetComponent<ISwipe>();
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if (_swipeDetector == null)
            throw new NullReferenceException("Вы забыли прикрепить SwipeDetector!");

        _swipeDetector.OnSwipe += OnSwipe;
        _swipeDetector.OnSwipeEnd += OnSwipeEnd;
    }

    private void Unsubscribe()
    {
        if (_swipeDetector == null)
            return;

        _swipeDetector.OnSwipe -= OnSwipe;
    }

    private void OnSwipe(Vector2 delta)
    {
        if (_movableObject == null)
        {
            return;
        }

        if (Mathf.Abs(delta.x - Mathf.Epsilon) <= 0)
            return;

        var borderDistance = Mathf.Abs(_rightBorder.position.x - _leftBorder.position.x);
        var offset = borderDistance * _normalizedCoefficient * delta.x / Screen.width;
        var currentPos = _movableObject.transform.position;

        _movableObject.transform.position = new Vector3(currentPos.x + offset, currentPos.y, currentPos.z);

        if (_movableObject.transform.position.x > _rightBorder.position.x)
            _movableObject.transform.position = _rightBorder.transform.position;
        else if (_movableObject.transform.position.x < _leftBorder.position.x)
            _movableObject.transform.position = _leftBorder.transform.position;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        _movableObject = null;
    }

    public void Inject(GameObject dependency)
    {
        _movableObject = dependency;
    }
}
