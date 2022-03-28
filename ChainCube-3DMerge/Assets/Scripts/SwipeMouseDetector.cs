using System;
using UnityEngine;

public class SwipeMouseDetector : MonoBehaviour, ISwipe
{
    [SerializeField] private InterAd _interAd;

    private int _counTouchesToAd;
    private int _countEndedTouches;
    private bool _isSwipe;
    private Vector3 _lastPosition = new Vector2();

    public event Action<Vector2> OnSwipe;
    public event Action<Vector2> OnSwipeEnd;

    private void Start()
    {
        _counTouchesToAd = UnityEngine.Random.Range(10,20);
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (_isSwipe)
            {
                _countEndedTouches++;
                _interAd.ShowAd(ref _countEndedTouches, ref _counTouchesToAd);
                    
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


