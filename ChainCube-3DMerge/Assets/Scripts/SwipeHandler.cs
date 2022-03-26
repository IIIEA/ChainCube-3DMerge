using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour, ISwipe
{
    public event Action<Vector2> OnSwipeStart;
    public event Action<Vector2> OnSwipe;
    public event Action<Vector2> OnSwipeEnd;

    private bool _isSwipe;
    private Vector2 _lastPosition = new Vector2();

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (_isSwipe)
            {
                _isSwipe = false;
                OnSwipeEnd?.Invoke(_lastPosition);
            }

            _lastPosition = Input.GetTouch(0).position;
            return;
        }

        if (!_isSwipe)
        {
            _isSwipe = true;
            OnSwipeStart?.Invoke(Input.GetTouch(0).position - _lastPosition);
        }

        OnSwipe?.Invoke(Input.GetTouch(0).position - _lastPosition);
        _lastPosition = Input.GetTouch(0).position;
    }
}

