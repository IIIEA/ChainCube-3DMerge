using System;
using UnityEngine;

public class SwipeTouchDetector : MonoBehaviour, ISwipe
{
    [SerializeField] private InterAd _interAd;

    private int _countEndedTouches;
    private int _counTouchesToAd;
    private bool _isSwipeEnded = false;
    private Vector3 _lastPosition = new Vector2();

    public event Action<Vector2> OnSwipe;
    public event Action<Vector2> OnSwipeEnd;

    private void Start()
    {
        _counTouchesToAd = UnityEngine.Random.Range(10, 20);
    }

    private void Update()
    {
        Swipe();
    }

    private void Swipe()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    _isSwipeEnded = true;
                    break;
                case TouchPhase.Began:
                    _isSwipeEnded = false;
                    _lastPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    OnSwipe?.Invoke((Vector3)touch.position - _lastPosition);
                    _lastPosition = touch.position;
                    break;
            }
        }

        if(_isSwipeEnded == true)
        {
            _isSwipeEnded = false;

            _countEndedTouches++;
            _interAd.ShowAd(ref _countEndedTouches, ref _counTouchesToAd);

            OnSwipeEnd?.Invoke(_lastPosition);
        }
    }
}


