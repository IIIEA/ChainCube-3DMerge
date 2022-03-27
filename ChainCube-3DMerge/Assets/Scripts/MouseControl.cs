using System;
using UnityEngine;

public class MouseControl : MonoBehaviour, ISwipe
{
    public event Action<Vector2> OnSwipeStart;
    public event Action<Vector2> OnSwipe;
    public event Action<Vector2> OnSwipeEnd;

    private bool _isSwipe;
    private Vector3 _lastPosition = new Vector2();

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (_isSwipe)
            {
                _isSwipe = false;
                OnSwipeEnd?.Invoke(_lastPosition);
            }

            _lastPosition = Input.mousePosition;
            return;
        }

        if (!_isSwipe)
        {
            _isSwipe = true;
        }

        OnSwipe?.Invoke(Input.mousePosition - _lastPosition);
        _lastPosition = Input.mousePosition;
    }
}


